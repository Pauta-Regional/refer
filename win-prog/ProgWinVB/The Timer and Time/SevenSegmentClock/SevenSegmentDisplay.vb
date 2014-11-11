'----------------------------------------------------
' SevenSegmentDisplay.vb (c) 2002 by Charles Petzold
'----------------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SevenSegmentDisplay
    ' Indicates what segments are illuminated for all 10 digits
    Shared bySegment(,) As Byte = {{1, 1, 1, 0, 1, 1, 1}, _
                                   {0, 0, 1, 0, 0, 1, 0}, _
                                   {1, 0, 1, 1, 1, 0, 1}, _
                                   {1, 0, 1, 1, 0, 1, 1}, _
                                   {0, 1, 1, 1, 0, 1, 0}, _
                                   {1, 1, 0, 1, 0, 1, 1}, _
                                   {1, 1, 0, 1, 1, 1, 1}, _
                                   {1, 0, 1, 0, 0, 1, 0}, _
                                   {1, 1, 1, 1, 1, 1, 1}, _
                                   {1, 1, 1, 1, 0, 1, 1}}

    ' Points that define each of the seven segments
    ReadOnly apt(6)() As Point

    ' A field initialized by the constructor
    ReadOnly grfx As Graphics

    ' Only constructor requires Graphics argument
    Sub New(ByVal grfx As Graphics)
        Me.grfx = grfx

        ' Initialize jagged Point array.
        apt(0) = New Point() {New Point(3, 2), New Point(39, 2), _
                              New Point(31, 10), New Point(11, 10)}
        apt(1) = New Point() {New Point(2, 3), New Point(10, 11), _
                              New Point(10, 31), New Point(2, 35)}
        apt(2) = New Point() {New Point(40, 3), New Point(40, 35), _
                              New Point(32, 31), New Point(32, 11)}
        apt(3) = New Point() {New Point(3, 36), New Point(11, 32), _
                              New Point(31, 32), New Point(39, 36), _
                              New Point(31, 40), New Point(11, 40)}
        apt(4) = New Point() {New Point(2, 37), New Point(10, 41), _
                              New Point(10, 61), New Point(2, 69)}
        apt(5) = New Point() {New Point(40, 37), New Point(40, 69), _
                              New Point(32, 61), New Point(32, 41)}
        apt(6) = New Point() {New Point(11, 62), New Point(31, 62), _
                              New Point(39, 70), New Point(3, 70)}
    End Sub

    Function MeasureString(ByVal str As String, ByVal fnt As Font) As SizeF
        Dim szf As New SizeF(0, grfx.DpiX * fnt.SizeInPoints / 72)
        Dim ch As Char
        For Each ch In str
            If Char.IsDigit(ch) Then
                szf.Width += 42 * grfx.DpiX * fnt.SizeInPoints / 72 / 72
            ElseIf ch.Equals(":"c) Then
                szf.Width += 12 * grfx.DpiX * fnt.SizeInPoints / 72 / 72
            End If
        Next ch
        Return szf
    End Function

    Sub DrawString(ByVal str As String, ByVal fnt As Font, _
                   ByVal br As Brush, ByVal x As Single, ByVal y As Single)
        Dim ch As Char
        For Each ch In str
            If Char.IsDigit(ch) Then
                x = Number(AscW(ch) - AscW("0"), fnt, br, x, y)
            ElseIf ch.Equals(":"c) Then
                x = Colon(fnt, br, x, y)
            End If
        Next ch
    End Sub

    Private Function Number(ByVal num As Integer, ByVal fnt As Font, _
                            ByVal br As Brush, ByVal x As Single, _
                            ByVal y As Single) As Single
        Dim i As Integer
        For i = 0 To apt.GetUpperBound(0)
            If bySegment(num, i) = 1 Then
                Fill(apt(i), fnt, br, x, y)
            End If
        Next i
        Return x + 42 * grfx.DpiX * fnt.SizeInPoints / 72 / 72
    End Function

    Private Function Colon(ByVal fnt As Font, ByVal br As Brush, _
                           ByVal x As Single, ByVal y As Single) As Single
        Dim i As Integer
        Dim apt(1)() As Point
        apt(0) = New Point() {New Point(2, 21), New Point(6, 17), _
                              New Point(10, 21), New Point(6, 25)}
        apt(1) = New Point() {New Point(2, 51), New Point(6, 47), _
                              New Point(10, 51), New Point(6, 55)}

        For i = 0 To apt.GetUpperBound(0)
            Fill(apt(i), fnt, br, x, y)
        Next i
        Return x + 12 * grfx.DpiX * fnt.SizeInPoints / 72 / 72
    End Function

    Private Sub Fill(ByVal apt() As Point, ByVal fnt As Font, _
                     ByVal br As Brush, ByVal x As Single, ByVal y As Single)
        Dim i As Integer
        Dim aptf(apt.GetUpperBound(0)) As PointF

        For i = 0 To apt.GetUpperBound(0)
            aptf(i).X = x + apt(i).X * grfx.DpiX * fnt.SizeInPoints / 72 / 72
            aptf(i).Y = y + apt(i).Y * grfx.DpiY * fnt.SizeInPoints / 72 / 72
        Next i
        grfx.FillPolygon(br, aptf)
    End Sub
End Class
