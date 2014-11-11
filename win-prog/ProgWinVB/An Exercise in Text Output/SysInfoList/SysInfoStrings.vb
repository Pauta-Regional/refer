'-----------------------------------------------
' SysInfoStrings.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class SysInfoStrings
    Shared ReadOnly Property Labels() As String()
        Get
            Return New String() _
            { _
                "ArrangeDirection", _
                "ArrangeStartingPosition", _
                "BootMode", _
                "Border3DSize", _
                "BorderSize", _
                "CaptionButtonSize", _
                "CaptionHeight", _
                "ComputerName", _
                "CursorSize", _
                "DbcsEnabled", _
                "DebugOS", _
                "DoubleClickSize", _
                "DoubleClickTime", _
                "DragFullWindows", _
                "DragSize", _
                "FixedFrameBorderSize", _
                "FrameBorderSize", _
                "HighContrast", _
                "HorizontalScrollBarArrowWidth", _
                "HorizontalScrollBarHeight", _
                "HorizontalScrollBarThumbWidth", _
                "IconSize", _
                "IconSpacingSize", _
                "KanjiWindowHeight", _
                "MaxWindowTrackSize", _
                "MenuButtonSize", _
                "MenuCheckSize", _
                "MenuFont", _
                "MenuHeight", _
                "MidEastEnabled", _
                "MinimizedWindowSize", _
                "MinimizedWindowSpacingSize", _
                "MinimumWindowSize", _
                "MinWindowTrackSize", _
                "MonitorCount", _
                "MonitorsSameDisplayFormat", _
                "MouseButtons", _
                "MouseButtonsSwapped", _
                "MousePresent", _
                "MouseWheelPresent", _
                "MouseWheelScrollLines", _
                "NativeMouseWheelSupport", _
                "Network", _
                "PenWindows", _
                "PrimaryMonitorMaximizedWindowSize", _
                "PrimaryMonitorSize", _
                "RightAlignedMenus", _
                "Secure", _
                "ShowSounds", _
                "SmallIconSize", _
                "ToolWindowCaptionButtonSize", _
                "ToolWindowCaptionHeight", _
                "UserDomainName", _
                "UserInteractive", _
                "UserName", _
                "VerticalScrollBarArrowHeight", _
                "VerticalScrollBarThumbHeight", _
                "VerticalScrollBarWidth", _
                "VirtualScreen", _
                "WorkingArea" _
            }
        End Get
    End Property

    Shared ReadOnly Property Values() As String()
        Get
            Return New String() _
            { _
            SystemInformation.ArrangeDirection.ToString(), _
            SystemInformation.ArrangeStartingPosition.ToString(), _
            SystemInformation.BootMode.ToString(), _
            SystemInformation.Border3DSize.ToString(), _
            SystemInformation.BorderSize.ToString(), _
            SystemInformation.CaptionButtonSize.ToString(), _
            SystemInformation.CaptionHeight.ToString(), _
            SystemInformation.ComputerName, _
            SystemInformation.CursorSize.ToString(), _
            SystemInformation.DbcsEnabled.ToString(), _
            SystemInformation.DebugOS.ToString(), _
            SystemInformation.DoubleClickSize.ToString(), _
            SystemInformation.DoubleClickTime.ToString(), _
            SystemInformation.DragFullWindows.ToString(), _
            SystemInformation.DragSize.ToString(), _
            SystemInformation.FixedFrameBorderSize.ToString(), _
            SystemInformation.FrameBorderSize.ToString(), _
            SystemInformation.HighContrast.ToString(), _
            SystemInformation.HorizontalScrollBarArrowWidth.ToString(), _
            SystemInformation.HorizontalScrollBarHeight.ToString(), _
            SystemInformation.HorizontalScrollBarThumbWidth.ToString(), _
            SystemInformation.IconSize.ToString(), _
            SystemInformation.IconSpacingSize.ToString(), _
            SystemInformation.KanjiWindowHeight.ToString(), _
            SystemInformation.MaxWindowTrackSize.ToString(), _
            SystemInformation.MenuButtonSize.ToString(), _
            SystemInformation.MenuCheckSize.ToString(), _
            SystemInformation.MenuFont.ToString(), _
            SystemInformation.MenuHeight.ToString(), _
            SystemInformation.MidEastEnabled.ToString(), _
            SystemInformation.MinimizedWindowSize.ToString(), _
            SystemInformation.MinimizedWindowSpacingSize.ToString(), _
            SystemInformation.MinimumWindowSize.ToString(), _
            SystemInformation.MinWindowTrackSize.ToString(), _
            SystemInformation.MonitorCount.ToString(), _
            SystemInformation.MonitorsSameDisplayFormat.ToString(), _
            SystemInformation.MouseButtons.ToString(), _
            SystemInformation.MouseButtonsSwapped.ToString(), _
            SystemInformation.MousePresent.ToString(), _
            SystemInformation.MouseWheelPresent.ToString(), _
            SystemInformation.MouseWheelScrollLines.ToString(), _
            SystemInformation.NativeMouseWheelSupport.ToString(), _
            SystemInformation.Network.ToString(), _
            SystemInformation.PenWindows.ToString(), _
            SystemInformation.PrimaryMonitorMaximizedWindowSize.ToString(), _
            SystemInformation.PrimaryMonitorSize.ToString(), _
            SystemInformation.RightAlignedMenus.ToString(), _
            SystemInformation.Secure.ToString(), _
            SystemInformation.ShowSounds.ToString(), _
            SystemInformation.SmallIconSize.ToString(), _
            SystemInformation.ToolWindowCaptionButtonSize.ToString(), _
            SystemInformation.ToolWindowCaptionHeight.ToString(), _
            SystemInformation.UserDomainName, _
            SystemInformation.UserInteractive.ToString(), _
            SystemInformation.UserName, _
            SystemInformation.VerticalScrollBarArrowHeight.ToString(), _
            SystemInformation.VerticalScrollBarThumbHeight.ToString(), _
            SystemInformation.VerticalScrollBarWidth.ToString(), _
            SystemInformation.VirtualScreen.ToString(), _
            SystemInformation.WorkingArea.ToString() _
            }
        End Get
    End Property

    Shared ReadOnly Property Count() As Integer
        Get
            Return Labels.Length
        End Get
    End Property

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
        For Each str In astr
            fMax = Math.Max(fMax, grfx.MeasureString(str, fnt).Width)
        Next str
        Return fMax
    End Function
End Class
