//---------------------------------------------------
// ToolStripColorGrid.cs (c) 2005 by Charles Petzold
//---------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ToolStripColorGrid : ToolStripControlHost
{
    public ToolStripColorGrid() : base(new ColorGrid())
    {
    }
    public Color SelectedColor
    {
        get
        {
            return ((ColorGrid)Control).SelectedColor;
        }
        set
        {
            ((ColorGrid)Control).SelectedColor = value;
        }
    }
    protected override void OnClick(EventArgs args)
    {
        base.OnClick(args);
        ((ToolStripDropDown)Owner).Close(ToolStripDropDownCloseReason.ItemClicked);
    }
}
