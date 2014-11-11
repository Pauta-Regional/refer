//---------------------------------------------
// ComboBoxBind.cs (c) 2005 by Charles Petzold
//---------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ComboBoxBind : Form
{
    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new ComboBoxBind());
    }
    public ComboBoxBind()
    {
        Text = "ComboBox Bind";

        // Create the ComboBox.
        ComboBox combo = new ComboBox();
        combo.Parent = this;
        combo.DropDownStyle = ComboBoxStyle.DropDownList;
        combo.AutoSize = true;
        combo.Width = 12 * Font.Height;

        // Set the data source, display and value members.
        combo.DataSource = KnownColorClass.KnownColorArray;
        combo.ValueMember = "Color";
        combo.DisplayMember = "Name";

        // Bind the ComboBox with the form background color.
        combo.DataBindings.Add("SelectedValue", this, "BackColor");
        combo.DataBindings[0].DataSourceUpdateMode = 
                                DataSourceUpdateMode.OnPropertyChanged;
    }
}

