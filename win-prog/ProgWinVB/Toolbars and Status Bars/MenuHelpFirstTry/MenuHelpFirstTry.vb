'-------------------------------------------------
' MenuHelpFirstTry.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class MenuHelpFirstTry
    Inherits Form

    Private sbpMenuHelp As StatusBarPanel
    Private strSavePanelText As String

    Shared Sub Main()
        Application.Run(New MenuHelpFirstTry())
    End Sub

    Sub New()
        Text = "Menu Help (First Try)"
        BackColor = SystemColors.Window
        ForeColor = SystemColors.WindowText

        ' Create a status bar with one panel.
        Dim sbar As New StatusBar()
        sbar.Parent = Me
        sbar.ShowPanels = True
        sbpMenuHelp = New StatusBarPanel()
        sbpMenuHelp.Text = "Ready"
        sbpMenuHelp.AutoSize = StatusBarPanelAutoSize.Spring
        sbar.Panels.Add(sbpMenuHelp)

        ' Construct a simple menu.
        ' For this demo, we can ignore the Click handlers
        '   but what we really need are Select handlers.
        Menu = New MainMenu()
        Dim ehSelect As EventHandler = AddressOf MenuOnSelect

        ' Create File menu items.
        Dim mi As New MenuItem("File")
        AddHandler mi.Select, ehSelect
        Menu.MenuItems.Add(mi)

        mi = New MenuItem("Open")
        AddHandler mi.Select, ehSelect
        Menu.MenuItems(0).MenuItems.Add(mi)

        mi = New MenuItem("Close")
        AddHandler mi.Select, ehSelect
        Menu.MenuItems(0).MenuItems.Add(mi)

        mi = New MenuItem("Save")
        AddHandler mi.Select, ehSelect
        Menu.MenuItems(0).MenuItems.Add(mi)

        ' Create Edit menu items.
        mi = New MenuItem("Edit")
        AddHandler mi.Select, ehSelect
        Menu.MenuItems.Add(mi)

        mi = New MenuItem("Cut")
        AddHandler mi.Select, ehSelect
        Menu.MenuItems(1).MenuItems.Add(mi)

        mi = New MenuItem("Copy")
        AddHandler mi.Select, ehSelect
        Menu.MenuItems(1).MenuItems.Add(mi)

        mi = New MenuItem("Paste")
        AddHandler mi.Select, ehSelect
        Menu.MenuItems(1).MenuItems.Add(mi)
    End Sub

    Protected Overrides Sub OnMenuStart(ByVal ea As EventArgs)
        strSavePanelText = sbpMenuHelp.Text
    End Sub

    Protected Overrides Sub OnMenuComplete(ByVal ea As EventArgs)
        sbpMenuHelp.Text = strSavePanelText
    End Sub

    Private Sub MenuOnSelect(ByVal obj As Object, ByVal ea As EventArgs)
        Dim mi As MenuItem = DirectCast(obj, MenuItem)
        Dim str As String

        Select Case mi.Text
            Case "File" : str = "Commands for working with files"
            Case "Open" : str = "Opens an existing document"
            Case "Close" : str = "Closes the current document"
            Case "Save" : str = "Saves the current document"
            Case "Edit" : str = "Commands for editing the document"
            Case "Cut" : str = "Deletes the selection and " & _
                               "copies it to the clipboard"
            Case "Copy" : str = "Copies the selection to the " & _
                                "clipboard"
            Case "Paste" : str = "Replaces the current selection " & _
                                 "with the clipboard contents"
            Case Else : str = ""
        End Select

        sbpMenuHelp.Text = str
    End Sub
End Class
