﻿using IdleSwarm.Classes;
using IdleSwarm.Droid.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace IdleSwarm
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public Things Things;
        public ManualResetEvent Free = new ManualResetEvent(false);
        ImageSource _backgroundImage = "DarkerBackground.jpg";
        public ImageSource BackgroundImage
        {
            get { return _backgroundImage; }
            set
            {
                _backgroundImage = value;
                OnPropertyChanged(nameof(BackgroundImage));
            }
        }
        public int Width;
        public ListView DisplayView { get; set; }
        int Selected = 0;
        public int ImageHeight { get; set; }
        public ICommand UnitsCommand { get; set; }
        public ICommand StructuresCommand { get; set; }
        public ICommand UpgradesCommand { get; set; }
        public bool Running = true;
        ImageSource _mineralImage = "MineralImage.jpg";
        public ImageSource MineralImage
        {
            get { return _mineralImage; }
            set
            {
                _mineralImage = value;
                OnPropertyChanged(nameof(MineralImage));
            }
        }
        ImageSource _vespImage = "VespeneGas.gif";
        public ImageSource VespImage
        {
            get { return _vespImage; }
            set
            {
                _vespImage = value;
                OnPropertyChanged(nameof(VespImage));
            }
        }
        ImageSource _larvaImage = "Larva.png";
        public ImageSource LarvaImage
        {
            get { return _larvaImage; }
            set
            {
                _larvaImage = value;
                OnPropertyChanged(nameof(LarvaImage));
            }
        }
        ImageSource _supplyImage = "OverLord.PNG";
        public ImageSource SupplyImage
        {
            get { return _supplyImage; }
            set
            {
                _supplyImage = value;
                OnPropertyChanged(nameof(SupplyImage));
            }
        }
        string _mineralCount = "5000";
        public string MineralCount
        {
            get { return _mineralCount; }
            set
            {
                _mineralCount = value;
                RecalculateCanAfford();
                OnPropertyChanged(nameof(MineralCount));
            }
        }
        public BigInteger MineralValue
        {
            get
            {
                return BigInteger.Parse(MineralCount);
            }
            set
            {
                MineralCount = value.ToString();
            }
        }
        string _vespeneCount = "0";
        public string VespeneCount
        {
            get { return _vespeneCount; }
            set
            {
                _vespeneCount = value;
                RecalculateCanAfford();
                OnPropertyChanged(nameof(VespeneCount));
            }
        }
        public BigInteger VespeneValue
        {
            get
            {
                return BigInteger.Parse(VespeneCount);
            }
            set
            {
                VespeneCount = value.ToString();
            }
        }
        string _larvaCount = "10";
        public string LarvaCount
        {
            get { return _larvaCount; }
            set
            {
                _larvaCount = value;
                RecalculateCanAfford();
                OnPropertyChanged(nameof(LarvaCount));
            }
        }
        string _supplyCount = "50";
        public string SupplyCount
        {
            get { return _supplyCount; }
            set
            {
                _supplyCount = value;
                OnPropertyChanged(nameof(SupplyCount));
            }
        }
        public BigInteger SupplyValue
        {
            get
            {
                return BigInteger.Parse(SupplyCount);
            }
            set
            {
                SupplyCount = value.ToString();
            }
        }
        public BigInteger LarvaValue
        {
            get
            {
                return BigInteger.Parse(LarvaCount);
            }
            set
            {
                LarvaCount = value.ToString();
            }
        }
        int _numberPerClick = 1;
        public int NumberPerClick
        {
            get { return _numberPerClick; }
            set
            {
                _numberPerClick = value;
                OnPropertyChanged(nameof(NumberPerClick));
            }
        }
        public ObservableCollection<UnitRow> Units { get; set; }
        public ObservableCollection<StructureRow> Structures { get; set; }
        public ObservableCollection<UpgradeRow> Upgrades { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        Thread SaveThread;
        public MainPageViewModel(ListView displayView)
        {
            Things = new Things(this);
            Width = (int)(DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density);
            ImageHeight = (int)(Width * 0.2886 / 2);
            DisplayView = displayView;
            UpgradesCommand = new Command(UpgradeOnClick);
            StructuresCommand = new Command(StructureOnClick);
            UnitsCommand = new Command(UnitOnClick);
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Save.txt");
            if (File.Exists(fileName))
            {
                //File.Delete(fileName);
                Load(fileName);
            }
            else
            {
                GenerateUnits();
                GenerateStructures();
                GenerateUpgrades();
            }
            SaveThread = new Thread(new ThreadStart(SaveThreadFunction));
            SaveThread.Start();
            UnitOnClick();
            Free.Set();
            RecalculateCanAfford();
        }
        void Load(string filepath)
        {
            // | per feild _ per variable
            //UpgradeString
            //Name + "|" + Description + "|" + MineralsRequired + "|" + VespRequired + "|" + DronesRequired + "|" + EvolutionChambersRequired + "|"
            //StructureString
            //Name + "|"+Description+"|" + IncomeSpeed + "|" + MineralsPerRound + "|" + VespPerRound + "|" + LarvaPerRound + "|" + MineralsRequired + "|" + VespRequired + "|" + DronesRequired + "|" + ImagePath + "|" + Count + "|"+DronesPerRound +"|"
            //UnitString
            // Name + "|" + SupplyProvided + "|" + SupplyRequired + "|" + Description + "|" + IncomeSpeed + "|" + MineralsPerRound + "|" + VespPerRound + "|" + LarvaPerRound + "|" + MineralsRequired + "|" + VespRequired + "|" + LarvaRequired + "|" + ImagePath + "|" + Count + "|";
            Units = new ObservableCollection<UnitRow>();
            Structures = new ObservableCollection<StructureRow>();
            Upgrades = new ObservableCollection<UpgradeRow>();
            string text = File.ReadAllText(filepath);
            string CurrentData = "";
            int NumberSeenAlready = 0;
            for (int CurrentChar = 0; CurrentChar < text.Length; CurrentChar++)
            {
                if (text[CurrentChar] == '_')
                {
                    switch (NumberSeenAlready)
                    {
                        case 0:
                            MineralValue = BigInteger.Parse(CurrentData);
                            break;

                        case 1:
                            VespeneValue = BigInteger.Parse(CurrentData);
                            break;

                        case 2:
                            LarvaValue = BigInteger.Parse(CurrentData);
                            break;

                        case 3:
                            SupplyValue = BigInteger.Parse(CurrentData);
                            break;

                        case 4:
                            if (CurrentData == "")
                            {
                            }
                            else
                            {
                                Units.Add(new UnitRow(this, CurrentData));
                                NumberSeenAlready--;

                            }
                            break;

                        case 5:
                            if (CurrentData == "")
                            {

                            }
                            else
                            {
                                Structures.Add(new StructureRow(this, CurrentData));
                                NumberSeenAlready--;

                            }
                            break;

                        case 6:
                            if (CurrentData == "")
                            {

                            }
                            else
                            {
                                Upgrades.Add(Things.GetUpgradeFromString(CurrentData));
                                NumberSeenAlready--;
                            }
                            break;
                    }
                    NumberSeenAlready++;
                    CurrentData = "";
                }
                else
                {
                    CurrentData += text[CurrentChar];
                }
            }
            UpgradeFunctions UpgradeFunctions = new UpgradeFunctions(this);
            Things.ZerglingUpgrades.Add(
                     new UpgradeRow(this,
                     "Metabolic Boost",
                     "MetabolicBoost.png",
                     1000000, 2000000, 500,
                     "Using 500 drones as testing biomass and some Minerals/vesp as Catalysts We can fuse wings to the Zerglings allowing faster mineral generation (+50) requires 2 chambers",
                     new Command(UpgradeFunctions.MetabolicBoost),
                     2));
            Things.ZerglingUpgrades.Add(
                 new UpgradeRow(this,
                 "Aggressive Mutation",
                 "AggressiveMutation.png",
                 10000000, 20000000, 5000,
                 "Using 5000 drones as testing biomass and some Minerals/vesp as Catalysts We can Increase adrenaline produced creating more agressive zerglings (+50 Minerals per second) requires 3 chambers",
                 new Command(UpgradeFunctions.AgressiveMutation),
                 3));
            Things.ZerglingUpgrades.Add(
                 new UpgradeRow(this,
                 "More Effecient Zerglings",
                 "ZerglingUpgrade1.png",
                 100000, 200000, 50,
                 "Using 50 drones as testing biomass and some Minerals/vesp as Catalysts We can Increase Zergling Effeciency halving the supply they require requires 2 chambers",
                 new Command(UpgradeFunctions.MoreEffecientZerglings),
                 2));
            //zergling upgrades
            //RoachUpgrades
            Things.RoachUpgrades.Add(
                 new UpgradeRow(this,
                 "Tunnling Claws",
                 "TunnlingClaws.png",
                 10000000, 2000000, 500,
                 "Using 500 drones as testing biomass and some Minerals/vesp as Catalysts Our roaches develop the ability to tunnle through the ground. (Doubles Mineral Generation)requires 2 chambers",
                 new Command(UpgradeFunctions.TunnlingClaws),
                 2));
            Things.RoachUpgrades.Add(
                 new UpgradeRow(this,
                 "Purpler Roaches",
                 "PurplerRoaches.PNG",
                 100000000, 20000000, 5000,
                 "Using 5000 drones as testing biomass and some Minerals/vesp as Catalysts Our roaches Become more purple therefore becomeing better roaches (Doubles Mineral Generation) requires 3 chambers",
                 new Command(UpgradeFunctions.PurplerRoaches),
                 3));
            Things.RoachUpgrades.Add(
                 new UpgradeRow(this,
                 "More Purple Roaches",
                 "MorePurpleRoaches.PNG",
                 100000000, 20000000, 5000,
                 "Using 5000 drones as testing biomass and some Minerals/vesp as Catalysts We create the only thing better then a purple roach; more purple roaches (Doubles Mineral Generation) requires 3 chambers",
                 new Command(UpgradeFunctions.MorePurpleRoaches),
                 3));
            //RoachUpgrades
            //InfestedTerranUpgrades
            Things.InfestedTerranUpgrades.Add(
                 new UpgradeRow(this,
                 "Strong Spikes",
                 "StrongSpikes.PNG",
                 100000000, 20000000, 5000,
                 "Using 5000 drones as testing biomass and some Minerals/vesp as Catalysts we can Strengthen the spieks infested terrans fire(doubles infested terran mineral generation",
                 new Command(UpgradeFunctions.StrongSpikes),
                 3));
            Things.InfestedTerranUpgrades.Add(
                 new UpgradeRow(this,
                 "Stronger Spikes",
                 "StrongerSpikes.PNG",
                 1000000000, 200000000, 50000,
                 "Using 50000 drones as testing biomass and some Minerals/vesp as Catalysts we can Strengthen the spikes infested terrans fire even more(doubles infested terran mineral generation",
                 new Command(UpgradeFunctions.StrongerSpikes),
                 3));
            Things.InfestedTerranUpgrades.Add(
                 new UpgradeRow(this,
                 "Explosive Spikes",
                 "ExplosiveSpikes.PNG",
                 1000000000, 200000000, 50000,
                 "Using 50000 drones as testing biomass and some Minerals/vesp as Catalysts we can cause the spieks to expload on impact increasing effeciency(doubles infested terran mineral generation",
                 new Command(UpgradeFunctions.ExplosiveSpikes),
                 4));
            //InfestedTerranUpgradesz

        }
        void SaveThreadFunction()
        {
            Thread.Sleep(5000);
            while (Running)
            {
                Free.WaitOne();
                SaveData(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Save.txt"));
                Thread.Sleep(5000);
            }
        }
        void SaveData(string filepath)
        {
            // | per feild _ per variable
            //UpgradeString
            //Name + "|" + Description + "|" + MineralsRequired + "|" + VespRequired + "|" + DronesRequired + "|" + EvolutionChambersRequired + "|"
            //StructureString
            //Name + "|"+Description+"|" + IncomeSpeed + "|" + MineralsPerRound + "|" + VespPerRound + "|" + LarvaPerRound + "|" + MineralsRequired + "|" + VespRequired + "|" + DronesRequired + "|" + ImagePath + "|" + Count + "|"+DronesPerRound +"|"
            //UnitString
            // Name + "|" + SupplyProvided + "|" + SupplyRequired + "|" + Description + "|" + IncomeSpeed + "|" + MineralsPerRound + "|" + VespPerRound + "|" + LarvaPerRound + "|" + MineralsRequired + "|" + VespRequired + "|" + LarvaRequired + "|" + ImagePath + "|" + Count + "|";

            string text = "";
            text += MineralValue + "_" + VespeneValue + "_" + LarvaValue + "_" + SupplyValue + "_";
            foreach (UnitRow Unit in Units)
            {
                Free.WaitOne();
                text += Unit.GetSaveString();
            }
            text += "_";
            foreach (StructureRow Structure in Structures)
            {
                text += Structure.GetSaveString();
            }
            text += "_";
            foreach (UpgradeRow Upgrade in Upgrades)
            {
                text += Upgrade.GetSaveString();
            }
            text += "_";
            if (File.Exists(filepath))
                File.Delete(filepath);
            File.WriteAllText(filepath, text);
        }
        ~MainPageViewModel()
        {
            Running = false;
        }
        void RecalculateCanAfford()
        {
            if (Free.WaitOne(0))
            {

                for (int Current = 0; Current < Units.Count; Current++)
                {
                    if (Current == 5)
                    {

                    }
                    else
                    {
                        if (Units[Current].Name == "Drone")
                        {

                            BigInteger x = 10000000000;
                            if (Units[Current].MineralsRequired != 0)
                                x = (BigInteger)(MineralValue / Units[Current].MineralsRequired);
                            if (Units[Current].LarvaRequired != 0)
                                if (x > (BigInteger)(LarvaValue / Units[Current].LarvaRequired))
                                    x = (BigInteger)(LarvaValue / Units[Current].LarvaRequired);
                            if (Units[Current].VespRequired != 0)
                                if (x > (BigInteger)(VespeneValue / Units[Current].VespRequired))
                                    x = (BigInteger)(VespeneValue / Units[Current].VespRequired);
                            if (Units[Current].SupplyRequired != 0)
                                if (x > (BigInteger)((SupplyValue) / Units[Current].SupplyRequired))
                                    x = (BigInteger)((SupplyValue) / Units[Current].SupplyRequired);
                            if (x > (BigInteger.Parse(Things.FindStructure("Hatchery").Count) * 30 + 30) - (BigInteger.Parse(Units[Current].Count)))
                                x = (BigInteger.Parse(Things.FindStructure("Hatchery").Count) * 30 + 30) - (BigInteger.Parse(Units[Current].Count));

                            Units[Current].NumberCanAfford = x;
                        }
                        else
                        {
                            if (Units[Current].Name == "Queen")
                            {

                                BigInteger x = 10000000000;
                                if (Units[Current].MineralsRequired != 0)
                                    x = (BigInteger)(MineralValue / Units[Current].MineralsRequired);
                                if (Units[Current].LarvaRequired != 0)
                                    if (x > (BigInteger)(LarvaValue / Units[Current].LarvaRequired))
                                        x = (BigInteger)(LarvaValue / Units[Current].LarvaRequired);
                                if (Units[Current].VespRequired != 0)
                                    if (x > (BigInteger)(VespeneValue / Units[Current].VespRequired))
                                        x = (BigInteger)(VespeneValue / Units[Current].VespRequired);
                                if (Units[Current].SupplyRequired != 0)
                                    if (x > (BigInteger)((SupplyValue) / Units[Current].SupplyRequired))
                                        x = (BigInteger)((SupplyValue) / Units[Current].SupplyRequired);
                                if (x > (BigInteger.Parse(Things.FindStructure("Hatchery").Count) + 1) - (BigInteger.Parse(Units[Current].Count)))
                                    x = (BigInteger.Parse(Things.FindStructure("Hatchery").Count) + 1) - (BigInteger.Parse(Units[Current].Count));

                                Units[Current].NumberCanAfford = x;
                            }
                            else
                            {

                                BigInteger x = 10000000000;
                                if (Units[Current].MineralsRequired != 0)
                                    x = (BigInteger)(MineralValue / Units[Current].MineralsRequired);
                                if (Units[Current].LarvaRequired != 0)
                                    if (x > (BigInteger)(LarvaValue / Units[Current].LarvaRequired))
                                        x = (BigInteger)(LarvaValue / Units[Current].LarvaRequired);
                                if (Units[Current].VespRequired != 0)
                                    if (x > (BigInteger)(VespeneValue / Units[Current].VespRequired))
                                        x = (BigInteger)(VespeneValue / Units[Current].VespRequired);
                                if (Units[Current].SupplyRequired != 0)
                                    if (x > (BigInteger)((SupplyValue) / Units[Current].SupplyRequired))
                                        x = (BigInteger)((SupplyValue) / Units[Current].SupplyRequired);
                                Units[Current].NumberCanAfford = x;
                            }
                        }
                    }
                }
                for (int Current = 0; Current < Structures.Count; Current++)
                {
                    if (Structures[Current].Name == "EvolutionChamber" || Structures[Current].Name == "Spawning Pool" || Structures[Current].Name == "Roach Warren")
                    {
                        BigInteger x = 10000000;
                        if (Structures[Current].MineralsRequired != 0)
                            x = (BigInteger)(MineralValue / Structures[Current].MineralsRequired);
                        if (Structures[Current].DronesRequired != 0)
                            if (x > BigInteger.Parse(Units[0].Count) / Structures[Current].DronesRequired)
                                x = BigInteger.Parse(Units[0].Count) / Structures[Current].DronesRequired;
                        if (Structures[Current].VespRequired != 0)
                            if (x > (BigInteger)(VespeneValue / Structures[Current].VespRequired))
                                x = (BigInteger)(VespeneValue / Structures[Current].VespRequired);
                        if (x > 0)
                        {
                            Structures[Current].Button2IsVisible = true;
                        }
                        else
                        {
                            Structures[Current].Button2IsVisible = false;

                        }
                    }
                    else
                    {
                        if (Structures[Current].Name == "Extractor")
                        {
                            BigInteger x = 10000000;
                            if (Structures[Current].MineralsRequired != 0)
                                x = (BigInteger)(MineralValue / Structures[Current].MineralsRequired);
                            if (Structures[Current].DronesRequired != 0)
                                if (x > BigInteger.Parse(Units[0].Count) / Structures[Current].DronesRequired)
                                    x = BigInteger.Parse(Units[0].Count) / Structures[Current].DronesRequired;
                            if (Structures[Current].VespRequired != 0)
                                if (x > (BigInteger)(VespeneValue / Structures[Current].VespRequired))
                                    x = (BigInteger)(VespeneValue / Structures[Current].VespRequired);
                            if (x > ((BigInteger.Parse(Things.FindStructure("Hatchery").Count) * 2 + 2) - (BigInteger.Parse(Structures[Current].Count))))
                                x = (BigInteger.Parse(Things.FindStructure("Hatchery").Count) * 2 + 2) - (BigInteger.Parse(Structures[Current].Count));
                            Structures[Current].NumberCanAfford = x;
                        }
                        else
                        {
                            BigInteger x = 10000000;
                            if (Structures[Current].MineralsRequired != 0)
                                x = (BigInteger)(MineralValue / Structures[Current].MineralsRequired);
                            if (Structures[Current].DronesRequired != 0)
                                if (x > BigInteger.Parse(Units[0].Count) / Structures[Current].DronesRequired)
                                    x = BigInteger.Parse(Units[0].Count) / Structures[Current].DronesRequired;
                            if (Structures[Current].VespRequired != 0)
                                if (x > (BigInteger)(VespeneValue / Structures[Current].VespRequired))
                                    x = (BigInteger)(VespeneValue / Structures[Current].VespRequired);
                            Structures[Current].NumberCanAfford = x;
                        }
                    }
                }
                for (int Current = 0; Current < Upgrades.Count; Current++)
                {
                    bool x = true;
                    if (Upgrades[Current].MineralsRequired != 0)
                        if ((0 > MineralValue - Upgrades[Current].MineralsRequired))
                            x = false;
                    if (Upgrades[Current].DronesRequired != 0)
                        if (0 > BigInteger.Parse(Units[0].Count) - Upgrades[Current].DronesRequired)
                            x = false;
                    if (Upgrades[Current].VespRequired != 0)
                        if (0 > BigInteger.Parse(VespeneCount) - Upgrades[Current].VespRequired)
                            x = false;
                    if (Upgrades[Current].EvolutionChambersRequired != 0)
                        if (0 > BigInteger.Parse(Structures[2].Count) - Upgrades[Current].EvolutionChambersRequired)
                            x = false;
                    Upgrades[Current].CanAfford = x;
                }
            }

        }
        void GenerateStructures()
        {
            Structures = new ObservableCollection<StructureRow>();
            Structures.Add(Things.Extractor());
            Structures.Add(Things.Hatchery());
            Structures.Add(Things.Evolution());
            Structures.Add(Things.Spawning());
            Structures.Add(Things.RoachWarren());
            Structures.Add(Things.InfestedBarracks());
        }
        void GenerateUnits()
        {
            Units = new ObservableCollection<UnitRow>();
            Units.Add(Things.GetDrone());
            Units.Add(Things.GetQueen());
            Units.Add(Things.GetOverlord());
        }
        void GenerateUpgrades()
        {
            UpgradeFunctions UpgradeFunctions = new UpgradeFunctions(this);
            //normal upgrades
            Upgrades = new ObservableCollection<UpgradeRow>();
            Upgrades.Add(Things.GetUpgradeFromString("Hardened Mandibles"));
            Upgrades.Add(Things.GetUpgradeFromString("Super Sonic Drones"));
            Upgrades.Add(Things.GetUpgradeFromString("Injection Infusion"));
            Upgrades.Add(Things.GetUpgradeFromString("Bigger Overlords"));
            Upgrades.Add(Things.GetUpgradeFromString("Lairs"));
            Upgrades.Add(Things.GetUpgradeFromString("Hives"));
            Upgrades.Add(Things.GetUpgradeFromString("Energized Injection"));
            Upgrades.Add(Things.GetUpgradeFromString("Quicker Gather"));
            Upgrades.Add(Things.GetUpgradeFromString("Hives"));
            Upgrades.Add(Things.GetUpgradeFromString("Hives"));
            Things.AddSpecialUpgrades();

        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var propertyChangedCallback = PropertyChanged;
            propertyChangedCallback?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void StructureOnClick()
        {
            DisplayView.ItemsSource = Structures;
            Selected = 1;
        }

        void UnitOnClick()
        {
            DisplayView.ItemsSource = Units;
            Selected = 0;
        }

        void UpgradeOnClick()
        {
            DisplayView.ItemsSource = Upgrades;
            Selected = 2;
        }



    }

}

