//--------------------------------------------
// EventLogger.cs (c) 2005 by Charles Petzold
// From ControlExplorer program
//--------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows.Forms;

class EventLogger: SplitContainer
{
    object objSelected;
    CheckBox chkbox;
    CheckedListBox lstbox;
    ConsoleControl cons;
    Dictionary<string, Delegate> deledict = new Dictionary<string, Delegate>();
    static Dictionary<object, ConsoleControl> consdict = 
        new Dictionary<object, ConsoleControl>();

    // Public property for SelectedObject.
    public object SelectedObject
    {
        get
        {
            return objSelected;
        }
        set
        {
            if (objSelected != null)
                consdict.Remove(objSelected);

            objSelected = value;

            if (objSelected != null)
                consdict.Add(objSelected, cons);

            FullRefresh();
        }
    }
    // Constructor.
    public EventLogger()
    {
        // CheckedListBox displays all events supported by control.
        lstbox = new CheckedListBox();
        lstbox.Parent = Panel1;
        lstbox.Dock = DockStyle.Fill;
        lstbox.CheckOnClick = true;
        lstbox.Sorted = true;
        lstbox.ItemCheck += ListBoxOnItemCheck;

        // CheckBox for checking or unchecking all events.
        chkbox = new CheckBox();
        chkbox.Parent = Panel1;
        chkbox.AutoSize = true;
        chkbox.AutoCheck = false;
        chkbox.ThreeState = true;
        chkbox.Text = "All";
        chkbox.Dock = DockStyle.Top;
        chkbox.Click += CheckBoxOnClick;

        // ConsoleControl for logging events.
        cons = new ConsoleControl();
        cons.Parent = Panel2;
        cons.Dock = DockStyle.Fill;
    }
    // ListBoxOnItemCheck: Attach and detach event handlers
    void ListBoxOnItemCheck(object objSrc, ItemCheckEventArgs args)
    {
        if (args.CurrentValue == args.NewValue)
            return;

        CheckedListBox lstbox = (CheckedListBox) objSrc;
        string strEvent = lstbox.Items[args.Index].ToString();

        // Attach a handler if the item is being checked.
        if (args.NewValue == CheckState.Checked)
        {
            AttachHandler(strEvent);

            if (lstbox.CheckedIndices.Count + 1 == lstbox.Items.Count)
                chkbox.CheckState = CheckState.Checked;
            else
                chkbox.CheckState = CheckState.Indeterminate;
        }
        // Detach a handler otherwise.
        else
        {
            RemoveHandler(strEvent);

            if (lstbox.CheckedIndices.Count == 1)
                chkbox.CheckState = CheckState.Unchecked;
            else
                chkbox.CheckState = CheckState.Indeterminate;
        }
    }
    // When "All" CheckBox checked or unchecked, apply change to list box.
    void CheckBoxOnClick(object objSrc, EventArgs args)
    {
        CheckBox chkbox = (CheckBox)objSrc;

        // Let ListBoxOnItemChecked change the state of the check box check.
        if (chkbox.CheckState == CheckState.Unchecked)
        {
            for (int i = 0; i < lstbox.Items.Count; i++)
                lstbox.SetItemChecked(i, true);
        }
        else
        {
            for (int i = 0; i < lstbox.Items.Count; i++)
                lstbox.SetItemChecked(i, false);
        }
    }
    // FullRefresh called when SelectedProperty changes.
    void FullRefresh()
    {
        cons.Clear();
        lstbox.Items.Clear();

        if (SelectedObject == null)
            return;

        // Fill an array with all the events.
        EventInfo[] aevtinfo = SelectedObject.GetType().GetEvents();

        lstbox.BeginUpdate();

        // Loop through the events.
        foreach (EventInfo evtinfo in aevtinfo)
        {
            bool bChecked = false;

            // Check the event if the SelectedObject is a Control.
            if (SelectedObject.GetType() == typeof(Control))
                bChecked = true;

            // Otherwise, check if the event is not implemented by Control.
            else if (SelectedObject is Control)
                bChecked = typeof(Control).GetEvent(evtinfo.Name) == null;

            lstbox.Items.Add(evtinfo.Name, bChecked);
        }
        lstbox.EndUpdate();
    }
    // AttachHandler
    void AttachHandler(string strEvent)
    {
        // Information about the particular event.
        EventInfo evtinfo = SelectedObject.GetType().GetEvent(strEvent);

        // The type of the event handler.
        Type htype = evtinfo.EventHandlerType;

        // Information about the event handler method.
        MethodInfo[] methinfo = htype.GetMethods();

        // Arguments to the event handler.
        ParameterInfo[] parminfo = methinfo[0].GetParameters();

        // Create a method with a name like "ClickEventHandler"
        //  with the proper return type and arguments.
        DynamicMethod dynameth = 
            new DynamicMethod(strEvent + "EventHandler", typeof(void),
                              new Type[] { typeof(object), 
                                parminfo[1].ParameterType }, 
                              GetType());

        // The ILGenerator allows generating code for this method.
        ILGenerator ilg = dynameth.GetILGenerator();

        // The event handler will call the EventDump static method below.
        MethodInfo miEventDump = GetType().GetMethod("EventDump");

        // Generate code to push event name and two arguments on stack,
        //  and then call the EventDump method.
        ilg.Emit(OpCodes.Ldstr, strEvent);
        ilg.Emit(OpCodes.Ldarg_0);
        ilg.Emit(OpCodes.Ldarg_1);
        ilg.EmitCall(OpCodes.Call, miEventDump, null);
        ilg.Emit(OpCodes.Ret);

        // Create a delegate based on the event handler type.
        Delegate dynadele = dynameth.CreateDelegate(htype);

        // Finally, install the event handler.
        evtinfo.AddEventHandler(SelectedObject, dynadele);

        // Add the delegate to a dictionary for later detachment.
        deledict.Add(strEvent, dynadele);
    }
    // Remove event handler.
    void RemoveHandler(string strEvent)
    {
        EventInfo evtinfo = SelectedObject.GetType().GetEvent(strEvent);
        evtinfo.RemoveEventHandler(SelectedObject, deledict[strEvent]);
        deledict.Remove(strEvent);
    }
    // This static method displays the event. 
    // Because the method must be static, it has to obtain the correct 
    //  ConsoleControl object from the ConsoleControl dictionary (consdict).
    // Although the last argument is defined as EventArgs, for many events it
    //  will actually be a descendent of EventArgs.
    public static void EventDump(string strEvent, object objSrc, EventArgs args)
    {
        // Fish the correct ConsoleControl from the static dictionary.
        ConsoleControl cons = consdict[objSrc];

        // Display the event with the seconds and milliseconds.
        DateTime dt = DateTime.Now;
        cons.Write("{0}.{1:D3} {2}", dt.Second % 10, dt.Millisecond, strEvent);

        // Display all the properties in the EventArgs (or descendent).
        PropertyInfo[] api = args.GetType().GetProperties();

        foreach (PropertyInfo pi in api)
            cons.Write(" {0}={1}", pi.Name, pi.GetValue(args, null));

        // If the event ends with "Changed", display the new property.
        if (strEvent.EndsWith("Changed"))
        {
            string strProperty = strEvent.Substring(0, strEvent.Length - 7);
            PropertyInfo pi = objSrc.GetType().GetProperty(strProperty);
            cons.Write(" [{0}={1}]", strProperty, pi.GetValue(objSrc, null));
        }
        // End the line.
        cons.WriteLine();
    }
}