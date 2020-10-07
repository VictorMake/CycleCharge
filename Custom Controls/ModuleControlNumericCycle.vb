Imports System.Windows.Forms

Public Class ModuleControlNumericCycle
    Inherits ToolStripControlHost

    Public Sub New()
        MyBase.New(New UserControlCycle())

        'Этот вызов является обязательным для конструктора компонентов.
        InitializeComponent()
    End Sub

    Public ReadOnly Property UserControlCycle() As UserControlCycle
        Get
            Return CType(CType(MyBase.Control, UserControl), UserControlCycle)
        End Get
    End Property

End Class
