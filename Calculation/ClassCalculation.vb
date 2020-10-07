Imports BaseForm

' для COM видимости
'<System.Runtime.InteropServices.ProgId("ClassDiagram_NET.ClassDiagram")> Public Class ClassCalculation
'    Implements BaseForm.IClassCalculation
Public Class ClassCalculation
    Implements IClassCalculation

    Public Property Manager() As ProjectManager Implements IClassCalculation.Manager

    ''' <summary>
    ''' Входные аргументы
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InputParams() As InputParameters

    ''' <summary>
    ''' Настроечные параметры
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TuningParams() As TuningParameters
    Public Property MainFomMdiParent As FrmMain

    ''' <summary>
    '''  Расчетные параметры
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CalculatedParams() As CalculatedParameters

    'Delegate Sub DataErrorventHandler(ByVal sender As Object, ByVal e As DataErrorEventArgs)
    'Public Event DataError(ByVal sender As Object, ByVal e As BaseForm.IClassCalculation.DataErrorEventArgs) Implements BaseForm.IClassCalculation.DataError
    'Public Event DataError(ByVal sender As BaseForm.IClassCalculation, ByVal e As BaseForm.DataErrorEventArgs) Implements BaseForm.DataError
    ''' <summary>
    ''' событие для выдачи ошибки в вызывающую программу
    ''' </summary>
    Public Event DataError As EventHandler(Of DataErrorEventArgs)

    Public Sub New(ByVal manager As ProjectManager)
        MyBase.New()

        Me.Manager = manager
        InputParams = New InputParameters
        TuningParams = New TuningParameters
        CalculatedParams = New CalculatedParameters
    End Sub

    ''' <summary>
    ''' Последовательное прохождение по этапам приведениия и вычисления.
    ''' Здесь индивидуальные настройки для класса.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Calculate() Implements IClassCalculation.Calculate
        ' Для Приведенных и Пересчитанных параметров входные единицы измерения
        ' только в единицах СИ, выходные единицы измерения - любого типа
        'gMainFomMdiParent.VarFormDetermineDevices.TextError.Visible = False

        Try
            ' здесь пока не надо получать от контролов
            If Manager.NeedToRewrite Then GetValuesTuningParameters()
            ' Переводим в Си только измеренные пареметры
            Manager.СonversionToSiUnitMeasurementParameters()
            'получение абсолютных давлений
            'mProjectManager.УчетБазовыхВеличин()
            ' весь подсчет производится исключительно в единицах СИ
            ' извлекаем значения измеренных параметров
            GetValuesMeasurementParameters()
            SetCalculatedParameters()
        Catch ex As Exception
            ' ошибка проглатывается
            'Description = "Процедура: Подсчет"
            ''перенаправление встроенной ошибки
            'Dim fireDataErrorEventArgs As New IClassCalculation.DataErrorEventArgs(ex.ToString, Description)
            ''  Теперь вызов события с помощью вызова делегата. Проходя в
            ''   object которое инициирует  событие (Me) такое же как FireEventArgs. 
            ''  Вызов обязан соответствовать сигнатуре FireEventHandler.
            'RaiseEvent DataError(Me, fireDataErrorEventArgs)
        End Try
    End Sub

    '''' <summary>
    '''' Подсчёты не связанные с графическим интерфейсом.
    '''' Графический интерфейс не блокируется.
    '''' </summary>
    '''' <returns></returns>
    'Public Async Function CalcAsynchronouslyAsync() As Task 'Task(Of String) '
    '    'Await Task.Delay(10000)
    '    'Return "Finished"
    '    Dim t As Task = Task.Factory.StartNew(Sub()
    '                                              ' здесь пока не надо получать от контролов
    '                                              If mProjectManager.NeedToRewrite Then GetValuesTuningParameters()
    '                                              ' Переводим в Си только измеренные пареметры
    '                                              Manager.СonversionToSiUnitMeasurementParameters()
    '                                              'получение абсолютных давлений
    '                                              Manager.УчетБазовыхВеличин()
    '                                              ' весь подсчет производится исключительно в единицах СИ
    '                                              ' извлекаем значения измеренных параметров
    '                                              ИзвлечьЗначенияИзмеренныхПараметров()
    '                                              ВычислитьРасчетныеПараметры()
    '                                              Manager.СonversionToTuningUnitCalculationParameters()

    '                                              gMainFomMdiParent.VarFormDetermineDevices.UpdateVisualControls()
    '                                          End Sub)
    '    t.Wait()

    '    Await t
    'End Function

    ReadOnly description As String = $"Процедура: <{NameOf(GetValuesTuningParameters)}>"
    ''' <summary>
    ''' Получить Значения Настроечных Параметров.
    ''' Получить значения параметров, используемых как настраиваемые глобальные переменные.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetValuesTuningParameters()
        If Manager.TuningDataTable Is Nothing Then Exit Sub

        ' проверяется наличие расчетных параметров в базе
        For Each nameTuningParameter As String In TuningParams.TuningDictionary.Keys.ToArray
            Dim query = From row In Manager.TuningDataTable.Rows
                        Where CType(row, BaseFormDataSet.НастроечныеПараметрыRow).ИмяПараметра = nameTuningParameter
                        Select row

            If query.Count = 0 Then
                ' перенаправление встроенной ошибки
                RaiseEvent DataError(Me, New DataErrorEventArgs($"Настроечный параметр {nameTuningParameter} в базе параметров не найден!", description)) ' не ловит в конструкторе
                'MessageBox.Show(Message, Description, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Next

        ' проверяется наличие в расчетном модуле переменных, соответствующих расчетным настроечным
        ' и присвоение им значений
        Dim success As Boolean = True
        Try
            For Each rowTuningParameter As BaseFormDataSet.НастроечныеПараметрыRow In Manager.TuningDataTable.Rows
                If TuningParams.TuningDictionary.Keys.Contains(rowTuningParameter.ИмяПараметра) Then
                    Select Case rowTuningParameter.ИмяПараметра
                        'Case "GвМПитоПриводить"
                        '    'GвМПитоПриводить = rowНастроечныйПараметр.ЦифровоеЗначение
                        '    'n1ГПриводить = CInt(rowНастроечныйПараметр.ЛогическоеЗначение)
                        '    GвМПитоПриводить = rowНастроечныйПараметр.ЛогическоеЗначение
                        '    Exit Select
                    End Select
                Else
                    success = False
                    ' перенаправление встроенной ошибки
                    RaiseEvent DataError(Me, New DataErrorEventArgs($"Настроечный параметр {rowTuningParameter.ИмяПараметра} не имеет соответствующей переменной в модуле расчета!", description)) ' не ловит в конструкторе
                    'MessageBox.Show(Message, Description, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Next

            'With gMainFomMdiParent.Manager.НастроечныеПараметры
            '    'D20трубОсн = .FindByИмяПараметра(TuningParameters.conD20трубОсн).ЦифровоеЗначение
            'End With

            If success = False Then Exit Sub

            ' занести значения настроечных параметров
            With Manager.TuningDataTable
                For Each keysTuning As String In TuningParams.TuningDictionary.Keys.ToArray
                    If .FindByИмяПараметра(keysTuning).ЛогикаИлиЧисло Then
                        TuningParams.TuningDictionary(keysTuning).IsLogicalOrDigital = True
                        TuningParams.TuningDictionary(keysTuning).LogicalValue = .FindByИмяПараметра(keysTuning).ЛогическоеЗначение
                    Else
                        TuningParams.TuningDictionary(keysTuning).IsLogicalOrDigital = False
                        TuningParams.TuningDictionary(keysTuning).DigitalValue = .FindByИмяПараметра(keysTuning).ЦифровоеЗначение
                    End If
                Next
            End With
        Catch ex As Exception
            ' перенаправление встроенной ошибки
            RaiseEvent DataError(Me, New DataErrorEventArgs(ex.Message, description)) ' не ловит в конструкторе
            'MessageBox.Show(fireDataErrorEventArgs, Description, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ''' <summary>
    ''' Извлечь Значения Измеренных Параметров.
    ''' Поиск всех параметров по пользовательскому запросу в DataSet.ИзмеренныеПараметры
    ''' (с одним входным параметром являющимся именем связи для реального измеряемого канала Сервера).
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetValuesMeasurementParameters()
        Try
            With Manager.MeasurementDataTable
                ' вместо последовательного извлечения применяется обход по коллекции
                ' ARG1 = .FindByИмяПараметра(conARG1).ЗначениеВСИ
                ' ...
                ' ARG10 = .FindByИмяПараметра(conARG10).ЗначениеВСИ

                'For Each keysArg As String In inputArg.InputArgDictionary.Keys.ToArray
                '    inputArg.InputArgDictionary(keysArg) = .FindByИмяПараметра(keysArg).ЗначениеВСИ
                'Next

                ' '' иттератор по коллекции как KeyValuePair objects.
                ''For Each kvp As KeyValuePair(Of String, Double) In inputArg.InputArgDictionary
                ''    'Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value)
                ''    inputArg.InputArgDictionary(kvp.Key) = .FindByИмяПараметра(kvp.Key).ЗначениеВСИ
                ''Next

                ''For Each value As Double In inputArg.InputArgDictionary.Values
                ''    Console.WriteLine("Value = {0}", value)
                ''Next

                InputParams.N1 = .FindByИмяПараметра(InputParameters.conN1).ЗначениеВСИ
                InputParams.N2 = .FindByИмяПараметра(InputParameters.conN2).ЗначениеВСИ
                InputParams.RUD = .FindByИмяПараметра(InputParameters.conRUD).ЗначениеВСИ
            End With
        Catch ex As Exception
            MainFomMdiParent.ShowError("Ошибка извлечения измеренных параметров")
            'перенаправление встроенной ошибки
            RaiseEvent DataError(Me, New DataErrorEventArgs(ex.Message, $"Процедура: <{NameOf(GetValuesMeasurementParameters)}>"))
        End Try
    End Sub

    ''' <summary>
    ''' Вычислить Расчетные Параметры.
    ''' Поиск всех параметров по пользовательскому запросу в DataSet.РасчетныеПараметры
    ''' (с одним входным параметром являющимся именем расчётной величины).
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCalculatedParameters()
        If MainFomMdiParent.IsReloadParameters Then Exit Sub

        Try
            ' занести вычисленные значения
            With Manager.CalculatedDataTable
                '' вместо последовательного извлечения применяется обход по коллекции
                '' .FindByИмяПараметра(conCalc1).ВычисленноеЗначениеВСИ = Calc1
                ' ********************************** и т.д. ********************************
                '' .FindByИмяПараметра(conCalc10).ВычисленноеЗначениеВСИ = Calc10
                ' ********************************** и т.д. ********************************

                For Each keysCalc As String In CalculatedParams.CalcDictionary.Keys.ToArray
                    .FindByИмяПараметра(keysCalc).ВычисленноеЗначениеВСИ = CalculatedParams(keysCalc)
                Next
            End With

            Manager.СonversionToTuningUnitCalculationParameters()

            'Dim result As String = Await AsynchronouslyAsync()
            'Await CalcAsynchronouslyAsync()
        Catch ex As Exception
            MainFomMdiParent.ShowError("Ошибка вычисления расчётных параметров")
            ' перенаправление встроенной ошибки
            RaiseEvent DataError(Me, New DataErrorEventArgs(ex.Message, $"Процедура: <{NameOf(SetCalculatedParameters)}>"))
        End Try
    End Sub
End Class