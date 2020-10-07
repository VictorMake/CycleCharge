<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigurationDevices
    Inherits System.Windows.Forms.UserControl

    'Пользовательский элемент управления (UserControl) переопределяет метод Dispose для очистки списка компонентов.
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
        Me.TableLayoutPanelPort = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelCaption = New System.Windows.Forms.Label()
        Me.ButtonAllHigh = New System.Windows.Forms.Button()
        Me.ButtonAllLow = New System.Windows.Forms.Button()
        Me.LabelHigh = New System.Windows.Forms.Label()
        Me.LabelLow = New System.Windows.Forms.Label()
        Me.TableLayoutPanelByte = New System.Windows.Forms.TableLayoutPanel()
        Me.TablePanelByte1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelWordH1 = New System.Windows.Forms.Label()
        Me.LabelWordL1 = New System.Windows.Forms.Label()
        Me.TablePanelByte2 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelWordH2 = New System.Windows.Forms.Label()
        Me.LabelWordL2 = New System.Windows.Forms.Label()
        Me.TablePanelByte3 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelWordH3 = New System.Windows.Forms.Label()
        Me.LabelWordL3 = New System.Windows.Forms.Label()
        Me.TablePanelByte4 = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelWordH4 = New System.Windows.Forms.Label()
        Me.LabelWordL4 = New System.Windows.Forms.Label()
        Me.LabelState = New System.Windows.Forms.Label()
        Me.ListBoxFrameByte = New System.Windows.Forms.ListBox()
        Me.ButtonUp = New System.Windows.Forms.Button()
        Me.ButtonDown = New System.Windows.Forms.Button()
        Me.LabelSelectState = New System.Windows.Forms.Label()
        Me.ToolTipTable = New System.Windows.Forms.ToolTip(Me.components)
        Me.TableLayoutPanelList = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelTable = New System.Windows.Forms.Label()
        Me.TableLayoutPanelState = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanelByteUp = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanelAll = New System.Windows.Forms.TableLayoutPanel()
        Me.ComboBoxDevices = New System.Windows.Forms.ComboBox()
        Me.LabelPort = New System.Windows.Forms.Label()
        Me.LabelDevice = New System.Windows.Forms.Label()
        Me.ComboBoxPorts = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanelPort.SuspendLayout()
        Me.TableLayoutPanelByte.SuspendLayout()
        Me.TablePanelByte1.SuspendLayout()
        Me.TablePanelByte2.SuspendLayout()
        Me.TablePanelByte3.SuspendLayout()
        Me.TablePanelByte4.SuspendLayout()
        Me.TableLayoutPanelList.SuspendLayout()
        Me.TableLayoutPanelState.SuspendLayout()
        Me.TableLayoutPanelByteUp.SuspendLayout()
        Me.TableLayoutPanelAll.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanelPort
        '
        Me.TableLayoutPanelPort.ColumnCount = 10
        Me.TableLayoutPanelPort.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74.0!))
        Me.TableLayoutPanelPort.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanelPort.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanelPort.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanelPort.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanelPort.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanelPort.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanelPort.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanelPort.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanelPort.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141.0!))
        Me.TableLayoutPanelPort.Controls.Add(Me.LabelCaption, 1, 0)
        Me.TableLayoutPanelPort.Controls.Add(Me.ButtonAllHigh, 9, 1)
        Me.TableLayoutPanelPort.Controls.Add(Me.ButtonAllLow, 9, 2)
        Me.TableLayoutPanelPort.Controls.Add(Me.LabelHigh, 0, 1)
        Me.TableLayoutPanelPort.Controls.Add(Me.LabelLow, 0, 2)
        Me.TableLayoutPanelPort.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelPort.Location = New System.Drawing.Point(153, 23)
        Me.TableLayoutPanelPort.Name = "TableLayoutPanelPort"
        Me.TableLayoutPanelPort.RowCount = 3
        Me.TableLayoutPanelPort.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanelPort.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelPort.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelPort.Size = New System.Drawing.Size(527, 157)
        Me.TableLayoutPanelPort.TabIndex = 0
        '
        'LabelCaption
        '
        Me.LabelCaption.AutoSize = True
        Me.TableLayoutPanelPort.SetColumnSpan(Me.LabelCaption, 8)
        Me.LabelCaption.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelCaption.ForeColor = System.Drawing.Color.Blue
        Me.LabelCaption.Location = New System.Drawing.Point(77, 0)
        Me.LabelCaption.Name = "LabelCaption"
        Me.LabelCaption.Size = New System.Drawing.Size(306, 20)
        Me.LabelCaption.TabIndex = 0
        Me.LabelCaption.Text = "port0/line0:7"
        '
        'ButtonAllHigh
        '
        Me.ButtonAllHigh.Dock = System.Windows.Forms.DockStyle.Top
        Me.ButtonAllHigh.ForeColor = System.Drawing.Color.Red
        Me.ButtonAllHigh.Location = New System.Drawing.Point(396, 30)
        Me.ButtonAllHigh.Margin = New System.Windows.Forms.Padding(10)
        Me.ButtonAllHigh.Name = "ButtonAllHigh"
        Me.ButtonAllHigh.Size = New System.Drawing.Size(121, 25)
        Me.ButtonAllHigh.TabIndex = 2
        Me.ButtonAllHigh.Text = "Все высокий"
        Me.ButtonAllHigh.UseVisualStyleBackColor = True
        '
        'ButtonAllLow
        '
        Me.ButtonAllLow.Dock = System.Windows.Forms.DockStyle.Top
        Me.ButtonAllLow.ForeColor = System.Drawing.Color.Green
        Me.ButtonAllLow.Location = New System.Drawing.Point(396, 98)
        Me.ButtonAllLow.Margin = New System.Windows.Forms.Padding(10)
        Me.ButtonAllLow.Name = "ButtonAllLow"
        Me.ButtonAllLow.Size = New System.Drawing.Size(121, 25)
        Me.ButtonAllLow.TabIndex = 3
        Me.ButtonAllLow.Text = "Все низкий"
        Me.ButtonAllLow.UseVisualStyleBackColor = True
        '
        'LabelHigh
        '
        Me.LabelHigh.AutoSize = True
        Me.LabelHigh.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelHigh.ForeColor = System.Drawing.Color.Red
        Me.LabelHigh.Location = New System.Drawing.Point(3, 20)
        Me.LabelHigh.Name = "LabelHigh"
        Me.LabelHigh.Size = New System.Drawing.Size(68, 68)
        Me.LabelHigh.TabIndex = 4
        Me.LabelHigh.Text = "Высокий (1)"
        Me.LabelHigh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelLow
        '
        Me.LabelLow.AutoSize = True
        Me.LabelLow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelLow.ForeColor = System.Drawing.Color.Green
        Me.LabelLow.Location = New System.Drawing.Point(3, 88)
        Me.LabelLow.Name = "LabelLow"
        Me.LabelLow.Size = New System.Drawing.Size(68, 69)
        Me.LabelLow.TabIndex = 5
        Me.LabelLow.Text = "Низкий (0)"
        Me.LabelLow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TableLayoutPanelByte
        '
        Me.TableLayoutPanelByte.ColumnCount = 4
        Me.TableLayoutPanelByte.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanelByte.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanelByte.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanelByte.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanelByte.Controls.Add(Me.TablePanelByte1, 3, 1)
        Me.TableLayoutPanelByte.Controls.Add(Me.TablePanelByte2, 2, 1)
        Me.TableLayoutPanelByte.Controls.Add(Me.TablePanelByte3, 1, 1)
        Me.TableLayoutPanelByte.Controls.Add(Me.TablePanelByte4, 0, 1)
        Me.TableLayoutPanelByte.Controls.Add(Me.LabelState, 0, 0)
        Me.TableLayoutPanelByte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelByte.Location = New System.Drawing.Point(67, 3)
        Me.TableLayoutPanelByte.Name = "TableLayoutPanelByte"
        Me.TableLayoutPanelByte.RowCount = 2
        Me.TableLayoutPanelByte.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanelByte.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelByte.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanelByte.Size = New System.Drawing.Size(326, 65)
        Me.TableLayoutPanelByte.TabIndex = 1
        '
        'TablePanelByte1
        '
        Me.TablePanelByte1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TablePanelByte1.ColumnCount = 1
        Me.TablePanelByte1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TablePanelByte1.Controls.Add(Me.LabelWordH1, 0, 0)
        Me.TablePanelByte1.Controls.Add(Me.LabelWordL1, 0, 1)
        Me.TablePanelByte1.Location = New System.Drawing.Point(246, 23)
        Me.TablePanelByte1.Name = "TablePanelByte1"
        Me.TablePanelByte1.RowCount = 2
        Me.TablePanelByte1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TablePanelByte1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TablePanelByte1.Size = New System.Drawing.Size(77, 39)
        Me.TablePanelByte1.TabIndex = 2
        '
        'LabelWordH1
        '
        Me.LabelWordH1.AutoSize = True
        Me.LabelWordH1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelWordH1.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelWordH1.Location = New System.Drawing.Point(5, 2)
        Me.LabelWordH1.Name = "LabelWordH1"
        Me.LabelWordH1.Size = New System.Drawing.Size(67, 16)
        Me.LabelWordH1.TabIndex = 0
        Me.LabelWordH1.Text = "00000000"
        '
        'LabelWordL1
        '
        Me.LabelWordL1.AutoSize = True
        Me.LabelWordL1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelWordL1.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelWordL1.Location = New System.Drawing.Point(5, 20)
        Me.LabelWordL1.Name = "LabelWordL1"
        Me.LabelWordL1.Size = New System.Drawing.Size(67, 17)
        Me.LabelWordL1.TabIndex = 1
        Me.LabelWordL1.Text = "7      0"
        '
        'TablePanelByte2
        '
        Me.TablePanelByte2.BackColor = System.Drawing.SystemColors.Control
        Me.TablePanelByte2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TablePanelByte2.ColumnCount = 1
        Me.TablePanelByte2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TablePanelByte2.Controls.Add(Me.LabelWordH2, 0, 0)
        Me.TablePanelByte2.Controls.Add(Me.LabelWordL2, 0, 1)
        Me.TablePanelByte2.Location = New System.Drawing.Point(165, 23)
        Me.TablePanelByte2.Name = "TablePanelByte2"
        Me.TablePanelByte2.RowCount = 2
        Me.TablePanelByte2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TablePanelByte2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TablePanelByte2.Size = New System.Drawing.Size(75, 39)
        Me.TablePanelByte2.TabIndex = 3
        '
        'LabelWordH2
        '
        Me.LabelWordH2.AutoSize = True
        Me.LabelWordH2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelWordH2.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelWordH2.Location = New System.Drawing.Point(5, 2)
        Me.LabelWordH2.Name = "LabelWordH2"
        Me.LabelWordH2.Size = New System.Drawing.Size(65, 16)
        Me.LabelWordH2.TabIndex = 0
        Me.LabelWordH2.Text = "00000000"
        '
        'LabelWordL2
        '
        Me.LabelWordL2.AutoSize = True
        Me.LabelWordL2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelWordL2.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelWordL2.Location = New System.Drawing.Point(5, 20)
        Me.LabelWordL2.Name = "LabelWordL2"
        Me.LabelWordL2.Size = New System.Drawing.Size(65, 17)
        Me.LabelWordL2.TabIndex = 1
        Me.LabelWordL2.Text = "15     8"
        '
        'TablePanelByte3
        '
        Me.TablePanelByte3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TablePanelByte3.ColumnCount = 1
        Me.TablePanelByte3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TablePanelByte3.Controls.Add(Me.LabelWordH3, 0, 0)
        Me.TablePanelByte3.Controls.Add(Me.LabelWordL3, 0, 1)
        Me.TablePanelByte3.Location = New System.Drawing.Point(84, 23)
        Me.TablePanelByte3.Name = "TablePanelByte3"
        Me.TablePanelByte3.RowCount = 2
        Me.TablePanelByte3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TablePanelByte3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TablePanelByte3.Size = New System.Drawing.Size(75, 39)
        Me.TablePanelByte3.TabIndex = 4
        '
        'LabelWordH3
        '
        Me.LabelWordH3.AutoSize = True
        Me.LabelWordH3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelWordH3.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelWordH3.Location = New System.Drawing.Point(5, 2)
        Me.LabelWordH3.Name = "LabelWordH3"
        Me.LabelWordH3.Size = New System.Drawing.Size(65, 16)
        Me.LabelWordH3.TabIndex = 0
        Me.LabelWordH3.Text = "00000000"
        '
        'LabelWordL3
        '
        Me.LabelWordL3.AutoSize = True
        Me.LabelWordL3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelWordL3.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelWordL3.Location = New System.Drawing.Point(5, 20)
        Me.LabelWordL3.Name = "LabelWordL3"
        Me.LabelWordL3.Size = New System.Drawing.Size(65, 17)
        Me.LabelWordL3.TabIndex = 1
        Me.LabelWordL3.Text = "23    16"
        '
        'TablePanelByte4
        '
        Me.TablePanelByte4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset
        Me.TablePanelByte4.ColumnCount = 1
        Me.TablePanelByte4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TablePanelByte4.Controls.Add(Me.LabelWordH4, 0, 0)
        Me.TablePanelByte4.Controls.Add(Me.LabelWordL4, 0, 1)
        Me.TablePanelByte4.Location = New System.Drawing.Point(3, 23)
        Me.TablePanelByte4.Name = "TablePanelByte4"
        Me.TablePanelByte4.RowCount = 2
        Me.TablePanelByte4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TablePanelByte4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TablePanelByte4.Size = New System.Drawing.Size(75, 39)
        Me.TablePanelByte4.TabIndex = 5
        '
        'LabelWordH4
        '
        Me.LabelWordH4.AutoSize = True
        Me.LabelWordH4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelWordH4.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelWordH4.Location = New System.Drawing.Point(5, 2)
        Me.LabelWordH4.Name = "LabelWordH4"
        Me.LabelWordH4.Size = New System.Drawing.Size(65, 16)
        Me.LabelWordH4.TabIndex = 0
        Me.LabelWordH4.Text = "00000000"
        '
        'LabelWordL4
        '
        Me.LabelWordL4.AutoSize = True
        Me.LabelWordL4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelWordL4.Font = New System.Drawing.Font("Consolas", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelWordL4.Location = New System.Drawing.Point(5, 20)
        Me.LabelWordL4.Name = "LabelWordL4"
        Me.LabelWordL4.Size = New System.Drawing.Size(65, 17)
        Me.LabelWordL4.TabIndex = 1
        Me.LabelWordL4.Text = "31    24"
        '
        'LabelState
        '
        Me.LabelState.AutoSize = True
        Me.TableLayoutPanelByte.SetColumnSpan(Me.LabelState, 4)
        Me.LabelState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelState.ForeColor = System.Drawing.Color.Blue
        Me.LabelState.Location = New System.Drawing.Point(3, 0)
        Me.LabelState.Name = "LabelState"
        Me.LabelState.Size = New System.Drawing.Size(320, 20)
        Me.LabelState.TabIndex = 0
        Me.LabelState.Text = "port0 Состояние"
        '
        'ListBoxFrameByte
        '
        Me.ListBoxFrameByte.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBoxFrameByte.FormattingEnabled = True
        Me.ListBoxFrameByte.Location = New System.Drawing.Point(3, 22)
        Me.ListBoxFrameByte.Name = "ListBoxFrameByte"
        Me.TableLayoutPanelList.SetRowSpan(Me.ListBoxFrameByte, 2)
        Me.ListBoxFrameByte.Size = New System.Drawing.Size(106, 209)
        Me.ListBoxFrameByte.TabIndex = 2
        '
        'ButtonUp
        '
        Me.ButtonUp.Image = Global.CycleCharge.My.Resources.Resources.up
        Me.ButtonUp.Location = New System.Drawing.Point(115, 22)
        Me.ButtonUp.Name = "ButtonUp"
        Me.ButtonUp.Size = New System.Drawing.Size(24, 32)
        Me.ButtonUp.TabIndex = 3
        Me.ButtonUp.UseVisualStyleBackColor = True
        '
        'ButtonDown
        '
        Me.ButtonDown.Image = Global.CycleCharge.My.Resources.Resources.down
        Me.ButtonDown.Location = New System.Drawing.Point(115, 63)
        Me.ButtonDown.Name = "ButtonDown"
        Me.ButtonDown.Size = New System.Drawing.Size(24, 32)
        Me.ButtonDown.TabIndex = 4
        Me.ButtonDown.UseVisualStyleBackColor = True
        '
        'LabelSelectState
        '
        Me.LabelSelectState.AutoSize = True
        Me.LabelSelectState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelSelectState.ForeColor = System.Drawing.Color.Blue
        Me.LabelSelectState.Location = New System.Drawing.Point(3, 0)
        Me.LabelSelectState.Name = "LabelSelectState"
        Me.LabelSelectState.Size = New System.Drawing.Size(106, 19)
        Me.LabelSelectState.TabIndex = 5
        Me.LabelSelectState.Text = "Port/Line структура"
        '
        'TableLayoutPanelList
        '
        Me.TableLayoutPanelList.ColumnCount = 2
        Me.TableLayoutPanelList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanelList.Controls.Add(Me.LabelSelectState, 0, 0)
        Me.TableLayoutPanelList.Controls.Add(Me.ButtonDown, 1, 2)
        Me.TableLayoutPanelList.Controls.Add(Me.ListBoxFrameByte, 0, 1)
        Me.TableLayoutPanelList.Controls.Add(Me.ButtonUp, 1, 1)
        Me.TableLayoutPanelList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelList.Location = New System.Drawing.Point(3, 23)
        Me.TableLayoutPanelList.Name = "TableLayoutPanelList"
        Me.TableLayoutPanelList.RowCount = 3
        Me.TableLayoutPanelState.SetRowSpan(Me.TableLayoutPanelList, 2)
        Me.TableLayoutPanelList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19.0!))
        Me.TableLayoutPanelList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41.0!))
        Me.TableLayoutPanelList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelList.Size = New System.Drawing.Size(144, 234)
        Me.TableLayoutPanelList.TabIndex = 6
        '
        'LabelTable
        '
        Me.LabelTable.AutoSize = True
        Me.TableLayoutPanelState.SetColumnSpan(Me.LabelTable, 2)
        Me.LabelTable.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.LabelTable.ForeColor = System.Drawing.Color.Blue
        Me.LabelTable.Location = New System.Drawing.Point(3, 0)
        Me.LabelTable.Name = "LabelTable"
        Me.LabelTable.Size = New System.Drawing.Size(159, 13)
        Me.LabelTable.TabIndex = 7
        Me.LabelTable.Text = "3. Установить Состояние"
        '
        'TableLayoutPanelState
        '
        Me.TableLayoutPanelState.ColumnCount = 2
        Me.TableLayoutPanelState.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanelState.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelState.Controls.Add(Me.LabelTable, 0, 0)
        Me.TableLayoutPanelState.Controls.Add(Me.TableLayoutPanelList, 0, 1)
        Me.TableLayoutPanelState.Controls.Add(Me.TableLayoutPanelPort, 1, 1)
        Me.TableLayoutPanelState.Controls.Add(Me.TableLayoutPanelByteUp, 1, 2)
        Me.TableLayoutPanelState.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelState.Location = New System.Drawing.Point(102, 3)
        Me.TableLayoutPanelState.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanelState.Name = "TableLayoutPanelState"
        Me.TableLayoutPanelState.RowCount = 3
        Me.TableLayoutPanelAll.SetRowSpan(Me.TableLayoutPanelState, 4)
        Me.TableLayoutPanelState.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanelState.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelState.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77.0!))
        Me.TableLayoutPanelState.Size = New System.Drawing.Size(683, 260)
        Me.TableLayoutPanelState.TabIndex = 8
        '
        'TableLayoutPanelByteUp
        '
        Me.TableLayoutPanelByteUp.ColumnCount = 3
        Me.TableLayoutPanelByteUp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64.0!))
        Me.TableLayoutPanelByteUp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelByteUp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 131.0!))
        Me.TableLayoutPanelByteUp.Controls.Add(Me.TableLayoutPanelByte, 1, 0)
        Me.TableLayoutPanelByteUp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelByteUp.Location = New System.Drawing.Point(153, 186)
        Me.TableLayoutPanelByteUp.Name = "TableLayoutPanelByteUp"
        Me.TableLayoutPanelByteUp.RowCount = 1
        Me.TableLayoutPanelByteUp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelByteUp.Size = New System.Drawing.Size(527, 71)
        Me.TableLayoutPanelByteUp.TabIndex = 8
        '
        'TableLayoutPanelAll
        '
        Me.TableLayoutPanelAll.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble
        Me.TableLayoutPanelAll.ColumnCount = 2
        Me.TableLayoutPanelAll.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96.0!))
        Me.TableLayoutPanelAll.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelAll.Controls.Add(Me.ComboBoxDevices, 0, 1)
        Me.TableLayoutPanelAll.Controls.Add(Me.TableLayoutPanelState, 1, 0)
        Me.TableLayoutPanelAll.Controls.Add(Me.LabelPort, 0, 2)
        Me.TableLayoutPanelAll.Controls.Add(Me.LabelDevice, 0, 0)
        Me.TableLayoutPanelAll.Controls.Add(Me.ComboBoxPorts, 0, 3)
        Me.TableLayoutPanelAll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelAll.Location = New System.Drawing.Point(5, 5)
        Me.TableLayoutPanelAll.Name = "TableLayoutPanelAll"
        Me.TableLayoutPanelAll.RowCount = 4
        Me.TableLayoutPanelAll.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.TableLayoutPanelAll.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelAll.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
        Me.TableLayoutPanelAll.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelAll.Size = New System.Drawing.Size(788, 266)
        Me.TableLayoutPanelAll.TabIndex = 9
        '
        'ComboBoxDevices
        '
        Me.ComboBoxDevices.FormattingEnabled = True
        Me.ComboBoxDevices.Location = New System.Drawing.Point(6, 55)
        Me.ComboBoxDevices.Name = "ComboBoxDevices"
        Me.ComboBoxDevices.Size = New System.Drawing.Size(90, 21)
        Me.ComboBoxDevices.TabIndex = 10
        '
        'LabelPort
        '
        Me.LabelPort.AutoSize = True
        Me.LabelPort.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.LabelPort.ForeColor = System.Drawing.Color.Blue
        Me.LabelPort.Location = New System.Drawing.Point(6, 143)
        Me.LabelPort.Name = "LabelPort"
        Me.LabelPort.Size = New System.Drawing.Size(90, 28)
        Me.LabelPort.TabIndex = 11
        Me.LabelPort.Text = "2. Выбрать Порт"
        '
        'LabelDevice
        '
        Me.LabelDevice.AutoSize = True
        Me.LabelDevice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabelDevice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.LabelDevice.ForeColor = System.Drawing.Color.Blue
        Me.LabelDevice.Location = New System.Drawing.Point(6, 3)
        Me.LabelDevice.Name = "LabelDevice"
        Me.LabelDevice.Size = New System.Drawing.Size(90, 46)
        Me.LabelDevice.TabIndex = 10
        Me.LabelDevice.Text = "1. Выбрать цифровое устройство"
        '
        'ComboBoxPorts
        '
        Me.ComboBoxPorts.FormattingEnabled = True
        Me.ComboBoxPorts.Location = New System.Drawing.Point(6, 177)
        Me.ComboBoxPorts.Name = "ComboBoxPorts"
        Me.ComboBoxPorts.Size = New System.Drawing.Size(90, 21)
        Me.ComboBoxPorts.TabIndex = 12
        '
        'ConfigurationDevices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanelAll)
        Me.Name = "ConfigurationDevices"
        Me.Padding = New System.Windows.Forms.Padding(5)
        Me.Size = New System.Drawing.Size(798, 276)
        Me.TableLayoutPanelPort.ResumeLayout(False)
        Me.TableLayoutPanelPort.PerformLayout()
        Me.TableLayoutPanelByte.ResumeLayout(False)
        Me.TableLayoutPanelByte.PerformLayout()
        Me.TablePanelByte1.ResumeLayout(False)
        Me.TablePanelByte1.PerformLayout()
        Me.TablePanelByte2.ResumeLayout(False)
        Me.TablePanelByte2.PerformLayout()
        Me.TablePanelByte3.ResumeLayout(False)
        Me.TablePanelByte3.PerformLayout()
        Me.TablePanelByte4.ResumeLayout(False)
        Me.TablePanelByte4.PerformLayout()
        Me.TableLayoutPanelList.ResumeLayout(False)
        Me.TableLayoutPanelList.PerformLayout()
        Me.TableLayoutPanelState.ResumeLayout(False)
        Me.TableLayoutPanelState.PerformLayout()
        Me.TableLayoutPanelByteUp.ResumeLayout(False)
        Me.TableLayoutPanelAll.ResumeLayout(False)
        Me.TableLayoutPanelAll.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TableLayoutPanelPort As Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelCaption As Windows.Forms.Label
    Friend WithEvents ButtonAllHigh As Windows.Forms.Button
    Friend WithEvents ButtonAllLow As Windows.Forms.Button
    Friend WithEvents LabelHigh As Windows.Forms.Label
    Friend WithEvents LabelLow As Windows.Forms.Label
    Friend WithEvents TableLayoutPanelByte As Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelState As Windows.Forms.Label
    Friend WithEvents TablePanelByte1 As Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelWordH1 As Windows.Forms.Label
    Friend WithEvents LabelWordL1 As Windows.Forms.Label
    Friend WithEvents TablePanelByte2 As Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelWordH2 As Windows.Forms.Label
    Friend WithEvents LabelWordL2 As Windows.Forms.Label
    Friend WithEvents TablePanelByte3 As Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelWordH3 As Windows.Forms.Label
    Friend WithEvents LabelWordL3 As Windows.Forms.Label
    Friend WithEvents TablePanelByte4 As Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelWordH4 As Windows.Forms.Label
    Friend WithEvents LabelWordL4 As Windows.Forms.Label
    Friend WithEvents ListBoxFrameByte As Windows.Forms.ListBox
    Friend WithEvents ButtonUp As Windows.Forms.Button
    Friend WithEvents ButtonDown As Windows.Forms.Button
    Friend WithEvents LabelSelectState As Windows.Forms.Label
    Friend WithEvents ToolTipTable As Windows.Forms.ToolTip
    Friend WithEvents TableLayoutPanelList As Windows.Forms.TableLayoutPanel
    Friend WithEvents LabelTable As Windows.Forms.Label
    Friend WithEvents TableLayoutPanelState As Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanelAll As Windows.Forms.TableLayoutPanel
    Friend WithEvents ComboBoxDevices As Windows.Forms.ComboBox
    Friend WithEvents LabelPort As Windows.Forms.Label
    Friend WithEvents LabelDevice As Windows.Forms.Label
    Friend WithEvents ComboBoxPorts As Windows.Forms.ComboBox
    Friend WithEvents TableLayoutPanelByteUp As Windows.Forms.TableLayoutPanel
End Class
