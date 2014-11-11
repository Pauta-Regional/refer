//-----------------------------------------------------
// PersonPanelNoBinding.cs (c) 2005 by Charles Petzold
//-----------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class PersonPanelNoBinding : FlowLayoutPanel
{
    TextBox txtboxFirstName, txtboxLastName;
    DateTimePicker dtPicker;

    // Public property.
    public Person Person
    {
        set
        {
            txtboxFirstName.Text = value.FirstName;
            txtboxLastName.Text = value.LastName;
            dtPicker.Value = value.BirthDate;
        }
        get
        {
            Person pers = new Person();
            pers.FirstName = txtboxFirstName.Text;
            pers.LastName = txtboxLastName.Text;
            pers.BirthDate = dtPicker.Value;
            return pers;
        }
    }

    // Constructor.
    public PersonPanelNoBinding()
    {
        Label lbl = new Label();
        lbl.Parent = this;
        lbl.Text = "First Name: ";
        lbl.AutoSize = true;
        lbl.Anchor = AnchorStyles.Left;

        txtboxFirstName = new TextBox();
        txtboxFirstName.Parent = this;
        txtboxFirstName.AutoSize = true;

        this.SetFlowBreak(txtboxFirstName, true);

        lbl = new Label();
        lbl.Parent = this;
        lbl.Text = "Last Name: ";
        lbl.AutoSize = true;
        lbl.Anchor = AnchorStyles.Left;

        txtboxLastName = new TextBox();
        txtboxLastName.Parent = this;
        txtboxLastName.AutoSize = true;

        this.SetFlowBreak(txtboxLastName, true);

        lbl = new Label();
        lbl.Parent = this;
        lbl.Text = "Birth Date: ";
        lbl.AutoSize = true;
        lbl.Anchor = AnchorStyles.Left;

        dtPicker = new DateTimePicker();
        dtPicker.Parent = this;
        dtPicker.Format = DateTimePickerFormat.Long;
        dtPicker.AutoSize = true;
    }
}
