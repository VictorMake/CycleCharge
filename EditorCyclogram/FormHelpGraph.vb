Imports System.Windows.Forms

Public Class FormHelpGraph
    ' ссылка на форму откуда вызвана
    Friend Property ParentGraphForm() As Form

    Public Sub New(ByVal inParentForm As Form)
        ' Этот вызов является обязательным для конструктора.
        InitializeComponent()
        ' Добавьте все инициализирующие действия после вызова InitializeComponent().
        Me.ParentGraphForm = inParentForm
    End Sub

    Private Sub HelpGraph_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If WindowState <> FormWindowState.Minimized Then
            SaveSetting(Reflection.Assembly.GetExecutingAssembly.GetName.Name, "Settings", "ViewTextLeft", CStr(Left))
            SaveSetting(Reflection.Assembly.GetExecutingAssembly.GetName.Name, "Settings", "ViewTextTop", CStr(Top))
            SaveSetting(Reflection.Assembly.GetExecutingAssembly.GetName.Name, "Settings", "ViewTextWidth", CStr(Width))
            SaveSetting(Reflection.Assembly.GetExecutingAssembly.GetName.Name, "Settings", "ViewTextHeight", CStr(Height))
        End If

        If TypeOf ParentGraphForm Is FormEditorCyclogram Then
            CType(ParentGraphForm, FormEditorCyclogram).TSMenuItemNavigationPanel.Checked = False
        ElseIf TypeOf ParentGraphForm Is FormPlayerCyclogram Then
            CType(ParentGraphForm, FormPlayerCyclogram).TSMenuItemShowNavigationPanel.Checked = False
        End If
    End Sub

    Private Sub HelpGraph_Load(sender As Object, e As EventArgs) Handles Me.Load
        Left = Convert.ToSingle(GetSetting(Reflection.Assembly.GetExecutingAssembly.GetName.Name, "Settings", "ViewTextLeft", CStr(0)))
        Top = Convert.ToSingle(GetSetting(Reflection.Assembly.GetExecutingAssembly.GetName.Name, "Settings", "ViewTextTop", CStr(0)))
        Width = Convert.ToSingle(GetSetting(Reflection.Assembly.GetExecutingAssembly.GetName.Name, "Settings", "ViewTextWidth", CStr(640)))
        Height = Convert.ToSingle(GetSetting(Reflection.Assembly.GetExecutingAssembly.GetName.Name, "Settings", "ViewTextHeight", CStr(480)))
    End Sub
End Class