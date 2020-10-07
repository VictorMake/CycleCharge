Imports System.Data.OleDb
Imports System.Threading
Imports System.Windows.Forms

''' <summary>
''' Менеджер управления коллекцией ChargeParameter
''' </summary>
Friend Class ChargeParameters
    Implements IEnumerable

    Private mCountChargeParameters As Integer
    ''' <summary>
    ''' Количество Параметров ChargeParameters
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CountChargeParameters() As Integer
        Get
            Return mCountChargeParameters
        End Get
    End Property

    ''' <summary>
    ''' Реализация интерфейса IEnumerable предполагает стандартную реализацию перечислителя.
    ''' Не полагаться на стандартную реализацию, а создать свою логику итератора с помощью ключевых слов Iterator и Yield.
    ''' Конструкция итератора представляет метод, в котором используется ключевое слово Yield для перебора по коллекции или массиву.
    ''' </summary>
    ''' <returns></returns>
    Public Iterator Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        For Each keysCalc As String In ChargeParameterDictionary.Keys.ToArray
            Yield ChargeParameterDictionary(keysCalc)
        Next
    End Function

    Default Friend ReadOnly Property Item(key As String) As ChargeParameter
        Get
            Return ChargeParameterDictionary(key)
        End Get
    End Property

    Private ChargeParameterDictionary As Dictionary(Of String, ChargeParameter)
    Public Property MainFomMdiParent As FrmMain

    Public Sub New()
        MyBase.New()

        ChargeParameterDictionary = New Dictionary(Of String, ChargeParameter)
    End Sub

    ''' <summary>
    ''' Считать Параметры.
    ''' </summary>
    ''' <param name="inPathDBaseCycle"></param>
    Public Sub LoadParameters(ByVal inPathDBaseCycle As String)
        Dim indexParameters As Short
        Dim nameParameter As String
        Dim strSQL As String
        Dim cn As OleDbConnection = Nothing
        Dim cmd As OleDbCommand
        Dim rdr As OleDbDataReader

        ChargeParameterDictionary.Clear()

        Try
            cn = New OleDbConnection(BuildCnnStr(PROVIDER_JET, inPathDBaseCycle))
            cn.Open()

            strSQL = "SELECT Count(*) AS Выражение1 FROM Устройства1;"
            cmd = cn.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQL
            mCountChargeParameters = CInt(cmd.ExecuteScalar)

            strSQL = "SELECT Устройства1.* FROM Устройства1 ORDER BY Устройства1.keyУстройства;"
            cmd.CommandText = strSQL
            rdr = cmd.ExecuteReader

            ' заполнить TypeChargeParameter записями ChargeParameter
            If mCountChargeParameters > 0 Then
                Do While (rdr.Read)
                    nameParameter = CStr(rdr("ИмяУстройства"))
                    ChargeParameterDictionary.Add(nameParameter, New ChargeParameter)

                    With ChargeParameterDictionary.Item(nameParameter)
                        .KeyID = CInt(rdr("keyУстройства"))
                        .NameParameter = nameParameter
                        .NumberChannel = indexParameters ' для того чтобы знать индекс для этого канала в массиве типа
                        .NumberDevice = indexParameters
                        .Description = CStr(rdr("Описание"))
                        .UnitOfMeasure = ConvertUnitOfMeasure(rdr("ЕдиницаИзмерения").ToString)
                        .RangeOfChangingValueMin = CSng(rdr("ДиапазонИзмененияMin"))
                        .RangeOfChangingValueMax = CSng(rdr("ДиапазонИзмененияMax"))
                        .RangeYmin = CShort(rdr("РазносУмин"))
                        .RangeYmax = CShort(rdr("РазносУмакс"))
                        .AlarmValueMin = CSng(rdr("АварийноеЗначениеМин"))
                        .AlarmValueMax = CSng(rdr("АварийноеЗначениеМакс"))
                        '.Blocking = rdr("Блокировка")
                        .IsVisible = CBool(rdr("Видимость"))
                        .IsVisibleRegistration = CBool(rdr("ВидимостьРегистратор"))
                    End With

                    indexParameters += 1S
                Loop
            End If

            rdr.Close()
            cn.Close()
        Catch ex As Exception
            Dim caption As String = $"Процедура <{NameOf(LoadParameters)}> - " & ex.Source
            Dim text As String = ex.ToString
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try

        UpdateDBaseCycle()
    End Sub

    ''' <summary>
    ''' Заполнить заново таблицу РасчетныеПараметры базы Cycle.
    ''' по всем каналам ChargeParameter из сформированного TypeChargeParameter.
    ''' </summary>
    Private Sub UpdateDBaseCycle()
        ' --- Таблица РасчётныеПараметры
        'ИмяПараметра	                    Короткий текст
        'ОписаниеПараметра	                Короткий текст
        'ВычисленноеЗначениеВСИ	            Числовой
        'ВычисленноеПереведенноеЗначение	Числовой
        'РазмерностьСИ	                    Короткий текст
        'РазмерностьВыходная	            Короткий текст
        'НакопленноеЗначение	            Числовой
        'ИндексКаналаИзмерения	            Числовой
        'ДопускМинимум	                    Числовой
        'ДопускМаксимум	                    Числовой
        'РазносУмин	                        Числовой
        'РазносУмакс	                    Числовой
        'АварийноеЗначениеМин	            Числовой
        'АварийноеЗначениеМакс	            Числовой
        'Видимость	                        Логический
        'ВидимостьРегистратор               Логический

        Dim newRow As BaseForm.BaseFormDataSet.РасчетныеПараметрыRow

        ' вначале очистить BaseForm.Manager.РасчетныеПараметры
        ' !!! это gMainFomMdiParent.Manager.РасчетныеПараметры.Rows.Clear() не работает. Надо конкретно по записям:
        For Each itemRow As BaseForm.BaseFormDataSet.РасчетныеПараметрыRow In MainFomMdiParent.Manager.CalculatedDataTable.Rows
            itemRow.Delete()
        Next

        ' из TypeChargeParameter занести в типизированную таблицу BaseForm.Manager.РасчетныеПараметры
        For Each itemChargeParameter As ChargeParameter In ChargeParameterDictionary.Values
            newRow = MainFomMdiParent.Manager.CalculatedDataTable.NewРасчетныеПараметрыRow
            With newRow
                .ИмяПараметра = itemChargeParameter.NameParameter
                .ОписаниеПараметра = Left(itemChargeParameter.Description, 49)
                .ВычисленноеЗначениеВСИ = 0.0
                .ВычисленноеПереведенноеЗначение = 0.0
                .РазмерностьСИ = itemChargeParameter.UnitOfMeasure
                .РазмерностьВыходная = itemChargeParameter.UnitOfMeasure
                .НакопленноеЗначение = 0.0
                .ИндексКаналаИзмерения = CShort(itemChargeParameter.NumberChannel)
                .ДопускМинимум = itemChargeParameter.RangeOfChangingValueMin
                .ДопускМаксимум = itemChargeParameter.RangeOfChangingValueMax
                .РазносУмин = itemChargeParameter.RangeYmin
                .РазносУмакс = itemChargeParameter.RangeYmax
                .АварийноеЗначениеМин = itemChargeParameter.AlarmValueMin
                .АварийноеЗначениеМакс = itemChargeParameter.AlarmValueMax
                .Видимость = itemChargeParameter.IsVisible
                .ВидимостьРегистратор = itemChargeParameter.IsVisibleRegistration
            End With

            MainFomMdiParent.Manager.CalculatedDataTable.AddРасчетныеПараметрыRow(newRow)
        Next

        ' сохранить изменения
        MainFomMdiParent.Manager.SaveTable()
    End Sub

    Const IndexChargeParameter As Single = 10000 ' признак расчётного параметра

    ''' <summary>
    ''' Считать из базы Channels параметры ChargeParameter и если есть копировать в массив признаки.
    ''' Вызывается при закрытии окна расчётного модуля.
    ''' </summary>
    Public Sub UpdateTableCycleFromSettingsChannels(inPathChannels As String, inChannelLast As String, inPathDBaseCycle As String)
        Dim I, countChannels As Integer
        Dim cn As OleDbConnection = Nothing
        Dim cmd As OleDbCommand
        Dim rdr As OleDbDataReader
        Dim odaDataAdapter As OleDbDataAdapter
        Dim dtDataTable As New DataTable
        Dim cb As OleDbCommandBuilder
        Dim strSQL As String = $"SELECT COUNT(*) FROM {inChannelLast} WHERE Погрешность={IndexChargeParameter}"

        Try
            cn = New OleDbConnection(BuildCnnStr(PROVIDER_JET, inPathChannels))
            cn.Open()
            cmd = cn.CreateCommand
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQL
            countChannels = CInt(cmd.ExecuteScalar)

            If countChannels > 0 Then
                Dim cloneOfPropertyChannels(countChannels - 1) As ChargeParameter

                I = 0
                strSQL = "SELECT НаименованиеПараметра, Погрешность, РазносУмин, РазносУмакс, ДопускМинимум, ДопускМаксимум, АварийноеЗначениеМин, АварийноеЗначениеМакс, Видимость, ВидимостьРегистратор" &
                        " FROM " & inChannelLast &
                        " WHERE Погрешность=" & IndexChargeParameter.ToString ' признак расчётного параметра

                cmd.CommandText = strSQL
                rdr = cmd.ExecuteReader

                Do While (rdr.Read)
                    cloneOfPropertyChannels(I) = New ChargeParameter With {
                        .NameParameter = CStr(rdr("НаименованиеПараметра")),
                        .RangeYmin = CShort(rdr("РазносУмин")),
                        .RangeYmax = CShort(rdr("РазносУмакс")),
                        .RangeOfChangingValueMin = CSng(rdr("ДопускМинимум")),
                        .RangeOfChangingValueMax = CSng(rdr("ДопускМаксимум")),
                        .AlarmValueMin = CSng(rdr("АварийноеЗначениеМин")),
                        .AlarmValueMax = CSng(rdr("АварийноеЗначениеМакс")),
                        .IsVisible = CBool(rdr("Видимость")),
                        .IsVisibleRegistration = CBool(rdr("ВидимостьРегистратор"))
                    }
                    'cloneOfPropertyChannels(I).Blocking = rdr("Блокировка")
                    I += 1
                Loop

                rdr.Close()
                cn.Close()

                ' считать из базы Cycle АтрибутыВидимости и поиск по именам и обновить поля РазносУмин, РазносУмакс, АварийноеЗначениеМин, АварийноеЗначениеМакс, Блокировка, Видимость, ВидимостьРегистратор
                ' обновить базу
                cn = New OleDbConnection(BuildCnnStr(PROVIDER_JET, inPathDBaseCycle))
                cn.Open()

                strSQL = "SELECT Устройства1.* FROM Устройства1 ORDER BY Устройства1.keyУстройства;"

                odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
                odaDataAdapter.Fill(dtDataTable)

                If dtDataTable.Rows.Count > 0 Then
                    ' должны быть только по 1 на данном запуске
                    For Each itemDataRow As DataRow In dtDataTable.Rows
                        Dim queryChargeParameters = From itemChargeParameter In cloneOfPropertyChannels
                                                    Where itemChargeParameter.NameParameter = CStr(itemDataRow("ИмяУстройства"))
                                                    Select itemChargeParameter

                        If queryChargeParameters.Count > 0 Then
                            itemDataRow("ДиапазонИзмененияMin") = queryChargeParameters(0).RangeOfChangingValueMin
                            itemDataRow("ДиапазонИзмененияMax") = queryChargeParameters(0).RangeOfChangingValueMax
                            itemDataRow("РазносУмин") = queryChargeParameters(0).RangeYmin
                            itemDataRow("РазносУмакс") = queryChargeParameters(0).RangeYmax
                            itemDataRow("АварийноеЗначениеМин") = queryChargeParameters(0).AlarmValueMin
                            itemDataRow("АварийноеЗначениеМакс") = queryChargeParameters(0).AlarmValueMax
                            'itemDataRow("Блокировка") = queryChargeParameters(0).Blocking
                            itemDataRow("Видимость") = queryChargeParameters(0).IsVisible
                            itemDataRow("ВидимостьРегистратор") = queryChargeParameters(0).IsVisibleRegistration
                        End If
                    Next

                    cb = New OleDbCommandBuilder(odaDataAdapter)
                    odaDataAdapter.Update(dtDataTable)
                    dtDataTable.AcceptChanges()
                End If
            End If

            cn.Close()

            Thread.Sleep(500)
            Application.DoEvents()
        Catch ex As Exception
            Dim caption As String = $"Процедура <{NameOf(UpdateTableCycleFromSettingsChannels)}> - " & ex.Source
            Dim text As String = ex.ToString
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Использовать в будущем.
    ''' Добавить параметры в базу ChannelNNN.
    ''' </summary>
    Public Sub InsertFromCycleToChannels(inPathChannels As String, inChannelLast As String)
        Dim J As Integer
        Dim cn As OleDbConnection = Nothing
        Dim strSQL As String = $"SELECT * FROM {inChannelLast} ORDER BY НомерПараметра" ' & " WHERE Погрешность=" & IndexChargeParameter.tostring' 1000 признак каналов Cycle
        Dim odaDataAdapter As OleDbDataAdapter
        Dim dtDataTable As New DataTable
        Dim newDataRow As DataRow
        Dim cb As OleDbCommandBuilder

        Try
            ' запрос на выборку(вставку) и добавление параметров в базу ChannelNNN
            cn = New OleDbConnection(BuildCnnStr(PROVIDER_JET, inPathChannels))
            cn.Open()
            odaDataAdapter = New OleDbDataAdapter(strSQL, cn)
            dtDataTable = New DataTable
            odaDataAdapter.Fill(dtDataTable)
            ' узнать последий номер канала в таблице стенда
            J = CInt(dtDataTable.Rows(dtDataTable.Rows.Count - 1).Item("НомерПараметра")) + 1

            Dim listNameRows As New List(Of String)
            For Each row As DataRow In dtDataTable.Rows
                listNameRows.Add(CStr(row("НаименованиеПараметра")))
            Next

            For Each itemChargeParameter As ChargeParameter In ChargeParameterDictionary.Values
                With itemChargeParameter
                    newDataRow = dtDataTable.NewRow
                    newDataRow.BeginEdit()
                    newDataRow("НомерПараметра") = J
                    Dim name As String = .NameParameter

                    If listNameRows.Contains(name) Then
                        name &= "-" & J.ToString
                        MessageBox.Show($"Невозможно дабавить канал с именем <{ .NameParameter}>" & vbCrLf &
                                "из базы каналов Cycle, т.к. в текущей базе каналов уже содержится с таким именем." & vbCrLf &
                                $"Канал будет добавлен под именем <{name}>." & vbCrLf &
                                "Приведите имена в соответствие и повторно запустите программу.",
                                "Конфликт имён каналов", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                    newDataRow("НаименованиеПараметра") = name
                    newDataRow("НомерКанала") = .NumberChannel
                    newDataRow("НомерУстройства") = .NumberDevice
                    newDataRow("НомерМодуляКорзины") = 0
                    newDataRow("НомерКаналаМодуля") = 0
                    newDataRow("ТипПодключения") = "DIFF"
                    newDataRow("НижнийПредел") = 0
                    newDataRow("ВерхнийПредел") = 0
                    newDataRow("ТипСигнала") = "DC"
                    newDataRow("НомерФормулы") = 1
                    newDataRow("СтепеньАппроксимации") = 1
                    newDataRow("A0") = 0
                    newDataRow("A1") = 0
                    newDataRow("A2") = 0
                    newDataRow("A3") = 0
                    newDataRow("A4") = 0
                    newDataRow("A5") = 0
                    newDataRow("Смещение") = 0

                    newDataRow("КомпенсацияХС") = False
                    newDataRow("ЕдиницаИзмерения") = .UnitOfMeasure
                    newDataRow("ДопускМинимум") = .RangeOfChangingValueMin
                    newDataRow("ДопускМаксимум") = .RangeOfChangingValueMax
                    newDataRow("РазносУмин") = .RangeYmin
                    newDataRow("РазносУмакс") = .RangeYmax
                    newDataRow("АварийноеЗначениеМин") = .AlarmValueMin
                    newDataRow("АварийноеЗначениеМакс") = .AlarmValueMax
                    'newDataRow("Блокировка") = .Blocking
                    newDataRow("Дата") = Date.Today.ToShortDateString

                    newDataRow("Видимость") = .IsVisible
                    newDataRow("ВидимостьРегистратор") = .IsVisibleRegistration
                    newDataRow("Погрешность") = IndexChargeParameter
                    newDataRow("Примечания") = Left(.Description, 99) '100)
                    newDataRow.EndEdit()
                    dtDataTable.Rows.Add(newDataRow)
                    J += 1
                End With
            Next

            cb = New OleDbCommandBuilder(odaDataAdapter)
            'odaDataAdapter.InsertCommand = cb.GetInsertCommand
            '' Update Database with OleDbDataAdapter
            'dtDataTable.AcceptChanges()
            'odaDataAdapter.InsertCommand.Connection.Close()
            odaDataAdapter.Update(dtDataTable)
            cn.Close()

            Thread.Sleep(500)
            Application.DoEvents()
        Catch ex As Exception
            Dim caption As String = $"Процедура <{NameOf(InsertFromCycleToChannels)}> - " & ex.Source
            Dim text As String = ex.ToString
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Перевести Ед Измерения
    ''' </summary>
    ''' <param name="inUnitOfMeasure"></param>
    ''' <returns></returns>
    Public Shared Function ConvertUnitOfMeasure(ByVal inUnitOfMeasure As String) As String
        Dim outUnitOfMeasure As String = "Деления"

        Select Case inUnitOfMeasure
            Case "Давление"
                outUnitOfMeasure = "кгс/м^2"
            Case "Обороты"
                outUnitOfMeasure = "%"
            Case "Ток"
                outUnitOfMeasure = "Вольт"
            Case "Температура"
                outUnitOfMeasure = "град С"
            Case "Вибрация"
                outUnitOfMeasure = "кг/час"
            Case "Расход"
                outUnitOfMeasure = "Деления"
            Case Else
                outUnitOfMeasure = "Деления"
        End Select

        Return outUnitOfMeasure
    End Function
End Class
