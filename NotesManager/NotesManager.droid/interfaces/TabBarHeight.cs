using NotesManager.droid.interfaces;
using NotesManager.interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(TabBarHeight))]
namespace NotesManager.droid.interfaces
{
    public class TabBarHeight : ITabBar
    {
        public double GetHeaderHeight()
        {
            return 0;
        }
    }
}