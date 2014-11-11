//---------------------------------------------------
// DataEntryNoBinding.cs (c) 2005 by Charles Petzold
//---------------------------------------------------
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

class DataEntryNoBinding : Form
{
    const string strFilter = "Person XML files (*.PersonXml)|" + 
                             "*.PersonXml|All files (*.*)|*.*";
    PersonPanelNoBinding personpnl;
    XmlSerializer xmlser = new XmlSerializer(typeof(Person));

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new DataEntryNoBinding());
    }
    public DataEntryNoBinding()
    {
        Text = "Simple Data Entry (No Binding)";

        // Create the panel.
        personpnl = new PersonPanelNoBinding();
        personpnl.Parent = this;
        personpnl.Dock = DockStyle.Fill;

        // Create the menu.
        MenuStrip menu = new MenuStrip();
        menu.Parent = this;
        ToolStripMenuItem item = (ToolStripMenuItem)menu.Items.Add("&File");
        item.DropDownItems.Add("&New", null, FileNewOnClick);
        item.DropDownItems.Add("&Open...", null, FileOpenOnClick);
        item.DropDownItems.Add("Save &As...", null, FileSaveAsOnClick);
    }
    void FileNewOnClick(object objSrc, EventArgs args)
    {
        personpnl.Person = new Person();
    }
    void FileOpenOnClick(object objSrc, EventArgs args)
    {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = strFilter;

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            StreamReader sr = new StreamReader(dlg.FileName);
            personpnl.Person = xmlser.Deserialize(sr) as Person;
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
            xmlser.Serialize(sw, personpnl.Person);
            sw.Close();
        }
    }
}
