'-----------------------------------------
' HtmlDump.vb (c) 2002 by Charles Petzold
'-----------------------------------------
Imports System
Imports System.IO
Imports System.Net

Module HtmlDump
    Function Main(ByVal astrArgs() As String) As Integer
        Dim strUri As String

        ' If there's no argument, display syntax and ask for URI.
        If astrArgs.Length = 0 Then
            Console.WriteLine("Syntax: HtmlDump URI")
            Console.Write("Enter URI: ")
            strUri = Console.ReadLine()
        Else
            strUri = astrArgs(0)
        End If

        ' If the user enters a blank string, dive out.
        If strUri.Length = 0 Then
            Return 1
        End If

        Dim webreq As WebRequest
        Dim webres As WebResponse

        Try
            webreq = WebRequest.Create(strUri)
            webres = webreq.GetResponse()
        Catch exc As Exception
            Console.WriteLine("HtmlDump: {0}", exc.Message)
            Return 1
        End Try

        If webres.ContentType.Substring(0, 4) <> "text" Then
            Console.WriteLine("HtmlDump: URI must be a text type.")
            Return 1
        End If

        Dim strm As Stream = webres.GetResponseStream()
        Dim sr As New StreamReader(strm)
        Dim strLine As String
        Dim iLine As Integer = 1

        strLine = sr.ReadLine()
        While Not strLine Is Nothing
            Console.WriteLine("{0:D5}: {1}", iLine, strLine)
            iLine += 1
            strLine = sr.ReadLine()
        End While

        strm.Close()
        Return 0
    End Function
End Module
