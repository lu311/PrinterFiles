using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterFiles
{
    static class FilesFolders
    {
        static public List<FilesInfo> lstFiles { get; set; }

        public class FilesInfo
        {
            public string Name { get; set; } // Cliente123.BCK
            public DateTime DateCreate { get; set; } // 10/10/2020 20:12:56
            public double Size { get; set; } // 37 GB
        }


        static public void CheckFiles()
        {
            lstFiles = new List<FilesInfo>();

            FilesCheck();
        }

        static private void FilesCheck()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Config.Folder);
            FileInfo[] files = null;

            if (directoryInfo.Exists)
                files = directoryInfo.GetFiles();
            else
                return;

            foreach (FileInfo file in files)
            {
                string fileName = file.Name;
                DateTime creationDate = file.CreationTime;
                double sizeInMegabytes = file.Length / (1024 * 1024.0);

                lstFiles.Add(new FilesInfo
                {
                    Name = fileName,
                    DateCreate = creationDate,
                    Size = sizeInMegabytes
                });
            }

            lstFiles = lstFiles.OrderBy(f => f.DateCreate).ToList();
        }
    }
}
