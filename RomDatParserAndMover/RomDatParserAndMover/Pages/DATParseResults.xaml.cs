using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RomDatParserAndMover.Pages
{
    /// <summary>
    /// Interaction logic for DATParseResults.xaml
    /// </summary>
    public partial class DATParseResults : Page
    {
        private List<string> RomList { get; set; }

        public DATParseResults(List<string> romList)
        {
            this.RomList = romList;
            InitializeComponent();
            ParseResultsGrid.ItemsSource = RomList;
            ParseResultsLbl.Content = "The following " + RomList.Count + " matching roms were found:";
        }

        private void ContinueBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StartFileMove(RomList));
        }
    }
}
