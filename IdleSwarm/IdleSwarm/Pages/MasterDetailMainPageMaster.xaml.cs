using IdleSwarm.Classes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IdleSwarm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailMainPageMaster : ContentPage
    {
        public ListView ListView;

        public MasterDetailMainPageMaster()
        {
            InitializeComponent();

            BindingContext = new MasterDetailMainPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailMainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<DrawerPage> MenuItems { get; set; }

            public MasterDetailMainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<DrawerPage>(new[]
                {
                    new DrawerPageClass(){PageTitle="Units",PageID=1 },
                    new DrawerPageClass(){PageTitle="Structures",PageID=2 },
                    new DrawerPageClass(){PageTitle="Upgrades",PageID=3 },
                   // new DrawerPageClass(){PageTitle="Infestation Panel",PageID=4 },
                   // new DrawerPageClass(){PageTitle="Group Chat",PageID=5 }
                   new DrawerPageClass(){PageTitle="Reset Progress",PageID=6}
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}