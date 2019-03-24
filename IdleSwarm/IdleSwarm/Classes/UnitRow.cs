using System;
using System.ComponentModel;
using System.Numerics;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace IdleSwarm.Droid.Classes
{
    public class UnitRow : INotifyPropertyChanged, DisplayRow
    {
        //Display Interface
        public bool _button2IsVisible = true;
        public bool Button2IsVisible
        {
            get { return _button2IsVisible; }
            set
            {
                _button2IsVisible = value;
                OnPropertyChanged(nameof(Button2IsVisible));
            }
        }
        public bool _button1IsVisible = true;
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
        public string _leftButtonText = "0";
        public string LeftButtonText
        {
            get { return Page.NumberPerClick.ToString(); }
            set
            {
                _leftButtonText = value;
                OnPropertyChanged(nameof(LeftButtonText));
            }
        }
        public string _rightButtonText = "0";
        public string RightButtonText
        {
            get { return _rightButtonText; }
            set
            {
                _rightButtonText = value;
                OnPropertyChanged(nameof(RightButtonText));
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
            get { return "Larva Required: " + LarvaRequired.ToString(); }
            set { }
        }

        //end Display Interface

        Thread IncomeThread;
        public string Name;
        string _count = "0";
        public string Count
        {
            get { return _count; }
            set
            {
                _count = value;
                DisplayLabel = Name + ": " + Count;
            }
        }

        int IncomeSpeed;
        public int MineralsPerRound = 5;
        public int VespPerRound = 0;
        public int LarvaPerRound = 0;
        public int LarvaRequired = 1;
        public int SupplyRequired = 1;
        public int SupplyProvided = 0;
        BigInteger _numberCanAfford = 0;
        public BigInteger NumberCanAfford
        {
            get { return _numberCanAfford; }
            set
            {
                _numberCanAfford = value;
                RightButtonText = value.ToString();
                if (Name != "Infested Terran")
                {
                    if (value > 0)
                        Button2IsVisible = true;
                    if (value == 0)
                        Button2IsVisible = false;
                    Button1IsVisible = !(_numberCanAfford < 1);
                }
            }
        }
        int _numberPerClick = 1;
        public int NumberPerClick
        {
            get { return _numberPerClick; }
            set
            {
                _numberPerClick = value;
                LeftButtonText = value.ToString();
            }
        }

        MainPageViewModel Page;
        string ImagePath;
        public event PropertyChangedEventHandler PropertyChanged;
        public UnitRow(MainPageViewModel Mainpage, string name, string imagepath, int mineralPerRound, int vespPerRound, int larvaPerRound, int mineralCost, int vespCost, int larvaCost, int incomeSpeed, string description, int SupplyRequirement, int supplyProvided)
        {
            SupplyProvided = supplyProvided;
            SupplyRequired = SupplyRequirement;
            Description = description;
            IncomeSpeed = incomeSpeed;
            MineralsPerRound = mineralPerRound;
            VespPerRound = vespPerRound;
            LarvaPerRound = larvaPerRound;
            MineralsRequired = mineralCost;
            VespRequired = vespCost;
            LarvaRequired = larvaCost;
            Page = Mainpage;
            Image = imagepath;
            ImagePath = imagepath;
            Name = name;
            DisplayLabel = Name + ": " + Count;
            ButtonLeft = new Command(BuySelectionFunction);
            ButtonRight = new Command(BuyAllFunction);
            IncomeThread = new Thread(new ThreadStart(IncomeFunction));
            IncomeThread.Start();
        }
        public UnitRow(MainPageViewModel mainpage, string name)
        {
            Page = mainpage;
            // | per feild _ per variable
            //UnitString
            // Name + "|" + SupplyProvided + "|" + SupplyRequired + "|" + Description + "|" + IncomeSpeed + "|" + MineralsPerRound + "|" + VespPerRound + "|" + LarvaPerRound + "|" + MineralsRequired + "|" + VespRequired + "|" + LarvaRequired + "|" + ImagePath + "|" + Count + "|";
            string CurrentData = "";
            int NumberSeenAlready = 0;
            for (int CurrentChar = 0; CurrentChar < name.Length; CurrentChar++)
            {
                if (name[CurrentChar] == '|')
                {
                    switch (NumberSeenAlready)
                    {
                        case 0:
                            Name = CurrentData;
                            break;
                        case 1:
                            SupplyProvided = Convert.ToInt32(CurrentData);
                            break;
                        case 2:
                            SupplyRequired = Convert.ToInt32(CurrentData);
                            break;
                        case 3:
                            Description = CurrentData;
                            break;
                        case 4:
                            IncomeSpeed = Convert.ToInt32(CurrentData);
                            break;
                        case 5://min
                            MineralsPerRound = Convert.ToInt32(CurrentData);
                            break;
                        case 6://vesp
                            VespPerRound = Convert.ToInt32(CurrentData);
                            break;
                        case 7://larv
                            LarvaPerRound = Convert.ToInt32(CurrentData);
                            break;
                        case 8:
                            MineralsRequired = Convert.ToInt32(CurrentData);
                            break;
                        case 9:
                            VespRequired = Convert.ToInt32(CurrentData);
                            break;
                        case 10:
                            LarvaRequired = Convert.ToInt32(CurrentData);
                            break;
                        case 11:
                            Image = CurrentData;
                            ImagePath = CurrentData;
                            break;
                        case 12:
                            Count = CurrentData;
                            if (Name == "Overlord")
                            {
                                ButtonLeft = new Command(OverlordBuySelection);
                                ButtonRight = new Command(OverlordBuyAll);
                                IncomeThread = new Thread(new ThreadStart(IncomeFunction));
                                IncomeThread.Start();
                            }
                            else
                            {
                                if (Name == "Infested Terran")
                                {
                                    Button1IsVisible = false;
                                    Button2IsVisible = false;
                                    IncomeThread = new Thread(new ThreadStart(IncomeFunction));
                                    IncomeThread.Start();
                                }
                                else
                                {
                                    ButtonLeft = new Command(BuySelectionFunction);
                                    ButtonRight = new Command(BuyAllFunction);
                                    IncomeThread = new Thread(new ThreadStart(IncomeFunction));
                                    IncomeThread.Start();
                                }
                            }
                            break;
                    }
                    NumberSeenAlready++;
                    CurrentData = "";
                }
                else
                {
                    CurrentData += name[CurrentChar];
                }
            }
        }
        void IncomeFunction()
        {
            Page.Free.WaitOne();
            while (Page.Running)
            {
                Thread.Sleep(IncomeSpeed);
                Page.MineralValue = Page.MineralValue + MineralsPerRound * BigInteger.Parse(Count);
                Page.VespeneValue = (Page.VespeneValue + VespPerRound * BigInteger.Parse(Count));
                Page.LarvaValue = (Page.LarvaValue + LarvaPerRound * BigInteger.Parse(Count));
            }
        }
        void BuySelectionFunction()
        {
            Page.Free.WaitOne();
            if (Page.MineralValue >= (BigInteger)Page.NumberPerClick * (BigInteger)MineralsRequired && Page.VespeneValue >= Page.NumberPerClick * VespRequired && Page.LarvaValue >= Page.NumberPerClick * LarvaRequired)
            {
                Count = (BigInteger.Parse(Count) + Page.NumberPerClick).ToString();
                Page.MineralValue = Page.MineralValue - Page.NumberPerClick * MineralsRequired;
                Page.VespeneValue = (Page.VespeneValue - Page.NumberPerClick * VespRequired);
                Page.LarvaValue = (Page.LarvaValue - Page.NumberPerClick * LarvaRequired);
                Page.SupplyValue -= Page.NumberPerClick * SupplyRequired;
            }
        }
        void BuyAllFunction()
        {
            Page.Free.WaitOne();
            if (Page.MineralValue >= (BigInteger)NumberCanAfford * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)NumberCanAfford * (BigInteger)VespRequired && Page.LarvaValue >= (BigInteger)NumberCanAfford * (BigInteger)LarvaRequired)
            {
                BigInteger numberCanAfford = NumberCanAfford;
                Count = (BigInteger.Parse(Count) + numberCanAfford).ToString();
                Page.MineralValue = Page.MineralValue - numberCanAfford * (BigInteger)MineralsRequired;
                Page.VespeneValue = (Page.VespeneValue - numberCanAfford * (BigInteger)VespRequired);
                Page.LarvaValue = (Page.LarvaValue - numberCanAfford * (BigInteger)LarvaRequired);
                Page.SupplyValue -= numberCanAfford * SupplyRequired;
            }
        }
        public void DroneBuyAll()
        {
            Page.Free.WaitOne();
            if (BigInteger.Parse(Count) + NumberCanAfford <= 30 + (BigInteger.Parse(Page.Things.FindStructure("Hatchery").Count) * 30))
            {
                if (Page.MineralValue >= (BigInteger)NumberCanAfford * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)NumberCanAfford * (BigInteger)VespRequired && Page.LarvaValue >= (BigInteger)NumberCanAfford * (BigInteger)LarvaRequired)
                {
                    BigInteger numberCanAfford = NumberCanAfford;
                    Count = (BigInteger.Parse(Count) + numberCanAfford).ToString();
                    Page.MineralValue = Page.MineralValue - numberCanAfford * (BigInteger)MineralsRequired;
                    Page.VespeneValue = (Page.VespeneValue - numberCanAfford * (BigInteger)VespRequired);
                    Page.LarvaValue = (Page.LarvaValue - numberCanAfford * (BigInteger)LarvaRequired);
                    Page.SupplyValue -= numberCanAfford * SupplyRequired;
                }
            }
        }
        public void DroneBuySelection()
        {
            Page.Free.WaitOne();
            if (BigInteger.Parse(Count) + Page.NumberPerClick <= 30 + (BigInteger.Parse(Page.Things.FindStructure("Hatchery").Count) * 30))
            {
                if (Page.MineralValue >= (BigInteger)Page.NumberPerClick * (BigInteger)MineralsRequired && Page.VespeneValue >= Page.NumberPerClick * VespRequired && Page.LarvaValue >= Page.NumberPerClick * LarvaRequired)
                {
                    Count = (BigInteger.Parse(Count) + Page.NumberPerClick).ToString();
                    Page.MineralValue = Page.MineralValue - Page.NumberPerClick * MineralsRequired;
                    Page.VespeneValue = (Page.VespeneValue - Page.NumberPerClick * VespRequired);
                    Page.LarvaValue = (Page.LarvaValue - Page.NumberPerClick * LarvaRequired);
                    Page.SupplyValue -= Page.NumberPerClick * SupplyRequired;
                }
            }
        }
        public void QueenBuyAll()
        {
            Page.Free.WaitOne();
            if (BigInteger.Parse(Count) + NumberCanAfford <= 1 + (BigInteger.Parse(Page.Things.FindStructure("Hatchery").Count)))
            {
                if (Page.MineralValue >= (BigInteger)NumberCanAfford * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)NumberCanAfford * (BigInteger)VespRequired && Page.LarvaValue >= (BigInteger)NumberCanAfford * (BigInteger)LarvaRequired)
                {
                    BigInteger numberCanAfford = NumberCanAfford;
                    Count = (BigInteger.Parse(Count) + numberCanAfford).ToString();
                    Page.MineralValue = Page.MineralValue - numberCanAfford * (BigInteger)MineralsRequired;
                    Page.VespeneValue = (Page.VespeneValue - numberCanAfford * (BigInteger)VespRequired);
                    Page.LarvaValue = (Page.LarvaValue - numberCanAfford * (BigInteger)LarvaRequired);
                    Page.SupplyValue -= numberCanAfford * SupplyRequired;
                }
            }
        }
        public void QueenBuySelection()
        {
            Page.Free.WaitOne();
            if (BigInteger.Parse(Count) + Page.NumberPerClick <= 1 + (BigInteger.Parse(Page.Things.FindStructure("Hatchery").Count)))
            {
                if (Page.MineralValue >= (BigInteger)Page.NumberPerClick * (BigInteger)MineralsRequired && Page.VespeneValue >= Page.NumberPerClick * VespRequired && Page.LarvaValue >= Page.NumberPerClick * LarvaRequired)
                {
                    Count = (BigInteger.Parse(Count) + Page.NumberPerClick).ToString();
                    Page.MineralValue = Page.MineralValue - Page.NumberPerClick * MineralsRequired;
                    Page.VespeneValue = (Page.VespeneValue - Page.NumberPerClick * VespRequired);
                    Page.LarvaValue = (Page.LarvaValue - Page.NumberPerClick * LarvaRequired);
                    Page.SupplyValue -= Page.NumberPerClick * SupplyRequired;
                }
            }
        }
        void OnPropertyChanged(string propertyName)
        {
            var propertyChangedCallback = PropertyChanged;
            propertyChangedCallback?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void OverlordBuySelection()
        {
            Page.Free.WaitOne();
            if (Page.MineralValue >= (BigInteger)Page.NumberPerClick * (BigInteger)MineralsRequired && Page.VespeneValue >= Page.NumberPerClick * VespRequired && Page.LarvaValue >= Page.NumberPerClick * LarvaRequired)
            {
                Count = (BigInteger.Parse(Count) + Page.NumberPerClick).ToString();
                Page.MineralValue = Page.MineralValue - Page.NumberPerClick * MineralsRequired;
                Page.VespeneValue = (Page.VespeneValue - Page.NumberPerClick * VespRequired);
                Page.LarvaValue = (Page.LarvaValue - Page.NumberPerClick * LarvaRequired);
                Page.SupplyValue += Page.NumberPerClick * SupplyProvided;
            }
        }
        public void OverlordBuyAll()
        {

            Page.Free.WaitOne();
            if (Page.MineralValue >= (BigInteger)NumberCanAfford * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)NumberCanAfford * (BigInteger)VespRequired && Page.LarvaValue >= (BigInteger)NumberCanAfford * (BigInteger)LarvaRequired)
            {
                BigInteger numberCanAfford = NumberCanAfford;
                Count = (BigInteger.Parse(Count) + numberCanAfford).ToString();
                Page.MineralValue = Page.MineralValue - numberCanAfford * (BigInteger)MineralsRequired;
                Page.VespeneValue = (Page.VespeneValue - numberCanAfford * (BigInteger)VespRequired);
                Page.LarvaValue = (Page.LarvaValue - numberCanAfford * (BigInteger)LarvaRequired);
                Page.SupplyValue += numberCanAfford * SupplyProvided;
            }
        }
        public void ZerglingBuySelection()
        {
            Page.Free.WaitOne();
            if (Page.MineralValue >= (BigInteger)Page.NumberPerClick * (BigInteger)MineralsRequired && Page.VespeneValue >= Page.NumberPerClick * VespRequired && Page.LarvaValue >= Page.NumberPerClick * LarvaRequired)
            {
                Count = (BigInteger.Parse(Count) + Page.NumberPerClick * 2).ToString();
                Page.MineralValue = Page.MineralValue - Page.NumberPerClick * MineralsRequired;
                Page.VespeneValue = (Page.VespeneValue - Page.NumberPerClick * VespRequired);
                Page.LarvaValue = (Page.LarvaValue - Page.NumberPerClick * LarvaRequired);
                Page.SupplyValue -= Page.NumberPerClick * SupplyRequired;
            }
        }
        public void ZerglingBuyAll()
        {

            Page.Free.WaitOne();
            if (Page.MineralValue >= (BigInteger)NumberCanAfford * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)NumberCanAfford * (BigInteger)VespRequired && Page.LarvaValue >= (BigInteger)NumberCanAfford * (BigInteger)LarvaRequired)
            {
                BigInteger numberCanAfford = NumberCanAfford;
                Count = (BigInteger.Parse(Count) + numberCanAfford * 2).ToString();
                Page.MineralValue = Page.MineralValue - numberCanAfford * (BigInteger)MineralsRequired;
                Page.VespeneValue = (Page.VespeneValue - numberCanAfford * (BigInteger)VespRequired);
                Page.LarvaValue = (Page.LarvaValue - numberCanAfford * (BigInteger)LarvaRequired);
                Page.SupplyValue -= numberCanAfford * SupplyRequired;
            }
        }
        public string GetSaveString()
        {
            return Name + "|" + SupplyProvided + "|" + SupplyRequired + "|" + Description + "|" + IncomeSpeed + "|" + MineralsPerRound + "|" + VespPerRound + "|" + LarvaPerRound + "|" + MineralsRequired + "|" + VespRequired + "|" + LarvaRequired + "|" + ImagePath + "|" + Count + "|_";
        }
    }
}