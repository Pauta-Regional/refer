'------------------------------------------
' ImageOpen.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Class ImageOpen
    Inherits Form

    Protected strProgName As String
    Protected strFileName As String
    Protected img As Image

    Shared Sub Main()
        Application.Run(New ImageOpen())
    End Sub

    Sub New()
        strProgName = "Image Open"
        Text = strProgName
        ResizeRedraw = True

        Menu = New MainMenu()
        Menu.MenuItems.Add("&File")
        Menu.MenuItems(0).MenuItems.Add(New MenuItem("&Open...", _
                                AddressOf MenuFileOpenOnClick, _
                                Shortcut.CtrlO))
    End Sub

    Sub MenuFileOpenOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim dlg As New OpenFileDialog()

        dlg.Filter = "All Image Files|*.bmp;*.ico;*.gif;*.jpeg;*.jpg;" & _
                     "*.jfif;*.png;*.tif;*.tiff;*.wmf;*.emf|" & _
                     "Windows Bitmap (*.bmp)|*.bmp|" & _
                     "Windows Icon (*.ico)|*.ico|" & _
                     "Graphics Interchange Format (*.gif)|*.gif|" & _
                     "JPEG File Interchange Format (*.jpg)|" & _
                                                "*.jpg;*.jpeg;*.jfif|" & _
                     "Portable Network Graphics (*.png)|*.png|" & _
                     "Tag Image File Format (*.tif)|*.tif;*.tiff|" & _
                     "Windows Metafile (*.wmf)|*.wmf|" & _
                     "Enhanced Metafile (*.emf)|*.emf|" & _
                     "All Files (*.*)|*.*"

        If dlg.ShowDialog() = DialogResult.OK Then
            Try
                img = Image.FromFile(dlg.FileName)
            Catch exc As Exception
                MessageBox.Show(exc.Message, strProgName)
                Return
            End Try

            strFileName = dlg.FileName
            Text = strProgName & " - " & Path.GetFileName(strFileName)
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics

        If Not img Is Nothing Then
            grfx.DrawImage(img, 0, 0)
        End If
    End Sub
End Class
