namespace ViscaCamLink.Views
{
    using CameraControl.Visca;
    using System;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Timers;
    using System.Windows;
    using System.Windows.Input;
    using ViscaCamLink.Properties;
    using ViscaCamLink.Util;

    /// <summary>
    /// Interaction logic for ViscaCamLinkView.xaml
    /// </summary>
    public partial class ViscaCamLinkView : Window
    {
        public ViscaCamLinkView()
        {            
            InitializeComponent();
            var t = new System.Timers.Timer(100);
            t.Elapsed += T_Elapsed;
            t.Start();
        }

        private void T_Elapsed(object? sender, ElapsedEventArgs e) {
            
        }

        private void Window_LayoutUpdated(Object sender, EventArgs e)
        {
            SizeToContent = SizeToContent.Height; // To only allow horizontal resize
        }

        private void TextBox_PreviewTextInput_Numeric(Object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private void TextBox_PreviewTextInput_NumericDot(Object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumericDot(e.Text);
        }

        private void TextBox_PastingHandler_Numeric(Object sender, DataObjectPastingEventArgs e)
        {
            PastingHandler(e, IsTextNumeric);
        }

        private void TextBox_PastingHandler_NumericDot(Object sender, DataObjectPastingEventArgs e)
        {
            PastingHandler(e, IsTextNumericDot);
        }

        private void PastingHandler(DataObjectPastingEventArgs e, Func<String, Boolean> pastedTextTester)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));

                if (!pastedTextTester(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private Boolean IsTextNumeric(String text)
        {
            Regex numericRegex = new Regex("[0-9]+");

            return numericRegex.IsMatch(text);
        }

        private Boolean IsTextNumericDot(String text)
        {
            Regex numericRegex = new Regex("[0-9.]+");

            return numericRegex.IsMatch(text);
        }
    }
}
