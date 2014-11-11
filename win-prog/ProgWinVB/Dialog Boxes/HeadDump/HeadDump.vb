'-----------------------------------------
' HeadDump.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Class HeadDump
    Inherits Form

    Const strProgName As String = "Head Dump"
    Private strFileName As String = ""

    Shared Sub Main()
        Application.Run(New HeadDump())
    End Sub

    Sub New()
        Text = strProgName
        Font = New Font(FontFamily.GenericMonospace, Font.SizeInPoints)

        Menu = New MainMenu()
        Menu.MenuItems.Add("&File")
        Menu.MenuItems(0).MenuItems.Add("&Open...", _
                                AddressOf MenuFileOpenOnClick)
        Menu.MenuItems.Add("F&ormat")
        Menu.MenuItems(1).MenuItems.Add("&Font...", _
                                AddressOf MenuFormatFontOnClick)
    End Sub

    Sub MenuFileOpenOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim dlg As New OpenFileDialog()

        If dlg.ShowDialog() = DialogResult.OK Then
            strFileName = dlg.FileName
            Text = strProgName & " - " & Path.GetFileName(strFileName)
            Invalidate()
        End If
    End Sub

    Sub MenuFormatFontOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim dlg As New FontDialog()

        dlg.Font = Font
        dlg.FixedPitchOnly = True

        If dlg.ShowDialog() = DialogResult.OK Then
            Font = dlg.Font
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim br As New SolidBrush(ForeColor)
        Dim fs As FileStream
        Dim iLine As Integer

        Try
            fs = New FileStream(strFileName, FileMode.Open, _
                                FileAccess.Read, FileShare.Read)
        Catch
            Return
        End Try

        For iLine = 0 To ClientSize.Height \ Font.Height
            Dim abyBuffer(16) As Byte
            Dim iCount As Integer = fs.Read(abyBuffer, 0, 16)

            If iCount = 0 Then Exit For

            Dim str As String = HexDump.ComposeLine(16 * iLine, _
                                                    abyBuffer, iCount)
            grfx.DrawString(str, Font, br, 0, iLine * Font.Height)
        Next iLine

        fs.Close()
    End Sub
End Class
