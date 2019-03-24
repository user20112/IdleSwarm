using IdleSwarm.Droid.Classes;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IdleSwarm.Classes
{
    public class Things
    {
        MainPageViewModel page;
        UpgradeFunctions UpgradeFunctions;
        public List<UpgradeRow> ZerglingUpgrades = new List<UpgradeRow>();
        public List<UpgradeRow> RoachUpgrades = new List<UpgradeRow>();
        public List<UpgradeRow> InfestedTerranUpgrades = new List<UpgradeRow>();
        public Things(MainPageViewModel Page)
        {
            page = Page;
            UpgradeFunctions = new UpgradeFunctions(page);
        }
        public void SetupSpecialUpgrades()
        {

        }
        public UnitRow GetDrone()
        {
            UnitRow value = new UnitRow(page, "Drone", "Drone.png", 1, 0, 0, 50, 0, 1, 2000, "Gathers 1 Minerals per 5 seconds and can be used to build Structures.", 1, 0);
            value.ButtonRight = new Command(value.DroneBuyAll);
            value.ButtonLeft = new Command(value.DroneBuySelection);
            return value;
        }
        public UnitRow GetZergling()
        {
            UnitRow value = new UnitRow(page, "Zergling", "Zergling.PNG", 1, 0, 0, 30, 0, 1, 2000, "Harrasses others Hives workers and takes 50 of their minerals per second", 1, 0);
            value.ButtonLeft = new Command(value.ZerglingBuySelection);
            value.ButtonRight = new Command(value.ZerglingBuyAll);
            return value;
        }
        public UnitRow GetRoach()
        {
            UnitRow value = new UnitRow(page, "Roach", "Roach.PNG", 4, 0, 0, 75, 0, 1, 2000, "Harrasses others Hives Zerglings and takes 250 of their minerals per second", 3, 0);
            return value;
        }
        public UnitRow GetQueen()
        {
            UnitRow value = new UnitRow(page, "Queen", "Queen.PNG", 0, 0, 3, 150, 0, 0, 3000, "Generates 1 Larva per second.", 1, 0);
            value.ButtonLeft = new Command(value.QueenBuySelection);
            value.ButtonLeft = new Command(value.QueenBuyAll);
            return value;
        }
        public UnitRow GetInfestedTerran()
        {
            UnitRow value = new UnitRow(page, "Infested Terran", "InfestedMarine.PNG", 25, 0, 0, 0, 0, 0, 2000, "Created by Infestation Pits. Very dumb generates Minerals slower than zerglings but is passivly generated. (2 minerals)", 0, 0);
            value.Button1IsVisible = false;
            value.Button2IsVisible = false;
            return value;
        }
        public UnitRow GetOverlord()
        {
            UnitRow value = new UnitRow(page, "Overlord", "OverLord.PNG", 0, 0, 0, 500, 0, 1, 1000000, "Gives 50 Supply. supply is required for nearly all units.", 0, 0);
            value.SupplyProvided = 50;
            value.ButtonLeft = new Command(value.OverlordBuySelection);
            value.ButtonRight = new Command(value.OverlordBuyAll);
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
        public StructureRow Hatchery()
        {
            StructureRow value = new StructureRow(page, "Hatchery", "Hatchery.png", 0, 0, 0, 800, 50, 10, 2000, "Increases Max Drone Count by 30 and Max Extractor Count by 2");
            return value;
        }
        public StructureRow Evolution()
        {
            StructureRow value = new StructureRow(page, "EvolutionChamber", "EvolutionChamber.png", 0, 0, 0, 300, 0, 1, 1000000, "Unlocks New Upgrades on the upgrade section 1000 times more expensive for each one.");
            value.Button1IsVisible = false;
            value.RightButtonText = "Build";
            value.ButtonRight = new Command(value.BuyEvolutionChamber);
            return value;
        }
        public StructureRow Spawning()
        {
            StructureRow value = new StructureRow(page, "Spawning Pool", "SpawningPool.png", 0, 0, 0, 1000, 100, 20, 1000000, "Unlocks Zerglings. They generate more minerals than drones by harrasing other hives workers.");
            value.Button1IsVisible = false;
            value.RightButtonText = "Build";
            value.ButtonRight = new Command(value.BuySpawningPool);
            return value;
        }
        public StructureRow RoachWarren()
        {
            StructureRow value = new StructureRow(page, "Roach Warren", "RoachWarren.png", 0, 0, 0, 3000000, 10000, 10000, 1000000, "Unlocks Roaches. They generate more minerals than Zerglings by harrasing other hives Zerglings.");
            value.Button1IsVisible = false;
            value.RightButtonText = "Build";
            value.ButtonRight = new Command(value.BuyRoachWarren);
            return value;
        }
        public StructureRow InfestedBarracks()
        {
            StructureRow value = new StructureRow(page, "Infested Baracks", "InfestedBarracks.png", 0, 0, 0, 3000000, 1000, 100, 2000, "Unlocks and generates Infested Terrans. They generate more minerals than Drones by mindlessly harrasing other hives Drones. Generates 2 per second per barracks");
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
                case "Hardened Mandibles":
                    return
                    new UpgradeRow(page,
                    "Hardened Mandibles",
                    "StrongerMandibles.png",
                    1000, 2000, 5,
                    "Using 5 drones as testing biomass and some Minerals/vesp as Catalysts we can increase the density of our drones mandibles increaseing Minerals Gathered (Doubled) requires 1 chamber",
                    new Command(UpgradeFunctions.HardenedMandibles),
                    1);

                case "Super Sonic Drones":
                    return
                        new UpgradeRow(page,
                        "Super Sonic Drones",
                         "SuperSonicDrones.png",
                          10000, 20000, 50,
                         "Using 50 drones as testing biomass and some Minerals/vesp as Catalysts we can increase the density of our drones Leg Muscles increaseing Minerals Gathered (Doubled) requires 1 chamber",
                          new Command(UpgradeFunctions.SuperSonicDrones),
                         1);


                case "Injection Infusion":

                    return
                         new UpgradeRow(page,
                         "Injection Infusion",
                         "Larva.png",
                         10000, 20000, 50,
                         "Using 50 drones as testing biomass and some Minerals/vesp as Catalysts We can infuse our queens with a better Larva Inject doubleing larva generation requires 1 chamber",
                         new Command(UpgradeFunctions.InjectionInfusion),
                         1);

                case "Bigger Overlords":
                    return
                         new UpgradeRow(page,
                         "Bigger Overlords",
                         "OverLordUpgrade.png",
                         10000, 20000, 50,
                         "Using 50 drones as testing biomass and some Minerals/vesp as Catalysts We have discovered a way to Increase the size of our overlords letting them provide 50 more supply requires 1 chamber",
                         new Command(UpgradeFunctions.BiggerOverlords),
                         1);


                case "Lairs":

                    return
                         new UpgradeRow(page,
                         "Lairs",
                         "LairUpgrade.png",
                         10000, 20000, 50,
                         "Sacrificing 50 drones and some inerals/vesp as a catalyst we can create deeper lairs as apposed to our Hatcheries doubling our drone production",
                         new Command(UpgradeFunctions.Lairs),
                         1);

                case "Hives":
                    return
                         new UpgradeRow(page,
                         "Hives",
                         "LairUpgrade.png",
                         100000, 200000, 500,
                         "Sacrificing 500 drones and some inerals/vesp as a catalyst we can create deeper Hives as apposed to our Lairs doubling our drone production",
                         new Command(UpgradeFunctions.Hives),
                         2);


                case "Energized Injection":
                    return
                         new UpgradeRow(page,
                         "Energized Injection",
                         "LarvaUpgrade.PNG",
                         100000, 200000, 500,
                         "Using 500 drones as testing biomass and some Minerals/vesp as Catalysts We have discovered a way to Energize injected larva createing double the larva per second requires 1 chamber",
                         new Command(UpgradeFunctions.EnergizedInjection),
                         1);


                case "Quicker Gather":
                    return
                         new UpgradeRow(page,
                         "Quicker Gather",
                         "GatherUpgrade.png",
                         1000000, 2000000, 500,
                         "Using 500 drones as testing biomass and some Minerals/vesp as Catalysts we can increase the speed they harvest the minerals increaseing Minerals Gathered (Doubled) requires 1 chamber",
                         new Command(UpgradeFunctions.QuickerGather),
                         1);



                case "Metabolic Boost":
                    return
                             new UpgradeRow(page,
                             "Metabolic Boost",
                             "MetabolicBoost.png",
                             1000000, 2000000, 500,
                             "Using 500 drones as testing biomass and some Minerals/vesp as Catalysts We can fuse wings to the Zerglings allowing faster mineral generation (+50) requires 2 chambers",
                             new Command(UpgradeFunctions.MetabolicBoost),
                             2);

                case "Aggressive Mutation":
                    return
                         new UpgradeRow(page,
                         "Aggressive Mutation",
                         "AggressiveMutation.png",
                         10000000, 20000000, 5000,
                         "Using 5000 drones as testing biomass and some Minerals/vesp as Catalysts We can Increase adrenaline produced creating more agressive zerglings (+50 Minerals per second) requires 3 chambers",
                         new Command(UpgradeFunctions.AgressiveMutation),
                         3);

                case "More Effecient Zerglings":
                    return
                         new UpgradeRow(page,
                         "More Effecient Zerglings",
                         "ZerglingUpgrade1.png",
                         100000, 200000, 50,
                         "Using 50 drones as testing biomass and some Minerals/vesp as Catalysts We can Increase Zergling Effeciency halving the supply they require requires 2 chambers",
                         new Command(UpgradeFunctions.MoreEffecientZerglings),
                         2);

                case "Tunnling Claws":
                    return
                         new UpgradeRow(page,
                         "Tunnling Claws",
                         "TunnlingClaws.png",
                         10000000, 2000000, 500,
                         "Using 500 drones as testing biomass and some Minerals/vesp as Catalysts Our roaches develop the ability to tunnle through the ground. (Doubles Mineral Generation)requires 2 chambers",
                         new Command(UpgradeFunctions.TunnlingClaws),
                         2);

                case "Purpler Roaches":
                    return
                         new UpgradeRow(page,
                         "Purpler Roaches",
                         "PurplerRoaches.PNG",
                         100000000, 20000000, 5000,
                         "Using 5000 drones as testing biomass and some Minerals/vesp as Catalysts Our roaches Become more purple therefore becomeing better roaches (Doubles Mineral Generation) requires 3 chambers",
                         new Command(UpgradeFunctions.PurplerRoaches),
                         3);

                case "More Purple Roaches":
                    return
                         new UpgradeRow(page,
                         "More Purple Roaches",
                         "MorePurpleRoaches.PNG",
                         100000000, 20000000, 5000,
                         "Using 5000 drones as testing biomass and some Minerals/vesp as Catalysts We create the only thing better then a purple roach; more purple roaches (Doubles Mineral Generation) requires 3 chambers",
                         new Command(UpgradeFunctions.MorePurpleRoaches),
                         3);

                case "Strong Spikes":
                    return
                         new UpgradeRow(page,
                         "Strong Spikes",
                         "StrongSpikes.PNG",
                         100000000, 20000000, 5000,
                         "Using 5000 drones as testing biomass and some Minerals/vesp as Catalysts we can Strengthen the spieks infested terrans fire(doubles infested terran mineral generation",
                         new Command(UpgradeFunctions.StrongSpikes),
                         3);

                case "Stronger Spikes":
                    return
                         new UpgradeRow(page,
                         "Stronger Spikes",
                         "StrongerSpikes.PNG",
                         1000000000, 200000000, 50000,
                         "Using 50000 drones as testing biomass and some Minerals/vesp as Catalysts we can Strengthen the spikes infested terrans fire even more(doubles infested terran mineral generation",
                         new Command(UpgradeFunctions.StrongerSpikes),
                         3);

                case "Explosive Spikes":
                    return
                         new UpgradeRow(page,
                         "Explosive Spikes",
                         "ExplosiveSpikes.PNG",
                         1000000000, 200000000, 50000,
                         "Using 50000 drones as testing biomass and some Minerals/vesp as Catalysts we can cause the spieks to expload on impact increasing effeciency(doubles infested terran mineral generation",
                         new Command(UpgradeFunctions.ExplosiveSpikes),
                         4);
                default:
                    return default(UpgradeRow);

            }
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
                 new Command(UpgradeFunctions.AgressiveMutation),
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
