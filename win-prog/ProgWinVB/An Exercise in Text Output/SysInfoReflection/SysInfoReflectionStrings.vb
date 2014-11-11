'---------------------------------------------------------
' SysInfoReflectionStrings.vb (c) 2002 by Charles Petzold
'---------------------------------------------------------
Imports Microsoft.Win32
Imports System
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms

Class SysInfoReflectionStrings

    ' Fields
    Shared bValidInfo As Boolean = False
    Shared iCount As Integer
    Shared astrLabels(), astrValues() As String

    ' Constructor
    Shared Sub New()
        AddHandler SystemEvents.UserPreferenceChanged, _
                                    AddressOf UserPreferenceChanged
        AddHandler SystemEvents.DisplaySettingsChanged, _
                                    AddressOf DisplaySettingsChanged
    End Sub

    ' Properties
    Shared ReadOnly Property Labels() As String()
        Get
            GetSysInfo()
            Return astrLabels
        End Get
    End Property

    Shared ReadOnly Property Values() As String()
        Get
            GetSysInfo()
            Return astrValues
        End Get
    End Property

    Shared ReadOnly Property Count() As Integer
        Get
            GetSysInfo()
            Return iCount
        End Get
    End Property

    ' Event handlers
    Private Shared Sub UserPreferenceChanged(ByVal obj As Object, _
            ByVal ea As UserPreferenceChangedEventArgs)
        bValidInfo = False
    End Sub

    Private Shared Sub DisplaySettingsChanged(ByVal obj As Object, _
                                              ByVal ea As EventArgs)
        bValidInfo = False
    End Sub

    ' Methods
    Private Shared Sub GetSysInfo()
        If bValidInfo Then Return

        ' Get property information for SystemInformation class.
        Dim type As Type = GetType(SystemInformation)
        Dim apropinfo As PropertyInfo() = type.GetProperties()

        ' Count the number of static readable properties.
        iCount = 0
        Dim pi As PropertyInfo
        For Each pi In apropinfo
            If pi.CanRead AndAlso pi.GetGetMethod().IsStatic Then
                iCount += 1
            End If
        Next pi

        ' Allocate string arrays.
        ReDim astrLabels(iCount - 1)
        ReDim astrValues(iCount - 1)

        ' Loop through the property information classes again.
        iCount = 0
        For Each pi In apropinfo
            If pi.CanRead AndAlso pi.GetGetMethod().IsStatic Then
                ' Get the property names and values.
                astrLabels(iCount) = pi.Name
                astrValues(iCount) = pi.GetValue(type, Nothing).ToString()
                iCount += 1
            End If
        Next pi

        Array.Sort(astrLabels, astrValues)
        bValidInfo = True
    End Sub

    Shared Function MaxLabelWidth(ByVal grfx As Graphics, _
                                  ByVal fnt As Font) As Single
        Return MaxWidth(Labels, grfx, fnt)
    End Function

    Shared Function MaxValueWidth(ByVal grfx As Graphics, _
                                  ByVal fnt As Font) As Single
        Return MaxWidth(Values, grfx, fnt)
    End Function

    Private Shared Function MaxWidth(ByVal astr() As String, _
            ByVal grfx As Graphics, ByVal fnt As Font) As Single
        Dim fMax As Single = 0
        Dim str As String

        GetSysInfo()

        For Each str In astr
            fMax = Math.Max(fMax, grfx.MeasureString(str, fnt).Width)
        Next str

        Return fMax
    End Function
End Class
