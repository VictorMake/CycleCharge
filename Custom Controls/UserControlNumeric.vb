Public Class UserControlNumeric
    'невозможно сослаться
    'Public Event NumericUpDownNewValue(ByVal sender As Object, ByVal e As NumericUpDownNewValueEventArgs)

    'Public Class NumericUpDownNewValueEventArgs
    '    Inherits EventArgs

    '    Public Sub New(NewValue As Double)
    '        Me.NewValue = NewValue
    '    End Sub
    '    Public Property NewValue As Double
    'End Class

    'Private Sub NumericUpDownValue_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownValue.ValueChanged
    '    Dim fireNumericUpDownTimeNewValueEventArgs As New NumericUpDownNewValueEventArgs(NumericUpDownValue.Value)
    '    RaiseEvent NumericUpDownNewValue(Me, fireNumericUpDownTimeNewValueEventArgs)
    'End Sub
End Class
