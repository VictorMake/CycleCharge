Imports System.Windows.Forms

Public Class ToolStripDoudble
    Inherits ToolStripControlHost

    Public Sub New()
        MyBase.New(New UserControlDouble())

        ' Этот вызов является обязательным для конструктора компонентов.
        InitializeComponent()
    End Sub

    Public ReadOnly Property NumericControl() As UserControlDouble
        Get
            Return CType(CType(MyBase.Control, UserControl), UserControlDouble)
        End Get
    End Property
End Class
