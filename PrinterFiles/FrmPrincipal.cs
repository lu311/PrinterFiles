﻿using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace PrinterFiles
{
    public partial class FrmPrincipal : Form
    {
        static List<string> lstFilePrintOK = new List<string>();
        int contador = 0;
        int contprint = 0;

        public FrmPrincipal()
        {
            InitializeComponent();

            timer1.Interval = Convert.ToInt16(Config.TimeCheck) * 1000;
            timer1.Start();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string arq = string.Empty;
            FilesFolders.CheckFiles();
            contador += 1;
            lblContador.Text = contador.ToString();

            foreach (var item in FilesFolders.lstFiles)
            {
                if (!lstFilePrintOK.Contains(item.Name))
                {
                    if (File.Exists($"{Config.Folder}\\{item.Name}"))
                    {
                        try
                        {
                            PrintPdf($"{Config.Folder}\\{item.Name}");
                            lstFilePrintOK.Add(item.Name);
                            contprint += 1;
                            lblPrintCont.Text = contprint.ToString();
                        }
                        catch (Exception)
                        {
                            lstFilePrintOK.Add(item.Name);
                            throw;
                        }
                    }
                }
            }

            FileMove();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        public void PrintPdf(string pdfPath)
        {
            // https://github.com/pvginkel/PdfiumBuild/tree/master/Builds/2018-04-08
            // DLL x64 Download - Run Program Base x64
            
            try
            {
                using (var document = PdfDocument.Load(pdfPath))
                {
                    using (var printDocument = document.CreatePrintDocument())
                    {
                        printDocument.PrinterSettings.PrinterName = new PrinterSettings().PrinterName;
                        printDocument.PrintController = new StandardPrintController();
                        printDocument.Print();
                        printDocument.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                txtMensagem.Text += "\n Falhar ao imprimir o arquivo. Verifique se o arquivo é mesmo uma PDF. " +
                 $"{pdfPath} - {ex.Message}";
               
                //throw;
            }

        }

        public void FileMove()
        {
            try
            {
                foreach (var item in lstFilePrintOK)
                {
                    if (File.Exists($"{Config.Folder}\\{item}"))
                        File.Move($"{Config.Folder}\\{item}",
                            $"{Config.FolderMove}\\{DateTime.Now.Ticks}-{item}");
                }

                lstFilePrintOK = new List<string>();
            }
            catch (Exception ex)
            {
                txtMensagem.Text += "\n Falha ao move o arquivo. Feche os arquivos aberto no computador. \n" + ex.Message;
            }
        }
    }
}
