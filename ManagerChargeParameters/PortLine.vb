Imports System.ComponentModel
Imports System.Runtime.Serialization
Imports System.Windows.Forms

''' <summary>
''' 8 линий Port существующего порта в иерархии Port->line.
''' </summary>
<DataContract>
Public Class PortLine
    Implements IEnumerable

    Enum ByteNumber
        Byte1 = 1
        Byte2 = 2
        Byte3 = 3
        Byte4 = 4
    End Enum

    ''' <summary>
    ''' Номер слова 1:4
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property Number As Integer

    ''' <summary>
    ''' Строка в формате port0/line0:7
    ''' </summary>
    ''' <returns></returns>
    Public Shadows ReadOnly Property ToString() As String
        Get
            Return $"port{port}/line{strByte}"
        End Get
    End Property

    Private ReadOnly port As Integer
    Private ReadOnly strByte As String

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
        LineList.Add(newLine)
    End Sub

    Public Sub Add(lineNumber As Integer, inIsEnabled As Boolean)
        If Not CheckNumber(lineNumber) Then Exit Sub
        LineDictionary.Add(lineNumber, New Line(lineNumber, inIsEnabled))
        LineList.Add(LineDictionary(lineNumber))
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
#End Region

    Public Sub New(inPort As Integer, inByteNumber As ByteNumber)
        Number = inByteNumber
        port = inPort
        strByte = GetLineString(inByteNumber)
        LineDictionary = New Dictionary(Of Integer, Line)
        LineList = New BindingList(Of Line)
    End Sub
End Class
