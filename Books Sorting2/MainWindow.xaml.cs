using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System;
using System.Collections;
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
using System.Windows.Shapes;
using MongoDB.Driver.Builders;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.ComponentModel;
using System.Threading;

namespace Books_Sorting2
{
    public partial class MainWindow : Window
    {
        List<BookDetails2> call_number = new List<BookDetails2>();
        Random range = new Random();
        NextDouble nextDouble = new NextDouble();
        public MainWindow()
        {
            InitializeComponent();
            AddUnsortedCollection();
        }
        private void AddUnsortedCollection()
        {
            call_number.Add(new BookDetails2(nextDouble.NextDouble2(range, 005.73, 050.73).ToString("000.00"), "SIG"));
            call_number.Add(new BookDetails2(nextDouble.NextDouble2(range, 005.73, 050.73).ToString("000.00"), "DAN"));
            call_number.Add(new BookDetails2(nextDouble.NextDouble2(range, 005.73, 050.73).ToString("000.00"), "CKA"));
            call_number.Add(new BookDetails2(nextDouble.NextDouble2(range, 005.73, 050.73).ToString("000.00"), "SOS"));
            call_number.Add(new BookDetails2(nextDouble.NextDouble2(range, 005.73, 050.73).ToString("000.00"), "ABR"));
            call_number.Add(new BookDetails2(nextDouble.NextDouble2(range, 005.73, 050.73).ToString("000.00"), "CON"));
            call_number.Add(new BookDetails2(nextDouble.NextDouble2(range, 005.73, 050.73).ToString("000.00"), "GLO"));
            call_number.Add(new BookDetails2(nextDouble.NextDouble2(range, 005.73, 050.73).ToString("000.00"), "MAN"));
            call_number.Add(new BookDetails2(nextDouble.NextDouble2(range, 005.73, 050.73).ToString("000.00"), "MBA"));
            call_number.Add(new BookDetails2(nextDouble.NextDouble2(range, 005.73, 050.73).ToString("000.00"), "JAM"));

            foreach (var item in call_number)
            {
                listName_ID.Items.Add(item.BookNumber + ": " + item.AuthorDetails);
            }
        }
        private void AddCollection()
        {
            listName_ID.Items.Clear();
            //Orders the call numbers in lowest to largest 
            var orderByDescendingResult = from s in call_number
                                          orderby s.BookNumber ascending
                                          select s;

            foreach (var item in orderByDescendingResult)
            {
                listName_ID.Items.Add(item.BookNumber + ": " + item.AuthorDetails);
            }
        }
        async Task UseDelay()
        {
            await Task.Delay(1000);
            AddCollection();
            await Task.Delay(1000);
            CorrectCallNumberMessage();
            await Task.Delay(1000);
            Compare();
        }
        private void CorrectCallNumberMessage()
        {
            MessageBox.Show("The Call Numbers Have Been Sorted Correctly", "CORRECT");
        }
        //Shows the first range then compares it to the new one
        private void Compare()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in call_number)
            {
                sb.AppendLine(item.BookNumber +  ": " + item.AuthorDetails);
            }
            MessageBox.Show(sb.ToString(), "Unsorted Call Number Version");
        }
        //ProgressBar Status
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBarStatus.Value = e.ProgressPercentage;
            ProgressTextBlock.Text = e.UserState.ToString();
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            worker.ReportProgress(0, String.Format("Processing Iteration 1."));
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                worker.ReportProgress((i + 1) * 10, String.Format("Processing Iteration {0}", i + 2));
            }
            worker.ReportProgress(100, "Done Processing");
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Done Processing");
            ProgressBarStatus.Value = 0;
            ProgressTextBlock.Text = "";
            UseDelay();
        }
        //Button that compares 
        private void DoWorkButton_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
            DoWorkButton.Visibility = Visibility.Hidden;
        }
        //When you tap on the power icon the program ends
        private void id_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void TabablzControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }
    }
}
