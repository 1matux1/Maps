using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NoPermissions : ContentPage
	{
		public NoPermissions ()
		{
			InitializeComponent ();
		}

        private async void GrantPermissions(object sender, EventArgs e) // Nie jestem pewien czy popup się nie wyświetla przez problem z emulatorem czy co, przez chwilkę działało xD | bynajmniej naprawię
		{
			var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            PermStatus.Text = status.ToString ();
        }
        // TEMP \/ \/ \/
        #region TEMP
        private async void GoTo(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new MainPage());
        }
        #endregion
    }
}