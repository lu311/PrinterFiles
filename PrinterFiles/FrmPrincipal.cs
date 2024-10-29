using PdfiumViewer;
using System;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace PrinterFiles
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();

            timer1.Interval = Convert.ToInt16(Config.TimeCheck) * 1000;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string arq, arqMove = string.Empty;
            FilesFolders.CheckFiles();
            foreach (var item in FilesFolders.lstFiles)
            {
                arq = $"{Config.Folder}\\{item.Name}";
                arqMove = $"{Config.FolderMove}\\{item.Name}";
                PrintPdf(arq);
                File.Move(arq, arqMove);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        public static void PrintPdf(string pdfPath)
        {
            // https://github.com/pvginkel/PdfiumBuild/tree/master/Builds/2018-04-08
            // DLL x64 Download - Run Program Base x64
            using (var document = PdfDocument.Load(pdfPath))
            {
                using (var printDocument = document.CreatePrintDocument())
                {
                    printDocument.PrinterSettings.PrinterName = new PrinterSettings().PrinterName;
                    printDocument.PrintController = new StandardPrintController();
                    printDocument.Print();
                }
            }
        }
    }
}
