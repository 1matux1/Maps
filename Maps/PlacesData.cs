using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Maps
{
    public class PlacesData
    {
        public class Geometry
        {
            public Location Location { get; set; }
        }

        public class Location
        {
            public double Lat { get; set; }
            public double Lng { get; set; }
        }

        public class OpeningHours
        {
            public bool OpenNow { get; set; }
        }

        public class Photo
        {
            public int Height { get; set; }
            public string[] HtmlAttributions { get; set; }
            public string Photo_Reference { get; set; }
            public int Width { get; set; }
            public ImageSource PhotoUrl { get; set; }
        }

        public class Place
        {
            public string BusinessStatus { get; set; }
            public Geometry Geometry { get; set; }
            public string Icon { get; set; }
            public string IconBackgroundColor { get; set; }
            public string IconMaskBaseUri { get; set; }
            public string Name { get; set; }
            public OpeningHours OpeningHours { get; set; }
            public Photo[] Photos { get; set; }
            public string PlaceId { get; set; }
            public PlusCode PlusCode { get; set; }
            public double Rating { get; set; }
            public string Reference { get; set; }
            public string Scope { get; set; }
            public string[] Types { get; set; }
            public int UserRatingsTotal { get; set; }
            public string Vicinity { get; set; }
        }

        public class PlusCode
        {
            public string CompoundCode { get; set; }
            public string GlobalCode { get; set; }
        }

        public class PlacesResult
        {
            public string[] HtmlAttributions { get; set; }
            public string NextPageToken { get; set; }
            public Place[] Results { get; set; }
        }

        public async Task<List<Place>> GetInterestingPlaces()
        {
            string apiKey = Secret.api_key;

            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location != null)
            {
                string baseUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json";
                string locationStr = $"{location.Latitude},{location.Longitude}";
                string radius = "1000";
                string type = "point_of_interest";

                string requestUrl = $"{baseUrl}?location={locationStr}&radius={radius}&type={type}&key={apiKey}";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(requestUrl);
                    PlacesResult placesResult = JsonConvert.DeserializeObject<PlacesResult>(response);

                    if (placesResult.Results != null)
                    {
                        foreach (var place in placesResult.Results)
                        {
                            if (place.Photos?.Any() == true)
                            {
                                foreach (var photo in place.Photos)
                                {
                                    if (photo.Photo_Reference != null)
                                    {
                                        string imageRequestUrl = $"https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photo_reference={photo.Photo_Reference}&key={apiKey}";
                                        var imageStream = await client.GetStreamAsync(imageRequestUrl);

                                        if (imageStream != null)
                                        {
                                            photo.PhotoUrl = ImageSource.FromStream(() => imageStream);
                                        }
                                    }
                                }
                            }
                        }
                        return placesResult.Results.ToList();
                    }
                }
            }
            return new List<Place>();
        }
    }
}
