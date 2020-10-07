<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormChildAux
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormChildAux))
        Me.ImageListForms = New System.Windows.Forms.ImageList(Me.components)
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.ListViewForm = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'ImageListForms
        '
        Me.ImageListForms.ImageStream = CType(resources.GetObject("ImageListForms.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListForms.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListForms.Images.SetKeyName(0, "Ni4882Task.ico")
        Me.ImageListForms.Images.SetKeyName(1, "filmstri.gif")
        Me.ImageListForms.Images.SetKeyName(2, "player_play.png")
        Me.ImageListForms.Images.SetKeyName(3, "edit_user.png")
        Me.ImageListForms.Images.SetKeyName(4, "spellcheck.png")
        Me.ImageListForms.Images.SetKeyName(5, "SINEWAVE.ICO")
        Me.ImageListForms.Images.SetKeyName(6, "netcenter_8.ico")
        Me.ImageListForms.Images.SetKeyName(7, "calc.png")
        Me.ImageListForms.Images.SetKeyName(8, "colors.png")
        Me.ImageListForms.Images.SetKeyName(9, "tab_new_bg.png")
        Me.ImageListForms.Images.SetKeyName(10, "configure.png")
        Me.ImageListForms.Images.SetKeyName(11, "mail_replayall.png")
        Me.ImageListForms.Images.SetKeyName(12, "unsortedlist1.png")
        Me.ImageListForms.Images.SetKeyName(13, "special_paste.png")
        Me.ImageListForms.Images.SetKeyName(14, "video.png")
        Me.ImageListForms.Images.SetKeyName(15, "clock.png")
        Me.ImageListForms.Images.SetKeyName(16, "AnalyzeHysteresis.ico")
        Me.ImageListForms.Images.SetKeyName(17, "configure.png")
        Me.ImageListForms.Images.SetKeyName(18, "database.png")
        '
        'LabelTitle
        '
        Me.LabelTitle.BackColor = System.Drawing.Color.LightSteelBlue
        Me.LabelTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.LabelTitle.ForeColor = System.Drawing.Color.Black
        Me.LabelTitle.Location = New System.Drawing.Point(0, 0)
        Me.LabelTitle.Name = "LabelTitle"
        Me.LabelTitle.Size = New System.Drawing.Size(184, 15)
        Me.LabelTitle.TabIndex = 2
        Me.LabelTitle.Text = "Доступные окна для загрузки"
        Me.LabelTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ListViewForm
        '
        Me.ListViewForm.CheckBoxes = True
        Me.ListViewForm.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.ListViewForm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewForm.Location = New System.Drawing.Point(0, 15)
        Me.ListViewForm.MultiSelect = False
        Me.ListViewForm.Name = "ListViewForm"
        Me.ListViewForm.Size = New System.Drawing.Size(184, 370)
        Me.ListViewForm.SmallImageList = Me.ImageListForms
        Me.ListViewForm.TabIndex = 3
        Me.ListViewForm.UseCompatibleStateImageBehavior = False
        Me.ListViewForm.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Имя Окна"
        Me.ColumnHeader1.Width = 250
        '
        'FormChildAux
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(184, 385)
        Me.ControlBox = False
        Me.Controls.Add(Me.ListViewForm)
        Me.Controls.Add(Me.LabelTitle)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FormChildAux"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Выбор Окна"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ImageListForms As System.Windows.Forms.ImageList
    Friend WithEvents LabelTitle As System.Windows.Forms.Label
    Friend WithEvents ListViewForm As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
End Class
