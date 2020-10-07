Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.Drawing
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Security.Permissions
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports BaseForm
Imports CycleCharge.FormEditorCyclogram
Imports NationalInstruments.Analysis.Math
Imports NationalInstruments.UI
Imports NationalInstruments.UI.WindowsForms

''' <summary>
''' Проигрыватель циклограмм исполнительных устройств для загрузки двигателя в автоматическом режиме.
''' </summary>
Public Class FormPlayerCyclogram
    Implements IMdiChildrenWindow

    Private ReadOnly Property MainFomMdiParent As FrmMain Implements IMdiChildrenWindow.MainFomMdiParent

    ''' <summary>
    ''' Циклограмма Запущена
    ''' </summary>
    ''' <returns></returns>
    Public Property IsLanchCycleProgram As Boolean

    Enum LedColorType
        <Description("Default")>
        TypeDefault = 1
        <Description("BackColor")>
        TypeOnColor = 2
        <Description("ForeColor")>
        TypeOffColor = 3
        <Description("Неизв.")>
        Unknown
    End Enum

#Region "Const"
    Private Const HIDE_PANEL_LED As String = "\/ Скрыть дополнительную панель {Порты управления}"
    Private Const SHOW_PANEL_LED As String = "^^ Показать дополнительную панель {Порты управления}"
    Private Const HIDE_PANEL_TREECYCLE As String = "<< Скрыть дополнительную панель {Выбор циклограмм}"
    Private Const SHOW_PANEL_LED_TREECYCLE As String = ">> Показать дополнительную панель {Выбор циклограмм}"
    Private Const HIDE_PANEL_POWERS As String = ">> Скрыть дополнительную панель {Индикаторы}"
    Private Const SHOW_PANEL_POWERS As String = "<< Показать дополнительную панель {Индикаторы}"
    Private Const HIDE_TEXT As String = "Скрыть"
    Private Const SHOW_TEXT As String = "Показать"

    Private Const OptionsXml As String = "Options.xml"
    Private Const Separator As String = "\"

    Private Const TSSplitButtonOnColor As String = "TSSplitButtonOnColor"
    Private Const TSSplitButtonOffColor As String = "TSSplitButtonOffColor"
    Private Const ColorRed As String = "Red"
    Private Const ColorYellow As String = "Yellow"
    Private Const ColorGreen As String = "Green"
    Private ColorsNet As Color() = {Color.Blue, Color.Red, Color.DarkGreen, Color.DarkOrange, Color.DarkCyan, Color.Magenta, Color.Maroon, Color.Black}
    Private ledOnColor As String = ColorRed
    Private ledOffColor As String = ColorYellow
#End Region

#Region "контроллы"
    Private WithEvents TableLayoutPanelAllPorts As TableLayoutPanel
    Private UserNumericCycle As ModuleControlNumericCycle
    Private TSComboBoxEngines As ToolStripComboBox
    Private WithEvents TempPointAnnotation As XYPointAnnotation
    Private curToolStrip As ToolStrip
    Private curMenuItem As ToolStripMenuItem
    Private ReadOnly subMenu(0) As ToolStripMenuItem
    Private ReadOnly subMenuUserNumericCycle(1) As ToolStripMenuItem
    'Private utilityHelper As UtilityHelper
    'Private WithEvents mmTimer As Multimedia.Timer
    'Private timerInterval As Integer = 100 ' миллисекунд
#End Region

#Region "для запоминания настроек"
    Private pathOptionsXml As String
    Private selectedTypeEngine, selectedTestProgram As String
    ''' <summary>
    ''' keyТипИзделия
    ''' </summary>
    Private keyTypeEngine As Integer
    ''' <summary>
    ''' keyПрограммаИспытаний
    ''' </summary>
    Private keyTestProgram As Integer
    ''' <summary>
    ''' Для запоминания KeyЦиклЗагрузки из текущей строки из таблицы ЦиклЗагрузки3 перед сменой позиции для вопроса сохранения незаписанной перекладки
    ''' </summary>
    Private keyCycleStage As Integer
    ''' <summary>
    ''' keyПерекладкиЦикла
    ''' </summary>
    Private keyStepCycleStage As Integer
#End Region

    Private isVisibleUserNumericCycle As Boolean = False
    Private isCheckCyclogramCorrect As Boolean
    Private isBackward, isForward As Boolean

    Private currentTime As Date = Date.Now
    Private previousTime As Date
    Private previousTimeAsDouble As Double
    ''' <summary>
    ''' время между измерениями
    ''' </summary>
    Private spendTime As Double
    ''' <summary>
    ''' суммарное время всех перекладкок
    ''' </summary>
    Private maxSumTimeCycle As Double
    ''' <summary>
    ''' счетчик Циклов
    ''' </summary>
    Private counterCycles As Integer
    ''' <summary>
    ''' самое большое время выполнение из всех циклограмм в программе
    ''' </summary>
    Private requestedCount As Integer
    ''' <summary>
    ''' время Промежуточное
    ''' </summary>
    Private currentTimeCycle As Double
    ''' <summary>
    ''' количество Повторов
    ''' </summary>
    Private repeatCounter As Integer

    ''' <summary>
    ''' частота сбора регистратора
    ''' </summary>
    Private frequencyRegistration As Integer = 20
    Private delayPeriod As Single
    Private messageMemo As String

    Private mPortWriters As PortWriters
    Private mAggregates As Aggregates
    Private LedPorts As Dictionary(Of String, LedPort)
    Private ControlsSize As New Dictionary(Of Control, Size)

    ' в событии курсора определить время
    ' в цикле по  (циклограммам) по устройствам определить для каждого устройства величину
    ' в цикле по устройствам исполнить эту величину и отобразить на индикаторах
    ' сделать все проверки при выборе новой программы
    Public Sub New(inMainFomMdiParent As FrmMain)
        MyBase.New()

        InitializeComponent()
        MainFomMdiParent = inMainFomMdiParent

        subMenu(0) = New ToolStripMenuItem("Панель цикла загрузок...", Nothing)
        subMenuUserNumericCycle(0) = New ToolStripMenuItem("Добавить", Nothing, New EventHandler(AddressOf UserNumericCycleHandler))
        subMenuUserNumericCycle(1) = New ToolStripMenuItem("Удалить", Nothing, New EventHandler(AddressOf UserNumericCycleHandler)) With {.Enabled = False}
        subMenu(0).DropDownItems.AddRange(subMenuUserNumericCycle)

        ' добавить панель отслеживания количества циклов
        AddRemoveToolStripUserNumericCycle(True)
        ' включить связывание пунктов меню с текстами подсказок из файла ресурсов
        InitializeMenuHelperStrings(MenuStripForm)
        ' InitializeToolTips(toolStrip1)
        ' MapToolBarAndMenuItems()
        AddComboBoxEngines()
        InitializeListViewPowerValue()
        PopulateInputParameterToToolStrip()
    End Sub

#Region "FormPlayerCyclogram Event Handlers"
    Private Sub FormPlayerCyclogram_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        StatusStripForm.Height = 25
        TSProgressBar.Height = 19 ' В дизайнере сбрасывает на другую величину
        ' при настройке в среде разработки почему-то глючит, пришлось вынести в код
        YAxis1.Visible = False
        ScatterGraphCycle.Border = Border.ThinFrame3D

        'mmTimer = New Multimedia.Timer() With {.Mode = Multimedia.TimerMode.Periodic,
        '                                       .Period = timerInterval,
        '                                       .Resolution = 1}
        '' для отслеживания события в форме назначить объект синхронизации (должен быть компонент)
        '' если таймер работает самостоятельно, то форму назначать не надо
        'mmTimer.SynchronizingObject = Me ' MainFomMdiParent ' необходимо для отслеживания вызова событий

        CreateListViewAlarms()
        pathOptionsXml = Path.Combine(MainFomMdiParent.PathResourses, OptionsXml)
        If Not CheckOptionsXmlFile() Then Exit Sub

        LoadOptions()
        InitializeDigitalTableLayoutPanelPort()
        mPortWriters = New PortWriters()
        FillComboBoxEngines()
        AddColorSplitButtons()
        FlowLayoutPanelControlsResize()
        frequencyRegistration = Convert.ToInt32(MainFomMdiParent.MyClassCalculation.TuningParams.Frequency.DigitalValue)
        delayPeriod = CSng(1.0 / frequencyRegistration)
        ShowMessageOnPanel($"Стенд №: {MainFomMdiParent.MyClassCalculation.TuningParams.StandNumber.DigitalValue} готов к работе...")
        TimerResize.Enabled = True ' включить таймер для определения размеров панели индикаторов
    End Sub

    Private Sub PlayerCyclogramForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'If запущеноФоновоеВыполнениеЦикла AndAlso mCancellationTokenSource IsNot Nothing Then
        '    ' прекратить запущенные задачи
        '    mCancellationTokenSource.Cancel()
        '    Dim message As String = "Фоновая задача выполнения циклограммы прервана при попытки закрытия окна <Проигрыватель Циклограмм>."
        '    ShowMessageOnPanel(message)
        '    AddLogMessage(message, ColorForAlarm.AlarmMessage7)
        '    message += Environment.NewLine & "Произведите повторное закрытие окна проигрывателя циклограмм."
        '    MessageBox.Show(message, "Прерывание фоновых задач выполнения", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    AlarmStopCycle()
        '    e.Cancel = True
        'End If

        TimerCycle.Enabled = False
        UnBindingCalculatedParamsWithControls()
        MainFomMdiParent.MyClassCalculation.InputParams.UnBindingAllControls()
        'mmTimer.Stop()
        'mmTimer = Nothing

        ' обнулить перед выгрузкой
        If isCheckCyclogramCorrect AndAlso maxSumTimeCycle <> 0 Then
            WritePowersInPorts(True) ' нулевые загрузки
        End If

        Try
            mPortWriters.StopTasksDigitalOutput()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, $"Процедура <{NameOf(PlayerCyclogramForm_FormClosing)}>", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub PlayerCyclogramForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        SaveOptions()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMenuItemExit.Click
        Close()
    End Sub

    ''' <summary>
    ''' Проверка наличия файлов конфигурации
    ''' </summary>
    Private Function CheckOptionsXmlFile() As Boolean
        Dim success As Boolean = True

        If FileNotExists(pathOptionsXml) Then
            MessageBox.Show($"В каталоге нет файла {pathOptionsXml}!", NameOf(CheckOptionsXmlFile), MessageBoxButtons.OK, MessageBoxIcon.Error)
            success = False
        End If

        Return success
    End Function

#Region "Initialize DigitalTableLayoutPanelPort"
    ''' <summary>
    ''' Инициализация TableLayoutPanel по текущему составу оборудования на компьютере.
    ''' </summary>
    Private Sub InitializeDigitalTableLayoutPanelPort()
        Dim I As Integer
        Dim mSystemDevices As New SystemDevices

        If mSystemDevices.PhysicalPorts.Count = 0 Then Exit Sub

        TableLayoutPanelAllPorts = New TableLayoutPanel
        LedPorts = New Dictionary(Of String, LedPort)

        For I = 0 To mSystemDevices.PhysicalPorts.Count - 1
            LedPorts.Add(mSystemDevices.PhysicalPorts(I).ToString, New LedPort(mSystemDevices.PhysicalPorts(I).ToString, mSystemDevices.LinesCount(I) - 1, ColorOnFromName(ledOnColor)))
        Next

        TableLayoutPanelAllPorts.SuspendLayout()
        PanelDigitalLed.SuspendLayout()
        SuspendLayout()
        TableLayoutPanelAllPorts.CellBorderStyle = TableLayoutPanelCellBorderStyle.[Single]
        'TableLayoutPanelAllPorts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        TableLayoutPanelAllPorts.ColumnCount = 1
        TableLayoutPanelAllPorts.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0!))

        I = 0
        For Each itemPort As LedPort In LedPorts.Values
            'TableLayoutPanelAllPorts.Controls.Add(TableLayoutPanelPort0, column, row)
            TableLayoutPanelAllPorts.Controls.Add(itemPort.TableLayoutPanel, 0, I)
            I += 1
        Next

        TableLayoutPanelAllPorts.Location = New Point(0, 0)
        TableLayoutPanelAllPorts.GrowStyle = TableLayoutPanelGrowStyle.AddRows '.FixedSize
        TableLayoutPanelAllPorts.Margin = New Padding(0)
        TableLayoutPanelAllPorts.Name = "TableLayoutPanelPorts"
        TableLayoutPanelAllPorts.RowCount = LedPorts.Count

        For I = 0 To TableLayoutPanelAllPorts.RowCount - 1
            TableLayoutPanelAllPorts.RowStyles.Add(New RowStyle(SizeType.Percent, CSng(100 / (TableLayoutPanelAllPorts.RowCount - 1))))
        Next

        TableLayoutPanelAllPorts.GrowStyle = TableLayoutPanelGrowStyle.FixedSize
        PanelDigitalLed.Controls.Add(TableLayoutPanelAllPorts)

        TableLayoutPanelAllPorts.BringToFront()
        TableLayoutPanelAllPorts.ResumeLayout(False)
        PanelDigitalLed.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    ''' <summary>
    ''' Определить высоту
    ''' </summary>
    Private Sub DefineSizetableLayoutPanelPort()
        Const HeightOfPort As Integer = 48
        If LedPorts Is Nothing Then Exit Sub

        ' если портов мало, то полное заполнение, иначе размер потра 48 пикселей
        If LedPorts.Count * HeightOfPort < PanelDigitalLed.Size.Height Then
            PanelDigitalLed.AutoScroll = False
            TableLayoutPanelAllPorts.Dock = DockStyle.Fill
        Else
            TableLayoutPanelAllPorts.Size = New Size(PanelDigitalLed.Size.Width, LedPorts.Count * HeightOfPort)
            TableLayoutPanelAllPorts.Anchor = CType(((AnchorStyles.Top Or AnchorStyles.Left) Or AnchorStyles.Right), AnchorStyles)
            PanelDigitalLed.AutoScroll = True
        End If
    End Sub

    Private Sub TimerResize_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles TimerResize.Tick
        TimerResize.Enabled = False
        DefineSizetableLayoutPanelPort()
    End Sub

    Private Sub SplitContainerLed_Resize(sender As Object, e As EventArgs) Handles SplitContainerLed.Resize
        If IsHandleCreated Then
            If SplitContainerLed.Width > 1152 Then
                SplitContainerLed.FixedPanel = FixedPanel.Panel1
                SplitContainerLed.SplitterDistance = 1024
            Else
                SplitContainerLed.FixedPanel = FixedPanel.None
            End If
        End If
    End Sub
#End Region

#Region "Показ справочной информации в статусной строке "
    Private Sub MenuItem_MouseEnter(ByVal sender As Object, ByVal e As EventArgs)
        Dim selected As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        ShowMessageOnPanel(selected.Text)
    End Sub

    Private Sub MenuItem_MouseLeave(ByVal sender As Object, ByVal e As EventArgs)
        ShowMessageOnPanel("")
    End Sub
#End Region

#Region "Options"
    ''' <summary>
    ''' Записать в Опции цвет индикаторов.
    ''' </summary>
    Private Sub SaveOptions()
        Dim partition As String = "Engine"
        Dim cReadWriteIni As New ReadWriteIni(pathOptionsXml)
        Dim xmlDoc As XElement = XElement.Load(pathOptionsXml)

        With cReadWriteIni
            .writeINI(xmlDoc, partition, "Color", "OnColor", ledOnColor)
            .writeINI(xmlDoc, partition, "Color", "OffColor", ledOffColor)
        End With
    End Sub

    ''' <summary>
    ''' Последний загруженный тип изделия и цвета индикаторов.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadOptions()
        Dim partition As String = "Engine"
        Dim cReadWriteIni As New ReadWriteIni(pathOptionsXml)
        Dim xmlDoc As XElement = XElement.Load(pathOptionsXml)

        With cReadWriteIni
            selectedTypeEngine = .GetIni(xmlDoc, partition, "Common", "TypeEngine", "1")
            ledOnColor = .GetIni(xmlDoc, partition, "Color", "OnColor", ColorRed)
            ledOffColor = .GetIni(xmlDoc, partition, "Color", "OffColor", ColorYellow)
        End With
    End Sub

    ''' <summary>
    ''' Восстановить имя последней загруженной программы испытания.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RestoreNameTestProgram()
        Dim cReadWriteIni As New ReadWriteIni(pathOptionsXml)
        Dim xmlDoc As XElement = XElement.Load(pathOptionsXml)

        selectedTestProgram = cReadWriteIni.GetIni(xmlDoc, "Engine", "Common", "TestProgram", "1")
    End Sub

    ''' <summary>
    ''' Записать Выбранную Программу Испытаний
    ''' При выборе в дереве узла произвести его запись в конфигурацию для последующего восстановления.
    ''' </summary>
    ''' <param name="inSelectedTestProgram"></param>
    ''' <remarks></remarks>
    Private Sub SaveOptionsTypeEngine(inSelectedTestProgram As String)
        ' записать в Опции выбранный файл
        Dim cReadWriteIni As New ReadWriteIni(pathOptionsXml)
        Dim xmlDoc As XElement = XElement.Load(pathOptionsXml)

        cReadWriteIni.writeINI(xmlDoc, "Engine", "Common", "TestProgram", inSelectedTestProgram)
        selectedTestProgram = inSelectedTestProgram
    End Sub
#End Region

    ''' <summary>
    ''' Заполнить лист новыми именами агрегатов.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PopulateListViewPowerValue()
        Dim I As Integer = 0

        ListViewPowerValue.BeginUpdate()
        ListViewPowerValue.Items.Clear()

        For Each itemAggregate As AggregateCycle In mAggregates
            Dim itmX As New ListViewItem(itemAggregate.NameAggregate) With {.ForeColor = ColorsNet(I Mod 7)}
            itmX.SubItems.Add(CStr(0), Color.White, Color.Black, New Font("Microsoft Sans Serif", 12, FontStyle.Bold))
            ListViewPowerValue.Items.Add(itmX)
            I += 1
        Next

        ListViewPowerValue.EndUpdate()
    End Sub

    Private Sub ListViewPowerValue_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles ListViewPowerValue.Resize
        If IsHandleCreated Then
            For Each itemColumnHeader As ColumnHeader In ListViewPowerValue.Columns
                itemColumnHeader.Width = ListViewPowerValue.Width \ ListViewPowerValue.Columns.Count - 2
            Next
        End If
    End Sub

    ''' <summary>
    ''' Инициализация ListView текущих значений исполняемых мощностей. 
    ''' </summary>
    Private Sub InitializeListViewPowerValue()
        Dim columnHeaderParameter As ColumnHeader = New ColumnHeader("cycle") With {.Text = "Параметр"}
        Dim columnHeaderValue As ColumnHeader = New ColumnHeader("programm") With {.Text = "Значение"}

        With ListViewPowerValue
            '.Columns.Clear()
            .BorderStyle = BorderStyle.Fixed3D
            .View = View.Details
            .LabelEdit = False
            .AllowColumnReorder = False
            .CheckBoxes = False
            .GridLines = True

            ' Назначить ImageList для ListView.
            .LargeImageList = ImageListNodes
            .SmallImageList = ImageListNodes

            .Columns.AddRange({columnHeaderParameter, columnHeaderValue})

            For Each itemColumnHeader As ColumnHeader In .Columns
                itemColumnHeader.TextAlign = HorizontalAlignment.Center
                itemColumnHeader.Width = .Width \ .Columns.Count - 2
            Next
        End With
    End Sub

    Private Sub TSpMenuItemShowStatusStrip_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSpMenuItemShowStatusStrip.Click
        StatusStripForm.Visible = TSpMenuItemShowStatusStrip.Checked
    End Sub

    ''' <summary>
    ''' Показать панель подсказок навигации по графику
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TSMenuItemShowNavigationPanel_Click(sender As Object, e As EventArgs) Handles TSMenuItemShowNavigationPanel.Click
        Static frmHelpGraph As FormHelpGraph

        TSMenuItemShowNavigationPanel.Checked = Not TSMenuItemShowNavigationPanel.Checked

        If TSMenuItemShowNavigationPanel.Checked Then
            frmHelpGraph = New FormHelpGraph(Me)
            frmHelpGraph.Show()
            frmHelpGraph.Activate()
            Dim message As String = $"Выведено справочное окно навигации по графику <{frmHelpGraph.Text}>."
            ShowMessageOnPanel(message)
            AddLogMessage(message, ColorForAlarm.Information0)
        Else
            If frmHelpGraph IsNot Nothing Then frmHelpGraph.Close()
        End If
    End Sub
#End Region

#Region "Добавление пунктов в меню"
    Private Sub AddComboBoxEngines()
        MenuStripForm.Items.Add(New ToolStripLabel() With {.Text = "Выбрать изделие:"})
        TSComboBoxEngines = New ToolStripComboBox()
        AddHandler TSComboBoxEngines.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged
        MenuStripForm.Items.Add(TSComboBoxEngines)
    End Sub

    Private Sub AddColorSplitButtons()
        CreateTSSplitButton("Цвет ламп {вкл.}:", TSSplitButtonOnColor, ledOnColor, AddressOf ColorOnDropDownItem_Click)
        CreateTSSplitButton("{выкл.}:", TSSplitButtonOffColor, ledOffColor, AddressOf ColorOffDropDownItem_Click)
    End Sub

    Private Sub CreateTSSplitButton(inLabelText As String, inButtonName As String, inColorName As String, inDelegate As EventHandler)
        Dim newTSSplitButtonColor As New ToolStripSplitButton With {
            .Name = inButtonName,
            .Text = inColorName
        }
        newTSSplitButtonColor.DropDownItems.Add(CreateTSMenuItemColor(ColorRed, Resources.SampleImageRed, inDelegate))
        newTSSplitButtonColor.DropDownItems.Add(CreateTSMenuItemColor(ColorYellow, Resources.SampleImageYellow, inDelegate))
        newTSSplitButtonColor.DropDownItems.Add(CreateTSMenuItemColor(ColorGreen, Resources.SampleImageGreen, inDelegate))
        MenuStripForm.Items.Add(New ToolStripLabel With {.Text = inLabelText})
        MenuStripForm.Items.Add(newTSSplitButtonColor)
    End Sub

    Private Function CreateTSMenuItemColor(colorName As String, inImage As Image, inDelegate As EventHandler) As ToolStripMenuItem
        Dim colorMenuItem As New ToolStripMenuItem With {
            .Text = colorName,
            .ToolTipText = colorName,
            .Name = colorName,
            .Image = inImage
        }
        AddHandler colorMenuItem.Click, inDelegate ' AddressOf ColorOnDropDownItem_Click
        Return colorMenuItem
    End Function

    Private Sub ColorOnDropDownItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ledOnColor = CType(sender, ToolStripMenuItem).Name
        MenuStripForm.Items(TSSplitButtonOnColor).Text = ledOnColor
        SetLedsColor(LedColorType.TypeOnColor)
    End Sub

    Private Sub ColorOffDropDownItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ledOffColor = CType(sender, ToolStripMenuItem).Name
        MenuStripForm.Items(TSSplitButtonOffColor).Text = ledOffColor
        SetLedsColor(LedColorType.TypeOffColor)
    End Sub

    ''' <summary>
    ''' Обновить Фон Индикаторов
    ''' </summary>
    Private Sub SetLedsColor(inLedColorType As LedColorType)
        If LedPorts IsNot Nothing Then
            For Each itemPort As LedPort In LedPorts.Values
                For I As Integer = 0 To itemPort.LineCount 'Mod<slot#>/port0/lineN" 'Dev0/port1/line0
                    Dim itemLed As Led = CType(itemPort.TableLayoutPanel.Controls.Item("LedPortLine" & I.ToString), Led)
                    Select Case inLedColorType
                        Case LedColorType.TypeDefault
                            itemLed.Value = False
                            itemLed.OffColor = Color.Silver
                            itemLed.Caption = I.ToString
                            ToolTipForm.SetToolTip(itemLed, "")
                        Case LedColorType.TypeOnColor
                            itemLed.OnColor = ColorOnFromName(ledOnColor)
                        Case LedColorType.TypeOffColor
                            If itemLed.OffColor <> Color.Silver Then
                                itemLed.OffColor = ColorOffFromName(ledOffColor)
                            End If
                    End Select
                Next
            Next
        End If
        ' если нужен проход по контролу
        'For Each itemControl As Control In TableLayoutPanelAllPorts.Controls
        '    Dim itemTableLayoutPanel As TableLayoutPanel = CType(itemControl, TableLayoutPanel)
        '    For Each loopControl As Control In itemTableLayoutPanel.Controls
        '        If TypeOf loopControl Is Led Then
        '            Dim itemLed As Led = CType(loopControl, Led)
        '        End If
        '    Next
        'Next
    End Sub

    Private Function ColorOnFromName(inColorName As String) As Color
        Select Case inColorName
            Case ColorRed
                Return Color.Red
            Case ColorYellow
                Return Color.Yellow
            Case ColorGreen
                Return Color.Lime
        End Select
    End Function

    Private Function ColorOffFromName(inColorName As String) As Color
        Select Case inColorName
            Case ColorRed
                Return Color.Maroon
            Case ColorYellow
                Return Color.Olive
            Case ColorGreen
                Return Color.DarkGreen
        End Select
    End Function
#End Region

#Region "Создание меню ControlNumericCycle"
    ''' <summary>
    ''' Создать пользовательский экземпляр ToolStripTabControl.
    ''' </summary>
    Private Sub CreateInstanceForModuleControlNumericCycle()
        UserNumericCycle = New ModuleControlNumericCycle() With {.Size = New Size(102, 87)}
    End Sub

    ''' <summary>
    ''' Добавить панель отслеживания количества циклов.
    ''' </summary>
    ''' <param name="isAdd"></param>
    Private Sub AddRemoveToolStripUserNumericCycle(ByVal isAdd As Boolean)
        If isAdd Then
            If UserNumericCycle Is Nothing OrElse UserNumericCycle.IsDisposed Then
                CreateInstanceForModuleControlNumericCycle()
            End If

            ToolStripOperation.Items.Add(UserNumericCycle)
        Else
            ToolStripOperation.Items.Remove(UserNumericCycle)
            UserNumericCycle.Dispose()
            UserNumericCycle = Nothing
        End If

        isVisibleUserNumericCycle = isAdd
    End Sub

    Private Sub ViewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewToolStripMenuItem.Click
        curMenuItem = CType(sender, ToolStripMenuItem)

        If curMenuItem Is ViewToolStripMenuItem Then
            ViewToolStripMenuItem.DropDownItems.AddRange(subMenu)
            ViewToolStripMenuItem.ShowDropDown()
            curToolStrip = ToolStripVert
            subMenuUserNumericCycle(0).Enabled = Not isVisibleUserNumericCycle
            subMenuUserNumericCycle(1).Enabled = isVisibleUserNumericCycle
        End If
    End Sub

    Private Sub UserNumericCycleHandler(ByVal sender As Object, ByVal e As EventArgs)
        If curToolStrip IsNot Nothing Then
            If curToolStrip Is ToolStripVert Then
                AddRemoveToolStripUserNumericCycle(Not isVisibleUserNumericCycle)
            End If
        End If
    End Sub
#End Region

#Region "ToolTips"
    ' рекурсия по пунктам меню
    Private Sub InitializeMenuHelperStrings(ByRef menuItems As MenuStrip)
        For Each itemToolStrip As ToolStripItem In menuItems.Items
            If (TypeOf itemToolStrip Is ToolStripMenuItem) Then
                Dim tsMenuItem As ToolStripMenuItem = CType(itemToolStrip, ToolStripMenuItem)
                'utilityHelper.AddMenuString(tsMenuItem)
                'AddHandler tsMenuItem.MouseHover, AddressOf OnMenuSelect
                AddHandler tsMenuItem.MouseEnter, AddressOf MenuItem_MouseEnter
                AddHandler tsMenuItem.MouseLeave, AddressOf MenuItem_MouseLeave

                InitializeMenuHelperStrings(tsMenuItem.DropDownItems)
            End If
        Next
    End Sub

    Private Sub InitializeMenuHelperStrings(ByRef menuItems As ToolStripItemCollection)
        For Each itemControl As Object In menuItems
            If (TypeOf itemControl Is ToolStripMenuItem) Then
                Dim tsMenuItem As ToolStripMenuItem = CType(itemControl, ToolStripMenuItem)
                'utilityHelper.AddMenuString(tsMenuItem)
                'AddHandler tsMenuItem.MouseHover, AddressOf OnMenuSelect
                AddHandler tsMenuItem.MouseEnter, AddressOf MenuItem_MouseEnter
                AddHandler tsMenuItem.MouseLeave, AddressOf MenuItem_MouseLeave

                InitializeMenuHelperStrings(tsMenuItem.DropDownItems)
            End If
        Next
    End Sub

    'Private lastStatus As String 'для ToolTips

    'Protected Overrides Sub OnMenuStart(ByVal e As EventArgs)
    '    MyBase.OnMenuStart(e)
    '    lastStatus = TSSLabelStatus.Text
    'End Sub

    'Protected Overrides Sub OnMenuComplete(ByVal e As EventArgs)
    '    MyBase.OnMenuComplete(e)
    '    TSSLabelStatus.Text = lastStatus
    'End Sub

    'Private Sub OnMenuSelect(ByVal sender As Object, ByVal e As EventArgs)
    '    TSSLabelStatus.Text = utilityHelper.GetMenuString(sender)
    'End Sub
#End Region

#Region "toolStripButton панели"
#Region "взаимосвязанные контролы Button и MenuItem"
    ' Если есть 2 взаимосвязанных контрола, совместно изменяющих значения друг друга
    ' необходимо отсекать рекурсивный вызов инициатора из дочернего
    ' Шаблон:
    'Private Sub trackBarValue_ValueChanged(sender As Object, e As EventArgs)
    '    If sender IsNot Me Then
    '        numericUpDownValue.Value = trackBarValue.Value
    '        OnValueChanged()
    '    End If
    'End Sub
    'Private Sub numericUpDownValue_ValueChanged(sender As Object, e As EventArgs)
    '    If sender IsNot Me Then
    '        trackBarValue.Value = Convert.ToInt32(numericUpDownValue.Value)
    '        OnValueChanged()
    '    End If
    'End Sub
    'Protected Sub OnValueChanged()
    '    Refresh()
    '    RaiseEvent ValueChanged(Me, EventArgs.Empty)
    'End Sub

    Dim isTSButtonShowTreeView As Boolean
    Dim isTSMenuItemShowTreeView As Boolean

    Private Sub TSButtonShowTreeView_CheckedChanged(sender As Object, e As EventArgs) Handles TSButtonShowTreeView.CheckedChanged
        If isTSMenuItemShowTreeView Then Exit Sub

        isTSButtonShowTreeView = True
        TSMenuItemShowTreeView.Checked = Not TSButtonShowTreeView.Checked
        isTSButtonShowTreeView = False
        OnShowTreeView()
    End Sub

    Private Sub TSMenuItemShowTreeView_CheckedChanged(sender As Object, e As EventArgs) Handles TSMenuItemShowTreeView.CheckedChanged
        If isTSButtonShowTreeView Then Exit Sub

        isTSMenuItemShowTreeView = True
        TSButtonShowTreeView.Checked = Not TSMenuItemShowTreeView.Checked
        isTSMenuItemShowTreeView = False
        OnShowTreeView()
    End Sub

    Private Sub OnShowTreeView()
        If TSButtonShowTreeView.Checked Then
            TSButtonShowTreeView.Image = Resources.TreeviewClose
            TSButtonShowTreeView.Text = SHOW_TEXT
            TSButtonShowTreeView.ToolTipText = SHOW_PANEL_LED_TREECYCLE
            TSMenuItemShowTreeView.ToolTipText = SHOW_PANEL_LED_TREECYCLE
            Dim message As String = "Панель {Выбор циклограмм} скрыта"
            ShowMessageOnPanel(message)
            AddLogMessage(message, ColorForAlarm.Information0)
        Else
            TSButtonShowTreeView.Image = Resources.TreeviewOpen
            TSButtonShowTreeView.Text = HIDE_TEXT
            TSButtonShowTreeView.ToolTipText = HIDE_PANEL_TREECYCLE
            TSMenuItemShowTreeView.ToolTipText = HIDE_PANEL_TREECYCLE
            SplitContainerCycle.SplitterDistance = 226
            Dim message As String = "Панель {Выбор циклограмм} показана"
            ShowMessageOnPanel(message)
            AddLogMessage(message, ColorForAlarm.Information0)
        End If
        SplitContainerCycle.Panel1Collapsed = TSButtonShowTreeView.Checked
    End Sub

    Dim isTButtonShowPahelLeds As Boolean
    Dim isTSMenuItemShowPahelLeds As Boolean

    Private Sub TButtonShowPahelLeds_CheckedChanged(sender As Object, e As EventArgs) Handles TButtonShowPahelLeds.CheckedChanged
        If isTSMenuItemShowPahelLeds Then Exit Sub

        isTButtonShowPahelLeds = True
        TSMenuItemShowPahelLeds.Checked = Not TButtonShowPahelLeds.Checked
        isTButtonShowPahelLeds = False
        OnShowPahelLeds()
    End Sub

    Private Sub TSMenuItemShowPahelLeds_CheckedChanged(sender As Object, e As EventArgs) Handles TSMenuItemShowPahelLeds.CheckedChanged
        If isTButtonShowPahelLeds Then Exit Sub

        isTSMenuItemShowPahelLeds = True
        TButtonShowPahelLeds.Checked = Not TSMenuItemShowPahelLeds.Checked
        isTSMenuItemShowPahelLeds = False
        OnShowPahelLeds()
    End Sub

    Private Sub OnShowPahelLeds()
        If TButtonShowPahelLeds.Checked Then
            TButtonShowPahelLeds.Image = Resources.ShowLeds
            TButtonShowPahelLeds.Text = SHOW_TEXT
            TButtonShowPahelLeds.ToolTipText = SHOW_PANEL_LED
            TSMenuItemShowPahelLeds.ToolTipText = SHOW_PANEL_LED
            Dim message As String = "Панель {Порты управления} скрыта"
            ShowMessageOnPanel(message)
            AddLogMessage(message, ColorForAlarm.Information0)
        Else
            TButtonShowPahelLeds.Image = Resources.HideLeds
            TButtonShowPahelLeds.Text = HIDE_TEXT
            TButtonShowPahelLeds.ToolTipText = HIDE_PANEL_LED
            TSMenuItemShowPahelLeds.ToolTipText = HIDE_PANEL_LED
            Dim message As String = "Панель {Порты управления} показана"
            ShowMessageOnPanel(message)
            AddLogMessage(message, ColorForAlarm.Information0)
            SplitContainerLed.SplitterDistance = SplitContainerLed.SplitterDistance + 1 ' сдвинуть для перерисовки, чтобы появилась полоса прокрутки
        End If
        SplitContainerAll.Panel2Collapsed = TButtonShowPahelLeds.Checked
    End Sub

    Dim isTSButtonShowPowerMeterControls As Boolean
    Dim isTSMenuItemShowPowerMeterControls As Boolean

    Private Sub TSButtonShowPowerMeterControls_CheckedChanged(sender As Object, e As EventArgs) Handles TSButtonShowPowerMeterControls.CheckedChanged
        If isTSMenuItemShowPowerMeterControls Then Exit Sub

        isTSButtonShowPowerMeterControls = True
        TSMenuItemShowPowerMeterControls.Checked = Not TSButtonShowPowerMeterControls.Checked
        isTSButtonShowPowerMeterControls = False
        ShowPowerTanks()
    End Sub

    Private Sub TSMenuItemShowPowerMeterControls_CheckedChanged(sender As Object, e As EventArgs) Handles TSMenuItemShowPowerMeterControls.CheckedChanged
        If isTSButtonShowPowerMeterControls Then Exit Sub

        isTSMenuItemShowPowerMeterControls = True
        TSButtonShowPowerMeterControls.Checked = Not TSMenuItemShowPowerMeterControls.Checked
        isTSMenuItemShowPowerMeterControls = False
        ShowPowerTanks()
    End Sub

    Private Sub ShowPowerTanks()
        If TSButtonShowPowerMeterControls.Checked Then
            TSButtonShowPowerMeterControls.Image = Resources.ShowPowers
            TSButtonShowPowerMeterControls.Text = SHOW_TEXT
            TSButtonShowPowerMeterControls.ToolTipText = SHOW_PANEL_POWERS
            TSMenuItemShowPowerMeterControls.ToolTipText = SHOW_PANEL_POWERS
            Dim message As String = "Панель {Индикаторы} скрыта"
            ShowMessageOnPanel(message)
            AddLogMessage(message, ColorForAlarm.Information0)
        Else
            TSButtonShowPowerMeterControls.Image = Resources.HidePowers
            TSButtonShowPowerMeterControls.Text = HIDE_TEXT
            TSButtonShowPowerMeterControls.ToolTipText = HIDE_PANEL_POWERS
            TSMenuItemShowPowerMeterControls.ToolTipText = HIDE_PANEL_POWERS
            SplitContainerCycle.SplitterDistance = 226
            Dim message As String = "Панель {Индикаторы} показана"
            ShowMessageOnPanel(message)
            AddLogMessage(message, ColorForAlarm.Information0)
        End If
        SplitContainerGraph.Panel2Collapsed = TSButtonShowPowerMeterControls.Checked
    End Sub

    Dim isTSButtonLanch As Boolean
    Dim isTSMenuItemLanch As Boolean

    Private Sub TSButtonLanch_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonLanch.CheckedChanged
        If isTSMenuItemLanch Then Exit Sub
        'DisablePopoutAction(TSButtonLanch.Checked)
        isTSButtonLanch = True
        TSMenuItemLanch.Checked = TSButtonLanch.Checked
        isTSButtonLanch = False
        LanchCycleProgram(TSButtonLanch.Checked)
    End Sub

    Private Sub TSMenuItemLanch_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TSMenuItemLanch.CheckedChanged
        If isTSButtonLanch Then Exit Sub
        isTSMenuItemLanch = True
        TSButtonLanch.Checked = TSMenuItemLanch.Checked
        isTSMenuItemLanch = False
        LanchCycleProgram(TSMenuItemLanch.Checked)
    End Sub

    ''' <summary>
    ''' Выключить меню освобождения окна для предотвращения действий пользователя.
    ''' При освобождении окна скорее всего происходит потеря дескриптора окна и контролы-подписчики
    ''' типа BaseControlObserver теряют свой поток управления
    ''' </summary>
    ''' <param name="checked"></param>
    ''' <remarks></remarks>
    Private Sub DisablePopoutAction(checked As Boolean)
        MainFomMdiParent.WindowManagerPanel1.DisablePopoutAction = checked
        MainFomMdiParent.WindowPopOutMenuItem.Enabled = Not checked
    End Sub
#End Region

#Region "Navigation"
    Private Sub TSButtonAlarm_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonAlarm.Click
        AlarmStopCycle()
    End Sub

    ''' <summary>
    ''' Аварийная остановка программы цикла загрузки
    ''' </summary>
    Private Sub AlarmStopCycle()
        TSButtonPause.Enabled = False
        TSButtonPause.Checked = False
        TSButtonReady.Enabled = False
        TSButtonLanch.Enabled = True
        TSMenuItemLanch.Enabled = True
        TSButtonLanch.Checked = False
        TSButtonAlarm.Enabled = False

        Dim message As String = $"Аварийная остановка программы цикла загрузки <{selectedTestProgram}>!"
        ShowMessageOnPanel(message)
        AddLogMessage(message, ColorForAlarm.AlarmMessage7)
        IsLanchCycleProgram = False
        ' делема или останавливать курсор на месте или сбрасывать все устройства в положение 0 загрузки
        ' то же делать по закрытию программы все устройства в положение 0 загрузки
        'XyCursor1.XPosition = 0
        'XyCursor1.YPosition = 0
        SetAggregatesOnDefaultPower()
    End Sub

    ''' <summary>
    ''' Установить Все Устройства По Умолчанию
    ''' </summary>
    Private Sub SetAggregatesOnDefaultPower()
        If isCheckCyclogramCorrect AndAlso maxSumTimeCycle <> 0 Then
            WritePowersInPorts(True)
        End If
    End Sub

    Private Sub TSButtonReady_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonReady.Click
        TSButtonPause.Enabled = False
        TSButtonAlarm.Enabled = False
        TSButtonReady.Enabled = False
        TSButtonLanch.Enabled = True
        TSMenuItemLanch.Enabled = True
        TSButtonLanch.Checked = False
    End Sub

    Private Sub TSButtonPause_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonPause.CheckedChanged
        IsLanchCycleProgram = Not TSButtonPause.Checked

        If IsLanchCycleProgram Then
            TSButtonPause.Image = Resources.lock
            TSButtonPause.Text = "Пауза"
            TSButtonPause.ToolTipText = "Приостановить исполнение циклограммы"
            Dim message As String = "Возобновление цикла испытания"
            ShowMessageOnPanel(message)
            AddLogMessage(message, ColorForAlarm.Ok3)
        Else
            TSButtonPause.Image = Resources.start
            TSButtonPause.Text = "Далее" ' "Продолжить" длинное слово - вызывает сдвиг и длительную перерисовку
            TSButtonPause.ToolTipText = "Продолжить исполнение циклограммы"
            Dim message As String = "Приостановка цикла испытания"
            ShowMessageOnPanel(message)
            AddLogMessage(message, ColorForAlarm.Criterion6)
        End If
    End Sub

    Private Sub TSButtonBackwards_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles TSButtonBackwards.MouseDown
        isBackward = True
    End Sub

    Private Sub TSButtonBackwards_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles TSButtonBackwards.MouseUp
        isBackward = False
    End Sub

    Private Sub TSButtonForward_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles TSButtonForward.MouseDown
        isForward = True
    End Sub

    Private Sub TSButtonForward_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles TSButtonForward.MouseUp
        isForward = False
    End Sub
#End Region
#End Region

#Region "Изделия"
    ''' <summary>
    ''' Заполнить Список Типы Изделия.
    ''' Вызывается при выборе из списка типов изделия.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillComboBoxEngines()
        Dim dtDataTable As New DataTable
        Dim count As Integer = 1
        Dim isTypeEngineFound As Boolean ' тип Изделия Найден
        Dim selectedIndex As Integer

        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            Try
                cn.Open()
                TSComboBoxEngines.Items.Clear()
                Dim odaDataAdapter As OleDbDataAdapter = New OleDbDataAdapter("SELECT * FROM ТипИзделия1", cn)
                odaDataAdapter.Fill(dtDataTable) ' здесь должны быть все типы изделия
            Catch ex As Exception
                MessageBox.Show(ex.ToString, $"Ошибка обновления в процедуре <{NameOf(FillComboBoxEngines)}>.",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Using

        If dtDataTable.Rows.Count > 0 Then
            For Each itemRow As DataRow In dtDataTable.Rows
                TSComboBoxEngines.Items.Add(Convert.ToString(itemRow("ТипИзделия")))

                If selectedTypeEngine = Convert.ToString(itemRow("ТипИзделия")) Then
                    isTypeEngineFound = True
                    selectedIndex = count
                End If

                count += 1
            Next
        End If

        If isTypeEngineFound Then
            TSComboBoxEngines.SelectedIndex = selectedIndex - 1
        Else
            If TSComboBoxEngines.Items.Count > 0 Then
                TSComboBoxEngines.SelectedIndex = 0
            End If
        End If

        If isTypeEngineFound Then
            RestoreNameTestProgram()
            ' произведена загрузка в дерево типа изделия, далее выделить возможнуNumericUpDownPrecisionScreenю сохранённую программу испытаний
            SelectTreeViewNodeTestProgram()
        End If
    End Sub

    ''' <summary>
    ''' Найти и выделеить узел с именем memoTestProgram сохранённым в конфигурации.
    ''' Загрузить выделенный цикл.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectTreeViewNodeTestProgram()
        For Each itemNode As DirectoryNode In TreeViewSycleProgram.Nodes(0).Nodes
            If itemNode.Caption = selectedTestProgram Then
                TreeViewSycleProgram.SelectedNode = itemNode ' там происходит NodeLoop.Expand()
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' Заполнение дерева проводника при выборе типа изделия
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ComboBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim combo As ToolStripComboBox = CType(sender, ToolStripComboBox)

        selectedTypeEngine = combo.Text
        ' здесь по combo.Text обновление дерева загрузок по выбранному номеру изделия
        TSLabelSelectCycle.Text = "Выбрать циклограмму изделия " & selectedTypeEngine

        ' записать в Опции выбранный файл
        Dim cReadWriteIni As New ReadWriteIni(pathOptionsXml)
        Dim xmlDoc As XElement = XElement.Load(pathOptionsXml)

        cReadWriteIni.writeINI(xmlDoc, "Engine", "Common", "TypeEngine", selectedTypeEngine)
        PopulateEngineTree()
    End Sub
#End Region

#Region "TreeView"
    Private Class DirectoryNode
        Inherits TreeNode

        Public Property NodeType() As NodeType
        Public Property KeyId() As Integer
        Public Property SubDirectoriesAdded() As Boolean
        Public Property Caption As String

        Public Sub New(ByVal inText As String, ByVal nodeType As NodeType, ByVal KeyId As Integer, caption As String)
            MyBase.New(inText)
            Me.NodeType = nodeType
            Me.KeyId = KeyId
            Me.Caption = caption
        End Sub
    End Class

    ''' <summary>
    ''' Заполнить 1ТипыИзделий
    ''' </summary>
    Private Sub PopulateEngineTree()
        Dim strSQL As String = "SELECT * FROM ТипИзделия1 Where ТипИзделия = '" & selectedTypeEngine & "'"

        ' все типы изделия
        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            Try
                cn.Open()
                Dim odaDataAdapter As OleDbDataAdapter = New OleDbDataAdapter(strSQL, cn)
                Dim dtDataTable As New DataTable

                odaDataAdapter.Fill(dtDataTable)
                TreeViewSycleProgram.Nodes.Clear()
                isCheckCyclogramCorrect = False

                If dtDataTable.Rows.Count > 0 Then
                    For Each itemDataRow As DataRow In dtDataTable.Rows
                        keyTypeEngine = Convert.ToInt32(itemDataRow("keyТипИзделия"))
                        Dim cRoot As New DirectoryNode("Тип изделия-" & Convert.ToString(itemDataRow("ТипИзделия")),
                                                       NodeType.TypeEngine,
                                                       keyTypeEngine,
                                                       Convert.ToString(itemDataRow("ТипИзделия"))) With {.SelectedImageIndex = 0, .ImageIndex = 4}
                        TreeViewSycleProgram.Nodes.Add(cRoot)
                        AddDirectories(cRoot, cn)
                    Next
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString, $"Ошибка обновления в процедуре <{NameOf(PopulateEngineTree)}>.",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Using
    End Sub

    Private Sub AddDirectories(ByVal node As DirectoryNode, ByRef cn As OleDbConnection)
        Dim cmd As OleDbCommand = cn.CreateCommand
        cmd.CommandType = CommandType.Text

        Select Case node.NodeType
            Case NodeType.TypeEngine ' родитель ТипыИзделия
                node.ImageIndex = 1
                keyTypeEngine = node.KeyId
                cmd.CommandText = "SELECT * FROM ПрограммаИспытаний2 WHERE keyТипИзделия =  " & keyTypeEngine

                Using rdr As OleDbDataReader = cmd.ExecuteReader
                    Do While rdr.Read()
                        keyTestProgram = Convert.ToInt32(rdr("keyПрограммаИспытаний"))
                        node.Nodes.Add(New DirectoryNode("Программа-" & Convert.ToString(rdr("ИмяПрограммы")),
                                                         NodeType.TestProgram,
                                                         keyTestProgram,
                                                         Convert.ToString(rdr("ИмяПрограммы"))))
                        node.LastNode.SelectedImageIndex = 0
                        node.LastNode.ImageIndex = NodeType.TestProgram
                    Loop
                End Using
            Case NodeType.TestProgram ' родитель ПрограммаИспытаний
                node.ImageIndex = 2
                keyTestProgram = node.KeyId
                cmd.CommandText = "SELECT * FROM ЦиклЗагрузки3 WHERE keyПрограммаИспытаний =  " & keyTestProgram

                Using rdr As OleDbDataReader = cmd.ExecuteReader
                    Do While rdr.Read()
                        keyCycleStage = Convert.ToInt32(rdr("keyЦиклЗагрузки"))
                        Dim имяУстройства As String = Convert.ToString(rdr("ИмяУстройства"))
                        node.Nodes.Add(New DirectoryNode("Цикл-" & имяУстройства, NodeType.SycleDevice, keyCycleStage, имяУстройства))
                        node.LastNode.SelectedImageIndex = 0
                        node.LastNode.ImageIndex = NodeType.SycleDevice
                    Loop
                End Using
            Case NodeType.SycleDevice ' родитель ЦиклЗагрузки
                node.ImageIndex = 3
                keyCycleStage = node.KeyId
                cmd.CommandText = "SELECT ЦиклЗагрузки3.*, ПерекладкиЦикла4.* " &
                    "FROM ЦиклЗагрузки3 RIGHT JOIN ПерекладкиЦикла4 ON ЦиклЗагрузки3.keyЦиклЗагрузки = ПерекладкиЦикла4.keyЦиклЗагрузки " &
                    "WHERE (((ЦиклЗагрузки3.keyЦиклЗагрузки)=" & keyCycleStage.ToString & ")) " &
                    "ORDER BY ПерекладкиЦикла4.keyПерекладкиЦикла;"

                Using rdr As OleDbDataReader = cmd.ExecuteReader
                    Do While rdr.Read()
                        Dim caption As String = $"Величина: {Convert.ToString(rdr("ВеличинаЗагрузки"))} -> Время: {Convert.ToString(rdr("TimeAction"))} {Convert.ToString(rdr("TimeValue"))}"
                        keyStepCycleStage = Convert.ToInt32(rdr("keyПерекладкиЦикла"))
                        node.Nodes.Add(New DirectoryNode(caption, NodeType.SycleStage, keyStepCycleStage, caption))
                        node.LastNode.SelectedImageIndex = 0
                        node.LastNode.ImageIndex = NodeType.SycleStage
                    Loop
                End Using
        End Select
    End Sub

    Dim isAfterSelectIsRequire As Boolean = True
    Dim isBeforeExpandIsRequire As Boolean = True

    Private Sub TreeViewSycleProgram_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TreeViewSycleProgram.AfterSelect
        If IsHandleCreated Then
            Dim nodeSelect As DirectoryNode = CType(e.Node, DirectoryNode)

            Select Case nodeSelect.NodeType
                Case NodeType.TypeEngine
                    keyTypeEngine = nodeSelect.KeyId
                Case NodeType.TestProgram
                    PrepareLanchTestProgram(nodeSelect)
                Case NodeType.SycleDevice
                    keyCycleStage = nodeSelect.KeyId
                    SelectScatterPlot(keyCycleStage)
                Case NodeType.SycleStage
                    keyStepCycleStage = nodeSelect.KeyId
            End Select

            isAfterSelectIsRequire = False
            If isBeforeExpandIsRequire Then e.Node.Expand()
            isAfterSelectIsRequire = True
        End If
    End Sub

    Private Sub TreeViewSycleProgram_AfterCollapse(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TreeViewSycleProgram.AfterCollapse
        e.Node.BackColor = Color.White
    End Sub

    Private Sub TreeViewSycleProgram_BeforeExpand(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs) Handles TreeViewSycleProgram.BeforeExpand
        Dim nodeExpanding As DirectoryNode = CType(e.Node, DirectoryNode)

        If e.Node.Parent IsNot Nothing Then
            For Each itemNode As TreeNode In e.Node.Parent.Nodes
                If itemNode.IsExpanded Then itemNode.Collapse()
            Next
        Else ' по самому первому уровню
            For Each itemNode As TreeNode In TreeViewSycleProgram.Nodes
                If itemNode.IsExpanded Then itemNode.Collapse()
            Next
        End If

        If Not nodeExpanding.SubDirectoriesAdded Then AddSubDirectories(nodeExpanding)

        e.Node.EnsureVisible()
        e.Node.BackColor = Color.Gold

        isBeforeExpandIsRequire = False
        If isAfterSelectIsRequire Then TreeViewSycleProgram_AfterSelect(sender, New TreeViewEventArgs(e.Node, TreeViewAction.Expand))
        isBeforeExpandIsRequire = True
    End Sub

    Private Sub AddSubDirectories(ByVal node As DirectoryNode)
        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            cn.Open()

            For I As Integer = 0 To node.Nodes.Count - 1
                AddDirectories(CType(node.Nodes(I), DirectoryNode), cn)
            Next
        End Using

        node.SubDirectoriesAdded = True
    End Sub

    ''' <summary>
    ''' Полная проверка и настройка для запуска выбранной программы циклической загрузки.
    ''' </summary>
    ''' <param name="nodeSelect"></param>
    Private Sub PrepareLanchTestProgram(nodeSelect As DirectoryNode)
        'mmTimer.Stop()
        TimerCycle.Enabled = False ' чтобы не было событий обновления до окончательного считывания
        SetAggregatesOnDefaultPower()
        SetLedsColor(LedColorType.TypeDefault)
        keyTestProgram = nodeSelect.KeyId
        TSLabelSycle.Text = nodeSelect.Caption
        SaveOptionsTypeEngine(nodeSelect.Caption)
        currentTimeCycle = 0
        UpdateGraphCyclograms()
        CheckCyclogram()

        If mAggregates Is Nothing Then Exit Sub

        PopulateListViewPowerValue()
        PopulateControlToFlowLayoutPanel()
        CompletedProgressBar()

        If isCheckCyclogramCorrect Then TimerCycle.Enabled = True 'mmTimer.Start() 
    End Sub

    ''' <summary>
    ''' Вывод шлейфов всех циклограмм загрузок устройств после перезаписи или загрузки новой программы.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateGraphCyclograms()
        Dim AllPointsTimeDuration As New List(Of Double()) ' для храненеия массивов Длительность
        Dim AllPointsStageToNumeric As New List(Of Double()) ' для храненеия массивов Величина
        Dim AllSumTimeCycle As New List(Of Double) ' Суммарное Время Перекладки
        Dim dataTableSycleDevice As New DataTable
        Dim strSQL As String = "SELECT ЦиклЗагрузки3.* " &
            "FROM ЦиклЗагрузки3 " &
            "WHERE (((ЦиклЗагрузки3.keyПрограммаИспытаний)= " & keyTestProgram.ToString & ")) " &
            "ORDER BY ЦиклЗагрузки3.keyЦиклЗагрузки;"

        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            Try
                cn.Open()
                Dim odaDataAdapter As OleDbDataAdapter = New OleDbDataAdapter(strSQL, cn)
                odaDataAdapter.Fill(dataTableSycleDevice)
            Catch ex As Exception
                MessageBox.Show(ex.ToString, $"Ошибка обновления циклограммы в процедуре <{NameOf(UpdateGraphCyclograms)}>.",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Using

        If dataTableSycleDevice.Rows.Count > 0 Then
            PrepareCollectionsForUpdateGraph(AllPointsTimeDuration, AllPointsStageToNumeric, AllSumTimeCycle, dataTableSycleDevice)
        End If

        '--- Второй этап, если нет ошибки -------------------------------------
        Dim offset, offset2 As Double
        Dim plotCount As Integer = dataTableSycleDevice.Rows.Count ' кол. Шлейфов
        Dim slope As Double = 1 / plotCount
        Dim xoffset As Single = 10
        Dim yoffset As Single
        Dim maximumY, minimumY As Double

        For indexDevice As Integer = 0 To dataTableSycleDevice.Rows.Count - 1
            Dim tempCycleRow As DataRow = dataTableSycleDevice.Rows(indexDevice)

            If Not mAggregates.ContainsKey(CInt(tempCycleRow("keyЦиклЗагрузки"))) Then Continue For
            If indexDevice > AllPointsTimeDuration.Count - 1 Then Continue For
            If AllPointsTimeDuration.Item(indexDevice).Length = 0 Then Exit For

            ' привязать к началу шлейфа аннотацию с пояснением к какой сетевой переменной он относится
            ' привязка к осям если они разные
            Dim plot As ScatterPlot = New ScatterPlot With {
                .LineColor = ColorsNet((indexDevice) Mod 7),
                .PointColor = Color.Red,
                .PointStyle = PointStyle.SolidCircle,
                .XAxis = XAxis1,
                .YAxis = YAxis1,
                .Tag = CInt(tempCycleRow("keyЦиклЗагрузки")) ' для удаления или получения сведений
            }
            ScatterGraphCycle.Plots.Add(plot)

            If indexDevice = 0 Then
                offset2 = 0
            Else
                offset2 = (2 / plotCount) * indexDevice * 1.01
            End If

            ' нормализовать и привести щлейф для загрузки
            Dim mean As Double = 0
            Dim standartDeviation As Double = 0
            Dim scale As Double = 0

            ' нормализовать входной вектор
            Dim arrNormalize1D As Double() = ArrayOperation.Normalize1D(AllPointsStageToNumeric.Item(indexDevice), mean, standartDeviation) ' arrВеличина, mean, standartDeviation)
            ' скалировать входной вектор к диапазогу [-1;1]
            Dim scaleNormalizePointsStage As Double() = ArrayOperation.Scale1D(arrNormalize1D, offset, scale)
            ' произвести линеаризацию вектора к данному диапазону с  учётом смещения
            Dim linearScaleNormalizePointsStage As Double() = ArrayOperation.LinearEvaluation1D(scaleNormalizePointsStage, slope, offset2)
            ' найти мин и макс значения
            ArrayOperation.MaxMin1D(linearScaleNormalizePointsStage, maximumY, minimumY)
            ' заполнить график значениями
            plot.PlotXY(AllPointsTimeDuration.Item(indexDevice), linearScaleNormalizePointsStage)
            ' привязать курсор к графику
            XyCursor1.Plot = plot

            For I As Integer = 0 To UBound(AllPointsTimeDuration.Item(indexDevice)) - 1 Step 2
                If indexDevice = dataTableSycleDevice.Rows.Count - 1 Then
                    ' смещение в пикселях
                    If AllPointsStageToNumeric.Item(indexDevice)(I) = 0 Then
                        yoffset = -20
                    Else
                        yoffset = 20
                    End If
                Else
                    yoffset = -20
                End If

                TempPointAnnotation = New XYPointAnnotation With {
                    .ArrowColor = Color.Gold,
                    .ArrowHeadStyle = ArrowStyle.SolidStealth,
                    .ArrowLineWidth = 1.0!,
                    .ArrowTailSize = New Size(20, 15),
                    .Caption = $"{Convert.ToString(Math.Round(AllPointsStageToNumeric.Item(indexDevice)(I), 3))} на {Convert.ToString(AllPointsTimeDuration.Item(indexDevice)(I))}(ой) сек",
                    .CaptionAlignment = New AnnotationCaptionAlignment(BoundsAlignment.None, xoffset, yoffset),
                    .CaptionFont = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte)),
                    .CaptionForeColor = Color.Magenta,
                    .ShapeFillColor = Color.Red, ' идёт первым
                    .ShapeSize = New Size(5, 5),
                    .ShapeStyle = ShapeStyle.Oval,
                    .ShapeZOrder = AnnotationZOrder.AbovePlot,
                    .XAxis = XAxis1,
                    .XPosition = AllPointsTimeDuration.Item(indexDevice)(I),
                    .YAxis = YAxis1,
                    .YPosition = linearScaleNormalizePointsStage(I)
                    }
                TempPointAnnotation.SetPosition(AllPointsTimeDuration.Item(indexDevice)(I), linearScaleNormalizePointsStage(I))
                ScatterGraphCycle.Annotations.Add(TempPointAnnotation)
            Next

            ' привязать к началу шлейфа аннотацию с пояснением к какому агрегату он относится
            xoffset = 10
            yoffset = -20
            TempPointAnnotation = New XYPointAnnotation With {
                .ArrowColor = plot.LineColor,
                .ArrowHeadStyle = ArrowStyle.SolidStealth,
                .ArrowLineWidth = 2.0!,
                .ArrowTailSize = New Size(20, 15),
                .Caption = Convert.ToString(tempCycleRow("ИмяУстройства")),
                .CaptionAlignment = New AnnotationCaptionAlignment(BoundsAlignment.None, xoffset, yoffset),
                .CaptionFont = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte)),
                .CaptionForeColor = plot.LineColor,
                .ShapeSize = New Size(5, 5),
                .ShapeStyle = ShapeStyle.Oval,
                .ShapeZOrder = AnnotationZOrder.AbovePlot,
                .XAxis = XAxis1,
                .XPosition = AllPointsTimeDuration.Item(indexDevice)(0),
                .YAxis = YAxis1,
                .YPosition = linearScaleNormalizePointsStage(0)
            }

            TempPointAnnotation.SetPosition(AllPointsTimeDuration.Item(indexDevice)(0), linearScaleNormalizePointsStage(0))
            ScatterGraphCycle.Annotations.Add(TempPointAnnotation)
        Next indexDevice

        ' определим максимальное значение из всех sumTimeCycle
        If AllSumTimeCycle.Count > 0 Then ArrayOperation.MaxMin1D(AllSumTimeCycle.ToArray, maxSumTimeCycle, minimumY)
        maxSumTimeCycle = Math.Round(maxSumTimeCycle, 3)
    End Sub

    ''' <summary>
    ''' Подготовить коллекции для построения графиков циклограмм.
    ''' </summary>
    ''' <param name="refAllPointsTimeDuration"></param>
    ''' <param name="refAllPointsStageToNumeric"></param>
    ''' <param name="refAllSumTimeCycle"></param>
    Private Sub PrepareCollectionsForUpdateGraph(ByRef refAllPointsTimeDuration As List(Of Double()),
                                                 ByRef refAllPointsStageToNumeric As List(Of Double()),
                                                 ByRef refAllSumTimeCycle As List(Of Double),
                                                 ByRef refDataTableSycleDevice As DataTable)
        ' для данного цикла выбрать все устройства
        ' в цикле по устройствам делать запросы на выборку
        ' в цикле по записям настроить плоты и теги  в масштабе для перекладок цикла для данного устройства
        XyCursor1.XPosition = 0
        XyCursor1.YPosition = 0
        XyCursor1.Plot = Nothing
        ScatterGraphCycle.Annotations.Clear()
        ScatterGraphCycle.Plots.Clear()
        mAggregates = New Aggregates

        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            Try
                cn.Open()
                Dim cmd As OleDbCommand = cn.CreateCommand
                cmd.CommandType = CommandType.Text

                For Each itemCycleRow As DataRow In refDataTableSycleDevice.Rows
                    Dim nameDevice As String = Convert.ToString(itemCycleRow("ИмяУстройства"))
                    If mAggregates.NameExist(nameDevice) Then
                        MessageBox.Show($"Устройство <{nameDevice}> не может быть в цикле более одного раза!{Environment.NewLine}Устройство будет исключено из цикла.{Environment.NewLine}Исправьте циклограмму в редакторе.",
                                        "Проверка состава циклограммы", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Continue For
                    End If

                    Dim keyCycle As Integer = CInt(itemCycleRow("keyЦиклЗагрузки"))
                    ' заполнить коллекцию исполнительных устройств участвующих в циклограмме
                    Dim aggregateAttributes As AttributesChargeParameter = GetAttributesChargeParameter(nameDevice, MainFomMdiParent.PathDBaseCycle)
                    Dim newAggregate As AggregateCycle = New AggregateCycle(aggregateAttributes.KeyDevice, nameDevice, 0, aggregateAttributes.Target, aggregateAttributes.Port)

                    ' в каждом устройстве создается коллекция величин загрузок
                    cmd.CommandText = "SELECT Устройства1.*, ВеличинаЗагрузки2.* " &
                    "FROM Устройства1 RIGHT JOIN ВеличинаЗагрузки2 ON Устройства1.KeyУстройства = ВеличинаЗагрузки2.KeyУстройства " &
                    "WHERE (((Устройства1.ИмяУстройства)='" & nameDevice & "')) " &
                    "ORDER BY ВеличинаЗагрузки2.ВеличинаЗагрузки;"

                    Using rdr As OleDbDataReader = cmd.ExecuteReader
                        If Not rdr.HasRows Then
                            MessageBox.Show($"Для устройства <{nameDevice}> не заданы величины загрузок.",
                                            "Загрузка цикла", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            Do While rdr.Read()
                                newAggregate.Add(Convert.ToSingle(rdr("ЧисловоеЗначение")))
                            Loop
                        End If
                    End Using

                    cmd.CommandText = "SELECT ЦиклЗагрузки3.*, ПерекладкиЦикла4.* " &
                        "FROM ЦиклЗагрузки3 RIGHT JOIN ПерекладкиЦикла4 ON ЦиклЗагрузки3.keyЦиклЗагрузки = ПерекладкиЦикла4.keyЦиклЗагрузки " &
                        "WHERE (((ЦиклЗагрузки3.keyЦиклЗагрузки)= " & itemCycleRow("keyЦиклЗагрузки").ToString & ")) " &
                        "ORDER BY ПерекладкиЦикла4.keyПерекладкиЦикла;"

                    Dim Stages As New List(Of Single)
                    Dim TimeDurations As New List(Of Double)
                    Dim TimeUnits As New List(Of String)

                    Using rdr As OleDbDataReader = cmd.ExecuteReader
                        Do While rdr.Read()
                            Stages.Add(Convert.ToSingle(rdr("ЧисловоеЗначение")))
                            TimeDurations.Add(Convert.ToDouble(rdr("TimeAction"))) ' Длительность
                            TimeUnits.Add(rdr("TimeValue").ToString) ' Ед.изм.
                        Loop
                    End Using

                    If TimeDurations.Count = 0 Then
                        MessageBox.Show($"Для устройства <{nameDevice}::{itemCycleRow("Описание")}> {Environment.NewLine}цикл исполнения отсутствует. Необходимо ввести хотя бы один уровень.",
                                        "Загрузка цикла", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cn.Close()
                        Exit Sub
                    End If

                    newAggregate.PopulatePointsStageToNumeric(Stages, TimeDurations, TimeUnits)
                    mAggregates.Add(keyCycle, newAggregate)
                    WatchObject(newAggregate)
                Next

                mAggregates.PrepareCollectionsForUpdateGraph(refAllPointsTimeDuration, refAllPointsStageToNumeric, refAllSumTimeCycle)
            Catch ex As Exception
                MessageBox.Show(ex.ToString, $"Ошибка обновления циклограммы в процедуре <{NameOf(PrepareCollectionsForUpdateGraph)}>.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Using
    End Sub

    ''' <summary>
    ''' При загрузке циклограммы проводится проверка с предупреждением, если 
    ''' устройство включённое в циклограмму загрузок было удалено,
    ''' цикл не содержит перекладок или его длительность равна нулю.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckCyclogram()
        ' для накопления сообщений и логическая переменная  о проблеме
        Dim strbldResult As StringBuilder = New StringBuilder("Были обнаружены следующие проблемы: " & Environment.NewLine)

        isCheckCyclogramCorrect = True

        If mAggregates Is Nothing OrElse mAggregates.Count = 0 Then
            strbldResult.AppendLine("Устройства в цикле отсутствуют.")
            isCheckCyclogramCorrect = False
            Exit Sub
        End If

        mAggregates.CheckProgramExist(strbldResult, isCheckCyclogramCorrect)

        Dim aggregateNames As New List(Of String)

        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            cn.Open()
            Dim cmd As New OleDbCommand("SELECT Устройства1.ИмяУстройства FROM Устройства1;", cn) With {.CommandType = CommandType.Text}

            Using drDataReader As OleDbDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                If drDataReader.HasRows Then
                    Do While drDataReader.Read()
                        aggregateNames.Add(Convert.ToString(drDataReader("ИмяУстройства")))
                    Loop
                End If
            End Using
        End Using

        mAggregates.CheckAggregateIsRenameOrRemove(strbldResult, isCheckCyclogramCorrect, aggregateNames)

        If isCheckCyclogramCorrect Then PopulateAggregatesOnCycle(strbldResult)

        If isCheckCyclogramCorrect Then
            XyCursor1.SnapMode = CursorSnapMode.Floating
            Dim message As String = $"Программа циклограммы загрузки <{selectedTestProgram}> готова к выполнению."
            ShowMessageOnPanel(message)
            AddLogMessage(message, ColorForAlarm.Condition2)
        Else
            XyCursor1.SnapMode = CursorSnapMode.Fixed
            Dim message As String = $"Исполнение циклограммы загрузки <{selectedTestProgram}> невозможно до устранения всех ошибок!"
            ShowMessageOnPanel(message)
            AddLogMessage(message, ColorForAlarm.AlarmMessage7)
            strbldResult.AppendLine(message)
            MessageBox.Show(strbldResult.ToString, "Обнаружены следующие проблемы:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        TSButtonForward.Visible = isCheckCyclogramCorrect
        TSButtonBackwards.Visible = isCheckCyclogramCorrect
        TSButtonPause.Visible = isCheckCyclogramCorrect
        TSButtonLanch.Enabled = isCheckCyclogramCorrect
        TSMenuItemLanch.Enabled = isCheckCyclogramCorrect
    End Sub

    ''' <summary>
    ''' Загрузить все устройства Цикла.
    ''' </summary>
    ''' <param name="strbldResult"></param>
    Private Sub PopulateAggregatesOnCycle(ByRef strbldResult As StringBuilder)
        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            Try
                ' Считывается загрузка, в цикле создается коллекция устройств
                cn.Open()
                Dim cmd As OleDbCommand = cn.CreateCommand
                cmd.CommandType = CommandType.Text

                ' создается для величин загрузок коллекция битов портов
                For Each itemAggregate As AggregateCycle In mAggregates
                    cmd.CommandText = "SELECT DISTINCT ВеличинаЗагрузки2.ЧисловоеЗначение, ДискретныйВыход3.* " &
                        "FROM Устройства1 RIGHT JOIN (ВеличинаЗагрузки2 RIGHT JOIN ДискретныйВыход3 ON ВеличинаЗагрузки2.keyВеличинаЗагрузки = ДискретныйВыход3.keyВеличинаЗагрузки) ON Устройства1.KeyУстройства = ВеличинаЗагрузки2.KeyУстройства " &
                        "WHERE Устройства1.KeyУстройства = " & itemAggregate.KeyAggregate.ToString

                    Using rdr As OleDbDataReader = cmd.ExecuteReader
                        If Not rdr.HasRows Then
                            strbldResult.AppendLine($"Для устройства <{itemAggregate.NameAggregate}> не установлена ни одна линия порта ни для одной величины загрузки.")
                            isCheckCyclogramCorrect = False
                        Else
                            Do While rdr.Read
                                itemAggregate(Convert.ToSingle(rdr("ЧисловоеЗначение"))).Add(Convert.ToInt32(rdr("НомерЛинии"))) ' CBool(rdr("ЗначениеЛогики"))
                            Loop
                        End If
                    End Using
                Next
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                isCheckCyclogramCorrect = False
            End Try
        End Using

        mAggregates.CheckLinesInPort(strbldResult, isCheckCyclogramCorrect)

        If isCheckCyclogramCorrect Then
            isCheckCyclogramCorrect = LoadAllSelectedConfigurations(strbldResult)
        End If
    End Sub

    ''' <summary>
    ''' Создать коллекцию всех линий для всех агрегатов включённых программу загрузок.
    ''' Конфигурация всех коллекций дискретных выходов.
    ''' </summary>
    ''' <param name="inStrbldResult"></param>
    ''' <returns></returns>
    Public Function LoadAllSelectedConfigurations(ByRef inStrbldResult As StringBuilder) As Boolean
        If LedPorts Is Nothing Then Return isCheckCyclogramCorrect

        mPortWriters.LoadAllSelectedConfigurations(mAggregates)
        isCheckCyclogramCorrect = True

        For Each itemPortlWriter As PortWriter In mPortWriters
            ' поиск по контролам
            For Each itemDicPort As LedPort In LedPorts.Values
                If itemDicPort.DeviceName = itemPortlWriter.ToString Then
                    For Each itemLine As Line In itemPortlWriter.LinesToArray 'Dev0/port1/line0
                        ' Строковая заготовка
                        Dim textMessage As String
                        If itemPortlWriter.NumberModuleChassis = String.Empty Then
                            textMessage = $"Плата:<{itemPortlWriter.DeviceName}>{vbCrLf}Порт:<{itemPortlWriter.PortNumber}>{vbCrLf}Линия:<{itemLine.Caption}>{vbCrLf}"
                        Else
                            textMessage = $"Корзина:<{itemPortlWriter.NumberModuleChassis}>{vbCrLf}Модуль:<{itemPortlWriter.DeviceName}>{vbCrLf}Порт:<{itemPortlWriter.PortNumber}> & {vbCrLf}Линия:<{itemLine.Caption}>{vbCrLf}"
                        End If

                        If itemDicPort.LineCount >= itemLine.Number AndAlso itemDicPort.TableLayoutPanel.Controls.ContainsKey("LedPortLine" & itemLine.Number.ToString) Then
                            Dim tempLed As Led = CType(itemDicPort.TableLayoutPanel.Controls.Item("LedPortLine" & itemLine.Number.ToString), Led)
                            ' Линия дважды не должна использоваться
                            If tempLed.OffColor = ColorOffFromName(ledOffColor) Then
                                textMessage &= "Линия с данным номером уже используется в предыдущей конфигурации"
                                inStrbldResult.AppendLine(textMessage)
                                isCheckCyclogramCorrect = False
                            Else 'OK
                                tempLed.Caption = CStr(itemLine.Number)
                                tempLed.OffColor = ColorOffFromName(ledOffColor)
                                ToolTipForm.SetToolTip(tempLed, itemLine.Caption)
                            End If
                        Else
                            If itemPortlWriter.NumberModuleChassis = String.Empty Then
                                textMessage &= "Нет соответствующего устройства в компьютере."
                            Else
                                textMessage &= "Нет соответствующего модуля в корзине."
                            End If

                            inStrbldResult.AppendLine(textMessage)
                            isCheckCyclogramCorrect = False
                        End If
                    Next
                End If
            Next
        Next

        If isCheckCyclogramCorrect Then
            isCheckCyclogramCorrect = mPortWriters.ConfigureDigitalWriteTasks(inStrbldResult)
        End If

        Return isCheckCyclogramCorrect
    End Function

    ''' <summary>
    ''' Выделить шлейф при выделении узла в проводнике
    ''' </summary>
    ''' <param name="inKeyЦиклЗагрузки"></param>
    ''' <remarks></remarks>
    Private Sub SelectScatterPlot(inKeyЦиклЗагрузки As Integer)
        For Each itemPlot As ScatterPlot In ScatterGraphCycle.Plots
            If itemPlot.Tag IsNot Nothing Then
                If Integer.Parse(itemPlot.Tag.ToString) = inKeyЦиклЗагрузки Then
                    itemPlot.LineWidth = 3
                Else
                    itemPlot.LineWidth = 1
                End If
            End If
        Next
    End Sub
#End Region

#Region "Пуск Цикла"
    ''' <summary>
    ''' Пуск Цикла
    ''' </summary>
    ''' <param name="isChecked"></param>
    Private Sub LanchCycleProgram(isChecked As Boolean)
        TSButtonLanch.Enabled = Not isChecked
        TSMenuItemLanch.Enabled = Not isChecked
        TSButtonPause.Enabled = isChecked
        TSButtonPause.Checked = False
        TSButtonReady.Enabled = False
        TSButtonBackwards.Enabled = Not isChecked
        TSButtonForward.Enabled = Not isChecked
        TSComboBoxEngines.Enabled = Not isChecked
        SplitContainerCycle.Panel1.Enabled = Not isChecked
        TSButtonAlarm.Enabled = isChecked
        If UserNumericCycle IsNot Nothing Then UserNumericCycle.Enabled = Not isChecked

        If isChecked Then
            ' скрывать в любом случае
            SplitContainerCycle.Panel1Collapsed = isChecked
            currentTimeCycle = delayPeriod ' для сдвига курсора, чтобы первое значение не начиналочь с 0 отметки шкалы
            spendTime = 0
            If UserNumericCycle IsNot Nothing Then ViewCounterCycles(0)
            Dim message As String = $"Запуск программы цикла загрузки <{selectedTestProgram}> на исполнение..."
            messageMemo = message
            ShowMessageOnPanel(message)
            AddLogMessage(message, ColorForAlarm.Condition2)
            If isCheckCyclogramCorrect Then ShowProgressBar()
        Else
            If Not TSButtonShowTreeView.Checked Then
                ' показывать только тогда, когда пользователь не скрыл панель
                SplitContainerCycle.Panel1Collapsed = isChecked
            End If
        End If

        If UserNumericCycle.UserControlCycle.IsСontinuously Then
            repeatCounter = Short.MaxValue
        Else
            If UserNumericCycle IsNot Nothing Then
                repeatCounter = Convert.ToInt32(UserNumericCycle.Control.Controls("Panel3").Controls("NumUpDnЧислоПовторов").Text)
            Else
                repeatCounter = 1
            End If
        End If

        counterCycles = 0
        previousTime = Date.Now
        previousTimeAsDouble = 0

        If isCheckCyclogramCorrect Then
            IsLanchCycleProgram = isChecked
        Else
            IsLanchCycleProgram = False
        End If
    End Sub

    Private Sub ShowProgressBar()
        requestedCount = CInt(maxSumTimeCycle)
        TSProgressBar.Value = 0
        TSLabelRemain.Visible = True
        TSLabelTime.Visible = True
        TSProgressBar.Visible = True
        requestedCount = Fix(requestedCount)
        TSLabelTime.Text = GetTimeToString(TimeSpan.FromSeconds(requestedCount))
    End Sub

    Private Sub CompletedProgressBar()
        TSLabelRemain.Visible = False
        TSLabelTime.Visible = False
        TSProgressBar.Visible = False
    End Sub

    Private Sub ProgressChanged()
        If requestedCount <> 0 Then
            Dim ProgressPercentage As Integer = CInt(100 * currentTimeCycle / requestedCount)
            TSProgressBar.Value = If(ProgressPercentage > 100, 100, ProgressPercentage)
            Dim remainderSec As Integer = CInt(requestedCount - currentTimeCycle)
            TSLabelTime.Text = GetTimeToString(TimeSpan.FromSeconds(If(remainderSec < 0, 0, remainderSec)))
        End If
    End Sub
#End Region

    Private Sub TimerCycle_Tick(sender As Object, e As EventArgs) Handles TimerCycle.Tick
        SetPowers()
    End Sub

    'Private Sub mmTimer_Tick(sender As Object, e As EventArgs) Handles mmTimer.Tick
    '    ЗадатьУставки()
    'End Sub

    ''' <summary>
    ''' В событии таймера вычисляется положение курсора от премещения по циклограмме или по кнопкам управления
    ''' и в срезе курсора вычисляются уровни задатчиков для сетевых переменных
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPowers()
        ' таймер останавливать нельзя (можно IsLanchCycleProgram=False), т.к. необходимо обновлять последнее время (dtmPrevious = CurrentTime)
        currentTime = Date.Now

        If ((IsLanchCycleProgram OrElse isForward) AndAlso currentTimeCycle > 60.0) OrElse (isBackward AndAlso currentTimeCycle < maxSumTimeCycle - 60.0) Then
            currentTimeCycle = maxSumTimeCycle
            isForward = False
            isBackward = False
            ProgramCompleted()
            MessageBox.Show($"Демонстрационная версия системы управления загрузками позволяет запускать циклограммы длительностью не более 1 минуты.!{Environment.NewLine}Для получения полнофункциональной версии системы обратитесь к разработчику.",
                                        "Демонстрационная версия", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If IsLanchCycleProgram Then
            spendTime = currentTime.Subtract(previousTime).TotalSeconds

            If currentTimeCycle < maxSumTimeCycle Then
                If isCheckCyclogramCorrect AndAlso maxSumTimeCycle <> 0 Then
                    WritePowersInPorts(False)
                End If

                currentTimeCycle = Math.Round(currentTimeCycle + spendTime, 3)
                XyCursor1.XPosition = currentTimeCycle
            Else
                If repeatCounter > 1 Then
                    If counterCycles < repeatCounter Then
                        currentTimeCycle = spendTime
                        counterCycles += 1
                        If UserNumericCycle IsNot Nothing Then ViewCounterCycles(counterCycles)
                        previousTimeAsDouble = 0
                    End If

                    If counterCycles >= repeatCounter Then ProgramCompleted()
                Else
                    ProgramCompleted()
                End If
            End If
        ElseIf isBackward Then
            If isCheckCyclogramCorrect AndAlso maxSumTimeCycle <> 0 Then
                spendTime = currentTime.Subtract(previousTime).TotalSeconds

                If currentTimeCycle >= 0 Then
                    WritePowersInPorts(False)
                    currentTimeCycle = Math.Round(currentTimeCycle - spendTime, 3)
                End If

                XyCursor1.XPosition = currentTimeCycle
            End If
        ElseIf isForward Then
            If isCheckCyclogramCorrect AndAlso maxSumTimeCycle <> 0 Then
                spendTime = currentTime.Subtract(previousTime).TotalSeconds

                If currentTimeCycle <= maxSumTimeCycle Then
                    WritePowersInPorts(False)
                    currentTimeCycle = Math.Round(currentTimeCycle + spendTime, 3)
                End If

                XyCursor1.XPosition = currentTimeCycle
            End If
        End If

        previousTime = currentTime
        previousTimeAsDouble = currentTimeCycle
    End Sub

    ''' <summary>
    ''' Перекладка Завершена
    ''' </summary>
    Private Sub ProgramCompleted()
        TSButtonReady.Enabled = True
        TSButtonAlarm.Enabled = False
        TSButtonPause.Enabled = False

        IsLanchCycleProgram = False
        spendTime = 0
        SetAggregatesOnDefaultPower()
        Dim message As String = $"Программа цикла загрузки <{selectedTestProgram}> завершена."
        ShowMessageOnPanel(message)
        AddLogMessage(message, ColorForAlarm.Ok3)
    End Sub

    Private Function GetTimeToString(inTimeSpan As TimeSpan) As String
        Return $"[{inTimeSpan.Hours} час]:[{inTimeSpan.Minutes} мин]:[{inTimeSpan.Seconds} сек]"
    End Function

    Private Sub ViewCounterCycles(cycleCount As Integer)
        UserNumericCycle.Control.Controls("Panel3").Controls("lblВыполнено").Text = cycleCount.ToString
    End Sub

    ''' <summary>
    ''' В событии сбора или курсора определить время.
    ''' Только ручное перемещение курсора.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub XyCursor1_AfterMove(sender As Object, e As AfterMoveXYCursorEventArgs) Handles XyCursor1.AfterMove
        If IsLanchCycleProgram = False AndAlso isForward = False AndAlso isBackward = False Then
            If isCheckCyclogramCorrect AndAlso maxSumTimeCycle <> 0 Then
                currentTimeCycle = XyCursor1.XPosition

                If currentTimeCycle >= 0 AndAlso currentTimeCycle <= maxSumTimeCycle Then
                    ' исполнить загрузки надо в любом случае, чтобы может быть снять аварию
                    WritePowersInPorts(False)
                End If
            End If
        End If

        TSTextBoxCurrentTime.Text = GetTimeToString(TimeSpan.FromSeconds(currentTimeCycle))
        ProgressChanged()
    End Sub

    ''' <summary>
    ''' В цикле по циклограммам => по устройствам определить для каждого устройства величину.
    ''' В цикле по устройствам исполнить эту величину и отобразить на индикаторах.
    ''' В случае аварии исполнение циклограммы приостанавливается.
    ''' </summary>
    ''' <param name="isDefaul"></param>
    ''' <remarks></remarks>
    Private Sub WritePowersInPorts(ByVal isDefaul As Boolean)
        Dim isNeedUpdateControlsOfPowers As Boolean ' обновить индикаторы значений мощностей

        mPortWriters.SetAllLineByPortsOnZero()
        mAggregates.SetAllLineByPortsOnCurrentPower(isDefaul, currentTimeCycle, mPortWriters)
        isNeedUpdateControlsOfPowers = UpdateLedOnTableLayoutPanel()

        If isDefaul Then
            ' установить все линии в ноль
            isNeedUpdateControlsOfPowers = True
            Dim message As String = "Установлены все линии в ноль"
            ShowMessageOnPanel(message)
            AddLogMessage(message, ColorForAlarm.PowerDown4)
        End If

        If isNeedUpdateControlsOfPowers Then
            UpdateListViewAlarms()
            UpdateListViewPowerValue()
            UpdateVisualControls()
        End If

        mPortWriters.WritePortsMultiLine()
    End Sub

    'Private syncPointDoMonitor As Integer = 0 ' для синхронизации
    'Dim sync As Integer = Interlocked.CompareExchange(syncPointDoMonitor, 1, 0)
    'If sync = 0 Then
    '    syncPointDoMonitor = 0 ' освободить
    'End If

    ''' <summary>
    ''' Обновить индикаторы линий портов в соответствии с логческими уровнями.
    ''' </summary>
    ''' <returns></returns>
    Private Function UpdateLedOnTableLayoutPanel() As Boolean
        Dim isNeedUpdateControlsOfPowers As Boolean ' обновить индикаторы значений мощностей

        For Each itemPortlWriter As PortWriter In mPortWriters
            ' поиск по контролам
            For Each itemDicPort As LedPort In LedPorts.Values
                If itemDicPort.DeviceName = itemPortlWriter.ToString Then
                    Dim valPort As Long = 0
                    For Each itemLine As Line In itemPortlWriter.LinesToArray 'Dev0/port1/line0
                        Dim tempLed As Led = CType(itemDicPort.TableLayoutPanel.Controls.Item("LedPortLine" & itemLine.Number.ToString), Led)

                        If itemLine.IsEnabled Then ' установить линию в единицу
                            If tempLed.Value <> True Then
                                tempLed.Value = True
                                ToolTipForm.SetToolTip(tempLed, itemLine.Caption)
                            End If
                        Else ' установить линиию в ноль
                            If tempLed.Value <> False Then
                                tempLed.Value = False
                                ToolTipForm.SetToolTip(tempLed, "Выключен")
                            End If
                        End If

                        If itemLine.IsEnabled = True Then
                            valPort += 1L << itemLine.Number 'Номер Бита
                        End If
                    Next

                    ' вывести на индикаторы двоичное значение включенных бит
                    CType(itemDicPort.TableLayoutPanel.Controls.Item("Panel" & itemPortlWriter.ToString).Controls.Item("NumericEditPort" & itemPortlWriter.ToString), NumericEdit).Value = valPort 'String.Format("0x{0:X}", valPort)
                    itemPortlWriter.PortValueBit = valPort

                    If itemPortlWriter.IsNeedUpdate Then
                        ' значение индикатора изменилось, значит вывести в лист событий
                        'AlarmsQueue.Enqueue($"Порт:<{itemPortlWriter.ToString}> изменил значение на:<{valPort}>")
                        isNeedUpdateControlsOfPowers = True
                    End If

                    Exit For
                End If
            Next
        Next

        Return isNeedUpdateControlsOfPowers
    End Function

    ''' <summary>
    ''' Вывести сообщение в панели статуса.
    ''' </summary>
    ''' <param name="message"></param>
    Private Sub ShowMessageOnPanel(ByVal message As String)
        TSSLabelStatus.Text = message
        'AddLogMessage($"InformationMessage. Процедура: <{NameOf(TestAlarm)}>", ColorForAlarm.InformationMessage)
    End Sub

#Region "ListViewAlarms"
    '''' <summary>
    '''' Для предотвращения преждевременной обработки событий
    '''' </summary>
    'Private isCreatedForm As Boolean
    Private Const NUMBER_COLUMN As String = "Номер"
    Private Const MESSAGE_COLUMN As String = "Сообщение"
    Private Const DATE_COLUMN As String = "Дата"
    Private Const TIME_COLUMN As String = "Время"
    Private Const LIMIT_LIST_COUNT As Integer = 200

    Private Enum ColorForAlarm
        Information0
        Information1
        Condition2
        Ok3
        PowerDown4
        PowerUp5
        Criterion6
        AlarmMessage7
    End Enum

    ''' <summary>
    ''' Настройка ListView.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateListViewAlarms()
        ' Установить вид просмотра по умолчанию.
        ListViewAlarms.View = View.Details
        ' Разрешить пользователю редактировать текст.
        'ListViewAlarms.LabelEdit = True
        ' Разрешить пользователю реорганизовывать колонки.
        ListViewAlarms.AllowColumnReorder = True
        ' Показать check boxes.
        'ListViewAlarms.CheckBoxes = True
        ' Выделять item и subitems в режиме выделения.
        'ListViewAlarms.FullRowSelect = True
        ' Показать сетку линий.
        ListViewAlarms.GridLines = True
        ' Сортировать элементы листа по возрастанию.
        'ListViewAlarms.Sorting = SortOrder.Ascending

        ' Назначить ImageList для ListView.
        ListViewAlarms.LargeImageList = ImageListMessage
        ListViewAlarms.SmallImageList = ImageListMessage

        ' Создать колонки для элементов и подэлементов.
        ' При установке в -2 индикация в auto-size.
        ListViewAlarms.Columns.Add(NUMBER_COLUMN, NUMBER_COLUMN, 90, HorizontalAlignment.Center, NUMBER_COLUMN)
        ListViewAlarms.Columns.Add(MESSAGE_COLUMN, MESSAGE_COLUMN, -2, HorizontalAlignment.Left, MESSAGE_COLUMN)
        ListViewAlarms.Columns.Add(DATE_COLUMN, DATE_COLUMN, 90, HorizontalAlignment.Center, DATE_COLUMN)
        ListViewAlarms.Columns.Add(TIME_COLUMN, TIME_COLUMN, 90, HorizontalAlignment.Center, TIME_COLUMN)

        ' Добавить элементы в ListView.
        'ListViewAlarm.Items.AddRange(New ListViewItem() {item1, item2, item3})
        'AddToListView("AlertUp", ColorForAlarm.AlertUp)

        ' Создать 2 ImageList программно.
        'Dim imageListSmall As New ImageList()
        'Dim imageListLarge As New ImageList()

        ' Инициализировать ImageList из bitmaps.
        'imageListSmall.Images.Add(Bitmap.FromFile("C:\MySmallImage1.bmp"))
        'imageListSmall.Images.Add(Bitmap.FromFile("C:\MySmallImage2.bmp"))

        ' Добавить ListView коллекцию контролов.
        'Controls.Add(ListViewAlarms)
    End Sub

    Private Sub ListViewAlarm_Resize(sender As Object, e As EventArgs) Handles ListViewAlarms.Resize
        If Me.IsHandleCreated Then ' AndAlso isCreatedForm
            If ListViewAlarms.Columns.Count > 0 Then
                ListViewAlarms.Columns(NUMBER_COLUMN).Width = 90
                ListViewAlarms.Columns(DATE_COLUMN).Width = 90
                ListViewAlarms.Columns(TIME_COLUMN).Width = 90
                ListViewAlarms.Columns(MESSAGE_COLUMN).Width = ListViewAlarms.Width - (ListViewAlarms.Columns(NUMBER_COLUMN).Width + ListViewAlarms.Columns(DATE_COLUMN).Width + ListViewAlarms.Columns(TIME_COLUMN).Width)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Возвращает значение Color в зависимости от типа загрузки.
    ''' </summary>
    Private Shared Sub GetAlarmColor(ByRef subItem As ListViewItem.ListViewSubItem, ByVal alarm As ColorForAlarm)
        subItem.Font = New Font("Arial", 10, FontStyle.Bold) 'System.Drawing.FontStyle.Italic)

        Select Case alarm
            Case ColorForAlarm.Information0, ColorForAlarm.Information1
                subItem.ForeColor = Color.Blue
                subItem.BackColor = Color.White
                subItem.Font = New Font("Arial", 8, FontStyle.Regular)
                Exit Select
            Case ColorForAlarm.Condition2
                subItem.ForeColor = Color.Blue
                subItem.BackColor = Color.LightSteelBlue
                subItem.Font = New Font("Arial", 8, FontStyle.Regular)
                Exit Select
            Case ColorForAlarm.Ok3
                subItem.ForeColor = Color.Green
                subItem.BackColor = Color.LightGreen
                Exit Select
            Case ColorForAlarm.PowerDown4
                subItem.ForeColor = Color.Olive
                subItem.BackColor = Color.PaleGoldenrod
                Exit Select
            Case ColorForAlarm.PowerUp5
                subItem.ForeColor = Color.Red
                subItem.BackColor = Color.MistyRose
                Exit Select
            Case ColorForAlarm.Criterion6, ColorForAlarm.AlarmMessage7
                subItem.ForeColor = Color.Black
                subItem.BackColor = Color.OrangeRed
                Exit Select
        End Select
    End Sub

    ''' <summary>
    ''' Эта процедура добавляет новое сообщение 
    ''' в ListView и задает текст, размер и цвет.
    ''' </summary>
    Private Sub AddToListView(ByVal message As String, ByVal alarm As ColorForAlarm)
        'If InvokeRequired Then
        '    Invoke(New MethodInvoker(Sub() AddToListView(message, alarm)))
        'Else

        'Dim item1 As New ListViewItem("item1", 0)
        ''item1.Checked = True
        'item1.SubItems.Add("1")

        'ListViewAlarm.Items(0).UseItemStyleForSubItems = False
        'ListViewAlarm.Items(0).SubItems.Add("Авария", Color.Pink, Color.Yellow, Font)

        ListViewAlarms.BeginUpdate()

        Do While ListViewAlarms.Items.Count > LIMIT_LIST_COUNT
            ListViewAlarms.Items.RemoveAt(0)
        Loop

        Dim listViewItem As New ListViewItem()
        Dim listViewSubItemДата As ListViewItem.ListViewSubItem = New ListViewItem.ListViewSubItem() With {.Text = Date.Now.ToShortDateString} '= listViewItem.SubItems.Add("1")
        Dim listViewSubItemВремя As ListViewItem.ListViewSubItem = New ListViewItem.ListViewSubItem() With {.Text = Date.Now.ToLongTimeString}
        Dim listViewSubItemСообщение As ListViewItem.ListViewSubItem = New ListViewItem.ListViewSubItem() With {.Text = message}

        listViewItem.ImageKey = alarm.ToString
        listViewItem.UseItemStyleForSubItems = False

        If ListViewAlarms.Items.Count = 0 Then
            listViewItem.Text = "1"
        Else
            listViewItem.Text = CStr(Integer.Parse(ListViewAlarms.Items(ListViewAlarms.Items.Count - 1).Text) + 1)
        End If

        'listViewItem.BackColor = GetBackColor(ColorForAlarm.Normal)
        GetAlarmColor(listViewSubItemДата, ColorForAlarm.Information1)
        GetAlarmColor(listViewSubItemВремя, ColorForAlarm.Information1)
        GetAlarmColor(listViewSubItemСообщение, alarm)

        listViewItem.SubItems.Add(listViewSubItemСообщение)
        listViewItem.SubItems.Add(listViewSubItemДата)
        listViewItem.SubItems.Add(listViewSubItemВремя)
        ListViewAlarms.Items.Add(listViewItem)
        listViewItem.EnsureVisible()
        ListViewAlarms.EndUpdate()
        'End If
    End Sub

    ''' <summary>
    ''' Окрашивание индикаторов в различные цвета в зависимости от уровня
    ''' вывод предупреждений в лист сообщений.
    ''' Поддержка вывода из другого потока.
    ''' </summary>
    ''' <param name="message"></param>
    ''' <param name="stateColor"></param>
    ''' <remarks></remarks>
    Private Sub AddLogMessage(message As String, ByVal stateColor As ColorForAlarm)
        If InvokeRequired Then
            Invoke(New MethodInvoker(Sub() AddLogMessage(message, stateColor)))
        Else
            AddToListView(message, stateColor)
            'ShowMessageOnPanel(message)
        End If
    End Sub

    ' ''' <summary>
    ' ''' Очистить панель статуса.
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub ClearPanel()
    '    tsStatus.Text = ""
    'End Sub

    'Private Sub TestAlarm()
    '    AddLogMessage($"<{NameOf(ColorForAlarm.Information0)}>", ColorForAlarm.Information0)
    '    AddLogMessage($"<{NameOf(ColorForAlarm.Information1)}>", ColorForAlarm.Information1)
    '    AddLogMessage($"<{NameOf(ColorForAlarm.Condition2)}>", ColorForAlarm.Condition2)
    '    AddLogMessage($"<{NameOf(ColorForAlarm.Ok3)}>", ColorForAlarm.Ok3)
    '    AddLogMessage($"<{NameOf(ColorForAlarm.PowerDown4)}>", ColorForAlarm.PowerDown4)
    '    AddLogMessage($"<{NameOf(ColorForAlarm.PowerUp5)}>", ColorForAlarm.PowerUp5)
    '    AddLogMessage($"<{NameOf(ColorForAlarm.Criterion6)}>", ColorForAlarm.Criterion6)
    '    AddLogMessage($"<{NameOf(ColorForAlarm.AlarmMessage7)}>", ColorForAlarm.AlarmMessage7)
    'End Sub
#End Region

#Region "Панели индикаторов с цифровым полем для измеренных и рачётных параметров"

#Region "Обновить индикаторы"
    ''' <summary>
    ''' Обновить значения ListViewAlarms на форме.
    ''' </summary>
    Private Sub UpdateListViewAlarms()
        For Each itemAggregate As AggregateCycle In mAggregates
            If itemAggregate.IsNeedUpdate Then
                AddLogMessage($"Для устройства <{itemAggregate.NameAggregate}> установлена <{Convert.ToString(Math.Round(itemAggregate.CurrentPower, 1))}> единиц загрузки",
                              If(itemAggregate.IsPowerUp, ColorForAlarm.PowerUp5, ColorForAlarm.PowerDown4))
            End If
        Next
    End Sub

    ''' <summary>
    ''' Обновить значения ListViewPowerValue на форме.
    ''' </summary>
    Private Sub UpdateListViewPowerValue()
        Dim J As Integer
        ' установить текущее значение мощности для агрегатов
        For Each itemAggregate As AggregateCycle In mAggregates
            With ListViewPowerValue ' обновить лист
                .BeginUpdate()
                .Items(J).SubItems(1).Text = Convert.ToString(Math.Round(itemAggregate.CurrentPower, 1))
                .EndUpdate()
            End With
            J += 1
        Next
    End Sub

    ''' <summary>
    ''' Обновить значения индикаторов на форме.
    ''' Индикатор выведет самое первое неправильное значение.
    ''' Остальные неправильные не будут выведены для предотвращения мерцания.
    ''' </summary>
    Private Sub UpdateVisualControls()
        If syncPoint = 1 Then Exit Sub

        Dim sync As Integer = Interlocked.CompareExchange(syncPoint, 1, 0)
        If sync = 0 Then
            Dim isShowError As Boolean = False

            For Each itemAggregate As AggregateCycle In mAggregates
                CheckParameterInRange(itemAggregate, isShowError)
            Next

            ' ошибок не было, значить погасить индикатор, если он зажегся ло этого
            TextError.Visible = isShowError
            CheckAlarmLimit()
            syncPoint = 0  ' освободить
        End If
    End Sub

    ''' <summary>
    ''' Проверка Параметр В Диапазоне
    ''' </summary>
    ''' <param name="inAggregate"></param>
    ''' <param name="isShowError"></param>
    Private Sub CheckParameterInRange(inAggregate As AggregateCycle, ByRef isShowError As Boolean)
        ' NaN == NaN: False применять нельзя
        ' Double.NaN.Equals(Double.NaN) - можно NaN.Equals(NaN): True
        ' Double.IsNaN(Double.NaN))- можно IsNaN: True

        Dim inCurrentValue As Double = Convert.ToDouble(inAggregate.CurrentPower)

        If Double.IsNaN(inCurrentValue) OrElse Double.IsNegativeInfinity(inCurrentValue) OrElse Double.IsPositiveInfinity(inCurrentValue) Then
            If Not isShowError Then
                ShowError($"Ошибка расчета: параметр <{inAggregate.NameAggregate}> не вычислен!")
                isShowError = True
            End If

            inCurrentValue = 0.0
        End If

        With MainFomMdiParent.MyClassCalculation.CalculatedParams
            .CalcDictionary(inAggregate.NameAggregate).CalculatedValue = Math.Round(inCurrentValue, 1)
            .CalcDictionary(inAggregate.NameAggregate).UpdateControls()
        End With
    End Sub

    ''' <summary>
    ''' Проверка на настроенные аварийные допуски параметров в таблице измеренных параметров.
    ''' Вывод собщения в статусной строке. Аварийный индикатор не нужен.
    ''' </summary>
    Private Sub CheckAlarmLimit()
        ShowMessageOnPanel(messageMemo)

        With MainFomMdiParent.Manager
            Dim valueT As Double

            For Each rowИзмеренныйПараметр As BaseFormDataSet.ИзмеренныеПараметрыRow In .MeasurementDataTable.Rows
                valueT = rowИзмеренныйПараметр.ЗначениеВСИ
                If valueT < rowИзмеренныйПараметр.ДопускМинимум OrElse valueT > rowИзмеренныйПараметр.ДопускМаксимум Then
                    ' вывести сообщение об обрыве
                    ShowMessageOnPanel($"Значение параметра {rowИзмеренныйПараметр.ИмяПараметра} = {Format(valueT, "##,##0.00")} вне допуска ({Format(rowИзмеренныйПараметр.ДопускМинимум, "F")} : {Format(rowИзмеренныйПараметр.ДопускМаксимум, "F")})!")
                End If
            Next

            For Each rowРасчетныйПараметр As BaseFormDataSet.РасчетныеПараметрыRow In .CalculatedDataTable.Rows
                valueT = rowРасчетныйПараметр.ВычисленноеЗначениеВСИ
                If valueT < rowРасчетныйПараметр.ДопускМинимум OrElse valueT > rowРасчетныйПараметр.ДопускМаксимум Then
                    ' вывести сообщение об обрыве
                    ShowMessageOnPanel($"Значение параметра {rowРасчетныйПараметр.ИмяПараметра} = {Format(valueT, "##,##0.00")} вне допуска ({Format(rowРасчетныйПараметр.ДопускМинимум, "F")} : {Format(rowРасчетныйПараметр.ДопускМаксимум, "F")})!")
                End If
            Next
        End With
    End Sub

    Public Sub ShowError(message As String)
        TextError.Text = message
        TextError.Visible = True
    End Sub

    '''' <summary>
    '''' Обновить коллекцию расчётных параметров значениями текущей вычисленной мощности.
    '''' </summary>
    'Public Sub GetChargeParametersFromCycle()
    '    With MainFomMdiParent.MyClassCalculation.CalculatedParams
    '        For Each itemAggregate As AggregateCycle In mAggregates
    '            .CalcDictionary(itemAggregate.NameAggregate).CalculatedValue = Math.Round(itemAggregate.CurrentPower, 1)
    '        Next
    '    End With
    'End Sub
#End Region

    Enum Side
        Left
        Right
    End Enum

    Enum MeterControl
        Tank
        MeterL
        MeterR
        Gauge
        Thermometer
        Slide
    End Enum

    ''' <summary>
    ''' Динамическое заполнение панели контроллами измеренных параметров.
    ''' </summary>
    Public Sub PopulateInputParameterToToolStrip()
        With MainFomMdiParent.MyClassCalculation
            If .InputParams.Count > 0 Then
                Me.ToolStripDevice.SuspendLayout()
                Me.SuspendLayout()

                For Each itemInputParameter As InputParameter In .InputParams
                    AddInputParameterToToolStrip(itemInputParameter.Name)
                Next

                Me.ToolStripDevice.ResumeLayout(False)
                Me.ToolStripDevice.PerformLayout()
                Me.ResumeLayout(False)
            End If
        End With
    End Sub

    ''' <summary>
    ''' Добавление в панель индикаторы с цифровым полем для измеренных параметров.
    ''' Связывание измеренного параметра с этими контролами.
    ''' </summary>
    ''' <param name="inInputParameterName"></param>
    Private Sub AddInputParameterToToolStrip(inInputParameterName As String)
        Dim tsLabelInputParameter As New ToolStripLabel With {
            .Name = "TSLabel" & inInputParameterName,
            .Size = New Size(135, 22),
            .Text = inInputParameterName & ":"
        }

        Dim tsTextBoxInputParameter As New ToolStripTextBox With {
            .BackColor = SystemColors.Control,
            .Font = New Font("Segoe UI", 9.0!, FontStyle.Bold, GraphicsUnit.Point, CType(204, Byte)),
            .ForeColor = Color.Blue,
            .Name = "TSTextBox" & inInputParameterName,
            .ReadOnly = True,
            .Size = New Size(50, 25),
            .Text = "0.00",
            .TextBoxTextAlign = HorizontalAlignment.Right
        }

        Dim tsSeparatorInputParameter As New ToolStripSeparator With {
            .Name = "TSSeparator" & inInputParameterName,
            .Size = New Size(6, 25)
        }

        Me.ToolStripDevice.Items.AddRange(New ToolStripItem() {tsLabelInputParameter, tsTextBoxInputParameter, tsSeparatorInputParameter})
        MainFomMdiParent.MyClassCalculation.InputParams.BindingWithControls(inInputParameterName, tsTextBoxInputParameter)
    End Sub

    Private syncPoint As Integer = 0 ' для синхронизации
    ''' <summary>
    ''' Динамическое заполнение панели контроллами расчётных параметров
    ''' </summary>
    Public Sub PopulateControlToFlowLayoutPanel()
        If syncPoint = 1 Then Exit Sub

        Dim sync As Integer = Interlocked.CompareExchange(syncPoint, 1, 0)

        If sync = 0 Then
            ' Перед добавлением очистить коллекции
            UnBindingCalculatedParamsWithControls()
            Me.FlowLayoutPanelControls.SuspendLayout()

            For Each itemAggregate As AggregateCycle In mAggregates
                AddParameterPanel(CreateINumericPointer(itemAggregate.NameAggregate, GetAttributesChargeParameter(itemAggregate.NameAggregate, MainFomMdiParent.PathDBaseCycle)),
                                  CreateNumericEdit)
            Next

            Me.FlowLayoutPanelControls.ResumeLayout(False)
            SplitContainerGraph.SplitterDistance = SplitContainerGraph.SplitterDistance + 1
            SplitContainerGraph.SplitterDistance = SplitContainerGraph.SplitterDistance - 1
            'FlowLayoutPanelControlsResize()
            syncPoint = 0  ' освободить
        End If
    End Sub

    ''' <summary>
    ''' Отменить связи с индикаторами и цифровыми полями все элементы коллекции.
    ''' </summary>
    Private Sub UnBindingCalculatedParamsWithControls()
        MainFomMdiParent.MyClassCalculation.CalculatedParams.UnBindingAllControls()
        Me.FlowLayoutPanelControls.Controls.Clear()
        ControlsSize.Clear()
    End Sub

    ''' <summary>
    ''' Добавление панели с вложенным индикатором и цифровым полем в FlowLayoutPanelControls.
    ''' Связывание расчётного параметра с этими контролами.
    ''' </summary>
    ''' <param name="inTupleControlSize"></param>
    ''' <param name="inNumericEdit"></param>
    Private Sub AddParameterPanel(inTupleControlSize As Tuple(Of Control, Size), inNumericEdit As NumericEdit)
        Dim newPanel As New Panel()

        With newPanel
            .SuspendLayout()
            .BorderStyle = BorderStyle.Fixed3D
            '.Controls.Add(inTupleControlSize.numericPointer)
            .Controls.Add(inTupleControlSize.Item1)
            .Controls.Add(inNumericEdit)
            .Location = New Point(3, 3)
            .Name = "Panel" & FlowLayoutPanelControls.Controls.Count
            '.Size = inTupleControlSize.panelSize
            .Size = inTupleControlSize.Item2
            .ResumeLayout(False)
        End With

        Me.FlowLayoutPanelControls.Controls.Add(newPanel)

        MainFomMdiParent.MyClassCalculation.CalculatedParams.BindingWithControls(inTupleControlSize.Item1.Tag.ToString,
                                                                                 CType(inTupleControlSize.Item1, INumericPointer),
                                                                                 inNumericEdit)
        ControlsSize(newPanel) = newPanel.Size
    End Sub

    ''' <summary>
    ''' Фабрика создающая кортеж из контрола типа INumericPointer и размера для содержащей его панели.
    ''' </summary>
    ''' <param name="inParameter"></param>
    ''' <returns></returns>
    Private Function CreateINumericPointer(inNameAggregate As String, ByVal inParameter As AttributesChargeParameter) As Tuple(Of Control, Size)

        With inParameter
            Select Case .UnitOfMeasure
                Case "Давление"
                    Return New Tuple(Of Control, Size)(CreateTank(inNameAggregate, .Description, .RangeOfChangingValueMin, .AlarmValueMin, .AlarmValueMax, .RangeOfChangingValueMax), GetSize(MeterControl.Tank))
                Case "Обороты"
                    Return New Tuple(Of Control, Size)(CreateGauge(inNameAggregate, .Description, .RangeOfChangingValueMin, .AlarmValueMin, .AlarmValueMax, .RangeOfChangingValueMax), GetSize(MeterControl.Gauge))
                Case "Ток"
                    Return New Tuple(Of Control, Size)(CreateMeter(inNameAggregate, .Description, Side.Left, .RangeOfChangingValueMin, .AlarmValueMin, .AlarmValueMax, .RangeOfChangingValueMax), GetSize(MeterControl.MeterL))
                Case "Вибрация"
                    Return New Tuple(Of Control, Size)(CreateMeter(inNameAggregate, .Description, Side.Right, .RangeOfChangingValueMin, .AlarmValueMin, .AlarmValueMax, .RangeOfChangingValueMax), GetSize(MeterControl.MeterR))
                Case "Температура"
                    Return New Tuple(Of Control, Size)(CreateThermometer(inNameAggregate, .Description, .RangeOfChangingValueMin, .AlarmValueMin, .AlarmValueMax, .RangeOfChangingValueMax), GetSize(MeterControl.Thermometer))
                Case "Расход"
                    Return New Tuple(Of Control, Size)(CreateSlide(inNameAggregate, .Description, .RangeOfChangingValueMin, .AlarmValueMin, .AlarmValueMax, .RangeOfChangingValueMax), GetSize(MeterControl.Slide))
                Case Else
                    ' Обороты
                    Return New Tuple(Of Control, Size)(CreateGauge(inNameAggregate, .Description, .RangeOfChangingValueMin, .AlarmValueMin, .AlarmValueMax, .RangeOfChangingValueMax), GetSize(MeterControl.Gauge))
            End Select
        End With
    End Function

    ''' <summary>
    ''' Определить размер панели содержащий индикатор в зависимости от его типа.
    ''' </summary>
    ''' <param name="inMeterControl"></param>
    ''' <returns></returns>
    Private Function GetSize(inMeterControl As MeterControl) As Size
        Select Case inMeterControl
            Case MeterControl.Tank, MeterControl.Slide
                Return New Size(80, 200)
            Case MeterControl.MeterL, MeterControl.MeterR
                Return New Size(120, 200)
            Case MeterControl.Gauge
                Return New Size(200, 200)
            Case MeterControl.Thermometer
                Return New Size(90, 200)
        End Select
    End Function

#Region "Создание индикаторов"
    'Private Shared Sub Swap(Of T)(ByRef lhs As T, ByRef rhs As T)
    '    Dim temp As T
    '    temp = lhs
    '    lhs = rhs
    '    rhs = temp
    'End Sub

    ''' <summary>
    ''' Заливка пределов диапазона.
    ''' </summary>
    ''' <param name="inMin"></param>
    ''' <param name="inLower"></param>
    ''' <param name="inAbove"></param>
    ''' <param name="inMax"></param>
    ''' <returns></returns>
    Private Function CreateScaleRangeFill(inMin As Double, inLower As Double, inAbove As Double, inMax As Double) As ScaleRangeFill()
        If inMin = inMax Then
            inMin = 0
            inMax = 1
        End If

        Dim range As Double = Math.Abs(inMax - inMin)

        If inLower = 0 AndAlso inAbove = 0 Then
            inLower = inMin + 0.1 * range
            inAbove = inMax - 0.1 * range
        End If

        If inMin >= inLower Then inLower = inMin + 0.1 * range
        If inAbove >= inMax Then inAbove = inMax - 0.1 * range

        'If inLower > inAbove Then Swap(Of Double)(inLower, inAbove)

        Dim ScaleRangeFillLower As New ScaleRangeFill With {
            .Range = New Range(inMin, inLower),
            .Style = ScaleRangeFillStyle.CreateGradientStyle(Color.Red, Color.Yellow, 0.5R)
        }
        Dim ScaleRangeFillNormal As New ScaleRangeFill With {
            .Range = New Range(inLower, inAbove),
            .Style = ScaleRangeFillStyle.CreateSolidStyle(Color.Lime)
        }
        Dim ScaleRangeFillAbove As New ScaleRangeFill With {
            .Range = New Range(inAbove, inMax),
            .Style = ScaleRangeFillStyle.CreateGradientStyle(Color.Yellow, Color.Red, 0.5R)
        }

        Return New ScaleRangeFill() {ScaleRangeFillLower, ScaleRangeFillNormal, ScaleRangeFillAbove}
    End Function

    Private Function CreateTank(inCaption As String, inDescription As String,
                                 inMin As Double, inLower As Double, inAbove As Double, inMax As Double) As Tank
        Dim newTank As New Tank()

        CType(newTank, System.ComponentModel.ISupportInitialize).BeginInit()
        With newTank
            .Caption = inCaption
            .Tag = inCaption
            .Dock = DockStyle.Fill
            .FillStyle = FillStyle.HorizontalGradient
            .ForeColor = Color.Lime
            .Location = New Point(0, 0)
            .MajorDivisions.TickColor = Color.Blue
            .MinorDivisions.TickColor = Color.Blue
            .Name = "Tank"
            .OutOfRangeMode = NumericOutOfRangeMode.CoerceToRange
            .Range = New Range(inMin, inMax)
            .RangeFills.AddRange(CreateScaleRangeFill(inMin, inLower, inAbove, inMax))
            .Size = New Size(76, 178)
            .Value = 0.0R
        End With
        CType(newTank, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolTipForm.SetToolTip(newTank, inDescription)

        Return newTank
    End Function

    Private Function CreateMeter(inCaption As String, inDescription As String, inSide As Side,
                                 inMin As Double, inLower As Double, inAbove As Double, inMax As Double) As Meter
        Dim newMeter As New Meter()

        CType(newMeter, System.ComponentModel.ISupportInitialize).BeginInit()
        With newMeter
            .AutoDivisionSpacing = False
            .Caption = inCaption
            .Tag = inCaption
            .DialColor = Color.Gray
            .Dock = DockStyle.Fill
            .ForeColor = Color.Lime
            .Location = New Point(0, 0)
            .MajorDivisions.Interval = 0.1R
            .MajorDivisions.TickColor = Color.Blue
            .MajorDivisions.TickLength = 8.0!
            .MinorDivisions.Interval = 0.05R
            .MinorDivisions.TickColor = Color.Blue
            .MinorDivisions.TickLength = 6.0!
            .Name = "Meter"
            .OutOfRangeMode = NumericOutOfRangeMode.CoerceToRange
            .PointerColor = Color.DarkGreen
            .Range = New Range(inMin, inMax)
            .RangeFills.AddRange(CreateScaleRangeFill(inMin, inLower, inAbove, inMax))

            Select Case inSide
                Case Side.Right
                    .ScaleArc = New Arc(300.0!, 125.0!)
                Case Side.Left
                    .ScaleArc = New Arc(225.0!, -90.0!)
            End Select

            .Size = New Size(116, 178)
            .SpindleColor = Color.DimGray
            .Value = 0.0R
        End With
        CType(newMeter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolTipForm.SetToolTip(newMeter, inDescription)

        Return newMeter
    End Function

    Private Function CreateGauge(inCaption As String, inDescription As String,
                                 inMin As Double, inLower As Double, inAbove As Double, inMax As Double) As Gauge
        Dim newGauge As New Gauge()

        CType(newGauge, System.ComponentModel.ISupportInitialize).BeginInit()
        With newGauge
            .AutoDivisionSpacing = False
            .Caption = inCaption
            .Tag = inCaption
            .DialColor = Color.Black
            .Dock = DockStyle.Fill
            .ForeColor = Color.Lime
            .Location = New Point(0, 0)
            .MajorDivisions.Interval = 0.2R
            .MajorDivisions.LabelFormat = New FormatString(FormatStringMode.Numeric, "0.0")
            .MajorDivisions.TickColor = Color.Blue
            .MinorDivisions.Interval = 0.05R
            .MinorDivisions.TickColor = Color.Blue
            .Name = "Gauge"
            .OutOfRangeMode = NumericOutOfRangeMode.CoerceToRange
            .PointerColor = Color.Lime
            .Range = New Range(inMin, inMax)
            .RangeFills.AddRange(CreateScaleRangeFill(inMin, inLower, inAbove, inMax))
            .ScaleArc = New Arc(230.0!, -280.0!)
            .Size = New Size(196, 178)
            .ToolTipFormat = New FormatString(FormatStringMode.Numeric, "0.0")
            .Value = 0.0R
        End With
        CType(newGauge, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolTipForm.SetToolTip(newGauge, inDescription)

        Return newGauge
    End Function

    Private Function CreateThermometer(inCaption As String, inDescription As String,
                                 inMin As Double, inLower As Double, inAbove As Double, inMax As Double) As Thermometer
        Dim newThermometer As New Thermometer()

        CType(newThermometer, System.ComponentModel.ISupportInitialize).BeginInit()
        With newThermometer
            .Caption = inCaption
            .Tag = inCaption
            .Dock = DockStyle.Fill
            .ForeColor = Color.Lime
            .Location = New Point(0, 0)
            .MajorDivisions.TickColor = Color.Blue
            .MinorDivisions.TickColor = Color.Blue
            .Name = "Thermometer"
            .OutOfRangeMode = NumericOutOfRangeMode.CoerceToRange
            .Range = New Range(inMin, inMax)
            .RangeFills.AddRange(CreateScaleRangeFill(inMin, inLower, inAbove, inMax))
            .Size = New Size(86, 178)
            .Value = 0.0R
        End With
        CType(newThermometer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolTipForm.SetToolTip(newThermometer, inDescription)

        Return newThermometer
    End Function

    Private Function CreateSlide(inCaption As String, inDescription As String,
                                 inMin As Double, inLower As Double, inAbove As Double, inMax As Double) As Slide
        Dim newSlide As New Slide()

        CType(newSlide, System.ComponentModel.ISupportInitialize).BeginInit()
        With newSlide
            .Caption = inCaption
            .Tag = inCaption
            .Dock = DockStyle.Fill
            .FillBackColor = Color.DarkOrange
            .FillMode = NumericFillMode.ToMaximum
            .ForeColor = Color.Lime
            .InteractionMode = LinearNumericPointerInteractionModes.Indicator
            .Location = New Point(0, 0)
            .MajorDivisions.Interval = 0.5R
            .MajorDivisions.TickColor = Color.Blue
            .MinorDivisions.Interval = 0.1R
            .MinorDivisions.TickColor = Color.Blue
            .Name = "Slide"
            .OutOfRangeMode = NumericOutOfRangeMode.CoerceToRange
            .PointerColor = Color.DarkRed
            .Range = New Range(inMin, inMax)
            .RangeFills.AddRange(CreateScaleRangeFill(inMin, inLower, inAbove, inMax))
            .Size = New Size(76, 178)
            .Value = 0.0R
        End With
        CType(newSlide, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolTipForm.SetToolTip(newSlide, inDescription)

        Return newSlide
    End Function

    Private Function CreateNumericEdit() As NumericEdit
        Dim newNumericEdit As New NumericEdit()

        CType(newNumericEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        With newNumericEdit
            .BackColor = Color.DimGray
            .BorderStyle = BorderStyle.None
            .Dock = DockStyle.Bottom
            .Font = New Font("Microsoft Sans Serif", 9.75!, FontStyle.Bold, GraphicsUnit.Point, CType(204, Byte))
            .ForeColor = Color.Yellow
            .FormatMode = NumericFormatMode.CreateSimpleDoubleMode(2)
            .InteractionMode = NumericEditInteractionModes.Indicator
            .Location = New Point(0, 178)
            .Name = "NumericEdit"
            .OutOfRangeMode = NumericOutOfRangeMode.CoerceToRange
            .Size = New Size(76, 18)
            .TextAlign = HorizontalAlignment.Center
            .UpDownAlign = LeftRightAlignment.Left
        End With

        CType(newNumericEdit, System.ComponentModel.ISupportInitialize).EndInit()

        Return newNumericEdit
    End Function

#End Region

    Private Sub FlowLayoutPanelControls_Resize(sender As Object, e As EventArgs) Handles FlowLayoutPanelControls.Resize
        FlowLayoutPanelControlsResize()
    End Sub

    Private Sub FlowLayoutPanelControlsResize()
        If Me.IsHandleCreated AndAlso ControlsSize.Count > 0 Then
            Const modelForScalling As Double = 500 '660.0
            Dim factor As Double = Math.Sqrt(FlowLayoutPanelControls.Width * FlowLayoutPanelControls.Height)

            If factor > modelForScalling Then
                Dim scalling As Double = factor / modelForScalling

                For Each itemControl As Control In FlowLayoutPanelControls.Controls
                    itemControl.Width = Convert.ToInt32(ControlsSize(itemControl).Width * scalling)
                    itemControl.Height = Convert.ToInt32(ControlsSize(itemControl).Height * scalling)
                Next
            Else
                For Each itemControl As Control In FlowLayoutPanelControls.Controls
                    itemControl.Size = ControlsSize(itemControl)
                Next
            End If
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Подключить событие уведомляющее об изменении объекта
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <remarks></remarks>
    Private Sub WatchObject(obj As Object)
        Dim watchableObj As INotifyPropertyChanged = TryCast(obj, INotifyPropertyChanged)

        If watchableObj IsNot Nothing Then
            AddHandler watchableObj.PropertyChanged, New PropertyChangedEventHandler(AddressOf Data_PropertyChanged)
        End If
    End Sub

    Private Sub Data_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        ' что-то сделать когда данные изменились
        Throw New MyException("моё исключение")
    End Sub

#Region "Динамическое добавление меню (зарезервировано)"
    '    Private Sub AddOptionMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddOptionMenuItem.Click
    '        AddOption()
    '    End Sub

    '    Private Sub RemoveOptionMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles RemoveOptionMenuItem.Click
    '        Dim itemToRemove As ToolStripMenuItem = Nothing

    '        ' 3 пункта надо зарезервировать для добавить, удалить и разделитель
    '        If CheckedListMenu.DropDownItems.Count > 3 Then
    '            Dim removeAt As Integer = CheckedListMenu.DropDownItems.Count - 1

    '            itemToRemove = CType(CheckedListMenu.DropDownItems(CheckedListMenu.DropDownItems.Count - 1), ToolStripMenuItem)

    '            If itemToRemove.Checked And CheckedListMenu.DropDownItems.Count > 4 Then
    '                Dim itemToCheck As ToolStripMenuItem = CType(CheckedListMenu.DropDownItems(CheckedListMenu.DropDownItems.Count - 2), ToolStripMenuItem)
    '                itemToCheck.Checked = True
    '            End If

    '            CheckedListMenu.DropDownItems.RemoveAt(removeAt)
    '        End If
    '    End Sub

    '    Private Sub AddOption()
    '        Dim newOption As New ToolStripMenuItem() With {.Checked = False,
    '                                                       .CheckOnClick = True,
    '                                                       .Text = "Option " & (CheckedListMenu.DropDownItems.Count - 2).ToString()}

    '        If CheckedListMenu.DropDownItems.Count = 3 Then
    '            newOption.Checked = True
    '        End If

    '        ' добавить обработчики событий Click и MouseEnter на новые пункты опций.
    '        AddHandler newOption.Click, AddressOf MenuOption_Click
    '        AddHandler newOption.MouseEnter, AddressOf MenuItem_MouseEnter
    '        AddHandler newOption.MouseLeave, AddressOf MenuItem_MouseLeave

    '        CheckedListMenu.DropDownItems.Add(newOption)
    '    End Sub

    'Private Sub MenuOption_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    ' сначало все пункты выключим
    '    For Each itemControl As Object In CheckedListMenu.DropDownItems
    '        If (TypeOf itemControl Is ToolStripMenuItem) Then
    '            Dim itemObject As ToolStripMenuItem = CType(itemControl, ToolStripMenuItem)
    '            itemObject.Checked = False
    '        End If
    '    Next

    '    ' затем включим нужный
    '    Dim selectedItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
    '    selectedItem.Checked = True
    'End Sub
#End Region

    'Private syncPointClient As Integer = 0 'для синхронизации

    ' ''' <summary>
    ' ''' Запуск таймера
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Sub StartAcquisition()
    '    'syncPointClient = 0

    '    mmTimer = New Multimedia.Timer()
    '    mmTimer.Mode = Multimedia.TimerMode.Periodic
    '    mmTimer.Period = TimerInterval
    '    mmTimer.Resolution = 1

    '    'Thread.CurrentThread.Priority = ThreadPriority.Normal

    '    'для отслеживания события в форме назначить объект синхронизации (должен быть компонент)
    '    'если таймер работает самостоятельно, то форму назначать не надо
    '    mmTimer.SynchronizingObject = Me 'gMainForm 'необходимо для отслеживания вызова событий
    '    'TODO: ME
    '    'mmTimer.SynchronizingObject = Me 'необходимо для отслеживания вызова событий

    '    Try
    '        '_ConnectionClient.ЧитатьМожно = True
    '        mmTimer.Start()
    '    Catch ex As Exception
    '        Const CAPTION As String = "Error StartAcquisition"
    '        Dim text As String = ex.ToString
    '        MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Stop)
    '    End Try

    '    _ConnectionClient.IsStartAcquisition = True
    'End Sub

    ' ''' <summary>
    ' ''' Остановить таймер
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Sub StopAcquisition()
    '    mmTimer.Stop()
    '    _ConnectionClient.IsStartAcquisition = False
    'End Sub

    'Private Sub mmTimer_Tick(sender As Object, e As EventArgs) Handles mmTimer.Tick
    '    If _ConnectionClient IsNot Nothing AndAlso _ConnectionClient.Tcp_Client.Connected AndAlso _ConnectionClient.IsStartAcquisition Then
    '        'If _ConnectionClient IsNot Nothing AndAlso _ConnectionClient.IsStartAcquisition Then
    '         _ConnectionClient._OnTimedEvent()
    '    Else
    '        mmTimer.Stop()
    '    End If
    'End Sub

    ' ''' <summary>
    ' ''' Запуск таймера
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Sub StartAcquisition()
    '    TimerClient.Interval = TimerInterval
    '    Try
    '        '_ConnectionClient.ЧитатьМожно = True
    '        TimerClient.Start()
    '    Catch ex As Exception
    '        Const CAPTION As String = "Error StartAcquisition"
    '        Dim text As String = ex.ToString
    '        MessageBox.Show(text, CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Stop)
    '    End Try

    '    _ConnectionClient.IsStartAcquisition = True
    'End Sub

    ' ''' <summary>
    ' ''' Остановить таймер
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Sub StopAcquisition()
    '    TimerClient.Stop()
    '    _ConnectionClient.IsStartAcquisition = False
    'End Sub

    'Private Sub TimerClient_Tick(sender As Object, e As EventArgs) Handles TimerClient.Tick
    '    If _ConnectionClient IsNot Nothing AndAlso _ConnectionClient.Tcp_Client.Connected AndAlso _ConnectionClient.IsStartAcquisition Then
    '         _ConnectionClient._OnTimedEvent()
    '    Else
    '        TimerClient.Stop()
    '    End If
    'End Sub

    'Private Sub TimerWatchDog_Tick(sender As Object, e As EventArgs)
    '    If СоединениеУстановлено AndAlso _ConnectionClient IsNot Nothing AndAlso _ConnectionClient.Tcp_Client.Connected AndAlso _ConnectionClient.IsStartAcquisition Then
    '        If OldCountAwaitData = _ConnectionClient.CountAwaitData Then 'зависание делегата асинхронного чтения 
    '            'остановить и возобновить связь с сервером
    '             StartStopConnectionWithServer(False)
    '            Application.DoEvents()
    '             StartStopConnectionWithServer(True)
    '        Else
    '            OldCountAwaitData = _ConnectionClient.CountAwaitData
    '        End If
    '    End If
    'End Sub

    'Private Sub TimerTest_Tick(sender As Object, e As EventArgs) Handles TimerTest.Tick
    '    'ToolStripStatusLabel.Text = String.Format("Соединение {0}", If(TestSend, "есть", "нет"))
    '    ToolStripStatusLabel.Text = String.Format("Откуда {0}", TestSend)
    'End Sub
End Class

''' <summary>
''' пользовательскле исключение на все случаи применения
''' </summary>
''' <remarks></remarks>
<Serializable>
Public Class MyException
    Inherits Exception
    Implements ISerializable

    Private ReadOnly _exceptionData As Double = 0.0 ' дополнительные данные для исключения
    Public ReadOnly Property ExceptionData() As Double
        Get
            Return _exceptionData
        End Get
    End Property

    Public Sub New()
    End Sub

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(message As String, innerException As Exception)
        MyBase.New(message, innerException)
    End Sub

    Public Sub New(exceptionData As Double, message As String)
        MyBase.New(message)
        _exceptionData = exceptionData
    End Sub

    Public Sub New(exceptionData As Double, message As String, innerException As Exception)
        MyBase.New(message, innerException)
        _exceptionData = exceptionData
    End Sub

    ' serialization handlers
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
        ' deserialize
        _exceptionData = info.GetDouble("MyExceptionData")
    End Sub

    <SecurityPermission(SecurityAction.Demand, SerializationFormatter:=True)>
    Public Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext) Implements ISerializable.GetObjectData
        ' serialize
        MyBase.GetObjectData(info, context)
        info.AddValue("MyExceptionData", _exceptionData)
    End Sub
End Class

' применить стиль, которые перечислены в комбобоксе как строки
' _tableLP.GrowStyle = DirectCast([Enum].Parse(GetType(TableLayoutPanelGrowStyle), _combGrowStyle.SelectedItem.ToString()), TableLayoutPanelGrowStyle)


''<System.Diagnostics.DebuggerNonUserCode()>
'Public Sub New()
'    MyBase.New()

'    'me call is required by the Windows Form Designer.
'    InitializeComponent()
'    ''// dynamically create sub-menu for main menu
'    '_subMenu(0) = New ToolStripMenuItem("GripStyle", Nothing)
'    '_subMenu(1) = New ToolStripMenuItem("Raft on...", Nothing)
'    '_subMenu(2) = New ToolStripMenuItem("ToolStripTabControl...", Nothing)
'    ''_subMenuGripStyle(0) = New ToolStripMenuItem("Visible", Nothing, New EventHandler(Me.GripStyleMenuHandler))
'    ''_subMenuGripStyle(1) = New ToolStripMenuItem("Hidden", Nothing, New EventHandler(Me.GripStyleMenuHandler))
'    '_subMenuGripStyle(1).Enabled = False
'    ''_subMenuRaftSide(0) = New ToolStripMenuItem("Top", Nothing, New EventHandler(Me.RaftMenuHandler))
'    ''_subMenuRaftSide(1) = New ToolStripMenuItem("Bottom", Nothing, New EventHandler(Me.RaftMenuHandler))
'    ''_subMenuRaftSide(2) = New ToolStripMenuItem("Left", Nothing, New EventHandler(Me.RaftMenuHandler))
'    ''_subMenuRaftSide(3) = New ToolStripMenuItem("Right", Nothing, New EventHandler(Me.RaftMenuHandler))
'    '_subMenuTabCntr(0) = New ToolStripMenuItem("Add on", Nothing, New EventHandler(AddressOf Me.TabCntrHandler))
'    '_subMenuTabCntr(1) = New ToolStripMenuItem("Remove from", Nothing, New EventHandler(AddressOf Me.TabCntrHandler))
'    '_subMenuTabCntr(1).Enabled = False
'    '_subMenu(0).DropDownItems.AddRange(Me._subMenuGripStyle)
'    ''_subMenu(1).DropDownItems.AddRange(Me._subMenuRaftSide)
'    '_subMenu(2).DropDownItems.AddRange(Me._subMenuTabCntr)

'    'utilityHelper = New UtilityHelper
'    'InitializeMenuHelperStrings(Me.MenuStripForm)
'    'InitializeToolTips(Me.toolStrip1)
'    'MapToolBarAndMenuItems()
'End Sub

'Private Sub InitializeMenuHelperStrings(ByVal menuItems As Menu.MenuItemCollection)
'    For Each item As MenuItem In menuItems
'        utilityHelper.AddMenuString(item)
'        AddHandler item.Select, AddressOf OnMenuSelect
'        InitializeMenuHelperStrings(item.MenuItems)
'    Next
'End Sub

'Private Sub InitializeToolTips(ByVal buttons As ToolBar.ToolBarButtonCollection)
'    Dim helpIndex As Integer = 0
'    For Each button As ToolBarButton In buttons
'        If Not button.Style = ToolBarButtonStyle.Separator Then
'            button.ToolTipText = utilityHelper.GetToolTip(helpIndex)
'            helpIndex += 1
'        End If
'    Next
'End Sub
'Private Shared Sub InitializeToolTips(ByRef ToolStripButtons As ToolStrip)
'    Dim helpIndex As Integer = 0
'    For Each item As ToolStripItem In ToolStripButtons.Items
'        If (TypeOf item Is ToolStripButton) Then
'            'item.ToolTipText = utilityHelper.GetToolTip(helpIndex)
'            helpIndex += 1
'        End If
'    Next
'End Sub

' пример связывания пункта меню с кнопкой
'Private Sub MapToolBarAndMenuItems()
'    'utilityHelper.MapMenuAndToolBar(toolStripButtonПуск, NewToolStripMenuItem)
'    ' связывает кнопки и меню с режимом Checked 
'    ' и так далее пример
'    utilityHelper.MapMenuAndToolBar(toolStripButtonПуск, Start)
'End Sub

'Private Sub FlowLayoutPanelControls_Resize(sender As Object, e As EventArgs) Handles FlowLayoutPanelControls.Resize
'    FlowLayoutPanelControlsResize()
'End Sub

'Private ControlsSize As New Dictionary(Of Control, Size)

'Private Sub FlowLayoutPanelControlsResize()
'    If Me.IsHandleCreated AndAlso ControlsSize.Count > 0 Then
'        Const modelForScalling As Double = 660.0
'        Dim factor As Double = Math.Sqrt(FlowLayoutPanelControls.Width * FlowLayoutPanelControls.Height)

'        If factor > modelForScalling Then
'            Dim scalling As Double = factor / modelForScalling

'            For Each itemControl As Control In FlowLayoutPanelControls.Controls
'                itemControl.Width = Convert.ToInt32(ControlsSize(itemControl).Width * scalling)
'                itemControl.Height = Convert.ToInt32(ControlsSize(itemControl).Height * scalling)
'            Next
'        Else
'            For Each itemControl As Control In FlowLayoutPanelControls.Controls
'                itemControl.Size = ControlsSize(itemControl)
'            Next
'        End If
'    End If
'End Sub


'Dim indexer As New Indexer(Of Part)()
'Dim p1 As New Part("1", "Part01", "The first part", 1.5)
'Dim p2 As New Part("2", "Part02", "The second part", 2.0)
'        indexer.Add(p1.PartId, p1)
'        indexer.Add(p2.PartId, p2)
'Dim p As Part = indexer.Find("2")
'        Console.WriteLine("Found: {0}", p.ToString())
' объект который необходимо добавить или индексировать
'Class Part
'    Private _partId As String
'    Private _name As String
'    Private _description As String
'    Private _weight As Double

'    Public ReadOnly Property PartId() As String
'        Get
'            Return _partId
'        End Get
'    End Property

'    Public Sub New(partId As String, name As String, description As String, weight As Double)
'        _partId = partId
'        _name = name
'        _description = description
'        _weight = weight
'    End Sub

'    Public Overrides Function ToString() As String
'        Return String.Format("Part: {0}, Name: {1}, Weight: {2}", _partId, _name, _weight)
'    End Function
'End Class

'''' <summary>
'''' Кнопки взятия управления шасси при проигрывании циклограмм
'''' </summary>
'''' <remarks></remarks>
'Class ToolStripButtonLinkChassis
'    Implements IDisposable

'    Private ReadOnly mChassisName As String
'    Private ReadOnly mToolStripButtonChassis As ToolStripButton

'    Public ReadOnly Property ChassisName() As String
'        Get
'            Return mChassisName
'        End Get
'    End Property

'    Public ReadOnly Property ToolStripButtonChassis() As ToolStripButton
'        Get
'            Return mToolStripButtonChassis
'        End Get
'    End Property

'    Public Sub New(ChassisName As String, ToolStripButtonChassis As ToolStripButton)
'        mChassisName = ChassisName
'        mToolStripButtonChassis = ToolStripButtonChassis
'    End Sub

'    Public Overrides Function ToString() As String
'        Return String.Format("ChassisName: {0}, ToolStripButton: {1}", mChassisName, mToolStripButtonChassis.ToString)
'    End Function

'#Region "IDisposable Support"
'    Private disposedValue As Boolean ' Чтобы обнаружить избыточные вызовы

'    ' IDisposable
'    Protected Overridable Sub Dispose(disposing As Boolean)
'        If Not disposedValue Then
'            If disposing Then
'                ' TODO: освободить управляемое состояние (управляемые объекты).
'                If mToolStripButtonChassis IsNot Nothing Then
'                    mToolStripButtonChassis.Dispose()
'                End If
'            End If

'            ' TODO: освободить неуправляемые ресурсы (неуправляемые объекты) и переопределить ниже Finalize().
'            ' TODO: задать большие поля как null.
'        End If
'        disposedValue = True
'    End Sub

'    ' TODO: переопределить Finalize(), только если Dispose(ByVal disposing As Boolean) выше имеет код для освобождения неуправляемых ресурсов.
'    Protected Overrides Sub Finalize()
'        ' Не изменяйте этот код.  Поместите код очистки в расположенную выше команду Удалить(ByVal удаление как булево).
'        Dispose(False)
'        'MyBase.Finalize()
'    End Sub

'    ' Этот код добавлен редактором Visual Basic для правильной реализации шаблона высвобождаемого класса.
'    Public Sub Dispose() Implements IDisposable.Dispose
'        ' Не изменяйте этот код. Разместите код очистки выше в методе Dispose(disposing As Boolean).
'        Dispose(True)
'        GC.SuppressFinalize(Me)
'    End Sub
'#End Region
'End Class


' ''' <summary>
' ''' универсальный indexer, доступа к generic type
' ''' внимание type обязан быть class (не value type)
' ''' </summary>
' ''' <typeparam name="T"></typeparam>
' ''' <remarks></remarks>
'Class Indexer(Of T As Class)
'    Private Structure ItemStruct
'        Public key As String
'        Public value As T
'        Public Sub New(key As String, value As T)
'            Me.key = key
'            Me.value = value
'        End Sub
'    End Structure

'    Private ReadOnly _items As New List(Of ItemStruct)()
'    ' T обязан быть class и может возвращать null если не найден
'    Public Function Find(key As String) As T
'        For Each itemStruct As ItemStruct In _items
'            If itemStruct.key = key Then
'                Return itemStruct.value
'            End If
'        Next
'        Return Nothing
'    End Function

'    Public Sub Add(key As String, value As T)
'        _items.Add(New ItemStruct(key, value))
'    End Sub
'End Class

''Пример добавления пунктов в combo box и связывание расположения с событием
'' Add a combo box and fill it with items.
''Dim menuComboBox As New ToolStripComboBox()
'    menuComboBox.Items.Add("Top")
'    menuComboBox.Items.Add("Bottom")
'    menuComboBox.Items.Add("Left")
'    menuComboBox.Items.Add("Right")
'    menuComboBox.SelectedIndex = 0
'    menuComboBox.ToolTipText = "Выберете расположение панели"

'    AddHandler menuComboBox.SelectedIndexChanged, AddressOf ComboBox_SelectedIndexChanged

'Private Sub ComboBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
'    Dim combo As ToolStripComboBox = CType(sender, ToolStripComboBox)
'    'Select Case combo.SelectedIndex
'    '    Case 0
'    '        MenuStripForm.Dock = DockStyle.Top
'    '    Case 1
'    '        MenuStripForm.Dock = DockStyle.Bottom
'    '    Case 2
'    '        MenuStripForm.Dock = DockStyle.Left
'    '        MenuStripForm.Width = 50
'    '    Case 3
'    '        MenuStripForm.Dock = DockStyle.Right
'    '        MenuStripForm.Width = 50
'    '    Case Else
'    '        MenuStripForm.Dock = DockStyle.Top
'    'End Select

'    'If Not curToolStrip Is Nothing Then
'    '    Select Case CType(sender, ToolStripMenuItem).Text
'    Select Case combo.Text
'        Case "Top"
'            tsContainer.TopToolStripPanel.Controls.Add(MenuStripForm)
'        Case "Bottom"
'            tsContainer.BottomToolStripPanel.Controls.Add(MenuStripForm)
'        Case "Left"
'            tsContainer.LeftToolStripPanel.Controls.Add(MenuStripForm)
'        Case "Right"
'            tsContainer.RightToolStripPanel.Controls.Add(MenuStripForm)
'    End Select
'    'End If
'End Sub