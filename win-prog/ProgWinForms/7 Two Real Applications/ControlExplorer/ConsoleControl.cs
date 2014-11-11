//-----------------------------------------------
// ConsoleControl.cs (c) 2005 by Charles Petzold
// From ControlExplorer program
//-----------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ConsoleControl: Control
{
    TextBox txtbox;

    public ConsoleControl()
    {
        txtbox = new TextBox();
        txtbox.Parent = this;
        txtbox.Multiline = true;
        txtbox.WordWrap = false;
        txtbox.ScrollBars = ScrollBars.Both;
        txtbox.ReadOnly = true;
        txtbox.Dock = DockStyle.Fill;
        txtbox.TabStop = false;
        txtbox.HideSelection = false;
    }
    public void Clear()
    {
        txtbox.Clear();
    }
    public void WriteLine()
    {
        Output("\r\n");
    }
    public void Write(object obj)
    {
        Output(obj.ToString());
    }
    public void WriteLine(object obj)
    {
        Output(obj + "\r\n");
    }
    public void Write(string strFormat, params object[] aobj)
    {
        Output(String.Format(strFormat, aobj));
    }
    public void WriteLine(string strFormat, params object[] aobj)
    {
        Output(String.Format(strFormat, aobj) + "\r\n");
    }
    void Output(string str)
    {
        txtbox.SelectionStart = txtbox.TextLength;
        txtbox.AppendText(str);
    }
}
