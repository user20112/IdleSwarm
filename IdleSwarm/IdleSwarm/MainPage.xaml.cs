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