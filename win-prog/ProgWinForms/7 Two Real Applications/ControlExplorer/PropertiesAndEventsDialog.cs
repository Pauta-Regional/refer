//----------------------------------------------------------
// PropertiesAndEventsDialog.cs (c) 2005 by Charles Petzold
// From ControlExplorer program
//----------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class PropertiesAndEventsDialog : Form
{
    PropertyGrid propgrid;
    ReadOnlyPropertyGrid ropropgrid;
    EventLogger evtlst;

    // SelectedObject distributes property to other controls.
    public object SelectedObject
    {
        get
        {
            return propgrid.SelectedObject;
        }
        set
        {
            propgrid.SelectedObject = value;
            ropropgrid.SelectedObject = value;
            evtlst.SelectedObject = value;
        }
    }
    // Constructor.
    public PropertiesAndEventsDialog()
    {
        MaximizeBox = false;
        ShowInTaskbar = false;
        ClientSize = new Size(800, 600);

        SplitContainer sc1 = new SplitContainer();
        sc1.Parent = this;
        sc1.Dock = DockStyle.Fill;
        sc1.SplitterDistance = ClientSize.Height / 2;
        sc1.Orientation = Orientation.Horizontal;

        SplitContainer sc2 = new SplitContainer();
        sc2.Parent = sc1.Panel1;
        sc2.Dock = DockStyle.Fill;
        sc2.SplitterDistance = ClientSize.Width / 2;

        // PropertyGrid control.
        propgrid = new PropertyGrid();
        propgrid.Parent = sc2.Panel1;
        propgrid.Dock = DockStyle.Fill;

        // ReadOnlyPropertyGrid control.
        ropropgrid = new ReadOnlyPropertyGrid();
        ropropgrid.Parent = sc2.Panel2;
        ropropgrid.Dock = DockStyle.Fill;

        // EventLogger controls.
        evtlst = new EventLogger();
        evtlst.Parent = sc1.Panel2;
        evtlst.Dock = DockStyle.Fill;
    }
}
