'----------------------------------------
' PoePoem.vb (c) 2002 by Charles Petzold
'----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class PoePoem
    Inherits Form

    Const strAnnabelLee As String = _
        "It was many and many a year ago," & vbLf & _
        "   In a kingdom by the sea," & vbLf & _
        "That a maiden there lived whom you may know" & vbLf & _
        "   By the name of Annabel Lee;" & ChrW(&H2014) & vbLf & _
        "And this maiden she lived with no other thought" & vbLf & _
        "   Than to love and be loved by me." & vbLf & _
        vbLf & _
        "I was a child and she was a child" & vbLf & _
        "   In this kingdom by the sea," & vbLf & _
        "But we loved with a love that was more than love" & _
                                        ChrW(&H2014) & vbLf & _
        "   I and my Annabel Lee" & ChrW(&H2014) & vbLf & _
        "With a love that the wing" & ChrW(233) & "d seraphs of Heaven" _
                                        & vbLf & _
        "   Coveted her and me." & vbLf & _
        vbLf & _
        "And this was the reason that, long ago," & vbLf & _
        "   In this kingdom by the sea," & vbLf & _
        "A wind blew out of a cloud, chilling" & vbLf & _
        "   My beautiful Annabel Lee;" & vbLf & _
        "So that her highborn kinsmen came" & vbLf & _
        "   And bore her away from me," & vbLf & _
        "To shut her up in a sepulchre," & vbLf & _
        "   In this kingdom by the sea." & vbLf & _
        vbLf & _
        "The angels, not half so happy in Heaven," & vbLf & _
        "   Went envying her and me" & ChrW(&H2014) & vbLf & _
        "Yes! that was the reason (as all men know," & vbLf & _
        "   In this kingdom by the sea)" & vbLf & _
        "That the wind came out of the cloud by night," & vbLf & _
        "   Chilling and killing my Annabel Lee." & vbLf & _
        vbLf & _
        "But our love it was stronger by far than the love" & vbLf & _
        "   Of those who were older than we" & ChrW(&H2014) & vbLf & _
        "   Of many far wiser than we" & ChrW(&H2014) & vbLf & _
        "And neither the angels in Heaven above" & vbLf & _
        "   Nor the demons down under the sea" & vbLf & _
        "Can ever dissever my soul from the soul" & vbLf & _
        "   Of the beautiful Annabel Lee:" & ChrW(&H2014) & vbLf & _
        vbLf & _
        "For the moon never beams, without bringing me dreams" & vbLf & _
        "   Of the beautiful Annabel Lee;" & vbLf & _
        "And the stars never rise, but I feel the bright eyes" & vbLf & _
        "   Of the beautiful Annabel Lee:" & ChrW(&H2014) & vbLf & _
        "And so, all the night-tide, I lie down by the side" & vbLf & _
        "Of my darling" & ChrW(&H2014) & "my darling" & ChrW(&H2014) & _
                                        "my life and my bride," & vbLf & _
        "   In her sepulchre there by the sea" & ChrW(&H2014) & vbLf & _
        "   In her tomb by the sounding sea." & vbLf & _
        vbLf & _
        "                                       [May 1849]"

    ReadOnly iTextLines As Integer = 0
    Private iClientLines As Integer
    Private iStartLine As Integer = 0
    Private cyText As Single

    Shared Sub Main()
        ' See whether the program makes sense.
        If Not SystemInformation.MouseWheelPresent Then
            MessageBox.Show("Program needs a mouse with a mouse wheel!", _
                            "PoePoem", MessageBoxButtons.OK, _
                            MessageBoxIcon.Error)
            Return
        End If

        ' Otherwise go normally.
        Application.Run(New PoePoem())
    End Sub

    Sub New()
        Text = """Annabel Lee"" by Edgar Allan Poe"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText
        ResizeRedraw = True

        ' Calculate the number of lines in the text.
        Dim iIndex As Integer = strAnnabelLee.IndexOf(vbLf, 0)
        While iIndex <> -1
            iTextLines += 1
            iIndex = strAnnabelLee.IndexOf(vbLf, iIndex + 1)
        End While

        ' Obtain line-spacing value.
        'Dim grfx As Graphics = CreateGraphics()
        cyText = Font.Height
        'grfx.Dispose()
        OnResize(EventArgs.Empty)
    End Sub

    Protected Overrides Sub OnResize(ByVal ea As EventArgs)
        MyBase.OnResize(ea)
        iClientLines = CInt(Math.Floor(ClientSize.Height / cyText))
        iStartLine = Math.Max(0, _
                     Math.Min(iStartLine, iTextLines - iClientLines))
    End Sub

    Protected Overrides Sub OnMouseWheel(ByVal mea As MouseEventArgs)
        Dim iScroll As Integer = _
            mea.Delta * SystemInformation.MouseWheelScrollLines \ 120
        iStartLine -= iScroll
        iStartLine = Math.Max(0, _
                     Math.Min(iStartLine, iTextLines - iClientLines))
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        grfx.DrawString(strAnnabelLee, Font, New SolidBrush(ForeColor), _
                        0, -iStartLine * cyText)
    End Sub
End Class
