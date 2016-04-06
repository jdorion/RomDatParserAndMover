using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Forms;
using RomDatParserAndMoverLibrary;
using System.Linq;

namespace RomDatParserAndMover.Pages
{
    /// <summary>
    /// Interaction logic for StartFileMove.xaml
    /// </summary>
    public partial class StartFileMove : Page
    {
        private List<string> RomList { get; set; }

        public StartFileMove(List<string> romList)
        {
            RomList = romList;
            InitializeComponent();
            WelcomeLbl.Content = "Hitting 'Go' will attempt to copy " + romList.Count + " files.";
        }

        #region click handlers
        private void RomsPathBtn_Click(object sender, RoutedEventArgs e)
        {
            var _folderDlg = new FolderBrowserDialog
            {
                Description = "Select a folder",
            };

            if (_folderDlg.ShowDialog() == DialogResult.OK)
            {
                RomsPathText.Text = _folderDlg.SelectedPath;
            }
        }

        private void OutputPathBtn_Click(object sender, RoutedEventArgs e)
        {
            var _folderDlg = new FolderBrowserDialog
            {
                Description = "Select a folder",
            };

            if (_folderDlg.ShowDialog() == DialogResult.OK)
            {
                OutputPathText.Text = _folderDlg.SelectedPath;
            }
        }

        private void GoBtn_Click(object sender, RoutedEventArgs e)
        {
            var romCopier = new RomCopier(RomList, RomsPathText.Text, OutputPathText.Text);
            if (MoveCheck.IsChecked.Value)
            {
                romCopier.MoveRoms();
            }
            else
            {
                romCopier.CopyRoms();
            }

            if (romCopier.Errors.Any())
            {
                NavigationService.Navigate(new ErrorPage(romCopier.Errors));
            }
            else
            {
                NavigationService.Navigate(new FileMoveResults());
            }
        }
        #endregion
    }
}
