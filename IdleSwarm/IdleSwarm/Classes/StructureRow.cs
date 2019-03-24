using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace IdleSwarm.Droid.Classes
{
    public class StructureRow : INotifyPropertyChanged, DisplayRow
    {
        //Display Interface
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
        public string ThirdDisplayLabel
        {
            get { return "Drones Required: " + DronesRequired.ToString(); }
            set { }
        }
        //end Display Interface
        bool Boughtyet = false;
        Thread IncomeThread;
        public ICommand incomeFunction;

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
        public int VespRequired { get; set; }
        public int MineralsRequired { get; set; }
        public int DronesRequired = 1;
        BigInteger _numberCanAfford = 0;
        public BigInteger NumberCanAfford
        {
            get { return _numberCanAfford; }
            set
            {
                _numberCanAfford = value;
                RightButtonText = value.ToString();

                if (value > 0)
                    Button2IsVisible = true;
                if (value == 0)
                    Button2IsVisible = false;
                Button1IsVisible = !(_numberCanAfford < 1);

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
        public int DronesPerRound = 1;
        string ImagePath;

        public event PropertyChangedEventHandler PropertyChanged;
        public StructureRow(MainPageViewModel Mainpage, string name, string imagepath, int mineralPerRound, int vespPerRound, int larvaPerRound, int mineralCost, int vespCost, int DroneCost, int incomeSpeed, string description)
        {
            Description = description;
            IncomeSpeed = incomeSpeed;
            MineralsPerRound = mineralPerRound;
            VespPerRound = vespPerRound;
            LarvaPerRound = larvaPerRound;
            MineralsRequired = mineralCost;
            VespRequired = vespCost;
            DronesRequired = DroneCost;
            Page = Mainpage;
            Image = imagepath;
            ImagePath = imagepath;
            Name = name;
            DisplayLabel = Name + ": " + Count;
            ButtonLeft = new Command(BuySelectionFunction);
            ButtonRight = new Command(BuyAllFunction);
            incomeFunction = new Command(DefualtIncomeFunction);
            IncomeThread = new Thread(new ThreadStart(() => incomeFunction.Execute(null)));
            IncomeThread.Start();
        }
        public StructureRow(MainPageViewModel MainPage, string name)
        {
            Page = MainPage;
            // | per feild _ per variable
            //StructureString
            //Name + "|"+Description+"|" + IncomeSpeed + "|" + MineralsPerRound + "|" + VespPerRound + "|" + LarvaPerRound + "|" + MineralsRequired + "|" + VespRequired + "|" + DronesRequired + "|" + ImagePath + "|" + Count + "|"+DronesPerRound +"|"
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
                            Description = CurrentData;
                            break;
                        case 2:
                            IncomeSpeed = Convert.ToInt32(CurrentData);
                            break;
                        case 3:
                            MineralsPerRound = Convert.ToInt32(CurrentData);
                            break;
                        case 4:
                            VespPerRound = Convert.ToInt32(CurrentData);
                            break;
                        case 5://min
                            LarvaPerRound = Convert.ToInt32(CurrentData);
                            break;
                        case 6://vesp
                            MineralsRequired = Convert.ToInt32(CurrentData);
                            break;
                        case 7://larv
                            VespRequired = Convert.ToInt32(CurrentData);
                            break;
                        case 8:
                            DronesRequired = Convert.ToInt32(CurrentData);
                            break;
                        case 9:
                            Image = CurrentData;
                            ImagePath = CurrentData;
                            break;
                        case 10:
                            Count = CurrentData;
                            break;
                        case 11:
                            DronesPerRound = Convert.ToInt32(CurrentData);
                            if (Name == "Extractor")
                            {
                                ButtonLeft = new Command(BuySelectionFunction);
                                ButtonRight = new Command(BuyAllFunction);
                                incomeFunction = new Command(ExtractorIncomeFunction);
                                IncomeThread = new Thread(new ThreadStart(() => incomeFunction.Execute(null)));
                                IncomeThread.Start();
                            }
                            else
                            {
                                if (Name == "Hatchery")
                                {
                                    ButtonLeft = new Command(BuySelectionFunction);
                                    ButtonRight = new Command(BuyAllFunction);
                                }
                                else
                                {
                                    if (Name == "EvolutionChamber")
                                    {
                                        ButtonLeft = new Command(BuySelectionFunction);
                                        ButtonRight = new Command(BuyEvolutionChamber);
                                        incomeFunction = new Command(DefualtIncomeFunction);
                                        IncomeThread = new Thread(new ThreadStart(() => incomeFunction.Execute(null)));
                                        IncomeThread.Start();
                                        Button1IsVisible = false;
                                        RightButtonText = "Build";
                                    }
                                    else
                                    {
                                        if (Name == "Spawning Pool")
                                        {
                                            ButtonLeft = new Command(BuySelectionFunction);
                                            ButtonRight = new Command(BuySpawningPool);
                                            incomeFunction = new Command(DefualtIncomeFunction);
                                            IncomeThread = new Thread(new ThreadStart(() => incomeFunction.Execute(null)));
                                            IncomeThread.Start();
                                            Button1IsVisible = false;
                                            RightButtonText = "Build";
                                        }
                                        else
                                        {
                                            if (Name == "Roach Warren")
                                            {
                                                ButtonLeft = new Command(BuySelectionFunction);
                                                ButtonRight = new Command(BuyRoachWarren);
                                                incomeFunction = new Command(DefualtIncomeFunction);
                                                IncomeThread = new Thread(new ThreadStart(() => incomeFunction.Execute(null)));
                                                IncomeThread.Start();
                                                Button1IsVisible = false;
                                                RightButtonText = "Build";
                                            }
                                            else
                                            {
                                                if (Name == "Infested Baracks")
                                                {
                                                    if (Page.Units.Count > 5)
                                                    {
                                                        Boughtyet = true;
                                                        ButtonLeft = new Command(BuyIBSelectionFunction);
                                                        ButtonRight = new Command(BuyIBAllFunction);
                                                        incomeFunction = new Command(InfestedBaracksIncome);
                                                        IncomeThread = new Thread(new ThreadStart(() => incomeFunction.Execute(null)));
                                                        IncomeThread.Start();
                                                    }
                                                    else
                                                    {

                                                        Boughtyet = false;
                                                        ButtonLeft = new Command(BuyIBSelectionFunction);
                                                        ButtonRight = new Command(BuyIBAllFunction);
                                                        incomeFunction = new Command(InfestedBaracksIncome);
                                                        IncomeThread = new Thread(new ThreadStart(() => incomeFunction.Execute(null)));
                                                        IncomeThread.Start();
                                                    }
                                                }
                                                else
                                                {
                                                    ButtonLeft = new Command(BuySelectionFunction);
                                                    ButtonRight = new Command(BuyAllFunction);
                                                    incomeFunction = new Command(DefualtIncomeFunction);
                                                    IncomeThread = new Thread(new ThreadStart(() => incomeFunction.Execute(null)));
                                                    IncomeThread.Start();
                                                }
                                            }
                                        }
                                    }
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
        public void OverWriteIncomeFunction(ICommand NewFunction)
        {
            IncomeThread.Abort();
            incomeFunction = NewFunction;
            IncomeThread = new Thread(new ThreadStart(() => incomeFunction.Execute(null)));
            IncomeThread.Start();
        }
        public void DefualtIncomeFunction()
        {
            while (Page.Running)
            {
                Page.Free.WaitOne();
                Thread.Sleep(IncomeSpeed);
            }
        }
        public void ExtractorIncomeFunction()
        {
            while (Page.Running)
            {
                Page.Free.WaitOne();
                Thread.Sleep(IncomeSpeed);
                Page.VespeneValue = (Page.VespeneValue + VespPerRound * BigInteger.Parse(Count));
            }
        }
        public void BuySelectionFunction()
        {
            Page.Free.WaitOne();
            if (Page.MineralValue >= (BigInteger)Page.NumberPerClick * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)Page.NumberPerClick * (BigInteger)VespRequired && BigInteger.Parse(Page.Units[0].Count) >= Page.NumberPerClick * DronesRequired)
            {
                Count = (BigInteger.Parse(Count) + Page.NumberPerClick).ToString();
                Page.MineralValue = (Page.MineralValue - Page.NumberPerClick * MineralsRequired);
                Page.VespeneValue = (Page.VespeneValue - Page.NumberPerClick * VespRequired);
                Page.Units[0].Count = (BigInteger.Parse(Page.Units[0].Count) - Page.NumberPerClick * DronesRequired).ToString();
            }
        }
        public void BuyEvolutionChamber()
        {
            Page.Free.WaitOne();
            if (Convert.ToInt32(Count) < 4)
            {
                if (Page.MineralValue >= (BigInteger)Page.NumberPerClick * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)Page.NumberPerClick * (BigInteger)VespRequired && BigInteger.Parse(Page.Units[0].Count) >= Page.NumberPerClick * DronesRequired)
                {
                    Count = (BigInteger.Parse(Count) + Page.NumberPerClick).ToString();
                    Page.MineralValue = (Page.MineralValue - Page.NumberPerClick * MineralsRequired);
                    Page.VespeneValue = (Page.VespeneValue - Page.NumberPerClick * VespRequired);
                    Page.Units[0].Count = (BigInteger.Parse(Page.Units[0].Count) - Page.NumberPerClick * DronesRequired).ToString();
                    MineralsRequired = MineralsRequired * 100;
                    VespRequired = VespRequired * 100;
                    DronesRequired = DronesRequired * 100;
                    if (Convert.ToInt32(Count) == 3)
                    {
                        Button1IsVisible = false;
                        Button2IsVisible = false;
                        MineralsRequired = 0;
                        VespRequired = 0;
                        DronesRequired = 0;
                    }
                }
            }
        }
        public void BuyRoachWarren()
        {
            if (Page.MineralValue >= (BigInteger)Page.NumberPerClick * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)Page.NumberPerClick * (BigInteger)VespRequired && BigInteger.Parse(Page.Units[0].Count) >= Page.NumberPerClick * DronesRequired)
            {
                Count = (BigInteger.Parse(Count) + Page.NumberPerClick).ToString();
                Page.MineralValue = (Page.MineralValue - Page.NumberPerClick * MineralsRequired);
                Page.VespeneValue = (Page.VespeneValue - Page.NumberPerClick * VespRequired);
                Page.Units[0].Count = (BigInteger.Parse(Page.Units[0].Count) - Page.NumberPerClick * DronesRequired).ToString();
                Page.Units.Add(Page.Things.GetRoach());
                RemoveStructure("Roach Warren");
                foreach (UpgradeRow upgrade in Page.Things.RoachUpgrades)
                {
                    Page.Upgrades.Add(upgrade);
                }
            }
        }
        public void BuySpawningPool()
        {
            Page.Free.WaitOne();
            if (Page.MineralValue >= (BigInteger)Page.NumberPerClick * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)Page.NumberPerClick * (BigInteger)VespRequired && BigInteger.Parse(Page.Units[0].Count) >= Page.NumberPerClick * DronesRequired)
            {
                Count = (BigInteger.Parse(Count) + Page.NumberPerClick).ToString();
                Page.MineralValue = (Page.MineralValue - Page.NumberPerClick * MineralsRequired);
                Page.VespeneValue = (Page.VespeneValue - Page.NumberPerClick * VespRequired);
                Page.Units[0].Count = (BigInteger.Parse(Page.Units[0].Count) - Page.NumberPerClick * DronesRequired).ToString();
                RemoveStructure("Spawning Pool");
                Page.Units.Add(Page.Things.GetZergling());
                foreach (UpgradeRow upgrade in Page.Things.ZerglingUpgrades)
                {
                    Page.Upgrades.Add(upgrade);
                }
            }
        }
        public void BuyIBAllFunction()
        {
            if (!Boughtyet)
            {
                onFirstInfestedBuy();
            }
            if (Page.MineralValue >= (BigInteger)NumberCanAfford * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)NumberCanAfford * (BigInteger)VespRequired && BigInteger.Parse(Page.Units[0].Count) >= NumberCanAfford * DronesRequired)
            {
                BigInteger numberCanAfford = NumberCanAfford;
                Count = (BigInteger.Parse(Count) + numberCanAfford).ToString();
                Page.MineralValue = (Page.MineralValue - numberCanAfford * MineralsRequired);
                Page.VespeneValue = (Page.VespeneValue - numberCanAfford * VespRequired);
                Page.Units[0].Count = (BigInteger.Parse(Page.Units[0].Count) - numberCanAfford * DronesRequired).ToString();
            }

        }
        public void BuyIBSelectionFunction()
        {
            if (!Boughtyet)
            {
                onFirstInfestedBuy();
            }
            if (Page.MineralValue >= (BigInteger)Page.NumberPerClick * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)Page.NumberPerClick * (BigInteger)VespRequired && BigInteger.Parse(Page.Units[0].Count) >= Page.NumberPerClick * DronesRequired)
            {
                Count = (BigInteger.Parse(Count) + Page.NumberPerClick).ToString();
                Page.MineralValue = (Page.MineralValue - Page.NumberPerClick * MineralsRequired);
                Page.VespeneValue = (Page.VespeneValue - Page.NumberPerClick * VespRequired);
                Page.Units[0].Count = (BigInteger.Parse(Page.Units[0].Count) - Page.NumberPerClick * DronesRequired).ToString();
            }
        }
        void onFirstInfestedBuy()
        {
            Page.Free.Reset();
            Boughtyet = true;
            Page.Units.Add(Page.Things.GetInfestedTerran());
            foreach (UpgradeRow upgrade in Page.Things.InfestedTerranUpgrades)
            {
                Page.Upgrades.Add(upgrade);
            }
            Page.Free.Set();
        }
        public void InfestedBaracksIncome()
        {
            while (Page.Running)
            {
                Thread.Sleep(IncomeSpeed);
                foreach (UnitRow Unit in Page.Units)
                {
                    if (Unit.Name == "Infested Terran")
                    {
                        Unit.Count = (BigInteger.Parse(Unit.Count) + BigInteger.Parse(Count) * 2).ToString();
                    }
                }
            }
        }
        void BuyAllFunction()
        {
            if (Page.MineralValue >= (BigInteger)NumberCanAfford * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)NumberCanAfford * (BigInteger)VespRequired && BigInteger.Parse(Page.Units[0].Count) >= NumberCanAfford * DronesRequired)
            {
                BigInteger numberCanAfford = NumberCanAfford;
                Count = (BigInteger.Parse(Count) + numberCanAfford).ToString();
                Page.MineralValue = (Page.MineralValue - numberCanAfford * MineralsRequired);
                Page.VespeneValue = (Page.VespeneValue - numberCanAfford * VespRequired);
                Page.Units[0].Count = (BigInteger.Parse(Page.Units[0].Count) - numberCanAfford * DronesRequired).ToString();
            }
        }
        void OnPropertyChanged(string propertyName)
        {
            var propertyChangedCallback = PropertyChanged;
            propertyChangedCallback?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void RemoveStructure(string StructureName)
        {
            for (int x = 0; x < Page.Structures.Count; x++)
            {
                if (Page.Structures[x].Name == StructureName)
                {
                    Page.Structures.RemoveAt(x);
                    break;
                }
            }
        }
        public void ExtractorBuyAll()
        {

            if (Page.MineralValue >= (BigInteger)NumberCanAfford * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)NumberCanAfford * (BigInteger)VespRequired && BigInteger.Parse(Page.Units[0].Count) >= NumberCanAfford * DronesRequired&& BigInteger.Parse(Count) + NumberCanAfford <= BigInteger.Parse(Page.Things.FindStructure("Hatchery").Count) * 2 + 2)
            {
                BigInteger numberCanAfford = NumberCanAfford;
                Count = (BigInteger.Parse(Count) + numberCanAfford).ToString();
                Page.MineralValue = (Page.MineralValue - numberCanAfford * MineralsRequired);
                Page.VespeneValue = (Page.VespeneValue - numberCanAfford * VespRequired);
                Page.Units[0].Count = (BigInteger.Parse(Page.Units[0].Count) - numberCanAfford * DronesRequired).ToString();
            }
            

        }
        public void ExtractorBuySelection()
        {
            Page.Free.WaitOne();
            if (Page.MineralValue >= (BigInteger)Page.NumberPerClick * (BigInteger)MineralsRequired && Page.VespeneValue >= (BigInteger)Page.NumberPerClick * (BigInteger)VespRequired && BigInteger.Parse(Page.Units[0].Count) >= Page.NumberPerClick * DronesRequired && BigInteger.Parse(Count)+Page.NumberPerClick <= BigInteger.Parse(Page.Things.FindStructure("Hatchery").Count) * 2 + 2)
            {
                Count = (BigInteger.Parse(Count) + Page.NumberPerClick).ToString();
                Page.MineralValue = (Page.MineralValue - Page.NumberPerClick * MineralsRequired);
                Page.VespeneValue = (Page.VespeneValue - Page.NumberPerClick * VespRequired);
                Page.Units[0].Count = (BigInteger.Parse(Page.Units[0].Count) - Page.NumberPerClick * DronesRequired).ToString();
            }
        }
        public string GetSaveString()
        {

            string temp = Name + "|" + Description + "|" + IncomeSpeed + "|" + MineralsPerRound + "|" + VespPerRound + "|" + LarvaPerRound + "|" + MineralsRequired + "|" + VespRequired + "|" + DronesRequired + "|" + ImagePath + "|" + Count + "|" + DronesPerRound + "|_";
            return temp;
        }
    }

}