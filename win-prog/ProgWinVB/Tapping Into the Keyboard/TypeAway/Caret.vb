'--------------------------------------
' Caret.vb (c) 2002 by Charles Petzold
'--------------------------------------
Imports System
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Class Caret
    ' External functions
    Declare Function CreateCaret Lib "user32.dll" _
                    (ByVal hwnd As IntPtr, ByVal hbm As IntPtr, _
                     ByVal cx As Integer, ByVal cy As Integer) As Integer
    Declare Function DestroyCaret Lib "user32.dll" () As Integer
    Declare Function SetCaretPos Lib "user32.dll" _
                    (ByVal x As Integer, ByVal y As Integer) As Integer
    Declare Function ShowCaret Lib "user32.dll" _
                    (ByVal hwnd As IntPtr) As Integer
    Declare Function HideCaret Lib "user32.dll" _
                    (ByVal hwnd As IntPtr) As Integer

    ' Fields
    Private ctrl As Control
    Private szCaret As Size
    Private ptPos As Point
    Private bVisible As Boolean

    ' Only allowable constructor has Control argument.
    Sub New(ByVal ctrl As Control)
        Me.ctrl = ctrl
        Position = Point.Empty
        Size = New Size(1, ctrl.Font.Height)
        AddHandler Control.GotFocus, AddressOf ControlOnGotFocus
        AddHandler Control.LostFocus, AddressOf ControlOnLostFocus

        ' If the control already has focus, create the caret.
        If ctrl.Focused Then
            ControlOnGotFocus(ctrl, New EventArgs())
        End If
    End Sub

    ' Properties
    ReadOnly Property Control() As Control
        Get
            Return ctrl
        End Get
    End Property

    Property Size() As Size
        Set(ByVal Value As Size)
            szCaret = Value
        End Set
        Get
            Return szCaret
        End Get
    End Property

    Property Position() As Point
        Set(ByVal Value As Point)
            ptPos = Value
            SetCaretPos(ptPos.X, ptPos.Y)
        End Set
        Get
            Return ptPos
        End Get
    End Property

    Property Visibility() As Boolean
        Set(ByVal Value As Boolean)
            bVisible = Value

            If bVisible Then
                ShowCaret(Control.Handle)
            Else
                HideCaret(Control.Handle)
            End If
        End Set
        Get
            Return bVisible
        End Get
    End Property

    ' Methods
    Sub Show()
        Visibility = True
    End Sub

    Sub Hide()
        Visibility = False
    End Sub

    Sub Dispose()
        ' If the control has focus, destroy the caret.
        If ctrl.Focused Then
            ControlOnLostFocus(ctrl, New EventArgs())
        End If
        RemoveHandler Control.GotFocus, AddressOf ControlOnGotFocus
        RemoveHandler Control.LostFocus, AddressOf ControlOnLostFocus
    End Sub

    ' Event handlers
    Private Sub ControlOnGotFocus(ByVal obj As Object, _
                                  ByVal ea As EventArgs)
        CreateCaret(Control.Handle, IntPtr.Zero, _
                  Size.Width, Size.Height)
        SetCaretPos(Position.X, Position.Y)
        Show()
    End Sub

    Private Sub ControlOnLostFocus(ByVal obj As Object, _
                                   ByVal ea As EventArgs)
        Hide()
        DestroyCaret()
    End Sub
End Class
