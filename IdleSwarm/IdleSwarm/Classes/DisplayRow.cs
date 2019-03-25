using System.Numerics;
using System.Windows.Input;
using Xamarin.Forms;

namespace IdleSwarm.Droid.Classes
{
    public interface DisplayRow
    {
        bool Button2IsVisible { get; set; }
        bool Button1IsVisible { get; set; }
        ICommand ButtonLeft { get; set; }
        ICommand ButtonRight { get; set; }
        ImageSource Image { get; set; }
        string DisplayLabel { get; set; }
        string LeftButtonText { get; set; }
        string RightButtonText { get; set; }
        string Description { get; set; }
        BigInteger MineralsRequired { get; set; }
        BigInteger VespRequired { get; set; }
        string ThirdDisplayLabel { get; set; }

        string GetSaveString();
    }
}