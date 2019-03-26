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

        public BigInteger VespRequired { get; set; }
        public BigInteger MineralsRequired { get; set; }

        public string ThirdDisplayLabel
        {
            get { return "Larva Required: " + LarvaRequired.ToString(); }
            set { }
        }

        //end Display Interface
        private Thread IncomeThread;

        public string Name;
        private string _count = "0";

        public string Count
        {
            get { return _count; }
            set
            {
                _count = value;
                DisplayLabel = Name + ": " + Count;
            }
        }

        public int IncreasePerBuy = 0;

        public Func<BigInteger> OtherRestrictions = DefualtOtherRestrictions;
        private int IncomeSpeed;
        public int MineralsPerRound = 5;
        public int VespPerRound = 0;
        public int LarvaPerRound = 0;
        public int LarvaRequired = 1;
        public int SupplyRequired = 1;
        public int SupplyProvided = 0;
        private BigInteger _numberCanAfford = 0;

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

        private int _numberPerClick = 1;

        public int NumberPerClick
        {
            get { return _numberPerClick; }
            set
            {
                _numberPerClick = value;
                LeftButtonText = value.ToString();
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            var propertyChangedCallback = PropertyChanged;
            propertyChangedCallback?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private HiveViewModel Page;
        private string ImagePath;

        public event PropertyChangedEventHandler PropertyChanged;

        public UnitRow(HiveViewModel Mainpage, string name, string imagepath, int mineralPerRound, int vespPerRound, int larvaPerRound, int mineralCost, int vespCost, int larvaCost, int incomeSpeed, string description, int SupplyRequirement, int supplyProvided)
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
        ~UnitRow()
        {
            IncomeThread.Abort();
        }
        public UnitRow(HiveViewModel mainpage, string name)
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
                            switch (Name)
                            {
                                case "Drone":
                                    ButtonRight = new Command(DroneBuyAll);
                                    ButtonLeft = new Command(DroneBuySelection);
                                    OtherRestrictions = DroneOtherRestrictions;
                                    break;

                                case "Zergling":
                                    ButtonLeft = new Command(ZerglingBuySelection);
                                    ButtonRight = new Command(ZerglingBuyAll);
                                    break;

                                case "Roach":

                                    break;

                                case "Queen":
                                    ButtonLeft = new Command(QueenBuySelection);
                                    ButtonLeft = new Command(QueenBuyAll);
                                    OtherRestrictions = QueenOtherRestrictions;
                                    break;

                                case "Infested Terran":
                                    Button1IsVisible = false;
                                    Button2IsVisible = false;
                                    break;

                                case "Overlord":
                                    ButtonLeft = new Command(OverlordBuySelection);
                                    ButtonRight = new Command(OverlordBuyAll);
                                    break;
                            }
                            IncomeThread = new Thread(new ThreadStart(IncomeFunction));
                            IncomeThread.Start();
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

        private void IncomeFunction()
        {
            Page.Free.WaitOne();
            while (Page.Running)
            {
                Thread.Sleep(IncomeSpeed);
                Page.MineralValue = ((Page.MineralValue + MineralsPerRound * BigInteger.Parse(Count)) *(100+BigInteger.Parse(Page.Things.FindStructure("CreepTumor").Count)) / 100);
                Page.VespeneValue = (Page.VespeneValue + VespPerRound * BigInteger.Parse(Count));
                Page.LarvaValue = (Page.LarvaValue + LarvaPerRound * BigInteger.Parse(Count));
            }
        }

        private void BuySelectionFunction()
        {
            Page.Free.WaitOne();
            if (Page.MineralValue >= (BigInteger)Page.NumberPerClick * (BigInteger)MineralsRequired && Page.VespeneValue >= Page.NumberPerClick * VespRequired && Page.LarvaValue >= Page.NumberPerClick * LarvaRequired)
            {
                Count = (BigInteger.Parse(Count) + Page.NumberPerClick).ToString();
                Page.MineralValue = Page.MineralValue - Page.NumberPerClick * MineralsRequired;
                Page.VespeneValue = (Page.VespeneValue - Page.NumberPerClick * VespRequired);
                Page.LarvaValue = (Page.LarvaValue - Page.NumberPerClick * LarvaRequired);
                Page.SupplyValue -= Page.NumberPerClick * SupplyRequired;
                MineralsRequired += ((MineralsRequired * IncreasePerBuy * Page.NumberPerClick)) / 100;
                VespRequired += ((VespRequired * IncreasePerBuy * Page.NumberPerClick)) / 100;
            }
        }

        private void BuyAllFunction()
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
                MineralsRequired += ((MineralsRequired * IncreasePerBuy * numberCanAfford)) / 100;
                VespRequired += ((VespRequired * IncreasePerBuy * numberCanAfford)) / 100;
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
                    MineralsRequired += ((MineralsRequired * IncreasePerBuy * numberCanAfford)) / 100;
                    VespRequired += ((VespRequired * IncreasePerBuy * numberCanAfford)) / 100;
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
                    MineralsRequired += ((MineralsRequired * IncreasePerBuy * Page.NumberPerClick)) / 100;
                    VespRequired += ((VespRequired * IncreasePerBuy * Page.NumberPerClick)) / 100;
                    MineralsRequired += ((MineralsRequired * IncreasePerBuy * Page.NumberPerClick)) / 100;
                    VespRequired += ((VespRequired * IncreasePerBuy * Page.NumberPerClick)) / 100;
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
                MineralsRequired += ((MineralsRequired * IncreasePerBuy * Page.NumberPerClick)) / 100;
                VespRequired += ((VespRequired * IncreasePerBuy * Page.NumberPerClick)) / 100;
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
                if (Page.Things.UpgradeExists("More Effecient Zerglings"))
                {
                    Count = (BigInteger.Parse(Count) + Page.NumberPerClick * 2).ToString();
                }
                else
                {
                    Count = (BigInteger.Parse(Count) + Page.NumberPerClick * 3).ToString();
                }
                Page.MineralValue = Page.MineralValue - Page.NumberPerClick * MineralsRequired;
                Page.VespeneValue = (Page.VespeneValue - Page.NumberPerClick * VespRequired);
                Page.LarvaValue = (Page.LarvaValue - Page.NumberPerClick * LarvaRequired);
                Page.SupplyValue -= Page.NumberPerClick * SupplyRequired;
                MineralsRequired += ((MineralsRequired * IncreasePerBuy * Page.NumberPerClick)) / 100;
                VespRequired += ((VespRequired * IncreasePerBuy * Page.NumberPerClick)) / 100;
            }
        }

        public void ZerglingBuyAll()
        {
            Page.Free.WaitOne();
            if (Page.MineralValue >= (BigInteger)NumberCanAfford * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)NumberCanAfford * (BigInteger)VespRequired && Page.LarvaValue >= (BigInteger)NumberCanAfford * (BigInteger)LarvaRequired)
            {
                BigInteger numberCanAfford = NumberCanAfford;
                if (Page.Things.UpgradeExists("More Effecient Zerglings"))
                {
                    Count = (BigInteger.Parse(Count) + numberCanAfford * 2).ToString();
                }
                else
                {
                    Count = (BigInteger.Parse(Count) + numberCanAfford * 3).ToString();
                }
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

        public static BigInteger DefualtOtherRestrictions()
        {
            return 100000000;
        }

        public BigInteger QueenOtherRestrictions()
        {
            return (BigInteger.Parse(Page.Things.FindStructure("Hatchery").Count) + 1) - (BigInteger.Parse(Count));
        }

        public BigInteger DroneOtherRestrictions()
        {
            return (BigInteger.Parse(Page.Things.FindStructure("Hatchery").Count) * 30 + 30) - (BigInteger.Parse(Count));
        }

        public BigInteger ZerglingOtherRestrictions()
        {
            return (BigInteger.Parse(Page.Things.FindStructure("Spawning Pool").Count) * 1000) - (BigInteger.Parse(Count));
        }

        public BigInteger RoachOtherRestrictions()
        {
            return (BigInteger.Parse(Page.Things.FindStructure("Roach Warren").Count) * 500) - (BigInteger.Parse(Count));
        }
    }
}