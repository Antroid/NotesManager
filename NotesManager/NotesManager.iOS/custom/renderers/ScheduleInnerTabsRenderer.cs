using System;
using CoreGraphics;
using NotesManager.interfaces;
using NotesManager.iOS.custom.renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(ScheduleInnerTabsRenderer))]
[assembly: ExportRenderer(typeof(TabbedPage), typeof(ScheduleInnerTabsRenderer))]
namespace NotesManager.iOS.custom.renderers
{
    [Foundation.Preserve(AllMembers = true)]
    
    public class ScheduleInnerTabsRenderer : TabbedRenderer, ITabBar
    {
        public static nfloat _tabBarHeight = 0;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            TabBar.BackgroundColor = ((Color)App.GetResourceValue("TabsColor")).ToUIColor();
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            if (_tabBarHeight==0)
            {
                _tabBarHeight = TabBar.Frame.Height;
            }


            

            TabBar.Frame = new CGRect(TabBar.Frame.X, 0, TabBar.Frame.Width, (int)(_tabBarHeight*0.75f));
  

        }


        public override void ViewWillAppear(bool animated)
        {
            if (TabBar?.Items == null)
                return;
        
            // Go through our elements and change them
            var tabs = Element as TabbedPage;
            if (tabs != null)
            {
                for (int i = 0; i < TabBar.Items.Length; i++)
                    UpdateTabBarItem(TabBar.Items[i]);
            }

            base.ViewWillAppear(animated);
        }
        
        private void UpdateTabBarItem(UITabBarItem item)
        {
            if (item == null)
                return;
        
            var TabsFont = new UITextAttributes()
            {
                Font = UIFont.SystemFontOfSize(15)
            };
            
            // Set the font for the title.
            item.SetTitleTextAttributes(TabsFont, UIControlState.Normal);
            item.SetTitleTextAttributes(TabsFont, UIControlState.Selected);
        
            // Moves the titles up just a bit.
            item.TitlePositionAdjustment = new UIOffset(0, -((int)UIApplication.SharedApplication.StatusBarFrame.Height/2));
           
        }

        public double GetHeaderHeight()
        {
            return _tabBarHeight*0.75;
        }
    }
    
}