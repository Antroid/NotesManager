using System;
using Foundation;
using NotesManager.iOS.custom.renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TimePicker), typeof(TimeRenderer))]
namespace NotesManager.iOS.custom.renderers
{
    public class TimeRenderer : TimePickerRenderer
    {
       
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);
            
            setTimeFormat();
            setTime();
           
        }

        private void setTime()
        {
            int hours = Element.Time.Hours;
            int minutes = Element.Time.Minutes;
            string format = Is12Hours()?"hh:mm tt":"HH:mm";
            DateTime currTime = DateTime.Now;
            currTime = new DateTime(currTime.Year,currTime.Month,currTime.Day,hours,minutes,0);
            Control.Text = currTime.ToString(format);
        }
        
        private void setTimeFormat()
        {
            if (Is12Hours())
            {
                Element.Format = "hh:mm tt";
            }
            else
            {
                var timePicker = (UIDatePicker)Control.InputView;  
                timePicker.Locale = new NSLocale("no_nb");

                Element.Format = "HH:mm";
            }
        }

        private bool Is12Hours()
        {
            var dateFormatter = new NSDateFormatter();
            dateFormatter.DateStyle = NSDateFormatterStyle.None;
            dateFormatter.TimeStyle = NSDateFormatterStyle.Short;

            var dateString = dateFormatter.ToString(NSDate.Now);
            var isTwelveHourFormat = 
                dateString.Contains(dateFormatter.AMSymbol) || 
                dateString.Contains(dateFormatter.PMSymbol);
            return isTwelveHourFormat;
        }
    }
}