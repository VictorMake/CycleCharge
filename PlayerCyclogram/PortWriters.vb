Imports System.Text
Imports System.Windows.Forms

''' <summary>
''' Менеджер работы с портами для всех агрегатов в циклограмме.
''' </summary>
Friend Class PortWriters
    Implements IDisposable
    Implements IEnumerable
    ''' <summary>
    ''' Коллекция класса пишущих портов.
    ''' </summary>
    ''' <returns></returns>
    Private Property PortlWriterDictionary As Dictionary(Of String, PortWriter)

    Private mDigitalSingleChannelWriters As DigitalSingleChannelWriters

    Public Sub New()
        PortlWriterDictionary = New Dictionary(Of String, PortWriter)
        mDigitalSingleChannelWriters = New DigitalSingleChannelWriters
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
            Close()
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Public Sub Close()
        Try
            mDigitalSingleChannelWriters.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
#End Region

#Region "IEnumerable Support"
    Default Public Property Item(key As String) As PortWriter
        Get
            Return PortlWriterDictionary(key)
        End Get
        Set(value As PortWriter)
            PortlWriterDictionary(key) = value
        End Set
    End Property

    'Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    Return PortlWriterDictionary.GetEnumerator()
    'End Function

    ' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    ' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    ' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each key As String In PortlWriterDictionary.Keys.ToArray
            Yield PortlWriterDictionary(key)
        Next
    End Function

    Public Sub Add(newPortlWriter As PortWriter)
        If Not CheckNumber(CStr(newPortlWriter.PortNumber)) Then Exit Sub
        PortlWriterDictionary.Add(newPortlWriter.ToString, newPortlWriter)
    End Sub

    Private Function CheckNumber(portName As String) As Boolean
        If PortlWriterDictionary.ContainsKey(portName) Then
            MessageBox.Show($"Порт с номером:{portName} уже существует!", "Ошибка добавления порта", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Return True
    End Function

    Public ReadOnly Property PortlWritersToArray() As PortWriter()
        Get
            Return PortlWriterDictionary.Values.ToArray()
        End Get
    End Property

    ''' <summary>
    ''' Определяет, содержит ли последовательность указанный элемент, используя компаратор
    ''' проверки на равенство по умолчанию.
    ''' </summary>
    ''' <param name="inPortName"></param>
    ''' <returns></returns>
    Public ReadOnly Property Contains(inPortName As String) As Boolean
        Get
            Return PortlWriterDictionary.Keys.Contains(inPortName)
        End Get
    End Property
#End Region

    ''' <summary>
    ''' Создать коллекцию всех линий для всех агрегатов включённых программу загрузок.
    ''' </summary>
    ''' <param name="inAggregates"></param>
    Public Sub LoadAllSelectedConfigurations(inAggregates As Aggregates)
        For Each itemAggregate As AggregateCycle In inAggregates
            ConfigurePorts(itemAggregate)
        Next

        mDigitalSingleChannelWriters.LoadAllSelectedConfigurations(Me)
    End Sub

    ''' <summary>
    ''' Конфигурация коллекции портов и линий в нём.
    ''' Динамическое добавление только по мере использования их в агрегатах.
    ''' </summary>
    ''' <param name="itemAggregate"></param>
    Private Sub ConfigurePorts(itemAggregate As AggregateCycle)
        ' добавить в случае отсутствия в глобальную коллекцию
        Dim newPortlWriter As PortWriter
        Dim keyPortlWriter As String = $"{itemAggregate.Target}/port{itemAggregate.Port}"

        If Contains(keyPortlWriter) Then
            newPortlWriter = PortlWriterDictionary(keyPortlWriter)
        Else
            newPortlWriter = New PortWriter(itemAggregate.NameAggregate, itemAggregate.Target, itemAggregate.Port)
        End If

        For Each itemPower As StagePower In itemAggregate.PowersToArray
            For Each itemLine As Line In itemPower.LinesToArray
                If Not newPortlWriter.Contains(itemLine.Number) Then
                    newPortlWriter.Add(itemLine.Number)
                    newPortlWriter(itemLine.Number).Caption = $"{itemLine.Number}-<{itemAggregate.NameAggregate}>:<{itemPower.StageToNumeric}>"
                End If
            Next
        Next

        If Not Contains(keyPortlWriter) Then Add(newPortlWriter)
    End Sub

    ''' <summary>
    ''' Конфигурация всех коллекций дискретных выходов.
    ''' </summary>
    ''' <param name="inStrbldResult"></param>
    Public Function ConfigureDigitalWriteTasks(inStrbldResult As StringBuilder) As Boolean
        Return mDigitalSingleChannelWriters.ConfigureDigitalWriteTasks(inStrbldResult)
    End Function

#Region "Исполнение загрузки"
    ''' <summary>
    ''' Для сконфигурированных устройств установить на портах для линий уровни по значениям соответствующего массива лигических значений.
    ''' </summary>
    Public Sub WritePortsMultiLine()
        If IsNeedUpdatePortWriters() Then mDigitalSingleChannelWriters.WritePortsMultiLine()
    End Sub

    ''' <summary>
    ''' Остановить все Task для оборудования и очистить коллекции.
    ''' </summary>
    Public Sub StopTasksDigitalOutput()
        mDigitalSingleChannelWriters.StopTasksDigitalOutput()
    End Sub

    ''' <summary>
    ''' Сбросить в 0 все линии портов.
    ''' </summary>
    Public Sub SetAllLineByPortsOnZero()
        For Each itemPortlWriter As PortWriter In Me
            For Each itemLine As Line In itemPortlWriter.LinesToArray
                itemLine.IsEnabled = False
            Next
        Next
    End Sub

    ''' <summary>
    ''' Обновить массивы выходных уровней линий только, если
    ''' десятичное значение суммы весов битов включённых линий в каком либо порту было изменено.
    ''' </summary>
    ''' <returns></returns>
    Private Function IsNeedUpdatePortWriters() As Boolean
        Dim isNeedUpdate As Boolean = False
        For Each itemPortlWriter As PortWriter In Me
            If itemPortlWriter.IsNeedUpdate Then
                isNeedUpdate = True
                Exit For
            End If
        Next

        If isNeedUpdate Then mDigitalSingleChannelWriters.UpdateDataDigitalOutArrayDictionary(Me)

        Return isNeedUpdate
    End Function
#End Region
End Class