Friend Class CreatorPlayerCyclogram
    Inherits CreatorMdiChildrenWindow

    Protected Overrides Function CreateWindow(inMainForm As FrmMain) As IMdiChildrenWindow
        Dim newFormPlayerCyclogram As IMdiChildrenWindow = New FormPlayerCyclogram(inMainForm)
        'newFormPlayerCyclogram.Set**** (20.3)
        Return newFormPlayerCyclogram
    End Function
End Class
