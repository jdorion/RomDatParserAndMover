using RomDatParserAndMoverLibrary;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RomDatParserAndMover.Pages
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartParse : Page
    {
        public StartParse()
        {
            InitializeComponent();
        }

        #region "click handlers"
        private void DATPathBtn_Click(object sender, RoutedEventArgs e)
        {
            var _fileDlg = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "",
                DefaultExt = ".dat",
                Filter = "ClrMamePro DAT (.dat)|*.dat"
            };

            if (_fileDlg.ShowDialog() ?? false)
            {
                DATPathText.Text = _fileDlg.FileName;
            }
        }

        private void GoBtn_Click(object sender, RoutedEventArgs e)
        {
            var datParser = new DATParser(DATPathText.Text, KeywordText.Text, IncludeClonesCheck.IsChecked.Value);

            if (datParser.Errors.Any())
            {
                NavigationService.Navigate(new ErrorPage(datParser.Errors));
            }
            else
            {
                NavigationService.Navigate(new DATParseResults(datParser.RomList));
            }
        }
        #endregion
    }
}
