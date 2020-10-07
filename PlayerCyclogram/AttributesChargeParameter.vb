''' <summary>
''' Временный буфер для хранения значений агрегатов имеющихся в базе в таблице устройств
''' отсутствующих в настройках конфигурационного файла расчётного модуля.
''' </summary>
''' <remarks></remarks>
Public Class AttributesChargeParameter
    ''' <summary>
    ''' keyУстройства
    ''' </summary>
    ''' <returns></returns>
    Public Property KeyDevice As Integer
    ''' <summary>
    ''' Описание
    ''' </summary>
    ''' <returns></returns>
    Public Property Description As String
    Public Property Target As String
    Public Property Port As Integer
    ''' <summary>
    ''' Диапазон Изменения Min
    ''' </summary>
    ''' <returns></returns>
    Public Property RangeOfChangingValueMin As Double
    ''' <summary>
    ''' Диапазон Изменения Max
    ''' </summary>
    ''' <returns></returns>
    Public Property RangeOfChangingValueMax As Double
    ''' <summary>
    ''' Единица Измерения
    ''' </summary>
    Public Property UnitOfMeasure As String
    ''' <summary>
    ''' АварийноеЗначениеМин
    ''' </summary>
    ''' <returns></returns>
    Public Property AlarmValueMin As Double
    ''' <summary>
    ''' АварийноеЗначениеМакс
    ''' </summary>
    ''' <returns></returns>
    Public Property AlarmValueMax As Double
End Class
