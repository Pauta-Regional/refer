//-----------------------------------------------------
// DataEntryWithBinding.cs (c) 2005 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

class DataEntryWithBinding : Form
{
    const string strFilter = "Person XML files (*.PersonXml)|" +
                             "*.PersonXml|All files (*.*)|*.*";
    XmlSerializer xmlser = new XmlSerializer(typeof(Person));
    BindingSource bindsrc = new BindingSource();

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new DataEntryWithBinding());
    }
    public DataEntryWithBinding()
    {
        Text = "Simple Data Entry with Binding";

        // Initialize BindingSource object.
        bindsrc.Add(new Person());

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
    }
    void FileNewOnClick(object objSrc, EventArgs args)
    {
        bindsrc[0] = new Person();
    }
    void FileOpenOnClick(object objSrc, EventArgs args)
    {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = strFilter;

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            StreamReader sr = new StreamReader(dlg.FileName);
            bindsrc[0] = xmlser.Deserialize(sr);
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
            xmlser.Serialize(sw, bindsrc[0]);
            sw.Close();
        }
    }
}
