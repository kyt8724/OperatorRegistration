using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OperatorRegistration.Utilities;
using OperatorRegistration.Entities;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO.Ports;
using System.Threading;
using System.Windows.Threading;
using OperatorRegistration.Repositories1;

namespace OperatorRegistration
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 17 Jan 2012  
    /// Last Modified Date : 9 Feb 2012
    /// Trainee Registration Main Window
    /// </summary>
    public partial class MainWindow : Window
    {
        private PersonAndDbidsOperators Operators { get; set; }
        private OperatorRegistration.Entities.PersonAndDbidsOperators.Person Person { get; set; }
        private List<OtherOptions.EmailServerOption> EmailList { get; set; }
        private List<Trainee> TraineeBySelectedDate { get; set; }
        private List<OFFICE> Offices { get; set; }
        private SerialPort mySerialPort { get; set; }
        private delegate void SetText();

        private string PhotoFileName { get; set; }
        private bool isFromLocalDB = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeComboBox();
            GetOperators();
        }

        #region Get data by scanning certificate bar code

        private void InitializeSerialPort()
        {
            mySerialPort = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);

            try
            {
                mySerialPort.DataReceived += new SerialDataReceivedEventHandler(mySerialPort_DataReceived);
                if (!mySerialPort.IsOpen)
                {
                    mySerialPort.Open();
                }
            }
            catch
            {
                MessageBox.Show("Serial Port hasn't been ready yet");
            }
        }

        // When receive data thru serial port
        private void mySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(500);
            string data = mySerialPort.ReadExisting();
            ProcessBuffer(data);
        }

        private void ProcessBuffer(string buffer)
        {
            try
            {
                int start = buffer.IndexOf("-") + 1;
                int end = buffer.LastIndexOf("-");
                string certificateNo = string.Empty;
                string pid = string.Empty;
                Person = null;
                if (start == end)
                    MessageBox.Show("This is not a valid certificate!!");
                else
                    certificateNo = buffer.Substring(start, end - start);

                //MessageBox.Show(string.Format("Certification Number : {0}", certificateNo));

                if (!string.IsNullOrEmpty(certificateNo))
                {
                    GetPerson gp = new GetPerson();
                    pid = gp.SearchPersonByCertificate(certificateNo);
                    if (!String.IsNullOrEmpty(pid))
                    {
                        // Set Text by Thread
                        Thread thread = new Thread(new ThreadStart(delegate()
                        {
                            DispatcherOperation dispatcherOp = txtLastName.Dispatcher.BeginInvoke(
                                DispatcherPriority.Normal, new Action(
                                    delegate()
                                    {
                                        SetPerson(false, pid);
                                        if (Person != null)
                                            FillPersonInformation();
                                        else
                                            MessageBox.Show(string.Format("Record Not Found - Certification Number : {0}", certificateNo));
                                    }
                            ));
                            //        dispatcherOp.Completed += new EventHandler(dispatcherOp_Completed);
                        }
                        ));
                        thread.Start();
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("Record Not Found - Certification Number : {0}", certificateNo));
                }
            }
            catch
            {
                MessageBox.Show("Serial No is Invalid");
            }
        }

        #endregion Get data by scanning certificate bar code

        #region Get Trainees List

        public void BindTree(List<TrainingDate> dateList)
        {
            cbxTrainedDate.ItemsSource = dateList;
            cbxTrainedDate.DisplayMemberPath = "DateOfTraining";
            cbxTrainedDate.SelectedValuePath = "DateOfTraining";
        }

        // Get Operators
        private void GetOperators()
        {
            DeserializeXml dx = new DeserializeXml();
            Operators = dx.DeserializePersons();
            DateTreeView dv = new DateTreeView();

            if (Operators != null)
            {
                var dateList = dv.GetDates(Operators);
                BindTree(dateList);
            }
        }

        private void ShowTraineesList(object sender)
        {
            var dates = ((ComboBox)sender).SelectedItem;
            DeserializeXml dx = new DeserializeXml();
            PersonAndDbidsOperators allTrainees = dx.DeserializePersons();
            DateTreeView dv = new DateTreeView();

            if (dates != null)
            {
                TrainingDate traningDate = dates as TrainingDate;
                if (traningDate != null)
                {
                    var traineesByDate = allTrainees.Persons.Where(p => p.LastTrainedDate.Equals(traningDate.DateOfTraining)).Select(p => p).ToList();
                    GetTrainees(traineesByDate);
                    //lbxTraineeList.ItemsSource = TraineeBySelectedDate;
                    dgdFindPersonList.ItemsSource = TraineeBySelectedDate;
                    txbNoOfTrainees.Text = traineesByDate.Count.ToString();
                }
            }
        }

        // Get List of Trainees
        private void GetTrainees(List<PersonAndDbidsOperators.Person> operators)
        {
            TraineeBySelectedDate = new List<Trainee>();
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
                TraineeBySelectedDate.Add(trainee);
            }
        }

        //// Display the list box
        //private void DisplayItemsOnListBox()
        //{
        //    lbxTraineeList.ItemsSource = null;
        //    lbxTraineeList.ItemsSource = TraineeBySelectedDate;
        //}

        // 
        private void SelectTrainee(object sender)
        {
            if (sender is ListView)
            {
                Trainee trainee = (Trainee)((ListView)sender).SelectedItem;
                Person = null;
                if (Operators != null)
                {
                    var persons = Operators.Persons.Where(p => p.PID.Equals(trainee.PID)).Select(p => p);

                    if (persons.Count() > 0)
                    {
                        Person = persons.First();
                        FillPersonInformation();
                    }
                }
            }
            else
            {
                Trainee trainee = (Trainee)((DataGrid)sender).SelectedItem;
                Person = null;
                if (trainee != null)
                {
                    if (Operators != null)
                    {
                        var persons = Operators.Persons.Where(p => p.PID.Equals(trainee.PID)).Select(p => p);

                        if (persons.Count() > 0)
                        {
                            Person = persons.First();
                            FillPersonInformation();
                        }
                    }
                }
            }
        }

        #endregion Get Trainees list

        #region Initialize

        private void InitializeComboBox()
        {
            DeserializeXml dx = new DeserializeXml();
            OtherOptions otherOptions = dx.DeserializeOtherOptions();
            CountriesNameOptions cnOptions = dx.DeserializeCountry();
            RankServiceOptions rsOptions = dx.DeserializeRank();
            PersonalInfoOptions piOptions = dx.DeserialzeObject();
            InstallationNameOptions inOptions = dx.DeserializeInstallation();
            Operators = dx.DeserializePersons();
            EmailList = otherOptions.EmailServerOptions; ;

            cbxService.ItemsSource = rsOptions.RankOptions.Select(ro => ro.Service).Distinct();

            cbxTypeOfPID.ItemsSource = piOptions.IDTypeOptions;
            cbxTypeOfPID.DisplayMemberPath = "IDType";
            cbxTypeOfPID.SelectedValuePath = "Value";

            cbxNationality.ItemsSource = cnOptions.CountriesOptions;
            cbxNationality.DisplayMemberPath = "Name";
            cbxNationality.SelectedValuePath = "Code";

            cbxGender.ItemsSource = piOptions.GenderOptions;
            cbxGender.DisplayMemberPath = "Gender";
            cbxGender.SelectedValuePath = "Value";

            //cbxService.ItemsSource = rsOptions.RankOptions.Select(ro => ro.Service).Distinct();

            cbxOperatorType.ItemsSource = otherOptions.OperatorOptions;
            cbxOperatorType.DisplayMemberPath = "Operator";
            cbxOperatorType.SelectedValuePath = "Value";

            cbxEmailServer.ItemsSource = otherOptions.EmailServerOptions;
            cbxEmailServer.DisplayMemberPath = "EmailServer";
            cbxEmailServer.SelectedValuePath = "Value";
            cbxMobilePhoneArea.ItemsSource = piOptions.MobilePhoneCodes;
            cbxMobilePhoneArea.DisplayMemberPath = "MobilePhone";
            cbxMobilePhoneArea.SelectedValuePath = "Value";
            cbxOfficePhoneArea.ItemsSource = piOptions.PhoneAreaCodes;
            cbxOfficePhoneArea.DisplayMemberPath = "PhoneArea";
            cbxOfficePhoneArea.SelectedValuePath = "Value";
            //cbxFaxArea.ItemsSource = piOptions.PhoneAreaCodes;
            //cbxFaxArea.DisplayMemberPath = "PhoneArea";
            //cbxFaxArea.SelectedValuePath = "Value";
            cbxInstallation.ItemsSource = inOptions.InstallationOptions.OrderBy(i => i.InstallationName);
            cbxInstallation.DisplayMemberPath = "InstallationName";
            cbxInstallation.SelectedValuePath = "InstallationCode";

            // Set the Default value of the combo box
            cbxService.SelectedValue = "KSG";
            cbxNationality.SelectedValue = "KR";
            cbxGender.SelectedIndex = 0;
            cbxOperatorType.SelectedIndex = 1;
            cbxEmailServer.SelectedIndex = 7;
            cbxMobilePhoneArea.SelectedIndex = 0;
            cbxOfficePhoneArea.SelectedIndex = 0;
            cbxTypeOfPID.SelectedIndex = 0;
            //cbxFaxArea.SelectedIndex = 0;
            cbxInstallation.SelectedIndex = 11;
            dpkDateOfBirth.DisplayDate = DateTime.Now.AddYears(-25);

            imgDBIDSOperator.Source = ShowPhoto(@"XMLData\Photos.png");
            GetOffices();
        }

        //Show the office name to the List Box
        private void ShowOfficeNameToComboBox(List<OFFICE> offices)
        {
            cbxOffice.ItemsSource = offices;
            cbxOffice.DisplayMemberPath = "OfficeName";
            cbxOffice.SelectedValuePath = "OFFICECODE";
            cbxOffice.SelectedIndex = 0;
        }

        // Fill the Rank Combo Box When select a service
        private void ChangeRankComboBox(string typeService)
        {
            DeserializeXml dx = new DeserializeXml();
            var rsServices = dx.DeserializeRank().RankOptions.Where(rs => rs.Service == typeService);

            cbxRank.ItemsSource = rsServices;
            cbxRank.DisplayMemberPath = "RankGrade";
            cbxRank.SelectedValuePath = "RankID";
            cbxRank.SelectedIndex = 0;
        }

        // Set the PID by the type of PID
        private string SetPID()
        {
            string pid = string.Empty;
            switch (cbxTypeOfPID.Text)
            {
                case ("KID"):
                    if (!string.IsNullOrEmpty(txtPrefixKID.Text) && !string.IsNullOrEmpty(txtSuffixKID.Text))
                        pid = string.Format("{0}-{1}", txtPrefixKID.Text, txtSuffixKID.Text);
                    break;
                case ("SSN"):
                    if (!string.IsNullOrEmpty(txtPrefixSSN.Text) && !string.IsNullOrEmpty(txtMidSSN.Text) && !string.IsNullOrEmpty(txtSuffixSSN.Text))
                        pid = string.Format("{0}-{1}-{2}", txtPrefixSSN.Text, txtMidSSN.Text, txtSuffixSSN.Text);
                    break;
                case ("Passport"):
                    if (!string.IsNullOrEmpty(txtPassport.Text))
                        pid = txtPassport.Text;
                    break;
            }
            return pid;
        }

        // Validate the KID
        private void ValidateKID(object sender, KeyEventArgs e)
        {
            bool validateOfKID = false;
            ValidationOfKID validate = new ValidationOfKID();
            validateOfKID = validate.ValidateKID(string.Format("{0}{1}", txtPrefixKID.Text, txtSuffixKID.Text));
            if (validateOfKID)
            {
                dpkDateOfBirth.SelectedDate = DateTime.Parse(string.Format("19{0}-{1}-{2}", txtPrefixKID.Text.Substring(0, 2),
                    txtPrefixKID.Text.Substring(2, 2), txtPrefixKID.Text.Substring(4, 2)));
                MoveFocus(sender);
                e.Handled = true;
            }
            else
            {
                MessageBox.Show(string.Format("{0}-{1} is not a valid KID, Please type in the correct KID.", txtPrefixKID.Text, txtSuffixKID.Text));
                txtPrefixKID.Clear();
                txtSuffixKID.Clear();
                txtPrefixKID.Focus();
            }
        }

        //Move the focus to the next text box
        private void MoveTheFocus(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if ((((TextBox)sender).Name).Equals("txtSuffixKID"))
                    ValidateKID(sender, e);
                else if ((((TextBox)sender).Name).Equals("txtPrefixKID"))
                {
                    if (txtPrefixKID.Text.Length == 6)
                    {
                        MoveFocus(sender);
                        e.Handled = true;
                    }
                    else
                    {
                        txtPrefixKID.Clear();
                        txtPrefixKID.Focus();
                    }
                }
                else
                {
                    MoveFocus(sender);
                    e.Handled = true;
                }
            }
            else
            {
                if (((TextBox)sender).Text.Length == ((TextBox)sender).MaxLength)
                {
                    if ((((TextBox)sender).Name).Equals("txtSuffixKID") && txtSuffixKID.Text.Length == 7)
                        ValidateKID(sender, e);
                    else
                    {
                        MoveFocus(sender);
                        e.Handled = true;
                    }
                }
                else
                {
                    if (((TextBox)sender).Name.Equals("txtOfficePhonePrefix"))
                    {
                        if (cbxOfficePhoneArea.SelectedValue.ToString().Equals("DSN"))
                        {
                            if (txtOfficePhonePrefix.Text.Length == 3)
                            {
                                MoveFocus(sender);
                                e.Handled = true;
                            }
                        }
                    }
                }
            }
        }


        //Moving the focus when 
        private void MoveFocus(object sender)
        {
            switch (((TextBox)sender).Name)
            {
                case "txtPrefixKID":
                    if (!string.IsNullOrEmpty(txtPrefixKID.Text))
                        txtSuffixKID.Focus(); break;
                case "txtSuffixKID":
                    if (!string.IsNullOrEmpty(txtSuffixKID.Text))
                    {
                        txtLastName.Focus();
                        bool isExist = GetPersonData();
                        if (!isExist)
                        {
                            string typeOfClass = ((ComboBoxItem)cbxTypeOfClass.SelectedValue).Content.ToString();
                            if (typeOfClass.Equals("Initial"))
                            {
                                cbxService.SelectedValue = "KSG";
                                if (txtSuffixKID.Text.Substring(0, 1) == "1")
                                {
                                    cbxGender.SelectedValue = "Male";
                                }
                            }
                            else
                            {
                                MessageBoxResult result = MessageBox.Show(string.Format("This person should be attend an Initial Dbids Operator training"), "Notice", MessageBoxButton.YesNo, MessageBoxImage.Information);
                                if (result.ToString().Equals("Yes"))
                                {
                                    txtSuffixKID.Text = string.Empty;
                                    txtPrefixKID.Text = string.Empty;
                                    txtPrefixKID.Focus();
                                }
                                else
                                {
                                    cbxService.SelectedValue = "KSG";
                                    if (txtSuffixKID.Text.Substring(0, 1) == "1")
                                    {
                                        cbxGender.SelectedValue = "Male";
                                    }
                                }
                            }
                        }
                        else
                        {
                            FillPersonInformation();
                        }
                    }
                    break;
                case "txtPrefixSSN":
                    if (!string.IsNullOrEmpty(txtPrefixSSN.Text))
                        txtMidSSN.Focus(); break;
                case "txtMidSSN":
                    if (!string.IsNullOrEmpty(txtMidSSN.Text))
                        txtSuffixSSN.Focus(); break;
                case "txtSuffixSSN":
                    if (!string.IsNullOrEmpty(txtSuffixSSN.Text))
                    {
                        txtLastName.Focus();
                        bool isSSNExist = GetPersonData();
                        if (!isSSNExist)
                        {
                            cbxService.SelectedValue = "ARMY";
                        }
                        else
                        {
                            FillPersonInformation();
                        }
                    }
                    break;
                case "txtPassport":
                    if (!string.IsNullOrEmpty(txtPassport.Text))
                    {
                        txtLastName.Focus();
                        bool isPassportExist = GetPersonData();
                        if (!isPassportExist)
                        {
                            string typeOfClass = ((ComboBoxItem)cbxTypeOfClass.SelectedValue).Content.ToString();
                            if (typeOfClass.Equals("Initial"))
                                cbxService.SelectedValue = "KSG";
                            else
                            {
                                MessageBoxResult result = MessageBox.Show(string.Format("This person should be attend an Initial Dbids Operator training"), "Notice", MessageBoxButton.YesNo, MessageBoxImage.Information);
                                if (result.ToString().Equals("Yes"))
                                {
                                    txtPassport.Text = string.Empty;
                                    txtPassport.Focus();
                                }
                                else
                                    cbxService.SelectedValue = "KSG";
                            }
                        }
                        else
                        {
                            FillPersonInformation();
                        }
                    }
                    break;
                case "txtLastName":
                    if (!string.IsNullOrEmpty(txtLastName.Text))
                        txtFirstName.Focus(); break;
                case "txtFirstName":
                    //btnCapturePersonPhoto.IsEnabled = true; 
                    //btnPastePersonPhoto.IsEnabled = true; 
                    //btnClearPersonPhoto.IsEnabled = true;
                    if (!string.IsNullOrEmpty(txtLastName.Text))
                        txtJobTitle.Focus();
                    break;
                case "txtJobTitle":
                    txtOfficePhonePrefix.Focus(); break;
                case "txtEmail": cbxPolicyAgreement.Focus();
                    break;
                case "txtHomePhoneNumber": txtOfficePhonePrefix.Focus(); break;
                case "txtOfficePhonePrefix": txtOfficePhoneNumber.Focus(); break;
                case "txtOfficePhoneNumber": txtMobilePhonePrefix.Focus(); break;
                case "txtMobilePhonePrefix": txtMobilePhoneNumber.Focus(); break;
                case "txtMobilePhoneNumber": txtEmail.Focus(); break;
                //case "txtPRRemarks": break;
                //case "txtMobilePhoneNumber": txtFaxPrefix.Focus(); break;
                //case "txtFaxPrefix": txtFaxNumber.Focus(); break;
            }
        }

        #endregion Initialize

        #region GetData


        private bool GetPersonData()
        {
            Person = null;
            bool isExist = false;
            string pid = SetPID();

            isExist = SetPerson(isExist, pid);


            return isExist;
        }

        // Serch the person data and set Person property and return bool isExist
        private bool SetPerson(bool isExist, string pid)
        {
            // Check the local database
            GetPerson gp = new GetPerson();
            Person = gp.SearchPersonByPID(pid);
            isExist = false;

            if (Person != null)
            {
                isFromLocalDB = true;
                isExist = GetDataFromXML(isExist, pid);
                if (!isExist)
                    isExist = true;
            }
            else
            {
                isExist = GetDataFromXML(isExist, pid);
            }
            return isExist;
        }

        // Get data from XML
        private bool GetDataFromXML(bool isExist, string pid)
        {
            if (Operators != null)
            {
                var persons = Operators.Persons.Where(p => p.PID.Equals(pid)).Select(p => p);

                if (persons.Count() > 0)
                {
                    string testResult = string.Empty;
                    if (Person != null)
                    {
                        if (!string.IsNullOrEmpty(Person.LastTrainingResult))
                            testResult = Person.LastTrainingResult;
                    }
                    Person = null;
                    Person = persons.First();
                    Person.LastTrainingResult = testResult;
                    isExist = true;
                    isFromLocalDB = false;
                }
                else
                    isExist = false;
            }
            return isExist;
        }

        private void FillPersonInformation()
        {
            if (Person != null)
            {
                if (isFromLocalDB)
                {
                    if (!Person.LastTrainingResult.Equals("Pass"))
                    {
                        string typeOfClass = ((ComboBoxItem)cbxTypeOfClass.SelectedValue).Content.ToString();
                        if (typeOfClass.Equals("Initial"))
                        {
                            // display information
                            DisplayPersonalInformation();
                        }
                        else
                        {
                            MessageBoxResult result = MessageBox.Show(string.Format("This person should be attend an Initial Dbids Operator training"), "Notice", MessageBoxButton.YesNo, MessageBoxImage.Information);
                            if (result.ToString().Equals("Yes"))
                            {
                                DisplayPersonalInformation();
                            }
                        }
                    }
                    else
                        DisplayPersonalInformation();
                    isFromLocalDB = false;
                }
                else
                    DisplayPersonalInformation();
            }
        }

        // Display Personal Information 
        private void DisplayPersonalInformation()
        {
            cbxTypeOfPID.SelectedValue = Person.TypeOfPID;
            if (Person.TypeOfPID == "KID")
            {
                spnKID.Visibility = Visibility.Visible;
                spnSSN.Visibility = Visibility.Collapsed;
                txtPassport.Visibility = Visibility.Collapsed;

                txtPrefixKID.Text = Person.PID.Substring(0, 6);
                txtSuffixKID.Text = Person.PID.Substring(Person.PID.IndexOf("-") + 1);
            }
            else if (Person.TypeOfPID == "SSN")
            {
                spnKID.Visibility = Visibility.Collapsed;
                spnSSN.Visibility = Visibility.Visible;
                txtPassport.Visibility = Visibility.Collapsed;

                txtPrefixSSN.Text = Person.PID.Substring(0, 3);
                txtMidSSN.Text = Person.PID.Substring(4, 2);
                txtSuffixSSN.Text = Person.PID.Substring(7);
            }
            else if (Person.TypeOfPID == "Passport")
            {
                spnKID.Visibility = Visibility.Collapsed;
                spnSSN.Visibility = Visibility.Collapsed;
                txtPassport.Visibility = Visibility.Visible;

                txtPassport.Text = Person.PID;
            }
            txtLastName.Text = Person.LastName;
            txtFirstName.Text = Person.FirstName;
            if (string.IsNullOrEmpty(Person.MiddleName))
                txtMiddleName.Text = Person.MiddleName;
            else
                txtMiddleName.Text = Person.MiddleName;

            // save image to file
            if (isFromLocalDB)
            {
                if (Person.fromPhoto != null)
                {
                    ImageConverter ic = new ImageConverter();
                    imgDBIDSOperator.Source = ic.FromDBToImage(Person.fromPhoto);
                }
            }
            else
                imgDBIDSOperator.Source = ShowPhoto(Person.Photo);

            cbxPolicyAgreement.IsChecked = Boolean.Parse(Person.isPolicyAgree);
            txtJobTitle.Text = Person.JobTitle;
            DateTime dateOfBirth;
            if (DateTime.TryParse(Person.DateOfBirth, out dateOfBirth))
                dpkDateOfBirth.SelectedDate = dateOfBirth;

            cbxTypeOfPID.SelectedItem = Person.TypeOfPID;
            cbxService.SelectedValue = Person.ServiceType;
            cbxRank.SelectedValue = Person.RankID;
            cbxNationality.SelectedValue = Person.Nationality;
            cbxGender.SelectedValue = Person.Gender;
            cbxOperatorType.SelectedValue = Person.OperatorType;
            cbxOffice.SelectedValue = Person.OfficeCode;
            txtPRRemarks.Text = Person.PersonRemarks;

            // Contact Information
            if (!string.IsNullOrEmpty(Person.Email))
            {
                int emailIndex = Person.Email.IndexOf("@");
                if (emailIndex > 0)
                {
                    var emailServer = EmailList.Where(e => e.EmailServer.Equals(Person.Email.Substring(emailIndex + 1)));
                    string domain = string.Empty;
                    if (emailServer.Count() > 0)
                    {
                        domain = emailServer.First().EmailServer;
                        txtEmail.Text = Person.Email.Substring(0, Person.Email.IndexOf("@"));
                        cbxEmailServer.SelectedValue = domain;
                    }
                    else
                    {
                        domain = "** Manual Input **";
                        txtEmail.Text = Person.Email;
                    }


                }
            }
            if (!string.IsNullOrEmpty(Person.OfficePhone))
            {
                if (Person.OfficePhone.Length > 9)
                {
                    if (Person.OfficePhone.IndexOf("-") != Person.OfficePhone.LastIndexOf("-"))
                    {
                        cbxOfficePhoneArea.SelectedItem = Person.OfficePhone.Substring(0, Person.OfficePhone.IndexOf("-"));
                        txtOfficePhonePrefix.Text = Person.OfficePhone.Substring(
                            Person.OfficePhone.IndexOf("-") + 1, Person.OfficePhone.LastIndexOf("-") - Person.OfficePhone.IndexOf("-") - 1);
                        txtOfficePhoneNumber.Text = Person.OfficePhone.Substring(Person.OfficePhone.LastIndexOf("-") + 1);
                    }
                }
            }
            if (!string.IsNullOrEmpty(Person.MobilePhone))
            {
                if (Person.MobilePhone.Length > 9)
                {
                    if (Person.MobilePhone.IndexOf("-") != Person.MobilePhone.LastIndexOf("-"))
                    {
                        cbxMobilePhoneArea.SelectedItem = Person.MobilePhone.Substring(0, Person.MobilePhone.IndexOf("-"));
                        txtMobilePhonePrefix.Text = Person.MobilePhone.Substring(
                            Person.MobilePhone.IndexOf("-") + 1, Person.MobilePhone.LastIndexOf("-") - Person.MobilePhone.IndexOf("-") - 1);
                        txtMobilePhoneNumber.Text = Person.MobilePhone.Substring(Person.MobilePhone.LastIndexOf("-") + 1);
                    }
                }
            }
            //if (!string.IsNullOrEmpty(Person.Fax))
            //{
            //    if (Person.Fax.Length > 9)
            //    {
            //        if (Person.Fax.IndexOf("-") != Person.Fax.LastIndexOf("-"))
            //        {
            //            cbxFaxArea.SelectedItem = Person.Fax.Substring(0, Person.Fax.IndexOf("-"));
            //            txtFaxPrefix.Text = Person.Fax.Substring(
            //                Person.Fax.IndexOf("-") + 1, Person.Fax.LastIndexOf("-") - Person.Fax.IndexOf("-") - 1);
            //            txtFaxNumber.Text = Person.Fax.Substring(Person.Fax.LastIndexOf("-") + 1);
            //        }
            //    }
            //}
            cbxInstallation.SelectedValue = Person.Installation;
            Person = null;
        }

        private BitmapImage ShowPhoto(string filename)
        {
            ImageConverter ic = new ImageConverter();
            BitmapImage image = ic.FromFileToImage(filename);
            return image;
        }

        #endregion GetData

        #region Save

        private void SaveTrainee()
        {
            try
            {
                if (cbxPolicyAgreement.IsChecked == true)
                {
                    bool isExist = false;
                    Person = new PersonAndDbidsOperators.Person();
                    PersonAndDbidsOperators.Person personExist = new PersonAndDbidsOperators.Person();
                    string date = DateTime.Now.ToShortDateString();
                    string birth = dpkDateOfBirth.SelectedDate.ToString();
                    string pid = SetPID();
                    string filename = string.Format(@"Images\{0}{1}{2}.jpg", txtLastName.Text, txtFirstName.Text.Substring(0, 1), pid.Substring(pid.Length - 4, 4));

                    if (Operators != null)
                    {
                        foreach (PersonAndDbidsOperators.Person person in Operators.Persons)
                        {
                            if (pid == person.PID)
                            {
                                isExist = true;
                                personExist = person;
                            }
                        }
                    }
                    else
                    {
                        Operators = new PersonAndDbidsOperators();
                        Operators.Persons = new List<PersonAndDbidsOperators.Person>();
                    }

                    Person.PID = pid;
                    Person.LastName = txtLastName.Text;
                    Person.MobilePhone = string.Format("{0}-{1}-{2}", cbxMobilePhoneArea.SelectedValue, txtMobilePhonePrefix.Text, txtMobilePhoneNumber.Text);
                    Person.FirstName = txtFirstName.Text;
                    Person.MiddleName = txtMiddleName.Text;
                    Person.DateOfBirth = birth;
                    Person.Gender = cbxGender.SelectedValue.ToString();
                    Person.OfficePhone = string.Format("{0}-{1}-{2}", cbxOfficePhoneArea.SelectedValue, txtOfficePhonePrefix.Text, txtOfficePhoneNumber.Text);
                    //Person.Fax = string.Format("{0}-{1}-{2}", cbxFaxArea.SelectedValue, txtFaxPrefix.Text, txtFaxNumber.Text);
                    Person.Photo = filename;
                    if (cbxEmailServer.SelectedValue.ToString() != "** Manual Input **")
                        Person.Email = string.Format("{0}@{1}", txtEmail.Text, cbxEmailServer.SelectedValue);
                    else
                        Person.Email = txtEmail.Text;
                    Person.ServiceType = cbxService.SelectedValue.ToString();
                    Person.RankID = cbxRank.SelectedValue.ToString();
                    Person.OfficeCode = cbxOffice.SelectedValue.ToString();
                    Person.TypeOfPID = cbxTypeOfPID.SelectedValue.ToString();
                    Person.RegDate = date;
                    Person.JobTitle = txtJobTitle.Text;
                    Person.PersonRemarks = txtPRRemarks.Text;
                    Person.OperatorType = cbxOperatorType.SelectedValue.ToString();
                    Person.LastTrainedDate = date;
                    Person.Installation = cbxInstallation.SelectedValue.ToString();
                    Person.Nationality = cbxNationality.SelectedValue.ToString();
                    Person.isPolicyAgree = cbxPolicyAgreement.IsChecked.ToString();
                    //if (cbxPolicyAgreement.IsChecked == true)
                    //    Person.isPolicyAgree = "true";
                    //else
                    //    Person.isPolicyAgree = "false";

                    if (isExist)
                    {
                        Operators.Persons.Remove(personExist);
                    }

                    if (imgDBIDSOperator.Source != null)
                        SaveImage(Person.LastName, Person.FirstName, pid, filename);
                    Operators.Persons.Add(Person);

                    SerializeXml sx = new SerializeXml();
                    sx.WriteXml(Operators);
                    if (string.IsNullOrEmpty(Person.MiddleName))
                        MessageBox.Show(string.Format("{0}, {1}'s information has been saved.", txtLastName.Text, txtFirstName.Text));
                    else
                        MessageBox.Show(string.Format("{0}, {1} {2}'s information has been saved.", txtLastName.Text, txtFirstName.Text, txtMiddleName.Text));
                    GetOperators();
                }
                else
                {
                    MessageBox.Show("You should agree with the privacy information policy");
                    cbxPolicyAgreement.Background = Brushes.Red;
                }
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(Person.MiddleName))
                    MessageBox.Show(string.Format("{0}, {1}'s information couldn't saved. /n{2}", txtLastName.Text, txtFirstName.Text, ex.ToString()));
                else
                    MessageBox.Show(string.Format("{0}, {1} {2}'s information couldn't saved. /n{3}", txtLastName.Text, txtFirstName.Text, txtMiddleName.Text, ex.ToString()));
            }
            finally
            {
                DeserializeXml dx = new DeserializeXml();
                Operators = dx.DeserializePersons();
            }

        }

        private void SaveImage(string lastName, string firstName, string pid, string filename)
        {
            ImageConverter ic = new ImageConverter();
            BitmapSource imageSource = (BitmapSource)imgDBIDSOperator.Source;
            ic.SaveImageToFile(filename, imageSource);
        }

        #endregion Save

        #region Clear

        // Show the PID Input Box by the value of the type of the PID
        private void ControlPIDVisibility()
        {
            ClearAll();
        }

        private void ClearAll()
        {
            if (txtPrefixKID != null)
            {
                // Clear Personal Information
                txtPrefixKID.Text = string.Empty;
                txtSuffixKID.Text = string.Empty;
                txtPrefixSSN.Text = string.Empty;
                txtMidSSN.Text = string.Empty;
                txtSuffixSSN.Text = string.Empty;
                txtPassport.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtFirstName.Text = string.Empty;
                dpkDateOfBirth.SelectedDate = null;
                dpkDateOfBirth.DisplayDate = DateTime.Now.AddYears(-25);
                //dpkDateOfBirth.Text = "Select a date";
                txtMiddleName.Text = string.Empty;
                txtJobTitle.Text = string.Empty;
                ToggleByTypeOfPID();
                cbxGender.SelectedIndex = 0;
                txtPRRemarks.Text = string.Empty;

                imgDBIDSOperator.Source = ShowPhoto(@"XMLData\Photos.png");
                //btnCapturePersonPhoto.IsEnabled = false;
                //btnPastePersonPhoto.IsEnabled = false;
                //btnClearPersonPhoto.IsEnabled = false;

                // Clear Contact Information
                txtEmail.Text = string.Empty;
                txbNoOfTrainees.Text = string.Empty;
                cbxOfficePhoneArea.SelectedIndex = 0;
                cbxMobilePhoneArea.SelectedIndex = 0;
                //cbxFaxArea.SelectedIndex = 0;
                txtOfficePhonePrefix.Text = string.Empty;
                txtOfficePhoneNumber.Text = string.Empty;
                txtMobilePhonePrefix.Text = string.Empty;
                txtMobilePhoneNumber.Text = string.Empty;
                //txtFaxPrefix.Text = string.Empty;
                //txtFaxNumber.Text = string.Empty;
                cbxPolicyAgreement.IsChecked = false;
                cbxPolicyAgreement.Background = Brushes.White;
            }
        }

        private void ToggleByTypeOfPID()
        {
            if (cbxTypeOfPID.SelectedValue.Equals("KID"))
            {
                spnKID.Visibility = Visibility.Visible;
                txtPrefixKID.Focus();
                spnSSN.Visibility = Visibility.Collapsed;
                txtPassport.Visibility = Visibility.Collapsed;
                cbxNationality.SelectedValue = "KR";
                cbxService.SelectedValue = "KSG";
            }
            else if (cbxTypeOfPID.SelectedValue.Equals("SSN"))
            {
                spnKID.Visibility = Visibility.Collapsed;
                spnSSN.Visibility = Visibility.Visible;
                txtPrefixSSN.Focus();
                txtPassport.Visibility = Visibility.Collapsed;
                cbxNationality.SelectedValue = "US";
                cbxService.SelectedValue = "ARMY";
                cbxOperatorType.SelectedValue = "Law Enforcement";
            }
            else
            {
                spnKID.Visibility = Visibility.Collapsed;
                spnSSN.Visibility = Visibility.Collapsed;
                txtPassport.Visibility = Visibility.Visible;
                txtPassport.Focus();
                cbxNationality.SelectedValue = "US";
                cbxService.SelectedValue = "ARMY";
                cbxOperatorType.SelectedValue = "Law Enforcement";
            }
        }

        #endregion Clear

        #region Capture Photo

        // Capture photo from webcam
        private void GetPhoto(object sender)
        {
            if (((Button)sender).Content.ToString() == "Capture")
            {
                CapturePhoto cp = new CapturePhoto();
                cp.ShowDialog();
                if (cp.bitmap != null)
                {
                    imgDBIDSOperator.Source = cp.bitmap;
                }
            }
            else if (((Button)sender).Content.ToString() == "Paste")
            {

            }
            else
            {
                imgDBIDSOperator.Source = null;
            }
        }

        #endregion Capture Photo

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCapturePersonPhoto_Click(object sender, RoutedEventArgs e)
        {
            GetPhoto((Button)sender);
        }

        private void btnPastePersonPhoto_Click(object sender, RoutedEventArgs e)
        {
            GetPhoto((Button)sender);
        }

        private void btnClearPersonPhoto_Click(object sender, RoutedEventArgs e)
        {
            GetPhoto((Button)sender);
        }

        private void cbxTypeOfPID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ControlPIDVisibility();
        }

        private void txtBoxKey_up(object sender, KeyEventArgs e)
        {
            MoveTheFocus(sender, e);
        }

        private void cbxService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(cbxService.SelectedItem.ToString()))
            {
                ChangeRankComboBox(cbxService.SelectedItem.ToString());
                GetOffices();
            }
        }

        private void GetOffices()
        {
            GetOfficeFromLocalDB gol = new GetOfficeFromLocalDB();
            Offices = gol.GetOffices(cbxService.SelectedItem.ToString());
            ShowOfficeNameToComboBox(Offices);
        }

        private void btnDBIDSClear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                string.Format("Do you want to clear all?"), "Clear Information", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result.ToString().Equals("Yes"))
            {
                ClearAll();
                // clear trainee list
                GetOperators();
                dgdFindPersonList.ItemsSource = null;
            }
        }

        private void btnDBIDSSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show(
                    string.Format("Do you want to save {0}, {1}'s record?", txtLastName.Text, txtFirstName.Text),
                    "Save Person", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result.ToString().Equals("Yes"))
                {
                    SaveTrainee();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnDBIDSClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbxInstallation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(cbxInstallation.SelectedItem.ToString()))
            {
                InstallationNameOptions.InstallationNameOption inOption = new InstallationNameOptions.InstallationNameOption();
                inOption = (InstallationNameOptions.InstallationNameOption)cbxInstallation.SelectedItem;
                txbCity.Text = inOption.City;
                txbArea.Text = inOption.Area;
                if (cbxOffice.ItemsSource != null)
                {
                    List<OFFICE> offices = Offices.Where(o => o.OfficeName.Contains(inOption.InstallationName.ToUpper())).ToList();
                    if (offices.Count > 0)
                        cbxOffice.SelectedValue = offices.First().OFFICECODE;
                }
            }
        }

        private void cbxPolicyAgreement_Checked(object sender, RoutedEventArgs e)
        {
            if (cbxPolicyAgreement.IsChecked == true)
                cbxPolicyAgreement.Background = Brushes.White;
        }

        private void btnDBIDSRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show(string.Format("Do you want to remove {0}, {1}'s record?", txtLastName.Text, txtFirstName.Text),
                    "Remove Person", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result.ToString().Equals("Yes"))
                {
                    if (Person != null)
                    {
                        var trainedPerson = Operators.Persons.Where(p => p.PID.Equals(Person.PID)).Select(p => p).First();
                        Operators.Persons.Remove(trainedPerson);
                        SerializeXml sx = new SerializeXml();
                        sx.WriteXml(Operators);
                        GetOperators();
                        dgdFindPersonList.ItemsSource = null;
                        MessageBox.Show(string.Format("{0}'s record has been removed", txtLastName.Text));
                        ClearAll();
                    }
                    else
                    {
                        MessageBox.Show(string.Format("{0}'s record not found", txtLastName.Text));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't remove data /r Error : {0}", ex.ToString());
            }
        }

        private void btnDBIDSUpload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (new Upload()).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbxTrainedDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void btnRefreshList_Click(object sender, RoutedEventArgs e)
        {
            GetOperators();
            dgdFindPersonList.ItemsSource = null;
        }

        private void dgdFindPersonList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectTrainee(sender);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeSerialPort();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mySerialPort.IsOpen)
            {
                mySerialPort.Close();
            }
        }
    }
}
