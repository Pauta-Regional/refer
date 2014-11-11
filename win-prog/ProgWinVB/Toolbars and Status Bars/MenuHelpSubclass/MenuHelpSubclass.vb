'-------------------------------------------------
' MenuHelpSubclass.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class MenuHelpSubclass
    Inherits Form

    Private sbpMenuHelp As StatusBarPanel
    Private strSavePanelText As String

    Shared Sub Main()
        Application.Run(New MenuHelpSubclass())
    End Sub

    Sub New()
        Text = "Menu Help"
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

        ' Construct a simple menu with MenuItemHelp items.
        Menu = New MainMenu()

        ' Create File menu items.
        Dim mih As New MenuItemHelp("&File")
        mih.HelpPanel = sbpMenuHelp
        mih.HelpText = "Commands for working with files"
        Menu.MenuItems.Add(mih)

        mih = New MenuItemHelp("&Open...")
        mih.HelpPanel = sbpMenuHelp
        mih.HelpText = "Opens an existing document"
        Menu.MenuItems(0).MenuItems.Add(mih)

        mih = New MenuItemHelp("&Close")
        mih.HelpPanel = sbpMenuHelp
        mih.HelpText = "Closes the current document"
        Menu.MenuItems(0).MenuItems.Add(mih)

        mih = New MenuItemHelp("&Save")
        mih.HelpPanel = sbpMenuHelp
        mih.HelpText = "Saves the current document"
        Menu.MenuItems(0).MenuItems.Add(mih)

        ' Create Edit menu items.
        mih = New MenuItemHelp("&Edit")
        mih.HelpPanel = sbpMenuHelp
        mih.HelpText = "Commands for editing the document"
        Menu.MenuItems.Add(mih)

        mih = New MenuItemHelp("Cu&t")
        mih.HelpPanel = sbpMenuHelp
        mih.HelpText = "Deletes the selection and " & _
                       "copies it to the clipboard"
        Menu.MenuItems(1).MenuItems.Add(mih)

        mih = New MenuItemHelp("&Copy")
        mih.HelpPanel = sbpMenuHelp
        mih.HelpText = "Copies the selection to the clipboard"
        Menu.MenuItems(1).MenuItems.Add(mih)

        mih = New MenuItemHelp("&Paste")
        mih.HelpPanel = sbpMenuHelp
        mih.HelpText = "Replaces the current selection " & _
                       "with the clipboard contents"
        Menu.MenuItems(1).MenuItems.Add(mih)
    End Sub

    Protected Overrides Sub OnMenuStart(ByVal ea As EventArgs)
        strSavePanelText = sbpMenuHelp.Text
    End Sub

    Protected Overrides Sub OnMenuComplete(ByVal ea As EventArgs)
        sbpMenuHelp.Text = strSavePanelText
    End Sub
End Class
