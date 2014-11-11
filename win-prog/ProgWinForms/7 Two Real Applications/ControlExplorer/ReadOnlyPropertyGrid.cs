//-----------------------------------------------------
// ReadOnlyPropertyGrid.cs (c) 2005 by Charles Petzold
// From ControlExplorer program
//-----------------------------------------------------
using System;
using System.ComponentModel; // for DescriptionAttribute
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

class ReadOnlyPropertyGrid: Control
{
    object objSelected;
    ListView lvProps;
    Label labProp, labDesc;
    Timer tmr;
        
    // Public property.
    public object SelectedObject
    {
        get 
        { 
            return objSelected; 
        }
        set
        {
            objSelected = value;
            FullRefresh();
            tmr.Enabled = objSelected != null;
        }
    }
    // Constructor.
    public ReadOnlyPropertyGrid()
    {
        ClientSize = new Size(30 * Font.Height, 30* Font.Height);

        SplitContainer sc = new SplitContainer();
        sc.Parent = this;
        sc.Dock = DockStyle.Fill;
        sc.Orientation = Orientation.Horizontal;
        sc.FixedPanel = FixedPanel.Panel2;
        sc.SplitterDistance = Height - 4 * Font.Height;

        // ListView displays properties and values.
        lvProps = new ListView();
        lvProps.Parent = sc.Panel1;
        lvProps.Dock = DockStyle.Fill;
        lvProps.View = View.Details;
        lvProps.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        lvProps.GridLines = true;
        lvProps.FullRowSelect = true;
        lvProps.MultiSelect = false;
        lvProps.HideSelection = false;
        lvProps.Activation = ItemActivation.OneClick;
        lvProps.SelectedIndexChanged += ListViewOnSelectedIndexChanged;

        lvProps.Columns.Add("Property", 12 * Font.Height, 
            HorizontalAlignment.Left);
        lvProps.Columns.Add("Value", 18 * Font.Height, 
            HorizontalAlignment.Left);

        // Labels show selected property and value.
        labDesc = new Label();
        labDesc.Parent = sc.Panel2;
        labDesc.Dock = DockStyle.Fill;

        labProp = new Label();
        labProp.Parent = sc.Panel2;
        labProp.Dock = DockStyle.Top;
        labProp.Height = Font.Height;
        labProp.Font = new Font(labProp.Font, FontStyle.Bold);

        tmr = new Timer();
        tmr.Interval = 100;
        tmr.Tick += TimerOnTick;
    }
    // Fill the ListView with properties and values.
    void FullRefresh()
    {
        lvProps.Items.Clear();

        if (SelectedObject == null)
            return;

        PropertyInfo[] api = SelectedObject.GetType().GetProperties();

        foreach (System.Reflection.PropertyInfo pi in api)
        {
            if (pi.CanRead && !pi.CanWrite)
            {
                ListViewItem lvi = new ListViewItem(pi.Name);
                lvi.Tag = pi;
                object objValue = pi.GetValue(SelectedObject, null);
                lvi.SubItems.Add(objValue == null ? "" : objValue.ToString());
                lvProps.Items.Add(lvi);
                lvi.Selected = pi.Name == "Bottom";
            }
        }
    }
    // Refresh the value of each property that has changed.
    void ValueRefresh()
    {
        foreach (ListViewItem lvi in lvProps.Items)
        {
            PropertyInfo pi = (PropertyInfo) lvi.Tag;
            object objValue = pi.GetValue(SelectedObject, null);
            string strNew = objValue == null ? "" : objValue.ToString();

            if (strNew != lvi.SubItems[1].Text)
                lvi.SubItems[1].Text = strNew;
        }
    }
    // Change the labels based on the ListView selection.
    void ListViewOnSelectedIndexChanged(object objSrc, EventArgs args)
    {
        ListView lv = (ListView)objSrc;

        if (lv.SelectedItems.Count == 0)
        {
            labProp.Text = labDesc.Text = "";
            return;
        }
        ListViewItem lvi = lv.SelectedItems[0];
        PropertyInfo pi = (PropertyInfo)lvi.Tag;
        DescriptionAttribute dattr = (DescriptionAttribute) 
            Attribute.GetCustomAttribute(pi, typeof(DescriptionAttribute));

        labProp.Text = pi.Name;
        labDesc.Text = dattr == null ? "" : dattr.Description;
    }
    // On timer tick, refresh all property values.
    void TimerOnTick(object objSrc, EventArgs args)
    {
        ValueRefresh();
    }
}