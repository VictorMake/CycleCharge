Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

''' <summary>
''' Контрол расширяющий поведение ленты
''' </summary>
''' <remarks></remarks>
Public Class StackStrip
    Inherits BaseStackStrip

    Const DEFAULT_ITEM_COUNT As Integer = 10
    Protected Event _itemHeightChanged As EventHandler

    Private _last As ToolStripButton = Nothing
    Private _ignore As Boolean = False
    Private _font As Font

#Region "Public API"

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'Этот вызов является обязательным для конструктора компонентов.
        InitializeComponent()
        ' установить размещение элементов в коллекции
        LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow
    End Sub

    Public ReadOnly Property ItemCount() As Integer
        Get
            Return Items.Count
        End Get
    End Property

    Public ReadOnly Property InitialDisplayCount() As Integer
        Get
            Return (Math.Min(ItemCount, DEFAULT_ITEM_COUNT))
        End Get
    End Property

    'Public Custom Event ItemHeightChanged As EventHandler
    '    AddHandler(ByVal value As EventHandler)
    '        AddHandler _itemHeightChanged, value
    '    End AddHandler
    '    RemoveHandler(ByVal value As EventHandler)
    '        RemoveHandler _itemHeightChanged, value
    '    End RemoveHandler
    'End Event

    'Public Custom Event ItemHeightChanged As EventHandler
    '    AddHandler(value As EventHandler)
    '        AddHandler _itemHeightChanged, value
    '    End AddHandler

    '    RemoveHandler(value As EventHandler)
    '        RemoveHandler _itemHeightChanged, value
    '    End RemoveHandler

    '    RaiseEvent(sender As Object, e As EventArgs)

    '    End RaiseEvent
    'End Event

    Public ReadOnly Property ItemHeight() As Integer
        Get
            Return (If((ItemCount > 0), Items(0).Height, 0))
        End Get
    End Property

#End Region

#Region "Protected API"
    Protected Overrides Sub OnSetRenderer(pr As ToolStripProfessionalRenderer)
        ' дать перерисовать базовому классу
        MyBase.OnSetRenderer(pr)

        ' Рисование кнопки
        AddHandler pr.RenderButtonBackground, New ToolStripItemRenderEventHandler(AddressOf StackStrip_RenderButtonBackground)
    End Sub

    Protected Overrides Sub OnRenderToolStripBackground(e As ToolStripRenderEventArgs)
        ' использовать базовую перерисовку
    End Sub

    Protected Overrides Sub OnSetFonts()
        ' Вызвать базовую
        _font = New Font(SystemFonts.IconTitleFont, FontStyle.Bold)

        ' Обновить если различно
        If Font IsNot _font Then
            Font = _font

            ' Известить контейнер
            OnItemHeightChanged(EventArgs.Empty)
        End If
    End Sub

    Protected Overridable Sub OnItemHeightChanged(e As EventArgs)
        RaiseEvent _itemHeightChanged(Me, e)
    End Sub

    Protected Overrides Sub OnItemAdded(e As ToolStripItemEventArgs)
        Dim button As ToolStripButton = TryCast(e.Item, ToolStripButton)

        If button IsNot Nothing Then
            ' Подписать на событие Click
            AddHandler button.CheckedChanged, New EventHandler(AddressOf StackStripButton_CheckedChanged)
        End If
    End Sub
#End Region

#Region "Event Handlers"
    Private Sub StackStrip_RenderButtonBackground(sender As Object, e As ToolStripItemRenderEventArgs)
        Dim g As Graphics = e.Graphics
        Dim bounds As New Rectangle(Point.Empty, e.Item.Size)

        Dim gradientBegin As Color = StackStripRenderer.ColorTable.ImageMarginGradientMiddle
        Dim gradientEnd As Color = StackStripRenderer.ColorTable.ImageMarginGradientEnd

        Dim button As ToolStripButton = TryCast(e.Item, ToolStripButton)
        If button.Pressed OrElse button.Checked Then
            gradientBegin = StackStripRenderer.ColorTable.ButtonPressedGradientBegin
            gradientEnd = StackStripRenderer.ColorTable.ButtonPressedGradientEnd
        ElseIf button.Selected Then
            gradientBegin = StackStripRenderer.ColorTable.ButtonSelectedGradientBegin
            gradientEnd = StackStripRenderer.ColorTable.ButtonSelectedGradientEnd
        End If

        ' Рисовать фон кнопки
        Using b As Brush = New LinearGradientBrush(bounds, gradientBegin, gradientEnd, LinearGradientMode.Vertical)
            g.FillRectangle(b, bounds)
        End Using

        ' Рисовать окантовку
        e.Graphics.DrawRectangle(SystemPens.ControlDarkDark, bounds)
    End Sub

    Private Sub StackStripButton_CheckedChanged(sender As Object, e As EventArgs)
        Dim button As ToolStripButton = TryCast(sender, ToolStripButton)

        ' Ни когда не должно быть нуль - только в случае добавления метки
        If _ignore OrElse (button IsNot Nothing) Then
            If button.Checked Then
                If (_last IsNot button) AndAlso (_last IsNot Nothing) Then
                    ' Unset
                    _ignore = True
                    _last.Checked = False
                    _ignore = False
                End If

                _last = button
            Else
                ' Удостовериться, что что-то помечено
                Dim foundItem As Boolean = False

                For Each item As ToolStripItem In Items
                    Dim btn As ToolStripButton = TryCast(item, ToolStripButton)

                    If btn IsNot Nothing Then
                        If btn.Checked Then
                            foundItem = True
                            Exit For
                        End If
                    End If
                Next

                ' Проверка
                If Not foundItem Then
                    ' Выделить последний элемент
                    _last = button

                    _ignore = True
                    button.Checked = True
                    _ignore = False
                End If
            End If
        End If
    End Sub
#End Region
End Class
