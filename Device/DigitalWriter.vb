Imports System.Windows.Forms
Imports NationalInstruments.DAQmx

''' <summary>
''' Работа с дискретным устройством оборудования компьютера.
''' </summary>
Public Class DigitalWriter
    Implements IDisposable

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
            Close()
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Private DigitalWriteTaskPort As Task
    Private WriterPort As DigitalSingleChannelWriter
    ''' <summary>
    ''' Полные имена линий в опросе для выполнения мощности.
    ''' </summary>
    Private DigitalFullLinesToStrings As New List(Of String)
    ''' <summary>
    ''' Массив логических значений линий в опросе для выполнения мощности.
    ''' </summary>
    Private DataDigitalOutArray As Boolean()
    Private mSystemConfigurationDevices As ConfigurationDevices

    Public Sub New(inSystemConfigurationDevices As ConfigurationDevices)
        mSystemConfigurationDevices = inSystemConfigurationDevices
    End Sub

    Public Sub Close()
        Try
            If DigitalWriteTaskPort IsNot Nothing Then
                DigitalWriteTaskPort.Dispose()
                DigitalWriteTaskPort = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' Создать выходные дискретные именованные каналы.
    ''' </summary>
    ''' <returns></returns>
    Public Function SetupDigitalWriteTask(ByVal isOn As Boolean) As Boolean
        Dim success As Boolean

        If DigitalWriteTaskPort IsNot Nothing Then
            DigitalWriteTaskPort.Dispose()
            DigitalWriteTaskPort = Nothing
        End If

        Try
            Dim devName As String = mSystemConfigurationDevices.DeviceBindingList(mSystemConfigurationDevices.DeviceSelectedIndex).ToString
            Dim portName As String = mSystemConfigurationDevices.DeviceBindingList(mSystemConfigurationDevices.DeviceSelectedIndex).PortBindingList(mSystemConfigurationDevices.PortSelectedIndex).ToString
            ' создать новую задачу для конкретного устройства
            DigitalWriteTaskPort = New Task($"{devName}{portName}")

            ' определить число каналов для конкретной мощности
            DigitalFullLinesToStrings.Clear()

            For Each itemLine As Line In mSystemConfigurationDevices.DeviceBindingList(mSystemConfigurationDevices.DeviceSelectedIndex).PortBindingList(mSystemConfigurationDevices.PortSelectedIndex)
                If isOn Then
                    If itemLine.IsEnabled AndAlso Not itemLine.IsBusy AndAlso itemLine.LineControl.Value Then
                        DigitalFullLinesToStrings.Add($"{devName}/{portName}/{itemLine.ToString}")
                    End If
                Else
                    If itemLine.IsEnabled AndAlso Not itemLine.IsBusy Then
                        DigitalFullLinesToStrings.Add($"{devName}/{portName}/{itemLine.ToString}")
                    End If
                End If
            Next

            If DigitalFullLinesToStrings.Count > 0 Then
                DataDigitalOutArray = New Boolean(DigitalFullLinesToStrings.Count - 1) {}

                For I As Integer = 0 To DataDigitalOutArray.Count - 1
                    DataDigitalOutArray(I) = isOn
                Next

                ' суммировать каналы в строку
                Dim physicalChannels As String = Join(DigitalFullLinesToStrings.ToArray, ", ")
                ' добавить каналы в задачу
                DigitalWriteTaskPort.DOChannels.CreateChannel(physicalChannels, "", ChannelLineGrouping.OneChannelForAllLines)
                WriterPort = New DigitalSingleChannelWriter(DigitalWriteTaskPort.Stream)
                ' проверить корректность задачи
                DigitalWriteTaskPort.Control(TaskAction.Verify)

                'DigitalWriteTaskPort.DOChannels.CreateChannel($"{devName}/{portName}",
                '                                              portName,
                '                                              ChannelLineGrouping.OneChannelForAllLines)
                'WriterPort = New DigitalSingleChannelWriter(DigitalWriteTaskPort.Stream)
                success = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            If DigitalWriteTaskPort IsNot Nothing Then
                DigitalWriteTaskPort.Dispose()
                DigitalWriteTaskPort = Nothing
            End If
        End Try

        Return success
    End Function

    ''' <summary>
    ''' Установить все линии порта устройства в нуль.
    ''' </summary>
    Public Sub SetAllLinesPortOff()
        If SetupDigitalWriteTask(False) Then WritePortLinesByPower()
    End Sub

    'Private valPort As Long
    ''' <summary>
    ''' Выполнить величину выбранной в дереве мощности устройства.
    ''' </summary>
    Public Sub WritePortLinesByPower()
        ' в цикле по линиям из функции найти значение в порт без округления
        'valPort = 0
        'For Each itemLine As Line In SystemConfigurationDevices.DeviceBindingList(SystemConfigurationDevices.DeviceSelectedIndex).PortBindingList(SystemConfigurationDevices.PortSelectedIndex)
        '    If itemLine.IsEnabled AndAlso Not itemLine.IsBusy AndAlso itemLine.LineControl.Value Then
        '        SumBits(valPort, itemLine.Number, 1, isOn)
        '    End If
        'Next

        Try
            'WriterPort.WriteSingleSamplePort(True, Decimal.ToUInt32(valPort))'CType(valPort, UInteger)) '  Long.ToUInt32(valPort0))

            WriterPort.WriteSingleSampleMultiLine(True, DataDigitalOutArray)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            If DigitalWriteTaskPort IsNot Nothing Then
                DigitalWriteTaskPort.Dispose()
                DigitalWriteTaskPort = Nothing
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Суммировать Биты
    ''' </summary>
    ''' <param name="valuePort"></param>
    ''' <param name="lineNumber"></param>
    ''' <param name="highLevelPort"></param>
    ''' <param name="isOn"></param>
    Public Shared Sub SumBits(ByRef valuePort As Long, ByVal lineNumber As Integer, ByVal highLevelPort As Integer, ByVal isOn As Boolean)
        If highLevelPort = 1 Then
            If isOn = True Then
                valuePort += 1L << lineNumber
            End If
        Else
            If isOn = False Then
                valuePort += 1L << lineNumber
            End If
        End If
    End Sub
End Class
