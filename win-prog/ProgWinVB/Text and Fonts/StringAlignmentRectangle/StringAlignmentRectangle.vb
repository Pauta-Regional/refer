'---------------------------------------------------------
' StringAlignmentRectangle.vb (c) 2002 by Charles Petzold
'---------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class StringAlignmentRectangle
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New StringAlignmentRectangle())
    End Sub

    Sub New()
        Text = "String Alignment (RectangleF in DrawString)"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim rectf As New RectangleF(0, 0, cx, cy)
        Dim astrAlign() As String = {"Near", "Center", "Far"}
        Dim strfmt As New StringFormat()
        Dim iVert, iHorz As Integer

        For iVert = 0 To 2
            For iHorz = 0 To 2
                strfmt.LineAlignment = CType(iVert, StringAlignment)
                strfmt.Alignment = CType(iHorz, StringAlignment)
                grfx.DrawString( _
                    String.Format("LineAlignment = {0}" & vbLf & _
                                  "Alignment = {1}", _
                                  astrAlign(iVert), astrAlign(iHorz)), _
                    Font, br, rectf, strfmt)
            Next iHorz
        Next iVert
    End Sub
End Class
