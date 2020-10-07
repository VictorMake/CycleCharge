Imports System.Drawing
Imports System.Windows.Forms
Imports NationalInstruments.UI

Public Class SwitchControl
    'Inherits System.Windows.Forms.UserControl
    ' закоментировать для работы в окне дизайнера
    Inherits BaseLineControl

    Public Overrides Sub SetControlEnabled(inIsEnabled As Boolean, caption As String)
        Switch1.Enabled = inIsEnabled
        Switch1.Caption = caption
    End Sub

    Public Sub New(inIsEnabled As Boolean, caption As String)
        MyBase.New(inIsEnabled)

        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()

        ' Добавить код инициализации после вызова InitializeComponent().
        SetControlEnabled(inIsEnabled, caption)
    End Sub

    'было Private Sub Switch1_StateChanged(sender As Object, e As ActionEventArgs) Handles Switch1.StateChanged
    Private Sub Switch1_StateChanged(sender As Object, e As EventArgs) Handles Switch1.StateChanged
        If InvokeRequired Then
            ' Если вызов не из UI thread, продолжить рекурсивный вызов, пока не достигнут UI thread
            Invoke(New EventHandler(Of EventArgs)(AddressOf Switch1_StateChanged), sender, e)
        Else
            Value = Switch1.Value

            Dim mSwitch As WindowsForms.Switch = CType(sender, WindowsForms.Switch)
            'Switch1.Caption = IIf(mSwitch.Value, "Вкл.", "Выкл.")

            If mSwitch.Value Then
                mSwitch.CaptionBackColor = Color.Red
            Else
                mSwitch.CaptionBackColor = Color.Transparent
                'mSwitch.CaptionBackColor = SystemColors.ActiveCaption
            End If
            'OnControlStateChanged(sender, e) ' Пользовательское управление событием
        End If
    End Sub

    Friend Overrides Sub ValueStateChanged(inValue As Boolean)
        Switch1.Value = inValue
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() ValueStateChanged(inValue)))
        Else
            Switch1.Value = inValue
        End If
    End Sub

    Friend Overrides Sub BusyChanged(inIsBusy As Boolean)
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() BusyChanged(inIsBusy)))
        Else
            Switch1.Enabled = Not inIsBusy

            If inIsBusy Then
                Switch1.OffColor = Color.Silver
            Else
                Switch1.OffColor = SystemColors.Control
            End If
        End If
    End Sub
End Class
