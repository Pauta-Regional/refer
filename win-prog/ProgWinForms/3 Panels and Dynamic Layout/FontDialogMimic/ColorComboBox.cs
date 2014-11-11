//----------------------------------------------
// ColorComboBox.cs (c) 2005 by Charles Petzold
//----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ColorComboBox : ComboBox
{
    public ColorComboBox()
    {
        DataSource = new string[] { "Black", "Maroon", "Green", "Olive", 
                                    "Navy", "Purple", "Teal", "Gray", 
                                    "Silver", "Red", "Lime", "Yellow", 
                                    "Blue", "Fuchsia", "Aqua", "White" };

        DropDownStyle = ComboBoxStyle.DropDownList;
        DrawMode = DrawMode.OwnerDrawFixed;
        ItemHeight = Font.Height;
    }
    public Color Color
    {
        get { return Color.FromName((string) SelectedItem); }
        set { SelectedText = value.Name; }
    }
    protected override void OnDrawItem(DrawItemEventArgs args)
    {
        Graphics grfx = args.Graphics;

        Rectangle rectColor = 
            new Rectangle(args.Bounds.Left, args.Bounds.Top,
                          2 * args.Bounds.Height, args.Bounds.Height);

        rectColor.Inflate(-1, -1);

        Rectangle rectText =
            new Rectangle(args.Bounds.Left + 2 * args.Bounds.Height,
                          args.Bounds.Top, 
                          args.Bounds.Width - 2 * args.Bounds.Height, 
                          args.Bounds.Height);

        args.DrawBackground();
        grfx.DrawRectangle(Pens.Black, rectColor);
        grfx.FillRectangle(
            new SolidBrush(Color.FromName(Items[args.Index].ToString())), 
            rectColor);
        grfx.DrawString(Items[args.Index].ToString(), Font, 
            new SolidBrush(args.ForeColor), rectText);
    }
}

