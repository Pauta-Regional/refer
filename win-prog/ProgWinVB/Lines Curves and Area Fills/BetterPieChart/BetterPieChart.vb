'-----------------------------------------------
' BetterPieChart.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BetterPieChart
    Inherits PieChart

    Shared Shadows Sub Main()
        Application.Run(New BetterPieChart())
    End Sub

    Sub New()
        Text = "Better " & Text
    End Sub

    Protected Overrides Sub DrawPieSlice(ByVal grfx As Graphics, _
            ByVal pn As Pen, ByVal rect As Rectangle, _
            ByVal fAngle As Single, ByVal fSweep As Single)
        Dim fSlice As Single = CSng((2 * Math.PI * _
                                    (fAngle + fSweep / 2) / 360))

        rect.Offset(CInt(rect.Width / 10 * Math.Cos(fSlice)), _
                    CInt(rect.Height / 10 * Math.Sin(fSlice)))

        MyBase.DrawPieSlice(grfx, pn, rect, fAngle, fSweep)
    End Sub
End Class
