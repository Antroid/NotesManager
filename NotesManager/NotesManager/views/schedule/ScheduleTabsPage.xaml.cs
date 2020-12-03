using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesManager.interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesManager.views.schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleTabsPage : TabbedPage
    {

        public ScheduleTabsPage()
        {
            InitializeComponent();
            Title = "Schedule";
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            double tabsHeight = DependencyService.Get<ITabBar>().GetHeaderHeight();
            
        }
    }
}