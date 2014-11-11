//--------------------------------------------------
// ModelessDialogBox.cs (c) 2005 by Charles Petzold
//--------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ModelessDialogBox: Form
{
    CheckBox cbGrayShades;

    public event EventHandler Change;

    public ModelessDialogBox()
    {
        Text = "Change Color";

        FormBorderStyle = FormBorderStyle.FixedDialog;
        MinimizeBox = false;
        MaximizeBox = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.Manual;
        Location = ActiveForm.Location +
                   SystemInformation.CaptionButtonSize +
                   SystemInformation.FrameBorderSize;

        ClientSize = new Size(144, 56);

        cbGrayShades = new CheckBox();
        cbGrayShades.Parent = this;
        cbGrayShades.Text = "Gray Shades Only";
        cbGrayShades.Location = new Point(32, 8);
        cbGrayShades.Size = new Size(80, 12);

        Button btn = new Button();
        btn.Parent = this;
        btn.Text = "Change";
        btn.Location = new Point(48, 32);
        btn.Size = new Size(48, 14);
        btn.Click += ButtonOnClick;
        AcceptButton = btn;

        AutoScaleDimensions = new Size(4, 8);
        AutoScaleMode = AutoScaleMode.Font;
    }
    public bool GrayShades
    {
        set
        {
            cbGrayShades.Checked = value;
        }
        get
        {
            return cbGrayShades.Checked;
        }
    }
    void ButtonOnClick(object objSrc, EventArgs args)
    {
        if (Change != null)
            Change(this, new EventArgs());
    }
}