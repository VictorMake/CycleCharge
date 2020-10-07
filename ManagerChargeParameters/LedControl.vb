Imports System.Drawing
Imports System.Windows.Forms
Imports NationalInstruments.UI

Public Class LedControl
    'Inherits System.Windows.Forms.UserControl
    ' закоментировать для работы в окне дизайнера
    Inherits BaseLineControl

    Public Overrides Sub SetControlEnabled(inIsEnabled As Boolean, caption As String)
        Led1.Enabled = inIsEnabled
        'Led1.Caption = caption
    End Sub

    Public Sub New(inIsEnabled As Boolean, caption As String)
        MyBase.New(inIsEnabled)

        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()

        ' Добавить код инициализации после вызова InitializeComponent().
        SetControlEnabled(inIsEnabled, caption)
    End Sub

    ' было Private Sub Led1_StateChanged(sender As Object, e As ActionEventArgs) Handles Led1.StateChanged
    Private Sub Led1_StateChanged(sender As Object, e As EventArgs) Handles Led1.StateChanged
        If InvokeRequired Then
            ' Если вызов не из UI thread, продолжить рекурсивный вызов, пока не достигнут UI thread
            Invoke(New EventHandler(Of EventArgs)(AddressOf Led1_StateChanged), sender, e)
        Else
            Value = Led1.Value
        End If
    End Sub

    Private Sub LedControl_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Dim square As Integer

        If Me.Width < Me.Height Then
            square = Me.Width
        Else
            square = Me.Height
        End If

        Led1.Location = New Point(0, 0)
        Led1.Height = square
        Led1.Width = square
    End Sub

    Friend Overrides Sub ValueStateChanged(inValue As Boolean)
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() ValueStateChanged(inValue)))
        Else
            Led1.Value = inValue
        End If
    End Sub

    Friend Overrides Sub BusyChanged(inIsBusy As Boolean)
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() BusyChanged(inIsBusy)))
        Else
            Led1.Enabled = Not inIsBusy
        End If
    End Sub
End Class
