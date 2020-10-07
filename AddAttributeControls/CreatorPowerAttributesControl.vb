Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
''' Унаследованный класс фабричного метода, который возвращает контрол для настройки свойств Power.
''' </summary>
Friend Class CreatorPowerAttributesControl
    Inherits BaseCreatorAttributesControl

    Protected Overrides Function CreateAttributeControl(inFormEditorPower As FormEditorPower, inCaption As String) As BaseAttributesControl
        Return New PowerAttributesControl(inCaption) With {
                    .Dock = DockStyle.Fill,
                    .Location = New Point(0, 0),
                    .Name = "PowerAttributeControl1",
                    .Size = New Size(140, 109)
                }
    End Function
End Class
