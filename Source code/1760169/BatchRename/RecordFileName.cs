using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BatchRename
{
    public class RecordFileName : INotifyPropertyChanged
    {
        private string filename;
        private string newFilename;
        private string filePath;
        private string fileError;

        public string Filename
        {
            get
            {
                return filename;
            }

            set
            {
                filename = value;
                NotifyPropertyChanged("Filename");
            }
        }

        public string NewFilename
        {
            get
            {
                return newFilename;
            }

            set
            {
                newFilename = value;
                NotifyPropertyChanged("NewFilename");
            }
        }

        public string FilePath
        {
            get
            {
                return filePath;
            }

            set
            {
                filePath = value;
                NotifyPropertyChanged("FilePath");
            }
        }

        public string FileError
        {
            get
            {
                return fileError;
            }

            set
            {
                fileError = value;
                NotifyPropertyChanged("FileError");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static RecordFileName ConvFullnameToRecordFilename(OpenFileDialog ofd)
        {
            RecordFileName rfn = new RecordFileName();

            rfn.Filename = ofd.SafeFileName;
            rfn.NewFilename = rfn.Filename;
            rfn.FilePath = Path.GetDirectoryName(ofd.FileName);
            rfn.FileError = "";

            return rfn;
        }

        public static RecordFileName ConvFullnameToRecordFilename(string path)
        {
            RecordFileName rfn = new RecordFileName();

            rfn.Filename = Path.GetFileName(path);
            rfn.NewFilename = rfn.Filename;
            rfn.FilePath = Path.GetDirectoryName(path);
            rfn.FileError = "";

            return rfn;
        }
    }
}
