Friend Class CreatorEditorPower
    Inherits CreatorMdiChildrenWindow

    Protected Overrides Function CreateWindow(inMainForm As FrmMain) As IMdiChildrenWindow
        Dim newFormEditorPower As IMdiChildrenWindow = New FormEditorPower(inMainForm)
        'newFormEditorPower.Set**** (20.3)
        Return newFormEditorPower
    End Function
End Class
