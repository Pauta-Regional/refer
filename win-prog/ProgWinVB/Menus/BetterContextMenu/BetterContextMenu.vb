'--------------------------------------------------
' BetterContextMenu.vb (c) 2002 by Charles Petzold
'--------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BetterContextMenu
    Inherits Form

    Private micColor As MenuItemColor

    Shared Sub Main()
        Application.Run(New BetterContextMenu())
    End Sub

    Sub New()
        Text = "Better Context Menu Demo"

        Dim eh As EventHandler = AddressOf MenuColorOnClick
        Dim amic As MenuItemColor() = _
        { _
            New MenuItemColor(Color.Black, "&Black", eh), _
            New MenuItemColor(Color.Blue, "B&lue", eh), _
            New MenuItemColor(Color.Green, "&Green", eh), _
            New MenuItemColor(Color.Cyan, "&Cyan", eh), _
            New MenuItemColor(Color.Red, "&Red", eh), _
            New MenuItemColor(Color.Magenta, "&Magenta", eh), _
            New MenuItemColor(Color.Yellow, "&Yellow", eh), _
            New MenuItemColor(Color.White, "&White", eh) _
        }
        Dim mic As MenuItemColor

        For Each mic In amic
            mic.RadioCheck = True
        Next mic

        micColor = amic(3)
        micColor.Checked = True
        BackColor = micColor.Color

        ContextMenu = New ContextMenu(amic)
    End Sub

    Private Sub MenuColorOnClick(ByVal obj As Object, _
                                 ByVal ea As EventArgs)
        micColor.Checked = False
        micColor = DirectCast(obj, MenuItemColor)
        micColor.Checked = True
        BackColor = micColor.Color
    End Sub
End Class

Class MenuItemColor
    Inherits MenuItem

    Private clr As Color

    Sub New(ByVal clr As Color, ByVal str As String, _
                                ByVal eh As EventHandler)
        MyBase.New(str, eh)
        Color = clr
    End Sub

    Property Color() As Color
        Set(ByVal Value As Color)
            clr = Value
        End Set
        Get
            Return clr
        End Get
    End Property
End Class
