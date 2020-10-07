Public Class UserControlCycle
    ''' <summary>
    ''' Íåïğåğûâíî
    ''' </summary>
    ''' <returns></returns>
    Public Property IsÑontinuously As Boolean

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    MessageBox.Show("Íàæàòà êíîïêà", "UserControl", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    'End Sub
    Private Sub RadioButtonÏîâòîğèòü_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonÏîâòîğèòü.CheckedChanged
        IsÑontinuously = False
        NumUpDn×èñëîÏîâòîğîâ.Visible = True
        lblĞàç.Visible = True
    End Sub

    Private Sub RadioButtonÍåïğåğûâíî_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButtonÍåïğåğûâíî.CheckedChanged
        IsÑontinuously = True
        NumUpDn×èñëîÏîâòîğîâ.Visible = False
        lblĞàç.Visible = False
    End Sub
End Class
