' Copyright (c) Microsoft Corporation. All rights reserved.
Partial Public Class FormPlayerCyclogram
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
            If TSComboBoxEngines IsNot Nothing Then
                TSComboBoxEngines.Dispose()
                TSComboBoxEngines = Nothing
            End If
            If UserNumericCycle IsNot Nothing Then
                UserNumericCycle.Dispose()
                UserNumericCycle = Nothing
            End If
            If curToolStrip IsNot Nothing Then
                curToolStrip.Dispose()
                curToolStrip = Nothing
            End If
            If curMenuItem IsNot Nothing Then
                curMenuItem.Dispose()
                curMenuItem = Nothing
            End If
            If TempPointAnnotation IsNot Nothing Then
                TempPointAnnotation.Dispose()
                TempPointAnnotation = Nothing
            End If
            'If backgroundWorker IsNot Nothing Then
            '    backgroundWorker.Dispose()
            '    backgroundWorker = Nothing
            'End If
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormPlayerCyclogram))
        Me.SplitContainerAll = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerCycle = New System.Windows.Forms.SplitContainer()
        Me.TreeViewSycleProgram = New System.Windows.Forms.TreeView()
        Me.ImageListNodes = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolStripSelectCycle = New System.Windows.Forms.ToolStrip()
        Me.TSLabelSelectedCycle = New System.Windows.Forms.ToolStripLabel()
        Me.TSLabelSycle = New System.Windows.Forms.ToolStripLabel()
        Me.HeaderStripSelectCycle = New CycleCharge.HeaderStrip(Me.components)
        Me.TSLabelSelectCycle = New System.Windows.Forms.ToolStripLabel()
        Me.SplitContainerGraph = New System.Windows.Forms.SplitContainer()
        Me.ScatterGraphCycle = New NationalInstruments.UI.WindowsForms.ScatterGraph()
        Me.XyCursor1 = New NationalInstruments.UI.XYCursor()
        Me.ScatterPlot2 = New NationalInstruments.UI.ScatterPlot()
        Me.XAxis1 = New NationalInstruments.UI.XAxis()
        Me.YAxis1 = New NationalInstruments.UI.YAxis()
        Me.FlowLayoutPanelControls = New System.Windows.Forms.FlowLayoutPanel()
        Me.SplitContainerLed = New System.Windows.Forms.SplitContainer()
        Me.PanelDigitalLed = New System.Windows.Forms.Panel()
        Me.HeaderStripPanelPorts = New CycleCharge.HeaderStrip(Me.components)
        Me.TSLabelPorts = New System.Windows.Forms.ToolStripLabel()
        Me.PanelValuePovers = New System.Windows.Forms.Panel()
        Me.SplitContainerMessage = New System.Windows.Forms.SplitContainer()
        Me.ListViewPowerValue = New CycleCharge.DbListView(Me.components)
        Me.ListViewAlarms = New CycleCharge.DbListView(Me.components)
        Me.HeaderStripMessage = New CycleCharge.HeaderStrip(Me.components)
        Me.TSLabelMessage = New System.Windows.Forms.ToolStripLabel()
        Me.HeaderStripPanel2 = New CycleCharge.HeaderStrip(Me.components)
        Me.ToolStripLabelOutput = New System.Windows.Forms.ToolStripLabel()
        Me.StatusStripForm = New System.Windows.Forms.StatusStrip()
        Me.TSSLabelStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSLabelRemain = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSLabelTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TSProgressBar = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripOperation = New System.Windows.Forms.ToolStrip()
        Me.TSButtonLanch = New System.Windows.Forms.ToolStripButton()
        Me.TSButtonPause = New System.Windows.Forms.ToolStripButton()
        Me.TSButtonReady = New System.Windows.Forms.ToolStripButton()
        Me.TSButtonAlarm = New System.Windows.Forms.ToolStripButton()
        Me.TSButtonForward = New System.Windows.Forms.ToolStripButton()
        Me.TSButtonBackwards = New System.Windows.Forms.ToolStripButton()
        Me.MenuStripForm = New System.Windows.Forms.MenuStrip()
        Me.FileMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemLanch = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSMenuItemExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemShowTreeView = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemShowPahelLeds = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemShowPowerMeterControls = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSpMenuItemShowStatusStrip = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMenuItemShowNavigationPanel = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckedListMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddOptionMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveOptionMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripBar = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolTipForm = New System.Windows.Forms.ToolTip(Me.components)
        Me.ScatterPlot1 = New NationalInstruments.UI.ScatterPlot()
        Me.ToolStripVert = New System.Windows.Forms.ToolStrip()
        Me.TimerCycle = New System.Windows.Forms.Timer(Me.components)
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStripDevice = New System.Windows.Forms.ToolStrip()
        Me.TSButtonShowTreeView = New System.Windows.Forms.ToolStripButton()
        Me.TButtonShowPahelLeds = New System.Windows.Forms.ToolStripButton()
        Me.TSButtonShowPowerMeterControls = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSLabelCurrentTime = New System.Windows.Forms.ToolStripLabel()
        Me.TSTextBoxCurrentTime = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.TextError = New System.Windows.Forms.ToolStripTextBox()
        Me.TimerResize = New System.Windows.Forms.Timer(Me.components)
        Me.ImageListMessage = New System.Windows.Forms.ImageList(Me.components)
        CType(Me.SplitContainerAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerAll.Panel1.SuspendLayout()
        Me.SplitContainerAll.Panel2.SuspendLayout()
        Me.SplitContainerAll.SuspendLayout()
        CType(Me.SplitContainerCycle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerCycle.Panel1.SuspendLayout()
        Me.SplitContainerCycle.Panel2.SuspendLayout()
        Me.SplitContainerCycle.SuspendLayout()
        Me.ToolStripSelectCycle.SuspendLayout()
        Me.HeaderStripSelectCycle.SuspendLayout()
        CType(Me.SplitContainerGraph, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerGraph.Panel1.SuspendLayout()
        Me.SplitContainerGraph.Panel2.SuspendLayout()
        Me.SplitContainerGraph.SuspendLayout()
        CType(Me.ScatterGraphCycle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XyCursor1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainerLed, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerLed.Panel1.SuspendLayout()
        Me.SplitContainerLed.Panel2.SuspendLayout()
        Me.SplitContainerLed.SuspendLayout()
        Me.HeaderStripPanelPorts.SuspendLayout()
        Me.PanelValuePovers.SuspendLayout()
        CType(Me.SplitContainerMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerMessage.Panel1.SuspendLayout()
        Me.SplitContainerMessage.Panel2.SuspendLayout()
        Me.SplitContainerMessage.SuspendLayout()
        Me.HeaderStripMessage.SuspendLayout()
        Me.HeaderStripPanel2.SuspendLayout()
        Me.StatusStripForm.SuspendLayout()
        Me.ToolStripOperation.SuspendLayout()
        Me.MenuStripForm.SuspendLayout()
        Me.ToolStripDevice.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainerAll
        '
        Me.SplitContainerAll.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerAll.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerAll.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainerAll.Location = New System.Drawing.Point(74, 49)
        Me.SplitContainerAll.Name = "SplitContainerAll"
        Me.SplitContainerAll.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerAll.Panel1
        '
        Me.SplitContainerAll.Panel1.Controls.Add(Me.SplitContainerCycle)
        '
        'SplitContainerAll.Panel2
        '
        Me.SplitContainerAll.Panel2.Controls.Add(Me.SplitContainerLed)
        Me.SplitContainerAll.Size = New System.Drawing.Size(1123, 655)
        Me.SplitContainerAll.SplitterDistance = 427
        Me.SplitContainerAll.TabIndex = 26
        '
        'SplitContainerCycle
        '
        Me.SplitContainerCycle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerCycle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerCycle.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerCycle.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerCycle.Name = "SplitContainerCycle"
        '
        'SplitContainerCycle.Panel1
        '
        Me.SplitContainerCycle.Panel1.Controls.Add(Me.TreeViewSycleProgram)
        Me.SplitContainerCycle.Panel1.Controls.Add(Me.ToolStripSelectCycle)
        Me.SplitContainerCycle.Panel1.Controls.Add(Me.HeaderStripSelectCycle)
        '
        'SplitContainerCycle.Panel2
        '
        Me.SplitContainerCycle.Panel2.Controls.Add(Me.SplitContainerGraph)
        Me.SplitContainerCycle.Size = New System.Drawing.Size(1123, 427)
        Me.SplitContainerCycle.SplitterDistance = 226
        Me.SplitContainerCycle.TabIndex = 1
        '
        'TreeViewSycleProgram
        '
        Me.TreeViewSycleProgram.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TreeViewSycleProgram.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TreeViewSycleProgram.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeViewSycleProgram.FullRowSelect = True
        Me.TreeViewSycleProgram.HideSelection = False
        Me.TreeViewSycleProgram.HotTracking = True
        Me.TreeViewSycleProgram.ImageIndex = 0
        Me.TreeViewSycleProgram.ImageList = Me.ImageListNodes
        Me.TreeViewSycleProgram.Location = New System.Drawing.Point(0, 50)
        Me.TreeViewSycleProgram.Name = "TreeViewSycleProgram"
        Me.TreeViewSycleProgram.SelectedImageIndex = 0
        Me.TreeViewSycleProgram.Size = New System.Drawing.Size(222, 373)
        Me.TreeViewSycleProgram.TabIndex = 20
        '
        'ImageListNodes
        '
        Me.ImageListNodes.ImageStream = CType(resources.GetObject("ImageListNodes.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListNodes.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListNodes.Images.SetKeyName(0, "select")
        Me.ImageListNodes.Images.SetKeyName(1, "engine")
        Me.ImageListNodes.Images.SetKeyName(2, "programm")
        Me.ImageListNodes.Images.SetKeyName(3, "cycle")
        Me.ImageListNodes.Images.SetKeyName(4, "power")
        '
        'ToolStripSelectCycle
        '
        Me.ToolStripSelectCycle.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripSelectCycle.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSLabelSelectedCycle, Me.TSLabelSycle})
        Me.ToolStripSelectCycle.Location = New System.Drawing.Point(0, 25)
        Me.ToolStripSelectCycle.Name = "ToolStripSelectCycle"
        Me.ToolStripSelectCycle.Size = New System.Drawing.Size(222, 25)
        Me.ToolStripSelectCycle.TabIndex = 21
        Me.ToolStripSelectCycle.Text = "TSВыборЦикла"
        '
        'TSLabelSelectedCycle
        '
        Me.TSLabelSelectedCycle.Name = "TSLabelSelectedCycle"
        Me.TSLabelSelectedCycle.Size = New System.Drawing.Size(59, 22)
        Me.TSLabelSelectedCycle.Text = "Выбрана:"
        '
        'TSLabelSycle
        '
        Me.TSLabelSycle.BackColor = System.Drawing.Color.White
        Me.TSLabelSycle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.TSLabelSycle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TSLabelSycle.ForeColor = System.Drawing.Color.Blue
        Me.TSLabelSycle.Name = "TSLabelSycle"
        Me.TSLabelSycle.Size = New System.Drawing.Size(39, 22)
        Me.TSLabelSycle.Text = "Цикл"
        Me.TSLabelSycle.ToolTipText = "Имя выбранной перекладки"
        '
        'HeaderStripSelectCycle
        '
        Me.HeaderStripSelectCycle.AutoSize = False
        Me.HeaderStripSelectCycle.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold)
        Me.HeaderStripSelectCycle.ForeColor = System.Drawing.Color.White
        Me.HeaderStripSelectCycle.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.HeaderStripSelectCycle.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSLabelSelectCycle})
        Me.HeaderStripSelectCycle.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table
        Me.HeaderStripSelectCycle.Location = New System.Drawing.Point(0, 0)
        Me.HeaderStripSelectCycle.Name = "HeaderStripSelectCycle"
        Me.HeaderStripSelectCycle.Size = New System.Drawing.Size(222, 25)
        Me.HeaderStripSelectCycle.TabIndex = 22
        Me.HeaderStripSelectCycle.Text = "HeaderStrip3"
        '
        'TSLabelSelectCycle
        '
        Me.TSLabelSelectCycle.ForeColor = System.Drawing.Color.Maroon
        Me.TSLabelSelectCycle.Name = "TSLabelSelectCycle"
        Me.TSLabelSelectCycle.Size = New System.Drawing.Size(285, 19)
        Me.TSLabelSelectCycle.Text = "Выбор циклограмы для изделия"
        '
        'SplitContainerGraph
        '
        Me.SplitContainerGraph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerGraph.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerGraph.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainerGraph.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerGraph.Name = "SplitContainerGraph"
        '
        'SplitContainerGraph.Panel1
        '
        Me.SplitContainerGraph.Panel1.Controls.Add(Me.ScatterGraphCycle)
        '
        'SplitContainerGraph.Panel2
        '
        Me.SplitContainerGraph.Panel2.Controls.Add(Me.FlowLayoutPanelControls)
        Me.SplitContainerGraph.Size = New System.Drawing.Size(893, 427)
        Me.SplitContainerGraph.SplitterDistance = 676
        Me.SplitContainerGraph.TabIndex = 2
        '
        'ScatterGraphCycle
        '
        Me.ScatterGraphCycle.Caption = "Циклограмма управления устройствами в испытаниии"
        Me.ScatterGraphCycle.CaptionBackColor = System.Drawing.Color.LightGray
        Me.ScatterGraphCycle.CaptionFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ScatterGraphCycle.CaptionForeColor = System.Drawing.Color.Maroon
        Me.ScatterGraphCycle.Cursors.AddRange(New NationalInstruments.UI.XYCursor() {Me.XyCursor1})
        Me.ScatterGraphCycle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScatterGraphCycle.InteractionMode = CType(((((NationalInstruments.UI.GraphInteractionModes.ZoomX Or NationalInstruments.UI.GraphInteractionModes.ZoomAroundPoint) _
            Or NationalInstruments.UI.GraphInteractionModes.PanX) _
            Or NationalInstruments.UI.GraphInteractionModes.DragCursor) _
            Or NationalInstruments.UI.GraphInteractionModes.DragAnnotationCaption), NationalInstruments.UI.GraphInteractionModes)
        Me.ScatterGraphCycle.Location = New System.Drawing.Point(0, 0)
        Me.ScatterGraphCycle.Name = "ScatterGraphCycle"
        Me.ScatterGraphCycle.PlotAreaColor = System.Drawing.Color.WhiteSmoke
        Me.ScatterGraphCycle.Plots.AddRange(New NationalInstruments.UI.ScatterPlot() {Me.ScatterPlot2})
        Me.ScatterGraphCycle.Size = New System.Drawing.Size(672, 423)
        Me.ScatterGraphCycle.TabIndex = 0
        Me.ScatterGraphCycle.XAxes.AddRange(New NationalInstruments.UI.XAxis() {Me.XAxis1})
        Me.ScatterGraphCycle.YAxes.AddRange(New NationalInstruments.UI.YAxis() {Me.YAxis1})
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
        Me.ScatterPlot2.XAxis = Me.XAxis1
        Me.ScatterPlot2.YAxis = Me.YAxis1
        '
        'FlowLayoutPanelControls
        '
        Me.FlowLayoutPanelControls.AutoScroll = True
        Me.FlowLayoutPanelControls.BackColor = System.Drawing.Color.DimGray
        Me.FlowLayoutPanelControls.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanelControls.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanelControls.Name = "FlowLayoutPanelControls"
        Me.FlowLayoutPanelControls.Size = New System.Drawing.Size(209, 423)
        Me.FlowLayoutPanelControls.TabIndex = 1
        '
        'SplitContainerLed
        '
        Me.SplitContainerLed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerLed.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerLed.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerLed.Name = "SplitContainerLed"
        '
        'SplitContainerLed.Panel1
        '
        Me.SplitContainerLed.Panel1.Controls.Add(Me.PanelDigitalLed)
        Me.SplitContainerLed.Panel1.Controls.Add(Me.HeaderStripPanelPorts)
        '
        'SplitContainerLed.Panel2
        '
        Me.SplitContainerLed.Panel2.Controls.Add(Me.PanelValuePovers)
        Me.SplitContainerLed.Size = New System.Drawing.Size(1119, 220)
        Me.SplitContainerLed.SplitterDistance = 670
        Me.SplitContainerLed.TabIndex = 15
        '
        'PanelDigitalLed
        '
        Me.PanelDigitalLed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelDigitalLed.Location = New System.Drawing.Point(0, 22)
        Me.PanelDigitalLed.Name = "PanelDigitalLed"
        Me.PanelDigitalLed.Size = New System.Drawing.Size(670, 198)
        Me.PanelDigitalLed.TabIndex = 0
        '
        'HeaderStripPanelPorts
        '
        Me.HeaderStripPanelPorts.AutoSize = False
        Me.HeaderStripPanelPorts.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.HeaderStripPanelPorts.ForeColor = System.Drawing.Color.White
        Me.HeaderStripPanelPorts.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.HeaderStripPanelPorts.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSLabelPorts})
        Me.HeaderStripPanelPorts.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table
        Me.HeaderStripPanelPorts.Location = New System.Drawing.Point(0, 0)
        Me.HeaderStripPanelPorts.Name = "HeaderStripPanelPorts"
        Me.HeaderStripPanelPorts.Size = New System.Drawing.Size(670, 22)
        Me.HeaderStripPanelPorts.TabIndex = 13
        Me.HeaderStripPanelPorts.Text = "HeaderStripPanelPorts"
        '
        'TSLabelPorts
        '
        Me.TSLabelPorts.ForeColor = System.Drawing.Color.Maroon
        Me.TSLabelPorts.Name = "TSLabelPorts"
        Me.TSLabelPorts.Size = New System.Drawing.Size(166, 19)
        Me.TSLabelPorts.Text = "Порты управления"
        '
        'PanelValuePovers
        '
        Me.PanelValuePovers.Controls.Add(Me.SplitContainerMessage)
        Me.PanelValuePovers.Controls.Add(Me.HeaderStripPanel2)
        Me.PanelValuePovers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelValuePovers.Location = New System.Drawing.Point(0, 0)
        Me.PanelValuePovers.Name = "PanelValuePovers"
        Me.PanelValuePovers.Size = New System.Drawing.Size(445, 220)
        Me.PanelValuePovers.TabIndex = 1
        '
        'SplitContainerMessage
        '
        Me.SplitContainerMessage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerMessage.Location = New System.Drawing.Point(0, 22)
        Me.SplitContainerMessage.Name = "SplitContainerMessage"
        Me.SplitContainerMessage.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerMessage.Panel1
        '
        Me.SplitContainerMessage.Panel1.Controls.Add(Me.ListViewPowerValue)
        '
        'SplitContainerMessage.Panel2
        '
        Me.SplitContainerMessage.Panel2.Controls.Add(Me.ListViewAlarms)
        Me.SplitContainerMessage.Panel2.Controls.Add(Me.HeaderStripMessage)
        Me.SplitContainerMessage.Size = New System.Drawing.Size(445, 198)
        Me.SplitContainerMessage.SplitterDistance = 76
        Me.SplitContainerMessage.TabIndex = 3
        '
        'ListViewPowerValue
        '
        Me.ListViewPowerValue.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ListViewPowerValue.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListViewPowerValue.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewPowerValue.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold)
        Me.ListViewPowerValue.ForeColor = System.Drawing.Color.Black
        Me.ListViewPowerValue.GridLines = True
        Me.ListViewPowerValue.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.ListViewPowerValue.HideSelection = False
        Me.ListViewPowerValue.Location = New System.Drawing.Point(0, 0)
        Me.ListViewPowerValue.MultiSelect = False
        Me.ListViewPowerValue.Name = "ListViewPowerValue"
        Me.ListViewPowerValue.Size = New System.Drawing.Size(441, 72)
        Me.ListViewPowerValue.TabIndex = 147
        Me.ListViewPowerValue.UseCompatibleStateImageBehavior = False
        Me.ListViewPowerValue.View = System.Windows.Forms.View.Details
        '
        'ListViewAlarms
        '
        Me.ListViewAlarms.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ListViewAlarms.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListViewAlarms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewAlarms.HideSelection = False
        Me.ListViewAlarms.Location = New System.Drawing.Point(0, 25)
        Me.ListViewAlarms.MultiSelect = False
        Me.ListViewAlarms.Name = "ListViewAlarms"
        Me.ListViewAlarms.Size = New System.Drawing.Size(441, 89)
        Me.ListViewAlarms.TabIndex = 24
        Me.ListViewAlarms.UseCompatibleStateImageBehavior = False
        Me.ListViewAlarms.View = System.Windows.Forms.View.SmallIcon
        '
        'HeaderStripMessage
        '
        Me.HeaderStripMessage.AutoSize = False
        Me.HeaderStripMessage.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold)
        Me.HeaderStripMessage.ForeColor = System.Drawing.Color.White
        Me.HeaderStripMessage.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.HeaderStripMessage.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSLabelMessage})
        Me.HeaderStripMessage.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table
        Me.HeaderStripMessage.Location = New System.Drawing.Point(0, 0)
        Me.HeaderStripMessage.Name = "HeaderStripMessage"
        Me.HeaderStripMessage.Size = New System.Drawing.Size(441, 25)
        Me.HeaderStripMessage.TabIndex = 23
        Me.HeaderStripMessage.Text = "HeaderStrip3"
        '
        'TSLabelMessage
        '
        Me.TSLabelMessage.ForeColor = System.Drawing.Color.Maroon
        Me.TSLabelMessage.Name = "TSLabelMessage"
        Me.TSLabelMessage.Size = New System.Drawing.Size(195, 19)
        Me.TSLabelMessage.Text = "Сообщения оператору"
        '
        'HeaderStripPanel2
        '
        Me.HeaderStripPanel2.AutoSize = False
        Me.HeaderStripPanel2.Font = New System.Drawing.Font("Arial", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.HeaderStripPanel2.ForeColor = System.Drawing.Color.White
        Me.HeaderStripPanel2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.HeaderStripPanel2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabelOutput})
        Me.HeaderStripPanel2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table
        Me.HeaderStripPanel2.Location = New System.Drawing.Point(0, 0)
        Me.HeaderStripPanel2.Name = "HeaderStripPanel2"
        Me.HeaderStripPanel2.Size = New System.Drawing.Size(445, 22)
        Me.HeaderStripPanel2.TabIndex = 14
        Me.HeaderStripPanel2.Text = "HeaderStripPanel2"
        '
        'ToolStripLabelOutput
        '
        Me.ToolStripLabelOutput.ForeColor = System.Drawing.Color.Maroon
        Me.ToolStripLabelOutput.Name = "ToolStripLabelOutput"
        Me.ToolStripLabelOutput.Size = New System.Drawing.Size(261, 19)
        Me.ToolStripLabelOutput.Text = "Значения мощностей загрузок"
        '
        'StatusStripForm
        '
        Me.StatusStripForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSSLabelStatus, Me.TSLabelRemain, Me.TSLabelTime, Me.TSProgressBar})
        Me.StatusStripForm.Location = New System.Drawing.Point(0, 704)
        Me.StatusStripForm.Name = "StatusStripForm"
        Me.StatusStripForm.Size = New System.Drawing.Size(1197, 25)
        Me.StatusStripForm.TabIndex = 32
        '
        'TSSLabelStatus
        '
        Me.TSSLabelStatus.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.TSSLabelStatus.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.TSSLabelStatus.Image = CType(resources.GetObject("TSSLabelStatus.Image"), System.Drawing.Image)
        Me.TSSLabelStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TSSLabelStatus.Name = "TSSLabelStatus"
        Me.TSSLabelStatus.Size = New System.Drawing.Size(1182, 20)
        Me.TSSLabelStatus.Spring = True
        Me.TSSLabelStatus.Text = "Готово"
        Me.TSSLabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TSLabelRemain
        '
        Me.TSLabelRemain.Name = "TSLabelRemain"
        Me.TSLabelRemain.Size = New System.Drawing.Size(62, 20)
        Me.TSLabelRemain.Text = "Осталось:"
        Me.TSLabelRemain.Visible = False
        '
        'TSLabelTime
        '
        Me.TSLabelTime.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.TSLabelTime.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.TSLabelTime.Name = "TSLabelTime"
        Me.TSLabelTime.Size = New System.Drawing.Size(23, 20)
        Me.TSLabelTime.Text = "10"
        Me.TSLabelTime.Visible = False
        '
        'TSProgressBar
        '
        Me.TSProgressBar.Name = "TSProgressBar"
        Me.TSProgressBar.Size = New System.Drawing.Size(300, 19)
        Me.TSProgressBar.Visible = False
        '
        'ToolStripOperation
        '
        Me.ToolStripOperation.Dock = System.Windows.Forms.DockStyle.Left
        Me.ToolStripOperation.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStripOperation.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSButtonLanch, Me.TSButtonPause, Me.TSButtonReady, Me.TSButtonAlarm, Me.TSButtonForward, Me.TSButtonBackwards})
        Me.ToolStripOperation.Location = New System.Drawing.Point(0, 24)
        Me.ToolStripOperation.Name = "ToolStripOperation"
        Me.ToolStripOperation.Size = New System.Drawing.Size(74, 680)
        Me.ToolStripOperation.TabIndex = 30
        Me.ToolStripOperation.Text = "toolStrip1"
        '
        'TSButtonLanch
        '
        Me.TSButtonLanch.CheckOnClick = True
        Me.TSButtonLanch.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TSButtonLanch.Image = CType(resources.GetObject("TSButtonLanch.Image"), System.Drawing.Image)
        Me.TSButtonLanch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSButtonLanch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonLanch.Name = "TSButtonLanch"
        Me.TSButtonLanch.Size = New System.Drawing.Size(71, 70)
        Me.TSButtonLanch.Text = "Пуск"
        Me.TSButtonLanch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TSButtonLanch.ToolTipText = "Запуск выбранной циклограммы"
        '
        'TSButtonPause
        '
        Me.TSButtonPause.CheckOnClick = True
        Me.TSButtonPause.Enabled = False
        Me.TSButtonPause.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TSButtonPause.Image = CType(resources.GetObject("TSButtonPause.Image"), System.Drawing.Image)
        Me.TSButtonPause.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonPause.Name = "TSButtonPause"
        Me.TSButtonPause.Size = New System.Drawing.Size(71, 70)
        Me.TSButtonPause.Text = "Пауза"
        Me.TSButtonPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TSButtonPause.ToolTipText = "Пауза"
        '
        'TSButtonReady
        '
        Me.TSButtonReady.Enabled = False
        Me.TSButtonReady.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TSButtonReady.Image = CType(resources.GetObject("TSButtonReady.Image"), System.Drawing.Image)
        Me.TSButtonReady.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSButtonReady.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonReady.Name = "TSButtonReady"
        Me.TSButtonReady.Size = New System.Drawing.Size(71, 70)
        Me.TSButtonReady.Text = "Готово"
        Me.TSButtonReady.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TSButtonReady.ToolTipText = "Подтвердить выполнение по окончанию"
        '
        'TSButtonAlarm
        '
        Me.TSButtonAlarm.Enabled = False
        Me.TSButtonAlarm.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TSButtonAlarm.Image = CType(resources.GetObject("TSButtonAlarm.Image"), System.Drawing.Image)
        Me.TSButtonAlarm.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSButtonAlarm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonAlarm.Name = "TSButtonAlarm"
        Me.TSButtonAlarm.Size = New System.Drawing.Size(71, 70)
        Me.TSButtonAlarm.Text = "Авария"
        Me.TSButtonAlarm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'TSButtonForward
        '
        Me.TSButtonForward.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TSButtonForward.Image = CType(resources.GetObject("TSButtonForward.Image"), System.Drawing.Image)
        Me.TSButtonForward.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSButtonForward.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonForward.Name = "TSButtonForward"
        Me.TSButtonForward.Size = New System.Drawing.Size(71, 54)
        Me.TSButtonForward.Text = "Вперед"
        Me.TSButtonForward.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TSButtonForward.ToolTipText = "Вперед"
        '
        'TSButtonBackwards
        '
        Me.TSButtonBackwards.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TSButtonBackwards.Image = CType(resources.GetObject("TSButtonBackwards.Image"), System.Drawing.Image)
        Me.TSButtonBackwards.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSButtonBackwards.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonBackwards.Name = "TSButtonBackwards"
        Me.TSButtonBackwards.Size = New System.Drawing.Size(71, 54)
        Me.TSButtonBackwards.Text = "Назад"
        Me.TSButtonBackwards.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TSButtonBackwards.ToolTipText = "Назад"
        '
        'MenuStripForm
        '
        Me.MenuStripForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenu, Me.ViewToolStripMenuItem, Me.CheckedListMenu})
        Me.MenuStripForm.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripForm.Name = "MenuStripForm"
        Me.MenuStripForm.ShowItemToolTips = True
        Me.MenuStripForm.Size = New System.Drawing.Size(1197, 24)
        Me.MenuStripForm.TabIndex = 29
        Me.MenuStripForm.Text = "MenuStrip"
        '
        'FileMenu
        '
        Me.FileMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMenuItemLanch, Me.ToolStripSeparator1, Me.TSMenuItemExit})
        Me.FileMenu.MergeIndex = 1
        Me.FileMenu.Name = "FileMenu"
        Me.FileMenu.Size = New System.Drawing.Size(48, 20)
        Me.FileMenu.Text = "&Окно"
        Me.FileMenu.ToolTipText = "Программа"
        '
        'TSMenuItemLanch
        '
        Me.TSMenuItemLanch.CheckOnClick = True
        Me.TSMenuItemLanch.Image = CType(resources.GetObject("TSMenuItemLanch.Image"), System.Drawing.Image)
        Me.TSMenuItemLanch.Name = "TSMenuItemLanch"
        Me.TSMenuItemLanch.Size = New System.Drawing.Size(148, 22)
        Me.TSMenuItemLanch.Text = "Запуск &цикла"
        Me.TSMenuItemLanch.ToolTipText = "Запуск цикла загрузок"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(145, 6)
        '
        'TSMenuItemExit
        '
        Me.TSMenuItemExit.Image = CType(resources.GetObject("TSMenuItemExit.Image"), System.Drawing.Image)
        Me.TSMenuItemExit.Name = "TSMenuItemExit"
        Me.TSMenuItemExit.Size = New System.Drawing.Size(148, 22)
        Me.TSMenuItemExit.Text = "&Закрыть"
        Me.TSMenuItemExit.ToolTipText = "Закрыть окно"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMenuItemShowTreeView, Me.TSMenuItemShowPahelLeds, Me.TSMenuItemShowPowerMeterControls, Me.TSpMenuItemShowStatusStrip, Me.TSMenuItemShowNavigationPanel})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(69, 20)
        Me.ViewToolStripMenuItem.Text = "&Показать"
        Me.ViewToolStripMenuItem.ToolTipText = "Показать панели"
        '
        'TSMenuItemShowTreeView
        '
        Me.TSMenuItemShowTreeView.Checked = True
        Me.TSMenuItemShowTreeView.CheckOnClick = True
        Me.TSMenuItemShowTreeView.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TSMenuItemShowTreeView.Name = "TSMenuItemShowTreeView"
        Me.TSMenuItemShowTreeView.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.TSMenuItemShowTreeView.Size = New System.Drawing.Size(273, 22)
        Me.TSMenuItemShowTreeView.Text = "Панель выбора циклограмм"
        Me.TSMenuItemShowTreeView.ToolTipText = "<< Скрыть дополнительную панель {Выбор циклограмм}"
        '
        'TSMenuItemShowPahelLeds
        '
        Me.TSMenuItemShowPahelLeds.Checked = True
        Me.TSMenuItemShowPahelLeds.CheckOnClick = True
        Me.TSMenuItemShowPahelLeds.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TSMenuItemShowPahelLeds.Name = "TSMenuItemShowPahelLeds"
        Me.TSMenuItemShowPahelLeds.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.TSMenuItemShowPahelLeds.Size = New System.Drawing.Size(273, 22)
        Me.TSMenuItemShowPahelLeds.Text = "Панель порты управления"
        Me.TSMenuItemShowPahelLeds.ToolTipText = "\/ Скрыть дополнительную панель {Порты управления}"
        '
        'TSMenuItemShowPowerMeterControls
        '
        Me.TSMenuItemShowPowerMeterControls.Checked = True
        Me.TSMenuItemShowPowerMeterControls.CheckOnClick = True
        Me.TSMenuItemShowPowerMeterControls.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TSMenuItemShowPowerMeterControls.Name = "TSMenuItemShowPowerMeterControls"
        Me.TSMenuItemShowPowerMeterControls.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.TSMenuItemShowPowerMeterControls.Size = New System.Drawing.Size(273, 22)
        Me.TSMenuItemShowPowerMeterControls.Text = "Панель индикаторов"
        Me.TSMenuItemShowPowerMeterControls.ToolTipText = ">> Скрыть дополнительную панель {Индикаторы}"
        '
        'TSpMenuItemShowStatusStrip
        '
        Me.TSpMenuItemShowStatusStrip.Checked = True
        Me.TSpMenuItemShowStatusStrip.CheckOnClick = True
        Me.TSpMenuItemShowStatusStrip.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TSpMenuItemShowStatusStrip.Name = "TSpMenuItemShowStatusStrip"
        Me.TSpMenuItemShowStatusStrip.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.TSpMenuItemShowStatusStrip.Size = New System.Drawing.Size(273, 22)
        Me.TSpMenuItemShowStatusStrip.Text = "Панель статуса"
        Me.TSpMenuItemShowStatusStrip.ToolTipText = "Видимость панели статуса"
        '
        'TSMenuItemShowNavigationPanel
        '
        Me.TSMenuItemShowNavigationPanel.Image = Global.CycleCharge.My.Resources.Resources.Help
        Me.TSMenuItemShowNavigationPanel.Name = "TSMenuItemShowNavigationPanel"
        Me.TSMenuItemShowNavigationPanel.ShortcutKeyDisplayString = "Alt+F1"
        Me.TSMenuItemShowNavigationPanel.Size = New System.Drawing.Size(273, 22)
        Me.TSMenuItemShowNavigationPanel.Text = "&Панель подсказки"
        Me.TSMenuItemShowNavigationPanel.ToolTipText = "Показать панель справки управления графиком"
        '
        'CheckedListMenu
        '
        Me.CheckedListMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddOptionMenuItem, Me.RemoveOptionMenuItem, Me.ToolStripBar})
        Me.CheckedListMenu.Name = "CheckedListMenu"
        Me.CheckedListMenu.Size = New System.Drawing.Size(86, 20)
        Me.CheckedListMenu.Text = "&Checked List"
        Me.CheckedListMenu.ToolTipText = "Отметить"
        Me.CheckedListMenu.Visible = False
        '
        'AddOptionMenuItem
        '
        Me.AddOptionMenuItem.Name = "AddOptionMenuItem"
        Me.AddOptionMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.AddOptionMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.AddOptionMenuItem.Text = "Add Option"
        Me.AddOptionMenuItem.ToolTipText = "Add Option"
        '
        'RemoveOptionMenuItem
        '
        Me.RemoveOptionMenuItem.Name = "RemoveOptionMenuItem"
        Me.RemoveOptionMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.RemoveOptionMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.RemoveOptionMenuItem.Text = "Remove Option"
        Me.RemoveOptionMenuItem.ToolTipText = "Remove Option"
        '
        'ToolStripBar
        '
        Me.ToolStripBar.Name = "ToolStripBar"
        Me.ToolStripBar.Size = New System.Drawing.Size(195, 6)
        '
        'ToolStripVert
        '
        Me.ToolStripVert.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStripVert.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.ToolStripVert.Location = New System.Drawing.Point(301, 0)
        Me.ToolStripVert.Name = "ToolStripVert"
        Me.ToolStripVert.Size = New System.Drawing.Size(111, 25)
        Me.ToolStripVert.TabIndex = 33
        Me.ToolStripVert.Visible = False
        '
        'TimerCycle
        '
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "KeyКонтрТочка"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "Start"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "ИмяКонтрольнойТочки"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "Описание"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "КТВыполнена"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'ToolStripDevice
        '
        Me.ToolStripDevice.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSButtonShowTreeView, Me.TButtonShowPahelLeds, Me.TSButtonShowPowerMeterControls, Me.ToolStripSeparator2, Me.TSLabelCurrentTime, Me.TSTextBoxCurrentTime, Me.ToolStripSeparator3, Me.TextError})
        Me.ToolStripDevice.Location = New System.Drawing.Point(74, 24)
        Me.ToolStripDevice.Name = "ToolStripDevice"
        Me.ToolStripDevice.Size = New System.Drawing.Size(1123, 25)
        Me.ToolStripDevice.TabIndex = 155
        '
        'TSButtonShowTreeView
        '
        Me.TSButtonShowTreeView.CheckOnClick = True
        Me.TSButtonShowTreeView.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TSButtonShowTreeView.Image = CType(resources.GetObject("TSButtonShowTreeView.Image"), System.Drawing.Image)
        Me.TSButtonShowTreeView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSButtonShowTreeView.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonShowTreeView.Name = "TSButtonShowTreeView"
        Me.TSButtonShowTreeView.Size = New System.Drawing.Size(91, 22)
        Me.TSButtonShowTreeView.Text = "Скрыть"
        Me.TSButtonShowTreeView.ToolTipText = "<< Скрыть дополнительную панель {Выбор циклограмм}"
        '
        'TButtonShowPahelLeds
        '
        Me.TButtonShowPahelLeds.CheckOnClick = True
        Me.TButtonShowPahelLeds.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TButtonShowPahelLeds.Image = CType(resources.GetObject("TButtonShowPahelLeds.Image"), System.Drawing.Image)
        Me.TButtonShowPahelLeds.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TButtonShowPahelLeds.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TButtonShowPahelLeds.Name = "TButtonShowPahelLeds"
        Me.TButtonShowPahelLeds.Size = New System.Drawing.Size(91, 22)
        Me.TButtonShowPahelLeds.Text = "Скрыть"
        Me.TButtonShowPahelLeds.ToolTipText = "\/ Скрыть дополнительную панель {Порты управления}"
        '
        'TSButtonShowPowerMeterControls
        '
        Me.TSButtonShowPowerMeterControls.CheckOnClick = True
        Me.TSButtonShowPowerMeterControls.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TSButtonShowPowerMeterControls.Image = Global.CycleCharge.My.Resources.Resources.HidePowers
        Me.TSButtonShowPowerMeterControls.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSButtonShowPowerMeterControls.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSButtonShowPowerMeterControls.Name = "TSButtonShowPowerMeterControls"
        Me.TSButtonShowPowerMeterControls.Size = New System.Drawing.Size(91, 22)
        Me.TSButtonShowPowerMeterControls.Text = "Скрыть"
        Me.TSButtonShowPowerMeterControls.ToolTipText = ">> Скрыть дополнительную панель {Индикаторы}"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'TSLabelCurrentTime
        '
        Me.TSLabelCurrentTime.Name = "TSLabelCurrentTime"
        Me.TSLabelCurrentTime.Size = New System.Drawing.Size(57, 22)
        Me.TSLabelCurrentTime.Text = "Текущее:"
        '
        'TSTextBoxCurrentTime
        '
        Me.TSTextBoxCurrentTime.BackColor = System.Drawing.Color.Khaki
        Me.TSTextBoxCurrentTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TSTextBoxCurrentTime.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TSTextBoxCurrentTime.ForeColor = System.Drawing.Color.Maroon
        Me.TSTextBoxCurrentTime.Name = "TSTextBoxCurrentTime"
        Me.TSTextBoxCurrentTime.ReadOnly = True
        Me.TSTextBoxCurrentTime.Size = New System.Drawing.Size(160, 25)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'TextError
        '
        Me.TextError.BackColor = System.Drawing.Color.Yellow
        Me.TextError.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.TextError.ForeColor = System.Drawing.Color.Red
        Me.TextError.Name = "TextError"
        Me.TextError.ReadOnly = True
        Me.TextError.Size = New System.Drawing.Size(400, 26)
        Me.TextError.Text = "Ошибка"
        Me.TextError.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TextError.ToolTipText = "Ошибка извлечения измеренных параметров"
        Me.TextError.Visible = False
        '
        'TimerResize
        '
        Me.TimerResize.Interval = 2000
        '
        'ImageListMessage
        '
        Me.ImageListMessage.ImageStream = CType(resources.GetObject("ImageListMessage.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListMessage.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListMessage.Images.SetKeyName(0, "Information0")
        Me.ImageListMessage.Images.SetKeyName(1, "Information1")
        Me.ImageListMessage.Images.SetKeyName(2, "Condition2")
        Me.ImageListMessage.Images.SetKeyName(3, "Ok3")
        Me.ImageListMessage.Images.SetKeyName(4, "PowerDown4")
        Me.ImageListMessage.Images.SetKeyName(5, "PowerUp5")
        Me.ImageListMessage.Images.SetKeyName(6, "Criterion6")
        Me.ImageListMessage.Images.SetKeyName(7, "AlarmMessage7")
        Me.ImageListMessage.Images.SetKeyName(8, "Номер")
        Me.ImageListMessage.Images.SetKeyName(9, "Сообщение")
        Me.ImageListMessage.Images.SetKeyName(10, "Дата")
        Me.ImageListMessage.Images.SetKeyName(11, "Время")
        '
        'FormPlayerCyclogram
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1197, 729)
        Me.Controls.Add(Me.SplitContainerAll)
        Me.Controls.Add(Me.ToolStripDevice)
        Me.Controls.Add(Me.ToolStripVert)
        Me.Controls.Add(Me.ToolStripOperation)
        Me.Controls.Add(Me.StatusStripForm)
        Me.Controls.Add(Me.MenuStripForm)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "FormPlayerCyclogram"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "Проигрыватель циклограмм"
        Me.Text = "Проигрыватель циклограмм"
        Me.SplitContainerAll.Panel1.ResumeLayout(False)
        Me.SplitContainerAll.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerAll.ResumeLayout(False)
        Me.SplitContainerCycle.Panel1.ResumeLayout(False)
        Me.SplitContainerCycle.Panel1.PerformLayout()
        Me.SplitContainerCycle.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerCycle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerCycle.ResumeLayout(False)
        Me.ToolStripSelectCycle.ResumeLayout(False)
        Me.ToolStripSelectCycle.PerformLayout()
        Me.HeaderStripSelectCycle.ResumeLayout(False)
        Me.HeaderStripSelectCycle.PerformLayout()
        Me.SplitContainerGraph.Panel1.ResumeLayout(False)
        Me.SplitContainerGraph.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerGraph, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerGraph.ResumeLayout(False)
        CType(Me.ScatterGraphCycle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XyCursor1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerLed.Panel1.ResumeLayout(False)
        Me.SplitContainerLed.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerLed, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerLed.ResumeLayout(False)
        Me.HeaderStripPanelPorts.ResumeLayout(False)
        Me.HeaderStripPanelPorts.PerformLayout()
        Me.PanelValuePovers.ResumeLayout(False)
        Me.SplitContainerMessage.Panel1.ResumeLayout(False)
        Me.SplitContainerMessage.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerMessage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerMessage.ResumeLayout(False)
        Me.HeaderStripMessage.ResumeLayout(False)
        Me.HeaderStripMessage.PerformLayout()
        Me.HeaderStripPanel2.ResumeLayout(False)
        Me.HeaderStripPanel2.PerformLayout()
        Me.StatusStripForm.ResumeLayout(False)
        Me.StatusStripForm.PerformLayout()
        Me.ToolStripOperation.ResumeLayout(False)
        Me.ToolStripOperation.PerformLayout()
        Me.MenuStripForm.ResumeLayout(False)
        Me.MenuStripForm.PerformLayout()
        Me.ToolStripDevice.ResumeLayout(False)
        Me.ToolStripDevice.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainerAll As System.Windows.Forms.SplitContainer
    Friend WithEvents MenuStripForm As System.Windows.Forms.MenuStrip
    Friend WithEvents FileMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSMenuItemExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSpMenuItemShowStatusStrip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStripForm As System.Windows.Forms.StatusStrip
    Friend WithEvents TSSLabelStatus As System.Windows.Forms.ToolStripStatusLabel
    Private WithEvents ToolStripOperation As System.Windows.Forms.ToolStrip
    Private WithEvents TSButtonPause As System.Windows.Forms.ToolStripButton
    Private WithEvents TSButtonAlarm As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTipForm As System.Windows.Forms.ToolTip
    Friend WithEvents ScatterPlot1 As NationalInstruments.UI.ScatterPlot
    Friend WithEvents ScatterGraphCycle As NationalInstruments.UI.WindowsForms.ScatterGraph
    Friend WithEvents XyCursor1 As NationalInstruments.UI.XYCursor
    Friend WithEvents ScatterPlot2 As NationalInstruments.UI.ScatterPlot
    Friend WithEvents XAxis1 As NationalInstruments.UI.XAxis
    Friend WithEvents YAxis1 As NationalInstruments.UI.YAxis
    Friend WithEvents ListViewPowerValue As CycleCharge.DbListView
    Friend WithEvents SplitContainerMessage As System.Windows.Forms.SplitContainer
    Friend WithEvents CheckedListMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddOptionMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveOptionMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripBar As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripVert As System.Windows.Forms.ToolStrip
    Friend WithEvents TreeViewSycleProgram As System.Windows.Forms.TreeView
    Friend WithEvents ImageListNodes As System.Windows.Forms.ImageList
    Friend WithEvents ToolStripSelectCycle As System.Windows.Forms.ToolStrip
    Friend WithEvents TSLabelSelectedCycle As System.Windows.Forms.ToolStripLabel
    Friend WithEvents TimerCycle As System.Windows.Forms.Timer
    Friend WithEvents TSLabelSycle As System.Windows.Forms.ToolStripLabel
    Friend WithEvents SplitContainerCycle As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainerLed As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelDigitalLed As System.Windows.Forms.Panel
    Private WithEvents HeaderStripPanelPorts As CycleCharge.HeaderStrip
    Friend WithEvents TSLabelPorts As System.Windows.Forms.ToolStripLabel
    Friend WithEvents PanelValuePovers As System.Windows.Forms.Panel
    Private WithEvents HeaderStripPanel2 As CycleCharge.HeaderStrip
    Friend WithEvents ToolStripLabelOutput As System.Windows.Forms.ToolStripLabel
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TSLabelRemain As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TSLabelTime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TSProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents TSMenuItemShowNavigationPanel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripDevice As Windows.Forms.ToolStrip
    Friend WithEvents TimerResize As Windows.Forms.Timer
    Private WithEvents HeaderStripSelectCycle As HeaderStrip
    Friend WithEvents TSLabelSelectCycle As Windows.Forms.ToolStripLabel
    Private WithEvents HeaderStripMessage As HeaderStrip
    Friend WithEvents TSLabelMessage As Windows.Forms.ToolStripLabel
    Private WithEvents TSButtonLanch As Windows.Forms.ToolStripButton
    Private WithEvents TSButtonReady As Windows.Forms.ToolStripButton
    Private WithEvents TSButtonForward As Windows.Forms.ToolStripButton
    Private WithEvents TSButtonBackwards As Windows.Forms.ToolStripButton
    Friend WithEvents TSMenuItemLanch As Windows.Forms.ToolStripMenuItem
    Private WithEvents TSButtonShowTreeView As Windows.Forms.ToolStripButton
    Private WithEvents TButtonShowPahelLeds As Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As Windows.Forms.ToolStripSeparator
    Friend WithEvents TSMenuItemShowTreeView As Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMenuItemShowPahelLeds As Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextError As Windows.Forms.ToolStripTextBox
    Friend WithEvents TSLabelCurrentTime As Windows.Forms.ToolStripLabel
    Friend WithEvents TSTextBoxCurrentTime As Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripSeparator3 As Windows.Forms.ToolStripSeparator
    Friend WithEvents ListViewAlarms As DbListView
    Friend WithEvents ImageListMessage As Windows.Forms.ImageList
    Friend WithEvents FlowLayoutPanelControls As Windows.Forms.FlowLayoutPanel
    Private WithEvents TSButtonShowPowerMeterControls As Windows.Forms.ToolStripButton
    Friend WithEvents TSMenuItemShowPowerMeterControls As Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainerGraph As Windows.Forms.SplitContainer
End Class
