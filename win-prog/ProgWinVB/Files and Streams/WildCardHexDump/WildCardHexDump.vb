'------------------------------------------------
' WildCardHexDump.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System
Imports System.IO

Module WildCardHexDump
    Function Main(ByVal astrArgs() As String) As Integer
        ' If there's no argument, display syntax and ask for filenames.
        If astrArgs.Length = 0 Then
            Console.WriteLine("Syntax: WildCardHexDump file1 file2 ...")
            Console.Write("Enter file specifications(s): ")
            astrArgs = Console.ReadLine().Split(Nothing)
        End If

        ' If the user enters nothing, dive out.
        If astrArgs(0).Length = 0 Then
            Return 1
        End If

        ' Dump each file or collection of files.
        Dim str As String
        For Each str In astrArgs
            ExpandWildCard(str)
        Next str

        Return 0
    End Function

    Private Sub ExpandWildCard(ByVal strWildCard As String)
        Dim strFile, astrFiles(), strDir As String

        Try
            astrFiles = Directory.GetFiles(strWildCard)
        Catch
            Try
                strFile = Path.GetFileName(strWildCard)
                If strDir Is Nothing OrElse strDir.Length = 0 Then
                    strDir = "."
                End If
                astrFiles = Directory.GetFiles(strDir, strFile)
            Catch
                Console.WriteLine(strWildCard & ": No Files found!")
                Return
            End Try
        End Try

        If astrFiles.Length = 0 Then
            Console.WriteLine(strWildCard & ": No files found!")
        End If

        For Each strFile In astrFiles
            HexDump.DumpFile(strFile)
        Next strFile
    End Sub
End Module
