using IdleSwarm.Droid.Classes;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IdleSwarm.Classes
{
    public class Things
    {
        private HiveViewModel page;
        private UpgradeFunctions UpgradeFunctions;
        public List<UpgradeRow> ZerglingUpgrades = new List<UpgradeRow>();
        public List<UpgradeRow> RoachUpgrades = new List<UpgradeRow>();
        public List<UpgradeRow> InfestedTerranUpgrades = new List<UpgradeRow>();

        public Things(HiveViewModel Page)
        {
            page = Page;
            UpgradeFunctions = new UpgradeFunctions(page);
        }

        public void SetupSpecialUpgrades()
        {
        }

        public UnitRow GetDrone()
        {
            UnitRow value = new UnitRow(page, "Drone", "Drone.png", 1, 0, 0, 50, 0, 1, 2000, "Gathers 1 Minerals every other second, and can be used to build Structures.", 1, 0);
            value.ButtonRight = new Command(value.DroneBuyAll);
            value.ButtonLeft = new Command(value.DroneBuySelection);
            value.Count = "10";
            value.OtherRestrictions = value.DroneOtherRestrictions;
            return value;
        }

        public UnitRow GetZergling()
        {
            UnitRow value = new UnitRow(page, "Zergling", "Zergling.PNG", 1, 0, 0, 30, 0, 1, 2000, "Harrasses other hives workers gathering their minerals. Morphs in pairs.", 1, 0);
            value.ButtonLeft = new Command(value.ZerglingBuySelection);
            value.ButtonRight = new Command(value.ZerglingBuyAll);
            value.OtherRestrictions = value.ZerglingOtherRestrictions;
            return value;
        }

        public UnitRow GetRoach()
        {
            UnitRow value = new UnitRow(page, "Roach", "Roach.PNG", 4, 0, 0, 75, 0, 1, 2000, "Steals another hives minerals from their zerglings. Brings home 4 minerals every other second.", 3, 0);
            value.OtherRestrictions = value.ZerglingOtherRestrictions;
            return value;
        }

        public UnitRow GetQueen()
        {
            UnitRow value = new UnitRow(page, "Queen", "Queen.PNG", 0, 0, 3, 20000, 0, 0, 3000, "Generates 1 Larva per second. Cost increases every Queen.", 1, 0);
            value.ButtonLeft = new Command(value.QueenBuySelection);
            value.ButtonLeft = new Command(value.QueenBuyAll);
            value.Count = "1";
            value.OtherRestrictions = value.QueenOtherRestrictions;
            value.IncreasePerBuy = 10;
            return value;
        }

        public UnitRow GetInfestedTerran()
        {
            UnitRow value = new UnitRow(page, "Infested Terran", "InfestedMarine.PNG", 2, 0, 0, 0, 0, 0, 2000, "Harrasses Workers. Slowly generated from the Infested Barracks 2 Minerals every other second.", 0, 0);
            value.Button1IsVisible = false;
            value.Button2IsVisible = false;
            return value;
        }

        public UnitRow GetOverlord()
        {
            UnitRow value = new UnitRow(page, "Overlord", "OverLord.PNG", 0, 0, 0, 500, 0, 1, 1000000, "Gives 50 Supply. supply is required for nearly all units. Cost increases each time.", 0, 0);
            value.SupplyProvided = 50;
            value.ButtonLeft = new Command(value.OverlordBuySelection);
            value.ButtonRight = new Command(value.OverlordBuyAll);
            value.IncreasePerBuy = 10;
            return value;
        }

        public StructureRow Extractor()
        {
            StructureRow value = new StructureRow(page, "Extractor", "Extractor.png", 0, 5, 0, 200, 0, 4, 10000, "Generates 1 vesp per second. takes 1 drone to create and 3 more to operate it. (costs 4 drones)");
            value.OverWriteIncomeFunction(new Command(value.ExtractorIncomeFunction));
            value.ButtonRight = new Command(value.ExtractorBuyAll);
            value.ButtonLeft = new Command(value.ExtractorBuySelection);
            return value;
        }

        public StructureRow CreepTumor()
        {
            StructureRow value = new StructureRow(page, "CreepTumor", "CreepTumor.PNG", 0, 0, 0,20000,2000,1, 10000, "Increases All army Mineral Generation rate by 1%. Cost increases by 10 times each time.");
            value.IncreasePerBuy = 9900;
            return value;
        }

        public StructureRow Hatchery()
        {
            StructureRow value = new StructureRow(page, "Hatchery", "Hatchery.png", 0, 0, 0, 800, 50, 10, 2000, "Increases Max Drone Count by 30 and Max Extractor Count by 2");
            value.IncreasePerBuy = 10;
            return value;
        }

        public StructureRow Evolution()
        {
            StructureRow value = new StructureRow(page, "EvolutionChamber", "EvolutionChamber.png", 0, 0, 0, 300, 0, 1, 1000000, "Unlocks New Upgrades on the upgrade section 100 times more expensive for each one.");
            value.Button1IsVisible = false;
            value.RightButtonText = "Build";
            value.ButtonRight = new Command(value.BuyEvolutionChamber);
            value.IncreasePerBuy = 9900;
            return value;
        }

        public StructureRow Spawning()
        {
            StructureRow value = new StructureRow(page, "Spawning Pool", "SpawningPool.png", 0, 0, 0, 1000, 100, 20, 1000000, "Unlocks/Allows you to build 1000 more batches of zerglings.");
            value.Button1IsVisible = false;
            value.RightButtonText = "Build";
            value.ButtonRight = new Command(value.BuySpawningPool);
            return value;
        }

        public StructureRow RoachWarren()
        {
            StructureRow value = new StructureRow(page, "Roach Warren", "RoachWarren.png", 0, 0, 0, 3000000, 10000, 10000, 1000000, "Unlocks roaches/ Allows you to build 500 more.");
            value.Button1IsVisible = false;
            value.RightButtonText = "Build";
            value.ButtonRight = new Command(value.BuyRoachWarren);
            return value;
        }

        public StructureRow InfestedBarracks()
        {
            StructureRow value = new StructureRow(page, "Infested Baracks", "InfestedBarracks.png", 0, 0, 0, 3000000, 1000, 100, 2000, "Unlocks and generates 1 infested terran per second.");
            value.OverWriteIncomeFunction(new Command(value.InfestedBaracksIncome));
            value.ButtonLeft = new Command(value.BuyIBSelectionFunction);
            value.ButtonRight = new Command(value.BuyIBAllFunction);

            return value;
        }

        public UnitRow FindUnit(string name)
        {
            foreach (UnitRow unit in page.Units)
            {
                if (unit.Name == name)
                {
                    return unit;
                }
            }
            return default(UnitRow);
        }

        public StructureRow FindStructure(string name)
        {
            foreach (StructureRow unit in page.Structures)
            {
                if (unit.Name == name)
                {
                    return unit;
                }
            }
            return default(StructureRow);
        }

        public UpgradeRow FindUpgrade(string name)
        {
            foreach (UpgradeRow unit in page.Upgrades)
            {
                if (unit.Name == name)
                {
                    return unit;
                }
            }
            return default(UpgradeRow);
        }

        public UpgradeRow GetUpgradeFromString(string name)
        {
            // | per feild _ per variable
            //UpgradeString
            //Name"
            switch (name)
            {
                case "Faster Extractors":
                    return
                    new UpgradeRow(page,
                    "Faster Extractors",
                    "VespeneHarvestors.PNG",
                    10000, 20000, 50,
                    "Using 5 drones as testing biomass extractor effeciency can be improved doubleing Vesp Production!",
                    new Command(UpgradeFunctions.FasterExtractors),
                    2);
                case "Larger Tanks":
                    return
                    new UpgradeRow(page,
                    "Larger Tanks",
                    "FasterExtractors.PNG",
                    100000, 200000, 500,
                    "Using 50 drones as testing biomass extractor effeciency can be improved doubleing Vesp Production!",
                    new Command(UpgradeFunctions.LargerTanks),
                    3);
                case "Hardened Mandibles":
                    return
                    new UpgradeRow(page,
                    "Hardened Mandibles",
                    "StrongerMandibles.png",
                    1000, 2000, 5,
                    "Using 5 drones as testing biomass we can increase the density of our drones mandibles Doubles mineral generation!",
                    new Command(UpgradeFunctions.HardenedMandibles),
                    1);

                case "Super Sonic Drones":
                    return
                        new UpgradeRow(page,
                        "Super Sonic Drones",
                         "SuperSonicDrones.png",
                          10000, 20000, 50,
                         "Using 50 drones as testing biomass  we can increase the density of our drones Leg Muscles Doubling Mineral Generation!",
                          new Command(UpgradeFunctions.SuperSonicDrones),
                         1);

                case "Injection Infusion":

                    return
                         new UpgradeRow(page,
                         "Injection Infusion",
                         "Larva.png",
                         10000, 20000, 50,
                         "Using 50 drones as testing biomass We can infuse our queens with a better Larva Inject Doubleing Larva Generation!",
                         new Command(UpgradeFunctions.InjectionInfusion),
                         1);

                case "Bigger Overlords":
                    return
                         new UpgradeRow(page,
                         "Bigger Overlords",
                         "OverLordUpgrade.png",
                         10000, 20000, 50,
                         "Using 50 drones as testing biomass all new overlords can be outfitted with larger sacs, doubling supply provided!",
                         new Command(UpgradeFunctions.BiggerOverlords),
                         1);

                case "Lairs":

                    return
                         new UpgradeRow(page,
                         "Lairs",
                         "LairUpgrade.png",
                         10000, 20000, 50,
                         "Sacrificing 50 drones and some we can create deeper lairs as apposed to our Hatcheries allowing them to support 1 more extractor and 15 more drones!",
                         new Command(UpgradeFunctions.Lairs),
                         1);

                case "Hives":
                    return
                         new UpgradeRow(page,
                         "Hives",
                         "LairUpgrade.png",
                         100000, 200000, 500,
                         "Sacrificing 500 drones and some we can create deeper Hives as apposed to our Lairs allowing them to support 1 more extractor and 15 more drones!",
                         new Command(UpgradeFunctions.Hives),
                         2);

                case "Energized Injection":
                    return
                         new UpgradeRow(page,
                         "Energized Injection",
                         "LarvaUpgrade.PNG",
                         100000, 200000, 500,
                         "Using 500 drones as testing biomass We can Increase the energy of our queens, Doubling Larva Generation!",
                         new Command(UpgradeFunctions.EnergizedInjection),
                         1);

                case "Quicker Gather":
                    return
                         new UpgradeRow(page,
                         "Quicker Gather",
                         "GatherUpgrade.png",
                         1000000, 2000000, 500,
                         "Using 500 drones as testing biomass We can scare the drones into going faster, Doubling Mineral Generation!",
                         new Command(UpgradeFunctions.QuickerGather),
                         1);

                case "Metabolic Boost":
                    return
                             new UpgradeRow(page,
                             "Metabolic Boost",
                             "MetabolicBoost.png",
                             1000000, 2000000, 500,
                             "Using 500 drones as testing biomass We can fuse wings to the Zerglings Doubling Mineral Generation!",
                             new Command(UpgradeFunctions.MetabolicBoost),
                             2);

                case "Aggressive Mutation":
                    return
                         new UpgradeRow(page,
                         "Aggressive Mutation",
                         "AggressiveMutation.png",
                         10000000, 20000000, 5000,
                         "Using 5000 drones as testing biomass We can Increase adrenaline produced creating more agressive zerglings Doubling the number of workers harrassed!",
                         new Command(UpgradeFunctions.AggressiveMutation),
                         3);

                case "More Effecient Zerglings":
                    return
                         new UpgradeRow(page,
                         "More Effecient Zerglings",
                         "ZerglingUpgrade1.png",
                         100000, 200000, 50,
                         "Using 50 drones as testing biomass We can Increase Zergling Effeciency increaseing the batch from 2 to 3 per Morph",
                         new Command(UpgradeFunctions.MoreEffecientZerglings),
                         2);

                case "Tunnling Claws":
                    return
                         new UpgradeRow(page,
                         "Tunnling Claws",
                         "TunnlingClaws.png",
                         10000000, 2000000, 500,
                         "Using 500 drones as testing biomass Our roaches develop the ability to tunnle through the ground. Doubling Mineral Generation!",
                         new Command(UpgradeFunctions.TunnlingClaws),
                         2);

                case "Purpler Roaches":
                    return
                         new UpgradeRow(page,
                         "Purpler Roaches",
                         "PurplerRoaches.PNG",
                         100000000, 20000000, 5000,
                         "Using 5000 drones as testing biomass Our roaches Become more purple therefore becomeing better roaches Doubles Mineral Generation!",
                         new Command(UpgradeFunctions.PurplerRoaches),
                         3);

                case "More Purple Roaches":
                    return
                         new UpgradeRow(page,
                         "More Purple Roaches",
                         "MorePurpleRoaches.PNG",
                         100000000, 20000000, 5000,
                         "Using 5000 drones as testing biomass We create the only thing better then a purple roach; more purple roaches Doubles Mineral Generation!",
                         new Command(UpgradeFunctions.MorePurpleRoaches),
                         3);

                case "Strong Spikes":
                    return
                         new UpgradeRow(page,
                         "Strong Spikes",
                         "StrongSpikes.PNG",
                         100000000, 20000000, 5000,
                         "Using 5000 drones as testing biomass we can Strengthen the spieks infested terrans fire Doubles Mineral Generation!",
                         new Command(UpgradeFunctions.StrongSpikes),
                         3);

                case "Stronger Spikes":
                    return
                         new UpgradeRow(page,
                         "Stronger Spikes",
                         "StrongerSpikes.PNG",
                         1000000000, 200000000, 50000,
                         "Using 50000 drones as testing biomass we can Strengthen the spikes infested terrans fire even more Doubles Mineral Generation!",
                         new Command(UpgradeFunctions.StrongerSpikes),
                         3);

                case "Explosive Spikes":
                    return
                         new UpgradeRow(page,
                         "Explosive Spikes",
                         "ExplosiveSpikes.PNG",
                         1000000000, 200000000, 50000,
                         "Using 50000 drones as testing biomass we can cause the spikes to expload on impact, Doubles Mineral Generation!",
                         new Command(UpgradeFunctions.ExplosiveSpikes),
                         4);
                default:
                    return default(UpgradeRow);
            }
        }

        public bool StructureExists(string name)
        {
            foreach (StructureRow unit in page.Structures)
            {
                if (unit.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UpgradeExists(string name)
        {
            foreach (UpgradeRow unit in page.Upgrades)
            {
                if (unit.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public bool UnitExists(string name)
        {
            foreach (UnitRow unit in page.Units)
            {
                if (unit.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddSpecialUpgrades()
        {
            //zergling upgrades
            ZerglingUpgrades.Add(
                 new UpgradeRow(page,
                 "Metabolic Boost",
                 "MetabolicBoost.png",
                 1000000, 2000000, 500,
                 "Using 500 drones as testing biomass and some Minerals/vesp as Catalysts We can fuse wings to the Zerglings allowing faster mineral generation (+50) requires 2 chambers",
                 new Command(UpgradeFunctions.MetabolicBoost),
                 2));
            ZerglingUpgrades.Add(
                 new UpgradeRow(page,
                 "Aggressive Mutation",
                 "AggressiveMutation.png",
                 10000000, 20000000, 5000,
                 "Using 5000 drones as testing biomass and some Minerals/vesp as Catalysts We can Increase adrenaline produced creating more agressive zerglings (+50 Minerals per second) requires 3 chambers",
                 new Command(UpgradeFunctions.AggressiveMutation),
                 3));
            ZerglingUpgrades.Add(
                 new UpgradeRow(page,
                 "More Effecient Zerglings",
                 "ZerglingUpgrade1.png",
                 100000, 200000, 50,
                 "Using 50 drones as testing biomass and some Minerals/vesp as Catalysts We can Increase Zergling Effeciency halving the supply they require requires 2 chambers",
                 new Command(UpgradeFunctions.MoreEffecientZerglings),
                 2));
            //zergling upgrades
            //RoachUpgrades
            RoachUpgrades.Add(
                 new UpgradeRow(page,
                 "Tunnling Claws",
                 "TunnlingClaws.png",
                 10000000, 2000000, 500,
                 "Using 500 drones as testing biomass and some Minerals/vesp as Catalysts Our roaches develop the ability to tunnle through the ground. (Doubles Mineral Generation)requires 2 chambers",
                 new Command(UpgradeFunctions.TunnlingClaws),
                 2));
            RoachUpgrades.Add(
                 new UpgradeRow(page,
                 "Purpler Roaches",
                 "PurplerRoaches.PNG",
                 100000000, 20000000, 5000,
                 "Using 5000 drones as testing biomass and some Minerals/vesp as Catalysts Our roaches Become more purple therefore becomeing better roaches (Doubles Mineral Generation) requires 3 chambers",
                 new Command(UpgradeFunctions.PurplerRoaches),
                 3));
            RoachUpgrades.Add(
                 new UpgradeRow(page,
                 "More Purple Roaches",
                 "MorePurpleRoaches.PNG",
                 100000000, 20000000, 5000,
                 "Using 5000 drones as testing biomass and some Minerals/vesp as Catalysts We create the only thing better then a purple roach; more purple roaches (Doubles Mineral Generation) requires 3 chambers",
                 new Command(UpgradeFunctions.MorePurpleRoaches),
                 3));
            //RoachUpgrades
            //InfestedTerranUpgrades
            InfestedTerranUpgrades.Add(
                 new UpgradeRow(page,
                 "Strong Spikes",
                 "StrongSpikes.PNG",
                 100000000, 20000000, 5000,
                 "Using 5000 drones as testing biomass and some Minerals/vesp as Catalysts we can Strengthen the spieks infested terrans fire(doubles infested terran mineral generation",
                 new Command(UpgradeFunctions.StrongSpikes),
                 3));
            InfestedTerranUpgrades.Add(
                 new UpgradeRow(page,
                 "Stronger Spikes",
                 "StrongerSpikes.PNG",
                 1000000000, 200000000, 50000,
                 "Using 50000 drones as testing biomass and some Minerals/vesp as Catalysts we can Strengthen the spikes infested terrans fire even more(doubles infested terran mineral generation",
                 new Command(UpgradeFunctions.StrongerSpikes),
                 3));
            InfestedTerranUpgrades.Add(
                 new UpgradeRow(page,
                 "Explosive Spikes",
                 "ExplosiveSpikes.PNG",
                 1000000000, 200000000, 50000,
                 "Using 50000 drones as testing biomass and some Minerals/vesp as Catalysts we can cause the spieks to expload on impact increasing effeciency(doubles infested terran mineral generation",
                 new Command(UpgradeFunctions.ExplosiveSpikes),
                 4));
            //InfestedTerranUpgradesz
        }
    }
}