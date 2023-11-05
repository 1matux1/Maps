using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Maps.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class YourLocation : ContentPage
	{
		public YourLocation ()
		{
			InitializeComponent ();

			Location();
        }

		public async void Location()
		{
			var location = await Geolocation.GetLastKnownLocationAsync();

			if (location != null)
			{
				var initialLocation = new Position(location.Latitude, location.Longitude);
				Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(initialLocation, Distance.FromKilometers(1.0))); // nie skompiluje, nie ma tokena API

                var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);

                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    Mapa.Pins.Clear();
                    Mapa.Pins.Add(new Pin
                    {
                        Position = new Position(location.Latitude, location.Longitude),
                        Label = "Twoja Lokalizacja!",
                        Address = placemark.Locality + ", " + placemark.Thoroughfare +" "+ placemark.SubThoroughfare,
                    });
                }
            }
		}
	}
}