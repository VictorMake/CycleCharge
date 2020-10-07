<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMain
    Inherits BaseForm.FrmBase

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.ImageListMenu = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusStripForm = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSSLabelStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.toolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStripForm = New System.Windows.Forms.MenuStrip()
        Me.ClassicMdiWindowMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CascadeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TileVerticalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TileHorizontalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArrangeIconsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowHTileMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowTileMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WindowPopOutMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.WindowCloseAllMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.WindowMoreWindowsMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusBarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewSimpleMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewAdvRightMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewAdvBottomMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewAdvLeftMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.ViewAppearanceMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewTabStylesMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewTabStylesClassicMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewTabStylesModernMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewTabStylesFlatHiliteMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewTabStylesAngledHiliteMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewButtonStylesMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewButtonStylesStandardMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewButtonStylesSystemMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewButtonStylesProMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.ViewShowTitleMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewShowIconsMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewShowLayoutButtonsMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewShowCloseButtonMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.SwitchToClassicMdiMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTipForm = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolBoxSplitter = New System.Windows.Forms.Splitter()
        Me.ToolBoxPanel = New System.Windows.Forms.Panel()
        Me.ToolBoxLabel = New System.Windows.Forms.Label()
        Me.StatusStripForm.SuspendLayout()
        Me.MenuStripForm.SuspendLayout()
        Me.ToolBoxPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'WindowManagerPanel1
        '
        Me.WindowManagerPanel1.Location = New System.Drawing.Point(2, 52)
        Me.WindowManagerPanel1.ShowTitle = True
        Me.WindowManagerPanel1.Size = New System.Drawing.Size(780, 56)
        '
        'ImageListMenu
        '
        Me.ImageListMenu.ImageStream = CType(resources.GetObject("ImageListMenu.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListMenu.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListMenu.Images.SetKeyName(0, "netcenter_22.ico")
        Me.ImageListMenu.Images.SetKeyName(1, "netcenter_23.ico")
        Me.ImageListMenu.Images.SetKeyName(2, "signal-1.png")
        Me.ImageListMenu.Images.SetKeyName(3, "")
        Me.ImageListMenu.Images.SetKeyName(4, "gg_connecting.png")
        '
        'StatusStripForm
        '
        Me.StatusStripForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel, Me.TSSLabelStatus, Me.toolStripStatusLabel4})
        Me.StatusStripForm.Location = New System.Drawing.Point(0, 537)
        Me.StatusStripForm.Name = "StatusStripForm"
        Me.StatusStripForm.Size = New System.Drawing.Size(784, 25)
        Me.StatusStripForm.TabIndex = 74
        Me.StatusStripForm.Text = "StatusStrip"
        '
        'ToolStripStatusLabel
        '
        Me.ToolStripStatusLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.ToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.ToolStripStatusLabel.Image = CType(resources.GetObject("ToolStripStatusLabel.Image"), System.Drawing.Image)
        Me.ToolStripStatusLabel.Name = "ToolStripStatusLabel"
        Me.ToolStripStatusLabel.Size = New System.Drawing.Size(58, 20)
        Me.ToolStripStatusLabel.Text = "Готово"
        Me.ToolStripStatusLabel.ToolTipText = "Количество подключённых шасси"
        '
        'TSSLabelStatus
        '
        Me.TSSLabelStatus.Name = "TSSLabelStatus"
        Me.TSSLabelStatus.Size = New System.Drawing.Size(676, 20)
        Me.TSSLabelStatus.Spring = True
        '
        'toolStripStatusLabel4
        '
        Me.toolStripStatusLabel4.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left
        Me.toolStripStatusLabel4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None
        Me.toolStripStatusLabel4.Name = "toolStripStatusLabel4"
        Me.toolStripStatusLabel4.Size = New System.Drawing.Size(4, 20)
        Me.toolStripStatusLabel4.Text = "toolStripStatusLabel4"
        '
        'MenuStripForm
        '
        Me.MenuStripForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClassicMdiWindowMenuItem, Me.WindowMenuItem, Me.ViewMenuItem})
        Me.MenuStripForm.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripForm.MdiWindowListItem = Me.ClassicMdiWindowMenuItem
        Me.MenuStripForm.Name = "MenuStripForm"
        Me.MenuStripForm.Size = New System.Drawing.Size(784, 24)
        Me.MenuStripForm.TabIndex = 75
        Me.MenuStripForm.Text = "MenuStrip"
        '
        'ClassicMdiWindowMenuItem
        '
        Me.ClassicMdiWindowMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CascadeToolStripMenuItem, Me.TileVerticalToolStripMenuItem, Me.TileHorizontalToolStripMenuItem, Me.CloseAllToolStripMenuItem, Me.ArrangeIconsToolStripMenuItem})
        Me.ClassicMdiWindowMenuItem.Name = "ClassicMdiWindowMenuItem"
        Me.ClassicMdiWindowMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ClassicMdiWindowMenuItem.Text = "&Окна"
        '
        'CascadeToolStripMenuItem
        '
        Me.CascadeToolStripMenuItem.Image = CType(resources.GetObject("CascadeToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CascadeToolStripMenuItem.Name = "CascadeToolStripMenuItem"
        Me.CascadeToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.CascadeToolStripMenuItem.Text = "&Каскадом"
        '
        'TileVerticalToolStripMenuItem
        '
        Me.TileVerticalToolStripMenuItem.Image = CType(resources.GetObject("TileVerticalToolStripMenuItem.Image"), System.Drawing.Image)
        Me.TileVerticalToolStripMenuItem.Name = "TileVerticalToolStripMenuItem"
        Me.TileVerticalToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.TileVerticalToolStripMenuItem.Text = "С&лева направо"
        '
        'TileHorizontalToolStripMenuItem
        '
        Me.TileHorizontalToolStripMenuItem.Image = CType(resources.GetObject("TileHorizontalToolStripMenuItem.Image"), System.Drawing.Image)
        Me.TileHorizontalToolStripMenuItem.Name = "TileHorizontalToolStripMenuItem"
        Me.TileHorizontalToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.TileHorizontalToolStripMenuItem.Text = "С&верху вниз"
        '
        'CloseAllToolStripMenuItem
        '
        Me.CloseAllToolStripMenuItem.Image = CType(resources.GetObject("CloseAllToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CloseAllToolStripMenuItem.Name = "CloseAllToolStripMenuItem"
        Me.CloseAllToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.CloseAllToolStripMenuItem.Text = "&Закрыть все"
        '
        'ArrangeIconsToolStripMenuItem
        '
        Me.ArrangeIconsToolStripMenuItem.Image = CType(resources.GetObject("ArrangeIconsToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ArrangeIconsToolStripMenuItem.Name = "ArrangeIconsToolStripMenuItem"
        Me.ArrangeIconsToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.ArrangeIconsToolStripMenuItem.Text = "&Упорядочить значки"
        '
        'WindowMenuItem
        '
        Me.WindowMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WindowHTileMenuItem, Me.WindowTileMenuItem, Me.WindowPopOutMenuItem, Me.ToolStripSeparator9, Me.WindowCloseAllMenuItem, Me.ToolStripSeparator10, Me.WindowMoreWindowsMenuItem})
        Me.WindowMenuItem.Name = "WindowMenuItem"
        Me.WindowMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.WindowMenuItem.Text = "&Окна"
        '
        'WindowHTileMenuItem
        '
        Me.WindowHTileMenuItem.Image = CType(resources.GetObject("WindowHTileMenuItem.Image"), System.Drawing.Image)
        Me.WindowHTileMenuItem.Name = "WindowHTileMenuItem"
        Me.WindowHTileMenuItem.Size = New System.Drawing.Size(355, 22)
        Me.WindowHTileMenuItem.Text = "&Новая Горизонтальная Группа Закладок"
        '
        'WindowTileMenuItem
        '
        Me.WindowTileMenuItem.Image = CType(resources.GetObject("WindowTileMenuItem.Image"), System.Drawing.Image)
        Me.WindowTileMenuItem.Name = "WindowTileMenuItem"
        Me.WindowTileMenuItem.Size = New System.Drawing.Size(355, 22)
        Me.WindowTileMenuItem.Text = "&Группировать или Разгруппировать Текущее Окно"
        '
        'WindowPopOutMenuItem
        '
        Me.WindowPopOutMenuItem.Image = CType(resources.GetObject("WindowPopOutMenuItem.Image"), System.Drawing.Image)
        Me.WindowPopOutMenuItem.Name = "WindowPopOutMenuItem"
        Me.WindowPopOutMenuItem.Size = New System.Drawing.Size(355, 22)
        Me.WindowPopOutMenuItem.Text = "&Освободить Текущее Окно"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(352, 6)
        '
        'WindowCloseAllMenuItem
        '
        Me.WindowCloseAllMenuItem.Image = CType(resources.GetObject("WindowCloseAllMenuItem.Image"), System.Drawing.Image)
        Me.WindowCloseAllMenuItem.Name = "WindowCloseAllMenuItem"
        Me.WindowCloseAllMenuItem.Size = New System.Drawing.Size(355, 22)
        Me.WindowCloseAllMenuItem.Text = "&Закрыть Все Окна"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(352, 6)
        '
        'WindowMoreWindowsMenuItem
        '
        Me.WindowMoreWindowsMenuItem.Image = CType(resources.GetObject("WindowMoreWindowsMenuItem.Image"), System.Drawing.Image)
        Me.WindowMoreWindowsMenuItem.Name = "WindowMoreWindowsMenuItem"
        Me.WindowMoreWindowsMenuItem.Size = New System.Drawing.Size(355, 22)
        Me.WindowMoreWindowsMenuItem.Text = "О&стальные окна..."
        '
        'ViewMenuItem
        '
        Me.ViewMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusBarToolStripMenuItem, Me.ViewSimpleMenuItem, Me.ViewAdvRightMenuItem, Me.ViewAdvBottomMenuItem, Me.ViewAdvLeftMenuItem, Me.ToolStripSeparator11, Me.ViewAppearanceMenuItem, Me.ToolStripSeparator12, Me.SwitchToClassicMdiMenuItem})
        Me.ViewMenuItem.Name = "ViewMenuItem"
        Me.ViewMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.ViewMenuItem.Text = "&Вид"
        '
        'StatusBarToolStripMenuItem
        '
        Me.StatusBarToolStripMenuItem.Checked = True
        Me.StatusBarToolStripMenuItem.CheckOnClick = True
        Me.StatusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.StatusBarToolStripMenuItem.Name = "StatusBarToolStripMenuItem"
        Me.StatusBarToolStripMenuItem.Size = New System.Drawing.Size(337, 22)
        Me.StatusBarToolStripMenuItem.Text = "&Строка состояния"
        '
        'ViewSimpleMenuItem
        '
        Me.ViewSimpleMenuItem.Name = "ViewSimpleMenuItem"
        Me.ViewSimpleMenuItem.Size = New System.Drawing.Size(337, 22)
        Me.ViewSimpleMenuItem.Text = "Без списка окон"
        '
        'ViewAdvRightMenuItem
        '
        Me.ViewAdvRightMenuItem.Name = "ViewAdvRightMenuItem"
        Me.ViewAdvRightMenuItem.Size = New System.Drawing.Size(337, 22)
        Me.ViewAdvRightMenuItem.Text = "Список окон Слева"
        '
        'ViewAdvBottomMenuItem
        '
        Me.ViewAdvBottomMenuItem.Name = "ViewAdvBottomMenuItem"
        Me.ViewAdvBottomMenuItem.Size = New System.Drawing.Size(337, 22)
        Me.ViewAdvBottomMenuItem.Text = "Список окон Сверху"
        '
        'ViewAdvLeftMenuItem
        '
        Me.ViewAdvLeftMenuItem.Name = "ViewAdvLeftMenuItem"
        Me.ViewAdvLeftMenuItem.Size = New System.Drawing.Size(337, 22)
        Me.ViewAdvLeftMenuItem.Text = "Список окон Справа"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(334, 6)
        '
        'ViewAppearanceMenuItem
        '
        Me.ViewAppearanceMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewTabStylesMenuItem, Me.ViewButtonStylesMenuItem, Me.ToolStripSeparator14, Me.ViewShowTitleMenuItem, Me.ViewShowIconsMenuItem, Me.ViewShowLayoutButtonsMenuItem, Me.ViewShowCloseButtonMenuItem})
        Me.ViewAppearanceMenuItem.Name = "ViewAppearanceMenuItem"
        Me.ViewAppearanceMenuItem.Size = New System.Drawing.Size(337, 22)
        Me.ViewAppearanceMenuItem.Text = "Внешний Вид"
        '
        'ViewTabStylesMenuItem
        '
        Me.ViewTabStylesMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewTabStylesClassicMenuItem, Me.ViewTabStylesModernMenuItem, Me.ViewTabStylesFlatHiliteMenuItem, Me.ViewTabStylesAngledHiliteMenuItem})
        Me.ViewTabStylesMenuItem.Name = "ViewTabStylesMenuItem"
        Me.ViewTabStylesMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.ViewTabStylesMenuItem.Text = "Стиль Вкладок"
        '
        'ViewTabStylesClassicMenuItem
        '
        Me.ViewTabStylesClassicMenuItem.Name = "ViewTabStylesClassicMenuItem"
        Me.ViewTabStylesClassicMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ViewTabStylesClassicMenuItem.Text = "Классический"
        '
        'ViewTabStylesModernMenuItem
        '
        Me.ViewTabStylesModernMenuItem.Name = "ViewTabStylesModernMenuItem"
        Me.ViewTabStylesModernMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ViewTabStylesModernMenuItem.Text = "Современный"
        '
        'ViewTabStylesFlatHiliteMenuItem
        '
        Me.ViewTabStylesFlatHiliteMenuItem.Name = "ViewTabStylesFlatHiliteMenuItem"
        Me.ViewTabStylesFlatHiliteMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ViewTabStylesFlatHiliteMenuItem.Text = "Плоский"
        '
        'ViewTabStylesAngledHiliteMenuItem
        '
        Me.ViewTabStylesAngledHiliteMenuItem.Name = "ViewTabStylesAngledHiliteMenuItem"
        Me.ViewTabStylesAngledHiliteMenuItem.Size = New System.Drawing.Size(153, 22)
        Me.ViewTabStylesAngledHiliteMenuItem.Text = "Угловой"
        '
        'ViewButtonStylesMenuItem
        '
        Me.ViewButtonStylesMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewButtonStylesStandardMenuItem, Me.ViewButtonStylesSystemMenuItem, Me.ViewButtonStylesProMenuItem})
        Me.ViewButtonStylesMenuItem.Name = "ViewButtonStylesMenuItem"
        Me.ViewButtonStylesMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.ViewButtonStylesMenuItem.Text = "Стиль Кнопок"
        '
        'ViewButtonStylesStandardMenuItem
        '
        Me.ViewButtonStylesStandardMenuItem.Name = "ViewButtonStylesStandardMenuItem"
        Me.ViewButtonStylesStandardMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.ViewButtonStylesStandardMenuItem.Text = "Стандартный"
        '
        'ViewButtonStylesSystemMenuItem
        '
        Me.ViewButtonStylesSystemMenuItem.Name = "ViewButtonStylesSystemMenuItem"
        Me.ViewButtonStylesSystemMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.ViewButtonStylesSystemMenuItem.Text = "Системный"
        '
        'ViewButtonStylesProMenuItem
        '
        Me.ViewButtonStylesProMenuItem.Name = "ViewButtonStylesProMenuItem"
        Me.ViewButtonStylesProMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.ViewButtonStylesProMenuItem.Text = "Профессиональный"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(281, 6)
        '
        'ViewShowTitleMenuItem
        '
        Me.ViewShowTitleMenuItem.Name = "ViewShowTitleMenuItem"
        Me.ViewShowTitleMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.ViewShowTitleMenuItem.Text = "Показать Ленту"
        '
        'ViewShowIconsMenuItem
        '
        Me.ViewShowIconsMenuItem.Name = "ViewShowIconsMenuItem"
        Me.ViewShowIconsMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.ViewShowIconsMenuItem.Text = "Показать Иконки"
        '
        'ViewShowLayoutButtonsMenuItem
        '
        Me.ViewShowLayoutButtonsMenuItem.Name = "ViewShowLayoutButtonsMenuItem"
        Me.ViewShowLayoutButtonsMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.ViewShowLayoutButtonsMenuItem.Text = "Показать Кнопки Управления Окнами"
        '
        'ViewShowCloseButtonMenuItem
        '
        Me.ViewShowCloseButtonMenuItem.Name = "ViewShowCloseButtonMenuItem"
        Me.ViewShowCloseButtonMenuItem.Size = New System.Drawing.Size(284, 22)
        Me.ViewShowCloseButtonMenuItem.Text = "Показать Кнопку Закрытия"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(334, 6)
        '
        'SwitchToClassicMdiMenuItem
        '
        Me.SwitchToClassicMdiMenuItem.Name = "SwitchToClassicMdiMenuItem"
        Me.SwitchToClassicMdiMenuItem.Size = New System.Drawing.Size(337, 22)
        Me.SwitchToClassicMdiMenuItem.Text = "Переключить в Классический Мультидокумент"
        '
        'ToolBoxSplitter
        '
        Me.ToolBoxSplitter.Dock = System.Windows.Forms.DockStyle.Top
        Me.ToolBoxSplitter.Location = New System.Drawing.Point(0, 48)
        Me.ToolBoxSplitter.Name = "ToolBoxSplitter"
        Me.ToolBoxSplitter.Size = New System.Drawing.Size(784, 2)
        Me.ToolBoxSplitter.TabIndex = 76
        Me.ToolBoxSplitter.TabStop = False
        '
        'ToolBoxPanel
        '
        Me.ToolBoxPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ToolBoxPanel.Controls.Add(Me.ToolBoxLabel)
        Me.ToolBoxPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.ToolBoxPanel.Location = New System.Drawing.Point(0, 24)
        Me.ToolBoxPanel.Name = "ToolBoxPanel"
        Me.ToolBoxPanel.Size = New System.Drawing.Size(784, 24)
        Me.ToolBoxPanel.TabIndex = 77
        '
        'ToolBoxLabel
        '
        Me.ToolBoxLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolBoxLabel.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ToolBoxLabel.ForeColor = System.Drawing.Color.Black
        Me.ToolBoxLabel.Location = New System.Drawing.Point(2, 2)
        Me.ToolBoxLabel.Name = "ToolBoxLabel"
        Me.ToolBoxLabel.Size = New System.Drawing.Size(775, 14)
        Me.ToolBoxLabel.TabIndex = 2
        Me.ToolBoxLabel.Text = "Toolbox"
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(784, 562)
        Me.Controls.Add(Me.ToolBoxSplitter)
        Me.Controls.Add(Me.ToolBoxPanel)
        Me.Controls.Add(Me.MenuStripForm)
        Me.Controls.Add(Me.StatusStripForm)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "FrmMain"
        Me.Tag = "CycleCharge"
        Me.Text = "Автоматизированное управление загрузками по циклограммам"
        Me.Controls.SetChildIndex(Me.WindowManagerPanel1, 0)
        Me.Controls.SetChildIndex(Me.StatusStripForm, 0)
        Me.Controls.SetChildIndex(Me.MenuStripForm, 0)
        Me.Controls.SetChildIndex(Me.ToolBoxPanel, 0)
        Me.Controls.SetChildIndex(Me.ToolBoxSplitter, 0)
        Me.StatusStripForm.ResumeLayout(False)
        Me.StatusStripForm.PerformLayout()
        Me.MenuStripForm.ResumeLayout(False)
        Me.MenuStripForm.PerformLayout()
        Me.ToolBoxPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ImageListMenu As Windows.Forms.ImageList
    Friend WithEvents StatusStripForm As Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel As Windows.Forms.ToolStripStatusLabel
    Private WithEvents TSSLabelStatus As Windows.Forms.ToolStripStatusLabel
    Private WithEvents toolStripStatusLabel4 As Windows.Forms.ToolStripStatusLabel
    Friend WithEvents MenuStripForm As Windows.Forms.MenuStrip
    Friend WithEvents ClassicMdiWindowMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CascadeToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents TileVerticalToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents TileHorizontalToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseAllToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ArrangeIconsToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents WindowMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents WindowHTileMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents WindowTileMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents WindowPopOutMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As Windows.Forms.ToolStripSeparator
    Friend WithEvents WindowCloseAllMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As Windows.Forms.ToolStripSeparator
    Friend WithEvents WindowMoreWindowsMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusBarToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewSimpleMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewAdvRightMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewAdvBottomMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewAdvLeftMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ViewAppearanceMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewTabStylesMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewTabStylesClassicMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewTabStylesModernMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewTabStylesFlatHiliteMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewTabStylesAngledHiliteMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewButtonStylesMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewButtonStylesStandardMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewButtonStylesSystemMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewButtonStylesProMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ViewShowTitleMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewShowIconsMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewShowLayoutButtonsMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewShowCloseButtonMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As Windows.Forms.ToolStripSeparator
    Friend WithEvents SwitchToClassicMdiMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTipForm As Windows.Forms.ToolTip
    Friend WithEvents ToolBoxSplitter As Windows.Forms.Splitter
    Friend WithEvents ToolBoxPanel As Windows.Forms.Panel
    Friend WithEvents ToolBoxLabel As Windows.Forms.Label
End Class
