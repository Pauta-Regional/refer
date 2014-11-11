//--------------------------------------------
// PersonPanel.cs (c) 2005 by Charles Petzold
//--------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class PersonPanel : FlowLayoutPanel
{
    // Constructor.
    public PersonPanel(BindingSource bindsrc)
    {
        Label lbl = new Label();
        lbl.Parent = this;
        lbl.Text = "First Name: ";
        lbl.AutoSize = true;
        lbl.Anchor = AnchorStyles.Left;

        TextBox txtboxFirstName = new TextBox();
        txtboxFirstName.Parent = this;
        txtboxFirstName.AutoSize = true;
        txtboxFirstName.DataBindings.Add("Text", bindsrc, "FirstName");
        txtboxFirstName.DataBindings[0].DataSourceUpdateMode = 
            DataSourceUpdateMode.OnPropertyChanged;

        this.SetFlowBreak(txtboxFirstName, true);

        lbl = new Label();
        lbl.Parent = this;
        lbl.Text = "Last Name: ";
        lbl.AutoSize = true;
        lbl.Anchor = AnchorStyles.Left;

        TextBox txtboxLastName = new TextBox();
        txtboxLastName.Parent = this;
        txtboxLastName.AutoSize = true;
        txtboxLastName.DataBindings.Add("Text", bindsrc, "LastName");
        txtboxLastName.DataBindings[0].DataSourceUpdateMode =
            DataSourceUpdateMode.OnPropertyChanged;

        this.SetFlowBreak(txtboxLastName, true);

        lbl = new Label();
        lbl.Parent = this;
        lbl.Text = "Birth Date: ";
        lbl.AutoSize = true;
        lbl.Anchor = AnchorStyles.Left;

        DateTimePicker dtPicker = new DateTimePicker();
        dtPicker.Parent = this;
        dtPicker.Format = DateTimePickerFormat.Long;
        dtPicker.AutoSize = true;
        dtPicker.DataBindings.Add("Value", bindsrc, "BirthDate");
        dtPicker.DataBindings[0].DataSourceUpdateMode =
            DataSourceUpdateMode.OnPropertyChanged;
    }
}
