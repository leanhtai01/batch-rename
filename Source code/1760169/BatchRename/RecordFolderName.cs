using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class RecordFolderName : INotifyPropertyChanged
    {
        private string foldername;
        private string newFoldername;
        private string folderPath;
        private string folderError;

        public string Foldername
        {
            get
            {
                return foldername;
            }

            set
            {
                foldername = value;
                NotifyPropertyChanged("Foldername");
            }
        }

        public string NewFoldername
        {
            get
            {
                return newFoldername;
            }

            set
            {
                newFoldername = value;
                NotifyPropertyChanged("NewFoldername");
            }
        }

        public string FolderPath
        {
            get
            {
                return folderPath;
            }

            set
            {
                folderPath = value;
                NotifyPropertyChanged("FolderPath");
            }
        }

        public string FolderError
        {
            get
            {
                return folderError;
            }

            set
            {
                folderError = value;
                NotifyPropertyChanged("FolderError");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static RecordFolderName ConvFullnameToRecordFoldername(System.Windows.Forms.FolderBrowserDialog fbd)
        {
            RecordFolderName rfn = new RecordFolderName();

            rfn.Foldername = Path.GetFileName(fbd.SelectedPath);
            rfn.NewFoldername = string.Copy(rfn.Foldername);
            rfn.FolderPath = Path.GetDirectoryName(fbd.SelectedPath);
            rfn.FolderError = "";

            return rfn;
        }

        public static RecordFolderName ConvFullnameToRecordFoldername(string path)
        {
            RecordFolderName rfn = new RecordFolderName();

            rfn.Foldername = Path.GetFileName(path);
            rfn.NewFoldername = string.Copy(rfn.Foldername);
            rfn.FolderPath = Path.GetDirectoryName(path);
            rfn.FolderError = "";

            return rfn;
        }
    }
}
