namespace IdleSwarm.Classes
{
    public class UpgradeFunctions
    {
        private MainPageViewModel Page;

        public UpgradeFunctions(MainPageViewModel MainPage)
        {
            Page = MainPage;
        }

        public void AfterUpgrade(string upgradeName)
        {
            for (int x = 0; x < Page.Upgrades.Count; x++)
            {
                if (Page.Upgrades[x].Name == upgradeName)
                {
                    Page.Upgrades.RemoveAt(x);
                    break;
                }
            }
        }

        public void HardenedMandibles()
        {
            Page.Units[0].MineralsPerRound += Page.Units[0].MineralsPerRound;
            Page.Units[0].Description = "Gathers " + Page.Units[0].MineralsPerRound.ToString() + " Minerals per second and can be used to build Structures.";
            AfterUpgrade("Hardened Mandibles");
        }

        public void SuperSonicDrones()
        {
            Page.Units[0].MineralsPerRound += Page.Units[0].MineralsPerRound;
            Page.Units[0].Description = "Gathers " + Page.Units[0].MineralsPerRound.ToString() + " Minerals per second and can be used to build Structures.";
            AfterUpgrade("Super Sonic Drones");
        }

        public void QuickerGather()
        {
            Page.Units[0].MineralsPerRound += Page.Units[0].MineralsPerRound;
            Page.Units[0].Description = "Gathers " + Page.Units[0].MineralsPerRound.ToString() + " Minerals per second and can be used to build Structures.";
            AfterUpgrade("Quicker Gather");
        }

        public void BiggerOverlords()
        {
            Page.Units[2].SupplyProvided += 50;
            Page.Units[2].Description = "Gives " + Page.Units[2].SupplyProvided + " Supply.supply is required for nearly all units.";
            AfterUpgrade("Bigger Overlords");
        }

        public void InjectionInfusion()
        {
            Page.Units[1].LarvaPerRound += Page.Units[1].LarvaPerRound;
            Page.Units[1].Description = "Generates " + Page.Units[1].LarvaPerRound + " Larva per second.";
            AfterUpgrade("Injection Infusion");
        }

        public void EnergizedInjection()
        {
            Page.Units[1].LarvaPerRound += Page.Units[1].LarvaPerRound;
            Page.Units[1].Description = "Generates " + Page.Units[1].LarvaPerRound + " Larva per second.";
            AfterUpgrade("Energized Injection");
        }

        public void MetabolicBoost()
        {
            Page.Units[3].MineralsPerRound += Page.Units[3].MineralsPerRound;
            Page.Units[3].Description = "Harrasses others Hives workers and takes " + Page.Units[3].MineralsPerRound + " of their minerals per second";
            AfterUpgrade("Metabolic Boost");
        }

        public void TunnlingClaws()
        {
            Page.Units[4].MineralsPerRound += Page.Units[4].MineralsPerRound;
            Page.Units[4].Description = "Harrasses others Hives Zerglings and takes " + Page.Units[4].MineralsPerRound + " of their minerals per second";
            AfterUpgrade("Tunnling Claws");
        }

        public void PurplerRoaches()
        {
            Page.Units[4].MineralsPerRound += Page.Units[4].MineralsPerRound;
            Page.Units[4].Description = "Harrasses others Hives Zerglings and takes " + Page.Units[4].MineralsPerRound + " of their minerals per second";
            AfterUpgrade("Purpler Roaches");
        }

        public void MorePurpleRoaches()
        {
            Page.Units[4].MineralsPerRound += Page.Units[4].MineralsPerRound;
            Page.Units[4].Description = "Harrasses others Hives Zerglings and takes " + Page.Units[4].MineralsPerRound + " of their minerals per second";
            AfterUpgrade("More Purple Roaches");
        }

        public void AggressiveMutation()
        {
            Page.Units[3].MineralsPerRound += Page.Units[3].MineralsPerRound;
            Page.Units[3].Description = "Harrasses others Hives workers and takes " + Page.Units[3].MineralsPerRound + " of their minerals per second";
            AfterUpgrade("Aggressive Mutation");
        }

        public void MoreEffecientZerglings()
        {
            AfterUpgrade("More Effecient Zerglings");
        }

        public void StrongSpikes()
        {
            Page.Units[5].MineralsPerRound += Page.Units[5].MineralsPerRound;
            Page.Units[5].Description = "Created by Infestation Pits. Very dumb generates Minerals slower than zerglings but is passivly generated. (" + Page.Units[5].MineralsPerRound + ")";
            AfterUpgrade("Strong Spikes");
        }

        public void StrongerSpikes()
        {
            Page.Units[5].MineralsPerRound += Page.Units[5].MineralsPerRound;
            Page.Units[5].Description = "Created by Infestation Pits. Very dumb generates Minerals slower than zerglings but is passivly generated. (" + Page.Units[5].MineralsPerRound + ")";
            AfterUpgrade("Stronger Spikes");
        }

        public void ExplosiveSpikes()
        {
            Page.Units[5].MineralsPerRound += Page.Units[5].MineralsPerRound;
            Page.Units[5].Description = "Created by Infestation Pits. Very dumb generates Minerals slower than zerglings but is passivly generated. (" + Page.Units[5].MineralsPerRound + ")";
            AfterUpgrade("Explosive Spikes");
        }

        public void Lairs()
        {
            Page.DronesPerHatchery += 15;
            Page.DronesPerHatchery += 1;
        }

        public void Hives()
        {
            Page.Structures[2].DronesPerRound += Page.Structures[2].DronesPerRound;
            Page.Structures[2].Description = "Creates " + Page.Structures[2].DronesPerRound + " Drone Per Second";
            AfterUpgrade("Hives");
        }

        public void FasterExtractors()
        {
            Page.Structures[0].VespPerRound += Page.Structures[0].VespPerRound;
            Page.Structures[0].Description = "Creates " + Page.Structures[0].VespPerRound + " Drone Per Second";
            AfterUpgrade("Faster Extractors");
        }

        public void LargerTanks()
        {
            Page.Structures[0].VespPerRound += Page.Structures[0].VespPerRound;
            Page.Structures[0].Description = "Creates " + Page.Structures[0].VespPerRound + " Drone Per Second";
            AfterUpgrade("Larger Tanks");
        }
    }
}