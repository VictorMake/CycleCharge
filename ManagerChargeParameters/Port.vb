Imports System.ComponentModel
Imports System.Runtime.Serialization
Imports System.Windows.Forms
Imports CycleCharge.PortLine

''' <summary>
''' Содержит коллекцию Line и DevicePortLine.
''' </summary>
<DataContract>
Public Class Port
    Implements IEnumerable
    Implements IEnumerable(Of Line)

#Region "LineDictionary"
    <DataMember>
    Private Property LineDictionary As Dictionary(Of Integer, Line)
    Private ReadOnly LineList As BindingList(Of Line)

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

    'Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    For Each key As Integer In LineDictionary.Keys.ToArray
    '        Yield LineDictionary(key)
    '    Next
    'End Function

    '' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    '' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    '' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Iterator Function GetEnumerator() As IEnumerator(Of Line) Implements IEnumerable(Of Line).GetEnumerator
        For Each key As Integer In LineDictionary.Keys.ToArray
            Yield LineDictionary(key)
        Next
    End Function

    Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each key As Integer In LineDictionary.Keys.ToArray
            Yield LineDictionary(key)
        Next
    End Function

    Public Sub Add(newLine As Line)
        If Not CheckNumber(newLine.Number) Then Exit Sub
        LineDictionary.Add(newLine.Number, newLine)
        LineList.Add(newLine)
        AddLineInPortLine(newLine)
    End Sub

    Public Sub Add(lineNumber As Integer, inIsEnabled As Boolean)
        If Not CheckNumber(lineNumber) Then Exit Sub
        LineDictionary.Add(lineNumber, New Line(lineNumber, inIsEnabled))
        LineList.Add(LineDictionary(lineNumber))
        AddLineInPortLine(LineDictionary(lineNumber))
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

    Public ReadOnly Property LineBindingList() As BindingList(Of Line)
        Get
            Return LineList
        End Get
    End Property

    Public ReadOnly Property Contains(inNumber As Integer) As Boolean
        Get
            Return LineDictionary.Keys.Contains(inNumber)
        End Get
    End Property
#End Region

#Region "PortLineDictionary"
    Private Property PortLineDictionary As Dictionary(Of Integer, PortLine)
    Private ReadOnly PortLineList As BindingList(Of PortLine)

    Public Property ItemPortLine(key As Integer) As PortLine
        Get
            Return PortLineDictionary(key)
        End Get
        Set(value As PortLine)
            PortLineDictionary(key) = value
        End Set
    End Property

    Public Iterator Function GetEnumeratorPortLine() As IEnumerable
        For Each key As Integer In PortLineDictionary.Keys.ToArray
            Yield PortLineDictionary(key)
        Next
    End Function

    Public Sub AddPortLine(newPortLine As PortLine)
        If Not CheckNumber(newPortLine.Number) Then Exit Sub
        PortLineDictionary.Add(newPortLine.Number, newPortLine)
        PortLineList.Add(newPortLine)
    End Sub

    Public Sub AddPortLine(inByteNumber As ByteNumber)
        If Not CheckNumber(inByteNumber) Then Exit Sub
        PortLineDictionary.Add(inByteNumber, New PortLine(Number, inByteNumber))
        PortLineList.Add(PortLineDictionary(inByteNumber))
    End Sub

    Private Function CheckNumberPortLine(inByteNumber As Integer) As Boolean
        If PortLineDictionary.ContainsKey(inByteNumber) Then
            MessageBox.Show($"Имя PortLine {inByteNumber} уже существует!", "Ошибка добавления PortLine", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Return True
    End Function

    Private Sub AddLineInPortLine(inLine As Line)
        Dim lineNumber As Integer = inLine.Number
        Dim mByteNumber As ByteNumber

        If lineNumber <= 7 Then
            mByteNumber = ByteNumber.Byte1
        ElseIf lineNumber > 7 AndAlso lineNumber <= 15 Then
            mByteNumber = ByteNumber.Byte2
        ElseIf lineNumber > 15 AndAlso lineNumber <= 23 Then
            mByteNumber = ByteNumber.Byte3
        ElseIf lineNumber > 23 Then
            mByteNumber = ByteNumber.Byte4
        End If
        AddLineInPortLine(inLine, mByteNumber)
    End Sub

    Private Sub AddLineInPortLine(inLine As Line, inByteNumber As ByteNumber)
        If Not PortLineDictionary.ContainsKey(inByteNumber) Then
            PortLineDictionary.Add(inByteNumber, New PortLine(Number, inByteNumber))
        End If
        PortLineDictionary(inByteNumber).Add(inLine)
    End Sub

    Public ReadOnly Property PortLinesToArray() As PortLine()
        Get
            Return PortLineDictionary.Values.ToArray()
        End Get
    End Property

    Public ReadOnly Property PortLineBindingList() As BindingList(Of PortLine)
        Get
            Return PortLineList
        End Get
    End Property
#End Region

    ''' <summary>
    ''' Номер порта
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property Number As Integer

    Public Shadows ReadOnly Property ToString() As String
        Get
            Return "port" & Number
        End Get
    End Property

    Public Sub New(inNumber As Integer)
        Number = inNumber
        LineDictionary = New Dictionary(Of Integer, Line)
        LineList = New BindingList(Of Line)
        PortLineDictionary = New Dictionary(Of Integer, PortLine)
        PortLineList = New BindingList(Of PortLine)
    End Sub
End Class
