Imports CycleCharge.FormEditorPower

''' <summary>
''' Класс Создатель объявляет фабричный метод, который должен возвращать
''' объект класса BaseAttributeControl. Подклассы Создателя обычно предоставляют
''' реализацию этого метода.
''' </summary>
MustInherit Class BaseCreatorAttributesControl
    ''' <summary>
    ''' Почти так же, как фабрика, просто дополнительная экспозиция, чтобы сделать что-то с созданным методом
    ''' </summary>
    ''' <param name="inCaption"></param>
    ''' <returns></returns>
    Protected MustOverride Function CreateAttributeControl(inFormEditorPower As FormEditorPower, inCaption As String) As BaseAttributesControl

    ' Создатель может также обеспечить реализацию
    ' фабричного метода по умолчанию.
    ''' <summary>
    ''' FactoryMethod
    ''' </summary>
    ''' <returns></returns>      
    Public Function GetAttributeControl(inFormEditorPower As FormEditorPower, inCaption As String) As BaseAttributesControl
        Return Me.CreateAttributeControl(inFormEditorPower, inCaption)
    End Function

    '' Также несмотря на название, основная обязанность
    '' Создателя не заключается в создании форм. Обычно он содержит
    '' некоторую базовую бизнес-логику, которая основана  на объектах
    '' BaseAttributeControl, возвращаемых фабричным методом.  Подклассы могут косвенно
    '' изменять эту бизнес-логику, переопределяя фабричный метод и возвращая
    '' из него другой тип продукта.
    'Public Function SomeOperation(inMainForm As FrmMain) As String
    '    ' Вызываем фабричный метод, чтобы получить объект-BaseAttributeControl.
    '    Dim product = CreateWindow(inMainForm)
    '    ' Далее, работаем с этим BaseAttributeControl.
    '    Dim result = "Creator: The same creator's code has just worked with " & product.Name

    '    Return result
    'End Function
End Class
