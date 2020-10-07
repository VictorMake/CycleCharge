Imports System.Text
Imports System.Windows.Forms
Imports NationalInstruments.DAQmx

''' <summary>
''' Работа с дискретными устройствами оборудования компьютера.
''' </summary>
Friend Class DigitalSingleChannelWriters
    ''' <summary>
    ''' Коллекция DAQmx.Task
    ''' </summary>
    Private DigitalWriteTasks As Dictionary(Of String, Task)
    ''' <summary>
    ''' Коллекция DAQmx.DigitalSingleChannelWriter
    ''' </summary>
    Private DigitalSingleChannelWriters As Dictionary(Of String, DigitalSingleChannelWriter)
    ''' <summary>
    ''' Для сконфигурированных устройств массив логических значений соответствующий уровням на линиях портов
    ''' </summary>
    Private DataDigitalOutArrays As Dictionary(Of String, Boolean())
    '''' <summary>
    '''' Полное имя линии {DeviceName}/port{PortNumber}/line{LineNumber}
    '''' </summary>
    Private DigitalLineToStrings As Dictionary(Of String, List(Of String))
    ''' <summary>
    ''' Скользящий счётчик для текущего индекса линиии порта (0, 1, 2...N) для каждого порта при установке значения.
    ''' </summary>
    Private OutIndexes As Dictionary(Of String, Integer)
    ''' <summary>
    ''' Все Task для оборудования сконфигурированы и готовы для выдачи управления в порты.
    ''' </summary>
    Private isTaskRunningDigitalOutput As Boolean

    Public Sub New()
        DigitalWriteTasks = New Dictionary(Of String, Task)
        DigitalSingleChannelWriters = New Dictionary(Of String, DigitalSingleChannelWriter)
        DataDigitalOutArrays = New Dictionary(Of String, Boolean())
        DigitalLineToStrings = New Dictionary(Of String, List(Of String))
        OutIndexes = New Dictionary(Of String, Integer)
    End Sub

    ''' <summary>
    ''' Создать коллекцию всех линий для всех агрегатов включённых программу загрузок.
    ''' </summary>
    ''' <param name="inPortWriters"></param>
    Public Sub LoadAllSelectedConfigurations(inPortWriters As PortWriters)
        ' в коллекции содержатся все каналы по устройствам
        DigitalLineToStrings.Clear()

        For Each itemPortlWriter As PortWriter In inPortWriters
            For Each itemLine As Line In itemPortlWriter.LinesToArray
                ' проверка если девайса еще не было, то создать коллекцию и добавить в словарь New List(Of String)
                If Not DigitalLineToStrings.ContainsKey(itemPortlWriter.DeviceName) Then
                    DigitalLineToStrings.Add(itemPortlWriter.DeviceName, New List(Of String))
                End If
                DigitalLineToStrings(itemPortlWriter.DeviceName).Add($"{itemPortlWriter.ToString}/line{itemLine.Number}")
            Next
        Next
    End Sub

    ''' <summary>
    ''' Конфигурация всех коллекций дискретных выходов.
    ''' </summary>
    ''' <param name="inStrbldResult"></param>
    Public Function ConfigureDigitalWriteTasks(inStrbldResult As StringBuilder) As Boolean
        If DigitalWriteTasks.Count > 0 Then
            For Each DigitalWriteTask In DigitalWriteTasks.Values
                If isTaskRunningDigitalOutput Then DigitalWriteTask.Stop()

                DigitalWriteTask.Dispose()
                DigitalWriteTask = Nothing
            Next
        End If

        isTaskRunningDigitalOutput = False
        DigitalWriteTasks.Clear()
        DigitalSingleChannelWriters.Clear()
        DataDigitalOutArrays.Clear()
        OutIndexes.Clear()

        Try
            For Each keyTaskAsDeviceName In DigitalLineToStrings.Keys
                ' определить число линий для конкретного устройства
                Dim dataDigitalOutArray(DigitalLineToStrings(keyTaskAsDeviceName).Count - 1) As Boolean
                DataDigitalOutArrays.Add(keyTaskAsDeviceName, dataDigitalOutArray)
                ' создать новую задачу для конкретного устройства
                DigitalWriteTasks.Add(keyTaskAsDeviceName, New Task(keyTaskAsDeviceName))
                ' суммировать каналы в строку
                Dim PhysicalChannels As String = Join(DigitalLineToStrings(keyTaskAsDeviceName).ToArray, ", ")
                ' добавить каналы в задачу
                DigitalWriteTasks(keyTaskAsDeviceName).DOChannels.CreateChannel(PhysicalChannels, "", ChannelLineGrouping.OneChannelForAllLines)

                'DigitalWriteTaskDictionary("doTask").Timing.SampleTimingType = SampleTimingType.OnDemand
                ' On Demand нужен наверно для платы, а для корзины наверно не нужен

                '  Write digital port data. WriteDigitalSingChanSingSampPort writes a single sample
                '  of digital data on demand, so no timeout is necessary.
                ' создать писателя для задачи и добавить в коллекцию
                DigitalSingleChannelWriters.Add(keyTaskAsDeviceName, New DigitalSingleChannelWriter(DigitalWriteTasks(keyTaskAsDeviceName).Stream))

                ' проверить корректность задачи
                DigitalWriteTasks(keyTaskAsDeviceName).Control(TaskAction.Verify)
                'DigitalWriteTaskDictionary("doTask").Start()
                'WriterDigitalLine.SynchronizeCallbacks = True
                'WriterDigitalLine.BeginWriteSingleSampleMultiLine(True, dataDigitalOutArray, New AsyncCallback(AddressOf OnCallbackDataWritten), DigitalWriteTask)
                OutIndexes.Add(keyTaskAsDeviceName, 0)
            Next

            isTaskRunningDigitalOutput = True
        Catch ex As Exception
            inStrbldResult.AppendLine(ex.ToString)
            StopTasksDigitalOutput()
        End Try

        Return isTaskRunningDigitalOutput
    End Function

    ''' <summary>
    ''' Остановить все Task для оборудования и очистить коллекции.
    ''' </summary>
    Public Sub StopTasksDigitalOutput()
        isTaskRunningDigitalOutput = False

        If DigitalWriteTasks.Count > 0 Then
            For Each itemTask In DigitalWriteTasks.Values
                'itemTask.Stop()
                itemTask.Dispose()
                itemTask = Nothing
            Next

            DigitalWriteTasks.Clear()
            DigitalSingleChannelWriters.Clear()
            DataDigitalOutArrays.Clear()
            DigitalLineToStrings.Clear()
        End If
    End Sub

    Public Sub Close()
        Try
            If DigitalWriteTasks IsNot Nothing AndAlso DigitalWriteTasks.Count > 0 Then
                For Each itemTask In DigitalWriteTasks.Values
                    'itemTask.Stop()
                    itemTask.Dispose()
                    itemTask = Nothing
                Next

                DigitalWriteTasks = Nothing
                DigitalSingleChannelWriters = Nothing
                DataDigitalOutArrays = Nothing
                DigitalLineToStrings = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Для сконфигурированных устройств установить на портах для линий уровни по значениям соответствующего массива лигических значений.
    ''' </summary>
    Public Sub WritePortsMultiLine()
        If isTaskRunningDigitalOutput Then
            Try
                For Each taskAsDeviceName In DigitalSingleChannelWriters.Keys
                    DigitalSingleChannelWriters(taskAsDeviceName).WriteSingleSampleMultiLine(True, DataDigitalOutArrays(taskAsDeviceName))
                Next
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                StopTasksDigitalOutput()
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Обновить массивы выходных уровней.
    ''' </summary>
    Public Sub UpdateDataDigitalOutArrayDictionary(inPortWriters As PortWriters)
        ' это счётчик
        Dim key() As String = OutIndexes.Keys.ToArray

        For J As Integer = 0 To key.Count - 1
            OutIndexes(key(J)) = 0
        Next

        For Each itemPortlWriter As PortWriter In inPortWriters
            For Each itemLine As Line In itemPortlWriter
                DataDigitalOutArrays(itemPortlWriter.DeviceName)(OutIndexes(itemPortlWriter.DeviceName)) = itemLine.IsEnabled
                OutIndexes(itemPortlWriter.DeviceName) += 1
            Next
        Next
    End Sub
End Class