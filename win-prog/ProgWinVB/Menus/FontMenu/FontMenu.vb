'-----------------------------------------
' FontMenu.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class FontMenu
    Inherits Form

    Const iPointSize As Integer = 24
    Private strFacename As String

    Shared Sub Main()
        Application.Run(New FontMenu())
    End Sub

    Sub New()
        Text = "Font Menu"
        strFacename = Font.Name
        Menu = New MainMenu()

        Dim mi As New MenuItem("&Facename")
        AddHandler mi.Popup, AddressOf MenuFacenameOnPopup
        mi.MenuItems.Add(" ")         ' Necessary for pop-up call
        Menu.MenuItems.Add(mi)
    End Sub

    Private Sub MenuFacenameOnPopup(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        Dim miFacename As MenuItem = DirectCast(obj, MenuItem)
        Dim aff() As FontFamily = FontFamily.Families
        Dim ehClick As EventHandler = AddressOf MenuFacenameOnClick
        Dim ami(aff.GetUpperBound(0)) As MenuItem
        Dim i As Integer

        For i = 0 To aff.GetUpperBound(0)
            ami(i) = New MenuItem(aff(i).Name)
            AddHandler ami(i).Click, ehClick

            If aff(i).Name = strFacename Then
                ami(i).Checked = True
            End If
        Next i
        miFacename.MenuItems.Clear()
        miFacename.MenuItems.AddRange(ami)
    End Sub

    Private Sub MenuFacenameOnClick(ByVal obj As Object, _
                                    ByVal ea As EventArgs)
        Dim mi As MenuItem = DirectCast(obj, MenuItem)
        strFacename = mi.Text
        Invalidate()
    End Sub

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        Dim grfx As Graphics = pea.Graphics
        Dim fnt As New Font(strFacename, iPointSize)
        Dim strfmt As New StringFormat()

        strfmt.Alignment = StringAlignment.Center
        strfmt.LineAlignment = StringAlignment.Center

        grfx.DrawString("Sample Text", fnt, New SolidBrush(ForeColor), _
                        RectangleF.op_Implicit(ClientRectangle), strfmt)
    End Sub
End Class
