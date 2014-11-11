'------------------------------------------------
' HuckleberryFinn.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class HuckleberryFinn
    Inherits Form

    Shared Sub Main()
        Application.Run(New HuckleberryFinn())
    End Sub

    Sub New()
        Text = """The Adventures of Huckleberry Finn"""
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics

        grfx.DrawString("You don't know about me, without you " & _
                        "have read a book by the name of ""The " & _
                        "Adventures of Tom Sawyer,"" but that " & _
                        "ain't no matter. That book was made by " & _
                        "Mr. Mark Twain, and he told the truth, " & _
                        "mainly. There was things which he " & _
                        "stretched, but mainly he told the truth. " & _
                        "That is nothing. I never seen anybody " & _
                        "but lied, one time or another, without " & _
                        "it was Aunt Polly, or the widow, or " & _
                        "maybe Mary. Aunt Polly" & ChrW(&H2014) & _
                        "Tom's Aunt Polly, she is" & ChrW(&H2014) & _
                        "and Mary, and the Widow Douglas, is all " & _
                        "told about in that book" & ChrW(&H2014) & _
                        "which is mostly a true book; with some " & _
                        "stretchers, as I said before.", _
                        Font, New SolidBrush(ForeColor), _
                        RectangleF.op_Implicit(ClientRectangle))
    End Sub
End Class
