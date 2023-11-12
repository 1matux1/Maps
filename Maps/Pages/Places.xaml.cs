using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Maps.PlacesData;

namespace Maps.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Places : ContentPage
    {
        public Places()
        {
            InitializeComponent();

            ListInsert();
        }

        public async void ListInsert()
        {
            PlacesData placesData = new PlacesData();
            var placesList = await placesData.GetInterestingPlaces();

            var listView = new ListView();
            listView.ItemsSource = placesList;
            listView.RowHeight = 240;
            listView.ItemTemplate = new DataTemplate(() =>
            {
                var nameLabel = new Label
                {
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 16,
                    Margin = new Thickness(5, 0, 0, 0)
                };
                nameLabel.SetBinding(Label.TextProperty, "Name");

                var addressLabel = new Label
                {
                    FontSize = 14,
                    Margin = new Thickness(5, 0, 0, 0)
                };
                addressLabel.SetBinding(Label.TextProperty, "Vicinity");

                var typeLabel = new Label
                {
                    FontSize = 14,
                    Margin = new Thickness(5, 0, 0, 0)
                };
                typeLabel.SetBinding(Label.TextProperty, "Types[0]");

                var iconImage = new Image
                {
                    WidthRequest = 100,
                    HeightRequest = 100,
                    Margin = new Thickness(5, 0, 0, 0),
                    Aspect = Aspect.AspectFill
                };
                iconImage.SetBinding(Image.SourceProperty, "Photos[0].PhotoUrl");

                var showOnMapButton = new Button
                {
                    Text = "Pokaż na Mapie",
                    FontSize = 14,
                    Margin = new Thickness(5, 0, 0, 0),
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                showOnMapButton.Clicked += (sender, e) =>
                {
                    var button = (Button)sender;
                    var place = (Place)button.BindingContext;
                    MarkLocation(place.Geometry.Location, place.Name, place.Vicinity);
                };

                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Padding = new Thickness(10),
                        Children = { iconImage, new StackLayout { Children = { nameLabel, addressLabel, typeLabel, showOnMapButton } } }
                    }
                };
            });

            Content = listView;
        }

        public async void MarkLocation(Location location, string name, string vicinity)
        {
            YourLocation Location = new YourLocation();
            await Navigation.PushAsync(Location);

            Location.UpdateMapPins(location, name, vicinity);
        }
    }
}