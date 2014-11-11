//---------------------------------------------
// SilentMsgBox.cs (c) 2005 by Charles Petzold
//---------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class SilentMsgBox
{
    public static DialogResult Show(string strMessage, string strCaption)
    {
        Form frm = new Form();
        frm.StartPosition = FormStartPosition.CenterScreen;
        frm.FormBorderStyle = FormBorderStyle.FixedDialog;
        frm.MinimizeBox = frm.MaximizeBox = frm.ShowInTaskbar = false;
        frm.AutoSize = true;
        frm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        frm.Text = strCaption;

        FlowLayoutPanel pnl = new FlowLayoutPanel();
        pnl.Parent = frm;
        pnl.AutoSize = true;
        pnl.FlowDirection = FlowDirection.TopDown;
        pnl.WrapContents = false;
        pnl.Padding = new Padding(pnl.Font.Height);

        Label lbl = new Label();
        lbl.Parent = pnl;
        lbl.AutoSize = true;
        lbl.Anchor = AnchorStyles.None;
        lbl.Margin = new Padding(lbl.Font.Height);
        lbl.Text = strMessage;

        Button btn = new Button();
        btn.Parent = pnl;
        btn.AutoSize = true;
        btn.Anchor = AnchorStyles.None;
        btn.Margin = new Padding(btn.Font.Height);
        btn.Text = "OK";
        btn.DialogResult = DialogResult.OK;

        return frm.ShowDialog();
    }
}
