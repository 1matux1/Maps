using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Maps.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreInfo : ContentPage
    {
        public MoreInfo()
        {
            InitializeComponent();
        }

        private void StartDiscover(object sender, EventArgs e)
        {
            var mainPage = this.Parent as TabbedPage;
            mainPage.CurrentPage = mainPage.Children[2];
        }

        public void GetMoreInfo(PlacesData.Place place)
        {
            // TU SIĘ BĘDZIE WYŚWIETLAŁO OBIECUJĘ
        }
    }
}