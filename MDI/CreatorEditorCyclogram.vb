Friend Class CreatorEditorCyclogram
    Inherits CreatorMdiChildrenWindow

    Protected Overrides Function CreateWindow(inMainForm As FrmMain) As IMdiChildrenWindow
        Dim newFormEditorCyclogram As IMdiChildrenWindow = New FormEditorCyclogram(inMainForm)
        'newFormEditorCyclogram.Set**** (20.3)
        Return newFormEditorCyclogram
    End Function
End Class