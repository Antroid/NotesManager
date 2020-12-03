using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotesManager.helpers;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace NotesManager.views.settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        bool createNewNoteIsOpen;
        int createNewNoteHeight;
        private Plugin.Geolocator.Abstractions.Position _currentLocation;

        public Plugin.Geolocator.Abstractions.Position CurrentLocation
        {
            get => _currentLocation;
            set => _currentLocation = value;
        }

        public SettingsPage()
        {
            InitializeComponent();
            Title = "Settings";


        }

        private void initSfMaps()
        {
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitLocation();
        }

        private async void InitLocation()
        {
            
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted || status == PermissionStatus.Restricted)
            {
                CheckGPSIsEnabled();
            }
            else
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status == PermissionStatus.Denied)
                {
                  
                }
                if (status == PermissionStatus.Granted || status == PermissionStatus.Restricted)
                {
                    CheckGPSIsEnabled();
                }
            }
            
           
        }

        private void ShowGPSLayout(bool showMap)
        {
            LoadingGPS.IsVisible = !showMap;
            //mapView.IsVisible = showMap;
        }
        
        private async void CheckGPSIsEnabled()
        {
            if (!GPSHelper.IsGPSEnabled())
            {
                ShowGPSLayout(false);
                Device.StartTimer(TimeSpan.FromSeconds(3), () =>
                {
                    if (GPSHelper.IsGPSEnabled())
                    {
                      SetCurrentLocation();
                    }
                    else
                    {
                        OpenGPSDialog();
                    }
                    return false;
                });
            }
            else
            {
                SetCurrentLocation();
            }
            
            
        }

        private async void SetCurrentLocation()
        {
            ShowGPSLayout(true);
            CurrentLocation = await CrossGeolocator.Current.GetLastKnownLocationAsync();
            Pin pin = new Pin
            {
                Label = "Santa Cruz",
                Address = "The city with a boardwalk",
                Type = PinType.Place,
                Position = new Position(CurrentLocation.Latitude, CurrentLocation.Longitude)
            };
            map.Pins.Add(pin);
            
            Position mapPos = new Position(CurrentLocation.Latitude,CurrentLocation.Longitude);
            MapSpan mapSpan = MapSpan.FromCenterAndRadius(mapPos, Distance.FromKilometers(0.444));
            map.MoveToRegion(mapSpan);
            
        }
        
        private async void OpenGPSDialog()
        {
            bool answer = await DisplayAlert ("GPS is Disable", "Please turn it on", "Yes", "No");
            if (answer)
            {
                GPSHelper.OpenGPSSettings();
            }
        }
        
       
    }
}