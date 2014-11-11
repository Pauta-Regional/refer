'------------------------------------------------
' BoxingTheClient.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BoxingTheClient
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New BoxingTheClient())
    End Sub

    Sub New()
        Text = "Boxing the Client"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim apt() As Point = {New Point(0, 0), _
                              New Point(cx - 1, 0), _
                              New Point(cx - 1, cy - 1), _
                              New Point(0, cy - 1), _
                              New Point(0, 0)}
        grfx.DrawLines(New Pen(clr), apt)
    End Sub
End Class
