'---------------------------------------------
' ImageFromWeb.vb (c) 2002 by Charles Petzold
'---------------------------------------------
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Windows.Forms

Class ImageFromWeb
    Inherits PrintableForm

    Private img As image

    Shared Shadows Sub Main()
        Application.Run(new ImageFromWeb())
    End Sub

    Sub New()
        Text = "Image From Web"

        Dim strUrl As String = _
                "http://images.jsc.nasa.gov/images/pao/AS11/10075267.jpg"

        Dim webreq As WebRequest = WebRequest.Create(strUrl)
        Dim webres As WebResponse = webreq.GetResponse()
        Dim strm As Stream = webres.GetResponseStream()

        img = Image.FromStream(strm)
        strm.Close()
    End Sub

    Protected Overrides Sub DoPage(ByVal grfx As Graphics, _
            ByVal clr As Color, ByVal cx As Integer, ByVal cy As Integer)
        grfx.DrawImage(img, 0, 0)
    End Sub
End Class
