using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IdleSwarm.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
        HiveViewModel Page;
		public Page1 (HiveViewModel page)
		{
            Page = page;
			InitializeComponent ();
            ResetButton.Command = new Command(ResetFunction);

        }
        void ResetFunction()
        {
            Page.Free.Reset();
            Thread.Sleep(2000);
            File.Delete(Page.fileName);
            Page.Structures.Clear();
            Page.Units.Clear();
            Page.Upgrades.Clear();
            Page.GenerateStructures();
            Page.GenerateUnits();
            Page.GenerateUpgrades();
            Page.Free.Set();
        }
	}
}