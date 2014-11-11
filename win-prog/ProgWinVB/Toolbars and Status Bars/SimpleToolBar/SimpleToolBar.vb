'----------------------------------------------
' SimpleToolBar.vb (c) 2002 by Charles Petzold
'----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SimpleToolBar
    Inherits Form

    Shared Sub Main()
        Application.Run(New SimpleToolBar())
    End Sub

    Sub New()
        Text = "Simple Toolbar"

        ' Create a simple menu (just for show).
        Menu = New MainMenu()
        Menu.MenuItems.Add("File")
        Menu.MenuItems.Add("Edit")
        Menu.MenuItems.Add("View")
        Menu.MenuItems.Add("Help")

        ' Create ImageList.
        Dim bm As New Bitmap(Me.GetType(), "StandardButtons.bmp")

        Dim imglst As New ImageList()
        imglst.Images.AddStrip(bm)
        imglst.TransparentColor = Color.Cyan

        ' Create ToolBar.
        Dim tbar As New ToolBar()
        tbar.Parent = Me
        tbar.ImageList = imglst
        tbar.ShowToolTips = True

        ' Create ToolBarButtons.
        Dim astr() As String = {"New", "Open", "Save", "Print", _
                                "Cut", "Copy", "Paste"}
        Dim i As Integer

        For i = 0 To astr.GetUpperBound(0)
            Dim tbb As New ToolBarButton()
            tbb.ImageIndex = i
            tbb.ToolTipText = astr(i)
            tbar.Buttons.Add(tbb)
        Next i
    End Sub
End Class
