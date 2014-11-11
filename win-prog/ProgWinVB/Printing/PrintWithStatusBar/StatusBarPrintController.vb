'---------------------------------------------------------
' StatusBarPrintController.vb (c) 2002 by Charles Petzold
'---------------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Class StatusBarPrintController
    Inherits StandardPrintController

    Private sbp As StatusBarPanel
    Private iPageNumber As Integer
    Private strSaveText As String

    Sub New(ByVal sbp As StatusBarPanel)
        Me.sbp = sbp
    End Sub

    Overrides Sub OnStartPrint(ByVal prndoc As PrintDocument, _
                               ByVal pea As PrintEventArgs)
        strSaveText = sbp.Text        ' Probably "Ready" or similar.
        sbp.Text = "Starting printing"
        iPageNumber = 1
        MyBase.OnStartPrint(prndoc, pea)
    End Sub

    Overrides Function OnStartPage(ByVal prndoc As PrintDocument, _
                            ByVal ppea As PrintPageEventArgs) As Graphics
        sbp.Text = "Printing page " & iPageNumber
        iPageNumber += 1
        Return MyBase.OnStartPage(prndoc, ppea)
    End Function

    Overrides Sub OnEndPage(ByVal prndoc As PrintDocument, _
                            ByVal ppea As PrintPageEventArgs)
        MyBase.OnEndPage(prndoc, ppea)
    End Sub

    Overrides Sub OnEndPrint(ByVal prndoc As PrintDocument, _
                             ByVal pea As PrintEventArgs)
        sbp.Text = strSaveText
        MyBase.OnEndPrint(prndoc, pea)
    End Sub
End Class
