//--------------------------------------------------------
// DataEntryWithNavigation.cs (c) 2005 by Charles Petzold
//--------------------------------------------------------
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

class DataEntryWithNavigation : Form
{
    const string strFilter = "Person File files (*.PersonFileXml)|" +
                             "*.PersonFileXml|All files (*.*)|*.*";
    XmlSerializer xmlser = new XmlSerializer(typeof(PersonFile));
    BindingSource bindsrc = new BindingSource();

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new DataEntryWithNavigation());
    }
    public DataEntryWithNavigation()
    {
        Text = "Simple Data Entry with Navigation";

        // Initialize the BindingSource.
        FileNewOnClick(null, EventArgs.Empty);

        // Create the panel.
        PersonPanel personpnl = new PersonPanel(bindsrc);
        personpnl.Parent = this;
        personpnl.Dock = DockStyle.Fill;

        // Create the menu.
        MenuStrip menu = new MenuStrip();
        menu.Parent = this;
        ToolStripMenuItem item = (ToolStripMenuItem) menu.Items.Add("&File");
        item.DropDownItems.Add("&New", null, FileNewOnClick);
        item.DropDownItems.Add("&Open...", null, FileOpenOnClick);
        item.DropDownItems.Add("Save &As...", null, FileSaveAsOnClick);

        // Create the BindingNavigator.
        BindingNavigator bindnav = new BindingNavigator(true);
        bindnav.Parent = this;
        bindnav.Dock = DockStyle.Bottom;
        bindnav.BindingSource = bindsrc;
    }
    void FileNewOnClick(object objSrc, EventArgs args)
    {
        PersonFile persfile = new PersonFile();
        persfile.Persons.Add(new Person());

        bindsrc.DataSource = persfile;
        bindsrc.DataMember = "Persons";
    }
    void FileOpenOnClick(object objSrc, EventArgs args)
    {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = strFilter;

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            StreamReader sr = new StreamReader(dlg.FileName);
            bindsrc.DataSource = xmlser.Deserialize(sr);
            sr.Close();
        }
    }
    void FileSaveAsOnClick(object objSrc, EventArgs args)
    {
        SaveFileDialog dlg = new SaveFileDialog();
        dlg.Filter = strFilter;

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            StreamWriter sw = new StreamWriter(dlg.FileName);
            xmlser.Serialize(sw, bindsrc.DataSource);
            sw.Close();
        }
    }
}
