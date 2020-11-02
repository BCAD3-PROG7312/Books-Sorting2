using MongoDB.Driver.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.Forms.MessageBox;

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
        int[] sortRandomly = { 0, 100, 200, 300, 400, 500, 600, 700, 800, 900 };
        List<string> ClassificationData = new List<string>();
        public static TreeOne<ClassData> Classification = new TreeOne<ClassData>();
        ShuffleArray shuffleArray = new ShuffleArray();
        static bool clickedAlt = false;

        int LevelTwoIndex = 0;
        int LevelOneIndex = 0;
        List<string> getFirstLevelDescriptionRandomly = new List<string>();
        List<string> getSecondDescriptionRandomly = new List<string>();
        List<string> getThirdDescriptionRandomly = new List<string>();

        int indexq = 0;

        List<string> getFirstLevelDescriptionRandomly2 = new List<string>();
        List<string> getSecondDescriptionRandomly2 = new List<string>();
        List<string> getThirdDescriptionRandomly2 = new List<string>();

        List<double> GetCallNumOfEachRadioButton = new List<double>();
        string GetCallDesc = "";
        string GetCallNum = "";
        int CountPoints = 1;

        int count = 0;

        public MainWindow()
        {
            InitializeComponent();
            AddUnsortedCollection();
            add();
            ReadTextFile();
            AddDataFromListToNode();
            LevelOne();
            ButtonHidden();
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
            UserOrderID.Visibility = Visibility.Hidden;
        }
        //Orders the call numbers in lowest to largest 
        private void AddCollection()
        {
            listName_ID.Items.Clear();
            var orderByDescendingResult = from s in call_number
                                          orderby s.BookNumber ascending
                                          select s;

            foreach (var item in orderByDescendingResult)
            {
                listName_ID.Items.Add(item.BookNumber + ": " + item.AuthorDetails);
            }
        }
        //Button the user can click to reorder the call numbers
        private void UserOrderID_Click(object sender, RoutedEventArgs e)
        {
            AddCollection();
            CorrectCallNumberMessage();
        }
        //delays the methods 
        async Task UseDelay()
        {
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
                sb.AppendLine(item.BookNumber + ": " + item.AuthorDetails);
            }
            MessageBox.Show(sb.ToString(), "Unsorted Call Number Version");
            MessageBox.Show("You may now sort the call numbers");
            UserOrderID.Visibility = Visibility.Visible;
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
        //Refresh button allows the user to keep practising 
        private void RefreshID_Click(object sender, RoutedEventArgs e)
        {
            if (clickedAlt == false)
            {
                listViewCallNumID.Items.Clear();
                listViewID.Items.Clear();

                shuffleArray.ShuffleArray2(sortRandomly);
                foreach (var item in sortRandomly)
                {
                    foreach (var item2 in CallNumbersAndClassificationDictionary)
                    {
                        if (item == item2.Key)
                        {
                            listViewID.Items.Add(item2.Value);
                        }
                    }
                }
                SortsCallNumbersRandomly();
            } else
            {
                listViewCallNumID.Items.Clear();
                listViewID.Items.Clear();

                shuffleArray.ShuffleArray2(sortRandomly);
                foreach (var item in sortRandomly)
                {
                    foreach (var item2 in CallNumbersAndClassificationDictionary)
                    {
                        if (item == item2.Key)
                        {
                            listViewCallNumID.Items.Add(item2.Value);
                        }
                    }
                }
                shuffleArray.ShuffleArray2(sortRandomly);
                foreach (var item in sortRandomly)
                {
                    foreach (var item2 in CallNumbersAndClassificationDictionary)
                    {
                        if (item == item2.Key)
                        {
                            listViewID.Items.Add(item2.Key);
                        }
                    }
                }
            }
        }
        //Shuffles the data within the array then adds it too the call number combobox
        private void SortsCallNumbersRandomly()
        {
            shuffleArray.ShuffleArray2(sortRandomly);
            foreach (var item in sortRandomly)
            {
                listViewCallNumID.Items.Add(item);
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

            if (clickedAlt == false)
            {
                ChecksIfItemIsCorrect();
            }
            else
            {
                ChecksIfItemIsCorrect2();
            }
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
            List<string> value = new List<string>();
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
                        value.Add("The correct value: " + item3.Value + " The correct call number: " + item3.Key);
                    }
                }
            }
            string toDisplay = string.Join(Environment.NewLine, value);
            MessageBox.Show(toDisplay);
        }
        private void ChecksIfItemIsCorrect2()
        {
            List<string> value = new List<string>();
            ArrayList saveSelectedITems = new ArrayList();

            foreach (var item in listViewCallNumID.SelectedItems)
            {
                saveSelectedITems.Add(item + "");
            }
            foreach (var item2 in saveSelectedITems)
            {
                foreach (var item3 in CallNumbersAndClassificationDictionary)
                {
                    if (item2.Equals(item3.Value))
                    {
                        value.Add("The correct value: " + item3.Value + " The correct call number: " + item3.Key);
                    }
                }
            }
            string toDisplay = string.Join(Environment.NewLine, value);
            MessageBox.Show(toDisplay);
        }
        //Altarnate button, it altarnate between the call number and call description
        private void AltinateID_Click(object sender, RoutedEventArgs e)
        {
            if (clickedAlt == false)
            {
                listViewCallNumID.Items.Clear();
                listViewID.Items.Clear();

                foreach (var item in CallNumbersAndClassificationDictionary)
                {
                    listViewCallNumID.Items.Add(item.Value);
                }

                shuffleArray.ShuffleArray2(sortRandomly);
                foreach (var item in sortRandomly)
                {
                    listViewID.Items.Add(item);
                }
                clickedAlt = true;
            }
            else
            {
                listViewCallNumID.Items.Clear();
                listViewID.Items.Clear();

                foreach (var item in CallNumbersAndClassificationDictionary)
                {
                    listViewID.Items.Add(item.Value);
                }

                shuffleArray.ShuffleArray2(sortRandomly);
                foreach (var item in sortRandomly)
                {
                    listViewCallNumID.Items.Add(item);
                }
                clickedAlt = false;
            }
        }
        //Adds the data from the textfile to a list
        private void ReadTextFile()
        {
            var fileStream = new FileStream(@"C:\\Users\\Sbongiseni\\Documents\\Degree in Computer Science in Application Development\\Year 3\\Samester 2\\PROG7312\\17395647_Task_One\\Books Sorting2\\Books Sorting2\\ClassificationData\\data.txt", FileMode.Open, FileAccess.Read);

            using (var streaRead = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;

                while ((line = streaRead.ReadLine()) != null)
                {
                    ClassificationData.Add(line);
                }
            }
        }
        //Adds data into the Node
        private void AddDataFromListToNode()
        {
            if (Classification.Root == null)
            {
                Classification.Root = new TreeNode<ClassData>();
            }
            int counter = 0;

            foreach (var item in ClassificationData)
            {

                string[] input = item.Split(' ');
                string callnumber = input[0];
                string description = "";

                //Gets the description
                for (int i = 1; i < input.Length; i++)
                {
                    description = description + " " + input[i];
                }

                if (callnumber.Substring(1, 2).Equals("00"))
                {
                    LevelOne(input[0], input[1]);
                    LevelOneIndex++;
                    LevelTwoIndex = 0;
                    LevelTwo(input[0], input[1]);

                    //Gets the first level of the description
                    getFirstLevelDescriptionRandomly.Add(callnumber + " " + description);
                    getFirstLevelDescriptionRandomly2.Add(callnumber);
                }
                else if (callnumber.Substring(2).Equals("0"))
                {
                    LevelTwoIndex++;
                    LevelTwo(input[0], input[1]);
                    getSecondDescriptionRandomly.Add(callnumber + " " + description);
                    getSecondDescriptionRandomly2.Add(callnumber);
                }
                else
                {
                    LevelThree(input[0], input[1]);
                    getThirdDescriptionRandomly.Add(callnumber + " " + description);
                    getThirdDescriptionRandomly2.Add(callnumber);
                    counter++;
                }
            }
        }
        private void LevelOne(string callnum, string description)
        {
            if (Classification.Root.Children == null)
            {
                Classification.Root.Children = new List<TreeNode<ClassData>>()
                {
                    new TreeNode<ClassData>()
                    {
                        Data = new ClassData(callnum, description)
                    }
                };
            }
            else
            {
                Classification.Root.Children.Add(new TreeNode<ClassData>()
                {
                    Data = new ClassData(callnum, description)
                }
                );
            }
        }
        private void LevelTwo(string callnum, string description)
        {
            if (Classification.Root.Children[(LevelOneIndex - 1)].Children == null)
            {
                Classification.Root.Children[(LevelOneIndex - 1)].Children = new List<TreeNode<ClassData>>()
                {
                    new TreeNode<ClassData>()
                    {
                        Data = new ClassData(callnum, description),
                        Parent = Classification.Root.Children[(LevelOneIndex - 1)].Data
                    }
                };
            }
            else
            {
                Classification.Root.Children[(LevelOneIndex - 1)].Children.Add(new TreeNode<ClassData>()
                {
                    Data = new ClassData(callnum, description),
                    Parent = Classification.Root.Children[(LevelOneIndex - 1)].Data
                }
                );
            }
        }
        private void LevelThree(string callnum, string description)
        {
            if (Classification.Root.Children[(LevelOneIndex - 1)].Children[LevelTwoIndex].Children == null)
            {
                Classification.Root.Children[(LevelOneIndex - 1)].Children[LevelTwoIndex].Children = new List<TreeNode<ClassData>>()
                {
                    new TreeNode<ClassData>()
                    {
                        Data = new ClassData(callnum, description),
                        Parent = Classification.Root.Children[(LevelOneIndex - 1)].Children[LevelTwoIndex].Data
                    }
                };
            }
            else
            {
                Classification.Root.Children[(LevelOneIndex - 1)].Children[LevelTwoIndex].Children.Add(new TreeNode<ClassData>()
                {
                    Data = new ClassData(callnum, description),
                    Parent = Classification.Root.Children[(LevelOneIndex - 1)].Children[LevelTwoIndex].Data
                }
                );
            }
        }
        //Gets the level One
        private void LevelOne()
        {
            Random random = new Random();
            NextDouble next = new NextDouble();

            //Randomly Generat the 
            bool check = true;

            int lvOne = 0;
            int lvTwo = 0;
            int lvThree = 0;

            while (check == true)
            {
                lvOne = (int)next.NextDouble2(random, 0, 9);
                lvTwo = (int)next.NextDouble2(random, 0, 9);
                lvThree = (int)next.NextDouble2(random, 0, 9);

                try
                {
                    thirdLevelID.Content = Classification.Root.Children[lvOne].Children[lvTwo].Children[lvThree].Data.Description;
                    GetCallNum = Classification.Root.Children[lvOne].Children[lvTwo].Children[lvThree].Data.CallNumber;
                    GetCallDesc = thirdLevelID.Content.ToString();
                    check = false;
                }
                catch (Exception)
                {

                }
            }

            int counter = 1;
            ArrayList arrayLevelOne = new ArrayList();
            arrayLevelOne.Add(lvOne);


            while (counter < 4)
            {
                lvOne = (int)next.NextDouble2(random, 0, 9);

                if (!arrayLevelOne.Contains(lvOne))
                {
                    arrayLevelOne.Add(lvOne);
                    counter++;
                }
            }
            arrayLevelOne.Sort();

            ArrayList arrayLevelOneStrings = new ArrayList();


            foreach (int item in arrayLevelOne)
            {
                arrayLevelOneStrings.Add(Classification.Root.Children[item].Data.Description);
            }

            levelThreeOptionOne.Content = arrayLevelOne[0] + "00 " + arrayLevelOneStrings[0];
            levelThreeOptionTwo.Content = arrayLevelOne[1] + "00 " + arrayLevelOneStrings[1];
            levelThreeOptionThree.Content = arrayLevelOne[2] + "00 " + arrayLevelOneStrings[2];
            levelThreeOptionFour.Content = arrayLevelOne[3] + "00 " + arrayLevelOneStrings[3];
        }
        //Gets the level Two
        private void LevelTwo()
        {
            Random random = new Random();
            NextDouble next = new NextDouble();

            //Randomly Generat the 
            bool check = true;

            int lvOne = 0;
            int lvTwo = 0;
            int lvThree = 0;

            while (check == true)
            {
                lvOne = (int)next.NextDouble2(random, 0, 9);
                lvTwo = (int)next.NextDouble2(random, 0, 9);
                lvThree = (int)next.NextDouble2(random, 0, 9);

                try
                {
                    thirdLevelID.Content = Classification.Root.Children[lvOne].Children[lvTwo].Children[lvThree].Data.Description;
                    GetCallNum = Classification.Root.Children[lvOne].Children[lvTwo].Children[lvThree].Data.CallNumber;
                    GetCallDesc = thirdLevelID.Content.ToString();
                    check = false;
                }
                catch (Exception)
                {

                }
            }

            int counter = 1;
            ArrayList arrayLevelOne = new ArrayList();
            arrayLevelOne.Add(lvTwo);


            while (counter < 4)
            {
                lvTwo = (int)next.NextDouble2(random, 0, 9);

                if (!arrayLevelOne.Contains(lvTwo))
                {
                    arrayLevelOne.Add(lvTwo);
                    counter++;
                }
            }
            arrayLevelOne.Sort();

            ArrayList arrayLevelOneStrings = new ArrayList();

            foreach (int item in arrayLevelOne)
            {
                arrayLevelOneStrings.Add(Classification.Root.Children[lvOne].Children[item].Data.Description);
            }

            levelThreeOptionOne.Content = lvOne.ToString() + arrayLevelOne[0] + "0 " + arrayLevelOneStrings[0];
            levelThreeOptionTwo.Content = lvOne.ToString() + arrayLevelOne[1] + "0 " + arrayLevelOneStrings[1];
            levelThreeOptionThree.Content = lvOne.ToString() + arrayLevelOne[2] + "0 " + arrayLevelOneStrings[2];
            levelThreeOptionFour.Content = lvOne.ToString() + arrayLevelOne[3] + "0 " + arrayLevelOneStrings[3];
        }
        //Score gameafication feature
        private void levelThreeOptionOne_Click(object sender, RoutedEventArgs e)
        {
            if (levelThreeOptionOne.IsChecked == true)
            {
                string content = "";
                string content2 = "";
                string content3 = "";
                string content4 = "";

                content = levelThreeOptionOne.Content.ToString().Substring(0, 3);
                content2 = levelThreeOptionTwo.Content.ToString().Substring(0, 3);
                content3 = levelThreeOptionThree.Content.ToString().Substring(0, 3);
                content4 = levelThreeOptionFour.Content.ToString().Substring(0, 3);

                //Level One Call Numbers
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content));
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content2));
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content3));
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content4));

                double searchValue = Convert.ToDouble(GetCallNum);

                double nearest = GetCallNumOfEachRadioButton.Select(p => new { Value = p, Difference = Math.Abs(p - searchValue) })
                  .OrderBy(p => p.Difference)
                  .First().Value;

                string firstNumber = GetCallNum.Substring(0, 1);
                //string firstNumberOfNearestRadioNum = nearest.ToString().Substring(0, 1);
                string radioButtonOneNum = content.Substring(0, 1);


                if (radioButtonOneNum.Equals(firstNumber))
                {
                    MessageBox.Show("CORRECT!");
                    scoreNumID.Content = CountPoints++ + 100;
                    LevelTwoBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("INCORRECT Try Again");
                    LevelOne();
                }
            }
        }
        private void levelThreeOptionTwo_Click(object sender, RoutedEventArgs e)
        {
            if (levelThreeOptionTwo.IsChecked == true)
            {
                string content = "";
                string content2 = "";
                string content3 = "";
                string content4 = "";

                content = levelThreeOptionOne.Content.ToString().Substring(0, 3);
                content2 = levelThreeOptionTwo.Content.ToString().Substring(0, 3);
                content3 = levelThreeOptionThree.Content.ToString().Substring(0, 3);
                content4 = levelThreeOptionFour.Content.ToString().Substring(0, 3);

                //Level One Call Numbers
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content));
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content2));
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content3));
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content4));

                double searchValue = Convert.ToDouble(GetCallNum);

                double nearest = GetCallNumOfEachRadioButton.Select(p => new { Value = p, Difference = Math.Abs(p - searchValue) })
                  .OrderBy(p => p.Difference)
                  .First().Value;


                string firstNumber = GetCallNum.Substring(0, 1);
                //string firstNumberOfNearestRadioNum = nearest.ToString().Substring(0, 1);
                string radioButtonOneNum = content2.Substring(0, 1);


                if (radioButtonOneNum.Equals(firstNumber))
                {
                    MessageBox.Show("CORRECT!");
                    scoreNumID.Content = CountPoints++ + 100;
                    LevelTwoBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("INCORRECT Try Again");
                    LevelOne();
                }
            }
        }
        private void levelThreeOptionThree_Click(object sender, RoutedEventArgs e)
        {
            if (levelThreeOptionThree.IsChecked == true)
            {
                string content = "";
                string content2 = "";
                string content3 = "";
                string content4 = "";

                content = levelThreeOptionOne.Content.ToString().Substring(0, 3);
                content2 = levelThreeOptionTwo.Content.ToString().Substring(0, 3);
                content3 = levelThreeOptionThree.Content.ToString().Substring(0, 3);
                content4 = levelThreeOptionFour.Content.ToString().Substring(0, 3);

                //Level One Call Numbers
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content));
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content2));
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content3));
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content4));

                double searchValue = Convert.ToDouble(GetCallNum);

                double nearest = GetCallNumOfEachRadioButton.Select(p => new { Value = p, Difference = Math.Abs(p - searchValue) })
                  .OrderBy(p => p.Difference)
                  .First().Value;


                string firstNumber = GetCallNum.Substring(0, 1);
                //string firstNumberOfNearestRadioNum = nearest.ToString().Substring(0, 1);
                string radioButtonOneNum = content3.Substring(0, 1);


                if (radioButtonOneNum.Equals(firstNumber))
                {
                    MessageBox.Show("CORRECT!");
                    scoreNumID.Content = CountPoints++ + 100;
                    LevelTwoBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("INCORRECT Try Again");
                    LevelOne();
                }
            }
        }
        private void levelThreeOptionFour_Click(object sender, RoutedEventArgs e)
        {
            if (levelThreeOptionFour.IsChecked == true)
            {
                string content = "";
                string content2 = "";
                string content3 = "";
                string content4 = "";

                content = levelThreeOptionOne.Content.ToString().Substring(0, 3);
                content2 = levelThreeOptionTwo.Content.ToString().Substring(0, 3);
                content3 = levelThreeOptionThree.Content.ToString().Substring(0, 3);
                content4 = levelThreeOptionFour.Content.ToString().Substring(0, 3);

                //Level One Call Numbers
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content));
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content2));
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content3));
                GetCallNumOfEachRadioButton.Add(Convert.ToDouble(content4));

                double searchValue = Convert.ToDouble(GetCallNum);

                double nearest = GetCallNumOfEachRadioButton.Select(p => new { Value = p, Difference = Math.Abs(p - searchValue) })
                  .OrderBy(p => p.Difference)
                  .First().Value;


                string firstNumber = GetCallNum.Substring(0, 1);
                //string firstNumberOfNearestRadioNum = nearest.ToString().Substring(0, 1);
                string radioButtonOneNum = content4.Substring(0, 1);


                if (radioButtonOneNum.Equals(firstNumber))
                {
                    MessageBox.Show("CORRECT!");
                    scoreNumID.Content = CountPoints++ + 100;
                    LevelTwoBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("INCORRECT Try Again");
                    LevelOne();
                }
            }
        }
        //Sends the user to level two
        private void LevelTwoBtn_Click(object sender, RoutedEventArgs e)
        {
            LevelTwo();
        }
        //Restart Button
        private void RestartBtn_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Game Restarted Your Score will not be lost!");
            LevelOne();
        }
        //Hides the button the level two button
        private void ButtonHidden()
        {
            LevelTwoBtn.Visibility = Visibility.Hidden;
        }
    }
}