using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterFiles
{
    static internal class Config
    {
        static public string Folder;
        static public string FolderMove;
        static public string TimeCheck;
        static public void LerFileConfig()
        {
            string arquivo = @"Configuracao.txt";
            string linha = string.Empty;
            string[] r = null;

            if (File.Exists(arquivo))
                using (StreamReader reader = new StreamReader(arquivo))
                {
                    while (reader.Peek() >= 0)
                    {
                        linha = reader.ReadLine();

                        r = linha.Split('=');

                        if (r[0].ToUpper() == "Folder".ToUpper())
                        {
                            Folder = r[1];
                        };
                        if (r[0].ToUpper() == "TimeCheck".ToUpper())
                        {
                            TimeCheck = r[1];
                        };
                        if (r[0].ToUpper() == "FolderMove".ToUpper())
                        {
                            FolderMove = r[1];
                        };
                    }
                }
        } 
    }
}
