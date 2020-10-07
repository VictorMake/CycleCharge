﻿Partial Class StackStrip
    Inherits BaseStackStrip

    <System.Diagnostics.DebuggerNonUserCode()>
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        MyClass.New()

        'Требуется для поддержки конструктора композиции классов Windows.Forms
        If (container IsNot Nothing) Then
            container.Add(Me)
        End If
    End Sub

    '<System.Diagnostics.DebuggerNonUserCode()> _
    'Public Sub New()
    '    MyBase.New()

    '    'Этот вызов является обязательным для конструктора компонентов.
    '    InitializeComponent()

    'End Sub

    'Компонент переопределяет метод dispose для очистки списка элементов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
                If _last IsNot Nothing Then
                    _last.Dispose()
                    _last = Nothing
                End If
                If _font IsNot Nothing Then
                    _font.Dispose()
                    _font = Nothing
                End If
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора компонентов
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора компонентов
    'Для ее изменения используйте конструктор компонентов.
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

End Class
