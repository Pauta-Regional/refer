'---------------------------------------
' Clover.vb (c) 2002 by Charles Petzold
'---------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Class Clover
    Inherits PrintableForm

    Shared Shadows Sub Main()
        Application.Run(New Clover())
    End Sub

    Sub New()
        Text = "Clover"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim path As New GraphicsPath()

        path.AddEllipse(0, cy \ 3, cx \ 2, cy \ 3)        ' Left
        path.AddEllipse(cx \ 2, cy \ 3, cx \ 2, cy \ 3)   ' Right
        path.AddEllipse(cx \ 3, 0, cx \ 3, cy \ 2)        ' Top
        path.AddEllipse(cx \ 3, cy \ 2, cx \ 3, cy \ 2)   ' Bottom

        grfx.SetClip(path)
        grfx.TranslateTransform(cx \ 2, cy \ 2)

        Dim pn As New Pen(clr)
        Dim fRadius As Single = CSng(Math.Sqrt(Math.Pow(cx \ 2, 2) + _
                                               Math.Pow(cy \ 2, 2)))
        Dim fAngle As Single

        For fAngle = Math.PI / 180 To Math.PI * 2 Step Math.PI / 180
            grfx.DrawLine(pn, 0, 0, fRadius * CSng(Math.Cos(fAngle)), _
                                   -fRadius * CSng(Math.Sin(fAngle)))
        Next fAngle
    End Sub
End Class
