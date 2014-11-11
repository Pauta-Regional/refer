'------------------------------------------
' ImageDrop.vb (c) 2002 by Charles Petzold
'------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Windows.Forms

Class ImageDrop
    Inherits ImageClip

    Private bIsTarget As Boolean

    Shared Shadows Sub Main()
        Application.Run(New ImageDrop())
    End Sub

    Sub New()
        strProgName = "Image Drop"
        Text = strProgName
        AllowDrop = True
    End Sub

    Protected Overrides Sub OnDragOver(ByVal dea As DragEventArgs)
        If dea.Data.GetDataPresent(DataFormats.FileDrop) OrElse _
            dea.Data.GetDataPresent(GetType(Metafile)) OrElse _
            dea.Data.GetDataPresent(GetType(Bitmap)) Then
            If (dea.AllowedEffect And DragDropEffects.Move) <> 0 Then
                dea.Effect = DragDropEffects.Move
            End If
            If ((dea.AllowedEffect And DragDropEffects.Copy) <> 0) AndAlso _
                ((dea.KeyState And &H8) <> 0) Then
                dea.Effect = DragDropEffects.Copy
            End If
        End If
    End Sub

    Protected Overrides Sub OnDragDrop(ByVal dea As DragEventArgs)
        If dea.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim astr() As String = _
                DirectCast(dea.Data.GetData(DataFormats.FileDrop), String())
            Try
                img = Image.FromFile(astr(0))
            Catch exc As Exception
                MessageBox.Show(exc.Message, Text)
                Return
            End Try

            strFileName = astr(0)
            Text = strProgName & " - " & Path.GetFileName(strFileName)
            Invalidate()
        Else
            If dea.Data.GetDataPresent(GetType(Metafile)) Then
                img = DirectCast(dea.Data.GetData(GetType(Metafile)), Image)
            ElseIf dea.Data.GetDataPresent(GetType(Bitmap)) Then
                img = DirectCast(dea.Data.GetData(GetType(Bitmap)), Image)
            End If

            bIsTarget = True
            strFileName = "DragAndDrop"
            Text = strProgName & " - " & strFileName
            Invalidate()
        End If
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal mea As MouseEventArgs)
        If Not img Is Nothing Then
            bIsTarget = False
            Dim dde As DragDropEffects = DoDragDrop(img, _
                            DragDropEffects.Copy Or DragDropEffects.Move)
            If dde = DragDropEffects.Move AndAlso Not bIsTarget Then
                img = Nothing
            End If
        End If
    End Sub
End Class
