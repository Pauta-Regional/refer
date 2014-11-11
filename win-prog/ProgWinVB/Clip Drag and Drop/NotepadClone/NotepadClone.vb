'---------------------------------------------
' NotepadClone.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class NotepadClone
    Inherits NotepadCloneWithPrinting

    Shared Shadows Sub Main()
        Application.Run(New NotepadClone())
    End Sub

    Sub New()
        strProgName = "NotepadClone"
        MakeCaption()

        txtbox.AllowDrop = True
        AddHandler txtbox.DragOver, AddressOf TextBoxOnDragOver
        AddHandler txtbox.DragDrop, AddressOf TextBoxOnDragDrop
    End Sub

    Private Sub TextBoxOnDragOver(ByVal obj As Object, _
                                  ByVal dea As DragEventArgs)
        If dea.Data.GetDataPresent(DataFormats.FileDrop) OrElse _
            dea.Data.GetDataPresent(DataFormats.StringFormat) Then
            If (dea.AllowedEffect And DragDropEffects.Move) <> 0 Then
                dea.Effect = DragDropEffects.Move
            End If
            If ((dea.AllowedEffect And DragDropEffects.Copy) <> 0) AndAlso _
                ((dea.KeyState And &H8) <> 0) Then
                dea.Effect = DragDropEffects.Copy
            End If
        End If
    End Sub

    Private Sub TextBoxOnDragDrop(ByVal obj As Object, _
                                  ByVal dea As DragEventArgs)
        If dea.Data.GetDataPresent(DataFormats.FileDrop) Then
            If Not OkToTrash() Then Return
            Dim astr() As String = _
                DirectCast(dea.Data.GetData(DataFormats.FileDrop), String())
            LoadFile(astr(0)) ' In NotepadCloneWithFile.cs
        ElseIf dea.Data.GetDataPresent(DataFormats.StringFormat) Then
            txtbox.SelectedText = _
                DirectCast(dea.Data.GetData(DataFormats.StringFormat), _
                           String)
        End If
    End Sub
End Class
