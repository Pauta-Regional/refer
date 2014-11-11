'------------------------------------------------
' InheritingTheClass.vb (c) 2002 by Charles Petzold
'------------------------------------------------
Imports System

Module InheritingTheClass
    Sub Main()
        Dim birth As New EnhancedDate(1953, 2, 2)
        Dim today As New EnhancedDate(2002, 8, 29)

        Console.WriteLine("Birthday = {0}", birth)
        Console.WriteLine("Today = " & today.ToString())
        Console.WriteLine("Days since birthday = {0}", _
                          today.Subtract(birth))
    End Sub
End Module

Class EnhancedDate
    Inherits CalendarDate

    ' Private field
    Private Shared str() As String = _
                        {"Jan", "Feb", "Mar", "Apr", "May", "Jun", _
                         "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"}
    ' Public constructor
    Sub New(ByVal yr As Integer, ByVal mn As Integer, ByVal dy As Integer)
        MyBase.New(yr, mn, dy)
    End Sub

    ' Public property
    ReadOnly Property DaysSince1600() As Integer
        Get
            Return 365 * (Year - 1600) + _
                         (Year - 1597) \ 4 - _
                         (Year - 1601) \ 100 + _
                         (Year - 1601) \ 400 + DayOfYear
        End Get
    End Property

    ' Public methods
    Overrides Function ToString() As String
        Return String.Format("{0} {1} {2}", Day, str(Month - 1), Year)
    End Function

    Function Subtract(ByVal subtrahend As EnhancedDate) As Integer
        Return Me.DaysSince1600 - subtrahend.DaysSince1600
    End Function
End Class
