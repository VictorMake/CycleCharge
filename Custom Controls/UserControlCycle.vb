Public Class UserControlCycle
    ''' <summary>
    ''' ����������
    ''' </summary>
    ''' <returns></returns>
    Public Property Is�ontinuously As Boolean

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    MessageBox.Show("������ ������", "UserControl", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    'End Sub
    Private Sub RadioButton���������_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButton���������.CheckedChanged
        Is�ontinuously = False
        NumUpDn�������������.Visible = True
        lbl���.Visible = True
    End Sub

    Private Sub RadioButton����������_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadioButton����������.CheckedChanged
        Is�ontinuously = True
        NumUpDn�������������.Visible = False
        lbl���.Visible = False
    End Sub
End Class
