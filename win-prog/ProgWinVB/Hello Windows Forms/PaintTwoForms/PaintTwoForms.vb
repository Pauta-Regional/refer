'----------------------------------------------
' PaintTwoForms.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Module PaintTwoForms
    Private frm1, frm2 As Form

    Sub Main()
        frm1 = New Form()
        frm2 = New Form()

        frm1.Text = "First Form"
        frm1.BackColor = Color.White
        AddHandler frm1.Paint, AddressOf MyPaintHandler

        frm2.Text = "Second Form"
        frm2.BackColor = Color.White
        AddHandler frm2.Paint, AddressOf MyPaintHandler
        frm2.Show()

        Application.Run(frm1)
    End Sub

    Sub MyPaintHandler(ByVal obj As Object, ByVal pea As PaintEventArgs)
        Dim frm As Form = DirectCast(obj, Form)
        Dim grfx As Graphics = pea.Graphics
        Dim str As String

        If frm Is frm1 Then
            str = "Hello from the first form"
        Else
            str = "Hello from the second form"
        End If

        grfx.DrawString(str, frm.Font, Brushes.Black, 0, 0)
    End Sub
End Module
