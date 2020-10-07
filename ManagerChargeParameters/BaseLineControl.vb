Imports System.ComponentModel

Public MustInherit Class BaseLineControl
    Implements INotifyPropertyChanged

    ''' <summary>
    ''' Флаг доступа для управления.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsEnabled As Boolean
    ''' <summary>
    ''' Флаг занятости этой линии из мощности другого агрегата.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsBusy As Boolean
        Get
            Return mIsBusy
        End Get
        Set
            mIsBusy = Value
            BusyChanged(Value)
        End Set
    End Property

    Private mValue As Boolean
    Private mIsBusy As Boolean

    Public Property Value As Boolean
        Get
            Return mValue
        End Get
        Set
            mValue = Value
            ValueStateChanged(Value)
            'OnPropertyChanged(NameOf(Me.Value))
            OnPropertyChanged()
        End Set
    End Property

    ''' <summary>
    ''' Контрол наследующий данный базовый класс обязан реализовать этот метод
    ''' для установки значения контрола
    ''' </summary>
    ''' <param name="inIsEnabled"></param>
    Public MustOverride Sub SetControlEnabled(inIsEnabled As Boolean, caption As String)
    Friend MustOverride Sub ValueStateChanged(inValue As Boolean)
    Friend MustOverride Sub BusyChanged(inIsBusy As Boolean)

#Region "Пользовательское управление событием"
    'Public Event ControlStateChanged(sender As Object, e As ActionEventArgs)

    'Friend Sub OnControlStateChanged(sender As Object, e As ActionEventArgs)
    '    RaiseEvent ControlStateChanged(sender, e)
    'End Sub
#End Region

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(Optional ByVal propertyName As String = Nothing)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Protected Sub New()
        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()
        ' Добавить код инициализации после вызова InitializeComponent().
    End Sub

    Public Sub New(inIsEnabled As Boolean)
        Me.New

        IsEnabled = inIsEnabled
    End Sub
End Class
