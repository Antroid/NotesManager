using System;
using Android.App;
using Android.Content;
using Android.Text.Format;
using Android.Widget;
using NotesManager.droid.custom.renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.TimePicker), typeof(TimeRenderer))]
namespace NotesManager.droid.custom.renderers
{
    public class TimeRenderer : TimePickerRenderer
    {

        public TimeRenderer(Context context) : base(context)
        {
           
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);

            TimePickerDialog timePickerDlg = new TimePickerDialog(Context,
                new EventHandler<TimePickerDialog.TimeSetEventArgs>(UpdateTime),
                Element.Time.Hours, Element.Time.Minutes, Is24Hours());

            var control = new EditText(this.Context);
            control.Focusable = false;
            control.FocusableInTouchMode = false;
            control.Clickable = false;
            control.Click += (sender, ea) => timePickerDlg.Show();
            control.Text = getFormattedTime();

            SetNativeControl(control);
        }

        void UpdateTime(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            Element.Time = new TimeSpan(e.HourOfDay, e.Minute, 0);
            Control.Text = getFormattedTime();
            MessagingCenter.Send<object>(this, "TimeChanged");
        }

        private string getFormattedTime()
        {
            string timeFormatter = Is24Hours() ?"HH:mm":"hh:mm tt";
            DateTime time = DateTime.Today.Add(Element.Time);
            return time.ToString(timeFormatter);
        }
        
        private bool Is24Hours()
        {
            return DateFormat.Is24HourFormat(Context);
        }
    }
}