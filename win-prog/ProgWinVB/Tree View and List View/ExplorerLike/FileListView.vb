'---------------------------------------------
' FileListView.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Diagnostics         ' For Process.Start
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Class FileListView
    Inherits ListView

    Private strDirectory As String

    Sub New()
        View = View.Details

        ' Get images for file icons.
        Dim imglst As New ImageList()
        imglst.Images.Add(New Bitmap(Me.GetType(), "DOC.BMP"))
        imglst.Images.Add(New Bitmap(Me.GetType(), "EXE.BMP"))
        SmallImageList = imglst
        LargeImageList = imglst

        ' Create columns.
        Columns.Add("Name", 100, HorizontalAlignment.Left)
        Columns.Add("Size", 100, HorizontalAlignment.Right)
        Columns.Add("Modified", 100, HorizontalAlignment.Left)
        Columns.Add("Attribute", 100, HorizontalAlignment.Left)
    End Sub

    Sub ShowFiles(ByVal strDirectory As String)
        ' Save directory name as field.
        Me.strDirectory = strDirectory
        Items.Clear()

        Dim dirinfo As New DirectoryInfo(strDirectory)
        Dim fi, afi() As FileInfo
        Try
            afi = dirinfo.GetFiles()
        Catch
            Return
        End Try

        For Each fi In afi
            ' Create ListViewItem.
            Dim lvi As New ListViewItem(fi.Name)

            ' Assign ImageIndex based on filename extension.
            If Path.GetExtension(fi.Name).ToUpper() = ".EXE" Then
                lvi.ImageIndex = 1
            Else
                lvi.ImageIndex = 0
            End If

            ' Add file length and modified time sub-items.
            lvi.SubItems.Add(fi.Length.ToString("N0"))
            lvi.SubItems.Add(fi.LastWriteTime.ToString())

            ' Add attribute subitem.
            Dim strAttr As String = ""
            If (fi.Attributes And FileAttributes.Archive) <> 0 Then
                strAttr &= "A"
            End If
            If (fi.Attributes And FileAttributes.Hidden) <> 0 Then
                strAttr &= "H"
            End If
            If (fi.Attributes And FileAttributes.ReadOnly) <> 0 Then
                strAttr &= "R"
            End If
            If (fi.Attributes And FileAttributes.System) <> 0 Then
                strAttr &= "S"
            End If
            lvi.SubItems.Add(strAttr)

            ' Add completed ListViewItem to FileListView.
            Items.Add(lvi)
        Next fi
    End Sub

    Protected Overrides Sub OnItemActivate(ByVal ea As EventArgs)
        MyBase.OnItemActivate(ea)
        Dim lvi As ListViewItem
        For Each lvi In SelectedItems
            Try
                Process.Start(Path.Combine(strDirectory, lvi.Text))
            Catch

            End Try
        Next lvi
    End Sub
End Class
