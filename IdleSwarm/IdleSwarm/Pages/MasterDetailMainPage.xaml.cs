using IdleSwarm.Classes;
using IdleSwarm.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IdleSwarm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailMainPage : MasterDetailPage
    {
        bool OnHome = true;
        public static MainPage mainpage = null;
        public static GroupChatPage GroupChat = null;
        public static InfestPage InfestPage = null;
        public MasterDetailMainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DrawerPage Item = (DrawerPage)e.SelectedItem;
            if (Item == null)
                return;
            switch (Item.PageID)
            {
                case 1:
                    if (Detail == null)
                    {
                        Detail = new NavigationPage(mainpage);
                    }
                    else
                        if (!OnHome)
                        Detail.Navigation.PopToRootAsync();
                    MasterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    mainpage.ViewModel.Selected = 0;
                    OnHome = true;
                    break;
                case 2:
                    if (Detail == null)
                        Detail = new NavigationPage(mainpage);
                    else
                        if (!OnHome)
                        Detail.Navigation.PopToRootAsync();
                    MasterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    mainpage.ViewModel.Selected = 1;
                    OnHome = true;
                    break;
                case 3:
                    if (Detail == null)
                        Detail = new NavigationPage(mainpage);
                    else
                        if (!OnHome)
                        Detail.Navigation.PopToRootAsync();
                    MasterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    mainpage.ViewModel.Selected = 2;
                    OnHome = true;
                    break;
                case 4:
                    Detail.Navigation.PushAsync(InfestPage ?? new InfestPage());
                    MasterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    OnHome = false;
                    break;
                case 5:
                    Detail.Navigation.PushAsync(GroupChat ?? new GroupChatPage());
                    MasterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    OnHome = false;
                    break;
                case 6:
                    Detail.Navigation.PushAsync(new Page1(mainpage.ViewModel));
                    MasterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    OnHome = false;
                    break;
            }
        }
    }
}