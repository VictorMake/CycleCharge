Imports System.Windows.Forms

''' <summary>
''' Мощность ступени загрузки.
''' </summary>
Public Class StagePower
    Implements IEnumerable

#Region "Implements IEnumerable"
    Private Lines As Dictionary(Of Integer, Line)

    Default Public ReadOnly Property Item(ByVal key As Integer) As Line
        Get
            Return Lines.Item(key)
        End Get
    End Property

    'Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    Return Lines.GetEnumerator
    'End Function

    ' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    ' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    ' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each key As Integer In Lines.Keys.ToArray
            Yield Lines(key)
        Next
    End Function

    'Public Sub Remove(ByRef Key As Integer)
    '    ' удаление по номеру или имени или объекту?
    '    ' если целый тип то по плавающему индексу, а если строковый то по ключу
    '    Lines.Remove(Key)
    'End Sub

    'Public Sub Clear()
    '    ''здесь удаление по ключу, а он строковый
    '    'не работает
    '    'Dim oneInst As Условие
    '    'For Each oneInst In mCol
    '    '    mCol.Remove(oneInst.ID.ToString)
    '    'Next
    '    'Dim I As Integer
    '    'With mCol
    '    '    For I = .Count To 1 Step -1
    '    '        .Remove(I)
    '    '    Next
    '    'End With
    '    Lines.Clear()
    'End Sub

    'Public ReadOnly Property Count() As Integer
    '    Get
    '        Return Lines.Count()
    '    End Get
    'End Property

    Public Sub Add(newLine As Line)
        If Not CheckNumber(newLine.Number) Then Exit Sub
        Lines.Add(newLine.Number, newLine)
    End Sub

    Public Sub Add(lineNumber As Integer)
        If Not CheckNumber(lineNumber) Then Exit Sub
        Lines.Add(lineNumber, New Line(lineNumber))
    End Sub

    Private Function CheckNumber(lineNumber As Integer) As Boolean
        If lineNumber < 0 OrElse lineNumber > 31 Then
            MessageBox.Show($"Номер линии {lineNumber} должн быть от 0 до 31 !", "Ошибка добавления линии", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        If Lines.ContainsKey(lineNumber) Then
            MessageBox.Show($"Имя линии {lineNumber} уже существует!", "Ошибка добавления линии", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Return True
    End Function

    Public ReadOnly Property LinesToArray() As Line()
        Get
            Return Lines.Values.ToArray()
        End Get
    End Property

    Public ReadOnly Property Contains(inNumber As Integer) As Boolean
        Get
            Return Lines.Keys.Contains(inNumber)
        End Get
    End Property
#End Region

    ''' <summary>
    ''' Величина Загрузки
    ''' </summary>
    ''' <returns></returns>
    Public Property StageToNumeric() As Single

    Public Sub New(ByVal newStageToNumeric As Single)
        StageToNumeric = newStageToNumeric
        Lines = New Dictionary(Of Integer, Line)
    End Sub

    Protected Overrides Sub Finalize()
        Lines = Nothing
        MyBase.Finalize()
    End Sub
End Class
