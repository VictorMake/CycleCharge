Imports System.Windows.Forms

''' <summary>
''' Работа с дискретным устройством оборудования компьютера.
''' </summary>
Public Class PortWriter
    Implements IDisposable
    Implements IEnumerable

    ''' <summary>
    ''' Имя устройства
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property AggregateName As String
    ''' <summary>
    ''' Имя устройства
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DeviceName As String
    ''' <summary>
    ''' Номер порта
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property PortNumber As Integer
    ''' <summary>
    ''' Номер Модуля Корзины
    ''' </summary>
    Public Property NumberModuleChassis() As String = String.Empty

    Private mPortValueBit As Long
    ''' <summary>
    ''' Десятичное значение суммы весов битов включённых линий
    ''' </summary>
    ''' <returns></returns>
    Public Property PortValueBit As Long
        Get
            Return mPortValueBit
        End Get
        Set
            IsNeedUpdate = Value <> mPortValueBit
            mPortValueBit = Value
        End Set
    End Property

    Public Property IsNeedUpdate As Boolean

    Private Property LineDictionary As Dictionary(Of Integer, Line)

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
            'Close()
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
#End Region

    Public Sub New(inAggregateName As String, inDevName As String, inPortNumber As Integer)
        DeviceName = inDevName
        PortNumber = inPortNumber
        AggregateName = inAggregateName
        LineDictionary = New Dictionary(Of Integer, Line)
    End Sub

#Region "LineDictionary"
    Default Public Property Item(key As Integer) As Line
        Get
            Return LineDictionary(key)
        End Get
        Set(value As Line)
            LineDictionary(key) = value
        End Set
    End Property

    'Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    Return LineDictionary.GetEnumerator()
    'End Function

    ' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    ' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    ' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each key As Integer In LineDictionary.Keys.ToArray
            Yield LineDictionary(key)
        Next
    End Function

    Public Sub Add(newLine As Line)
        If Not CheckNumber(newLine.Number) Then Exit Sub
        LineDictionary.Add(newLine.Number, newLine)
    End Sub

    Public Sub Add(lineNumber As Integer)
        If Not CheckNumber(lineNumber) Then Exit Sub
        LineDictionary.Add(lineNumber, New Line(lineNumber))
    End Sub

    Private Function CheckNumber(lineNumber As Integer) As Boolean
        If lineNumber < 0 OrElse lineNumber > 31 Then
            MessageBox.Show($"Номер линии {lineNumber} должн быть от 0 до 31 !", "Ошибка добавления линии", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        If LineDictionary.ContainsKey(lineNumber) Then
            MessageBox.Show($"Имя линии {lineNumber} уже существует!", "Ошибка добавления линии", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Return True
    End Function

    Public ReadOnly Property LinesToArray() As Line()
        Get
            Return LineDictionary.Values.ToArray()
        End Get
    End Property

    Public ReadOnly Property Contains(inNumber As Integer) As Boolean
        Get
            Return LineDictionary.Keys.Contains(inNumber)
        End Get
    End Property
#End Region

    Public Shadows ReadOnly Property ToString() As String
        Get
            Return $"{DeviceName}/port{PortNumber}"
        End Get
    End Property
End Class