'----------------------------------------
' ImageIO.vb (c) 2002 by Charles Petzold
'----------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Forms

Class ImageIO
    Inherits ImageOpen

    Private miSaveAs As MenuItem

    Shared Shadows Sub Main()
        Application.Run(new ImageIO())
    End Sub

    Sub New()
        strProgName = "Image I/O"
        Text = strProgName

        AddHandler Menu.MenuItems(0).Popup, AddressOf MenuFileOnPopup
        miSaveAs = new MenuItem("Save &As...")
        AddHandler miSaveAs.Click, AddressOf MenuFileSaveAsOnClick
        Menu.MenuItems(0).MenuItems.Add(miSaveAs)
    End Sub

    Sub MenuFileOnPopup(ByVal obj As Object, ByVal ea As EventArgs)
        miSaveAs.Enabled = Not img Is Nothing
    End Sub

    Sub MenuFileSaveAsOnClick(ByVal obj As Object, ByVal ea As EventArgs)
        Dim savedlg As New SaveFileDialog()

        savedlg.InitialDirectory = Path.GetDirectoryName(strFileName)
        savedlg.FileName = Path.GetFileNameWithoutExtension(strFileName)
        savedlg.AddExtension = True
        savedlg.Filter = "Windows Bitmap (*.bmp)|*.bmp|" & _
                         "Graphics Interchange Format (*.gif)|*.gif|" & _
                         "JPEG File Interchange Format (*.jpg)|" & _
                                                "*.jpg;* .jpeg;*.jfif|" & _
                         "Portable Network Graphics (*.png)|*.png|" & _
                         "Tagged Imaged File Format (*.tif)|*.tif;*.tiff"

        Dim aif() As ImageFormat = {ImageFormat.Bmp, ImageFormat.Gif, _
                                    ImageFormat.Jpeg, ImageFormat.Png, _
                                    ImageFormat.Tiff}

        If savedlg.ShowDialog() = DialogResult.OK Then
            Try
                img.Save(savedlg.FileName, aif(savedlg.FilterIndex - 1))
            Catch exc As Exception
                MessageBox.Show(exc.Message, Text)
                Return
            End Try

            strFileName = savedlg.FileName
            Text = strProgName & " - " & Path.GetFileName(strFileName)
        End If
    End Sub
End Class
