Imports System.ComponentModel
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

''' <summary>
''' Редактор мощностей загрузок.
''' Определение состав подключённого оборудования и составление списка свободных портов вывода.
''' </summary>
Public Class FormEditorPower
    Implements IMdiChildrenWindow

    Public Enum TypeNode
        Aggregate = 1
        Power = 2
    End Enum

    Private WithEvents DevicesBindingSource As BindingSource ' источник данных для таблицы Devices
    Private WithEvents PortsBindingSource As BindingSource ' источник данных для таблицы Ports
    Private WithEvents LinesBindingSource As BindingSource ' источник данных для таблицы Lines
    Private ReadOnly Property MainFomMdiParent As FrmMain Implements IMdiChildrenWindow.MainFomMdiParent
    Private ReadOnly mDigitalWriter As DigitalWriter
#Region " variable TreeViewDevice"
    Private Const POWER As String = "Величина-"
    Private connection As OleDbConnection
    'Private indexCurrentAggregate As Integer ' Index Текущего Редактируемого Устройства
    Private keyAggregate As Integer
    Private keyPower As Integer ' keyВеличинаЗагрузки
    Private nodeKey As Integer ' KeyУстройства
    Private Aggregates As New List(Of String)
    Private nameAggregateToLanch As String = String.Empty ' Имя Устройства Для Исполнения По Кнопке
    Private isAfterSelectIsRequire As Boolean = True
    Private isBeforeExpandIsRequire As Boolean = True
    Private isDirty As Boolean ' флаг, что было изменение агрегатов и требуется обновления таблиц расчётных параметров
    Private isPowerCorrect As Boolean ' Проверка Загрузки Проведена
#End Region

    ''' <summary>
    ''' Контрол для настройки свойств Aggregate и Power (полиморфный).
    ''' </summary>
    Private mAttributesControl As BaseAttributesControl
    ''' <summary>
    ''' Словарь фабричных методов, возвращающие контролы для настройки свойств Aggregate и Power.
    ''' </summary>
    Private Shared ReadOnly AttributeControlCreators As Dictionary(Of TypeNode, BaseCreatorAttributesControl) = New Dictionary(Of TypeNode, BaseCreatorAttributesControl) From {
        {TypeNode.Aggregate, New CreatorAggregateAttributesControl},
        {TypeNode.Power, New CreatorPowerAttributesControl}
    }

#Region "Форма"
    Sub New(inMainFomMdiParent As FrmMain)
        MyBase.New()

        InitializeComponent()
        MainFomMdiParent = inMainFomMdiParent
        mDigitalWriter = New DigitalWriter(SystemConfigurationDevices)
    End Sub

    Private Sub FormEditorPower_Load(ByVal eventSender As Object, ByVal eventArgs As EventArgs) Handles Me.Load
        LoadConfigurationDevices()
        AddHandler SystemConfigurationDevices.PropertyChanged, New PropertyChangedEventHandler(AddressOf Data_PropertyChanged)
        PopulateAggregateTree()
    End Sub

    Private Sub TSMenuItemExit_Click(sender As Object, e As EventArgs) Handles TSMenuItemExit.Click
        Close()
    End Sub

    Private Sub FormEditorPower_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If TSButtonLaunchPower.Checked Then TSButtonLaunchPower.Checked = False

        mDigitalWriter.Close()
        DataGridViewDevices.DataSource = Nothing

        If DevicesBindingSource IsNot Nothing Then
            DevicesBindingSource.Dispose()
            DevicesBindingSource = Nothing
        End If

        If isDirty Then MainFomMdiParent.ReloadParameters()
        ' при выставленном флааге isDirty  если в Registration будет ошибка,
        ' то возможно попробовать вывести сообщение о необходимости перезагрузки программы или
        ' попробовать редактировать устройства, когда сбор не запущен.
    End Sub

    ''' <summary>
    ''' Создать кофигурацию оборудования для передачи компоненту TableLineControl
    ''' путём считывания из файла.
    ''' </summary>
    Private Sub LoadConfigurationDevices()
        Dim mSystemDevices As New SystemDevices ' узнать конфигурацию платы
        ComboBoxDevices.Items.AddRange(mSystemDevices.Devices.ToArray)
        ComboBoxPhysicalPorts.Items.AddRange(mSystemDevices.PhysicalPorts.ToArray)
        ComboBoxPhysicalChannels.Items.AddRange(mSystemDevices.PhysicalChannels.ToArray)
        ComboBoxLineCount.Items.AddRange(mSystemDevices.LinesCountString.ToArray)
        ComboBoxDigitalWriteTask.Items.AddRange(mSystemDevices.DigitalWriteTasks.ToArray)

        If ComboBoxDevices.Items.Count > 0 Then ComboBoxDevices.SelectedIndex = 0
        If ComboBoxPhysicalPorts.Items.Count > 0 Then ComboBoxPhysicalPorts.SelectedIndex = 0
        If ComboBoxPhysicalChannels.Items.Count > 0 Then ComboBoxPhysicalChannels.SelectedIndex = 0
        If ComboBoxLineCount.Items.Count > 0 Then ComboBoxLineCount.SelectedIndex = 0
        If ComboBoxDigitalWriteTask.Items.Count > 0 Then ComboBoxDigitalWriteTask.SelectedIndex = 0

        SystemConfigurationDevices.Enabled = False
        SystemConfigurationDevices.RestoreDevicesFromFile(MainFomMdiParent.PathResourses)
        UpdateDevicesBindingSource()
    End Sub

    ''' <summary>
    ''' Создать кофигурацию оборудования для передачи компоненту TableLineControl
    ''' путем опроса железа.
    ''' </summary>  
    Private Async Sub UpdateConfigurationDevice()
        If MessageBox.Show("Определение состава оборудования производится при первоначальной установке программного обеспечения или при изменении подключённых плат сбора в системном блоке." & vbCrLf &
                           "Если Вы точно уверены в своих действиях нажмите <Да> для продолжения или <Нет> для отмены.",
                           "Определение дискретных выходов",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> Windows.Forms.DialogResult.Yes Then Exit Sub

        If MessageBox.Show("Для определения состава оборудования необходимо произвести сканирование дискретных выходов линий портов." & vbCrLf &
                           "Будет произведена установка и снятие логического сигнала на дискретные выходы." & vbCrLf &
                           "Если в данный момент подключено и запущено какое-либо оборудование, управляемое с дискретных выходов, то необходимо его отключить или перевести на ручное управление." & vbCrLf &
                           "Если стендовое оборудование переведено в безопасный режим нажмите <Да> для продолжения или <Нет> для отмены.",
                           "Определение дискретных выходов",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> Windows.Forms.DialogResult.Yes Then Exit Sub

        Dim mSystemDevices As New SystemDevices ' узнать конфигурацию платы
        Await mSystemDevices.CheckWriteAllLines()

        ' Сохранить новую конфигурацию железа
        ' тест Dim newDevices As Devices = SystemConfigurationDevices.CreateTestConfigurationDevices
        SystemConfigurationDevices.ReinitializeDevices(mSystemDevices.SystemDODevices)
        UpdateDevicesBindingSource()
        MessageBox.Show("Состав оборудования определён и сохранён успешно.", "Определение дискретных выходов", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' Отметка занятости линий портов для устройств кроме текущего для выбранного изделия.
    ''' Вызывается при смене устройства или агрегата.
    ''' </summary>
    Private Sub MarkLineIsBusy()
        ' Пройти по всем мощностям устройств (кроме текущего устройства) выбранного изделия  и составить их список занятых линий.
        ' Пройти по этому списку и заблокировать доступность линий порта.
        ' тест SystemConfigurationDevices.SynchronizeBusyLine(SystemConfigurationDevices.GetTestBusyDevices)
        Dim busyDevices As New Devices()
        Dim devNumber, portNumber, lineNumber As Integer

        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            cn.Open()
            Dim cmd As OleDbCommand = cn.CreateCommand
            cmd.CommandType = CommandType.Text

            ' полный запрос на всю иерархию Устройства1 -> ВеличинаЗагрузки2 -> ДискретныйВыход3
            cmd.CommandText = "SELECT Устройства1.*, ВеличинаЗагрузки2.*, ДискретныйВыход3.* " &
                "FROM (Устройства1 RIGHT JOIN ВеличинаЗагрузки2 ON Устройства1.KeyУстройства = ВеличинаЗагрузки2.KeyУстройства) RIGHT JOIN ДискретныйВыход3 ON ВеличинаЗагрузки2.keyВеличинаЗагрузки = ДискретныйВыход3.keyВеличинаЗагрузки " &
                "WHERE Устройства1.KeyУстройства <> " & keyAggregate.ToString

            Using rdr As OleDbDataReader = cmd.ExecuteReader
                If rdr.HasRows Then
                    ' заполнить значениями мощности
                    Do While rdr.Read
                        devNumber = Convert.ToInt32(Convert.ToString(rdr("Target")).Replace("Dev", ""))
                        portNumber = Convert.ToInt16(rdr("НомерПорта"))
                        lineNumber = Convert.ToInt16(rdr("НомерЛинии"))

                        If Not IsCheckContainsDevicePortLine(devNumber, portNumber, lineNumber) Then Continue Do

                        If Not busyDevices.Contains(devNumber) Then busyDevices.Add(New Device(devNumber))
                        If Not busyDevices(devNumber).Contains(portNumber) Then busyDevices(devNumber).Add(portNumber)
                        If Not busyDevices(devNumber)(portNumber).Contains(lineNumber) Then
                            Dim newLine As New Line(lineNumber, True) With {.IsBusy = True}
                            busyDevices(devNumber)(portNumber).Add(newLine)
                        End If
                    Loop
                End If
            End Using
        End Using

        SystemConfigurationDevices.SynchronizeBusyLine(busyDevices)
    End Sub

    ''' <summary>
    ''' Проверка наличия в системной конфигурации устройства, порта и линии из возможно некорректных настроек в базе данных.
    ''' </summary>
    ''' <param name="devNumber"></param>
    ''' <param name="portNumber"></param>
    ''' <param name="lineNumber"></param>
    ''' <returns></returns>
    Private Function IsCheckContainsDevicePortLine(devNumber As Integer, portNumber As Integer, lineNumber As Integer) As Boolean
        If SystemConfigurationDevices.ContainsDevice(devNumber) Then
            If SystemConfigurationDevices(devNumber).Contains(portNumber) Then
                If SystemConfigurationDevices(devNumber)(portNumber).Contains(lineNumber) Then
                    Return SystemConfigurationDevices(devNumber)(portNumber)(lineNumber).IsEnabled
                End If
            End If
        End If

        Return False
    End Function

    ''' <summary>
    ''' Задать источник данных для Devices.
    ''' </summary>
    Private Sub UpdateDevicesBindingSource()
        DevicesBindingSource = New BindingSource With {.DataSource = SystemConfigurationDevices.DeviceBindingList}
        DataGridViewDevices.Columns.Clear()
        DataGridViewDevices.DataSource = DevicesBindingSource

        For Each itemColumn As DataGridViewColumn In DataGridViewDevices.Columns
            itemColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Next

        UpdatePortsBindingSource()
    End Sub

    ''' <summary>
    ''' Задать источник данных для Ports.
    ''' </summary>
    Private Sub UpdatePortsBindingSource()
        If DevicesBindingSource.List.Count > 0 Then
            PortsBindingSource = New BindingSource With {
                .DataSource = SystemConfigurationDevices.DeviceBindingList(DevicesBindingSource.Position).PortBindingList
            }
            DataGridViewPorts.Columns.Clear()
            DataGridViewPorts.DataSource = PortsBindingSource

            For Each itemColumn As DataGridViewColumn In DataGridViewPorts.Columns
                itemColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            Next

            UpdateLinesBindingSource()
        End If
    End Sub

    ''' <summary>
    ''' Задать источник данных для Lines.
    ''' </summary>
    Private Sub UpdateLinesBindingSource()
        If PortsBindingSource.List.Count > 0 Then
            LinesBindingSource = New BindingSource With {
                .DataSource = SystemConfigurationDevices.DeviceBindingList(DevicesBindingSource.Position).PortBindingList(PortsBindingSource.Position).LineBindingList
            }
            DataGridViewLines.Columns.Clear()
            DataGridViewLines.DataSource = LinesBindingSource
            DataGridViewLines.Columns("LineControl").Visible = False
            DataGridViewLines.Columns("Caption").Visible = False

            For Each itemColumn As DataGridViewColumn In DataGridViewLines.Columns
                itemColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            Next
        End If
    End Sub

    ''' <summary>
    ''' Реализация INotifyPropertyChanged для отслеживания изменения выбора устройства или порта
    ''' и соответствующей строки в таблице.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Data_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        ' что-то сделать когда данные изменились
        If e.PropertyName = "DeviceSelectedIndex" Then
            DataGridViewDevices.Rows.Item(SystemConfigurationDevices.DeviceSelectedIndex).Selected = True
            DevicesBindingSource.Position = SystemConfigurationDevices.DeviceSelectedIndex
            UpdatePortsBindingSource()
        ElseIf e.PropertyName = "PortSelectedIndex" Then
            DataGridViewPorts.Rows.Item(SystemConfigurationDevices.PortSelectedIndex).Selected = True
            PortsBindingSource.Position = SystemConfigurationDevices.PortSelectedIndex
            UpdateLinesBindingSource()
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Показать панель инструментов
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TSpMenuItemToolbars_CheckedChanged(sender As Object, e As EventArgs) Handles TSpMenuItemShowToolbars.CheckedChanged
        ToolStripDevice.Visible = TSpMenuItemShowToolbars.Checked
    End Sub

    ''' <summary>
    ''' Показать панель статуса
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TSpMenuItemStatusStrip_CheckedChanged(sender As Object, e As EventArgs) Handles TSpMenuItemShowStatusStrip.CheckedChanged
        StatusStripForm.Visible = TSpMenuItemShowStatusStrip.Checked
    End Sub

    ''' <summary>
    ''' Выделить устройство в сетке.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridViewDevices_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridViewDevices.SelectionChanged
        UpdatePortsBindingSource()
        If SystemConfigurationDevices.DeviceBindingList.Count > 0 Then
            ' убрал для исключения изменения
            'If DataGridViewDevices.SelectedRows.Count <> 0 Then
            '    SystemConfigurationDevices.ComboBoxDevices.SelectedIndex = DataGridViewDevices.SelectedRows(0).Index
            'End If
        End If
    End Sub

    ''' <summary>
    ''' Выделить порт в сетке.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridViewPorts_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridViewPorts.SelectionChanged
        UpdateLinesBindingSource()
        ' убрал для исключения изменения
        'If SystemConfigurationDevices.ComboBoxPorts.Items.Count > 0 Then
        '    If DataGridViewPorts.SelectedRows.Count <> 0 Then
        '        SystemConfigurationDevices.ComboBoxPorts.SelectedIndex = DataGridViewPorts.SelectedRows(0).Index 'PortsBindingSource.Position
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' Создать кофигурацию оборудования.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TSButtonDevicesUpdate_Click(sender As Object, e As EventArgs) Handles TSButtonDevicesUpdate.Click
        If TSButtonLaunchPower.Checked Then TSButtonLaunchPower.Checked = False

        TSButtonDevicesUpdate.Enabled = False
        UpdateConfigurationDevice()
        TSButtonDevicesUpdate.Enabled = True
    End Sub

    Private Sub TSMenuItemShowNavigation_CheckedChanged(sender As Object, e As EventArgs) Handles TSMenuItemShowNavigation.CheckedChanged
        SplitContainerExistingStage.Panel2Collapsed = Not TSMenuItemShowNavigation.Checked
    End Sub

    ''' <summary>
    ''' Выбор порта из панели устройств.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ComboBoxPhysicalPort_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ComboBoxPhysicalPorts.SelectedIndexChanged
        If IsHandleCreated AndAlso ComboBoxPhysicalPorts.SelectedIndex <> -1 AndAlso ComboBoxLineCount.Items.Count > 0 Then
            ComboBoxLineCount.SelectedIndex = ComboBoxPhysicalPorts.SelectedIndex
        End If
    End Sub

    Private Sub ShowMessage(ByVal message As String)
        StatusLabelMessage.Text = message
    End Sub

    'Private Sub ClearMessage()
    '    StatusLabelMessage.Text = ""
    'End Sub

#Region "SetEnabledButtons"
    Private Enum EditMode
        TreeDeviceNodeEmpty
        TreeDeviceNodeSelectMode
        TreePowerNodeSelectMode
        AddDeviceMode
        EditDeviceModeOn
        EditDeviceModeOff
        SaveDeviceMode
        RemoveDeviceMode
        AddPowerMode
        EditPowerModeOn
        EditPowerModeOff
        SavePowerMode
        RemovePowerMode
        LaunchPowerModeOn
        LaunchPowerModeOff
    End Enum

    ''' <summary>
    ''' Включить доступность панелей и кнопок.
    ''' </summary>
    ''' <param name="inEditMode"></param>
    Private Sub SetEnabledOnEditMode(inEditMode As EditMode)
        TreeViewAggregate.Enabled = False
        SplitContainerAggregate.Panel2.Enabled = False
        SystemConfigurationDevices.Enabled = False

        TSButtonAddAggregate.Enabled = False
        TSButtonEditAggregate.Enabled = False
        TSButtonSaveAggregate.Enabled = False
        TSButtonRemoveAggregate.Enabled = False

        TSButtonAddPower.Enabled = False
        TSButtonEditPower.Enabled = False
        TSButtonSavePower.Enabled = False
        TSButtonRemovePower.Enabled = False
        TSButtonLaunchPower.Enabled = False

        Select Case inEditMode
            Case EditMode.TreeDeviceNodeEmpty
                TSButtonAddAggregate.Enabled = True
            Case EditMode.TreeDeviceNodeSelectMode
                TreeViewAggregate.Enabled = True

                TSButtonAddAggregate.Enabled = True
                TSButtonEditAggregate.Enabled = True
                TSButtonRemoveAggregate.Enabled = True
                TSButtonAddPower.Enabled = True
            Case EditMode.TreePowerNodeSelectMode
                TreeViewAggregate.Enabled = True

                TSButtonAddPower.Enabled = True
                TSButtonEditPower.Enabled = True
                TSButtonRemovePower.Enabled = True
                TSButtonLaunchPower.Enabled = True
            Case EditMode.AddDeviceMode
                SplitContainerAggregate.Panel2.Enabled = True

                TSButtonSaveAggregate.Enabled = True
            Case EditMode.EditDeviceModeOn
                SplitContainerAggregate.Panel2.Enabled = True

                TSButtonSaveAggregate.Enabled = True
            Case EditMode.EditDeviceModeOff
                TreeViewAggregate.Enabled = True
                SplitContainerAggregate.Panel2.Enabled = True

                TSButtonAddAggregate.Enabled = True
                TSButtonRemoveAggregate.Enabled = True
            Case EditMode.SaveDeviceMode
                TreeViewAggregate.Enabled = True

                TSButtonAddAggregate.Enabled = True
                TSButtonEditAggregate.Enabled = True
                TSButtonRemoveAggregate.Enabled = True
                TSButtonAddPower.Enabled = True
                isDirty = True
            Case EditMode.RemoveDeviceMode
                TreeViewAggregate.Enabled = True

                TSButtonAddAggregate.Enabled = True
                TSButtonEditAggregate.Enabled = True
                TSButtonRemoveAggregate.Enabled = True
                TSButtonAddPower.Enabled = True
                isDirty = True
            Case EditMode.AddPowerMode
                SplitContainerAggregate.Panel2.Enabled = True
                SystemConfigurationDevices.Enabled = True

                TSButtonSavePower.Enabled = True
            Case EditMode.EditPowerModeOn
                SplitContainerAggregate.Panel2.Enabled = True
                SystemConfigurationDevices.Enabled = True

                TSButtonSavePower.Enabled = True
            Case EditMode.EditPowerModeOff
                TreeViewAggregate.Enabled = True

                TSButtonAddPower.Enabled = True
                TSButtonRemovePower.Enabled = True
                TSButtonLaunchPower.Enabled = True
            Case EditMode.SavePowerMode, EditMode.LaunchPowerModeOff
                TreeViewAggregate.Enabled = True
                TSButtonAddAggregate.Enabled = True

                TSButtonAddPower.Enabled = True
                TSButtonRemovePower.Enabled = True
                TSButtonLaunchPower.Enabled = True
            Case EditMode.RemovePowerMode
                TSButtonAddPower.Enabled = True
                TSButtonEditPower.Enabled = True
                TSButtonRemovePower.Enabled = True
                TSButtonLaunchPower.Enabled = True
            Case EditMode.LaunchPowerModeOn
                TSButtonLaunchPower.Enabled = True
        End Select
    End Sub
#End Region

#Region "TreeViewDevice"
    ''' <summary>
    ''' Загрузить дерево устройствами.
    ''' </summary>
    Private Sub PopulateAggregateTree()
        Dim strSQL As String = "SELECT * FROM Устройства1 ORDER BY Устройства1.KeyУстройства"

        ' все типы устройств
        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            Try
                cn.Open()
                Dim odaDataAdapter As OleDbDataAdapter = New OleDbDataAdapter(strSQL, cn)
                Dim dtDataTable As New DataTable

                odaDataAdapter.Fill(dtDataTable)
                TreeViewAggregate.Nodes.Clear()

                If dtDataTable.Rows.Count > 0 Then
                    For Each itemDataRow As DataRow In dtDataTable.Rows
                        Aggregates.Add(CStr(itemDataRow("ИмяУстройства")))
                        keyAggregate = CInt(itemDataRow("KeyУстройства"))

                        Dim cRoot As New DirectoryNodeAggregate("Устройство-" & CStr(itemDataRow("ИмяУстройства")), TypeNode.Aggregate, keyAggregate) With {
                            .SelectedImageIndex = 0,
                            .ImageIndex = 1
                         }
                        TreeViewAggregate.Nodes.Add(cRoot)
                        AddDirectories(cRoot, cn)
                    Next
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString, $"Ошибка обновления в процедуре <{NameOf(PopulateAggregateTree)}>.",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Using

        'indexCurrentAggregate = -1
        If TreeViewAggregate.Nodes.Count = 0 Then SetEnabledOnEditMode(EditMode.TreeDeviceNodeEmpty)
    End Sub

    ''' <summary>
    ''' Добавить узел устройства.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="cn"></param>
    Private Sub AddDirectories(ByVal node As DirectoryNodeAggregate, ByRef cn As OleDbConnection)
        If node.NodeType = TypeNode.Aggregate Then
            keyAggregate = node.KeyId
            node.ImageIndex = 1

            Using cmd As OleDbCommand = cn.CreateCommand()
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "SELECT * From [ВеличинаЗагрузки2] Where KeyУстройства =  " & keyAggregate & " ORDER BY ЧисловоеЗначение"

                Using rdr As OleDbDataReader = cmd.ExecuteReader
                    Do While rdr.Read()
                        keyPower = CInt(rdr("keyВеличинаЗагрузки"))
                        node.Nodes.Add(New DirectoryNodeAggregate(POWER & CStr(rdr("ВеличинаЗагрузки")), TypeNode.Power, keyPower))
                        node.LastNode.SelectedImageIndex = 0
                        node.LastNode.ImageIndex = TypeNode.Power
                    Loop
                End Using
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Раскрыть узел и заполнить контрол свойствами.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TreeViewAggregate_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TreeViewAggregate.AfterSelect
        If Not isAfterSelectIsRequire Then Exit Sub
        If TSButtonLaunchPower.Checked Then TSButtonLaunchPower.Checked = False

        Dim nodeSelect As DirectoryNodeAggregate = CType(e.Node, DirectoryNodeAggregate)
        Select Case nodeSelect.NodeType
            Case TypeNode.Aggregate
                keyAggregate = nodeSelect.KeyId
                FillAttributesControl(TypeNode.Aggregate, keyAggregate)
            Case TypeNode.Power
                keyPower = nodeSelect.KeyId
                FillAttributesControl(TypeNode.Power, keyPower)
        End Select

        isAfterSelectIsRequire = False
        If isBeforeExpandIsRequire Then e.Node.Expand()
        isAfterSelectIsRequire = True

        If TSButtonEditAggregate.Checked Then TSButtonEditAggregate.Checked = False
        If TSButtonEditPower.Checked Then TSButtonEditPower.Checked = False
    End Sub

    Private Sub TreeViewAggregate_AfterCollapse(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TreeViewAggregate.AfterCollapse
        e.Node.BackColor = Color.White
    End Sub

    Private Sub TreeViewAggregate_BeforeExpand(ByVal sender As Object, ByVal e As TreeViewCancelEventArgs) Handles TreeViewAggregate.BeforeExpand
        Dim nodeExpanding As DirectoryNodeAggregate = CType(e.Node, DirectoryNodeAggregate)
        Dim success As Boolean

        ' проверка, что раскрываемый узел совпадает  с выбранным
        For Each itemNode As TreeNode In TreeViewAggregate.Nodes
            If itemNode.IsSelected AndAlso itemNode Is nodeExpanding Then
                success = True
                Exit For
            End If
        Next

        If Not success Then
            isAfterSelectIsRequire = False
            TreeViewAggregate.SelectedNode = nodeExpanding
            isAfterSelectIsRequire = True
        End If

        ' свернуть неактивные
        If Not e.Node.Parent Is Nothing Then
            For Each itemTreeNode As TreeNode In e.Node.Parent.Nodes
                If itemTreeNode.IsExpanded Then itemTreeNode.Collapse()
            Next
        Else ' по самому первому уровню
            For Each itemTreeNode As TreeNode In TreeViewAggregate.Nodes
                If itemTreeNode.IsExpanded Then itemTreeNode.Collapse()
            Next
        End If

        If Not nodeExpanding.IsSubDirectoriesAdded Then AddSubDirectories(nodeExpanding)

        e.Node.EnsureVisible()
        e.Node.BackColor = Color.Gold

        isBeforeExpandIsRequire = False
        If isAfterSelectIsRequire Then TreeViewAggregate_AfterSelect(sender, New TreeViewEventArgs(e.Node, TreeViewAction.Expand))
        isBeforeExpandIsRequire = True
    End Sub

    ''' <summary>
    ''' Загрузить подузлы для родителя.
    ''' </summary>
    ''' <param name="node"></param>
    Private Sub AddSubDirectories(ByVal node As DirectoryNodeAggregate)
        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            cn.Open()

            For I As Integer = 0 To node.Nodes.Count - 1
                AddDirectories(CType(node.Nodes(I), DirectoryNodeAggregate), cn)
            Next
        End Using

        node.IsSubDirectoriesAdded = True
    End Sub

    ''' <summary>
    ''' Заполнить контрол активного узла свойствами.
    ''' </summary>
    ''' <param name="inTypeNode"></param>
    ''' <param name="inKey"></param>
    Private Sub FillAttributesControl(inTypeNode As TypeNode, inKey As Integer)
        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            cn.Open()
            Dim cmd As OleDbCommand = cn.CreateCommand
            cmd.CommandType = CommandType.Text

            Select Case inTypeNode
                Case TypeNode.Aggregate
                    cmd.CommandText = "SELECT Устройства1.* FROM Устройства1 WHERE Устройства1.KeyУстройства = " & inKey.ToString

                    Using rdr As OleDbDataReader = cmd.ExecuteReader
                        If Not rdr.HasRows Then
                            rdr.Close()
                            cn.Close()
                            Exit Sub
                        End If

                        If rdr.Read() Then
                            AddAttributesControl(inTypeNode, Convert.ToString(rdr("ИмяУстройства")))
                            ' заполнить значениями устройства
                            mAttributesControl(cDeviceName) = rdr("ИмяУстройства")
                            mAttributesControl(cDescription) = rdr("Описание")
                            mAttributesControl(cTarget) = rdr("Target")
                            mAttributesControl(cPortNumber) = rdr("НомерПорта")
                            mAttributesControl(cPortHighLevel) = rdr("ВысокийУровеньПорта")
                            mAttributesControl(cUnitOfMeasure) = rdr("ЕдиницаИзмерения")
                            mAttributesControl(cRangeOfChangingValueMin) = rdr("ДиапазонИзмененияMin")
                            mAttributesControl(cRangeOfChangingValueMax) = rdr("ДиапазонИзмененияMax")
                            mAttributesControl(cRangeYmin) = rdr("РазносУмин")
                            mAttributesControl(cRangeYmax) = rdr("РазносУмакс")
                            mAttributesControl(cAlarmValueMin) = rdr("АварийноеЗначениеМин")
                            mAttributesControl(cAlarmValueMax) = rdr("АварийноеЗначениеМакс")
                            mAttributesControl(cIsVisible) = rdr("Видимость")
                            mAttributesControl(cIsVisibleRegistration) = rdr("ВидимостьРегистратор")

                            ShowMessage($"Загружено <{rdr("ИмяУстройства")}> устройство: <{rdr("Target")}> порт: <{rdr("НомерПорта")}> описание: <{rdr("Описание")}>")
                        End If
                    End Using

                    SetEnabledOnEditMode(EditMode.TreeDeviceNodeSelectMode)
                    SystemConfigurationDevices.ClearTableLayoutPanelPort()
                    MarkLineIsBusy()
                Case TypeNode.Power
                    Dim devNumber As Integer
                    Dim newPort As Port = Nothing

                    cmd.CommandText = "SELECT Устройства1.* FROM Устройства1 WHERE Устройства1.KeyУстройства = " & keyAggregate.ToString
                    Using rdr As OleDbDataReader = cmd.ExecuteReader
                        If rdr.Read() Then
                            nameAggregateToLanch = CStr(rdr("ИмяУстройства"))
                        Else
                            nameAggregateToLanch = String.Empty
                            rdr.Close()
                            cn.Close()
                            Exit Sub
                        End If
                    End Using

                    ' полный запрос на всю иерархию Устройства1 -> ВеличинаЗагрузки2 -> ДискретныйВыход3
                    cmd.CommandText = "SELECT Устройства1.*, ВеличинаЗагрузки2.*, ДискретныйВыход3.* " &
                        "FROM (Устройства1 RIGHT JOIN ВеличинаЗагрузки2 ON Устройства1.KeyУстройства = ВеличинаЗагрузки2.KeyУстройства) RIGHT JOIN ДискретныйВыход3 ON ВеличинаЗагрузки2.keyВеличинаЗагрузки = ДискретныйВыход3.keyВеличинаЗагрузки " &
                        "WHERE ВеличинаЗагрузки2.keyВеличинаЗагрузки = " & inKey.ToString &
                        " ORDER BY ВеличинаЗагрузки2.ЧисловоеЗначение"

                    Using rdr As OleDbDataReader = cmd.ExecuteReader
                        If rdr.HasRows Then
                            Dim isFirstRow As Boolean = True

                            ' заполнить значениями мощности
                            Do While rdr.Read
                                If isFirstRow Then
                                    PopulateAttributesControlForPower(devNumber, newPort, rdr)
                                    isFirstRow = False
                                End If

                                Dim newLine As New Line(Convert.ToInt16(rdr("НомерЛинии")), True)
                                newLine.LineControl.Value = True
                                newPort.Add(newLine)
                            Loop
                        End If
                    End Using

                    ' если порт не создан, значит не добавлены линии порта.
                    ' повторный запрос на иерархию Устройства1 -> ВеличинаЗагрузки2
                    If newPort Is Nothing Then
                        cmd.CommandText = "SELECT Устройства1.*, ВеличинаЗагрузки2.* " &
                        "FROM Устройства1 RIGHT JOIN ВеличинаЗагрузки2 ON Устройства1.keyУстройства = ВеличинаЗагрузки2.keyУстройства " &
                        "WHERE ВеличинаЗагрузки2.keyВеличинаЗагрузки = " & inKey.ToString
                        Using rdr As OleDbDataReader = cmd.ExecuteReader
                            If rdr.Read() Then PopulateAttributesControlForPower(devNumber, newPort, rdr)
                        End Using
                    End If

                    ViewLineControl(New Devices From {New Device(devNumber) From {newPort}})
                    SetEnabledOnEditMode(EditMode.TreePowerNodeSelectMode)
            End Select
        End Using
    End Sub

    ''' <summary>
    ''' Заполнить контрол активного узла мощности загрузки свойствами.
    ''' </summary>
    ''' <param name="devNumber"></param>
    ''' <param name="newPort"></param>
    ''' <param name="rdr"></param>
    Private Sub PopulateAttributesControlForPower(ByRef devNumber As Integer, ByRef newPort As Port, rdr As OleDbDataReader)
        AddAttributesControl(TypeNode.Power, Convert.ToString(rdr("ВеличинаЗагрузки")))

        mAttributesControl(cChargeValueToText) = rdr("ВеличинаЗагрузки")
        mAttributesControl(cStageToNumeric) = rdr("ЧисловоеЗначение")
        mAttributesControl(cDescriptionCharge) = rdr("Примечание")

        devNumber = Convert.ToInt32(Convert.ToString(rdr("Target")).Replace("Dev", ""))
        Dim portNumber As Integer = Convert.ToInt16(rdr("НомерПорта"))
        newPort = New Port(portNumber)
        ShowMessage($"Загружена мощность: <{rdr("ВеличинаЗагрузки")}> числовое значение: <{rdr("ЧисловоеЗначение")}> примечание: <{rdr("Примечание")}>")
    End Sub

    ''' <summary>
    ''' Динамическое добавление в SplitContainerAggregate.Panel2.Controls формы отображения и настройки аттрибутов для выделенных узлов
    ''' Устройство или Величина.
    ''' </summary>
    ''' <param name="ТипУзла"></param>
    ''' <param name="inCaption"></param>
    Private Sub AddAttributesControl(ТипУзла As TypeNode, inCaption As String)
        RemoveAttributeControl()
        mAttributesControl = AttributeControlCreators(ТипУзла).GetAttributeControl(Me, inCaption)

        CType(SplitContainerAggregate, ISupportInitialize).BeginInit()
        SplitContainerAggregate.Panel2.SuspendLayout()
        SplitContainerAggregate.SuspendLayout()
        SplitContainerAggregate.Panel2.Enabled = False

        mAttributesControl.InitializeAttributeControl()

        SplitContainerAggregate.Panel2.Controls.Add(mAttributesControl)
        SplitContainerAggregate.Panel2.ResumeLayout(False)
        ' передёрнуть для перерисовки
        SplitContainerAggregate.SplitterDistance = SplitContainerAggregate.SplitterDistance + 1
        SplitContainerAggregate.SplitterDistance = SplitContainerAggregate.SplitterDistance - 1

        CType(SplitContainerAggregate, ISupportInitialize).EndInit()
        SplitContainerAggregate.ResumeLayout(False)
    End Sub

    ''' <summary>
    ''' Динамическое удаление в SplitContainerAggregate.Panel2.Controls формы отображения и настройки аттрибутов для выделенных узлов
    ''' Устройство или Величина.
    ''' </summary>
    Private Sub RemoveAttributeControl()
        If SplitContainerAggregate.Panel2.Controls.Count = 0 Then Exit Sub

        CType(Me.SplitContainerAggregate, ISupportInitialize).BeginInit()
        SplitContainerAggregate.Panel2.SuspendLayout()
        SplitContainerAggregate.SuspendLayout()

        SplitContainerAggregate.Panel2.Controls.RemoveAt(0)

        SplitContainerAggregate.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerAggregate, ISupportInitialize).EndInit()
        Me.SplitContainerAggregate.ResumeLayout(False)

        mAttributesControl = Nothing
    End Sub

    ''' <summary>
    ''' Синхронизировать включение линий для величины загрузки выбранного агрегата текущего изделия.
    ''' </summary>
    ''' <param name="comparisonDevices"></param>
    Private Sub ViewLineControl(comparisonDevices As Devices)
        SystemConfigurationDevices.SynchronizeValueLine(comparisonDevices)
        UpdateDevicesBindingSource()
        SystemConfigurationDevices.PopulateAfterChargeChanged(comparisonDevices)
    End Sub
#End Region

#Region "Управление устройствами и величинами"
#Region "Event procedure for OnRowUpdated"
    Private Sub OnRowUpdatedAggregate(ByVal sender As Object, ByVal args As OleDbRowUpdatedEventArgs)
        ' включить переменную и команду для возврата идентификатора значения из базы данных
        Dim idCMD As OleDbCommand = New OleDbCommand("SELECT @@IDENTITY", connection)

        If args.StatementType = StatementType.Insert Then
            ' вернуть идентификатор значения и сохранить в соотвествующей колонке
            keyAggregate = CInt(idCMD.ExecuteScalar())
            ' продублировать, если сразу после создания будет редактировано и заново записано
            args.Row("KeyУстройства") = keyAggregate
        End If
    End Sub

    Private Sub OnRowUpdatedPower(ByVal sender As Object, ByVal args As OleDbRowUpdatedEventArgs)
        ' включить переменную и команду для возврата идентификатора значения из базы данных
        Dim idCMD As OleDbCommand = New OleDbCommand("SELECT @@IDENTITY", connection)

        If args.StatementType = StatementType.Insert Then
            ' вернуть идентификатор значения и сохранить в соотвествующей колонке
            keyPower = CInt(idCMD.ExecuteScalar())
            ' продублировать, если сразу после создания будет редактировано и заново записано
            args.Row("keyВеличинаЗагрузки") = keyPower
        End If
    End Sub
#End Region

    Private Sub TSButtonAddAggregate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonAddAggregate.Click
        AddAttributesControl(TypeNode.Aggregate, "Добавление нового агрегата")
        SetEnabledOnEditMode(EditMode.AddDeviceMode)
    End Sub

    Private Sub TSButtonEditAggregate_CheckedChanged(sender As Object, e As EventArgs) Handles TSButtonEditAggregate.CheckedChanged
        EditAggregate()
    End Sub

    Private Sub TSButtonSaveAggregate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonSaveAggregate.Click
        SaveAggregate()
    End Sub

    Private Sub TSButtonRemoveAggregate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonRemoveAggregate.Click
        RemoveAggregate()
    End Sub

    Private Sub TSButtonAddPower_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonAddPower.Click
        AddPower()
    End Sub

    ''' <summary>
    ''' Редактировать величину мощности
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TSButtonEditPower_CheckedChanged(sender As Object, e As EventArgs) Handles TSButtonEditPower.CheckedChanged
        If TSButtonEditPower.Checked Then
            SetEnabledOnEditMode(EditMode.EditPowerModeOn)
        Else
            SetEnabledOnEditMode(EditMode.EditPowerModeOff)
        End If
        ShowMessage("Можно редактировать величину...")
    End Sub

    Private Sub TSButtonSavePower_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonSavePower.Click
        SavePower()
    End Sub

    Private Sub TSButtonRemovePower_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonRemovePower.Click
        RemovePower()
    End Sub

    Private Sub TSButtonLaunchPower_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TSButtonLaunchPower.CheckedChanged
        LaunchPower()
    End Sub

    ''' <summary>
    ''' Исполнить величину загрузки.
    ''' </summary>
    Private Sub LaunchPower()
        If TSButtonLaunchPower.Checked Then
            Dim nodeSelected As DirectoryNodeAggregate = CType(TreeViewAggregate.SelectedNode, DirectoryNodeAggregate)
            If nodeSelected IsNot Nothing AndAlso nodeSelected.NodeType = TypeNode.Power AndAlso nameAggregateToLanch <> String.Empty Then
                If Not isPowerCorrect Then IsCheckPowerCorrect()

                If isPowerCorrect AndAlso mDigitalWriter.SetupDigitalWriteTask(True) Then
                    mDigitalWriter.WritePortLinesByPower()
                    ShowMessage($"Устройство {nameAggregateToLanch} выполняет выделенную величину загрузки.")
                    TSButtonLaunchPower.Image = My.Resources.PowerOn
                    TSButtonLaunchPower.ToolTipText = "Сбросить мощность загрузки в <0>"
                    TSTextBoxPowerOn.Text = $"Включена величина загрузки <{nodeSelected.Text.Remove(0, POWER.Length)}>"
                    TSTextBoxPowerOn.Visible = TSButtonLaunchPower.Checked
                    SetEnabledOnEditMode(EditMode.LaunchPowerModeOn)
                End If
            End If
        Else
            SetAllLinesPortOff()
            ShowMessage("Все устройства загружены нулевой величиной загрузки")
            TSButtonLaunchPower.Image = My.Resources.PowerOff
            TSButtonLaunchPower.ToolTipText = "Исполнить выбранную величину мощности загрузки"
            TSTextBoxPowerOn.Visible = TSButtonLaunchPower.Checked
            SetEnabledOnEditMode(EditMode.LaunchPowerModeOff)
        End If
    End Sub

    ''' <summary>
    ''' Установить все линии порта устройства в нуль.
    ''' </summary>
    Private Sub SetAllLinesPortOff()
        If Not isPowerCorrect Then IsCheckPowerCorrect()
        If isPowerCorrect Then mDigitalWriter.SetAllLinesPortOff()
    End Sub

    ''' <summary>
    ''' Запмсать настройки агрегата.
    ''' </summary>
    Private Sub SaveAggregate()
        Dim strSQL As String
        Dim odaDataAdapter As OleDbDataAdapter
        Dim dtDataTable As New DataTable
        Dim cb As OleDbCommandBuilder
        Dim newAggregateName As String = mAttributesControl(cDeviceName).ToString

        If Not IsCheckFillAttributesControl() Then Exit Sub

        ' записать устройство в базу и добавить в дерево и в список устройств
        ' проверить нет ли совпадающих имен (по списку проще)        
        If Aggregates.Contains(newAggregateName) Then
            Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
                Try
                    cn.Open()
                    strSQL = "SELECT Устройства1.* FROM Устройства1 WHERE Устройства1.KeyУстройства = " & keyAggregate.ToString
                    odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
                    odaDataAdapter.Fill(dtDataTable)

                    If dtDataTable.Rows.Count <> 0 Then
                        UpdateNewRowDevice(dtDataTable.Rows(0))
                        cb = New OleDbCommandBuilder(odaDataAdapter)
                        odaDataAdapter.Update(dtDataTable)
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.ToString, $"Ошибка обновления устройства в процедуре <{NameOf(SaveAggregate)}>.",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End Try
            End Using

            ShowMessage($"Сохранение изменний в устройстве {newAggregateName} успешно произведено.")
        Else
            Try
                connection = New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
                strSQL = "SELECT Устройства1.* FROM Устройства1;"
                odaDataAdapter = New OleDbDataAdapter(strSQL, connection)
                odaDataAdapter.Fill(dtDataTable)

                Dim newDataRow As DataRow = dtDataTable.NewRow
                UpdateNewRowDevice(newDataRow)
                dtDataTable.Rows.Add(newDataRow)
                ' включить событие заполнения автонумерации для новой записи
                AddHandler odaDataAdapter.RowUpdated, New OleDbRowUpdatedEventHandler(AddressOf OnRowUpdatedAggregate)
                cb = New OleDbCommandBuilder(odaDataAdapter)
                odaDataAdapter.Update(dtDataTable)
            Catch ex As Exception
                MessageBox.Show(ex.ToString, $"Ошибка добавления устройства в процедуре <{NameOf(SaveAggregate)}>.",
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Finally
                If connection.State = ConnectionState.Open Then connection.Close()
            End Try

            Aggregates.Add(newAggregateName)

            ' добавить в дерево в последнюю очередь т.к. вызывает события которые обновляют Key
            For Each itemTreeNode As TreeNode In TreeViewAggregate.Nodes
                If itemTreeNode.IsExpanded Then itemTreeNode.Collapse()
            Next
            Dim cRoot As New DirectoryNodeAggregate("Устройство-" & newAggregateName, TypeNode.Aggregate, keyAggregate) With {
                        .SelectedImageIndex = 0,
                        .ImageIndex = 1
                    }
            TreeViewAggregate.Nodes.Add(cRoot)
            TreeViewAggregate.SelectedNode = cRoot
            cRoot.EnsureVisible()
            ShowMessage($"Устройство с именем {newAggregateName} успешно создано.")
        End If ' проверка существования устройства

        TSButtonEditAggregate.Checked = False
        SetEnabledOnEditMode(EditMode.SaveDeviceMode)
    End Sub

    ''' <summary>
    ''' Проверка на отсутствие незаполненых полей и правильности приведения типа.
    ''' </summary>
    ''' <returns></returns>
    Private Function IsCheckFillAttributesControl() As Boolean
        Dim success As Boolean

        Try
            Dim port As Integer = Convert.ToInt16(mAttributesControl(cPortNumber))
            success = mAttributesControl(cDeviceName).ToString() <> String.Empty AndAlso mAttributesControl(cDescription).ToString() <> String.Empty AndAlso mAttributesControl(cTarget).ToString() <> String.Empty
        Catch ex As Exception
        Finally
            If Not success Then
                Dim message As String = "Должны быть заполнены поля <Имя устройства>, <Описание>, <Имя оборудования>, <Номер Порта>!"
                MessageBox.Show(message, "Проверка добавляемого устройства", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ShowMessage(message)
            End If
        End Try

        Return success
    End Function

    ''' <summary>
    ''' Заполнить поля новой записи в таблице соответствующими полями контрола агрегата.
    ''' Значения некоторых полей определяются автоматически.
    ''' </summary>
    ''' <param name="newDataRow"></param>
    Private Sub UpdateNewRowDevice(ByRef newDataRow As DataRow)
        mAttributesControl.AdjustProperties()
        newDataRow.BeginEdit()
        newDataRow("ИмяУстройства") = mAttributesControl(cDeviceName).ToString
        newDataRow("Описание") = mAttributesControl(cDescription).ToString
        newDataRow("Target") = mAttributesControl(cTarget).ToString
        newDataRow("НомерПорта") = Convert.ToInt16(mAttributesControl(cPortNumber))
        newDataRow("ВысокийУровеньПорта") = Convert.ToInt16(mAttributesControl(cPortHighLevel))
        newDataRow("ЕдиницаИзмерения") = mAttributesControl(cUnitOfMeasure).ToString
        newDataRow("ДиапазонИзмененияMin") = Convert.ToSingle(mAttributesControl(cRangeOfChangingValueMin))
        newDataRow("ДиапазонИзмененияMax") = Convert.ToSingle(mAttributesControl(cRangeOfChangingValueMax))
        newDataRow("РазносУмин") = Convert.ToInt32(mAttributesControl(cRangeYmin))
        newDataRow("РазносУмакс") = Convert.ToInt32(mAttributesControl(cRangeYmax))
        newDataRow("АварийноеЗначениеМин") = Convert.ToDouble(mAttributesControl(cAlarmValueMin))
        newDataRow("АварийноеЗначениеМакс") = Convert.ToDouble(mAttributesControl(cAlarmValueMax))
        newDataRow("Видимость") = Convert.ToBoolean(mAttributesControl(cIsVisible))
        newDataRow("ВидимостьРегистратор") = Convert.ToBoolean(mAttributesControl(cIsVisibleRegistration))
        newDataRow.EndEdit()
    End Sub

    ''' <summary>
    ''' Настроить окно для возможности редактирования агрегата.
    ''' </summary>
    Private Sub EditAggregate()
        If TSButtonEditAggregate.Checked Then
            SetEnabledOnEditMode(EditMode.EditDeviceModeOn)
        Else
            SetEnabledOnEditMode(EditMode.EditDeviceModeOff)
        End If

        If TSButtonEditAggregate.Checked Then
            nodeKey = 0
            ' найти по выделенному узлу дерева ID устройства
            For Each lNode As TreeNode In TreeViewAggregate.Nodes
                If lNode.IsExpanded OrElse lNode.IsSelected Then
                    nodeKey = CType(lNode, DirectoryNodeAggregate).KeyId
                    Exit For
                End If
            Next

            If nodeKey = 0 Then ' ID устройства не найдено и при записи можно обновлять
                SetEnabledOnEditMode(EditMode.EditDeviceModeOff)
                Dim message As String = "Необходимо выделить устройство в проводнике!"
                ShowMessage(message)
                MessageBox.Show(message, "Редактирование устройства", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' ID устройства найдено и при записи можно обновлять
                ShowMessage("Можно редактировать...")
            End If
        Else
            ShowMessage("Редактирование успешно завершено.")
        End If
    End Sub

    ''' <summary>
    ''' Удалить выделенный агрегат.
    ''' </summary>
    Private Sub RemoveAggregate()
        Dim removingDevice As String = String.Empty

        nodeKey = 0
        ' найти по выделенному узлу дерева ID устройства
        ' удалить из дерева и удалить из списка
        For Each itemNode As TreeNode In TreeViewAggregate.Nodes
            If itemNode.IsSelected Then
                nodeKey = CType(itemNode, DirectoryNodeAggregate).KeyId
                TreeViewAggregate.Nodes.Remove(itemNode)
                removingDevice = itemNode.Text.Remove(0, "Устройство-".Length)
                Aggregates.Remove(removingDevice)
                Exit For
            End If
        Next

        SetEnabledOnEditMode(EditMode.RemoveDeviceMode)

        If nodeKey = 0 Then ' ID устройства не найдено и при записи можно обновлять
            Dim message As String = "Необходимо выделить устройство в проводнике!"
            ShowMessage(message)
            MessageBox.Show(message, "Удаление устройства", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else ' удалить из базы данных
            Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
                Using cmd As OleDbCommand = cn.CreateCommand()
                    cmd.CommandType = CommandType.Text

                    Try
                        cn.Open()
                        cmd.CommandText = "DELETE Устройства1.* FROM(Устройства1) WHERE Устройства1.KeyУстройства = " & nodeKey 'KeyУстройства
                        cmd.ExecuteNonQuery()
                        cmd.CommandText = $"DELETE ЦиклЗагрузки3.* FROM(ЦиклЗагрузки3) WHERE ЦиклЗагрузки3.ИмяУстройства = '{removingDevice}'"
                        cmd.ExecuteNonQuery()
                        ShowMessage($"Удаление устройства <{removingDevice}> успешно произведено.")
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString, $"Ошибка удаление устройства в процедуре: <{NameOf(RemoveAggregate)}>", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End Try
                End Using
            End Using

            PopulateAggregateTree()
        End If
    End Sub

    ''' <summary>
    ''' Настроить окно для возможности редактирования при добавлении новой величину мощности для текущего агрегата.
    ''' </summary>
    Private Sub AddPower()
        Dim devNumber As Integer
        Dim portNumber As Integer

        AddAttributesControl(TypeNode.Power, "Добавление новой величины загрузки")
        SetEnabledOnEditMode(EditMode.AddPowerMode)
        keyAggregate = 0

        For Each itemNode As TreeNode In TreeViewAggregate.Nodes
            If itemNode.IsExpanded OrElse itemNode.IsSelected Then ' AndAlso itemNode.IsSelected
                keyAggregate = CType(itemNode, DirectoryNodeAggregate).KeyId
                Exit For
            End If
        Next

        If keyAggregate = 0 Then
            Throw New Exception("KeyУстройства = 0")
        End If

        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            cn.Open()
            Dim cmd As OleDbCommand = cn.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SELECT Устройства1.* FROM Устройства1 WHERE Устройства1.KeyУстройства = " & keyAggregate.ToString

            Using rdr As OleDbDataReader = cmd.ExecuteReader
                If Not rdr.HasRows Then
                    rdr.Close()
                    cn.Close()
                    Exit Sub
                End If

                If rdr.Read() Then
                    devNumber = Convert.ToInt32(Convert.ToString(rdr("Target")).Replace("Dev", ""))
                    portNumber = Convert.ToInt16(rdr("НомерПорта"))
                End If
            End Using
        End Using

        ViewLineControl(New Devices From {New Device(devNumber) From {New Port(portNumber)}})
    End Sub

    ''' <summary>
    ''' Сохранение отредактированной новой величины мощности для текущего агрегата.
    ''' </summary>
    Private Sub SavePower()
        Dim odaDataAdapter As OleDbDataAdapter
        Dim dtDataTable As New DataTable
        Dim drDataRow As DataRow
        Dim cb As OleDbCommandBuilder
        Dim nodeDevice As DirectoryNodeAggregate = Nothing
        Dim isNodeRootFounded, isPowerExist As Boolean
        Dim nameCurrentAggregate As String = Nothing ' Имя устройства куда добавляется величина
        Dim newTextPover As String = mAttributesControl(cChargeValueToText).ToString
        Dim newNumericPower As Single = Convert.ToSingle(mAttributesControl(cStageToNumeric))
        Dim newDescription As String = mAttributesControl(cDescriptionCharge).ToString

        ' проверка на существование
        If newTextPover = String.Empty OrElse newDescription = String.Empty Then
            Dim message As String = "Должны быть заполнены поля <Величины Загрузки> и <Примечание>!"
            MessageBox.Show(message, "Проверка величины загрузки", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ShowMessage(message)
            Exit Sub
        End If

        For Each itemNode As TreeNode In TreeViewAggregate.Nodes
            nodeDevice = CType(itemNode, DirectoryNodeAggregate)
            If nodeDevice.KeyId = keyAggregate Then
                nameCurrentAggregate = itemNode.Text.Remove(0, "Устройство-".Length)
                isNodeRootFounded = True
                Exit For
            End If
        Next

        If Not isNodeRootFounded Then
            Dim message As String = $"Данное устройство ID = {keyAggregate} в дереве устройств не найдено!"
            ShowMessage(message)
            MessageBox.Show(message, "Добавление новой величины загрузки.", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        Try
            connection = New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            Using cmd As OleDbCommand = connection.CreateCommand()
                Dim strSQL As String = "Select ВеличинаЗагрузки2.* FROM(ВеличинаЗагрузки2) WHERE (((ВеличинаЗагрузки2.ВеличинаЗагрузки) = '" & newTextPover & "' ) AND ((ВеличинаЗагрузки2.KeyУстройства) = " & keyAggregate & " ));"
                cmd.CommandType = CommandType.Text
                cmd.CommandText = strSQL
                connection.Open()

                Using rdr As OleDbDataReader = cmd.ExecuteReader
                    isPowerExist = rdr.HasRows
                End Using

                If isPowerExist Then
                    Dim message As String = $"Величина загрузки со значением <{newTextPover}> для данного устройства существует. Производится модификация..."
                    ShowMessage(message)
                    odaDataAdapter = New OleDbDataAdapter(strSQL, connection)
                    odaDataAdapter.Fill(dtDataTable)

                    If dtDataTable.Rows.Count <> 0 Then
                        UpdateRowPower(newTextPover, newNumericPower, newDescription, dtDataTable.Rows(0))
                        cb = New OleDbCommandBuilder(odaDataAdapter)
                        odaDataAdapter.Update(dtDataTable)
                    End If

                    strSQL = "DELETE ДискретныйВыход3.* FROM(ДискретныйВыход3) WHERE ДискретныйВыход3.keyВеличинаЗагрузки = " & keyPower
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()
                Else
                    ' предыдущий запрос проверил на отсутствие новой величины, значит можно добавить эту величину
                    odaDataAdapter = New OleDbDataAdapter(strSQL, connection)
                    odaDataAdapter.Fill(dtDataTable)

                    Dim newDataRow As DataRow = dtDataTable.NewRow
                    UpdateRowPower(newTextPover, newNumericPower, newDescription, newDataRow)
                    dtDataTable.Rows.Add(newDataRow)
                    ' включить событие заполнения автонумерации для новой записи
                    AddHandler odaDataAdapter.RowUpdated, New OleDbRowUpdatedEventHandler(AddressOf OnRowUpdatedPower)

                    cb = New OleDbCommandBuilder(odaDataAdapter)
                    odaDataAdapter.Update(dtDataTable)
                End If

                ' величина загрузки существует, вставка новых строк
                strSQL = "SELECT ДискретныйВыход3.* FROM(ДискретныйВыход3) WHERE ДискретныйВыход3.keyВеличинаЗагрузки = " & keyPower
                odaDataAdapter = New OleDbDataAdapter(strSQL, connection)
                odaDataAdapter.Fill(dtDataTable)
                ' таблица пустая
                For Each itemLine As Line In SystemConfigurationDevices.DeviceBindingList(SystemConfigurationDevices.DeviceSelectedIndex).PortBindingList(SystemConfigurationDevices.PortSelectedIndex).LineBindingList
                    If itemLine.IsEnabled AndAlso Not itemLine.IsBusy AndAlso itemLine.LineControl.Value Then
                        drDataRow = dtDataTable.NewRow
                        drDataRow.BeginEdit()
                        drDataRow("keyВеличинаЗагрузки") = keyPower
                        drDataRow("НомерЛинии") = itemLine.Number
                        drDataRow("ЗначениеЛогики") = True
                        drDataRow.EndEdit()
                        dtDataTable.Rows.Add(drDataRow)
                    End If
                Next

                cb = New OleDbCommandBuilder(odaDataAdapter)
                odaDataAdapter.Update(dtDataTable)

                If Not IsCheckZeroPowerExist(connection) Then
                    Dim message As String = "Числовое значение загрузки со значением равным '0' для данного устройства не определена!"
                    ShowMessage(message)
                    MessageBox.Show(message & vbCrLf & "Обязательно введите эту величину!", "Проверка нулевой величины", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using

            If isPowerExist Then
                '' модифицировать в дереве
                'For Each itemNode As TreeNode In nodeDevice.Nodes
                '    If CType(itemNode, DirectoryNodeУстройство).Text = Величина & НоваяВеличинаЗагрузки Then
                '        itemNode.EnsureVisible()
                '        Exit For
                '    End If
                'Next
                nodeDevice.EnsureVisible()
            Else
                ' добавить в дерево
                nodeDevice.Nodes.Add(New DirectoryNodeAggregate(POWER & newTextPover, TypeNode.Power, keyPower))
                nodeDevice.LastNode.SelectedImageIndex = 0
                nodeDevice.LastNode.ImageIndex = TypeNode.Power
                nodeDevice.EnsureVisible()
                TreeViewAggregate.SelectedNode = nodeDevice.LastNode 'вызывает срабатывание события
            End If

            ShowMessage($"Величина загрузки со значением {newTextPover} в устройство <{nameCurrentAggregate}> успешно добавлена.")
        Catch ex As Exception
            MessageBox.Show(ex.ToString, $"Ошибка величины загрузки в процедуре <{NameOf(SavePower)}>.",
                MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            If connection.State = ConnectionState.Open Then connection.Close()
            TSButtonEditPower.Checked = False
            SetEnabledOnEditMode(EditMode.SavePowerMode)
        End Try
    End Sub

    ''' <summary>
    ''' Заполнить поля записи в таблице соответствующими полями мощности.
    ''' </summary>
    ''' <param name="inTextPover"></param>
    ''' <param name="inNumericPower"></param>
    ''' <param name="inDescription"></param>
    ''' <param name="editedDataRow"></param>
    Private Sub UpdateRowPower(inTextPover As String, inNumericPower As Single, inDescription As String, ByRef editedDataRow As DataRow)
        editedDataRow.BeginEdit()
        editedDataRow("KeyУстройства") = keyAggregate
        editedDataRow("ВеличинаЗагрузки") = inTextPover
        editedDataRow("ЧисловоеЗначение") = inNumericPower
        editedDataRow("Примечание") = inDescription
        editedDataRow.EndEdit()
    End Sub

    ''' <summary>
    ''' Удалить выделенную мощность текущего агрегата.
    ''' </summary>
    Private Sub RemovePower()
        'keyВеличинаЗагрузки определен после выбора в дереве
        Dim nameCurrentAggregate As String = String.Empty ' имя устройства откуда удаляется величина мощности
        Dim selectedPower As String = String.Empty ' Удаляемая Величина
        Dim isNodeRootFounded As Boolean
        Dim nodePowerToRemove As TreeNode = Nothing

        ' найти по выделенному узлу дерева ID устройства
        For Each itemNodeDevice As TreeNode In TreeViewAggregate.Nodes
            If itemNodeDevice.IsExpanded OrElse itemNodeDevice.IsSelected Then
                For Each itemNodePower As TreeNode In itemNodeDevice.Nodes
                    nodeKey = CType(itemNodePower, DirectoryNodeAggregate).KeyId

                    If keyPower = nodeKey Then
                        isNodeRootFounded = True ' узел с величиной загрузки найден
                        nameCurrentAggregate = itemNodeDevice.Text.Remove(0, "Устройство-".Length)
                        selectedPower = itemNodePower.Text.Remove(0, POWER.Length)
                        nodePowerToRemove = itemNodePower
                        Exit For
                    End If
                Next
            End If

            If isNodeRootFounded Then Exit For
        Next

        If isNodeRootFounded Then ' ID устройства не найдено и при записи можно обновлять
            Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
                Using cmd As OleDbCommand = cn.CreateCommand()
                    cmd.CommandType = CommandType.Text

                    Try
                        cn.Open()
                        cmd.CommandText = "DELETE ВеличинаЗагрузки2.* FROM(ВеличинаЗагрузки2) WHERE ВеличинаЗагрузки2.keyВеличинаЗагрузки = " & keyPower
                        cmd.ExecuteNonQuery()
                        cmd.CommandText = $"DELETE ПерекладкиЦикла4.* FROM ПерекладкиЦикла4 Where (((ПерекладкиЦикла4.ВеличинаЗагрузки) ='{selectedPower}') AND ((ПерекладкиЦикла4.keyУстройства)={keyAggregate}));"
                        cmd.ExecuteNonQuery()
                        ShowMessage($"Удаление величины загрузки равной <{selectedPower}> из устройства <{nameCurrentAggregate}> призведено успешно.")
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString, $"Ошибка удаление величины загрузки из базы в процедуре: <{NameOf(RemovePower)}>",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End Try
                End Using
            End Using

            nodePowerToRemove.Remove() ' удалить из дерева
        Else
            SetEnabledOnEditMode(EditMode.RemovePowerMode)
            Dim message As String = "Необходимо выделить устройство в проводнике для удаляемой величины!"
            ShowMessage(message)
            MessageBox.Show(message, "Удаление величины", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ''' <summary>
    ''' Проверить общую правильность насттройки.
    ''' Проверить, что для устройства определен "0" величины загрузки.
    ''' </summary>
    Private Sub IsCheckPowerCorrect()
        Dim messageResult As New StringBuilder("Были обнаружены следующие проблемы:")

        isPowerCorrect = True

        Using cn As New OleDbConnection(BuildCnnStr(PROVIDER_JET, MainFomMdiParent.PathDBaseCycle))
            cn.Open()
            If Not IsCheckZeroPowerExist(cn) Then
                isPowerCorrect = False
                messageResult.AppendLine($"Для устройства {nameAggregateToLanch} не найдено нулевое значение загрузки.")

                Dim message As String = "Исполнение мощности загрузки невозможно до устранения всех ошибок!"
                ShowMessage(message)
                messageResult.AppendLine(message)
                MessageBox.Show(messageResult.ToString, "Обнаружены следующие проблемы...", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Проверить содержится ли нулевое значение величины загрузки.
    ''' </summary>
    ''' <param name="cn"></param>
    ''' <returns></returns>
    Private Function IsCheckZeroPowerExist(ByRef cn As OleDbConnection) As Boolean
        Dim success As Boolean
        Dim cmd As OleDbCommand = cn.CreateCommand

        cmd.CommandType = CommandType.Text
        cmd.CommandText = "SELECT ВеличинаЗагрузки2.ВеличинаЗагрузки, ВеличинаЗагрузки2.ЧисловоеЗначение " &
                "FROM ВеличинаЗагрузки2 " &
                "WHERE (((ВеличинаЗагрузки2.ЧисловоеЗначение)=0) AND ((ВеличинаЗагрузки2.keyУстройства)= " & keyAggregate.ToString & "));"

        Using rdr As OleDbDataReader = cmd.ExecuteReader
            success = rdr.HasRows
        End Using

        Return success
    End Function
#End Region

#Region "Тест мощности"
    'Private Sub RadioButtonCharge1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonCharge1.CheckedChanged
    '    ChargeChanged(1)
    'End Sub
    'Private Sub RadioButtonCharge2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonCharge2.CheckedChanged
    '    ChargeChanged(2)
    'End Sub

    'Private Sub ChargeChanged(numberCharge As Integer)
    '    Dim comparisonDevices As Devices = SystemConfigurationDevices.GetTestLineValueOnDevices(numberCharge)
    '    SystemConfigurationDevices.SynchronizeValueLine(comparisonDevices)
    '    UpdateDevicesBindingSource()
    '    SystemConfigurationDevices.PopulateAfterChargeChanged(comparisonDevices)
    'End Sub
#End Region
End Class


'Public Shared Sub SetData(ByVal Optional dataObjectTransformer As Func(Of DataObject, DataObject) = Nothing,
'                              ByVal Optional commitActionTransformer As Func(Of DataObject, Action, Action) = Nothing)
'    Clipboard.Clear()
'    Dim data = New DataObject()
'    If dataObjectTransformer IsNot Nothing Then data = dataObjectTransformer(data)
'    Dim commitAction As Action = Sub() Clipboard.SetDataObject(data, True)
'    If commitActionTransformer IsNot Nothing Then commitAction = commitActionTransformer(data, commitAction)
'    commitAction()
'End Sub
