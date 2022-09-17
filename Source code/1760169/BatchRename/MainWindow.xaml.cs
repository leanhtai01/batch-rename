using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace BatchRename
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<RecordFileName> recordFiles = new BindingList<RecordFileName>();
        private BindingList<RecordFolderName> recordFolders = new BindingList<RecordFolderName>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lvFiles.ItemsSource = recordFiles;
            lvFolder.ItemsSource = recordFolders;
        }

        private void menuReplace_Click(object sender, RoutedEventArgs e)
        {
            listBoxMethods.Items.Add(Method.CreateDropDownMethod(menuReplace.Header as string));
        }

        private void menuNewCase_Click(object sender, RoutedEventArgs e)
        {
            listBoxMethods.Items.Add(Method.CreateDropDownMethod(menuNewCase.Header as string));
        }

        private void menuFullnameNormalize_Click(object sender, RoutedEventArgs e)
        {
            listBoxMethods.Items.Add(Method.CreateSimpleMethod(menuFullnameNormalize.Header as string));
        }

        private void menuMove_Click(object sender, RoutedEventArgs e)
        {
            listBoxMethods.Items.Add(Method.CreateDropDownMethod(menuMove.Header as string));
        }

        private void menuUniqueName_Click(object sender, RoutedEventArgs e)
        {
            listBoxMethods.Items.Add(Method.CreateSimpleMethod(menuUniqueName.Header as string));
        }

        private void btnClearMethods_Click(object sender, RoutedEventArgs e)
        {
            listBoxMethods.Items.Clear();
        }

        private void menuAddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                recordFiles.Add(RecordFileName.ConvFullnameToRecordFilename(ofd));
            }
        }

        private void menuAddFileDirectory_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult dialogResult = fbd.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK
                    && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    foreach (var file in files)
                    {
                        recordFiles.Add(RecordFileName.ConvFullnameToRecordFilename(file));
                    }
                }
            }
        }

        private void menuAddFolder_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult dialogResult = fbd.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK
                    && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    recordFolders.Add(RecordFolderName.ConvFullnameToRecordFoldername(fbd));
                }
            }
        }

        private void menuAddFolderDirectory_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult dialogResult = fbd.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK
                    && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] folders = Directory.GetDirectories(fbd.SelectedPath);

                    foreach (var folder in folders)
                    {
                        recordFolders.Add(RecordFolderName.ConvFullnameToRecordFoldername(folder));
                    }
                }
            }
        }

        private void btnClearFileRecords_Click(object sender, RoutedEventArgs e)
        {
            recordFiles.Clear();
        }

        private void btnClearFolderRecords_Click(object sender, RoutedEventArgs e)
        {
            recordFolders.Clear();
        }

        private void BtnPreviewFile_Click(object sender, RoutedEventArgs e)
        {
            foreach (var record in recordFiles)
            {
                record.NewFilename = string.Copy(record.Filename);
                record.FileError = "";
            }

            foreach (StackPanel sp in listBoxMethods.Items)
            {
                var lb = LogicalTreeHelper.FindLogicalNode(sp, "labelMethod") as Label;

                if (lb != null)
                {
                    ProcessMethod(sp, lb, "file");
                }
            }

            CheckFilesCollision();
        }

        private void CheckFilesCollision()
        {
            for (var i = 0; i < recordFiles.Count - 1; ++i)
            {
                for (var j = i + 1; j < recordFiles.Count; ++j)
                {
                    if (string.Equals(recordFiles[i].NewFilename, recordFiles[j].NewFilename))
                    {
                        if (string.Equals(recordFiles[i].FileError, ""))
                        {
                            recordFiles[i].FileError = "Duplicate" + " " + recordFiles[i].NewFilename;
                        }

                        if (string.Equals(recordFiles[j].FileError, ""))
                        {
                            recordFiles[j].FileError = "Duplicate" + " " + recordFiles[i].NewFilename;
                        }
                    }
                }
            }
        }

        private void BtnPreviewFolder_Click(object sender, RoutedEventArgs e)
        {
            foreach (var record in recordFolders)
            {
                record.NewFoldername = string.Copy(record.Foldername);
                record.FolderError = "";
            }

            foreach (StackPanel sp in listBoxMethods.Items)
            {
                var lb = LogicalTreeHelper.FindLogicalNode(sp, "labelMethod") as Label;

                if (lb != null)
                {
                    ProcessMethod(sp, lb, "folder");
                }
            }

            CheckFoldersCollision();
        }

        private void CheckFoldersCollision()
        {
            for (var i = 0; i < recordFolders.Count - 1; ++i)
            {
                for (var j = i + 1; j < recordFolders.Count; ++j)
                {
                    if (string.Equals(recordFolders[i].NewFoldername, recordFolders[j].NewFoldername))
                    {
                        if (string.Equals(recordFolders[i].FolderError, ""))
                        {
                            recordFolders[i].FolderError = "Duplicate" + " " + recordFolders[i].NewFoldername;
                        }

                        if (string.Equals(recordFolders[j].FolderError, ""))
                        {
                            recordFolders[j].FolderError = "Duplicate" + " " + recordFolders[i].NewFoldername;
                        }
                    }
                }
            }
        }

        private void ProcessMethod(StackPanel sp, Label lb, string id)
        {
            switch (lb.Content as string)
            {
                case "Replace":
                    ProcessReplaceMethod(sp, id);
                    break;
                case "New Case":
                    ProcessNewCaseMethod(sp, id);
                    break;

                case "Fullname Normalize":
                    ProcessFullnameNormalize(sp, id);
                    break;

                case "Move":
                    ProcessMoveMethod(sp, id);
                    break;

                case "Unique Name":
                    ProcessUniqueNameMethod(sp, id);
                    break;
            }
        }

        private void ProcessReplaceMethod(StackPanel sp, string id)
        {
            var ddBtn = LogicalTreeHelper.FindLogicalNode(sp, "ddBtn") as DropDownButton;
            var cb = LogicalTreeHelper.FindLogicalNode(ddBtn, "checkBox") as CheckBox;
            StackPanel sp1 = ddBtn.DropDownContent as StackPanel;
            var tbTextReplaced = LogicalTreeHelper.FindLogicalNode(sp1, "tbTextReplaced") as TextBox;
            var tbReplaceWith = LogicalTreeHelper.FindLogicalNode(sp1, "tbReplaceWith") as TextBox;

            if (cb.IsChecked == true)
            {
                if (id == "file")
                {
                    foreach (var record in recordFiles)
                    {
                        if (!String.IsNullOrEmpty(tbTextReplaced.Text))
                        {
                            record.NewFilename = NameProcessor.Replace(record.NewFilename, tbTextReplaced.Text, tbReplaceWith.Text);
                        }
                    }
                }
                else
                {
                    foreach (var record in recordFolders)
                    {
                        if (!String.IsNullOrEmpty(tbTextReplaced.Text))
                        {
                            record.NewFoldername = NameProcessor.Replace(record.NewFoldername, tbTextReplaced.Text, tbReplaceWith.Text);
                        }
                    }
                }
            }
        }

        private void ProcessNewCaseMethod(StackPanel sp, string id)
        {
            var ddBtn = LogicalTreeHelper.FindLogicalNode(sp, "ddBtn") as DropDownButton;
            var cb = LogicalTreeHelper.FindLogicalNode(ddBtn, "checkBox") as CheckBox;
            StackPanel sp1 = ddBtn.DropDownContent as StackPanel;
            var radUpper = LogicalTreeHelper.FindLogicalNode(sp1, "radUpper") as RadioButton;
            var radLower = LogicalTreeHelper.FindLogicalNode(sp1, "radLower") as RadioButton;
            var radUpperFirst = LogicalTreeHelper.FindLogicalNode(sp1, "radUpperFirst") as RadioButton;

            if (cb.IsChecked == true)
            {
                if (id == "file")
                {
                    if (radUpper.IsChecked == true)
                    {
                        foreach (var record in recordFiles)
                        {
                            record.NewFilename = NameProcessor.ToUpper(record.NewFilename);
                        }
                    }
                    else if (radLower.IsChecked == true)
                    {
                        foreach (var record in recordFiles)
                        {
                            record.NewFilename = NameProcessor.ToLower(record.NewFilename);
                        }
                    }
                    else if (radUpperFirst.IsChecked == true)
                    {
                        foreach (var record in recordFiles)
                        {
                            record.NewFilename = NameProcessor.ToUpperFirstLetterOfWords(record.NewFilename);
                        }
                    }
                }
                else
                {
                    if (radUpper.IsChecked == true)
                    {
                        foreach (var record in recordFolders)
                        {
                            record.NewFoldername = NameProcessor.ToUpper(record.NewFoldername);
                        }
                    }
                    else if (radLower.IsChecked == true)
                    {
                        foreach (var record in recordFolders)
                        {
                            record.NewFoldername = NameProcessor.ToLower(record.NewFoldername);
                        }
                    }
                    else if (radUpperFirst.IsChecked == true)
                    {
                        foreach (var record in recordFolders)
                        {
                            record.NewFoldername = NameProcessor.ToUpperFirstLetterOfWords(record.NewFoldername);
                        }
                    }
                }
            }
        }

        private void ProcessFullnameNormalize(StackPanel sp, string id)
        {
            var btn = LogicalTreeHelper.FindLogicalNode(sp, "btn") as Button;
            var cb = LogicalTreeHelper.FindLogicalNode(btn, "checkBox") as CheckBox;

            if (cb.IsChecked == true)
            {
                if (id == "file")
                {
                    foreach (var record in recordFiles)
                    {
                        record.NewFilename = NameProcessor.FullnameNormalize(record.NewFilename);
                    }
                }
                else
                {
                    foreach (var record in recordFolders)
                    {
                        record.NewFoldername = NameProcessor.FullnameNormalize(record.NewFoldername);
                    }
                }
            }
        }

        private void ProcessMoveMethod(StackPanel sp, string id)
        {
            var ddBtn = LogicalTreeHelper.FindLogicalNode(sp, "ddBtn") as DropDownButton;
            var cb = LogicalTreeHelper.FindLogicalNode(ddBtn, "checkBox") as CheckBox;
            StackPanel sp1 = ddBtn.DropDownContent as StackPanel;
            var tbMoveFrom = LogicalTreeHelper.FindLogicalNode(sp1, "tbMoveFrom") as TextBox;
            var tbMoveCount = LogicalTreeHelper.FindLogicalNode(sp1, "tbMoveCount") as TextBox;
            var tbMoveTo = LogicalTreeHelper.FindLogicalNode(sp1, "tbMoveTo") as TextBox;


            if (cb.IsChecked == true)
            {
                if (id == "file")
                {
                    foreach (var record in recordFiles)
                    {
                        if (!String.IsNullOrEmpty(tbMoveFrom.Text) && !String.IsNullOrEmpty(tbMoveCount.Text)
                            && !String.IsNullOrEmpty(tbMoveTo.Text))
                        {
                            record.NewFilename = NameProcessor.Move(record.NewFilename, int.Parse(tbMoveFrom.Text), int.Parse(tbMoveCount.Text), int.Parse(tbMoveTo.Text));
                        }
                    }
                }
                else
                {
                    foreach (var record in recordFolders)
                    {
                        if (!String.IsNullOrEmpty(tbMoveFrom.Text) && !String.IsNullOrEmpty(tbMoveCount.Text)
                            && !String.IsNullOrEmpty(tbMoveTo.Text))
                        {
                            record.NewFoldername = NameProcessor.Move(record.NewFoldername, int.Parse(tbMoveFrom.Text), int.Parse(tbMoveCount.Text), int.Parse(tbMoveTo.Text));
                        }
                    }
                }
            }
        }

        private void ProcessUniqueNameMethod(StackPanel sp, string id)
        {
            var btn = LogicalTreeHelper.FindLogicalNode(sp, "btn") as Button;
            var cb = LogicalTreeHelper.FindLogicalNode(btn, "checkBox") as CheckBox;

            if (cb.IsChecked == true)
            {
                if (id == "file")
                {
                    foreach (var record in recordFiles)
                    {
                        record.NewFilename = NameProcessor.UniqueName(record.NewFilename);
                    }
                }
                else
                {
                    foreach (var record in recordFolders)
                    {
                        record.NewFoldername = NameProcessor.UniqueName(record.NewFoldername);
                    }
                }
            }
        }

        private void BtnStartBatch_Click(object sender, RoutedEventArgs e)
        {
            var ti = tabControl.SelectedItem as TabItem;
            MessageBoxResult result = MessageBoxResult.None;

            switch (ti.Header as string)
            {
                case "Rename Files":
                    BtnPreviewFile_Click(this, null);
                    result = System.Windows.MessageBox.Show("Do you want to continue?", "Rename all files", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        RenameFiles();
                        System.Windows.MessageBox.Show("All files renamed successfully!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    
                    break;

                case "Rename Folders":
                    BtnPreviewFolder_Click(this, null);
                    result = System.Windows.MessageBox.Show("Do you want to continue?", "Rename all folder", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        RenameFolder();
                        System.Windows.MessageBox.Show("All folders renamed successfully!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    
                    break;
            }
        }

        private void RenameFolder()
        {
            // append number to dupplicate files if need
            var rule = cbFolderNameCollisionRule.SelectedItem as ComboBoxItem;
            var strRule = rule.Content.ToString();

            if (string.Equals(strRule, "AppendNumber"))
            {
                for (var i = 0; i < recordFolders.Count - 1; ++i)
                {
                    if (!string.Equals(recordFolders[i].FolderError, ""))
                    {
                        string filename = "";
                        string extension = "";
                        var count = 2;

                        for (var j = i + 1; j < recordFolders.Count; ++j)
                        {
                            if (string.Equals(recordFolders[i].NewFoldername, recordFolders[j].NewFoldername))
                            {

                                NameProcessor.PreProcess(recordFolders[j].NewFoldername, ref filename, ref extension);
                                recordFolders[j].NewFoldername = filename + count + extension;
                                recordFolders[j].FolderError = "";
                                ++count;
                            }
                        }

                        NameProcessor.PreProcess(recordFolders[i].NewFoldername, ref filename, ref extension);
                        recordFolders[i].NewFoldername = filename + 1 + extension;
                        recordFolders[i].FolderError = "";
                        recordFolders[i].FolderError = "";
                    }
                }
            }

            foreach (var record in recordFolders)
            {
                if (record.FolderError == "")
                {
                    string oldName = record.FolderPath + "\\" + record.Foldername;
                    string newName = record.FolderPath + "\\" + record.NewFoldername;
                    string tmp = record.FolderPath + "\\" + NameProcessor.UniqueName(record.FolderPath);
                    Directory.CreateDirectory(tmp);

                    // actual rename
                    Directory.Move(oldName, tmp + "\\" + record.NewFoldername);
                    Directory.Move(tmp + "\\" + record.NewFoldername, record.FolderPath + "\\" + record.NewFoldername);
                    Directory.Delete(tmp);

                    // virtual rename
                    record.Foldername = string.Copy(record.NewFoldername);
                }
            }
            
            foreach (var record in recordFolders)
            {
                record.NewFoldername = "";
            }
        }

        private void RenameFiles()
        {
            // append number to dupplicate files if need
            var rule = cbFileNameCollisionRule.SelectedItem as ComboBoxItem;
            var strRule = rule.Content.ToString();

            if (string.Equals(strRule, "AppendNumber"))
            {
                for (var i = 0; i < recordFiles.Count - 1; ++i)
                {
                    if (!string.Equals(recordFiles[i].FileError, ""))
                    {
                        string filename = "";
                        string extension = "";
                        var count = 2;

                        for (var j = i + 1; j < recordFiles.Count; ++j)
                        {
                            if (string.Equals(recordFiles[i].NewFilename, recordFiles[j].NewFilename))
                            {
                                
                                NameProcessor.PreProcess(recordFiles[j].NewFilename, ref filename, ref extension);
                                recordFiles[j].NewFilename = filename + count + extension;
                                recordFiles[j].FileError = "";
                                ++count;
                            }
                        }

                        NameProcessor.PreProcess(recordFiles[i].NewFilename, ref filename, ref extension);
                        recordFiles[i].NewFilename = filename + 1 + extension;
                        recordFiles[i].FileError = "";
                        recordFiles[i].FileError = "";
                    }
                }
            }

            foreach (var record in recordFiles)
            {
                if (record.FileError == "")
                {
                    string oldName = record.FilePath + "\\" + record.Filename;
                    string newName = record.FilePath + "\\" + record.NewFilename;

                    // actual rename
                    File.Move(oldName, newName);

                    // virtual rename
                    record.Filename = string.Copy(record.NewFilename);
                }     
            }

            foreach (var record in recordFiles)
            {
                record.NewFilename = "";
            }
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }
    }
}
