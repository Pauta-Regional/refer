'-----------------------------------------------
' PictureBoxPlus.vb (c) 2002 by Charles Petzold
'-----------------------------------------------
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Class PictureBoxPlus
    Inherits PictureBox

    Private bNoDistort As Boolean = False

    Property NoDistort() As Boolean
        Set(ByVal Value As Boolean)
            bNoDistort = Value
            Invalidate()
        End Set
        Get
            Return bNoDistort
        End Get
    End Property

    Protected Overrides Sub OnPaint(ByVal pea As PaintEventArgs)
        If (Not Image Is Nothing) AndAlso NoDistort AndAlso _
                SizeMode = PictureBoxSizeMode.StretchImage Then
            ScaleImageIsotropically(pea.Graphics, Image, ClientRectangle)
        Else
            MyBase.OnPaint(pea)
        End If
    End Sub

    Private Sub ScaleImageIsotropically(ByVal grfx As Graphics, _
            ByVal img As Image, ByVal rect As Rectangle)
        Dim szf As New SizeF(img.Width / img.HorizontalResolution, _
                             img.Height / img.VerticalResolution)
        Dim fScale As Single = Math.Min(rect.Width / szf.Width, _
                                        rect.Height / szf.Height)
        szf.Width *= fScale
        szf.Height *= fScale
        grfx.DrawImage(img, rect.X + (rect.Width - szf.Width) / 2, _
                            rect.Y + (rect.Height - szf.Height) / 2, _
                            szf.Width, szf.Height)
    End Sub
End Class
