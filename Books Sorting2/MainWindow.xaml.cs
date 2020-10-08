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
    /// <summary>
    /// 17395647 Sbongiseni Mbaraga
    /// </summary>
    public partial class MainWindow : Window
    {
        //Stored all the call numbers into the list
        List<BookDetails2> call_number = new List<BookDetails2>();
        Random range = new Random();
        NextDouble nextDouble = new NextDouble();
        Dictionary<int, string> CallNumbersAndClassificationDictionary = new Dictionary<int, string>();
        int[] sortRandomly = {0, 100, 200, 300, 400, 500, 600, 700, 800, 900};

        public MainWindow()
        {
            InitializeComponent();
            AddUnsortedCollection();
            add();
        }
        //adds all the call numbers and randomly generate their number within a range then displays it
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
        //delays the methods 
        async Task UseDelay()
        {
            await Task.Delay(1000);
            AddCollection();
            await Task.Delay(1000);
            CorrectCallNumberMessage();
            await Task.Delay(1000);
            Compare();
        }
        //verification message that tells the user that the call number has been added
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
        //When you select a index you will get a message box
        private void TabablzControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }
        //adds all the data needed to be displayed and displays it too the user
        private void add()
        {
            CallNumberAndClassification();
            SortsCallNumbersRandomly();
        }
        //works when the user clicks on the view
        private void listViewID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        //Adds callnumbers and classification into dictionary then outputs the classification to listview
        private void CallNumberAndClassification()
        {

            CallNumbersAndClassificationDictionary.Add(0, "General Knowlege");
            CallNumbersAndClassificationDictionary.Add(100, "Philosophy");
            CallNumbersAndClassificationDictionary.Add(200, "Religion");
            CallNumbersAndClassificationDictionary.Add(300, "Social Science");
            CallNumbersAndClassificationDictionary.Add(400, "Language");
            CallNumbersAndClassificationDictionary.Add(500, "Science");
            CallNumbersAndClassificationDictionary.Add(600, "Technology");
            CallNumbersAndClassificationDictionary.Add(700, "Art and Recreation");
            CallNumbersAndClassificationDictionary.Add(800, "Literature");
            CallNumbersAndClassificationDictionary.Add(900, "History and Geography");

            foreach (var item in CallNumbersAndClassificationDictionary)
            {
                listViewID.Items.Add(item.Value);
            }

        }
        //Shuffles the data within the array then adds it too the call number combobox
        private void SortsCallNumbersRandomly()
        {
            ShuffleArray shuffleArray = new ShuffleArray();

            shuffleArray.ShuffleArray2(sortRandomly);
            foreach (var item in sortRandomly)
            {
                CallNumberItems.Items.Add(item);
                CallNumberItems2.Items.Add(item);
                CallNumberItems3.Items.Add(item);
                CallNumberItems4.Items.Add(item);
            }
        }
        //The Check button, starts the progress bar then executes the check function
        private async void BtnID_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(i.ToString());
            }
            var progress = new Progress<ProgressReport>();
            progress.ProgressChanged += (o, report) =>
             {
                 ProgressBarStatus2.Value = report.PercentComplete;
             };
            await ProcessData(list, progress);
            ChecksIfItemIsCorrect();
        }
        //Count in the background
        private Task ProcessData(List<string> list, IProgress<ProgressReport> progress)
        {
            int index = 1;
            int totalProcess = list.Count;
            var progressReport = new ProgressReport();
            return Task.Run(() =>
            {
                for (int i = 0; i < totalProcess; i++)
                {
                    progressReport.PercentComplete = index++ * 100 / totalProcess;
                    progress.Report(progressReport);
                    Thread.Sleep(2);
                }
            });
        }
        //Saves the Muiltiply selected description into an arraylist then compares it too the callnumber and classification dictionary and see which in is corret
        private void ChecksIfItemIsCorrect()
        {
            ArrayList saveSelectedITems = new ArrayList();

            foreach (var item in listViewID.SelectedItems)
            {
                saveSelectedITems.Add(item + "");
            }
            foreach (var item2 in saveSelectedITems)
            {
                foreach (var item3 in CallNumbersAndClassificationDictionary)
                {
                    if (item2.Equals(item3.Value))
                    {
                        if (CallNumberItems.SelectedItem.Equals(item3.Key) || CallNumberItems2.SelectedItem.Equals(item3.Key) || CallNumberItems3.SelectedItem.Equals(item3.Key) || CallNumberItems4.SelectedItem.Equals(item3.Key))
                        {
                            MessageBox.Show("Correct Answer CallNumber: " + item3.Key + " Description: " + item3.Value);
                        }
                    }
                }
            }
        }
    }
}
