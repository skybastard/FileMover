using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

namespace FileMover
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string testplanOriginPath;
        string testplanDestinationPath;
        string criteriaOriginPath;
        string criteriaDestinationPath;
        string testdataOriginPath;
        string testdataDestinationPath;

        public MainWindow()
        {
            InitializeComponent();
            // assign paths from Settings, also fill labels in GUI
            testplanOriginPath = Properties.Settings.Default.TestplanOriginPath;
            testplanOriginLabel.Content = testplanOriginPath;

            testplanDestinationPath = Properties.Settings.Default.TestPlanDestinationPath;
            testplanDestinationLabel.Content = testplanDestinationPath;

            criteriaOriginPath = Properties.Settings.Default.CriteriaOriginPath;
            criteriaOriginLabel.Content = criteriaOriginPath;

            criteriaDestinationPath = Properties.Settings.Default.CriteriaDestinationPath;
            criteriaDestinationLabel.Content = criteriaDestinationPath;

            testdataOriginPath = Properties.Settings.Default.TestdataOringinPath;
            testdataOriginLabel.Content = testdataOriginPath;

            testdataDestinationPath = Properties.Settings.Default.TestdataDestinationPath;
            testdataDestinationLabel.Content = testdataDestinationPath;

            
        }

        private void TestplanOriginBtn(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//TODO put source as initial folder
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    testplanOriginPath = openFileDialog.FileName;
                    testplanOriginLabel.Content = openFileDialog.FileName;
                    Properties.Settings.Default.TestplanOriginPath = testplanOriginPath;
                    Properties.Settings.Default.Save();
                }                                 
            }
        }

        private void TestplanDestinationBtn(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                
                testplanDestinationPath = dialog.FileName;
                testplanDestinationLabel.Content = dialog.FileName;
                Properties.Settings.Default.TestPlanDestinationPath = testplanDestinationPath;
                Properties.Settings.Default.Save();
            }
        }

        private void transferFilesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                copyCompleteLabel.Content = "copying...";
                File.Copy(testplanOriginPath, Path.Combine(testplanDestinationPath, Path.GetFileName(testplanOriginPath)), true);
                File.Copy(criteriaOriginPath, Path.Combine(criteriaDestinationPath, Path.GetFileName(criteriaOriginPath)), true);
                File.Copy(testdataOriginPath, Path.Combine(testdataDestinationPath, Path.GetFileName(testdataOriginPath)), true);
                copyCompleteLabel.Content = "Files Copied";
                
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message + " " + error.StackTrace);                
            }
            
        }

        private void CriteriaOriginBtn(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//TODO put source as initial folder
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    criteriaOriginPath = openFileDialog.FileName;
                    criteriaOriginLabel.Content = openFileDialog.FileName;
                    Properties.Settings.Default.CriteriaOriginPath = criteriaOriginPath;
                    Properties.Settings.Default.Save();
                }
            }

        }

        private void CriteriaDestinationBtn(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {

                criteriaDestinationPath = dialog.FileName;
                criteriaDestinationLabel.Content = dialog.FileName;
                Properties.Settings.Default.CriteriaDestinationPath = criteriaDestinationPath;
                Properties.Settings.Default.Save();
            }

        }

        private void TestDataOriginBtn(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);//TODO put source as initial folder
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    testdataOriginPath = openFileDialog.FileName;
                    testdataOriginLabel.Content = openFileDialog.FileName;
                    Properties.Settings.Default.TestdataOringinPath = testdataOriginPath;
                    Properties.Settings.Default.Save();
                }
            }

        }

        private void TestDataDestinationBtn(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {

                testdataDestinationPath = dialog.FileName;
                testdataDestinationLabel.Content = dialog.FileName;
                Properties.Settings.Default.TestdataDestinationPath = testdataDestinationPath;
                Properties.Settings.Default.Save();
            }

        }
    }
}
