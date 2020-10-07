Imports System.Runtime.Serialization

''' <summary>
''' Линия ввода/вывода порта (0:7 или 0:31)
''' </summary>
<DataContract>
Public Class Line
    Private _IsBusy As Boolean
    ''' <summary>
    ''' Флаг доступа для управления.
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property IsEnabled As Boolean

    ''' <summary>
    ''' Флаг занятости этой линии из мощности другого агрегата.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsBusy As Boolean
        Get
            Return _IsBusy
        End Get
        Set
            _IsBusy = Value
            If LineControl IsNot Nothing Then LineControl.IsBusy = Value
        End Set
    End Property

    ''' <summary>
    ''' Номер линии
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property Number As Integer
    Public ReadOnly Property LineControl As BaseLineControl

    Public Shadows ReadOnly Property ToString() As String
        Get
            Return "line" & Number
        End Get
    End Property

    Public Property Caption As String

    Public Sub New(inNumber As Integer, inIsEnabled As Boolean)
        Number = inNumber
        IsEnabled = inIsEnabled

        If IsEnabled Then
            LineControl = New SwitchControl(inIsEnabled, Number.ToString)
        Else
            LineControl = New LedControl(inIsEnabled, Number.ToString)
        End If
    End Sub

    Public Sub New(inNumber As Integer)
        Number = inNumber
    End Sub
End Class
