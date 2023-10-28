using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var initialLocation = new Position(37.7749, -122.4194); // San Francisco, CA
            Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(initialLocation, Distance.FromMiles(1.0))); // nie skompiluje, nie ma tokena API
        }
	}
}