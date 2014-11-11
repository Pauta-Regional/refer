//--------------------------------------------
// NumericScan.cs (c) 2005 by Charles Petzold
//--------------------------------------------
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

[assembly: AssemblyTitle("NumericScan")]
[assembly: AssemblyDescription("NumericScan Control")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("www.charlespetzold.com")]
[assembly: AssemblyProduct("NumericScan")]
[assembly: AssemblyCopyright("(c) Charles Petzold, 2005")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyVersion("1.0.*")]

namespace Petzold.ProgrammingWindowsForms
{
    [DefaultEvent("ValueChanged")]
    public class NumericScan : UserControl
    {
        public event EventHandler ValueChanged;

        TextBox txtbox;
        ArrowButton btn1, btn2;

        // These private fields have corresponding public properties.
        int iDecimalPlaces = 0;
        decimal mValue = 0;
        decimal mIncrement = 1;
        decimal mMinimum = 0;
        decimal mMaximum = 100;

        public NumericScan()
        {
            txtbox = new TextBox();
            txtbox.Parent = this;
            txtbox.TextAlign = HorizontalAlignment.Right;
            txtbox.Text = ValueToText(mValue);
            txtbox.TextChanged += TextBoxOnTextChanged;
            txtbox.KeyDown += TextBoxOnKeyDown;

            btn1 = new ArrowButton();
            btn1.Parent = this;
            btn1.Text = "btn1";
            btn1.ScrollButton = ScrollButton.Left;
            btn1.Click += ButtonOnClick;

            btn2 = new ArrowButton();
            btn2.Parent = this;
            btn2.Text = "btn2";
            btn2.ScrollButton = ScrollButton.Right;
            btn2.Click += ButtonOnClick;

            Width = 4 * Font.Height;
            Height = txtbox.PreferredHeight +
                                SystemInformation.HorizontalScrollBarHeight;
        }
        string ValueToText(decimal mValue)
        {
            return mValue.ToString("F" + DecimalPlaces);
        }

        [Category("Data"), Description("Value displayed in the control")]
        public decimal Value
        {
            set
            {
                txtbox.Text = ValueToText(mValue = value);
            }
            get
            {
                return mValue;
            }
        }
        
        [Category("Data"),
        Description("The amount to increment or decrement on a button click")]
        public decimal Increment
        {
            set { mIncrement = value; }
            get { return mIncrement; }
        }

        [Category("Data"), Description("Minimum allowed value")]
        public decimal Minimum
        {
            set
            {
                if ((mMinimum = value) > Value)
                    Value = mMinimum;
            }
            get { return mMinimum; }
        }

        [Category("Data"), Description("Maximum allowed value")]
        public decimal Maximum
        {
            set
            {
                if ((mMaximum = value) < Value)
                    Value = mMaximum;
            }
            get { return mMaximum; }
        }

        [Category("Data"), Description("Number of decimal places to display")]
        public int DecimalPlaces
        {
            set { iDecimalPlaces = value; }
            get { return iDecimalPlaces; }
        }
        public override Size GetPreferredSize(Size szProposed)
        {
            return new Size(4 * Font.Height, txtbox.PreferredHeight +
                                SystemInformation.HorizontalScrollBarHeight);
        }
        protected override void OnResize(EventArgs args)
        {
            base.OnResize(args);

            txtbox.Height = txtbox.PreferredHeight;
            txtbox.Width = Width;
            btn1.Location = new Point(0, txtbox.Height);
            btn2.Location = new Point(Width / 2, txtbox.Height);
            btn1.Size = btn2.Size = new Size(Width / 2, Height - txtbox.Height);
        }
        void TextBoxOnTextChanged(object objSrc, EventArgs args)
        {
            if (txtbox.Text.Length == 0)
                return;

            try
            {
                mValue = Decimal.Parse(txtbox.Text);
            }
            catch
            {
            }
            txtbox.Text = ValueToText(mValue);
        }
        void TextBoxOnKeyDown(object objSrc, KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.Enter:
                    OnValueChanged(EventArgs.Empty);
                    break;
            }                    
        }
        void ButtonOnClick(object objSrc, EventArgs args)
        {
            ArrowButton btn = objSrc as ArrowButton;
            decimal mNewValue = Value;

            if (btn == btn1)
                if ((mNewValue -= Increment) < Minimum)
                    return;

            if (btn == btn2)
                if ((mNewValue += Increment) > Maximum)
                    return;

            Value = mNewValue;
            OnValueChanged(EventArgs.Empty);
        }
        protected override void OnLeave(EventArgs args)
        {
            base.OnLeave(args);
            OnValueChanged(EventArgs.Empty);
        }
        protected virtual void OnValueChanged(EventArgs args)
        {
            Value = Math.Max(Minimum, Value);
            Value = Math.Min(Maximum, Value);
            Value = Decimal.Round(Value, DecimalPlaces);

            if (ValueChanged != null)
                ValueChanged(this, args);
        }
    }
}

