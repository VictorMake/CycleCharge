Imports System.ComponentModel
Imports System.Runtime.Serialization
Imports System.Windows.Forms

''' <summary>
''' Коллекция устройств.
''' </summary>
<DataContract>
Public Class Devices
    Implements IEnumerable
    Implements IEnumerable(Of Device)

    <DataMember>
    Private Property DeviceDictionary As Dictionary(Of Integer, Device)
    Private ReadOnly DeviceList As BindingList(Of Device)

    Default Public Property Item(key As Integer) As Device
        Get
            Return DeviceDictionary(key)
        End Get
        Set(value As Device)
            DeviceDictionary(key) = value
        End Set
    End Property

    'Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    Return DeviceDictionary.GetEnumerator()
    'End Function

    'Public Function GetEnumerator() As IEnumerator(Of Device) Implements IEnumerable(Of Device).GetEnumerator
    '    Return DirectCast(DeviceList, IEnumerable(Of Device)).GetEnumerator()
    'End Function

    'Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
    '    Return DirectCast(DeviceList, IEnumerable).GetEnumerator()
    'End Function

    Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each key As Integer In DeviceDictionary.Keys.ToArray
            Yield DeviceDictionary(key)
        Next
    End Function
    '' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    '' Однако мы можем не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    '' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    Public Iterator Function GetEnumerator() As IEnumerator(Of Device) Implements IEnumerable(Of Device).GetEnumerator
        For Each key As Integer In DeviceDictionary.Keys.ToArray
            Yield DeviceDictionary(key)
        Next
    End Function

    Public Sub Add(newDevice As Device)
        If Not CheckNumber(newDevice.Number) Then Exit Sub
        DeviceDictionary.Add(newDevice.Number, newDevice)
        DeviceList.Add(newDevice)
    End Sub

    Public Sub Add(inNumber As Integer)
        If Not CheckNumber(inNumber) Then Exit Sub
        DeviceDictionary.Add(inNumber, New Device(inNumber))
        DeviceList.Add(DeviceDictionary(inNumber))
    End Sub

    Private Function CheckNumber(inNumber As Integer) As Boolean
        If DeviceDictionary.ContainsKey(inNumber) Then
            MessageBox.Show($"Имя устройства {inNumber} уже существует!", "Ошибка добавления устройства", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End If
        Return True
    End Function

    Public ReadOnly Property DevicesToArray() As Device()
        Get
            Return DeviceDictionary.Values.ToArray()
        End Get
    End Property

    Public ReadOnly Property DeviceBindingList() As BindingList(Of Device)
        Get
            Return DeviceList
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
            Return DeviceDictionary.Keys.Contains(inNumber)
        End Get
    End Property

    Public Sub New()
        DeviceDictionary = New Dictionary(Of Integer, Device)
        DeviceList = New BindingList(Of Device)
    End Sub
End Class
