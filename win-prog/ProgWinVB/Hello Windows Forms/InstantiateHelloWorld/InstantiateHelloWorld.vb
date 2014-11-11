'------------------------------------------------------
' InstantiateHelloWorld.vb (c) 2002 by Charles Petzold
'------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Module InstantiateHelloWorld
    Sub Main()
        Dim frm As New HelloWorld()
        frm.Text = "Instantiate " & frm.Text
        AddHandler frm.Paint, AddressOf MyPaintHandler
        Application.Run(frm)
    End Sub

    Sub MyPaintHandler(ByVal obj As Object, ByVal pea As PaintEventArgs)
        Dim frm As Form = DirectCast(obj, Form)
        Dim grfx As Graphics = pea.Graphics
        grfx.DrawString("Hello from InstantiateHelloWorld!", _
                        frm.Font, Brushes.Black, 0, 100)
    End Sub
End Module
