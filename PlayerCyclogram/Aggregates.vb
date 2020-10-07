Imports System.Text
Imports System.Windows.Forms

''' <summary>
''' Представление устройство управляемое дискретными сигналами.
''' </summary>
Friend Class Aggregates
    Implements IEnumerable

#Region "Implements IEnumerable"
    Private aggregates As Dictionary(Of Integer, AggregateCycle)

    Default Public Property Item(key As Integer) As AggregateCycle
        Get
            Return aggregates(key)
        End Get
        Set(value As AggregateCycle)
            aggregates(key) = value
        End Set
    End Property

    'Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    Return aggregates.GetEnumerator()
    'End Function

    ' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    ' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    ' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each key As Integer In aggregates.Keys.ToArray
            Yield aggregates(key)
        Next
    End Function

    Public Sub Add(keyCycle As Integer, newAggregateCycle As AggregateCycle)
        If Not Exist(newAggregateCycle) Then Exit Sub
        aggregates.Add(keyCycle, newAggregateCycle)
    End Sub

    Private Function Exist(inAggregate As AggregateCycle) As Boolean
        If Contains(inAggregate) Then
            MessageBox.Show($"Имя устройства {inAggregate.NameAggregate} уже существует!", "Ошибка добавления устройства", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Return True
    End Function

    Public Function NameExist(inNameAggregate As String) As Boolean
        ' сделать проверку на неповторяемость исполнительных устройств
        ' проверить имя (по ключу проверить нельзя т.к. он уникальный
        Dim queryAggregates As IEnumerable(Of AggregateCycle) = From selectDevice In aggregates.Values
                                                                Where selectDevice.NameAggregate = inNameAggregate
                                                                Select selectDevice

        Return queryAggregates.Count > 0
    End Function

    Public ReadOnly Property AggregateCyclesToArray() As AggregateCycle()
        Get
            Return aggregates.Values.ToArray()
        End Get
    End Property

    ''' <summary>
    ''' Определяет, содержит ли последовательность указанный элемент, используя компаратор
    ''' проверки на равенство по умолчанию.
    ''' </summary>
    ''' <param name="inAggregate"></param>
    ''' <returns></returns>
    Public ReadOnly Property Contains(inAggregate As AggregateCycle) As Boolean
        Get
            Return aggregates.Values.Contains(inAggregate, New AggregateCycleComparer)
        End Get
    End Property

    Public ReadOnly Property Count As Integer
        Get
            Return aggregates.Count
        End Get
    End Property
#End Region

    Public Sub New()
        aggregates = New Dictionary(Of Integer, AggregateCycle)
    End Sub

    Public Function ContainsKey(keyCycle As Integer) As Boolean
        Return aggregates.ContainsKey(keyCycle)
    End Function

    ''' <summary>
    ''' Проверка, что для устройства определен "0" величины загрузки.
    ''' Проверка на наличие всех величин загрузок.
    ''' </summary>
    ''' <param name="inStrbldResult"></param>
    ''' <param name="isCheckCyclogramCorrect"></param>
    Public Sub CheckLinesInPort(ByRef inStrbldResult As StringBuilder, ByRef isCheckCyclogramCorrect As Boolean)
        For Each itemAggregate As AggregateCycle In aggregates.Values
            ' для устройства определен "0" величины загрузки
            Dim isZeroPowerFound As Boolean = False

            For Each itemPower As StagePower In itemAggregate.PowersToArray
                If itemPower.StageToNumeric = 0 Then
                    isZeroPowerFound = True
                    Exit For
                End If
            Next

            If Not isZeroPowerFound Then
                inStrbldResult.AppendLine($"Для устройства <{itemAggregate.NameAggregate}> не найдено нулевое значение загрузки.")
                isCheckCyclogramCorrect = False
            End If
            ' проверка на наличие всех величин загрузок
            ' (проверку имен загрузок и ее величин можно не проверять т.к. есть каскадное удаление и накладок не должно быть)
            ' или проверить по всем массивам точек перекладки на существование в УстройствоВЦикле для данного устройства
            If itemAggregate.IsPowerEmpty Then
                inStrbldResult.AppendLine($"Для устройства: <{itemAggregate.NameAggregate}> в циклограмме заданы такие величины загрузок, которые не описаны в определении устройства.")
                isCheckCyclogramCorrect = False
            End If

            CheckReuseLineInPort(itemAggregate, inStrbldResult, isCheckCyclogramCorrect)
        Next
    End Sub

    ''' <summary>
    ''' Проверка на использование одних и тех же линий портов от разных агрегатов.
    ''' </summary>
    ''' <param name="inStrbldResult"></param>
    ''' <param name="testAggregate"></param>
    Private Sub CheckReuseLineInPort(testAggregate As AggregateCycle, ByRef inStrbldResult As StringBuilder, ByRef isCheckCyclogramCorrect As Boolean)
        Dim linesInPortForAggregate As New Dictionary(Of Integer, Dictionary(Of Integer, Integer)) From {
            {testAggregate.Port, New Dictionary(Of Integer, Integer)}
        }

        ' определить сколько раз используется линия порта для образцового устройства
        For Each itemPower As StagePower In testAggregate.PowersToArray
            For Each itemLine As Line In itemPower.LinesToArray
                If Not linesInPortForAggregate(testAggregate.Port).Keys.Contains(itemLine.Number) Then
                    linesInPortForAggregate(testAggregate.Port).Add(itemLine.Number, 0) ' количество  умолчанию
                End If
                linesInPortForAggregate(testAggregate.Port)(itemLine.Number) += 1
            Next
        Next

        Dim queryAggregates As IEnumerable(Of AggregateCycle) = From anotherAggregate In aggregates.Values
                                                                Where anotherAggregate.NameAggregate <> testAggregate.NameAggregate AndAlso anotherAggregate.Target = testAggregate.Target
                                                                Select anotherAggregate
        If queryAggregates.Count > 0 Then
            For Each anotherAggregate As AggregateCycle In queryAggregates
                If linesInPortForAggregate.Keys.Contains(anotherAggregate.Port) Then
                    For Each itemPower As StagePower In anotherAggregate.PowersToArray
                        For Each itemLine As Line In itemPower.LinesToArray
                            If linesInPortForAggregate(anotherAggregate.Port).Keys.Contains(itemLine.Number) Then
                                inStrbldResult.AppendLine($"Порт с номером:<{anotherAggregate.Port}> линиия с номером:<{itemLine.Number}> используют:<{testAggregate.NameAggregate}> и <{anotherAggregate.NameAggregate}> устройства, что недопустимо.")
                                isCheckCyclogramCorrect = False
                            End If
                        Next
                    Next
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Проверка, что для агрегата определена циклограмма загрузки.
    ''' </summary>
    ''' <param name="inStrbldResult"></param>
    ''' <param name="isCheckCyclogramCorrect"></param>
    Public Sub CheckProgramExist(ByRef inStrbldResult As StringBuilder, ByRef isCheckCyclogramCorrect As Boolean)
        For Each aggregate As AggregateCycle In aggregates.Values
            If aggregate.PointsTimeDuration.Count = 0 Then
                inStrbldResult.AppendLine($"Для устройства: <{aggregate.NameAggregate}> не задана циклограмма загрузок.")
                isCheckCyclogramCorrect = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' Проверка, что имя агрегата включённое в циклограмму загрузок не было удалено или переименовано.
    ''' </summary>
    ''' <param name="inStrbldResult"></param>
    ''' <param name="isCheckCyclogramCorrect"></param>
    ''' <param name="aggregateNames"></param>
    Public Sub CheckAggregateIsRenameOrRemove(ByRef inStrbldResult As StringBuilder, ByRef isCheckCyclogramCorrect As Boolean, aggregateNames As List(Of String))
        For Each aggregate As AggregateCycle In aggregates.Values
            If Not aggregateNames.Contains(aggregate.NameAggregate) Then
                inStrbldResult.AppendLine($"Устройство: <{aggregate.NameAggregate}> включённое в циклограмму загрузок было удалено или переименовано.{Environment.NewLine}")
                isCheckCyclogramCorrect = False
            End If
        Next
    End Sub

    ''' <summary>
    ''' Подготовить коллекции для построения графиков циклограмм.
    ''' </summary>
    ''' <param name="refAllPointsTimeDuration"></param>
    ''' <param name="refAllPointsStageToNumeric"></param>
    ''' <param name="refAllSumTimeCycle"></param>
    Public Sub PrepareCollectionsForUpdateGraph(ByRef refAllPointsTimeDuration As List(Of Double()),
                                                 ByRef refAllPointsStageToNumeric As List(Of Double()),
                                                 ByRef refAllSumTimeCycle As List(Of Double))

        For Each itemAggregate As AggregateCycle In aggregates.Values
            refAllPointsTimeDuration.Add(CType(itemAggregate.PointsTimeDuration.Clone, Double()))
            refAllPointsStageToNumeric.Add(itemAggregate.PointsStageToNumericConvertToDouble)
            refAllSumTimeCycle.Add(itemAggregate.SumTimeCycle)
        Next
    End Sub

    ''' <summary>
    ''' Установить значения на линиях всех портов в соответсвии с значениями текущих мощностей всех агрегатов.
    ''' </summary>
    ''' <param name="isDefaul"></param>
    ''' <param name="currentTimeCycle"></param>
    ''' <param name="inPortWriters"></param>
    Public Sub SetAllLineByPortsOnCurrentPower(isDefaul As Boolean, currentTimeCycle As Double, inPortWriters As PortWriters)
        For Each itemAggregate As AggregateCycle In aggregates.Values
            itemAggregate.SetPower(isDefaul, currentTimeCycle)

            For Each itemLine As Line In itemAggregate(itemAggregate.CurrentPower).LinesToArray
                Dim linePortWriter As Line = inPortWriters($"{itemAggregate.Target}/port{itemAggregate.Port}")(itemLine.Number)
                linePortWriter.IsEnabled = True ' itemLine.IsEnabled
                linePortWriter.Caption = $"<{itemAggregate.NameAggregate}>:<{itemAggregate.CurrentPower}>"
            Next
        Next
    End Sub
End Class

''' <summary>
''' Компаратор сравнения класса AggregateCycle
''' </summary>
Friend Class AggregateCycleComparer
    Implements IEqualityComparer(Of AggregateCycle)

    Public Function Equals1(ByVal x As AggregateCycle, ByVal y As AggregateCycle) As Boolean Implements IEqualityComparer(Of AggregateCycle).Equals
        ' Проверить ссылается ли сравниваемый объект на теже данные
        If x Is y Then Return True

        ' Проверить не равны ли сравнивемые объекты null
        If x Is Nothing OrElse y Is Nothing Then Return False

        ' Проверить что свойства AggregateCycles эквивалентны
        Return (x.KeyAggregate = y.KeyAggregate) AndAlso (x.NameAggregate = y.NameAggregate)
    End Function

    Public Function GetHashCode1(ByVal AggregateCycle As AggregateCycle) As Integer Implements IEqualityComparer(Of AggregateCycle).GetHashCode
        ' Проверить что AggregateCycle не null.
        If AggregateCycle Is Nothing Then Return 0

        ' Получить hash code для поля Name если оно не null.
        Dim hashAggregateCycleName = If(AggregateCycle.NameAggregate Is Nothing, 0, AggregateCycle.NameAggregate.GetHashCode())

        ' Получить hash code для поля KeyAggregate
        Dim hashAggregateCycleCode = AggregateCycle.KeyAggregate.GetHashCode()

        ' Вычислить hash code скомбинировав hash
        Return hashAggregateCycleName Xor hashAggregateCycleCode
    End Function
End Class
