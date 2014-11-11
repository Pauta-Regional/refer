'----------------------------------------------------
' BetterImageFromFile.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class BetterImageFromFile
    Inherits PrintableForm

    Private img As image

    Shared Shadows Sub Main()
        Application.Run(new BetterImageFromFile())
    End Sub

    Sub New()
        Text = "Better Image From File"
        Dim strFileName As String = "..\..\Apollo11FullColor.jpg"

        Try
            img = Image.FromFile(strFileName)
        Catch
            MessageBox.Show("Cannot find file " & strFileName & "!", _
                            Text, MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End Try
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        If Not img Is Nothing Then grfx.DrawImage(img, 0, 0)
    End Sub
End Class
