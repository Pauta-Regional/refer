'-------------------------------------------------
' StreamWriterDemo.vb (c) 2002 by Charles Petzold
'-------------------------------------------------
Imports System
Imports System.IO

Module StreamWriterDemo
    Sub Main()
        Dim sw As New StreamWriter("StreamWriterDemo.txt", True)
        sw.WriteLine("You ran the StreamWriterDemo program on {0}", _
                     DateTime.Now)
        sw.Close()
    End Sub
End Module
