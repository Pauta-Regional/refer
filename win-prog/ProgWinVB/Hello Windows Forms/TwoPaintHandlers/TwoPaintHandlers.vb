'-------------------------------------------------
' TwoPaintHandlers.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Module TwoPaintHandlers
    Sub Main()
        Dim frm As New Form()
        frm.Text = "Two Paint Handlers"
        frm.BackColor = Color.White
        AddHandler frm.Paint, AddressOf PaintHandler1
        AddHandler frm.Paint, AddressOf PaintHandler2

        Application.Run(frm)
    End Sub

    Sub PaintHandler1(ByVal obj As Object, ByVal pea As PaintEventArgs)
        Dim frm As Form = DirectCast(obj, Form)
        Dim grfx As Graphics = pea.Graphics
        grfx.DrawString("First Paint Event Handler", frm.Font, _
                        Brushes.Black, 0, 0)
    End Sub

    Sub PaintHandler2(ByVal obj As Object, ByVal pea As PaintEventArgs)
        Dim frm As Form = DirectCast(obj, Form)
        Dim grfx As Graphics = pea.Graphics
        grfx.DrawString("Second Paint Event Handler", frm.Font, _
                        Brushes.Black, 0, 100)
    End Sub
End Module
