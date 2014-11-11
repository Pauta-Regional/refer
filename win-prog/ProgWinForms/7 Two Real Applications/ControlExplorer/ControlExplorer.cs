//------------------------------------------------
// ControlExplorer.cs (c) 2005 by Charles Petzold
//------------------------------------------------
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

[assembly: AssemblyTitle("Control Explorer")]
[assembly: AssemblyDescription("Developer Tool for Windows Forms")]
[assembly: AssemblyCompany("www.charlespetzold.com")]
[assembly: AssemblyProduct("Control Explorer")]
[assembly: AssemblyCopyright("(c) 2005 by Charles Petzold")]
[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyFileVersion("1.0.0.0")]

class ControlExplorer: Form
{
    const string strResource = "ControlExplorer";
    Panel pnl;

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ControlExplorer());
    }

    public ControlExplorer()
    {
        Text = "Control Explorer";
        Icon = new Icon(GetType(), strResource + ".ControlExplorer.ico");

        pnl = new Panel();
        pnl.Parent = this;
        pnl.Dock = DockStyle.Fill;

        MenuStrip menu = new MenuStrip();
        menu.Parent = this;
        menu.Items.Add(new ControlMenuItem(MenuItemOnClick));

        ToolStripMenuItem itemHelp = new ToolStripMenuItem("&Help");
        menu.Items.Add(itemHelp);
        ToolStripMenuItem itemAbout = new ToolStripMenuItem();
        itemAbout.Text = "&About Control Explorer...";
        itemAbout.Click += AboutOnClick;
        itemHelp.DropDownItems.Add(itemAbout);
    }

    void MenuItemOnClick(object objSrc, EventArgs args)
    {
        // Obtain the menu Item and the class it refers to.
        ToolStripMenuItem item = objSrc as ToolStripMenuItem;
        Type typ = (Type)item.Tag;

        // Get ready to create an object of that type.
        ConstructorInfo ci = typ.GetConstructor(System.Type.EmptyTypes);
        Control ctrl;

        // Try creating an object of that type.
        try
        {
            ctrl = (Control)ci.Invoke(null);
        }
        catch (Exception exc)
        {
            MessageBox.Show(exc.Message, Text);
            return;
        }

        // Create a dialog box that contains a PropertyGrid control.
        PropertiesAndEventsDialog dlg = new PropertiesAndEventsDialog();
        dlg.Owner = this;
        dlg.Text = item.Text + " Property Grid";
        dlg.SelectedObject = ctrl;
        dlg.Closed += new EventHandler(DialogOnClosed);
        dlg.Show();

        // If the Parent property can't be assigned, it's probably
        //  a Form, so just call Show for it.
        try
        {
            ctrl.Parent = pnl;
        }
        catch
        {
            ctrl.Show();
        }
    }

    // When the Properties dialog box is closed,
    //  get rid of the control.
    void DialogOnClosed(object objSrc, EventArgs args)
    {
        PropertiesAndEventsDialog dlg = (PropertiesAndEventsDialog)objSrc;
        Control ctrl = (Control)dlg.SelectedObject;
        ctrl.Dispose();
    }
    void AboutOnClick(object objSrc, EventArgs args)
    {
        new AboutDialog(strResource).ShowDialog(); 
    }
}
