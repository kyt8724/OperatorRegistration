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
using OperatorRegistration.Entities;
using OperatorRegistration.Utilities;
using OperatorRegistration.Repositories;
using System.Data.SqlClient;
using System.IO;

namespace OperatorRegistration
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 25 Jan 2012  
    /// Last Modified Date : 9 Feb 2012
    /// Trainee Registration Upload Window
    /// </summary>

    public partial class Upload : Window
    {
        private bool dbLogin = false;
        private PersonAndDbidsOperators Trainees { get; set; }
        private List<Trainee> TraineeToUpload { get; set; }
        private List<Trainee> TraineeByDate { get; set; }

        public Upload()
        {
            InitializeComponent();
            GetOperators();
        }

        public void BindTree(List<TrainingDate> dateList)
        {
            trvOperatorsByDate.ItemsSource = dateList;
        }

        #region Get and show Datas

        private void GetOperators()
        {
            DeserializeXml dx = new DeserializeXml();
            Trainees = dx.DeserializePersons();
            DateTreeView dv = new DateTreeView();
            if (Trainees != null)
            {
                var listOperator = dv.GetDates(Trainees);
                BindTree(listOperator);
            }
        }

        private void ShowTraineesList(object sender)
        {
            var types = ((TreeView)sender).SelectedItem;
            DeserializeXml dx = new DeserializeXml();
            PersonAndDbidsOperators allTrainees = dx.DeserializePersons();
            DateTreeView dv = new DateTreeView();

            if (types != null)
            {
                TrainingDate traningDate = types as TrainingDate;
                if (traningDate != null)
                {
                    var traineesByDate = allTrainees.Persons.Where(p => p.LastTrainedDate.Equals(traningDate.DateOfTraining)).Select(p => p).ToList();
                    GetTrainees(traineesByDate);
                    lbxTrainee.ItemsSource = TraineeByDate;
                }
            }
        }

        private void GetTrainees(List<PersonAndDbidsOperators.Person> operators)
        {
            TraineeByDate = new List<Trainee>();
            Trainee trainee;
            DeserializeXml dx = new DeserializeXml();

            foreach (PersonAndDbidsOperators.Person person in operators)
            {
                DateTime dateOfBirth;
                if (!string.IsNullOrEmpty(person.DateOfBirth))
                    dateOfBirth = DateTime.Parse(person.DateOfBirth);
                else
                    dateOfBirth = DateTime.Now;

                DateTime regDate = DateTime.Parse(person.RegDate);
                DateTime lastTrainedDate = DateTime.Parse(person.LastTrainedDate);
                Byte[] buffer = null;
                ImageConverter ic = new ImageConverter();
                buffer = ic.FromFileToByte(person.Photo);
                var installation = dx.DeserializeInstallation().InstallationOptions.Where(i => i.InstallationCode.Equals(person.Installation)).Select(i => i.InstallationName).First();

                trainee = new Trainee
                {
                    PID = person.PID,
                    LastName = person.LastName,
                    HomePhone = person.HomePhone,
                    MobilePhone = person.MobilePhone,
                    FirstName = person.FirstName,
                    MiddleName = person.MiddleName,
                    DateOfBirth = dateOfBirth,
                    Gender = person.Gender,
                    OfficePhone = person.OfficePhone,
                    Fax = person.Fax,
                    Photo = buffer,
                    Email = person.Email,
                    RankID = int.Parse(person.RankID),
                    OfficeCode = person.OfficeCode,
                    TypeOfPID = person.TypeOfPID,
                    RegDate = regDate,
                    JobTitle = person.JobTitle,
                    PersonRemarks = person.PersonRemarks,
                    OperatorType = person.OperatorType,
                    Installation = person.Installation,
                    InstallationName = installation.ToString(),
                    LastTrainedDate = lastTrainedDate,
                    Nationality = person.Nationality
                };
                TraineeByDate.Add(trainee);
            }
        }

        // Show the office information
        //private void ShowOfficeInformation()
        //{
        //    SearchOffice so = new SearchOffice();
        //    so.ShowDialog();

        //    if (so.DialogResult == true)
        //    {
        //        // show office information bottom of the selected list
        //        txbOfficeName.Text = so.fsOffice.OfficeName;
        //        txbOfficeCode.Text = so.fsOffice.OFFICECODE;
        //        txbInstallationName.Text = so.fsInstallation.INSTALLATIONNAME;
        //    }
        //}

        // Clear the office information
        private void ClearOfficeInformation()
        {
            //txbOfficeName.Text = string.Empty;
            //txbOfficeCode.Text = string.Empty;
            //txbInstallationName.Text = string.Empty;
        }

        // Display the list box
        private void DisplayItemsOnListBox()
        {
            lbxTrainee.ItemsSource = null;
            lbxUploading.ItemsSource = null;
            lbxTrainee.ItemsSource = TraineeByDate;
            lbxUploading.ItemsSource = TraineeToUpload;
        }

        #endregion Get Datas

        #region Insert, Update, Delete

        private void InsertOrUpdateData()
        {
            ProgressBarForProcess progressBarforProcess = new ProgressBarForProcess(this, TraineeToUpload);
            progressBarforProcess.ShowDialog();
            SerializeXml sx = new SerializeXml();
            sx.WriteXml(Trainees);
            GetOperators();
            MessageBox.Show("Completed to upload");
            lbxUploading.ItemsSource = null;
            lbxUploading.ItemsSource = TraineeToUpload;
        }

        // Delegate to remove trainee from xml
        private delegate void RemoveTraineeFromXMLDelegate(Trainee trainee, bool isSuccess);

        public void RemoveTraineeFromXML(object sender, Trainee trainee, bool isSuccess)
        {
            Dispatcher.Invoke(new RemoveTraineeFromXMLDelegate(RemoveTraineeFromXML), trainee, isSuccess);
        }

        // Remove trainee data from XML
        private void RemoveTraineeFromXML(Trainee trainee, bool isSuccess)
        {
            var trainedPerson = Trainees.Persons.Where(p => p.PID.Equals(trainee.PID)).Select(p => p).First();
            if (File.Exists(trainedPerson.Photo))
            {
                try
                {
                    File.Delete(trainedPerson.Photo);
                }
                catch
                {
                    MessageBox.Show(string.Format("{0}'s photo couldn't remove.", trainedPerson.LastName), "Image File Delete", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            Trainees.Persons.Remove(trainedPerson);
            TraineeToUpload.Remove(trainee);
        }

        // Save data to XML file after upload
        private void SaveTraineesToXML()
        {
            try
            {
                SerializeXml sx = new SerializeXml();
                sx.WriteXml(Trainees);
            }
            catch
            {
                MessageBox.Show("Trainees' data couldn't save.", "Save Data to XML File", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion Insert, Update, Delete

        #region Toggle


        // Check whether db connect
        private void DbConnectCheck()
        {
            StringBuilder errorMessages = new StringBuilder();
            try
            {
                if (!dbLogin)
                {

                    // Check user privilege
                    secdivDataContext db = new secdivDataContext();

                    if (db.DatabaseExists() == true)
                    {
                        LogIn login = new LogIn(db.DatabaseExists());
                        // Toggle upload and office button

                        if (login.ShowDialog() == true)
                            dbLogin = true;
                    }
                    else
                        MessageBox.Show("Database couldn't find!!");
                }
            }
            catch (SqlException ex)
            {
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");

                }
                MessageBox.Show(errorMessages.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Toggle upload and office button
        private void ToggleUploadButton()
        {
            if (lbxUploading.ItemsSource != null)
            {
                //btnOffice.IsEnabled = true;
                btnUpload.IsEnabled = true;
                imgNetworkStatus.Source = new BitmapImage(new Uri(@"common\images\connectnetwork.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                //btnOffice.IsEnabled = false;
                btnUpload.IsEnabled = false;
                imgNetworkStatus.Source = new BitmapImage(new Uri(@"common\images\disconnectnetwork.png", UriKind.RelativeOrAbsolute));
                ClearOfficeInformation();
            }
        }

        #endregion Toggle

        #region EventHandler

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        #endregion EventHandler

        private void trvOperatorsByDate_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                ShowTraineesList(sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Close this window
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Upload selected records to server
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(txbOfficeCode.Text))
            //{
            //    MessageBox.Show("Please click the 'Office' button and select an office!!", "Select Office", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //else
            //{
                // Xml to Sql Database
                if (TraineeToUpload != null)
                    InsertOrUpdateData();
                else
                    MessageBox.Show("Please select trainees to update!!", "Select Trainee", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        // Move selected trainees to upload list
        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            DbConnectCheck();

            if (dbLogin)
            {
                foreach (Trainee trainee in lbxTrainee.SelectedItems.OfType<Trainee>().ToList())
                {
                    if (TraineeByDate != null)
                        TraineeByDate.Remove(trainee);
                    if (TraineeToUpload == null)
                    {
                        TraineeToUpload = new List<Trainee>();
                    }
                    TraineeToUpload.Add(trainee);
                    DisplayItemsOnListBox();
                }

                if (TraineeToUpload != null)
                {
                    ToggleUploadButton();
                }
            }
        }

        // Move all trainees to upload list
        private void btnMoveAll_Click(object sender, RoutedEventArgs e)
        {
            DbConnectCheck();

            if (dbLogin)
            {
                if (lbxTrainee.ItemsSource != null)
                {
                    if (TraineeToUpload == null)
                        TraineeToUpload = new List<Trainee>();
                    foreach (Trainee trainee in TraineeByDate.ToList())
                    {
                        TraineeToUpload.Add(trainee);
                        TraineeByDate.Remove(trainee);
                    }
                    DisplayItemsOnListBox();
                }
                else
                {
                    MessageBox.Show("No records, please select a date");
                }

                if (TraineeToUpload != null)
                {
                    ToggleUploadButton();
                }
            }
        }

        // Remove selected trainees from upload list
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            foreach (Trainee trainee in lbxUploading.SelectedItems.OfType<Trainee>().ToList())
            {
                if (TraineeToUpload != null)
                    TraineeToUpload.Remove(trainee);
                if (TraineeByDate == null)
                    TraineeByDate = new List<Trainee>();
                TraineeByDate.Add(trainee);
                DisplayItemsOnListBox();
                ToggleUploadButton();
            }
        }

        // Remove all trainees from upload list
        private void btnRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            if (lbxUploading.ItemsSource != null)
            {
                if (TraineeByDate == null)
                    TraineeByDate = new List<Trainee>();
                foreach (Trainee trainee in TraineeToUpload.ToList())
                {
                    TraineeByDate.Add(trainee);
                    TraineeToUpload.Remove(trainee);
                }
                DisplayItemsOnListBox();
                ToggleUploadButton();
            }
            else
            {
                MessageBox.Show("No records, please select a date");
            }
        }

        //private void btnOffice_Click(object sender, RoutedEventArgs e)
        //{
        //    ShowOfficeInformation();
        //}
    }
}
