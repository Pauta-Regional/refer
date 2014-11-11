'---------------------------------------------------
' AddingConstructors.vb (c) 2002 by Charles Petzold
'---------------------------------------------------
Imports System

Module AddingConstructors
    Sub Main()
        Try
            Dim today As New CalendarDate(2002, 8, 29)
            Console.WriteLine("Day of year = {0}", today.DayOfYear())
        Catch exc As Exception
            Console.WriteLine(exc)
        End Try
    End Sub
End Module

Class CalendarDate
    ' Private fields
    Private yr As Integer
    Private mn As Integer
    Private dy As Integer
    Private Shared MonthDays() As Integer = _
                {0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334}

    ' Public constructors
    Sub New()
        Year = 1600
        Month = 1
        Day = 1
    End Sub

    Sub New(ByVal yr As Integer, ByVal mn As Integer, ByVal dy As Integer)
        If (mn = 2 AndAlso IsLeapYear(yr) AndAlso dy > 29) OrElse _
           (mn = 2 AndAlso Not IsLeapYear(yr) AndAlso dy > 28) OrElse _
           ((mn = 4 OrElse mn = 6 OrElse _
                           mn = 9 OrElse mn = 11) AndAlso dy > 30) Then
            Throw New ArgumentOutOfRangeException("Day")
        Else
            Year = yr
            Month = mn
            Day = dy
        End If
    End Sub

    ' Public properties
    Property Year() As Integer
        Set(ByVal Value As Integer)
            If (Value < 1600) Then
                Throw New ArgumentOutOfRangeException("Year")
            Else
                yr = Value
            End If
        End Set
        Get
            Return yr
        End Get
    End Property

    Property Month() As Integer
        Set(ByVal Value As Integer)
            If (Value < 1 Or Value > 12) Then
                Throw New ArgumentOutOfRangeException("Month")
            Else
                mn = Value
            End If
        End Set
        Get
            Return mn
        End Get
    End Property

    Property Day() As Integer
        Set(ByVal Value As Integer)
            If (Value < 1 Or Value > 31) Then
                Throw New ArgumentOutOfRangeException("Day")
            Else
                dy = Value
            End If
        End Set
        Get
            Return dy
        End Get
    End Property

    ReadOnly Property DayOfYear() As Integer
        Get
            DayOfYear = MonthDays(Month - 1) + Day
            If Month > 2 AndAlso IsLeapYear(Year) Then DayOfYear += 1
        End Get
    End Property

    ' Public method
    Shared Function IsLeapYear(ByVal yr As Integer) As Boolean
        Return yr Mod 4 = 0 AndAlso (yr Mod 100 <> 0 Or yr Mod 400 = 0)
    End Function
End Class
