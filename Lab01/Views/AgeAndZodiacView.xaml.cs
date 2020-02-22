using System.Windows.Controls;
using Lab01.ViewModels.AgeAndZodiac;

namespace Lab01.Views
{
    public partial class AgeAndZodiacView : UserControl
    {
        public AgeAndZodiacView()
        {
            InitializeComponent();
            DataContext = new AgeAndZodiacViewModel();
        }
    }
}