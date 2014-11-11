'------------------------------------------------
' SysInfoListView.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SysInfoListView
    Inherits Form

    Shared Sub Main()
        Application.Run(New SysInfoListView())
    End Sub

    Sub New()
        Text = "System Information (List View)"

        ' Create ListView control.
        Dim listvu As New ListView()
        listvu.Parent = Me
        listvu.Dock = DockStyle.Fill
        listvu.View = View.Details

        ' Define columns based on maximum string widths.
        Dim grfx As Graphics = CreateGraphics()
        listvu.Columns.Add("Property", _
                CInt(SysInfoReflectionStrings.MaxLabelWidth(grfx, Font)), _
                HorizontalAlignment.Left)
        listvu.Columns.Add("Value", _
                CInt(SysInfoReflectionStrings.MaxValueWidth(grfx, Font)), _
                HorizontalAlignment.Left)
        grfx.Dispose()

        ' Get the data that will be displayed.
        Dim iNumItems As Integer = SysInfoReflectionStrings.Count
        Dim astrLabels() As String = SysInfoReflectionStrings.Labels
        Dim astrValues() As String = SysInfoReflectionStrings.Values

        ' Define the items and subitems.
        Dim i As Integer
        For i = 0 To iNumItems - 1
            Dim lvi As New ListViewItem(astrLabels(i))
            lvi.SubItems.Add(astrValues(i))
            listvu.Items.Add(lvi)
        Next i
    End Sub
End Class
