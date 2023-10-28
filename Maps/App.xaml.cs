using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maps
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            base.OnStart();

            HasPermissions();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public void HasPermissions()
        {
            if(Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>().Result == PermissionStatus.Granted)
                MainPage = new NavigationPage(new MainPage());
            else
                MainPage = new NavigationPage(new NoPermissions());
        }
        #region TESTOWANIE CZEGOŚ xD
        public async void GetPermissions()
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            if (status == PermissionStatus.Granted)
                MainPage = new NavigationPage(new MainPage());
        }
        #endregion
    }
}
