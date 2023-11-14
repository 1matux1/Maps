using System;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Maps.Pages.Places;
using static Maps.PlacesData;

namespace Maps.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreInfo : ContentPage
    {
        public MoreInfo()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<Places, GetPlaceData>(this, "GetMoreInfo", (sender, args) =>
            {
                Content = null;
                Place place = args.placeData;
                Update(place);
            });
        }

        private void StartDiscover(object sender, EventArgs e)
        {
            var mainPage = this.Parent as TabbedPage;
            mainPage.CurrentPage = mainPage.Children[2];
        }

        private void Update(Place place)
        {
            var nameLabel = new Label
            {
                Text = place.Name,
                FontAttributes = FontAttributes.Bold,
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center
            };

            var addressLabel = new Label
            {
                Text = $"Adres: {place.Vicinity}",
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center
            };

            var ratingLabel = new Label
            {
                Text = $"Ocena: {place.Rating}",
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center
            };

            var openNowLabel = new Label
            {
                Text = $"Otwarte teraz: {(place.OpeningHours?.OpenNow ?? false ? "Tak" : "Nie")}",
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center
            };

            var userRatingsLabel = new Label
            {
                Text = $"Ocena klijentów: {place.UserRatingsTotal}",
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center
            };

            var typesLabel = new Label
            {
                Text = $"{string.Join(", ", place.Types)}",
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center
            };

            var stackLayout = new StackLayout
            {
                Padding = new Thickness(10),
                Spacing = 10,
                Children =
                {
                    nameLabel,
                    addressLabel,
                    ratingLabel,
                    openNowLabel,
                    userRatingsLabel,
                    typesLabel,
                }
            };

            Content = stackLayout;
        }
    }
}