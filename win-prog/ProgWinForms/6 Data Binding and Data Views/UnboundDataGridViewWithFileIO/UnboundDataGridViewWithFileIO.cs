//--------------------------------------------------------------
// UnboundDataGridViewWithFileIO.cs (c) 2005 by Charles Petzold
//--------------------------------------------------------------
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

class UnboundDataGridViewWithFileIO : UnboundDataGridView
{
    const string strFilter = "School files (*.SchoolXml)|" +
                             "*.SchoolXml|All files (*.*)|*.*";
    XmlSerializer xmlser = new XmlSerializer(typeof(School));

    [STAThread]
    public new static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new UnboundDataGridViewWithFileIO());
    }
    public UnboundDataGridViewWithFileIO()
    {
        Text = "Unbound DataGridView with File IO";

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
        grid.Rows.Clear();
    }
    void FileOpenOnClick(object objSrc, EventArgs args)
    {
        OpenFileDialog dlg = new OpenFileDialog();
        dlg.Filter = strFilter;

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            // Read the School object in as XML.
            StreamReader sr = new StreamReader(dlg.FileName);
            School sch = (School) xmlser.Deserialize(sr);
            sr.Close();

            // Clear the existing rows from the DataGridView.
            grid.Rows.Clear();

            // Add rows to the DataGridView for each student.
            foreach (Student sdt in sch.Students)
            {
                int index = grid.Rows.Add();

                grid["Courtesy", index].Value = sdt.Courtesy;
                grid["FirstName", index].Value = sdt.FirstName;
                grid["LastName", index].Value = sdt.LastName;
                grid["BirthDate", index].Value = 
                                        sdt.BirthDate.ToShortDateString();
                grid["Enrolled", index].Value = sdt.Enrolled;
            }
        }
    }
    void FileSaveAsOnClick(object objSrc, EventArgs args)
    {
        SaveFileDialog dlg = new SaveFileDialog();
        dlg.Filter = strFilter;

        if (dlg.ShowDialog() == DialogResult.OK)
        {
            // End any editing still in progress.
            grid.EndEdit();

            // Create a new School object.
            School sch = new School();

            // Add Student objects from rows in the DataGridView.
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow)
                    continue;

                Student sdt = new Student();
                sdt.Courtesy = (CourtesyTitle) row.Cells["Courtesy"].Value;
                sdt.FirstName = (string) row.Cells["FirstName"].Value;
                sdt.LastName = (string)row.Cells["LastName"].Value;
                sdt.BirthDate = 
                        DateTime.Parse((string) row.Cells["BirthDate"].Value);
                sdt.Enrolled = (bool)row.Cells["Enrolled"].Value;

                sch.Students.Add(sdt);
            }

            // Write the School object out as XML.
            StreamWriter sw = new StreamWriter(dlg.FileName);
            xmlser.Serialize(sw, sch);
            sw.Close();
        }
    }
}