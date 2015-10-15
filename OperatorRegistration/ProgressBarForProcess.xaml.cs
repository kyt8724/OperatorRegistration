using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using OperatorRegistration.Entities;
using OperatorRegistration.Repositories;
using OperatorRegistration.Utilities;

namespace OperatorRegistration
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 6 Feb 2012  
    /// Last Modified Date : 9 Feb 2012
    /// Trainee Registration Upload Window
    /// </summary>
    public partial class ProgressBarForProcess : Window
    {
        private int Current { get; set; }
        private int Total { get; set; }
        private List<Trainee> Trainees { get; set; }
        //private string OfficeCode { get; set; }

        public ProgressBarForProcess()
        {
            InitializeComponent();
        }

        public ProgressBarForProcess(object sender, List<Trainee> trainees)
        {
            InitializeComponent();
            Current = 0;
            Total = trainees.Count();
            Upload upload = sender as Upload;
            ShowTraineesDelegate += upload.RemoveTraineeFromXML;
            Trainees = trainees;
            //OfficeCode = officeCode;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UploadTrainee();
        }

        // Update ProgressBar Value
        private delegate void UpdateProgressBarDelegate(DependencyProperty dp, object value);

        // Delegate for update UI
        private delegate void ProcessDelegate(int current);

        // Invoke Process
        public void Process(object sender)
        {
            Dispatcher.Invoke(new ProcessDelegate(Process), Current);
        }

        private void Process(int current)
        {
            // stores the value of the ProgressBar
            double value = ((Double)current / (Double)Total) * 100;

            // Create a new instance of our progressbar delegate that points
            // to the progressbar's setvalue method.
            UpdateProgressBarDelegate updatePbDelegate = new UpdateProgressBarDelegate(pgbSearch.SetValue);

            /*
             * Update the value of the progressbar:
             * 1) Pass the "updatePbDelegate" delegate that points to the pgbSearch.SetValue method
             * 2) set the DispatcherPriority to "Background"
             * 3) Pass an Object() Array containing the property to update (progressBar.ValueProperty) and the new value
             */
            Dispatcher.Invoke(updatePbDelegate, DispatcherPriority.Background, new object[] { ProgressBar.ValueProperty, value });
            pgbSearch.Value = value;
            if (value == 100)
            {
                this.Close();
            }
        }

        private void UploadTrainee()
        {
            bool insertPerson = false;
            bool insertPersonDetail = false;
            bool insertDbidsOperator = false;
            bool insertDbidsCertificate = false;
            bool insertTrainingHistory = false;
            bool insertUserAccount = false;
            bool updatePerson = false;
            bool updatepersonDetail = false;
            bool updateDbidsOperator = false;
            bool updateDbidsCertificate = false;
            bool updateUserAccount = false;
            int totalNo = Trainees.Count();

            secdivDataContext db = new secdivDataContext();
            foreach (Trainee trainee in Trainees.ToList())
            {
                InsertData id = new InsertData();
                var person = db.Persons.Where(p => p.PID.Equals(trainee.PID));
                if (person.Count() > 0)
                {
                    UpdateData ud = new UpdateData();
                    // update person
                    updatePerson = ud.UpdatePerson(trainee);
                    var personDetail = db.PersonDetails.Where(pd => pd.PID.Equals(trainee.PID)).Select(pd => pd.PID);
                    if (personDetail.Count() > 0)
                    {
                        // update person details
                        updatepersonDetail = ud.UpdatePersonDetail(trainee);
                    }
                    else
                    {
                        // insert person details
                        insertPersonDetail = id.InsertPersonDetail(trainee);
                    }

                    var dbidsOperator = db.DbidsOperators.Where(d => d.PID.Equals(trainee.PID)).Select(d => d.PID);
                    if (dbidsOperator.Count() > 0)
                    {
                        // update dbids operator
                        updateDbidsOperator = ud.UpdateDbidsOperator(trainee);
                    }
                    else
                    {
                        // insert dbids operator
                        insertDbidsOperator = id.InsertDbidsOperator(trainee);
                    }

                    var dbidsCertificates = db.DbidsCertificates.Where(d => d.OperatorPID.Equals(trainee.PID));
                    if (dbidsCertificates.Count() > 0)
                    {
                        updateDbidsCertificate = ud.UpdateDbidsCertificate(trainee);
                    }
                    else
                    {
                        insertDbidsCertificate = id.InsertDbidsCertificate(trainee);
                    }

                    var trainingHistories = db.TrainingHistories.Where(t => t.Trainee.Equals(trainee.PID) && t.TrainedDate.ToString().Substring(0, 10).Equals(trainee.LastTrainedDate.ToShortDateString().Substring(0, 10)));
                    if (trainingHistories.Count() > 0)
                    {
                        // update training history
                    }
                    else
                    {
                        // insert training history
                        insertTrainingHistory = id.InsertTrainingHistory(trainee);
                    }


                    var userAccount = db.UserAccounts.Where(u => u.PID.Equals(trainee.PID)).Select(u => u.PID);
                    if (userAccount.Count() > 0)
                    {
                        // update user account
                        updateUserAccount = ud.UpdateUserAccount(trainee);
                    }
                    else
                    {
                        // insert user account
                        insertUserAccount = id.InsertUserAccount(trainee);
                    }

                }
                else
                {
                    // insert person
                    insertPerson = id.InsertPerson(trainee);
                    if (insertPerson)
                    {
                        var personDetail = db.PersonDetails.Where(pd => pd.PID.Equals(trainee.PID)).Select(pd => pd.PID);
                        if (personDetail.Count() == 0)
                        {
                            // insert person details
                            insertPersonDetail = id.InsertPersonDetail(trainee);
                        }

                        var dbidsOperator = db.DbidsOperators.Where(d => d.PID.Equals(trainee.PID)).Select(d => d.PID);
                        if (dbidsOperator.Count() == 0)
                        {
                            // insert dbids operator
                            insertDbidsOperator = id.InsertDbidsOperator(trainee);
                        }

                        var dbidsCertificates = db.DbidsCertificates.Where(d => d.OperatorPID.Equals(trainee.PID));
                        if (dbidsCertificates.Count() == 0)
                        {
                            insertDbidsCertificate = id.InsertDbidsCertificate(trainee);
                        }


                        var trainingHistories = db.TrainingHistories.Where(t => t.Trainee.Equals(trainee.PID) && t.TrainedDate.ToString().Substring(0, 10).Equals(trainee.LastTrainedDate.ToShortDateString()));
                        if (trainingHistories.Count() == 0)
                        {
                            // insert training history
                            insertTrainingHistory = id.InsertTrainingHistory(trainee);
                        }


                        var userAccount = db.UserAccounts.Where(u => u.PID.Equals(trainee.PID)).Select(u => u.PID);
                        if (userAccount.Count() == 0)
                        {
                            // insert user account
                            insertUserAccount = id.InsertUserAccount(trainee);
                        }
                    }
                }
                if (insertPerson)
                {
                    if (insertDbidsOperator)
                    {
                        if (ShowTraineesDelegate != null)
                            ShowTraineesDelegate(this, trainee, true);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0}'s operator information couldn't upload", trainee.LastName),
                            "Data Upload", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else if (updatePerson)
                {
                    if (updateDbidsOperator)
                    {
                        if (ShowTraineesDelegate != null)
                            ShowTraineesDelegate(this, trainee, true);
                    }
                    else if (insertDbidsOperator)
                    {
                        if (ShowTraineesDelegate != null)
                            ShowTraineesDelegate(this, trainee, true);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0}'s operator information couldn't upload", trainee.LastName),
                            "Data Upload", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("{0}'s personal information couldn't upload", trainee.LastName),
                            "Data Upload", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Current++;
                Process(Current);
            }
        }

        public event ShowTraineesHandler ShowTraineesDelegate;
    }
    public delegate void ShowTraineesHandler(object sender, Trainee trainee, bool success);
}
