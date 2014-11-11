'------------------------------------------------
' MetafileConvert.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO                  ' For Path class
Imports System.Windows.Forms

Class MetafileConvert
    Inherits MetafileViewer

    Shared Shadows Sub Main()
        Application.Run(new MetafileConvert())
    End Sub

    Sub New()
        strProgName = "Metafile Convert"
        Text = strProgName
    End Sub

    Protected Overrides Sub MenuFileSaveAsOnClick(ByVal obj As Object, _
                                                  ByVal ea As EventArgs)
        Dim dlg As New SaveFileDialog()
        If Not strFileName Is Nothing AndAlso strFileName.Length > 0 Then
            dlg.InitialDirectory = Path.GetDirectoryName(strFileName)
        End If
        dlg.FileName = Path.GetFileNameWithoutExtension(strFileName)
        dlg.AddExtension = True
        dlg.Filter = "Windows Bitmap (*.bmp)|*.bmp|" & _
                     "Graphics Interchange Format (*.gif)|*.gif|" & _
                     "JPEG File Interchange Format (*.jpg)|" & _
                         "*.jpg;*.jpeg;*.jfif|" & _
                     "Portable Network Graphics (*.png)|*.png|" & _
                     "Tagged Image File Format (*.tif)|*.tif;*.tiff"

        Dim aif As ImageFormat() = {ImageFormat.Bmp, ImageFormat.Gif, _
                                    ImageFormat.Jpeg, ImageFormat.Png, _
                                    ImageFormat.Tiff}

        If dlg.ShowDialog() = DialogResult.OK Then
            Dim bm As Bitmap = MetafileToBitmap(mf)
            Try
                bm.Save(dlg.FileName, aif(dlg.FilterIndex - 1))
            Catch exc As Exception
                MessageBox.Show(exc.Message, Text)
            End Try
        End If
    End Sub

    Private Function MetafileToBitmap(ByVal mf As Metafile) As Bitmap
        Dim grfx As Graphics = CreateGraphics()
        Dim cx As Integer = CInt(grfx.DpiX * mf.Width / _
                                             mf.HorizontalResolution)
        Dim cy As Integer = CInt(grfx.DpiY * mf.Height / _
                                             mf.VerticalResolution)
        Dim bm As New Bitmap(cx, cy, grfx)
        grfx.Dispose()

        grfx = Graphics.FromImage(bm)
        grfx.DrawImage(mf, 0, 0, cx, cy)
        grfx.Dispose()
        Return bm
    End Function
End Class
