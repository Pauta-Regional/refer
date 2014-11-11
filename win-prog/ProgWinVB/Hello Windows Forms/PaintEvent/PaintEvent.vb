'-------------------------------------------
' PaintEvent.vb (c) 2002 by Charles Petzold
'-------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Module PaintEvent
    Sub Main()
        Dim frm As New Form()
        frm.Text = "Paint Event"
        AddHandler frm.Paint, AddressOf MyPaintHandler

        Application.Run(frm)
    End Sub

    Sub MyPaintHandler(ByVal obj As Object, ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        grfx.Clear(Color.Chocolate)
    End Sub
End Module
