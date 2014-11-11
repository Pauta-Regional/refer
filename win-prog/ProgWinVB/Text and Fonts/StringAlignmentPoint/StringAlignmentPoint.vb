'-----------------------------------------------------
' StringAlignmentPoint.vb (c) 2002 by Charles Petzold
'-----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class StringAlignmentPoint
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(new StringAlignmentPoint())
    End Sub

    Sub New()
        Text = "String Alignment (PointF in DrawString)"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim br As New SolidBrush(clr)
        Dim pn As New Pen(clr)
        Dim astrAlign() As String = {"Near", "Center", "Far"}
        Dim strfmt As New StringFormat()
        Dim iVert, iHorz As Integer

        grfx.DrawLine(pn, 0, cy \ 2, cx, cy \ 2)
        grfx.DrawLine(pn, cx \ 2, 0, cx \ 2, cy)

        For iVert = 0 To 2 Step 2
            For iHorz = 0 To 2 Step 2
                strfmt.LineAlignment = CType(iVert, StringAlignment)
                strfmt.Alignment = CType(iHorz, StringAlignment)
                grfx.DrawString( _
                    String.Format("LineAlignment = {0}" & vbLf & _
                                  "Alignment = {1}", _
                                  astrAlign(iVert), astrAlign(iHorz)), _
                    Font, br, cx \ 2, cy \ 2, strfmt)
            Next iHorz
        Next iVert
    End Sub
End Class
