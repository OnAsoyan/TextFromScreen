using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tesseract;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace TextFromDesc
{
     class  Converter
    {
        public Converter(ProgressBar progressBar, Label label)
        {
            this.progressBar = progressBar;
            this.label = label;
        }

        private ProgressBar progressBar = new ProgressBar();
        private BackgroundWorker bw = new BackgroundWorker(); 
        private Bitmap btn = null;
        private Page page = null;
        private string ResultText = "";
        private bool ConvertingIsComplited = false;
        private Label label = new Label();
        private void RunOCRProcess(TesseractEngine ocr, Bitmap bitmap)
        {
            page = ocr.Process(bitmap);
            ResultText = page.GetText();
            btn = bitmap;
            ConvertingIsComplited = true;
        }
        public void Run(Bitmap bitmap)
        {
             Thread t = new Thread(() => RunOCRProcess(new TesseractEngine("./tessdata", "eng", EngineMode.Default), bitmap));
            t.Start();
            bw.DoWork += Bw_DoWork;
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.WorkerReportsProgress = true;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted; 
            bw.RunWorkerAsync();
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        { 
            label.Visible = false;
            Form form = new Form();
            form.Size = new Size(btn.Width, btn.Height*2);
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Size = new Size(btn.Width, btn.Height*2);
            richTextBox.Text = this.ResultText;
            form.Controls.Add(richTextBox);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        } 
        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        } 
        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            int sleep = 0;
            while (!ConvertingIsComplited)
            {
                if (sleep == 20000)
                {
                    bw.ReportProgress(25);
                    sleep = 0;
                }
                else
                {
                    sleep++;
                }
            }
            bw.ReportProgress(100);
        }
    }
}
