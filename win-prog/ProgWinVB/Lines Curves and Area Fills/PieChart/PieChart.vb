'-----------------------------------------
' PieChart.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class PieChart
    Inherits PrintableForm

    Private aiValues() As Integer = {50, 100, 25, 150, 100, 75}

    Shared Shadows Sub Main()
        Application.Run(New PieChart())
    End Sub

    Sub New()
        Text = "Pie Chart"
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        Dim rect As New Rectangle(50, 50, 200, 200)
        Dim pn As New Pen(clr)
        Dim iTotal As Integer = 0
        Dim fAngle As Single = 0
        Dim fSweep As Single
        Dim iValue As Integer

        For Each iValue In aiValues
            iTotal += iValue
        Next iValue

        For Each iValue In aiValues
            fSweep = 360.0F * iValue / iTotal
            DrawPieSlice(grfx, pn, rect, fAngle, fSweep)
            fAngle += fSweep
        Next iValue
    End Sub

    Protected Overridable Sub DrawPieSlice(ByVal grfx As Graphics, _
            ByVal pn As Pen, ByVal rect As Rectangle, _
            ByVal fAngle As Single, ByVal fSweep As Single)
        grfx.DrawPie(pn, rect, fAngle, fSweep)
    End Sub
End Class
