'---------------------------------------------
' WidePolyline.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class WidePolyline
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New WidePolyline())
    End Sub

    Sub New()
        Text = "Wide Polyline"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim pn As New Pen(clr, 25)
        grfx.DrawLines(pn, New Point() { _
                                New Point(25, 100), New Point(125, 100), _
                                New Point(125, 50), New Point(225, 50), _
                                New Point(225, 100), New Point(325, 100)})
    End Sub
End Class
