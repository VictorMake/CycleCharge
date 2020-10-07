Imports System.Windows.Forms

Public Class ToolStripNumeric
    Inherits ToolStripControlHost

    '<System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New(New UserControlNumeric())
        'Этот вызов является обязательным для конструктора компонентов.
        InitializeComponent()
    End Sub

    ' привести базовый контрол к пользовательскому контролу
    Public ReadOnly Property NumericControl() As UserControlNumeric
        Get
            Return CType(CType(MyBase.Control, UserControl), UserControlNumeric)
        End Get
    End Property

    ' попытка сделать событие изменения значения UserControlNumeric
    Public Event NumericUpDownNewValue(ByVal sender As Object, ByVal e As NumericUpDownTimeNewValueEventArgs)

    Public Class NumericUpDownTimeNewValueEventArgs
        Inherits EventArgs

        Public Sub New(NewValue As Double)
            Me.NewValue = NewValue
        End Sub
        Public Property NewValue As Double
    End Class

    'Private Sub ToolStripNumeric_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
    ' при покидании контрола вызов данного события
    Private Sub ToolStripNumeric_Leave(sender As Object, e As EventArgs) Handles Me.Leave
        Dim fireNumericUpDownNewValueEventArgs As New NumericUpDownTimeNewValueEventArgs(NumericControl.NumericUpDownValue.Value)
        RaiseEvent NumericUpDownNewValue(Me, fireNumericUpDownNewValueEventArgs)
    End Sub
End Class
