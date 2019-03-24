using IdleSwarm.Classes;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace IdleSwarm.Droid.Classes
{
    public class UpgradeRow : INotifyPropertyChanged, DisplayRow
    {
        //Display InterfaceSection
        public bool _button2IsVisible = false;
        public bool Button2IsVisible
        {
            get { return _button2IsVisible; }
            set
            {
                _button2IsVisible = value;
                OnPropertyChanged(nameof(Button2IsVisible));
            }
        }
        public bool _button1IsVisible = false;
        public bool Button1IsVisible
        {
            get { return _button1IsVisible; }
            set
            {
                _button1IsVisible = value;
                OnPropertyChanged(nameof(Button1IsVisible));
            }
        }
        public ICommand ButtonLeft { get; set; }
        public ICommand ButtonRight { get; set; }
        public ImageSource _image;
        public ImageSource Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        public string _leftButtonText = "Research";
        public string LeftButtonText
        {
            get { return _leftButtonText; }
            set
            {
                _leftButtonText = value;
                OnPropertyChanged(nameof(LeftButtonText));
            }
        }
        public string _rightButtonText = "Research";
        public string RightButtonText
        {
            get { return _rightButtonText; }
            set
            {
            }
        }
        public string _displayLabel;
        public string DisplayLabel
        {
            get { return _displayLabel; }
            set
            {
                _displayLabel = value;
                OnPropertyChanged(nameof(DisplayLabel));
            }
        }
        public string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public int VespRequired { get; set; }
        public int MineralsRequired { get; set; }
        public string ThirdDisplayLabel
        {
            get { return "ChambersRequired: " + EvolutionChambersRequired.ToString(); }
            set { }
        }
        //Display Interface Section

        //Inotify InterfaceSection
        public event PropertyChangedEventHandler PropertyChanged;
        string ImagePath;
        //Inotify Interface Section

        MainPageViewModel Page;
        string _count = "0";
        public string Count
        {
            get { return _count; }
            set
            {
                _count = value;
                DisplayLabel = Name;
            }
        }
        public string Name;
        public int DronesRequired;
        public int EvolutionChambersRequired;
        public bool _canAfford = false;
        public bool CanAfford
        {
            get
            {
                return _canAfford;
            }
            set
            {
                _canAfford = value;
                OnPropertyChanged(nameof(CanAfford));
                Button2IsVisible = value;
            }
        }

        public UpgradeRow(MainPageViewModel Mainpage, string name, string imagepath, int mineralCost, int vespCost, int DroneCost, string description, Command UpgradeClick, int evolutionChambersRequired)
        {
            EvolutionChambersRequired = evolutionChambersRequired;
            Description = description;
            MineralsRequired = mineralCost;
            VespRequired = vespCost;
            DronesRequired = DroneCost;
            Page = Mainpage;
            Image = imagepath;
            ImagePath = imagepath;
            Name = name;
            DisplayLabel = Name;
            ButtonRight = UpgradeClick;
        }
        
        void OnPropertyChanged(string propertyName)
        {
            var propertyChangedCallback = PropertyChanged;
            propertyChangedCallback?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GetSaveString()
        {
            return Name + "_";
        }
    }
}