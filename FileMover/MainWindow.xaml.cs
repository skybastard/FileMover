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
using System.ServiceProcess;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;

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
        string dllOriginPath;
        string dllDestinationPath;
        private static BackgroundWorker backgroundWorker;

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

            dllOriginPath = Properties.Settings.Default.DllOriginPath;
            dllOriginLabel.Content = dllOriginPath;

            dllDestinationPath = Properties.Settings.Default.DllDestinationPath;
            dllDestinationLabel.Content = dllDestinationPath;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork; // move files and thread.sleep in background not in UI


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

        





        public static void StopExcecManager()
        {
            try
            {
                ServiceController serviceController = new ServiceController("ExecManager(Raptor2)");
                TimeSpan timeout = TimeSpan.FromMilliseconds(7000);
                serviceController.Stop();
                serviceController.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                Thread.Sleep(1000);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message + " " + error.StackTrace);
            }         
            
        }

        public static void StartExcecManager()
        {
            try
            {
                ServiceController serviceController = new ServiceController("ExecManager(Raptor2)");
                TimeSpan timeout = TimeSpan.FromMilliseconds(15000);
                serviceController.Start();
                serviceController.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message + " " + error.StackTrace);
            }
            
        }

      

        private void DllOriginBtn(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                dllOriginPath = dialog.FileName;
                dllOriginLabel.Content = dllOriginPath;
                Properties.Settings.Default.DllOriginPath = dllOriginPath;
                Properties.Settings.Default.Save();

            }
        }

        private void DllDestinationBtn(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {

                dllDestinationPath = dialog.FileName;
                dllDestinationLabel.Content = dllDestinationPath;
                Properties.Settings.Default.DllDestinationPath = dllDestinationPath;
                Properties.Settings.Default.Save();
            }
        }

        private void transferFilesBtn_Click(object sender, RoutedEventArgs e)
        {
            //backgroundWorker.RunWorkerAsync();
            //backgroundWorker.Dispose();
            MoveFiles();
        }

        private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            StopExcecManager();
            MoveFiles();
            StartExcecManager();
        }

        public void MoveFiles()
        {
            try
            {
                File.Copy(testplanOriginPath, Path.Combine(testplanDestinationPath, Path.GetFileName(testplanOriginPath)), true);
                File.Copy(criteriaOriginPath, Path.Combine(criteriaDestinationPath, Path.GetFileName(criteriaOriginPath)), true);
                File.Copy(testdataOriginPath, Path.Combine(testdataDestinationPath, Path.GetFileName(testdataOriginPath)), true);
                Process.Start("cmd.exe", "/C XCOPY " + dllOriginPath + " " + dllDestinationPath + " " + "/E /S /Y");               
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message + " " + error.StackTrace);
            }
        }

    }
}
