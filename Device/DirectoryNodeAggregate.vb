Imports System.Windows.Forms
Imports CycleCharge.FormEditorPower

''' <summary>
''' Наследование от TreeNode с расширенными свойствами для упрощения манипуляции.
''' </summary>
Public Class DirectoryNodeAggregate
    Inherits TreeNode

    Public Property NodeType() As TypeNode
    Public Property KeyId() As Integer
    ''' <summary>
    ''' Подузлы имеются, но в данный момент не загружены.
    ''' </summary>
    ''' <returns></returns>
    Public Property IsSubDirectoriesAdded() As Boolean

    Public Sub New(ByVal inText As String, ByVal inNodeType As TypeNode, ByVal inKeyId As Integer)
        MyBase.New(inText)

        NodeType = inNodeType
        KeyId = inKeyId
    End Sub
End Class
