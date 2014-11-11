//-----------------------------------------------
// ModalDialogBox.cs (c) 2005 by Charles Petzold
//-----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ModalDialogBox: Form
{
    CheckBox cbGrayShades;

    public ModalDialogBox()
    {
        Text = "Change Color";

        FormBorderStyle = FormBorderStyle.FixedDialog;
        ControlBox = false;
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
        cbGrayShades.Location = new Point(16, 8);
        cbGrayShades.Size = new Size(80, 12);

        Button btn = new Button();
        btn.Parent = this;
        btn.Text = "OK";
        btn.Location = new Point(16, 32);
        btn.Size = new Size(48, 14);
        btn.DialogResult = DialogResult.OK;
        AcceptButton = btn;

        btn = new Button();
        btn.Parent = this;
        btn.Text = "Cancel";
        btn.Location = new Point(80, 32);
        btn.Size = new Size(48, 14);
        btn.DialogResult = DialogResult.Cancel;
        CancelButton = btn;

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
}
