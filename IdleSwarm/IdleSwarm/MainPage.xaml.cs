using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IdleSwarm
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BackgroundImage = "LighterBackground.jpg";
            BindingContext = new MainPageViewModel(DisplayView);
        }
    }
}
