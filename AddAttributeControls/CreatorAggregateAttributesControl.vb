Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
''' Унаследованный класс фабричного метода, который возвращает контрол для настройки свойств Aggregate.
''' </summary>
Friend Class CreatorAggregateAttributesControl
    Inherits BaseCreatorAttributesControl

    Protected Overrides Function CreateAttributeControl(inFormEditorPower As FormEditorPower, inCaption As String) As BaseAttributesControl

        Dim queryDevices As String() = inFormEditorPower.ComboBoxDigitalWriteTask.Items.Cast(Of String)().ToArray
        Dim queryPorts As String() = inFormEditorPower.ComboBoxPhysicalPorts.Items.Cast(Of String)().ToArray

        Return New AggregateAttributesControl(inCaption, queryDevices, queryPorts) With {
                                                               .Dock = DockStyle.Fill,
                                                               .Location = New Point(0, 0),
                                                               .Name = "AggregateAttributeControl1",
                                                               .Size = New Size(140, 109)
                }
    End Function
End Class
