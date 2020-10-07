Imports System.Data.OleDb
Imports System.IO
Imports System.Windows.Forms
Imports CycleCharge.PortLine

Module GlobalVariable

    Public Const PROVIDER_JET As String = "Provider=Microsoft.Jet.OLEDB.4.0;"
    ' для таблицы Устройства1
    Public Const cDeviceName As String = "DeviceName"
    Public Const cDescription As String = "Description"
    Public Const cTarget As String = "Target"
    Public Const cPortNumber As String = "PortNumber"
    Public Const cPortHighLevel As String = "PortHighLevel"
    Public Const cUnitOfMeasure As String = "UnitOfMeasure"
    Public Const cRangeOfChangingValueMin As String = "RangeOfChangingValueMin"
    Public Const cRangeOfChangingValueMax As String = "RangeOfChangingValueMax"
    Public Const cRangeYmin As String = "RangeYmin"
    Public Const cRangeYmax As String = "RangeYmax"
    Public Const cAlarmValueMin As String = "AlarmValueMin"
    Public Const cAlarmValueMax As String = "AlarmValueMax"
    Public Const cIsVisible As String = "IsVisible"
    Public Const cIsVisibleRegistration As String = "IsVisibleRegistration"
    ' для таблицы ВеличинаЗагрузки2
    Public Const cChargeValueToText As String = "ВеличинаЗагрузки"
    Public Const cStageToNumeric As String = "ЧисловоеЗначение"
    Public Const cDescriptionCharge As String = "Примечание"

    Public Function BuildCnnStr(ByVal provider As String, ByVal dataBase As String) As String
        'Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Data Source="D:\ПрограммыVBNET\RUD\RUD.NET\bin\Ресурсы\Channels.mdb";Jet OLEDB:Engine Type=5;Provider="Microsoft.Jet.OLEDB.4.0";Jet OLEDB:System database=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1
        Return $"{provider}Data Source={dataBase};"
    End Function

    ''' <summary>
    ''' True - файла нет
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Public Function FileNotExists(ByVal path As String) As Boolean
        'FileExists = CBool(Dir(FileName) = vbNullString) 
        Return Not File.Exists(path)
    End Function

    ''' <summary>
    ''' Но номеру байта в слове отдать его обозначение
    ''' </summary>
    ''' <param name="inByteNumber"></param>
    ''' <returns></returns>
    Public Function GetLineString(inByteNumber As ByteNumber) As String
        Select Case inByteNumber
            Case ByteNumber.Byte1
                Return "0:7"
            Case ByteNumber.Byte2
                Return "8:15"
            Case ByteNumber.Byte3
                Return "16:23"
            Case ByteNumber.Byte4
                Return "24:31"
            Case Else
                Return "0:7"
        End Select
    End Function

    ''' <summary>
    ''' Запись данных в INI файл - аргументы:
    ''' </summary>
    ''' <param name="sINIFile"></param>
    ''' <param name="sSection">sSection = Название раздела</param>
    ''' <param name="sKey">sKey = Название параметра</param>
    ''' <param name="sValue">sValue = Значение параметра</param>
    ''' <remarks></remarks>
    Public Sub WriteINI(ByRef sINIFile As String, ByRef sSection As String, ByRef sKey As String, ByRef sValue As String)
        Dim N As Integer
        Dim sTemp As String = sValue

        ' Заменить символы CR/LF на пробелы
        For N = 1 To Len(sValue)
            If Mid(sValue, N, 1) = vbCr Or Mid(sValue, N, 1) = vbLf Then Mid(sValue, N) = " "
        Next

        Try
            ' Пишем значения
            N = NativeMethods.WritePrivateProfileString(sSection, sKey, sTemp, sINIFile)
            ' Проверка результата записи
            If N <> 1 Then ' Неудачное завершение
                MsgBox($"Процедура WriteINI не смогла записать параметр INI Файла:{vbCrLf}{sINIFile}{vbCrLf}
-----------------------------------------------------------------{vbCrLf}[{sSection}]{vbCrLf}{sKey}={sValue}")
            End If
        Catch ex As ApplicationException
            MessageBox.Show($"Процедура {NameOf(WriteINI)} привела к ошибке:{vbCrLf}#{ex}",
                            "Ощибка чтения INI", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Чтение данных из файла INI - с возможностью записи значения по умолчанию где аргументы:
    ''' </summary>
    ''' <param name="sINIFile"></param>
    ''' <param name="sSection">sSection  = Название раздела</param>
    ''' <param name="sKey">sKey  = Название параметра</param>
    ''' <param name="sDefault">sDefault = Значение по умолчанию (на случай его отсутствия)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIni(ByRef sINIFile As String, ByRef sSection As String, ByRef sKey As String, Optional ByRef sDefault As String = "") As String
        ' Значение возвращаемое функцией GetPrivateProfileString если искомое значение параметра не найдено
        Const NO_VALUE As String = ""
        Dim nLength As Integer ' Длина возвращаемой строки (функцией GetPrivateProfileString)
        Dim sTemp As String ' Возвращаемая строка

        Try
            ' Получаем значение из файла - если его нет будет возвращен 4й аргумент = strNoValue
            ' sTemp.Value = Space(256)
            sTemp = New String(Chr(0), 255)
            nLength = NativeMethods.GetPrivateProfileString(sSection, sKey, NO_VALUE, sTemp, 255, sINIFile)
            sTemp = Left(sTemp, nLength)

            ' Определяем было найдено значение или нет (если возвращено знач. константы strNoValue то = НЕТ)
            If sTemp = NO_VALUE Then ' Значение не было найдено
                If sDefault <> "" Then ' Если знач по умолчанию задано
                    WriteINI(sINIFile, sSection, sKey, sDefault) ' Записываем заданное аргументом sDefault значение по умолчанию
                    sTemp = sDefault ' и возвращаем его же
                End If
            End If

            ' Возвращаем найденное
            Return sTemp
        Catch ex As ApplicationException
            MessageBox.Show($"Функция {NameOf(GetIni)} привела к ошибке:{vbCrLf}#{ex}",
                            "Ощибка чтения INI", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        End Try
    End Function

    '''' <summary>
    '''' Ликвидация секционирования базы данных для оптимизации функционирования
    '''' </summary>
    '''' <remarks></remarks>
    'Public Sub CompressDataBase()
    'Dim cReadWriteIni As New ReadWriteIni(gPathXmlОпции)
    'Dim xmlDoc As New XmlDocument
    'Dim числоЗапусков As Integer

    'xmlDoc.Load(gPathXmlОпции)

    'числоЗапусков = Convert.ToInt32(cReadWriteIni.GetIni(xmlDoc, "Engine", "Common", "LaunchingCount", "1")) + 1

    'If числоЗапусков > 100 Then
    '    числоЗапусков = 0
    '    ' заменил работу JRO в отдельном исполняемом файле
    '    ' было
    '    'Восстанавление(gPathChannels_cfg_lmz)
    '    'Восстанавление(gPathСycleBase)

    '    Try
    '        Dim startInfo As New ProcessStartInfo(Path.Combine(gPathРесурсы, "BackupJRO.exe"))
    '        startInfo.WindowStyle = ProcessWindowStyle.Minimized 'Normal 
    '        'startInfo.UseShellExecute = True
    '        startInfo.WorkingDirectory = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), ROOT_DIRECTORY)

    '        ' сжать базу gPathChannels_cfg_lmz
    '        WriteINI(gPathРесурсы & "\Опции.ini", "BackupJRO", "PathTarget", gPathChannels_cfg_lmz)
    '        Process.Start(startInfo)
    '        Thread.Sleep(500)
    '        ' сжать базу gPathСycleBase
    '        WriteINI(gPathРесурсы & "\Опции.ini", "BackupJRO", "PathTarget", gPathСycleBase)
    '        Process.Start(startInfo)
    '        Thread.Sleep(500)
    '    Catch ex As Exception
    '        Const captionEx As String = "Ошибка запуска BackupJRO.exe"
    '        Dim textEx As String = String.Format("Функция Process.Start привела к ошибке:{0}{1}", Environment.NewLine, ex.ToString)
    '        Console.WriteLine(String.Format("{0}{1}{2}", captionEx, Environment.NewLine, textEx))
    '        Console.ReadKey()
    '    End Try

    'End If

    '' число запусков
    'cReadWriteIni.writeINI(xmlDoc, "Engine", "Common", "LaunchingCount", числоЗапусков)
    'End Sub

    ''' <summary>
    ''' По таблице Устройства1 вытянуть все настройки устройства.
    ''' </summary>
    ''' <param name="nameDevice"></param>
    ''' <param name="inPathDBaseCycle"></param>
    ''' <returns></returns>
    Public Function GetAttributesChargeParameter(nameDevice As String, inPathDBaseCycle As String) As AttributesChargeParameter
        Dim drDataReader As OleDbDataReader = Nothing
        Dim cmmDbCommand As OleDbCommand
        Dim sSQL As String = "SELECT Устройства1.* FROM Устройства1 WHERE (((Устройства1.ИмяУстройства)='" & nameDevice & "'));"
        Dim newAttributesChargeParameter As New AttributesChargeParameter

        Using cnnOleDbConnection As New OleDbConnection(BuildCnnStr(PROVIDER_JET, inPathDBaseCycle))
            cmmDbCommand = New OleDbCommand(sSQL, cnnOleDbConnection)
            cnnOleDbConnection.Open()
            drDataReader = cmmDbCommand.ExecuteReader(CommandBehavior.CloseConnection)

            ' Всегда вызывать Read прежде получения данных.
            If drDataReader.Read Then
                newAttributesChargeParameter = New AttributesChargeParameter With {
                    .KeyDevice = Convert.ToInt32(drDataReader("keyУстройства")),
                    .Description = CStr(drDataReader("Описание")),
                    .Target = CStr(drDataReader("Target")),
                    .Port = Convert.ToInt32(drDataReader("НомерПорта")),
                    .RangeOfChangingValueMin = Convert.ToDouble(drDataReader("ДиапазонИзмененияMin")),
                    .RangeOfChangingValueMax = Convert.ToDouble(drDataReader("ДиапазонИзмененияMax")),
                    .UnitOfMeasure = Convert.ToString(drDataReader("ЕдиницаИзмерения")),
                    .AlarmValueMin = Convert.ToDouble(drDataReader("АварийноеЗначениеМин")),
                    .AlarmValueMax = Convert.ToDouble(drDataReader("АварийноеЗначениеМакс"))
                }
                '.RangeYmin = Convert.ToInt32(drDataReader("РазносУмин")),
                '.RangeYmax = Convert.ToInt32(drDataReader("РазносУмакс")),
            End If

            ' Всегда вызывать Close когда чтение завершено.
            drDataReader.Close()
        End Using

        Return newAttributesChargeParameter
    End Function

    ''' <summary>
    ''' Скрыть столбцы индексов.
    ''' </summary>
    ''' <param name="datagrid"></param>
    ''' <remarks></remarks>
    Public Sub HideDataGridViewKEYColumns(ByRef datagrid As DataGridView)
        For Each col As DataGridViewColumn In datagrid.Columns
            If col.HeaderText.ToUpper.IndexOf("KEY") <> -1 Then ' OrElse col.HeaderText.ToUpper.IndexOf("КЕУ") <> -1 OrElse strИмяСтолбца.IndexOf("Код") <> -1 
                col.Visible = False
            End If
        Next
    End Sub
End Module
