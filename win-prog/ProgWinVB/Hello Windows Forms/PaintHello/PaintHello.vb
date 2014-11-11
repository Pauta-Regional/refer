'-------------------------------------------
' PaintHello.vb (c) 2002 by Charles Petzold
'-------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Module PaintHello
    Sub Main()
        Dim frm As New Form()
        frm.Text = "Paint Hello"
        frm.BackColor = Color.White
        AddHandler frm.Paint, AddressOf MyPaintHandler

        Application.Run(frm)
    End Sub

    Sub MyPaintHandler(ByVal obj As Object, ByVal pea As PaintEventArgs)
        Dim frm As Form = DirectCast(obj, Form)
        Dim grfx As Graphics = pea.Graphics

        grfx.DrawString("Hello, world!", frm.Font, Brushes.Black, 0, 0)
    End Sub
End Module
