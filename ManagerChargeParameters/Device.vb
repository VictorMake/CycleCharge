Imports System.ComponentModel
Imports System.Runtime.Serialization
Imports System.Windows.Forms

''' <summary>
''' Старший в иерархии Dev->Port->line.
''' Содержит коллекцию портов.
''' </summary>
<DataContract>
Public Class Device
    Implements IEnumerable
    Implements IEnumerable(Of Port)

    <DataMember>
    Private Property PortDictionary As Dictionary(Of Integer, Port)
    Private ReadOnly PortList As BindingList(Of Port)

    Default Public Property Item(key As Integer) As Port
        Get
            Return PortDictionary(key)
        End Get
        Set(value As Port)
            PortDictionary(key) = value
        End Set
    End Property

    'Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    Return PortDictionary.GetEnumerator()
    'End Function

    'Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    For Each key As Integer In PortDictionary.Keys.ToArray
    '        Yield PortDictionary(key)
    '    Next
    'End Function

    '' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    '' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    '' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Iterator Function GetEnumerator() As IEnumerator(Of Port) Implements IEnumerable(Of Port).GetEnumerator
        For Each key As Integer In PortDictionary.Keys.ToArray
            Yield PortDictionary(key)
        Next
    End Function

    Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each key As Integer In PortDictionary.Keys.ToArray
            Yield PortDictionary(key)
        Next
    End Function

    Public Sub Add(newPort As Port)
        If Not CheckNumber(newPort.Number) Then Exit Sub
        PortDictionary.Add(newPort.Number, newPort)
        PortList.Add(newPort)
    End Sub

    Public Sub Add(inNumber As Integer)
        If Not CheckNumber(inNumber) Then Exit Sub
        PortDictionary.Add(inNumber, New Port(inNumber))
        PortList.Add(PortDictionary(inNumber))
    End Sub

    Private Function CheckNumber(lineNumber As Integer) As Boolean
        If PortDictionary.ContainsKey(lineNumber) Then
            MessageBox.Show($"Имя порта {lineNumber} уже существует!", "Ошибка добавления порта", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Return True
    End Function

    Public ReadOnly Property PortsToArray() As Port()
        Get
            Return PortDictionary.Values.ToArray()
        End Get
    End Property

    Public ReadOnly Property PortBindingList() As BindingList(Of Port)
        Get
            Return PortList
        End Get
    End Property

    Public Shadows ReadOnly Property ToString() As String
        Get
            Return "Dev" & Number
        End Get
    End Property

    ''' <summary>
    ''' Определяет, содержит ли последовательность указанный элемент, используя компаратор
    ''' проверки на равенство по умолчанию.
    ''' </summary>
    ''' <param name="inNumber"></param>
    ''' <returns></returns>
    Public ReadOnly Property Contains(inNumber As Integer) As Boolean
        Get
            Return PortDictionary.Keys.Contains(inNumber)
        End Get
    End Property

    ''' <summary>
    ''' Номер устройства
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property Number As Integer

    Public Sub New(inNumber As Integer)
        Number = inNumber
        PortDictionary = New Dictionary(Of Integer, Port)
        PortList = New BindingList(Of Port)
    End Sub
End Class
