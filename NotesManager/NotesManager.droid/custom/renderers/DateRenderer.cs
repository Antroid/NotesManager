using Android.App;
using Android.Content;
using NotesManager.droid.custom.renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.DatePicker), typeof(DateRenderer))]
namespace NotesManager.droid.custom.renderers
{
    public class DateRenderer : DatePickerRenderer
    {
        public DateRenderer(Context context) : base(context)
        {
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            //Disposing
            if (e.OldElement != null)
            {
                _element = null;
            }

            //Creating
            if (e.NewElement != null)
            {
                _element = e.NewElement;
            }
        }

        protected Xamarin.Forms.DatePicker _element;

        protected override DatePickerDialog CreateDatePickerDialog(int year, int month, int day)
        {
            var dialog = new DatePickerDialog(Context, (o, e) =>
            {
                _element.Date = e.Date;
                ((IElementController) _element).SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
            }, year, month, day);

            dialog.SetButton((int) DialogButtonType.Positive,
                Context.Resources.GetString(global::Android.Resource.String.Ok), OnOk);
            dialog.SetButton((int) DialogButtonType.Negative,
                Context.Resources.GetString(global::Android.Resource.String.Cancel), OnCancel);

            return dialog;
        }

        private void OnCancel(object sender, DialogClickEventArgs e)
        {
            _element.Unfocus();
            //((FixedDatePicker) _element)?.CallOnCancel();
        }

        private void OnOk(object sender, DialogClickEventArgs e)
        {
            //need to set date from native control manually now
            _element.Date = ((DatePickerDialog) sender).DatePicker.DateTime;
            _element.Unfocus();
            //((FixedDatePicker)_element)?.CallOnOk();
            MessagingCenter.Send<object>(this, "DateChanged");
        }
    }
}