'----------------------------------------
' HexDump.vb (c) 2002 by Charles Petzold
'----------------------------------------
Imports System
Imports System.IO

Module HexDump
    Function Main(ByVal astrArgs() As String) As Integer
        ' If there's no argument, display syntax and ask for filenames.
        If astrArgs.Length = 0 Then
            Console.WriteLine("Syntax: HexDump file1 file2 ...")
            Console.Write("Enter file name(s): ")
            astrArgs = Console.ReadLine().Split(Nothing)
        End If

        ' If the user enters nothing, dive out.
        If astrArgs(0).Length = 0 Then
            Return 1
        End If

        ' Dump each file.
        Dim strFileName As String
        For Each strFileName In astrArgs
            DumpFile(strFileName)
        Next strFileName
        Return 0
    End Function

    Sub DumpFile(ByVal strFileName As String)
        Dim fs As FileStream
        Try
            fs = New FileStream(strFileName, FileMode.Open, _
                                FileAccess.Read, FileShare.Read)
        Catch exc As Exception
            Console.WriteLine("HexDump: {0}", exc.Message)
            Return
        End Try
        Console.WriteLine(strFileName)
        DumpStream(fs)
        fs.Close()
    End Sub

    Private Sub DumpStream(ByVal strm As Stream)
        Dim abyBuffer(16) As Byte
        Dim lAddress As Long = 0
        Dim iCount As Integer = strm.Read(abyBuffer, 0, 16)

        While iCount > 0
            Console.WriteLine(ComposeLine(lAddress, abyBuffer, iCount))
            lAddress += 16
            iCount = strm.Read(abyBuffer, 0, 16)
        End While
    End Sub

    Function ComposeLine(ByVal lAddress As Long, ByVal abyBuffer() As Byte, _
                         ByVal iCount As Integer) As String
        Dim str As String = String.Format("{0:X4}-{1:X4}  ", _
                                          CInt(lAddress \ 65536), _
                                          CInt(lAddress Mod 65536))
        Dim i As Integer

        ' Format hexadecimal bytes.
        For i = 0 To 15
            If i < iCount Then
                str &= String.Format("{0:X2}", abyBuffer(i))
            Else
                str &= "  "
            End If

            If i = 7 AndAlso iCount > 7 Then
                str &= "-"
            Else
                str &= " "
            End If
        Next i

        str &= " "

        ' Format character display
        For i = 0 To 15
            Dim ch As Char = Chr(abyBuffer(i))

            If i >= iCount Then ch = " "c
            If Char.IsControl(ch) Then ch = "."c

            str += ch.ToString()
        Next i

        Return str
    End Function
End Module
