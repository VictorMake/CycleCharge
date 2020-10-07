Imports NationalInstruments.DAQmx
Imports System.Threading

''' <summary>
''' Класс определения состава оборудования.
''' </summary>
Public Class SystemDevices
    Public ReadOnly Property Devices As New List(Of String)
    Public ReadOnly Property PhysicalPorts As New List(Of String)
    Public ReadOnly Property PhysicalChannels As New List(Of String)
    Public ReadOnly Property LinesCount As New List(Of Integer)
    Public ReadOnly Property LinesCountString As New List(Of String)
    Public ReadOnly Property DigitalWriteTasks As New List(Of String)
    Public ReadOnly Property SystemDODevices As New Devices()
    Public ReadOnly Property LastError As String
        Get
            Return mLastError
        End Get
    End Property

    Private SuccessPhysicalChannels As New Dictionary(Of String, Boolean)
    Private mLastError As String

    Public Sub New()
        PopulateDeviceDictionary()
    End Sub

    ''' <summary>
    ''' Узнать Наличие Железа
    ''' </summary>
    Private Sub PopulateDeviceDictionary()
        ' узнать конфигурацию платы
        '(Led.Tag = "SC1Mod<slot#>/port" & mНомерПорта.ToString & "/line" & i.ToString) '"SC" & НомерУстройства & "Mod<slot#>/port0/lineN" 'Dev0/port1/line0:2
        '"SC" & НомерУстройства может ссылаться на на "Dev" & НомерУстройства 
        ' поэтому если есть ссылка "SC" & НомерУстройства = "Dev" & НомерУстройства , то порты с "Dev" не показывать
        Devices.AddRange(DaqSystem.Local.Devices)
        'PhysicalChannelTypes.DOPort Or PhysicalChannelTypes.DIPort
        PhysicalPorts.AddRange(DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.DOPort, PhysicalChannelAccess.External).Where(Function(nameDev) Not nameDev.StartsWith("Sim")).ToArray)
        Dim wordsPhysicalChannel As String() = DaqSystem.Local.GetPhysicalChannels(PhysicalChannelTypes.DOLine, PhysicalChannelAccess.External).Where(Function(nameDev) Not nameDev.StartsWith("Sim")).ToArray
        PhysicalChannels.AddRange(wordsPhysicalChannel)
        DetermineLinesCount(wordsPhysicalChannel)
        CheckCountsDigitalWriteTask()
    End Sub

    ''' <summary>
    ''' Определить число линий в портах.
    ''' </summary>
    ''' <param name="wordsPhysicalChannel"></param>
    Private Sub DetermineLinesCount(ByVal wordsPhysicalChannel() As String)
        If PhysicalPorts.Count > 0 Then
            For I As Integer = 0 To PhysicalPorts.Count - 1
                Dim index As Integer = I ' чтобы отладчик не ругался
                Dim wordGroups = From word In wordsPhysicalChannel
                                 Where word.IndexOf(PhysicalPorts(index)) <> -1
                                 Group word By Key = "line" Into Count()
                                 Select ProductCount = Count

                For Each w In wordGroups
                    LinesCount.Add(w)
                    LinesCountString.Add(w)
                Next
            Next
        End If
    End Sub

    ''' <summary>
    ''' Определить Число Возможных DigitalWriteTask
    ''' </summary>
    Private Sub CheckCountsDigitalWriteTask()
        Dim I, J As Integer

        If Devices.Count > 0 AndAlso PhysicalPorts.Count > 0 Then
            For I = 0 To Devices.Count - 1
                If Devices(I).IndexOf("Dev") <> -1 Then
                    For J = 0 To PhysicalPorts.Count - 1
                        If PhysicalPorts(J).IndexOf(Devices(I)) <> -1 Then
                            DigitalWriteTasks.Add(Devices(I))
                            Exit For
                        End If
                    Next
                ElseIf Devices(I).IndexOf("SC") <> -1 Then
                    For J = 0 To PhysicalPorts.Count - 1
                        If PhysicalPorts(J).IndexOf(Devices(I)) <> -1 Then
                            DigitalWriteTasks.Add(Devices(I))
                            Exit For
                        End If
                    Next
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Сканирование всех портов вывода для опредения доступных для управления.
    ''' Создать кофигурацию оборудования для передачи компоненту ConfigurationDevices.TableLineControl
    ''' </summary>
    Public Async Function CheckWriteAllLines() As Tasks.Task
        If Devices.Count = 0 OrElse PhysicalPorts.Count = 0 Then Exit Function

        Await CheckWriteAllAsync()
        CreateConfigurationDevices()
    End Function

    ''' <summary>
    ''' Задача сканирования всех портов вывода для опредения доступных для управления.
    ''' </summary>
    ''' <returns></returns>
    Private Function CheckWriteAllAsync() As Tasks.Task
        Return Tasks.Task.Run(Async Function()
                                  For Each itemPhysicalChannel As String In PhysicalChannels
                                      Dim success As Boolean = Await CheckWriteOneTask(itemPhysicalChannel)

                                      SuccessPhysicalChannels(itemPhysicalChannel) = success
                                      Debug.WriteLine(String.Format("Для линии порта {0} результат опроса равен: {1}", $"{itemPhysicalChannel}", success))
                                  Next
                              End Function)
    End Function

    ''' <summary>
    ''' Задача проверки конкретного порта.
    ''' </summary>
    ''' <param name="lines"></param>
    ''' <returns></returns>
    Private Function CheckWriteOneTask(lines As String) As Tasks.Task(Of Boolean)
        Return Tasks.Task.Run(Function()
                                  Return CheckWriteOneLineAsync(lines)
                              End Function)
    End Function

    ''' <summary>
    ''' Асинхронный попытка записи 1 -> 0 в линию порта дискретного выхода.
    ''' </summary>
    ''' <param name="lines">содержится имя линии цифрового порта</param>
    ''' <returns></returns>
    Private Async Function CheckWriteOneLineAsync(lines As String) As Tasks.Task(Of Boolean)
        Dim digitalWriteTask As Task = New Task("digital")
        Dim success As Boolean = False

        Try
            digitalWriteTask.DOChannels.CreateChannel(lines, "", ChannelLineGrouping.OneChannelForAllLines)
            '  Запись в цифровой порт значение. WriteDigitalSingChanSingSampPort записывает набор данных
            '  или цифроваые данные по требованию, таким образом в timeout нет необходимости.
            Dim writer As DigitalSingleChannelWriter = New DigitalSingleChannelWriter(digitalWriteTask.Stream)
            digitalWriteTask.Control(TaskAction.Verify)
            writer.WriteSingleSampleMultiLine(True, {True})
            Await Tasks.Task.Delay(50)
            writer.WriteSingleSampleMultiLine(True, {False})
            success = True
        Catch ex As Exception
            'Dim caption As String = $"Процедура <CheckWriteOneLineTask>"
            'Dim text As String = ex.ToString
            'MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            mLastError = ex.ToString ' можно вывести в панель сообщений
        Finally
            digitalWriteTask.Dispose()
        End Try

        Return success
    End Function

    ''' <summary>
    ''' Создать кофигурацию оборудования.
    ''' </summary>
    Private Sub CreateConfigurationDevices()
        For Each itemDigitalWriteTask As String In DigitalWriteTasks
            Dim newDevice As New Device(Convert.ToInt32(itemDigitalWriteTask.Replace("Dev", "")))

            For Each itemPhysicalPort As String In PhysicalPorts
                If itemPhysicalPort.Contains(itemDigitalWriteTask) Then
                    Dim newPort As New Port(Convert.ToInt32(itemPhysicalPort.Replace($"{itemDigitalWriteTask}/port", "")))

                    For Each itemPhysicalChannel As String In PhysicalChannels
                        If itemPhysicalChannel.Contains($"{itemDigitalWriteTask}/{newPort.ToString}") Then
                            newPort.Add(Convert.ToInt32(itemPhysicalChannel.Replace($"{itemDigitalWriteTask}/{newPort.ToString}/line", "")),
                                        SuccessPhysicalChannels(itemPhysicalChannel))
                        End If
                    Next

                    newDevice.Add(newPort)
                End If
            Next

            SystemDODevices.Add(newDevice)
        Next
    End Sub
End Class