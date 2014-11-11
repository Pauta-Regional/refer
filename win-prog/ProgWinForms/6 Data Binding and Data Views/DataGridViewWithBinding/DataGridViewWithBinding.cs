//--------------------------------------------------------
// DataGridViewWithBinding.cs (c) 2005 by Charles Petzold
//--------------------------------------------------------
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

class DataGridViewWithBinding : Form
{
    const string strFilter = "School files (*.SchoolXml)|" +
                         "*.SchoolXml|All files (*.*)|*.*";
    XmlSerializer xmlser = new XmlSerializer(typeof(School));
    BindingSource bindsrc = new BindingSource();

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new DataGridViewWithBinding());
    }
    public DataGridViewWithBinding()
    {
        Text = "DataGridView with Binding";
        Width *= 2;

        // Initialize the BindingSource.
        FileNewOnClick(null, EventArgs.Empty);

        // Create DataGridView control.
        DataGridView grid = new DataGridView();
        grid.Parent = this;
        grid.AutoSize = true;
        grid.Dock = DockStyle.Fill;

        // Bind the control to the data source.
        grid.AutoGenerateColumns = false;
        grid.DataSource = bindsrc;
        
        // Create desired columns for DataGridView.
        DataGridViewComboBoxColumn colCombo = new DataGridViewComboBoxColumn();
        colCombo.DataPropertyName = "Courtesy";
        colCombo.DataSource = Enum.GetValues(typeof(CourtesyTitle));
        colCombo.HeaderText = "Courtesy";
        grid.Columns.Add(colCombo);

        DataGridViewTextBoxColumn colText = new DataGridViewTextBoxColumn();
        colText.DataPropertyName = "FirstName";
        colText.HeaderText = "First Name";
        grid.Columns.Add(colText);

        colText = new DataGridViewTextBoxColumn();
        colText.DataPropertyName = "LastName";
        colText.HeaderText = "Last Name";
        grid.Columns.Add(colText);

        CalendarColumn colBirth = new CalendarColumn();
        colBirth.DataPropertyName = "BirthDate";
        colBirth.HeaderText = "Birth Date";
        grid.Columns.Add(colBirth);

        DataGridViewCheckBoxColumn colCheck = new DataGridViewCheckBoxColumn();
        colCheck.DataPropertyName = "Enrolled";
        colCheck.HeaderText = "Enrolled?";
        grid.Columns.Add(colCheck);

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
        bindsrc.DataSource = new School();
        bindsrc.DataMember = "Students";
    }
    void FileOpenOnClick(object objSrc, EventArgs args)
    {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = strFilter;

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            // Read the School object in as XML.
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
            // Write the School object out as XML.
            StreamWriter sw = new StreamWriter(dlg.FileName);
            xmlser.Serialize(sw, bindsrc.DataSource);
            sw.Close();
        }
    }
}
