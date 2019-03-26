using IdleSwarm.Classes;
using Xamarin.Forms;

namespace IdleSwarm
{
    public partial class MainPage : ContentPage,DrawerPage
    {
        public HiveViewModel ViewModel;
        public int PageID { get; set; }
        public string PageTitle { get; set; }
        public MainPage()
        {
            PageID = 1;
            PageTitle = "Hive";
            InitializeComponent();
            BackgroundImage = "LighterBackground.jpg";
            ViewModel = new HiveViewModel(DisplayView);
            BindingContext = ViewModel;
            
            MasterDetailMainPage.mainpage = this;
        }
    }
}