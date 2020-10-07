''' <summary>
''' Полное описание ChargeParameter для вычисления его значения и визуализации.
''' Эта мощность загрузки описывается в таблицах ВеличинаЗагрузки2.*, АтрибутыВеличинаЗагрузки.*"
''' </summary>
Friend Class ChargeParameter
    Public Property KeyID() As Integer
    ''' <summary>
    ''' Индекс В Массиве Значений
    ''' НомерКанала DAQ
    ''' </summary>
    ''' <returns></returns>
    Public Property NumberChannel() As Integer
    ''' <summary>
    ''' НомерУстройства или корзины
    ''' </summary>
    Public Property NumberDevice As Short
    ''' <summary>
    ''' Наименование Параметра
    ''' </summary>
    ''' <returns></returns>
    Public Property NameParameter() As String
    ''' <summary>
    ''' Описание Параметра, Примечания
    ''' </summary>
    ''' <returns></returns>
    Public Property Description() As String
    ''' <summary>
    ''' Единица Измерения
    ''' </summary>
    Public Property UnitOfMeasure As String
    ''' <summary>
    ''' Диапазон Изменения Min
    ''' </summary>
    ''' <returns></returns>
    Public Property RangeOfChangingValueMin() As Single
    ''' <summary>
    ''' Диапазон Изменения Max
    ''' </summary>
    ''' <returns></returns>
    Public Property RangeOfChangingValueMax() As Single
    ''' <summary>
    ''' Разнос Умин
    ''' </summary>
    Public Property RangeYmin As Short
    ''' <summary>
    ''' Разнос Умакс
    ''' </summary>
    Public Property RangeYmax As Short
    ''' <summary>
    ''' Аварийное Значение Мин
    ''' </summary>
    Public Property AlarmValueMin As Single
    ''' <summary>
    ''' Аварийное Значение Макс
    ''' </summary>
    Public Property AlarmValueMax As Single
    '''' <summary>
    '''' Блокировка
    '''' </summary>
    'Public Property Blocking As Boolean
    ''' <summary>
    ''' Видимость
    ''' </summary>
    Public Property IsVisible As Boolean
    ''' <summary>
    ''' Видимость Регистратор
    ''' </summary>
    Public Property IsVisibleRegistration As Boolean
    '''' <summary>
    '''' Значение
    '''' </summary>
    'Public Property ValueChargeParameter As Double
End Class