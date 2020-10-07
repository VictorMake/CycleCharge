<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormEditorCyclogram
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If tsNumericUpDownTimeDuration IsNot Nothing Then
                    tsNumericUpDownTimeDuration.Dispose()
                    tsNumericUpDownTimeDuration = Nothing
                End If
                If TempPointAnnotation IsNot Nothing Then
                    TempPointAnnotation.Dispose()
                    TempPointAnnotation = Nothing
                End If
                If mFont IsNot Nothing Then
                    mFont.Dispose()
                    mFont = Nothing
                End If
                If mboldFont IsNot Nothing Then
                    mboldFont.Dispose()
                    mboldFont = Nothing
                End If
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormEditorCyclogram))
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.StatusStripForm = New System.Windows.Forms.StatusStrip()
        Me.LabelDevicesCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSStatusLabelMessage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SplitContainerForm = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerTree = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerTypeEngine = New System.Windows.Forms.SplitContainer()
        Me.DataGridView1ТипИзделия = New System.Windows.Forms.DataGridView()
        Me.ТипИзделияDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KeyТипИзделияDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ОписаниеDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BindingSource1ТипИзделия = New System.Windows.Forms.BindingSource(Me.components)
        Me.CycleDataSet1 = New CycleCharge.CycleDataSet()
        Me.BindingNavigator1ТипИзделия = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorCountItem = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.BNDeleteTypeEngine = New System.Windows.Forms.ToolStripButton()
        Me.BNSaveTypeEngine = New System.Windows.Forms.ToolStripButton()
        Me.HeaderStripEngine = New CycleCharge.HeaderStrip(Me.components)
        Me.ToolStripLabelEngine = New System.Windows.Forms.ToolStripLabel()
        Me.DataGridView2ПрограммаИспытаний = New System.Windows.Forms.DataGridView()
        Me.KeyТипИзделияDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KeyПрограммаИспытанийDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ИмяПрограммыDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ОписаниеDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BindingSource2ПрограммаИспытаний = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigator2ПрограммаИспытаний = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorAddNewItem1 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorCountItem1 = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem1 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem1 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem1 = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem1 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem1 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.BNDeleteTestPrograms = New System.Windows.Forms.ToolStripButton()
        Me.BNSaveTestPrograms = New System.Windows.Forms.ToolStripButton()
        Me.HeaderStripProgram = New CycleCharge.HeaderStrip(Me.components)
        Me.ToolStripLabelProgram = New System.Windows.Forms.ToolStripLabel()
        Me.DataGridView3ЦиклУправИсполУстр = New System.Windows.Forms.DataGridView()
        Me.KeyПрограммаИспытанийDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KeyЦиклЗагрузкиDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ИмяУстройстваDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ИмяУстройстваNewDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.BindingSource3ЦиклУправИсполУстр = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigator3ЦиклУправИсполУстр = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorAddNewItem2 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorCountItem2 = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem2 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem2 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem2 = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem2 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem2 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.BNDeleteSycleStage = New System.Windows.Forms.ToolStripButton()
        Me.BNSaveSycleDevices = New System.Windows.Forms.ToolStripButton()
        Me.HeaderStripDevice = New CycleCharge.HeaderStrip(Me.components)
        Me.ToolStripLabelDevice = New System.Windows.Forms.ToolStripLabel()
        Me.SplitContainerEditor = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerGraf = New System.Windows.Forms.SplitContainer()
        Me.ScatterGraphCycle = New NationalInstruments.UI.WindowsForms.ScatterGraph()
        Me.XyCursor1 = New NationalInstruments.UI.XYCursor()
        Me.ScatterPlot2 = New NationalInstruments.UI.ScatterPlot()
        Me.XAxis2 = New NationalInstruments.UI.XAxis()
        Me.YAxis2 = New NationalInstruments.UI.YAxis()
        Me.PanelCycleAndLayoutPanel = New System.Windows.Forms.Panel()
        Me.ScatterGraphSelectDevice = New NationalInstruments.UI.WindowsForms.ScatterGraph()
        Me.ScatterPlot1 = New NationalInstruments.UI.ScatterPlot()
        Me.XAxis1 = New NationalInstruments.UI.XAxis()
        Me.YAxis1 = New NationalInstruments.UI.YAxis()
        Me.TableLayoutPanelAttributes = New System.Windows.Forms.TableLayoutPanel()
        Me.LabelTimeAllCyrcle = New System.Windows.Forms.Label()
        Me.TSCycle = New System.Windows.Forms.ToolStrip()
        Me.ButtonShowMore = New System.Windows.Forms.ToolStripButton()
        Me.LabelMinMaxDevice = New System.Windows.Forms.Label()
        Me.LabelReplied = New System.Windows.Forms.Label()
        Me.LabelMinMax = New System.Windows.Forms.Label()
        Me.LabelCaption = New System.Windows.Forms.Label()
        Me.LabelDescription = New System.Windows.Forms.Label()
        Me.LabelNameChargeParameter = New System.Windows.Forms.Label()
        Me.LabelUnitOfMeasure = New System.Windows.Forms.Label()
        Me.SplitContainerCycle = New System.Windows.Forms.SplitContainer()
        Me.DataGridView4ПерекладкиЦикла = New System.Windows.Forms.DataGridView()
        Me.KeyЦиклЗагрузкиDataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KeyПерекладкиЦиклаDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.KeyУстройстваDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ВеличинаЗагрузкиDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ЧисловоеЗначениеDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TimeActionDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BindingSource4ПерекладкиЦикла = New System.Windows.Forms.BindingSource(Me.components)
        Me.BindingNavigator4ПерекладкиЦикла = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.BindingNavigatorAddNewItem3 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorCountItem3 = New System.Windows.Forms.ToolStripLabel()
        Me.BNDeleteCycleShelf = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveFirstItem3 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem3 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorPositionItem3 = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem3 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem3 = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.BNSaveSycleStage = New System.Windows.Forms.ToolStripButton()
        Me.ListViewCycle = New System.Windows.Forms.ListView()
        Me.ImageListCycle = New System.Windows.Forms.ImageList(Me.components)
        Me.HeaderStripCycle = New CycleCharge.HeaderStrip(Me.components)
        Me.ToolStripLabelCycle = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripEditCycle = New System.Windows.Forms.ToolStrip()
        Me.TSLabelStage = New System.Windows.Forms.ToolStripLabel()
        Me.TSSeparator31 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSComboBoxStage = New System.Windows.Forms.ToolStripComboBox()
        Me.TSLabelUnit = New System.Windows.Forms.ToolStripLabel()
        Me.TSComboBoxTimeUnit = New System.Windows.Forms.ToolStripComboBox()
        Me.TSSeparator32 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSLabelTime = New System.Windows.Forms.ToolStripLabel()
        Me.TSSeparator33 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSButtonAddRecord = New System.Windows.Forms.ToolStripButton()
        Me.TSButtonEditRecord = New System.Windows.Forms.ToolStripButton()
        Me.TSSeparator34 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSButtonUp = New System.Windows.Forms.ToolStripButton()
        Me.TSButtonDown = New System.Windows.Forms.ToolStripButton()
        Me.TSSeparator35 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSButtonCutRecords = New System.Windows.Forms.ToolStripButton()
        Me.TSButtonCopyRecords = New System.Windows.Forms.ToolStripButton()
        Me.TSButtonPasteRecords = New System.Windows.Forms.ToolStripButton()
        Me.TSButtonClearRecords = New System.Windows.Forms.ToolStripButton()
        Me.TSSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabelSpase = New System.Windows.Forms.ToolStripLabel()
        Me.TSButtonSaveCycle = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripForm = New System.Windows.Forms.ToolStrip()
        Me.TSButtonNewItems = New System.Windows.Forms.ToolStripSplitButton()
        Me.TSMenuItemNewType = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemNewProgramm = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSSeparator20 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSMenuItemNewDevice = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemNewCycle = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSSeparator25 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSButtonFindAndReplace = New System.Windows.Forms.ToolStripSplitButton()
        Me.TSSeparator27 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSSeparator28 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSComboBoxFind = New System.Windows.Forms.ToolStripComboBox()
        Me.TSSeparator29 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSSplitButtonDropItems = New System.Windows.Forms.ToolStripSplitButton()
        Me.TSSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSMenuItemToolbars = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSMenuItemExpandCollapseGroups = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemShowNavigation = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemShowAllGraphs = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemNavigationPanel = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemView = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemStatusStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStripForm = New System.Windows.Forms.MenuStrip()
        Me.TimerUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.ErrorProvider4ПерекладкиЦикла = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTipForm = New System.Windows.Forms.ToolTip(Me.components)
        Me.ContextMenuAnnotation = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TSMenuItemDeletePause = New System.Windows.Forms.ToolStripMenuItem()
        Me.ТипИзделия1TableAdapter = New CycleCharge.CycleDataSetTableAdapters.ТипИзделия1TableAdapter()
        Me.ПрограммаИспытаний2TableAdapter = New CycleCharge.CycleDataSetTableAdapters.ПрограммаИспытаний2TableAdapter()
        Me.ЦиклЗагрузки3TableAdapter = New CycleCharge.CycleDataSetTableAdapters.ЦиклЗагрузки3TableAdapter()
        Me.ПерекладкиЦикла4TableAdapter = New CycleCharge.CycleDataSetTableAdapters.ПерекладкиЦикла4TableAdapter()
        Me.StatusStripForm.SuspendLayout()
        CType(Me.SplitContainerForm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerForm.Panel1.SuspendLayout()
        Me.SplitContainerForm.Panel2.SuspendLayout()
        Me.SplitContainerForm.SuspendLayout()
        CType(Me.SplitContainerTree, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerTree.Panel1.SuspendLayout()
        Me.SplitContainerTree.Panel2.SuspendLayout()
        Me.SplitContainerTree.SuspendLayout()
        CType(Me.SplitContainerTypeEngine, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerTypeEngine.Panel1.SuspendLayout()
        Me.SplitContainerTypeEngine.Panel2.SuspendLayout()
        Me.SplitContainerTypeEngine.SuspendLayout()
        CType(Me.DataGridView1ТипИзделия, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1ТипИзделия, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CycleDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator1ТипИзделия, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator1ТипИзделия.SuspendLayout()
        Me.HeaderStripEngine.SuspendLayout()
        CType(Me.DataGridView2ПрограммаИспытаний, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource2ПрограммаИспытаний, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator2ПрограммаИспытаний, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator2ПрограммаИспытаний.SuspendLayout()
        Me.HeaderStripProgram.SuspendLayout()
        CType(Me.DataGridView3ЦиклУправИсполУстр, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource3ЦиклУправИсполУстр, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator3ЦиклУправИсполУстр, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator3ЦиклУправИсполУстр.SuspendLayout()
        Me.HeaderStripDevice.SuspendLayout()
        CType(Me.SplitContainerEditor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerEditor.Panel1.SuspendLayout()
        Me.SplitContainerEditor.Panel2.SuspendLayout()
        Me.SplitContainerEditor.SuspendLayout()
        CType(Me.SplitContainerGraf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerGraf.Panel1.SuspendLayout()
        Me.SplitContainerGraf.Panel2.SuspendLayout()
        Me.SplitContainerGraf.SuspendLayout()
        CType(Me.ScatterGraphCycle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XyCursor1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelCycleAndLayoutPanel.SuspendLayout()
        CType(Me.ScatterGraphSelectDevice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanelAttributes.SuspendLayout()
        Me.TSCycle.SuspendLayout()
        CType(Me.SplitContainerCycle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerCycle.Panel1.SuspendLayout()
        Me.SplitContainerCycle.Panel2.SuspendLayout()
        Me.SplitContainerCycle.SuspendLayout()
        CType(Me.DataGridView4ПерекладкиЦикла, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource4ПерекладкиЦикла, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingNavigator4ПерекладкиЦикла, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BindingNavigator4ПерекладкиЦикла.SuspendLayout()
        Me.HeaderStripCycle.SuspendLayout()
        Me.ToolStripEditCycle.SuspendLayout()
        Me.ToolStripForm.SuspendLayout()
        Me.MenuStripForm.SuspendLayout()
        CType(Me.ErrorProvider4ПерекладкиЦикла, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuAnnotation.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStripForm
        '
        Me.StatusStripForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LabelDevicesCount, Me.TSStatusLabelMessage})
        Me.StatusStripForm.Location = New System.Drawing.Point(0, 814)
        Me.StatusStripForm.Name = "StatusStripForm"
        Me.StatusStripForm.Size = New System.Drawing.Size(1070, 24)
        Me.StatusStripForm.TabIndex = 0
        Me.StatusStripForm.Text = "19 Items"
        '
        'LabelDevicesCount
        '
        Me.LabelDevicesCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.LabelDevicesCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.LabelDevicesCount.Name = "LabelDevicesCount"
        Me.LabelDevicesCount.Padding = New System.Windows.Forms.Padding(0, 0, 4, 0)
        Me.LabelDevicesCount.Size = New System.Drawing.Size(76, 19)
        Me.LabelDevicesCount.Text = "{0} записей"
        '
        'TSStatusLabelMessage
        '
        Me.TSStatusLabelMessage.Name = "TSStatusLabelMessage"
        Me.TSStatusLabelMessage.Size = New System.Drawing.Size(979, 19)
        Me.TSStatusLabelMessage.Spring = True
        '
        'SplitContainerForm
        '
        Me.SplitContainerForm.BackColor = System.Drawing.Color.Transparent
        Me.SplitContainerForm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerForm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerForm.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerForm.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainerForm.Name = "SplitContainerForm"
        '
        'SplitContainerForm.Panel1
        '
        Me.SplitContainerForm.Panel1.Controls.Add(Me.SplitContainerTree)
        Me.SplitContainerForm.Panel1.Padding = New System.Windows.Forms.Padding(3, 3, 0, 3)
        '
        'SplitContainerForm.Panel2
        '
        Me.SplitContainerForm.Panel2.Controls.Add(Me.SplitContainerEditor)
        Me.SplitContainerForm.Size = New System.Drawing.Size(1070, 790)
        Me.SplitContainerForm.SplitterDistance = 290
        Me.SplitContainerForm.TabIndex = 0
        Me.SplitContainerForm.TabStop = False
        Me.SplitContainerForm.Text = "splitContainer1"
        '
        'SplitContainerTree
        '
        Me.SplitContainerTree.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerTree.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainerTree.Name = "SplitContainerTree"
        Me.SplitContainerTree.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerTree.Panel1
        '
        Me.SplitContainerTree.Panel1.Controls.Add(Me.SplitContainerTypeEngine)
        '
        'SplitContainerTree.Panel2
        '
        Me.SplitContainerTree.Panel2.Controls.Add(Me.DataGridView3ЦиклУправИсполУстр)
        Me.SplitContainerTree.Panel2.Controls.Add(Me.BindingNavigator3ЦиклУправИсполУстр)
        Me.SplitContainerTree.Panel2.Controls.Add(Me.HeaderStripDevice)
        Me.SplitContainerTree.Panel2.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.SplitContainerTree.Size = New System.Drawing.Size(287, 784)
        Me.SplitContainerTree.SplitterDistance = 542
        Me.SplitContainerTree.SplitterWidth = 6
        Me.SplitContainerTree.TabIndex = 7
        '
        'SplitContainerTypeEngine
        '
        Me.SplitContainerTypeEngine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerTypeEngine.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerTypeEngine.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerTypeEngine.Name = "SplitContainerTypeEngine"
        Me.SplitContainerTypeEngine.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerTypeEngine.Panel1
        '
        Me.SplitContainerTypeEngine.Panel1.Controls.Add(Me.DataGridView1ТипИзделия)
        Me.SplitContainerTypeEngine.Panel1.Controls.Add(Me.BindingNavigator1ТипИзделия)
        Me.SplitContainerTypeEngine.Panel1.Controls.Add(Me.HeaderStripEngine)
        '
        'SplitContainerTypeEngine.Panel2
        '
        Me.SplitContainerTypeEngine.Panel2.Controls.Add(Me.DataGridView2ПрограммаИспытаний)
        Me.SplitContainerTypeEngine.Panel2.Controls.Add(Me.BindingNavigator2ПрограммаИспытаний)
        Me.SplitContainerTypeEngine.Panel2.Controls.Add(Me.HeaderStripProgram)
        Me.SplitContainerTypeEngine.Size = New System.Drawing.Size(287, 542)
        Me.SplitContainerTypeEngine.SplitterDistance = 181
        Me.SplitContainerTypeEngine.TabIndex = 0
        '
        'DataGridView1ТипИзделия
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.DataGridView1ТипИзделия.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1ТипИзделия.AutoGenerateColumns = False
        Me.DataGridView1ТипИзделия.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1ТипИзделия.BackgroundColor = System.Drawing.Color.Lavender
        Me.DataGridView1ТипИзделия.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView1ТипИзделия.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkBlue
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LimeGreen
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Teal
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1ТипИзделия.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1ТипИзделия.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1ТипИзделия.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ТипИзделияDataGridViewTextBoxColumn, Me.KeyТипИзделияDataGridViewTextBoxColumn, Me.ОписаниеDataGridViewTextBoxColumn})
        Me.DataGridView1ТипИзделия.DataSource = Me.BindingSource1ТипИзделия
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Teal
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.PaleGreen
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1ТипИзделия.DefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1ТипИзделия.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1ТипИзделия.GridColor = System.Drawing.Color.RoyalBlue
        Me.DataGridView1ТипИзделия.Location = New System.Drawing.Point(0, 50)
        Me.DataGridView1ТипИзделия.MultiSelect = False
        Me.DataGridView1ТипИзделия.Name = "DataGridView1ТипИзделия"
        Me.DataGridView1ТипИзделия.Size = New System.Drawing.Size(283, 127)
        Me.DataGridView1ТипИзделия.TabIndex = 11
        Me.ToolTipForm.SetToolTip(Me.DataGridView1ТипИзделия, "Редактирование информации о типе изделия")
        '
        'ТипИзделияDataGridViewTextBoxColumn
        '
        Me.ТипИзделияDataGridViewTextBoxColumn.DataPropertyName = "ТипИзделия"
        Me.ТипИзделияDataGridViewTextBoxColumn.HeaderText = "ТипИзделия"
        Me.ТипИзделияDataGridViewTextBoxColumn.Name = "ТипИзделияDataGridViewTextBoxColumn"
        Me.ТипИзделияDataGridViewTextBoxColumn.Width = 95
        '
        'KeyТипИзделияDataGridViewTextBoxColumn
        '
        Me.KeyТипИзделияDataGridViewTextBoxColumn.DataPropertyName = "keyТипИзделия"
        Me.KeyТипИзделияDataGridViewTextBoxColumn.HeaderText = "keyТипИзделия"
        Me.KeyТипИзделияDataGridViewTextBoxColumn.Name = "KeyТипИзделияDataGridViewTextBoxColumn"
        Me.KeyТипИзделияDataGridViewTextBoxColumn.Width = 112
        '
        'ОписаниеDataGridViewTextBoxColumn
        '
        Me.ОписаниеDataGridViewTextBoxColumn.DataPropertyName = "Описание"
        Me.ОписаниеDataGridViewTextBoxColumn.HeaderText = "Описание"
        Me.ОписаниеDataGridViewTextBoxColumn.Name = "ОписаниеDataGridViewTextBoxColumn"
        Me.ОписаниеDataGridViewTextBoxColumn.Width = 82
        '
        'BindingSource1ТипИзделия
        '
        Me.BindingSource1ТипИзделия.DataMember = "ТипИзделия1"
        Me.BindingSource1ТипИзделия.DataSource = Me.CycleDataSet1
        '
        'CycleDataSet1
        '
        Me.CycleDataSet1.DataSetName = "CycleDataSet"
        Me.CycleDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'BindingNavigator1ТипИзделия
        '
        Me.BindingNavigator1ТипИзделия.AddNewItem = Me.BindingNavigatorAddNewItem
        Me.BindingNavigator1ТипИзделия.BindingSource = Me.BindingSource1ТипИзделия
        Me.BindingNavigator1ТипИзделия.CountItem = Me.BindingNavigatorCountItem
        Me.BindingNavigator1ТипИзделия.DeleteItem = Nothing
        Me.BindingNavigator1ТипИзделия.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.BindingNavigator1ТипИзделия.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.BindingNavigatorPositionItem, Me.BindingNavigatorCountItem, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.BindingNavigatorSeparator2, Me.BindingNavigatorAddNewItem, Me.BNDeleteTypeEngine, Me.BNSaveTypeEngine})
        Me.BindingNavigator1ТипИзделия.Location = New System.Drawing.Point(0, 25)
        Me.BindingNavigator1ТипИзделия.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.BindingNavigator1ТипИзделия.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.BindingNavigator1ТипИзделия.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.BindingNavigator1ТипИзделия.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.BindingNavigator1ТипИзделия.Name = "BindingNavigator1ТипИзделия"
        Me.BindingNavigator1ТипИзделия.PositionItem = Me.BindingNavigatorPositionItem
        Me.BindingNavigator1ТипИзделия.Size = New System.Drawing.Size(283, 25)
        Me.BindingNavigator1ТипИзделия.TabIndex = 10
        Me.BindingNavigator1ТипИзделия.Text = "BindingNavigator1ТипИзделия"
        '
        'BindingNavigatorAddNewItem
        '
        Me.BindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem.Image = CType(resources.GetObject("BindingNavigatorAddNewItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem.Name = "BindingNavigatorAddNewItem"
        Me.BindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorAddNewItem.Text = "Добавить"
        '
        'BindingNavigatorCountItem
        '
        Me.BindingNavigatorCountItem.Name = "BindingNavigatorCountItem"
        Me.BindingNavigatorCountItem.Size = New System.Drawing.Size(43, 22)
        Me.BindingNavigatorCountItem.Text = "для {0}"
        Me.BindingNavigatorCountItem.ToolTipText = "Общее число элементов"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Переместить в начало"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Переместить назад"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem
        '
        Me.BindingNavigatorPositionItem.AccessibleName = "Положение"
        Me.BindingNavigatorPositionItem.AutoSize = False
        Me.BindingNavigatorPositionItem.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BindingNavigatorPositionItem.Name = "BindingNavigatorPositionItem"
        Me.BindingNavigatorPositionItem.Size = New System.Drawing.Size(50, 23)
        Me.BindingNavigatorPositionItem.Text = "0"
        Me.BindingNavigatorPositionItem.ToolTipText = "Текущее положение"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Переместить вперед"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Переместить в конец"
        '
        'BindingNavigatorSeparator2
        '
        Me.BindingNavigatorSeparator2.Name = "BindingNavigatorSeparator2"
        Me.BindingNavigatorSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'BNDeleteTypeEngine
        '
        Me.BNDeleteTypeEngine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BNDeleteTypeEngine.Image = CType(resources.GetObject("BNDeleteTypeEngine.Image"), System.Drawing.Image)
        Me.BNDeleteTypeEngine.Name = "BNDeleteTypeEngine"
        Me.BNDeleteTypeEngine.RightToLeftAutoMirrorImage = True
        Me.BNDeleteTypeEngine.Size = New System.Drawing.Size(23, 22)
        Me.BNDeleteTypeEngine.Text = "Удалить"
        '
        'BNSaveTypeEngine
        '
        Me.BNSaveTypeEngine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BNSaveTypeEngine.Enabled = False
        Me.BNSaveTypeEngine.Image = CType(resources.GetObject("BNSaveTypeEngine.Image"), System.Drawing.Image)
        Me.BNSaveTypeEngine.Name = "BNSaveTypeEngine"
        Me.BNSaveTypeEngine.Size = New System.Drawing.Size(23, 22)
        Me.BNSaveTypeEngine.Text = "Save Data"
        Me.BNSaveTypeEngine.ToolTipText = "Сохранить данные"
        '
        'HeaderStripEngine
        '
        Me.HeaderStripEngine.AutoSize = False
        Me.HeaderStripEngine.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold)
        Me.HeaderStripEngine.ForeColor = System.Drawing.Color.White
        Me.HeaderStripEngine.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.HeaderStripEngine.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabelEngine})
        Me.HeaderStripEngine.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table
        Me.HeaderStripEngine.Location = New System.Drawing.Point(0, 0)
        Me.HeaderStripEngine.Name = "HeaderStripEngine"
        Me.HeaderStripEngine.Size = New System.Drawing.Size(283, 25)
        Me.HeaderStripEngine.TabIndex = 6
        Me.HeaderStripEngine.Text = "HeaderStrip3"
        '
        'ToolStripLabelEngine
        '
        Me.ToolStripLabelEngine.ForeColor = System.Drawing.Color.Maroon
        Me.ToolStripLabelEngine.Name = "ToolStripLabelEngine"
        Me.ToolStripLabelEngine.Size = New System.Drawing.Size(112, 19)
        Me.ToolStripLabelEngine.Text = "Тип изделия"
        '
        'DataGridView2ПрограммаИспытаний
        '
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Teal
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.DataGridView2ПрограммаИспытаний.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView2ПрограммаИспытаний.AutoGenerateColumns = False
        Me.DataGridView2ПрограммаИспытаний.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView2ПрограммаИспытаний.BackgroundColor = System.Drawing.Color.Lavender
        Me.DataGridView2ПрограммаИспытаний.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView2ПрограммаИспытаний.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.DarkBlue
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LimeGreen
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Teal
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView2ПрограммаИспытаний.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView2ПрограммаИспытаний.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2ПрограммаИспытаний.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.KeyТипИзделияDataGridViewTextBoxColumn1, Me.KeyПрограммаИспытанийDataGridViewTextBoxColumn, Me.ИмяПрограммыDataGridViewTextBoxColumn, Me.ОписаниеDataGridViewTextBoxColumn1})
        Me.DataGridView2ПрограммаИспытаний.DataSource = Me.BindingSource2ПрограммаИспытаний
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Teal
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.PaleGreen
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView2ПрограммаИспытаний.DefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridView2ПрограммаИспытаний.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2ПрограммаИспытаний.GridColor = System.Drawing.Color.RoyalBlue
        Me.DataGridView2ПрограммаИспытаний.Location = New System.Drawing.Point(0, 50)
        Me.DataGridView2ПрограммаИспытаний.MultiSelect = False
        Me.DataGridView2ПрограммаИспытаний.Name = "DataGridView2ПрограммаИспытаний"
        Me.DataGridView2ПрограммаИспытаний.Size = New System.Drawing.Size(283, 303)
        Me.DataGridView2ПрограммаИспытаний.TabIndex = 10
        Me.ToolTipForm.SetToolTip(Me.DataGridView2ПрограммаИспытаний, "Редактирование информации о программе испытания")
        '
        'KeyТипИзделияDataGridViewTextBoxColumn1
        '
        Me.KeyТипИзделияDataGridViewTextBoxColumn1.DataPropertyName = "keyТипИзделия"
        Me.KeyТипИзделияDataGridViewTextBoxColumn1.HeaderText = "keyТипИзделия"
        Me.KeyТипИзделияDataGridViewTextBoxColumn1.Name = "KeyТипИзделияDataGridViewTextBoxColumn1"
        Me.KeyТипИзделияDataGridViewTextBoxColumn1.Width = 112
        '
        'KeyПрограммаИспытанийDataGridViewTextBoxColumn
        '
        Me.KeyПрограммаИспытанийDataGridViewTextBoxColumn.DataPropertyName = "keyПрограммаИспытаний"
        Me.KeyПрограммаИспытанийDataGridViewTextBoxColumn.HeaderText = "keyПрограммаИспытаний"
        Me.KeyПрограммаИспытанийDataGridViewTextBoxColumn.Name = "KeyПрограммаИспытанийDataGridViewTextBoxColumn"
        Me.KeyПрограммаИспытанийDataGridViewTextBoxColumn.Width = 165
        '
        'ИмяПрограммыDataGridViewTextBoxColumn
        '
        Me.ИмяПрограммыDataGridViewTextBoxColumn.DataPropertyName = "ИмяПрограммы"
        Me.ИмяПрограммыDataGridViewTextBoxColumn.HeaderText = "ИмяПрограммы"
        Me.ИмяПрограммыDataGridViewTextBoxColumn.Name = "ИмяПрограммыDataGridViewTextBoxColumn"
        Me.ИмяПрограммыDataGridViewTextBoxColumn.Width = 115
        '
        'ОписаниеDataGridViewTextBoxColumn1
        '
        Me.ОписаниеDataGridViewTextBoxColumn1.DataPropertyName = "Описание"
        Me.ОписаниеDataGridViewTextBoxColumn1.HeaderText = "Описание"
        Me.ОписаниеDataGridViewTextBoxColumn1.Name = "ОписаниеDataGridViewTextBoxColumn1"
        Me.ОписаниеDataGridViewTextBoxColumn1.Width = 82
        '
        'BindingSource2ПрограммаИспытаний
        '
        Me.BindingSource2ПрограммаИспытаний.DataMember = "ПрограммаИспытаний2"
        Me.BindingSource2ПрограммаИспытаний.DataSource = Me.CycleDataSet1
        '
        'BindingNavigator2ПрограммаИспытаний
        '
        Me.BindingNavigator2ПрограммаИспытаний.AddNewItem = Me.BindingNavigatorAddNewItem1
        Me.BindingNavigator2ПрограммаИспытаний.BindingSource = Me.BindingSource2ПрограммаИспытаний
        Me.BindingNavigator2ПрограммаИспытаний.CountItem = Me.BindingNavigatorCountItem1
        Me.BindingNavigator2ПрограммаИспытаний.DeleteItem = Nothing
        Me.BindingNavigator2ПрограммаИспытаний.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.BindingNavigator2ПрограммаИспытаний.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem1, Me.BindingNavigatorMovePreviousItem1, Me.BindingNavigatorSeparator3, Me.BindingNavigatorPositionItem1, Me.BindingNavigatorCountItem1, Me.BindingNavigatorSeparator4, Me.BindingNavigatorMoveNextItem1, Me.BindingNavigatorMoveLastItem1, Me.BindingNavigatorSeparator5, Me.BindingNavigatorAddNewItem1, Me.BNDeleteTestPrograms, Me.BNSaveTestPrograms})
        Me.BindingNavigator2ПрограммаИспытаний.Location = New System.Drawing.Point(0, 25)
        Me.BindingNavigator2ПрограммаИспытаний.MoveFirstItem = Me.BindingNavigatorMoveFirstItem1
        Me.BindingNavigator2ПрограммаИспытаний.MoveLastItem = Me.BindingNavigatorMoveLastItem1
        Me.BindingNavigator2ПрограммаИспытаний.MoveNextItem = Me.BindingNavigatorMoveNextItem1
        Me.BindingNavigator2ПрограммаИспытаний.MovePreviousItem = Me.BindingNavigatorMovePreviousItem1
        Me.BindingNavigator2ПрограммаИспытаний.Name = "BindingNavigator2ПрограммаИспытаний"
        Me.BindingNavigator2ПрограммаИспытаний.PositionItem = Me.BindingNavigatorPositionItem1
        Me.BindingNavigator2ПрограммаИспытаний.Size = New System.Drawing.Size(283, 25)
        Me.BindingNavigator2ПрограммаИспытаний.TabIndex = 9
        Me.BindingNavigator2ПрограммаИспытаний.Text = "BindingNavigator1"
        '
        'BindingNavigatorAddNewItem1
        '
        Me.BindingNavigatorAddNewItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem1.Image = CType(resources.GetObject("BindingNavigatorAddNewItem1.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem1.Name = "BindingNavigatorAddNewItem1"
        Me.BindingNavigatorAddNewItem1.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem1.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorAddNewItem1.Text = "Добавить"
        '
        'BindingNavigatorCountItem1
        '
        Me.BindingNavigatorCountItem1.Name = "BindingNavigatorCountItem1"
        Me.BindingNavigatorCountItem1.Size = New System.Drawing.Size(43, 22)
        Me.BindingNavigatorCountItem1.Text = "для {0}"
        Me.BindingNavigatorCountItem1.ToolTipText = "Общее число элементов"
        '
        'BindingNavigatorMoveFirstItem1
        '
        Me.BindingNavigatorMoveFirstItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem1.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem1.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem1.Name = "BindingNavigatorMoveFirstItem1"
        Me.BindingNavigatorMoveFirstItem1.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem1.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem1.Text = "Переместить в начало"
        '
        'BindingNavigatorMovePreviousItem1
        '
        Me.BindingNavigatorMovePreviousItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem1.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem1.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem1.Name = "BindingNavigatorMovePreviousItem1"
        Me.BindingNavigatorMovePreviousItem1.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem1.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem1.Text = "Переместить назад"
        '
        'BindingNavigatorSeparator3
        '
        Me.BindingNavigatorSeparator3.Name = "BindingNavigatorSeparator3"
        Me.BindingNavigatorSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem1
        '
        Me.BindingNavigatorPositionItem1.AccessibleName = "Положение"
        Me.BindingNavigatorPositionItem1.AutoSize = False
        Me.BindingNavigatorPositionItem1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BindingNavigatorPositionItem1.Name = "BindingNavigatorPositionItem1"
        Me.BindingNavigatorPositionItem1.Size = New System.Drawing.Size(50, 23)
        Me.BindingNavigatorPositionItem1.Text = "0"
        Me.BindingNavigatorPositionItem1.ToolTipText = "Текущее положение"
        '
        'BindingNavigatorSeparator4
        '
        Me.BindingNavigatorSeparator4.Name = "BindingNavigatorSeparator4"
        Me.BindingNavigatorSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem1
        '
        Me.BindingNavigatorMoveNextItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem1.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem1.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem1.Name = "BindingNavigatorMoveNextItem1"
        Me.BindingNavigatorMoveNextItem1.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem1.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem1.Text = "Переместить вперед"
        '
        'BindingNavigatorMoveLastItem1
        '
        Me.BindingNavigatorMoveLastItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem1.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem1.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem1.Name = "BindingNavigatorMoveLastItem1"
        Me.BindingNavigatorMoveLastItem1.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem1.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem1.Text = "Переместить в конец"
        '
        'BindingNavigatorSeparator5
        '
        Me.BindingNavigatorSeparator5.Name = "BindingNavigatorSeparator5"
        Me.BindingNavigatorSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'BNDeleteTestPrograms
        '
        Me.BNDeleteTestPrograms.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BNDeleteTestPrograms.Image = CType(resources.GetObject("BNDeleteTestPrograms.Image"), System.Drawing.Image)
        Me.BNDeleteTestPrograms.Name = "BNDeleteTestPrograms"
        Me.BNDeleteTestPrograms.RightToLeftAutoMirrorImage = True
        Me.BNDeleteTestPrograms.Size = New System.Drawing.Size(23, 22)
        Me.BNDeleteTestPrograms.Text = "Удалить"
        '
        'BNSaveTestPrograms
        '
        Me.BNSaveTestPrograms.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BNSaveTestPrograms.Enabled = False
        Me.BNSaveTestPrograms.Image = CType(resources.GetObject("BNSaveTestPrograms.Image"), System.Drawing.Image)
        Me.BNSaveTestPrograms.Name = "BNSaveTestPrograms"
        Me.BNSaveTestPrograms.Size = New System.Drawing.Size(23, 22)
        Me.BNSaveTestPrograms.Text = "Save Data"
        Me.BNSaveTestPrograms.ToolTipText = "Сохранить данные"
        '
        'HeaderStripProgram
        '
        Me.HeaderStripProgram.AutoSize = False
        Me.HeaderStripProgram.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold)
        Me.HeaderStripProgram.ForeColor = System.Drawing.Color.White
        Me.HeaderStripProgram.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.HeaderStripProgram.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabelProgram})
        Me.HeaderStripProgram.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table
        Me.HeaderStripProgram.Location = New System.Drawing.Point(0, 0)
        Me.HeaderStripProgram.Name = "HeaderStripProgram"
        Me.HeaderStripProgram.Size = New System.Drawing.Size(283, 25)
        Me.HeaderStripProgram.TabIndex = 2
        Me.HeaderStripProgram.Text = "HeaderStripProgram"
        '
        'ToolStripLabelProgram
        '
        Me.ToolStripLabelProgram.ForeColor = System.Drawing.Color.Maroon
        Me.ToolStripLabelProgram.Name = "ToolStripLabelProgram"
        Me.ToolStripLabelProgram.Size = New System.Drawing.Size(197, 19)
        Me.ToolStripLabelProgram.Text = "Программа испытаний"
        '
        'DataGridView3ЦиклУправИсполУстр
        '
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Teal
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.DataGridView3ЦиклУправИсполУстр.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridView3ЦиклУправИсполУстр.AutoGenerateColumns = False
        Me.DataGridView3ЦиклУправИсполУстр.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView3ЦиклУправИсполУстр.BackgroundColor = System.Drawing.Color.Lavender
        Me.DataGridView3ЦиклУправИсполУстр.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView3ЦиклУправИсполУстр.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.DarkBlue
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LimeGreen
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Teal
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView3ЦиклУправИсполУстр.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridView3ЦиклУправИсполУстр.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3ЦиклУправИсполУстр.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.KeyПрограммаИспытанийDataGridViewTextBoxColumn1, Me.KeyЦиклЗагрузкиDataGridViewTextBoxColumn, Me.ИмяУстройстваDataGridViewTextBoxColumn, Me.ИмяУстройстваNewDataGridViewTextBoxColumn})
        Me.DataGridView3ЦиклУправИсполУстр.DataSource = Me.BindingSource3ЦиклУправИсполУстр
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Teal
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.PaleGreen
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView3ЦиклУправИсполУстр.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridView3ЦиклУправИсполУстр.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView3ЦиклУправИсполУстр.GridColor = System.Drawing.Color.RoyalBlue
        Me.DataGridView3ЦиклУправИсполУстр.Location = New System.Drawing.Point(0, 53)
        Me.DataGridView3ЦиклУправИсполУстр.MultiSelect = False
        Me.DataGridView3ЦиклУправИсполУстр.Name = "DataGridView3ЦиклУправИсполУстр"
        Me.DataGridView3ЦиклУправИсполУстр.Size = New System.Drawing.Size(283, 176)
        Me.DataGridView3ЦиклУправИсполУстр.TabIndex = 9
        Me.ToolTipForm.SetToolTip(Me.DataGridView3ЦиклУправИсполУстр, "Редактирование информации о циклах каждой уставки")
        '
        'KeyПрограммаИспытанийDataGridViewTextBoxColumn1
        '
        Me.KeyПрограммаИспытанийDataGridViewTextBoxColumn1.DataPropertyName = "keyПрограммаИспытаний"
        Me.KeyПрограммаИспытанийDataGridViewTextBoxColumn1.HeaderText = "keyПрограммаИспытаний"
        Me.KeyПрограммаИспытанийDataGridViewTextBoxColumn1.Name = "KeyПрограммаИспытанийDataGridViewTextBoxColumn1"
        Me.KeyПрограммаИспытанийDataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.KeyПрограммаИспытанийDataGridViewTextBoxColumn1.Width = 146
        '
        'KeyЦиклЗагрузкиDataGridViewTextBoxColumn
        '
        Me.KeyЦиклЗагрузкиDataGridViewTextBoxColumn.DataPropertyName = "keyЦиклЗагрузки"
        Me.KeyЦиклЗагрузкиDataGridViewTextBoxColumn.HeaderText = "keyЦиклЗагрузки"
        Me.KeyЦиклЗагрузкиDataGridViewTextBoxColumn.Name = "KeyЦиклЗагрузкиDataGridViewTextBoxColumn"
        Me.KeyЦиклЗагрузкиDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.KeyЦиклЗагрузкиDataGridViewTextBoxColumn.Width = 103
        '
        'ИмяУстройстваDataGridViewTextBoxColumn
        '
        Me.ИмяУстройстваDataGridViewTextBoxColumn.DataPropertyName = "ИмяУстройства"
        Me.ИмяУстройстваDataGridViewTextBoxColumn.HeaderText = "Имя Устройства"
        Me.ИмяУстройстваDataGridViewTextBoxColumn.Name = "ИмяУстройстваDataGridViewTextBoxColumn"
        Me.ИмяУстройстваDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ИмяУстройстваDataGridViewTextBoxColumn.Visible = False
        Me.ИмяУстройстваDataGridViewTextBoxColumn.Width = 98
        '
        'ИмяУстройстваNewDataGridViewTextBoxColumn
        '
        Me.ИмяУстройстваNewDataGridViewTextBoxColumn.HeaderText = "Имя Устройства"
        Me.ИмяУстройстваNewDataGridViewTextBoxColumn.Name = "ИмяУстройстваNewDataGridViewTextBoxColumn"
        Me.ИмяУстройстваNewDataGridViewTextBoxColumn.Width = 98
        '
        'BindingSource3ЦиклУправИсполУстр
        '
        Me.BindingSource3ЦиклУправИсполУстр.DataMember = "ЦиклЗагрузки3"
        Me.BindingSource3ЦиклУправИсполУстр.DataSource = Me.CycleDataSet1
        '
        'BindingNavigator3ЦиклУправИсполУстр
        '
        Me.BindingNavigator3ЦиклУправИсполУстр.AddNewItem = Me.BindingNavigatorAddNewItem2
        Me.BindingNavigator3ЦиклУправИсполУстр.BindingSource = Me.BindingSource3ЦиклУправИсполУстр
        Me.BindingNavigator3ЦиклУправИсполУстр.CountItem = Me.BindingNavigatorCountItem2
        Me.BindingNavigator3ЦиклУправИсполУстр.DeleteItem = Nothing
        Me.BindingNavigator3ЦиклУправИсполУстр.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.BindingNavigator3ЦиклУправИсполУстр.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem2, Me.BindingNavigatorMovePreviousItem2, Me.BindingNavigatorSeparator6, Me.BindingNavigatorPositionItem2, Me.BindingNavigatorCountItem2, Me.BindingNavigatorSeparator7, Me.BindingNavigatorMoveNextItem2, Me.BindingNavigatorMoveLastItem2, Me.BindingNavigatorSeparator8, Me.BindingNavigatorAddNewItem2, Me.BNDeleteSycleStage, Me.BNSaveSycleDevices})
        Me.BindingNavigator3ЦиклУправИсполУстр.Location = New System.Drawing.Point(0, 28)
        Me.BindingNavigator3ЦиклУправИсполУстр.MoveFirstItem = Me.BindingNavigatorMoveFirstItem2
        Me.BindingNavigator3ЦиклУправИсполУстр.MoveLastItem = Me.BindingNavigatorMoveLastItem2
        Me.BindingNavigator3ЦиклУправИсполУстр.MoveNextItem = Me.BindingNavigatorMoveNextItem2
        Me.BindingNavigator3ЦиклУправИсполУстр.MovePreviousItem = Me.BindingNavigatorMovePreviousItem2
        Me.BindingNavigator3ЦиклУправИсполУстр.Name = "BindingNavigator3ЦиклУправИсполУстр"
        Me.BindingNavigator3ЦиклУправИсполУстр.PositionItem = Me.BindingNavigatorPositionItem2
        Me.BindingNavigator3ЦиклУправИсполУстр.Size = New System.Drawing.Size(283, 25)
        Me.BindingNavigator3ЦиклУправИсполУстр.TabIndex = 8
        Me.BindingNavigator3ЦиклУправИсполУстр.Text = "BindingNavigator3ЦиклУправИсполУстр"
        '
        'BindingNavigatorAddNewItem2
        '
        Me.BindingNavigatorAddNewItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem2.Image = CType(resources.GetObject("BindingNavigatorAddNewItem2.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem2.Name = "BindingNavigatorAddNewItem2"
        Me.BindingNavigatorAddNewItem2.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem2.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorAddNewItem2.Text = "Добавить"
        Me.BindingNavigatorAddNewItem2.Visible = False
        '
        'BindingNavigatorCountItem2
        '
        Me.BindingNavigatorCountItem2.Name = "BindingNavigatorCountItem2"
        Me.BindingNavigatorCountItem2.Size = New System.Drawing.Size(43, 22)
        Me.BindingNavigatorCountItem2.Text = "для {0}"
        Me.BindingNavigatorCountItem2.ToolTipText = "Общее число элементов"
        '
        'BindingNavigatorMoveFirstItem2
        '
        Me.BindingNavigatorMoveFirstItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem2.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem2.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem2.Name = "BindingNavigatorMoveFirstItem2"
        Me.BindingNavigatorMoveFirstItem2.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem2.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem2.Text = "Переместить в начало"
        '
        'BindingNavigatorMovePreviousItem2
        '
        Me.BindingNavigatorMovePreviousItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem2.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem2.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem2.Name = "BindingNavigatorMovePreviousItem2"
        Me.BindingNavigatorMovePreviousItem2.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem2.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem2.Text = "Переместить назад"
        '
        'BindingNavigatorSeparator6
        '
        Me.BindingNavigatorSeparator6.Name = "BindingNavigatorSeparator6"
        Me.BindingNavigatorSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem2
        '
        Me.BindingNavigatorPositionItem2.AccessibleName = "Положение"
        Me.BindingNavigatorPositionItem2.AutoSize = False
        Me.BindingNavigatorPositionItem2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BindingNavigatorPositionItem2.Name = "BindingNavigatorPositionItem2"
        Me.BindingNavigatorPositionItem2.Size = New System.Drawing.Size(50, 23)
        Me.BindingNavigatorPositionItem2.Text = "0"
        Me.BindingNavigatorPositionItem2.ToolTipText = "Текущее положение"
        '
        'BindingNavigatorSeparator7
        '
        Me.BindingNavigatorSeparator7.Name = "BindingNavigatorSeparator7"
        Me.BindingNavigatorSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem2
        '
        Me.BindingNavigatorMoveNextItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem2.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem2.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem2.Name = "BindingNavigatorMoveNextItem2"
        Me.BindingNavigatorMoveNextItem2.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem2.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem2.Text = "Переместить вперед"
        '
        'BindingNavigatorMoveLastItem2
        '
        Me.BindingNavigatorMoveLastItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem2.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem2.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem2.Name = "BindingNavigatorMoveLastItem2"
        Me.BindingNavigatorMoveLastItem2.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem2.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem2.Text = "Переместить в конец"
        '
        'BindingNavigatorSeparator8
        '
        Me.BindingNavigatorSeparator8.Name = "BindingNavigatorSeparator8"
        Me.BindingNavigatorSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'BNDeleteSycleStage
        '
        Me.BNDeleteSycleStage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BNDeleteSycleStage.Image = CType(resources.GetObject("BNDeleteSycleStage.Image"), System.Drawing.Image)
        Me.BNDeleteSycleStage.Name = "BNDeleteSycleStage"
        Me.BNDeleteSycleStage.RightToLeftAutoMirrorImage = True
        Me.BNDeleteSycleStage.Size = New System.Drawing.Size(23, 22)
        Me.BNDeleteSycleStage.Text = "Удалить"
        '
        'BNSaveSycleDevices
        '
        Me.BNSaveSycleDevices.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BNSaveSycleDevices.Enabled = False
        Me.BNSaveSycleDevices.Image = CType(resources.GetObject("BNSaveSycleDevices.Image"), System.Drawing.Image)
        Me.BNSaveSycleDevices.Name = "BNSaveSycleDevices"
        Me.BNSaveSycleDevices.Size = New System.Drawing.Size(23, 22)
        Me.BNSaveSycleDevices.Text = "Save Data"
        Me.BNSaveSycleDevices.ToolTipText = "Сохранить данные"
        '
        'HeaderStripDevice
        '
        Me.HeaderStripDevice.AutoSize = False
        Me.HeaderStripDevice.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold)
        Me.HeaderStripDevice.ForeColor = System.Drawing.Color.White
        Me.HeaderStripDevice.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.HeaderStripDevice.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabelDevice})
        Me.HeaderStripDevice.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table
        Me.HeaderStripDevice.Location = New System.Drawing.Point(0, 3)
        Me.HeaderStripDevice.Name = "HeaderStripDevice"
        Me.HeaderStripDevice.Size = New System.Drawing.Size(283, 25)
        Me.HeaderStripDevice.TabIndex = 3
        Me.HeaderStripDevice.Text = "HeaderStrip6"
        '
        'ToolStripLabelDevice
        '
        Me.ToolStripLabelDevice.ForeColor = System.Drawing.Color.Maroon
        Me.ToolStripLabelDevice.Name = "ToolStripLabelDevice"
        Me.ToolStripLabelDevice.Size = New System.Drawing.Size(463, 19)
        Me.ToolStripLabelDevice.Text = "Исполнительные устройства в программе управления"
        '
        'SplitContainerEditor
        '
        Me.SplitContainerEditor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerEditor.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainerEditor.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerEditor.Name = "SplitContainerEditor"
        Me.SplitContainerEditor.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerEditor.Panel1
        '
        Me.SplitContainerEditor.Panel1.Controls.Add(Me.SplitContainerGraf)
        '
        'SplitContainerEditor.Panel2
        '
        Me.SplitContainerEditor.Panel2.Controls.Add(Me.SplitContainerCycle)
        Me.SplitContainerEditor.Panel2.Controls.Add(Me.HeaderStripCycle)
        Me.SplitContainerEditor.Panel2.Controls.Add(Me.ToolStripEditCycle)
        Me.SplitContainerEditor.Size = New System.Drawing.Size(776, 790)
        Me.SplitContainerEditor.SplitterDistance = 556
        Me.SplitContainerEditor.TabIndex = 1
        '
        'SplitContainerGraf
        '
        Me.SplitContainerGraf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerGraf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerGraf.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainerGraf.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerGraf.Name = "SplitContainerGraf"
        Me.SplitContainerGraf.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerGraf.Panel1
        '
        Me.SplitContainerGraf.Panel1.Controls.Add(Me.ScatterGraphCycle)
        Me.SplitContainerGraf.Panel1.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
        '
        'SplitContainerGraf.Panel2
        '
        Me.SplitContainerGraf.Panel2.BackColor = System.Drawing.Color.Silver
        Me.SplitContainerGraf.Panel2.Controls.Add(Me.PanelCycleAndLayoutPanel)
        Me.SplitContainerGraf.Panel2.Padding = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.SplitContainerGraf.Size = New System.Drawing.Size(776, 556)
        Me.SplitContainerGraf.SplitterDistance = 168
        Me.SplitContainerGraf.TabIndex = 0
        Me.SplitContainerGraf.TabStop = False
        Me.SplitContainerGraf.Text = "splitContainer2"
        '
        'ScatterGraphCycle
        '
        Me.ScatterGraphCycle.Border = NationalInstruments.UI.Border.ThinFrame3D
        Me.ScatterGraphCycle.Caption = "Общая циклограмма управления устройствами в испытании"
        Me.ScatterGraphCycle.CaptionBackColor = System.Drawing.Color.LightGray
        Me.ScatterGraphCycle.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ScatterGraphCycle.CaptionForeColor = System.Drawing.Color.Maroon
        Me.ScatterGraphCycle.Cursors.AddRange(New NationalInstruments.UI.XYCursor() {Me.XyCursor1})
        Me.ScatterGraphCycle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScatterGraphCycle.Location = New System.Drawing.Point(0, 3)
        Me.ScatterGraphCycle.Name = "ScatterGraphCycle"
        Me.ScatterGraphCycle.PlotAreaColor = System.Drawing.Color.WhiteSmoke
        Me.ScatterGraphCycle.Plots.AddRange(New NationalInstruments.UI.ScatterPlot() {Me.ScatterPlot2})
        Me.ScatterGraphCycle.Size = New System.Drawing.Size(772, 158)
        Me.ScatterGraphCycle.TabIndex = 10
        Me.ScatterGraphCycle.XAxes.AddRange(New NationalInstruments.UI.XAxis() {Me.XAxis2})
        Me.ScatterGraphCycle.YAxes.AddRange(New NationalInstruments.UI.YAxis() {Me.YAxis2})
        '
        'XyCursor1
        '
        Me.XyCursor1.HorizontalCrosshairMode = NationalInstruments.UI.CursorCrosshairMode.None
        Me.XyCursor1.LabelBackColor = System.Drawing.Color.WhiteSmoke
        Me.XyCursor1.LabelDisplay = NationalInstruments.UI.XYCursorLabelDisplay.ShowX
        Me.XyCursor1.LabelForeColor = System.Drawing.Color.Blue
        Me.XyCursor1.LabelVisible = True
        Me.XyCursor1.Plot = Me.ScatterPlot2
        Me.XyCursor1.PointSize = New System.Drawing.Size(0, 0)
        Me.XyCursor1.PointStyle = NationalInstruments.UI.PointStyle.None
        Me.XyCursor1.SnapMode = NationalInstruments.UI.CursorSnapMode.Floating
        '
        'ScatterPlot2
        '
        Me.ScatterPlot2.LineColor = System.Drawing.Color.Maroon
        Me.ScatterPlot2.LineColorPrecedence = NationalInstruments.UI.ColorPrecedence.UserDefinedColor
        Me.ScatterPlot2.LineStyle = NationalInstruments.UI.LineStyle.Dash
        Me.ScatterPlot2.XAxis = Me.XAxis2
        Me.ScatterPlot2.YAxis = Me.YAxis2
        '
        'PanelCycleAndLayoutPanel
        '
        Me.PanelCycleAndLayoutPanel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PanelCycleAndLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelCycleAndLayoutPanel.Controls.Add(Me.ScatterGraphSelectDevice)
        Me.PanelCycleAndLayoutPanel.Controls.Add(Me.TableLayoutPanelAttributes)
        Me.PanelCycleAndLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelCycleAndLayoutPanel.Location = New System.Drawing.Point(0, 3)
        Me.PanelCycleAndLayoutPanel.Name = "PanelCycleAndLayoutPanel"
        Me.PanelCycleAndLayoutPanel.Size = New System.Drawing.Size(769, 374)
        Me.PanelCycleAndLayoutPanel.TabIndex = 2
        '
        'ScatterGraphSelectDevice
        '
        Me.ScatterGraphSelectDevice.Border = NationalInstruments.UI.Border.ThinFrame3D
        Me.ScatterGraphSelectDevice.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScatterGraphSelectDevice.Location = New System.Drawing.Point(0, 94)
        Me.ScatterGraphSelectDevice.Name = "ScatterGraphSelectDevice"
        Me.ScatterGraphSelectDevice.Plots.AddRange(New NationalInstruments.UI.ScatterPlot() {Me.ScatterPlot1})
        Me.ScatterGraphSelectDevice.Size = New System.Drawing.Size(767, 278)
        Me.ScatterGraphSelectDevice.TabIndex = 25
        Me.ScatterGraphSelectDevice.XAxes.AddRange(New NationalInstruments.UI.XAxis() {Me.XAxis1})
        Me.ScatterGraphSelectDevice.YAxes.AddRange(New NationalInstruments.UI.YAxis() {Me.YAxis1})
        '
        'ScatterPlot1
        '
        Me.ScatterPlot1.PointColor = System.Drawing.Color.Blue
        Me.ScatterPlot1.PointStyle = NationalInstruments.UI.PointStyle.SolidCircle
        Me.ScatterPlot1.XAxis = Me.XAxis1
        Me.ScatterPlot1.YAxis = Me.YAxis1
        '
        'XAxis1
        '
        Me.XAxis1.AutoMinorDivisionFrequency = 5
        Me.XAxis1.Caption = "Время сек"
        Me.XAxis1.MajorDivisions.GridVisible = True
        Me.XAxis1.MinorDivisions.GridVisible = True
        Me.XAxis1.Range = New NationalInstruments.UI.Range(0R, 120.0R)
        '
        'YAxis1
        '
        Me.YAxis1.AutoMinorDivisionFrequency = 5
        Me.YAxis1.Caption = "РУД"
        Me.YAxis1.MajorDivisions.GridVisible = True
        Me.YAxis1.MinorDivisions.GridVisible = True
        Me.YAxis1.Mode = NationalInstruments.UI.AxisMode.Fixed
        Me.YAxis1.Range = New NationalInstruments.UI.Range(0R, 120.0R)
        '
        'TableLayoutPanelAttributes
        '
        Me.TableLayoutPanelAttributes.AutoSize = True
        Me.TableLayoutPanelAttributes.ColumnCount = 3
        Me.TableLayoutPanelAttributes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanelAttributes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanelAttributes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanelAttributes.Controls.Add(Me.LabelTimeAllCyrcle, 1, 5)
        Me.TableLayoutPanelAttributes.Controls.Add(Me.TSCycle, 2, 0)
        Me.TableLayoutPanelAttributes.Controls.Add(Me.LabelMinMaxDevice, 0, 2)
        Me.TableLayoutPanelAttributes.Controls.Add(Me.LabelReplied, 0, 5)
        Me.TableLayoutPanelAttributes.Controls.Add(Me.LabelMinMax, 1, 2)
        Me.TableLayoutPanelAttributes.Controls.Add(Me.LabelCaption, 1, 3)
        Me.TableLayoutPanelAttributes.Controls.Add(Me.LabelDescription, 0, 3)
        Me.TableLayoutPanelAttributes.Controls.Add(Me.LabelNameChargeParameter, 1, 0)
        Me.TableLayoutPanelAttributes.Controls.Add(Me.LabelUnitOfMeasure, 1, 1)
        Me.TableLayoutPanelAttributes.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanelAttributes.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanelAttributes.Name = "TableLayoutPanelAttributes"
        Me.TableLayoutPanelAttributes.RowCount = 7
        Me.TableLayoutPanelAttributes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAttributes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAttributes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAttributes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAttributes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAttributes.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanelAttributes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
        Me.TableLayoutPanelAttributes.Size = New System.Drawing.Size(767, 94)
        Me.TableLayoutPanelAttributes.TabIndex = 8
        '
        'LabelTimeAllCyrcle
        '
        Me.LabelTimeAllCyrcle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelTimeAllCyrcle.AutoSize = True
        Me.LabelTimeAllCyrcle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTimeAllCyrcle.Location = New System.Drawing.Point(212, 73)
        Me.LabelTimeAllCyrcle.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelTimeAllCyrcle.Name = "LabelTimeAllCyrcle"
        Me.LabelTimeAllCyrcle.Padding = New System.Windows.Forms.Padding(1)
        Me.LabelTimeAllCyrcle.Size = New System.Drawing.Size(15, 16)
        Me.LabelTimeAllCyrcle.TabIndex = 9
        Me.LabelTimeAllCyrcle.Text = "0"
        '
        'TSCycle
        '
        Me.TSCycle.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.TSCycle.BackColor = System.Drawing.SystemColors.Control
        Me.TSCycle.CanOverflow = False
        Me.TSCycle.Dock = System.Windows.Forms.DockStyle.None
        Me.TSCycle.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.TSCycle.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ButtonShowMore})
        Me.TSCycle.Location = New System.Drawing.Point(741, 0)
        Me.TSCycle.Name = "TSCycle"
        Me.TSCycle.Size = New System.Drawing.Size(26, 25)
        Me.TSCycle.TabIndex = 8
        Me.TSCycle.Text = "ToolStrip1"
        '
        'ButtonShowMore
        '
        Me.ButtonShowMore.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ButtonShowMore.Image = Global.CycleCharge.My.Resources.Resources.down
        Me.ButtonShowMore.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ButtonShowMore.Name = "ButtonShowMore"
        Me.ButtonShowMore.Size = New System.Drawing.Size(23, 22)
        Me.ButtonShowMore.Text = "Показать"
        Me.ButtonShowMore.ToolTipText = ">> Показать дополнительную панель всех циклограмм"
        '
        'LabelMinMaxDevice
        '
        Me.LabelMinMaxDevice.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelMinMaxDevice.AutoSize = True
        Me.LabelMinMaxDevice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelMinMaxDevice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(174, Byte), Integer))
        Me.LabelMinMaxDevice.Location = New System.Drawing.Point(0, 41)
        Me.LabelMinMaxDevice.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelMinMaxDevice.Name = "LabelMinMaxDevice"
        Me.LabelMinMaxDevice.Padding = New System.Windows.Forms.Padding(1)
        Me.LabelMinMaxDevice.Size = New System.Drawing.Size(65, 16)
        Me.LabelMinMaxDevice.TabIndex = 3
        Me.LabelMinMaxDevice.Text = "Диапазон:"
        '
        'LabelReplied
        '
        Me.LabelReplied.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelReplied.BackColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.LabelReplied.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelReplied.ForeColor = System.Drawing.SystemColors.Window
        Me.LabelReplied.Location = New System.Drawing.Point(0, 73)
        Me.LabelReplied.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelReplied.Name = "LabelReplied"
        Me.LabelReplied.Padding = New System.Windows.Forms.Padding(1)
        Me.LabelReplied.Size = New System.Drawing.Size(212, 16)
        Me.LabelReplied.TabIndex = 2
        Me.LabelReplied.Text = "Время исполнения циклограммы:"
        Me.LabelReplied.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LabelMinMax
        '
        Me.LabelMinMax.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelMinMax.AutoSize = True
        Me.LabelMinMax.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelMinMax.Location = New System.Drawing.Point(212, 41)
        Me.LabelMinMax.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelMinMax.Name = "LabelMinMax"
        Me.LabelMinMax.Padding = New System.Windows.Forms.Padding(1)
        Me.LabelMinMax.Size = New System.Drawing.Size(48, 16)
        Me.LabelMinMax.TabIndex = 6
        Me.LabelMinMax.Text = "min:max"
        '
        'LabelCaption
        '
        Me.LabelCaption.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelCaption.AutoSize = True
        Me.LabelCaption.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelCaption.Location = New System.Drawing.Point(212, 57)
        Me.LabelCaption.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelCaption.Name = "LabelCaption"
        Me.LabelCaption.Padding = New System.Windows.Forms.Padding(1)
        Me.LabelCaption.Size = New System.Drawing.Size(169, 16)
        Me.LabelCaption.TabIndex = 7
        Me.LabelCaption.Text = "управление насосом давления"
        '
        'LabelDescription
        '
        Me.LabelDescription.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelDescription.AutoSize = True
        Me.LabelDescription.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(174, Byte), Integer))
        Me.LabelDescription.Location = New System.Drawing.Point(0, 57)
        Me.LabelDescription.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelDescription.Name = "LabelDescription"
        Me.LabelDescription.Padding = New System.Windows.Forms.Padding(1)
        Me.LabelDescription.Size = New System.Drawing.Size(82, 16)
        Me.LabelDescription.TabIndex = 4
        Me.LabelDescription.Text = "Примечание:"
        '
        'LabelNameChargeParameter
        '
        Me.LabelNameChargeParameter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelNameChargeParameter.AutoSize = True
        Me.LabelNameChargeParameter.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.LabelNameChargeParameter.ForeColor = System.Drawing.Color.Blue
        Me.LabelNameChargeParameter.Location = New System.Drawing.Point(212, 2)
        Me.LabelNameChargeParameter.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelNameChargeParameter.Name = "LabelNameChargeParameter"
        Me.LabelNameChargeParameter.Padding = New System.Windows.Forms.Padding(1)
        Me.LabelNameChargeParameter.Size = New System.Drawing.Size(115, 21)
        Me.LabelNameChargeParameter.TabIndex = 1
        Me.LabelNameChargeParameter.Text = "Имя агрегата"
        '
        'LabelUnitOfMeasure
        '
        Me.LabelUnitOfMeasure.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.LabelUnitOfMeasure.AutoSize = True
        Me.LabelUnitOfMeasure.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.LabelUnitOfMeasure.ForeColor = System.Drawing.Color.Blue
        Me.LabelUnitOfMeasure.Location = New System.Drawing.Point(212, 25)
        Me.LabelUnitOfMeasure.Margin = New System.Windows.Forms.Padding(0)
        Me.LabelUnitOfMeasure.Name = "LabelUnitOfMeasure"
        Me.LabelUnitOfMeasure.Padding = New System.Windows.Forms.Padding(1)
        Me.LabelUnitOfMeasure.Size = New System.Drawing.Size(112, 16)
        Me.LabelUnitOfMeasure.TabIndex = 0
        Me.LabelUnitOfMeasure.Text = "Единица Измерения"
        '
        'SplitContainerCycle
        '
        Me.SplitContainerCycle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerCycle.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainerCycle.Name = "SplitContainerCycle"
        '
        'SplitContainerCycle.Panel1
        '
        Me.SplitContainerCycle.Panel1.Controls.Add(Me.DataGridView4ПерекладкиЦикла)
        Me.SplitContainerCycle.Panel1.Controls.Add(Me.BindingNavigator4ПерекладкиЦикла)
        '
        'SplitContainerCycle.Panel2
        '
        Me.SplitContainerCycle.Panel2.Controls.Add(Me.ListViewCycle)
        Me.SplitContainerCycle.Size = New System.Drawing.Size(772, 162)
        Me.SplitContainerCycle.SplitterDistance = 248
        Me.SplitContainerCycle.TabIndex = 8
        '
        'DataGridView4ПерекладкиЦикла
        '
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Teal
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.DataGridView4ПерекладкиЦикла.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridView4ПерекладкиЦикла.AutoGenerateColumns = False
        Me.DataGridView4ПерекладкиЦикла.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView4ПерекладкиЦикла.BackgroundColor = System.Drawing.Color.Lavender
        Me.DataGridView4ПерекладкиЦикла.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridView4ПерекладкиЦикла.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.DarkBlue
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.LimeGreen
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Teal
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView4ПерекладкиЦикла.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridView4ПерекладкиЦикла.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView4ПерекладкиЦикла.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.KeyЦиклЗагрузкиDataGridViewTextBoxColumn1, Me.KeyПерекладкиЦиклаDataGridViewTextBoxColumn, Me.KeyУстройстваDataGridViewTextBoxColumn, Me.ВеличинаЗагрузкиDataGridViewTextBoxColumn, Me.ЧисловоеЗначениеDataGridViewTextBoxColumn, Me.TimeValueDataGridViewTextBoxColumn, Me.TimeActionDataGridViewTextBoxColumn})
        Me.DataGridView4ПерекладкиЦикла.DataSource = Me.BindingSource4ПерекладкиЦикла
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Teal
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.PaleGreen
        DataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView4ПерекладкиЦикла.DefaultCellStyle = DataGridViewCellStyle12
        Me.DataGridView4ПерекладкиЦикла.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView4ПерекладкиЦикла.GridColor = System.Drawing.Color.RoyalBlue
        Me.DataGridView4ПерекладкиЦикла.Location = New System.Drawing.Point(0, 25)
        Me.DataGridView4ПерекладкиЦикла.MultiSelect = False
        Me.DataGridView4ПерекладкиЦикла.Name = "DataGridView4ПерекладкиЦикла"
        Me.DataGridView4ПерекладкиЦикла.Size = New System.Drawing.Size(248, 137)
        Me.DataGridView4ПерекладкиЦикла.TabIndex = 8
        '
        'KeyЦиклЗагрузкиDataGridViewTextBoxColumn1
        '
        Me.KeyЦиклЗагрузкиDataGridViewTextBoxColumn1.DataPropertyName = "keyЦиклЗагрузки"
        Me.KeyЦиклЗагрузкиDataGridViewTextBoxColumn1.HeaderText = "keyЦиклЗагрузки"
        Me.KeyЦиклЗагрузкиDataGridViewTextBoxColumn1.Name = "KeyЦиклЗагрузкиDataGridViewTextBoxColumn1"
        Me.KeyЦиклЗагрузкиDataGridViewTextBoxColumn1.Width = 122
        '
        'KeyПерекладкиЦиклаDataGridViewTextBoxColumn
        '
        Me.KeyПерекладкиЦиклаDataGridViewTextBoxColumn.DataPropertyName = "keyПерекладкиЦикла"
        Me.KeyПерекладкиЦиклаDataGridViewTextBoxColumn.HeaderText = "keyПерекладкиЦикла"
        Me.KeyПерекладкиЦиклаDataGridViewTextBoxColumn.Name = "KeyПерекладкиЦиклаDataGridViewTextBoxColumn"
        Me.KeyПерекладкиЦиклаDataGridViewTextBoxColumn.Width = 143
        '
        'KeyУстройстваDataGridViewTextBoxColumn
        '
        Me.KeyУстройстваDataGridViewTextBoxColumn.DataPropertyName = "keyУстройства"
        Me.KeyУстройстваDataGridViewTextBoxColumn.HeaderText = "keyУстройства"
        Me.KeyУстройстваDataGridViewTextBoxColumn.Name = "KeyУстройстваDataGridViewTextBoxColumn"
        Me.KeyУстройстваDataGridViewTextBoxColumn.Width = 109
        '
        'ВеличинаЗагрузкиDataGridViewTextBoxColumn
        '
        Me.ВеличинаЗагрузкиDataGridViewTextBoxColumn.DataPropertyName = "ВеличинаЗагрузки"
        Me.ВеличинаЗагрузкиDataGridViewTextBoxColumn.HeaderText = "ВеличинаЗагрузки"
        Me.ВеличинаЗагрузкиDataGridViewTextBoxColumn.Name = "ВеличинаЗагрузкиDataGridViewTextBoxColumn"
        Me.ВеличинаЗагрузкиDataGridViewTextBoxColumn.Width = 127
        '
        'ЧисловоеЗначениеDataGridViewTextBoxColumn
        '
        Me.ЧисловоеЗначениеDataGridViewTextBoxColumn.DataPropertyName = "ЧисловоеЗначение"
        Me.ЧисловоеЗначениеDataGridViewTextBoxColumn.HeaderText = "ЧисловоеЗначение"
        Me.ЧисловоеЗначениеDataGridViewTextBoxColumn.Name = "ЧисловоеЗначениеDataGridViewTextBoxColumn"
        Me.ЧисловоеЗначениеDataGridViewTextBoxColumn.Width = 130
        '
        'TimeValueDataGridViewTextBoxColumn
        '
        Me.TimeValueDataGridViewTextBoxColumn.DataPropertyName = "TimeValue"
        Me.TimeValueDataGridViewTextBoxColumn.HeaderText = "TimeValue"
        Me.TimeValueDataGridViewTextBoxColumn.Name = "TimeValueDataGridViewTextBoxColumn"
        Me.TimeValueDataGridViewTextBoxColumn.Width = 82
        '
        'TimeActionDataGridViewTextBoxColumn
        '
        Me.TimeActionDataGridViewTextBoxColumn.DataPropertyName = "TimeAction"
        Me.TimeActionDataGridViewTextBoxColumn.HeaderText = "TimeAction"
        Me.TimeActionDataGridViewTextBoxColumn.Name = "TimeActionDataGridViewTextBoxColumn"
        Me.TimeActionDataGridViewTextBoxColumn.Width = 85
        '
        'BindingSource4ПерекладкиЦикла
        '
        Me.BindingSource4ПерекладкиЦикла.DataMember = "ПерекладкиЦикла4"
        Me.BindingSource4ПерекладкиЦикла.DataSource = Me.CycleDataSet1
        '
        'BindingNavigator4ПерекладкиЦикла
        '
        Me.BindingNavigator4ПерекладкиЦикла.AddNewItem = Me.BindingNavigatorAddNewItem3
        Me.BindingNavigator4ПерекладкиЦикла.BindingSource = Me.BindingSource4ПерекладкиЦикла
        Me.BindingNavigator4ПерекладкиЦикла.CountItem = Me.BindingNavigatorCountItem3
        Me.BindingNavigator4ПерекладкиЦикла.DeleteItem = Me.BNDeleteCycleShelf
        Me.BindingNavigator4ПерекладкиЦикла.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.BindingNavigator4ПерекладкиЦикла.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem3, Me.BindingNavigatorMovePreviousItem3, Me.BindingNavigatorSeparator9, Me.BindingNavigatorPositionItem3, Me.BindingNavigatorCountItem3, Me.BindingNavigatorSeparator10, Me.BindingNavigatorMoveNextItem3, Me.BindingNavigatorMoveLastItem3, Me.BindingNavigatorSeparator11, Me.BindingNavigatorAddNewItem3, Me.BNDeleteCycleShelf, Me.BNSaveSycleStage})
        Me.BindingNavigator4ПерекладкиЦикла.Location = New System.Drawing.Point(0, 0)
        Me.BindingNavigator4ПерекладкиЦикла.MoveFirstItem = Me.BindingNavigatorMoveFirstItem3
        Me.BindingNavigator4ПерекладкиЦикла.MoveLastItem = Me.BindingNavigatorMoveLastItem3
        Me.BindingNavigator4ПерекладкиЦикла.MoveNextItem = Me.BindingNavigatorMoveNextItem3
        Me.BindingNavigator4ПерекладкиЦикла.MovePreviousItem = Me.BindingNavigatorMovePreviousItem3
        Me.BindingNavigator4ПерекладкиЦикла.Name = "BindingNavigator4ПерекладкиЦикла"
        Me.BindingNavigator4ПерекладкиЦикла.PositionItem = Me.BindingNavigatorPositionItem3
        Me.BindingNavigator4ПерекладкиЦикла.Size = New System.Drawing.Size(248, 25)
        Me.BindingNavigator4ПерекладкиЦикла.TabIndex = 7
        Me.BindingNavigator4ПерекладкиЦикла.Text = "BindingNavigator4ПерекладкиЦикла"
        '
        'BindingNavigatorAddNewItem3
        '
        Me.BindingNavigatorAddNewItem3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorAddNewItem3.Image = CType(resources.GetObject("BindingNavigatorAddNewItem3.Image"), System.Drawing.Image)
        Me.BindingNavigatorAddNewItem3.Name = "BindingNavigatorAddNewItem3"
        Me.BindingNavigatorAddNewItem3.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorAddNewItem3.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorAddNewItem3.Text = "Добавить"
        '
        'BindingNavigatorCountItem3
        '
        Me.BindingNavigatorCountItem3.Name = "BindingNavigatorCountItem3"
        Me.BindingNavigatorCountItem3.Size = New System.Drawing.Size(43, 22)
        Me.BindingNavigatorCountItem3.Text = "для {0}"
        Me.BindingNavigatorCountItem3.ToolTipText = "Общее число элементов"
        '
        'BNDeleteCycleShelf
        '
        Me.BNDeleteCycleShelf.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BNDeleteCycleShelf.Image = CType(resources.GetObject("BNDeleteCycleShelf.Image"), System.Drawing.Image)
        Me.BNDeleteCycleShelf.Name = "BNDeleteCycleShelf"
        Me.BNDeleteCycleShelf.RightToLeftAutoMirrorImage = True
        Me.BNDeleteCycleShelf.Size = New System.Drawing.Size(23, 20)
        Me.BNDeleteCycleShelf.Text = "Удалить"
        '
        'BindingNavigatorMoveFirstItem3
        '
        Me.BindingNavigatorMoveFirstItem3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem3.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem3.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem3.Name = "BindingNavigatorMoveFirstItem3"
        Me.BindingNavigatorMoveFirstItem3.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem3.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem3.Text = "Переместить в начало"
        '
        'BindingNavigatorMovePreviousItem3
        '
        Me.BindingNavigatorMovePreviousItem3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem3.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem3.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem3.Name = "BindingNavigatorMovePreviousItem3"
        Me.BindingNavigatorMovePreviousItem3.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem3.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem3.Text = "Переместить назад"
        '
        'BindingNavigatorSeparator9
        '
        Me.BindingNavigatorSeparator9.Name = "BindingNavigatorSeparator9"
        Me.BindingNavigatorSeparator9.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorPositionItem3
        '
        Me.BindingNavigatorPositionItem3.AccessibleName = "Положение"
        Me.BindingNavigatorPositionItem3.AutoSize = False
        Me.BindingNavigatorPositionItem3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.BindingNavigatorPositionItem3.Name = "BindingNavigatorPositionItem3"
        Me.BindingNavigatorPositionItem3.Size = New System.Drawing.Size(50, 23)
        Me.BindingNavigatorPositionItem3.Text = "0"
        Me.BindingNavigatorPositionItem3.ToolTipText = "Текущее положение"
        '
        'BindingNavigatorSeparator10
        '
        Me.BindingNavigatorSeparator10.Name = "BindingNavigatorSeparator10"
        Me.BindingNavigatorSeparator10.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem3
        '
        Me.BindingNavigatorMoveNextItem3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem3.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem3.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem3.Name = "BindingNavigatorMoveNextItem3"
        Me.BindingNavigatorMoveNextItem3.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem3.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem3.Text = "Переместить вперед"
        '
        'BindingNavigatorMoveLastItem3
        '
        Me.BindingNavigatorMoveLastItem3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem3.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem3.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem3.Name = "BindingNavigatorMoveLastItem3"
        Me.BindingNavigatorMoveLastItem3.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem3.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem3.Text = "Переместить в конец"
        '
        'BindingNavigatorSeparator11
        '
        Me.BindingNavigatorSeparator11.Name = "BindingNavigatorSeparator11"
        Me.BindingNavigatorSeparator11.Size = New System.Drawing.Size(6, 25)
        '
        'BNSaveSycleStage
        '
        Me.BNSaveSycleStage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BNSaveSycleStage.Enabled = False
        Me.BNSaveSycleStage.Image = CType(resources.GetObject("BNSaveSycleStage.Image"), System.Drawing.Image)
        Me.BNSaveSycleStage.Name = "BNSaveSycleStage"
        Me.BNSaveSycleStage.Size = New System.Drawing.Size(23, 20)
        Me.BNSaveSycleStage.Text = "Save Data"
        Me.BNSaveSycleStage.ToolTipText = "Сохранить данные"
        '
        'ListViewCycle
        '
        Me.ListViewCycle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewCycle.FullRowSelect = True
        Me.ListViewCycle.GridLines = True
        Me.ListViewCycle.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ListViewCycle.HideSelection = False
        Me.ListViewCycle.Location = New System.Drawing.Point(0, 0)
        Me.ListViewCycle.MultiSelect = False
        Me.ListViewCycle.Name = "ListViewCycle"
        Me.ListViewCycle.Size = New System.Drawing.Size(520, 162)
        Me.ListViewCycle.SmallImageList = Me.ImageListCycle
        Me.ListViewCycle.TabIndex = 6
        Me.ListViewCycle.UseCompatibleStateImageBehavior = False
        Me.ListViewCycle.View = System.Windows.Forms.View.Details
        '
        'ImageListCycle
        '
        Me.ImageListCycle.ImageStream = CType(resources.GetObject("ImageListCycle.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListCycle.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListCycle.Images.SetKeyName(0, "CycleUp")
        Me.ImageListCycle.Images.SetKeyName(1, "CycleShelf")
        Me.ImageListCycle.Images.SetKeyName(2, "CycleDown")
        Me.ImageListCycle.Images.SetKeyName(3, "StageToNumeric")
        Me.ImageListCycle.Images.SetKeyName(4, "TimeDuration")
        Me.ImageListCycle.Images.SetKeyName(5, "TimeUnit")
        Me.ImageListCycle.Images.SetKeyName(6, "FromStart")
        Me.ImageListCycle.Images.SetKeyName(7, "StageToText")
        Me.ImageListCycle.Images.SetKeyName(8, "CriterionUp")
        Me.ImageListCycle.Images.SetKeyName(9, "CriterionDown")
        Me.ImageListCycle.Images.SetKeyName(10, "InformationMessage")
        Me.ImageListCycle.Images.SetKeyName(11, "AlarmMessage")
        '
        'HeaderStripCycle
        '
        Me.HeaderStripCycle.AutoSize = False
        Me.HeaderStripCycle.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold)
        Me.HeaderStripCycle.ForeColor = System.Drawing.Color.White
        Me.HeaderStripCycle.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.HeaderStripCycle.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabelCycle})
        Me.HeaderStripCycle.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table
        Me.HeaderStripCycle.Location = New System.Drawing.Point(0, 0)
        Me.HeaderStripCycle.Name = "HeaderStripCycle"
        Me.HeaderStripCycle.Size = New System.Drawing.Size(772, 25)
        Me.HeaderStripCycle.TabIndex = 7
        Me.HeaderStripCycle.Text = "HeaderStripCycle"
        '
        'ToolStripLabelCycle
        '
        Me.ToolStripLabelCycle.ForeColor = System.Drawing.Color.Maroon
        Me.ToolStripLabelCycle.Name = "ToolStripLabelCycle"
        Me.ToolStripLabelCycle.Size = New System.Drawing.Size(507, 19)
        Me.ToolStripLabelCycle.Text = "Перекладки циклограммы загрузки выбранного устройства"
        '
        'ToolStripEditCycle
        '
        Me.ToolStripEditCycle.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStripEditCycle.Enabled = False
        Me.ToolStripEditCycle.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripEditCycle.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStripEditCycle.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSLabelStage, Me.TSSeparator31, Me.TSComboBoxStage, Me.TSLabelUnit, Me.TSComboBoxTimeUnit, Me.TSSeparator32, Me.TSLabelTime, Me.TSSeparator33, Me.TSButtonAddRecord, Me.TSButtonEditRecord, Me.TSSeparator34, Me.TSButtonUp, Me.TSButtonDown, Me.TSSeparator35, Me.TSButtonCutRecords, Me.TSButtonCopyRecords, Me.TSButtonPasteRecords, Me.TSButtonClearRecords, Me.TSSeparator8, Me.ToolStripLabelSpase, Me.TSButtonSaveCycle})
        Me.ToolStripEditCycle.Location = New System.Drawing.Point(0, 187)
        Me.ToolStripEditCycle.Name = "ToolStripEditCycle"
        Me.ToolStripEditCycle.Size = New System.Drawing.Size(772, 39)
        Me.ToolStripEditCycle.TabIndex = 0
        Me.ToolStripEditCycle.Text = "ToolStripEditCycle"
        '
        'TSLabelStage
        '
        Me.TSLabelStage.Name = "TSLabelStage"
        Me.TSLabelStage.Size = New System.Drawing.Size(64, 36)
        Me.TSLabelStage.Text = "Величина:"
        '
        'TSSeparator31
        '
        Me.TSSeparator31.Name = "TSSeparator31"
        Me.TSSeparator31.Size = New System.Drawing.Size(6, 39)
        '
        'TSComboBoxStage
        '
        Me.TSComboBoxStage.AutoSize = False
        Me.TSComboBoxStage.Name = "TSComboBoxStage"
        Me.TSComboBoxStage.Size = New System.Drawing.Size(100, 23)
        Me.TSComboBoxStage.ToolTipText = "Выбрать величину загрузки"
        '
        'TSLabelUnit
        '
        Me.TSLabelUnit.Name = "TSLabelUnit"
        Me.TSLabelUnit.Size = New System.Drawing.Size(76, 36)
        Me.TSLabelUnit.Text = "Ед. времени:"
        '
        'TSComboBoxTimeUnit
        '
        Me.TSComboBoxTimeUnit.AutoSize = False
        Me.TSComboBoxTimeUnit.DropDownWidth = 50
        Me.TSComboBoxTimeUnit.Items.AddRange(New Object() {"Сек", "Мин", "Час"})
        Me.TSComboBoxTimeUnit.Name = "TSComboBoxTimeUnit"
        Me.TSComboBoxTimeUnit.Size = New System.Drawing.Size(50, 23)
        Me.TSComboBoxTimeUnit.ToolTipText = "Выбрать единицу измерения времени"
        '
        'TSSeparator32
        '
        Me.TSSeparator32.Name = "TSSeparator32"
        Me.TSSeparator32.Size = New System.Drawing.Size(6, 39)
        '
        'TSLabelTime
        '
        Me.TSLabelTime.Name = "TSLabelTime"
        Me.TSLabelTime.Size = New System.Drawing.Size(87, 36)
        Me.TSLabelTime.Text = "Длительность:"
        '
        'TSSeparator33
        '
        Me.TSSeparator33.Name = "TSSeparator33"
        Me.TSSeparator33.Size = New System.Drawing.Size(6, 39)
        '
        'TSButtonAddRecord
        '
        Me.TSButtonAddRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSButtonAddRecord.Image = CType(resources.GetObject("TSButtonAddRecord.Image"), System.Drawing.Image)
        Me.TSButtonAddRecord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonAddRecord.Name = "TSButtonAddRecord"
        Me.TSButtonAddRecord.Size = New System.Drawing.Size(36, 36)
        Me.TSButtonAddRecord.Text = "Добавить перекладку в цикл"
        Me.TSButtonAddRecord.ToolTipText = "Добавить перекладку в цикл"
        '
        'TSButtonEditRecord
        '
        Me.TSButtonEditRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSButtonEditRecord.Image = CType(resources.GetObject("TSButtonEditRecord.Image"), System.Drawing.Image)
        Me.TSButtonEditRecord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonEditRecord.Name = "TSButtonEditRecord"
        Me.TSButtonEditRecord.Size = New System.Drawing.Size(36, 36)
        Me.TSButtonEditRecord.Text = "Редактировать выделенную перекладку в цикле"
        Me.TSButtonEditRecord.ToolTipText = "Редактировать выделенную перекладку в цикле"
        '
        'TSSeparator34
        '
        Me.TSSeparator34.Name = "TSSeparator34"
        Me.TSSeparator34.Size = New System.Drawing.Size(6, 39)
        '
        'TSButtonUp
        '
        Me.TSButtonUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSButtonUp.Image = CType(resources.GetObject("TSButtonUp.Image"), System.Drawing.Image)
        Me.TSButtonUp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonUp.Name = "TSButtonUp"
        Me.TSButtonUp.Size = New System.Drawing.Size(36, 36)
        Me.TSButtonUp.Text = "Переместить выделенную строку вверх"
        Me.TSButtonUp.ToolTipText = "Переместить выделенную строку вверх"
        '
        'TSButtonDown
        '
        Me.TSButtonDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSButtonDown.Image = CType(resources.GetObject("TSButtonDown.Image"), System.Drawing.Image)
        Me.TSButtonDown.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonDown.Name = "TSButtonDown"
        Me.TSButtonDown.Size = New System.Drawing.Size(36, 36)
        Me.TSButtonDown.Text = "Переместить выделенную строку внирз"
        Me.TSButtonDown.ToolTipText = "Переместить выделенную строку внирз"
        '
        'TSSeparator35
        '
        Me.TSSeparator35.Name = "TSSeparator35"
        Me.TSSeparator35.Size = New System.Drawing.Size(6, 39)
        '
        'TSButtonCutRecords
        '
        Me.TSButtonCutRecords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSButtonCutRecords.Enabled = False
        Me.TSButtonCutRecords.Image = CType(resources.GetObject("TSButtonCutRecords.Image"), System.Drawing.Image)
        Me.TSButtonCutRecords.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonCutRecords.Name = "TSButtonCutRecords"
        Me.TSButtonCutRecords.Size = New System.Drawing.Size(36, 36)
        Me.TSButtonCutRecords.Text = "Удалить выделенные строки"
        Me.TSButtonCutRecords.ToolTipText = "Удалить выделенные строки"
        '
        'TSButtonCopyRecords
        '
        Me.TSButtonCopyRecords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSButtonCopyRecords.Enabled = False
        Me.TSButtonCopyRecords.Image = CType(resources.GetObject("TSButtonCopyRecords.Image"), System.Drawing.Image)
        Me.TSButtonCopyRecords.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonCopyRecords.Name = "TSButtonCopyRecords"
        Me.TSButtonCopyRecords.Size = New System.Drawing.Size(36, 36)
        Me.TSButtonCopyRecords.Text = "Скопировать выделенные строки"
        Me.TSButtonCopyRecords.ToolTipText = "Скопировать выделенные строки"
        '
        'TSButtonPasteRecords
        '
        Me.TSButtonPasteRecords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSButtonPasteRecords.Enabled = False
        Me.TSButtonPasteRecords.Image = CType(resources.GetObject("TSButtonPasteRecords.Image"), System.Drawing.Image)
        Me.TSButtonPasteRecords.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonPasteRecords.Name = "TSButtonPasteRecords"
        Me.TSButtonPasteRecords.Size = New System.Drawing.Size(36, 36)
        Me.TSButtonPasteRecords.Text = "Вставить скопированные строки"
        Me.TSButtonPasteRecords.ToolTipText = "Вставить скопированные строки"
        '
        'TSButtonClearRecords
        '
        Me.TSButtonClearRecords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSButtonClearRecords.Image = CType(resources.GetObject("TSButtonClearRecords.Image"), System.Drawing.Image)
        Me.TSButtonClearRecords.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonClearRecords.Name = "TSButtonClearRecords"
        Me.TSButtonClearRecords.Size = New System.Drawing.Size(36, 36)
        Me.TSButtonClearRecords.Text = "Удалить все строки"
        Me.TSButtonClearRecords.ToolTipText = "Удалить все строки"
        '
        'TSSeparator8
        '
        Me.TSSeparator8.Name = "TSSeparator8"
        Me.TSSeparator8.Size = New System.Drawing.Size(6, 39)
        '
        'ToolStripLabelSpase
        '
        Me.ToolStripLabelSpase.Name = "ToolStripLabelSpase"
        Me.ToolStripLabelSpase.Size = New System.Drawing.Size(13, 36)
        Me.ToolStripLabelSpase.Text = "  "
        '
        'TSButtonSaveCycle
        '
        Me.TSButtonSaveCycle.AutoSize = False
        Me.TSButtonSaveCycle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSButtonSaveCycle.Image = CType(resources.GetObject("TSButtonSaveCycle.Image"), System.Drawing.Image)
        Me.TSButtonSaveCycle.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonSaveCycle.Name = "TSButtonSaveCycle"
        Me.TSButtonSaveCycle.Size = New System.Drawing.Size(36, 36)
        Me.TSButtonSaveCycle.Text = "Сохранить циклограмму"
        Me.TSButtonSaveCycle.ToolTipText = "Сохранить циклограмму"
        '
        'ToolStripForm
        '
        Me.ToolStripForm.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSButtonNewItems, Me.TSSeparator25, Me.TSButtonFindAndReplace, Me.TSSeparator28, Me.TSComboBoxFind, Me.TSSeparator29, Me.TSSplitButtonDropItems})
        Me.ToolStripForm.Location = New System.Drawing.Point(0, 24)
        Me.ToolStripForm.Name = "ToolStripForm"
        Me.ToolStripForm.Size = New System.Drawing.Size(1008, 25)
        Me.ToolStripForm.TabIndex = 0
        Me.ToolStripForm.Text = "ToolStripForm"
        Me.ToolStripForm.Visible = False
        '
        'TSButtonNewItems
        '
        Me.TSButtonNewItems.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMenuItemNewType, Me.TSMenuItemNewProgramm, Me.TSSeparator20, Me.TSMenuItemNewDevice, Me.TSMenuItemNewCycle})
        Me.TSButtonNewItems.ImageTransparentColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.TSButtonNewItems.Name = "TSButtonNewItems"
        Me.TSButtonNewItems.Size = New System.Drawing.Size(61, 22)
        Me.TSButtonNewItems.Text = "&Новый"
        '
        'TSMenuItemNewType
        '
        Me.TSMenuItemNewType.ImageTransparentColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.TSMenuItemNewType.Name = "TSMenuItemNewType"
        Me.TSMenuItemNewType.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.TSMenuItemNewType.Size = New System.Drawing.Size(181, 22)
        Me.TSMenuItemNewType.Text = "&Тип"
        '
        'TSMenuItemNewProgramm
        '
        Me.TSMenuItemNewProgramm.ImageTransparentColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.TSMenuItemNewProgramm.Name = "TSMenuItemNewProgramm"
        Me.TSMenuItemNewProgramm.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.TSMenuItemNewProgramm.Size = New System.Drawing.Size(181, 22)
        Me.TSMenuItemNewProgramm.Text = "&Программа"
        '
        'TSSeparator20
        '
        Me.TSSeparator20.Name = "TSSeparator20"
        Me.TSSeparator20.Size = New System.Drawing.Size(178, 6)
        '
        'TSMenuItemNewDevice
        '
        Me.TSMenuItemNewDevice.ImageTransparentColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.TSMenuItemNewDevice.Name = "TSMenuItemNewDevice"
        Me.TSMenuItemNewDevice.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.TSMenuItemNewDevice.Size = New System.Drawing.Size(181, 22)
        Me.TSMenuItemNewDevice.Text = "&Устройство"
        '
        'TSMenuItemNewCycle
        '
        Me.TSMenuItemNewCycle.ImageTransparentColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.TSMenuItemNewCycle.Name = "TSMenuItemNewCycle"
        Me.TSMenuItemNewCycle.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.TSMenuItemNewCycle.Size = New System.Drawing.Size(181, 22)
        Me.TSMenuItemNewCycle.Text = "&Перекладка"
        '
        'TSSeparator25
        '
        Me.TSSeparator25.Name = "TSSeparator25"
        Me.TSSeparator25.Size = New System.Drawing.Size(6, 25)
        '
        'TSButtonFindAndReplace
        '
        Me.TSButtonFindAndReplace.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSSeparator27})
        Me.TSButtonFindAndReplace.ImageTransparentColor = System.Drawing.Color.FromArgb(CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.TSButtonFindAndReplace.Name = "TSButtonFindAndReplace"
        Me.TSButtonFindAndReplace.Size = New System.Drawing.Size(104, 22)
        Me.TSButtonFindAndReplace.Text = "Поиск/Замена"
        '
        'TSSeparator27
        '
        Me.TSSeparator27.Name = "TSSeparator27"
        Me.TSSeparator27.Size = New System.Drawing.Size(57, 6)
        '
        'TSSeparator28
        '
        Me.TSSeparator28.Name = "TSSeparator28"
        Me.TSSeparator28.Size = New System.Drawing.Size(6, 25)
        '
        'TSComboBoxFind
        '
        Me.TSComboBoxFind.ForeColor = System.Drawing.SystemColors.GrayText
        Me.TSComboBoxFind.Name = "TSComboBoxFind"
        Me.TSComboBoxFind.Size = New System.Drawing.Size(92, 25)
        Me.TSComboBoxFind.Text = "Ввод поиска"
        '
        'TSSeparator29
        '
        Me.TSSeparator29.Name = "TSSeparator29"
        Me.TSSeparator29.Size = New System.Drawing.Size(6, 25)
        '
        'TSSplitButtonDropItems
        '
        Me.TSSplitButtonDropItems.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSSplitButtonDropItems.Image = CType(resources.GetObject("TSSplitButtonDropItems.Image"), System.Drawing.Image)
        Me.TSSplitButtonDropItems.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSSplitButtonDropItems.Name = "TSSplitButtonDropItems"
        Me.TSSplitButtonDropItems.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always
        Me.TSSplitButtonDropItems.Size = New System.Drawing.Size(149, 19)
        Me.TSSplitButtonDropItems.Text = "&Add or Remove Buttons"
        '
        'TSSeparator6
        '
        Me.TSSeparator6.Name = "TSSeparator6"
        Me.TSSeparator6.Size = New System.Drawing.Size(233, 6)
        '
        'TSMenuItemToolbars
        '
        Me.TSMenuItemToolbars.CheckOnClick = True
        Me.TSMenuItemToolbars.Name = "TSMenuItemToolbars"
        Me.TSMenuItemToolbars.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.TSMenuItemToolbars.Size = New System.Drawing.Size(236, 22)
        Me.TSMenuItemToolbars.Text = "Панель &инструментов"
        '
        'TSSeparator5
        '
        Me.TSSeparator5.Name = "TSSeparator5"
        Me.TSSeparator5.Size = New System.Drawing.Size(233, 6)
        '
        'TSMenuItemExpandCollapseGroups
        '
        Me.TSMenuItemExpandCollapseGroups.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMenuItemShowNavigation, Me.TSMenuItemShowAllGraphs})
        Me.TSMenuItemExpandCollapseGroups.Name = "TSMenuItemExpandCollapseGroups"
        Me.TSMenuItemExpandCollapseGroups.Size = New System.Drawing.Size(236, 22)
        Me.TSMenuItemExpandCollapseGroups.Text = "Показать/Скрыть Группу"
        Me.TSMenuItemExpandCollapseGroups.ToolTipText = "Показать/Скрыть дополнительные панели"
        '
        'TSMenuItemShowNavigation
        '
        Me.TSMenuItemShowNavigation.Checked = True
        Me.TSMenuItemShowNavigation.CheckOnClick = True
        Me.TSMenuItemShowNavigation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TSMenuItemShowNavigation.Name = "TSMenuItemShowNavigation"
        Me.TSMenuItemShowNavigation.Size = New System.Drawing.Size(249, 22)
        Me.TSMenuItemShowNavigation.Text = "Панель навигации по изделиям"
        Me.TSMenuItemShowNavigation.ToolTipText = "Показать/Скрыть панель навигации по изделиям"
        '
        'TSMenuItemShowAllGraphs
        '
        Me.TSMenuItemShowAllGraphs.CheckOnClick = True
        Me.TSMenuItemShowAllGraphs.Name = "TSMenuItemShowAllGraphs"
        Me.TSMenuItemShowAllGraphs.Size = New System.Drawing.Size(249, 22)
        Me.TSMenuItemShowAllGraphs.Text = "Панель всех графиков"
        Me.TSMenuItemShowAllGraphs.ToolTipText = "Показать/Скрыть панель графиков всех уставок"
        '
        'TSMenuItemNavigationPanel
        '
        Me.TSMenuItemNavigationPanel.Image = Global.CycleCharge.My.Resources.Resources.Help
        Me.TSMenuItemNavigationPanel.Name = "TSMenuItemNavigationPanel"
        Me.TSMenuItemNavigationPanel.ShortcutKeyDisplayString = "Alt+F1"
        Me.TSMenuItemNavigationPanel.Size = New System.Drawing.Size(236, 22)
        Me.TSMenuItemNavigationPanel.Text = "&Панель подсказки"
        Me.TSMenuItemNavigationPanel.ToolTipText = "Показать панель справки управления графиком"
        '
        'TSMenuItemView
        '
        Me.TSMenuItemView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMenuItemNavigationPanel, Me.TSSeparator5, Me.TSMenuItemExpandCollapseGroups, Me.TSSeparator6, Me.TSMenuItemToolbars, Me.TSMenuItemStatusStrip})
        Me.TSMenuItemView.Name = "TSMenuItemView"
        Me.TSMenuItemView.Size = New System.Drawing.Size(69, 20)
        Me.TSMenuItemView.Text = "&Показать"
        '
        'TSMenuItemStatusStrip
        '
        Me.TSMenuItemStatusStrip.Checked = True
        Me.TSMenuItemStatusStrip.CheckOnClick = True
        Me.TSMenuItemStatusStrip.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TSMenuItemStatusStrip.Name = "TSMenuItemStatusStrip"
        Me.TSMenuItemStatusStrip.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.TSMenuItemStatusStrip.Size = New System.Drawing.Size(236, 22)
        Me.TSMenuItemStatusStrip.Text = "Панель &статуса"
        Me.TSMenuItemStatusStrip.ToolTipText = "Видимость панели статуса"
        '
        'TSMenuItemFile
        '
        Me.TSMenuItemFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMenuItemExit})
        Me.TSMenuItemFile.Name = "TSMenuItemFile"
        Me.TSMenuItemFile.Size = New System.Drawing.Size(48, 20)
        Me.TSMenuItemFile.Text = "&Окно"
        '
        'TSMenuItemExit
        '
        Me.TSMenuItemExit.Image = CType(resources.GetObject("TSMenuItemExit.Image"), System.Drawing.Image)
        Me.TSMenuItemExit.Name = "TSMenuItemExit"
        Me.TSMenuItemExit.Size = New System.Drawing.Size(120, 22)
        Me.TSMenuItemExit.Text = "&Закрыть"
        Me.TSMenuItemExit.ToolTipText = "Закрыть окно"
        '
        'MenuStripForm
        '
        Me.MenuStripForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMenuItemFile, Me.TSMenuItemView})
        Me.MenuStripForm.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripForm.Name = "MenuStripForm"
        Me.MenuStripForm.Size = New System.Drawing.Size(1070, 24)
        Me.MenuStripForm.TabIndex = 1
        Me.MenuStripForm.Text = "menuStrip1"
        '
        'TimerUpdate
        '
        Me.TimerUpdate.Interval = 1000
        '
        'ErrorProvider4ПерекладкиЦикла
        '
        Me.ErrorProvider4ПерекладкиЦикла.ContainerControl = Me
        Me.ErrorProvider4ПерекладкиЦикла.DataSource = Me.BindingSource4ПерекладкиЦикла
        '
        'ContextMenuAnnotation
        '
        Me.ContextMenuAnnotation.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMenuItemDeletePause})
        Me.ContextMenuAnnotation.Name = "ContextMenuStrip1"
        Me.ContextMenuAnnotation.Size = New System.Drawing.Size(135, 26)
        '
        'TSMenuItemDeletePause
        '
        Me.TSMenuItemDeletePause.Image = CType(resources.GetObject("TSMenuItemDeletePause.Image"), System.Drawing.Image)
        Me.TSMenuItemDeletePause.Name = "TSMenuItemDeletePause"
        Me.TSMenuItemDeletePause.Size = New System.Drawing.Size(134, 22)
        Me.TSMenuItemDeletePause.Text = "Удалить КТ"
        Me.TSMenuItemDeletePause.ToolTipText = "Удалить выделенную паузу КТ"
        '
        'ТипИзделия1TableAdapter
        '
        Me.ТипИзделия1TableAdapter.ClearBeforeFill = True
        '
        'ПрограммаИспытаний2TableAdapter
        '
        Me.ПрограммаИспытаний2TableAdapter.ClearBeforeFill = True
        '
        'ЦиклЗагрузки3TableAdapter
        '
        Me.ЦиклЗагрузки3TableAdapter.ClearBeforeFill = True
        '
        'ПерекладкиЦикла4TableAdapter
        '
        Me.ПерекладкиЦикла4TableAdapter.ClearBeforeFill = True
        '
        'FormEditorCyclogram
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1070, 838)
        Me.Controls.Add(Me.SplitContainerForm)
        Me.Controls.Add(Me.StatusStripForm)
        Me.Controls.Add(Me.ToolStripForm)
        Me.Controls.Add(Me.MenuStripForm)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(636, 391)
        Me.Name = "FormEditorCyclogram"
        Me.Tag = "Редактор циклограмм"
        Me.Text = "Редактор циклограмм"
        Me.StatusStripForm.ResumeLayout(False)
        Me.StatusStripForm.PerformLayout()
        Me.SplitContainerForm.Panel1.ResumeLayout(False)
        Me.SplitContainerForm.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerForm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerForm.ResumeLayout(False)
        Me.SplitContainerTree.Panel1.ResumeLayout(False)
        Me.SplitContainerTree.Panel2.ResumeLayout(False)
        Me.SplitContainerTree.Panel2.PerformLayout()
        CType(Me.SplitContainerTree, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerTree.ResumeLayout(False)
        Me.SplitContainerTypeEngine.Panel1.ResumeLayout(False)
        Me.SplitContainerTypeEngine.Panel1.PerformLayout()
        Me.SplitContainerTypeEngine.Panel2.ResumeLayout(False)
        Me.SplitContainerTypeEngine.Panel2.PerformLayout()
        CType(Me.SplitContainerTypeEngine, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerTypeEngine.ResumeLayout(False)
        CType(Me.DataGridView1ТипИзделия, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1ТипИзделия, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CycleDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator1ТипИзделия, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator1ТипИзделия.ResumeLayout(False)
        Me.BindingNavigator1ТипИзделия.PerformLayout()
        Me.HeaderStripEngine.ResumeLayout(False)
        Me.HeaderStripEngine.PerformLayout()
        CType(Me.DataGridView2ПрограммаИспытаний, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource2ПрограммаИспытаний, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator2ПрограммаИспытаний, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator2ПрограммаИспытаний.ResumeLayout(False)
        Me.BindingNavigator2ПрограммаИспытаний.PerformLayout()
        Me.HeaderStripProgram.ResumeLayout(False)
        Me.HeaderStripProgram.PerformLayout()
        CType(Me.DataGridView3ЦиклУправИсполУстр, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource3ЦиклУправИсполУстр, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator3ЦиклУправИсполУстр, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator3ЦиклУправИсполУстр.ResumeLayout(False)
        Me.BindingNavigator3ЦиклУправИсполУстр.PerformLayout()
        Me.HeaderStripDevice.ResumeLayout(False)
        Me.HeaderStripDevice.PerformLayout()
        Me.SplitContainerEditor.Panel1.ResumeLayout(False)
        Me.SplitContainerEditor.Panel2.ResumeLayout(False)
        Me.SplitContainerEditor.Panel2.PerformLayout()
        CType(Me.SplitContainerEditor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerEditor.ResumeLayout(False)
        Me.SplitContainerGraf.Panel1.ResumeLayout(False)
        Me.SplitContainerGraf.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerGraf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerGraf.ResumeLayout(False)
        CType(Me.ScatterGraphCycle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XyCursor1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelCycleAndLayoutPanel.ResumeLayout(False)
        Me.PanelCycleAndLayoutPanel.PerformLayout()
        CType(Me.ScatterGraphSelectDevice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanelAttributes.ResumeLayout(False)
        Me.TableLayoutPanelAttributes.PerformLayout()
        Me.TSCycle.ResumeLayout(False)
        Me.TSCycle.PerformLayout()
        Me.SplitContainerCycle.Panel1.ResumeLayout(False)
        Me.SplitContainerCycle.Panel1.PerformLayout()
        Me.SplitContainerCycle.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerCycle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerCycle.ResumeLayout(False)
        CType(Me.DataGridView4ПерекладкиЦикла, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource4ПерекладкиЦикла, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingNavigator4ПерекладкиЦикла, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BindingNavigator4ПерекладкиЦикла.ResumeLayout(False)
        Me.BindingNavigator4ПерекладкиЦикла.PerformLayout()
        Me.HeaderStripCycle.ResumeLayout(False)
        Me.HeaderStripCycle.PerformLayout()
        Me.ToolStripEditCycle.ResumeLayout(False)
        Me.ToolStripEditCycle.PerformLayout()
        Me.ToolStripForm.ResumeLayout(False)
        Me.ToolStripForm.PerformLayout()
        Me.MenuStripForm.ResumeLayout(False)
        Me.MenuStripForm.PerformLayout()
        CType(Me.ErrorProvider4ПерекладкиЦикла, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuAnnotation.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents LabelDevicesCount As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents TSStatusLabelMessage As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents SplitContainerForm As System.Windows.Forms.SplitContainer
    Private WithEvents SplitContainerGraf As System.Windows.Forms.SplitContainer
    Private WithEvents ToolStripForm As System.Windows.Forms.ToolStrip
    Private WithEvents TSButtonNewItems As System.Windows.Forms.ToolStripSplitButton
    Private WithEvents TSMenuItemNewType As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents TSMenuItemNewProgramm As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents TSSeparator20 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents TSMenuItemNewDevice As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents TSMenuItemNewCycle As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents TSSeparator25 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents TSButtonFindAndReplace As System.Windows.Forms.ToolStripSplitButton
    Private WithEvents TSSeparator27 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents TSSeparator28 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents TSComboBoxFind As System.Windows.Forms.ToolStripComboBox
    Private WithEvents TSSeparator29 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents TSSplitButtonDropItems As System.Windows.Forms.ToolStripSplitButton
    Private WithEvents TSSeparator6 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents TSMenuItemToolbars As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents TSSeparator5 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents TSMenuItemExpandCollapseGroups As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents TSMenuItemShowNavigation As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents TSMenuItemView As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents TSMenuItemFile As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents MenuStripForm As System.Windows.Forms.MenuStrip
    Friend WithEvents SplitContainerEditor As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStripEditCycle As System.Windows.Forms.ToolStrip
    Friend WithEvents TSLabelStage As System.Windows.Forms.ToolStripLabel
    Friend WithEvents TSSeparator31 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSLabelUnit As System.Windows.Forms.ToolStripLabel
    Friend WithEvents TSComboBoxTimeUnit As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents TSSeparator32 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSLabelTime As System.Windows.Forms.ToolStripLabel
    Friend WithEvents TSSeparator33 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSButtonAddRecord As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSButtonEditRecord As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSSeparator34 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSButtonUp As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSButtonDown As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSSeparator35 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSButtonCutRecords As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSButtonClearRecords As System.Windows.Forms.ToolStripButton
    Friend WithEvents ListViewCycle As System.Windows.Forms.ListView
    Friend WithEvents SplitContainerTree As System.Windows.Forms.SplitContainer
    Private WithEvents HeaderStripCycle As CycleCharge.HeaderStrip
    Friend WithEvents ToolStripLabelCycle As System.Windows.Forms.ToolStripLabel
    Friend WithEvents StatusStripForm As System.Windows.Forms.StatusStrip
    Friend WithEvents SplitContainerTypeEngine As System.Windows.Forms.SplitContainer
    Private WithEvents HeaderStripEngine As CycleCharge.HeaderStrip
    Friend WithEvents ToolStripLabelEngine As System.Windows.Forms.ToolStripLabel
    Private WithEvents HeaderStripProgram As CycleCharge.HeaderStrip
    Friend WithEvents ToolStripLabelProgram As System.Windows.Forms.ToolStripLabel
    Private WithEvents HeaderStripDevice As CycleCharge.HeaderStrip
    Friend WithEvents ToolStripLabelDevice As System.Windows.Forms.ToolStripLabel
    Friend WithEvents TimerUpdate As System.Windows.Forms.Timer
    Friend WithEvents SplitContainerCycle As System.Windows.Forms.SplitContainer
    Friend WithEvents BindingNavigator4ПерекладкиЦикла As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingSource4ПерекладкиЦикла As System.Windows.Forms.BindingSource
    Friend WithEvents BindingNavigatorCountItem3 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BNDeleteCycleShelf As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveFirstItem3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem3 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BNSaveSycleStage As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigator3ЦиклУправИсполУстр As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingSource3ЦиклУправИсполУстр As System.Windows.Forms.BindingSource
    Friend WithEvents BindingNavigatorCountItem2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem2 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BNDeleteSycleStage As System.Windows.Forms.ToolStripButton
    Friend WithEvents BNSaveSycleDevices As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigator2ПрограммаИспытаний As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingSource2ПрограммаИспытаний As System.Windows.Forms.BindingSource
    Friend WithEvents BindingNavigatorCountItem1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem1 As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BNDeleteTestPrograms As System.Windows.Forms.ToolStripButton
    Friend WithEvents BNSaveTestPrograms As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigator1ТипИзделия As System.Windows.Forms.BindingNavigator
    Friend WithEvents BindingNavigatorAddNewItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingSource1ТипИзделия As System.Windows.Forms.BindingSource
    Friend WithEvents BindingNavigatorCountItem As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorPositionItem As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BNDeleteTypeEngine As System.Windows.Forms.ToolStripButton
    Friend WithEvents BNSaveTypeEngine As System.Windows.Forms.ToolStripButton
    Friend WithEvents ErrorProvider4ПерекладкиЦикла As System.Windows.Forms.ErrorProvider
    Friend WithEvents DataGridView1ТипИзделия As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2ПрограммаИспытаний As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView3ЦиклУправИсполУстр As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView4ПерекладкиЦикла As System.Windows.Forms.DataGridView
    Friend WithEvents KeyЦиклограммаЗапускаDataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Private WithEvents PanelCycleAndLayoutPanel As System.Windows.Forms.Panel
    Public WithEvents ScatterGraphSelectDevice As NationalInstruments.UI.WindowsForms.ScatterGraph
    Friend WithEvents ScatterPlot1 As NationalInstruments.UI.ScatterPlot
    Friend WithEvents XAxis1 As NationalInstruments.UI.XAxis
    Friend WithEvents YAxis1 As NationalInstruments.UI.YAxis
    Private WithEvents TableLayoutPanelAttributes As System.Windows.Forms.TableLayoutPanel
    Private WithEvents LabelMinMaxDevice As System.Windows.Forms.Label
    Private WithEvents LabelMinMax As System.Windows.Forms.Label
    Private WithEvents LabelCaption As System.Windows.Forms.Label
    Private WithEvents LabelDescription As System.Windows.Forms.Label
    Private WithEvents LabelReplied As System.Windows.Forms.Label
    Private WithEvents LabelNameChargeParameter As System.Windows.Forms.Label
    Private WithEvents LabelUnitOfMeasure As System.Windows.Forms.Label
    Friend WithEvents ToolTipForm As System.Windows.Forms.ToolTip
    Friend WithEvents TSCycle As System.Windows.Forms.ToolStrip
    Friend WithEvents ButtonShowMore As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabelSpase As System.Windows.Forms.ToolStripLabel
    Friend WithEvents TSButtonSaveCycle As System.Windows.Forms.ToolStripButton
    Private WithEvents LabelTimeAllCyrcle As System.Windows.Forms.Label
    Friend WithEvents ScatterGraphCycle As NationalInstruments.UI.WindowsForms.ScatterGraph
    Friend WithEvents XyCursor1 As NationalInstruments.UI.XYCursor
    Friend WithEvents ScatterPlot2 As NationalInstruments.UI.ScatterPlot
    Friend WithEvents XAxis2 As NationalInstruments.UI.XAxis
    Friend WithEvents YAxis2 As NationalInstruments.UI.YAxis
    Friend WithEvents ContextMenuAnnotation As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TSMenuItemDeletePause As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMenuItemNavigationPanel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMenuItemExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMenuItemStatusStrip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMenuItemShowAllGraphs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CycleDataSet1 As CycleDataSet
    Friend WithEvents ТипИзделия1TableAdapter As CycleDataSetTableAdapters.ТипИзделия1TableAdapter
    Friend WithEvents ТипИзделияDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KeyТипИзделияDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ОписаниеDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KeyТипИзделияDataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KeyПрограммаИспытанийDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ИмяПрограммыDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ОписаниеDataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ПрограммаИспытаний2TableAdapter As CycleDataSetTableAdapters.ПрограммаИспытаний2TableAdapter
    Friend WithEvents ЦиклЗагрузки3TableAdapter As CycleDataSetTableAdapters.ЦиклЗагрузки3TableAdapter
    Friend WithEvents ПерекладкиЦикла4TableAdapter As CycleDataSetTableAdapters.ПерекладкиЦикла4TableAdapter
    Friend WithEvents УстройствоNewDataGridViewTextBoxColumn As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents TSComboBoxStage As Windows.Forms.ToolStripComboBox
    Friend WithEvents KeyЦиклЗагрузкиDataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KeyПерекладкиЦиклаDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KeyУстройстваDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ВеличинаЗагрузкиDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ЧисловоеЗначениеDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TimeValueDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TimeActionDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KeyПрограммаИспытанийDataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents KeyЦиклЗагрузкиDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ИмяУстройстваDataGridViewTextBoxColumn As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ИмяУстройстваNewDataGridViewTextBoxColumn As Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents ImageListCycle As Windows.Forms.ImageList
    Friend WithEvents TSButtonCopyRecords As Windows.Forms.ToolStripButton
    Friend WithEvents TSButtonPasteRecords As Windows.Forms.ToolStripButton
End Class
