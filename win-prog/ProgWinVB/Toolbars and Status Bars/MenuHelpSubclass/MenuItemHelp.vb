'---------------------------------------------
' MenuItemHelp.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class MenuItemHelp
    Inherits MenuItem

    ' Private fields
    Private sbpHelpPanel As StatusBarPanel
    Private strHelpText As String

    ' Constructors
    Sub New()
    End Sub

    Sub New(ByVal strText As String)
        MyBase.New(strText)
    End Sub

    ' Properties
    Property HelpPanel() As StatusBarPanel
        Set(ByVal Value As StatusBarPanel)
            sbpHelpPanel = Value
        End Set
        Get
            Return sbpHelpPanel
        End Get
    End Property

    Property HelpText() As String
        Set(ByVal Value As String)
            strHelpText = Value
        End Set
        Get
            Return strHelpText
        End Get
    End Property

    ' Method override
    Protected Overrides Sub OnSelect(ByVal ea As EventArgs)
        MyBase.OnSelect(ea)
        If Not HelpPanel Is Nothing Then
            HelpPanel.Text = HelpText
        End If
    End Sub
End Class
