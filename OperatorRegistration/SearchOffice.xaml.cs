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
using OperatorRegistration.Repositories;
using OperatorRegistration.Utilities;
using OperatorRegistration.Entities;

namespace OperatorRegistration
{
    /// <summary>
    /// Interaction logic for SearchOffice.xaml
    /// </summary>
    public partial class SearchOffice : Window
    {
        /// <summary>
        /// Scripter : YONGTOK KIM
        /// Scripted Date : 25 Nov 2010
        /// Search Office
        /// </summary>

        public OFFICE fsOffice { get; set; }
        public Unit fsUnit { get; set; }
        public INSTALLATION fsInstallation { get; set; }
        public string officeCode { get; set; }
        private bool changeInstallation = false;

        public SearchOffice()
        {
            InitializeComponent();
            InitializeComboBox();
        }

        private void InitializeComboBox()
        {
            DeserializeXml dx = new DeserializeXml();
            var sortInstallation = dx.DeserializeInstallation().InstallationOptions.OrderBy(si => si.InstallationName);

            cbxInstallationName.ItemsSource = sortInstallation;
            cbxInstallationName.DisplayMemberPath = "InstallationName";
            cbxInstallationName.SelectedValuePath = "InstallationCode";

            cbxInstallationName.SelectedIndex = 0;
            rdbSOSearchByName.IsChecked = true;
        }

        #region Show and send information

        //Show the office name to the List Box
        private void ShowOfficeNameToListBox(List<OFFICE> offices)
        {
            lbxOfficeName.ItemsSource = offices;
            lbxOfficeName.DisplayMemberPath = "OfficeName";
            lbxOfficeName.SelectedValuePath = "OFFICECODE";
        }

        /// <summary>
        /// Scripter : YONGTOK KIM
        /// Scripted Date : 30 Nov 2010
        /// Show detail information of the Unit
        /// </summary>
        private void ShowOfficeInformation()
        {
            if (lbxOfficeName.SelectedIndex > -1)
            {
                officeCode = null;
                GetOffice go = new GetOffice();
                officeCode = lbxOfficeName.SelectedValue.ToString();
                var office = go.GetOfficesByOfficeCode(officeCode);

                if (office != null)
                {
                    OFFICE o = office.First();

                    txtOfficeName.Text = o.OfficeName;
                    txbOfficeCode.Text = o.OFFICECODE;
                    txtBuildingName.Text = o.BuildingName;
                    txtBuildingNo.Text = o.BuildingNo;
                    txtRoomNo.Text = o.RoomNo;

                    GetUnit gu = new GetUnit();
                    var unit = gu.GetUnitsByUIC(o.UIC);

                    if (unit != null)
                    {
                        Unit u = unit.First();
                        int hyphenIndex = u.UnitPhone.IndexOf("-");

                        txbUnitName.Text = u.UnitName;
                        txbUIC.Text = u.UIC;
                        txbUnitPhonePrefix.Text = u.UnitPhone.Substring(0, hyphenIndex + 1);
                        txbUnitPhoneNumber.Text = u.UnitPhone.Substring(hyphenIndex + 1);
                        txbUnitAPO.Text = u.APO;
                        txbUnitAP.Text = u.AP;

                        GetInstallation gi = new GetInstallation();
                        var installation = gi.GetInstallationByIC(u.INSTALLATIONCODE);

                        if (installation != null)
                        {
                            cbxInstallationName.Text = installation.INSTALLATIONNAME;
                            txbInstallationCode.Text = installation.INSTALLATIONCODE;
                            txbArea.Text = installation.AREA;
                            txbCity.Text = installation.City;
                        }
                    }
                    changeInstallation = true;
                }
                else
                {
                    MessageBox.Show(string.Format("{0} is not exist!!", lbxOfficeName.SelectedValue.ToString()));
                }
                go.officeSearchResult = false;
            }
        }

        //Set the Value of the Office, Unit, and Installation
        private void SendDataToPersonalInformationRegisterForm()
        {
            GetOffice go = new GetOffice();
            GetUnit gu = new GetUnit();
            GetInstallation gi = new GetInstallation();

            if (!string.IsNullOrEmpty(officeCode))
            {
                fsOffice = go.GetOfficesByOfficeCode(officeCode).First();
                fsUnit = gu.GetUnitsByUIC(fsOffice.UIC).First();
                fsInstallation = gi.GetInstallationByIC(fsUnit.INSTALLATIONCODE);
            }
            else
            {
                MessageBox.Show("Office code is not exitst");
                return;
            }

            if (fsOffice == null)
            {
                MessageBox.Show("No Office Information");
                return;
            }
            else if (fsUnit == null)
            {
                MessageBox.Show("No Unit Information");
                return;
            }
            else if (fsInstallation == null)
            {
                MessageBox.Show("No Installation Information");
                return;
            }
            else
            {
                DialogResult = true;
                this.Close();
            }
        }

        #endregion Show and send the information

        #region Clear the Controls

        /// <summary>
        /// Scripter : YONGTOK KIM
        /// Scripted Date : 25 Nov 2010
        /// Clear the controls and reset
        /// </summary>
        private void SOClearAll()
        {
            SOClearSearchResult();
            SOClearOfficeInformation();
            SOClearUnitInformation();
            LabelForegroundToBlack();
            cbxInstallationName.SelectedIndex = 0;
        }

        //Clear Search Office
        private void SOClearSearchResult()
        {
            txtSONameSearch.Text = string.Empty;
            lbxOfficeName.ItemsSource = null;
            rdbSOSearchByName.IsChecked = true;
        }

        //Clear Office Information
        private void SOClearOfficeInformation()
        {
            foreach (UIElement control in gdOfficeInformation.Children)
                if (control is TextBox)
                    ((TextBox)control).Text = string.Empty;
            txtRoomNo.Text = string.Empty;
            txbOfficeCode.Text = string.Empty;
        }

        //Clear Unit Information
        private void SOClearUnitInformation()
        {
            foreach (UIElement control in gdUnitInformation.Children)
                if (control is TextBlock)
                    ((TextBlock)control).Text = string.Empty;
            txbUnitPhonePrefix.Text = string.Empty;
            txbUnitPhoneNumber.Text = string.Empty;
            txbUnitAPO.Text = string.Empty;
            txbUnitAP.Text = string.Empty;
            changeInstallation = false;
        }

        //Change background color to Orange when mouse on the label
        private static void MouseOnLabel(object sender)
        {
            ((Label)sender).Foreground = Brushes.Black;
            ((Label)sender).Background = Brushes.Orange;
        }

        //Change background color to white when mouse off the label
        private static void MouseOffLabel(object sender)
        {
            ((Label)sender).Background = Brushes.White;
        }

        //Change background and foreground color when mouse double click the label
        private void MouseDoubleClicked(object sender)
        {
            LabelForegroundToBlack();
            ((Label)sender).Background = Brushes.Yellow;
            ((Label)sender).Foreground = Brushes.Red;

            lbxOfficeName.ItemsSource = null;
            GetOffice go = new GetOffice();
            List<OFFICE> offices = null;

            if (((Label)sender).Content.ToString() != "#")
                offices = go.GetOfficesByName(((Label)sender).Content.ToString());
            else
                offices = go.GetAllOfOffices();

            if (offices != null)
            {
                ShowOfficeNameToListBox(offices);
            }
        }

        //Change foreground color of the label to black
        private void LabelForegroundToBlack()
        {
            foreach (UIElement control in spSOLabelSelector.Children)
                if (control is Label)
                    ((Label)control).Foreground = Brushes.Black;
        }

        //Toggle the search option
        private void toggleSearchOption(object sender)
        {
            LabelForegroundToBlack();
            if (((RadioButton)sender).Name.ToString() == "rdbSOShowAll")
            {
                btnSONameSearch.IsEnabled = false;
                txtSONameSearch.IsEnabled = false;
                txtSONameSearch.Text = string.Empty;
            }
            else
            {
                btnSONameSearch.IsEnabled = true;
                txtSONameSearch.IsEnabled = true;
                txtSONameSearch.Text = string.Empty;
                txtSONameSearch.Focus();
            }
        }

        #endregion Clear Control

        #region Method for Searching

        //Get the information of the installation and put it into the TextBlock
        private void GetInstallationInformation(string installatioName)
        {
            DeserializeXml dx = new DeserializeXml();
            var installtion = dx.DeserializeInstallation().InstallationOptions.Where(i => i.InstallationCode == installatioName);

            foreach (InstallationNameOptions.InstallationNameOption inst in installtion)
            {
                txbInstallationCode.Text = inst.InstallationCode;
                txbArea.Text = inst.Area;
                txbCity.Text = inst.City;
            }
        }


        //Search the offices by the Unit
        private void OfficeSearchByUIC(string uic)
        {
            GetOffice go = new GetOffice();
            var offices = go.GetOfficesByUIC(txbUIC.Text);

            if (offices != null)
                ShowOfficeNameToListBox(offices);
        }

        //Search the Offices
        private void OfficeSearch()
        {
            lbxOfficeName.ItemsSource = null;
            LabelForegroundToBlack();
            GetOffice go = new GetOffice();
            List<OFFICE> offices = null;

            if (!string.IsNullOrEmpty(txtSONameSearch.Text))
            {
                if (rdbSOSearchByUnit.IsChecked == true)
                {
                    offices = go.GetOfficesByUnitName(txtSONameSearch.Text);
                }
                else
                {
                    offices = go.GetOfficesByName(txtSONameSearch.Text);
                }
                if (offices != null)
                    ShowOfficeNameToListBox(offices);
            }
        }

        //Search the Office by Installation (When change cbxInstallationName)
        private void OfficeSearchByInstallation()
        {
            if (!changeInstallation)
            {
                lbxOfficeName.ItemsSource = null;
                LabelForegroundToBlack();
                GetOffice go = new GetOffice();
                List<OFFICE> offices = null;

                offices = go.GetOfficesByInstallationCode(cbxInstallationName.SelectedValue.ToString());

                if (offices != null)
                    ShowOfficeNameToListBox(offices);
            }
        }

        //Search all of the offices
        private void AllOfOfficesSearch()
        {
            LabelForegroundToBlack();
            GetOffice go = new GetOffice();
            var offices = go.GetAllOfOffices();
            if (offices != null)
                ShowOfficeNameToListBox(offices);
        }

        #endregion Method Searching

        private void btnSOSelect_Click(object sender, RoutedEventArgs e)
        {
            SendDataToPersonalInformationRegisterForm();
        }

        private void btnSOClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClearOfficeInformation_Click(object sender, RoutedEventArgs e)
        {
            SOClearOfficeInformation();
        }

        private void rdbSOShowAll_Click(object sender, RoutedEventArgs e)
        {
            toggleSearchOption(sender);
            AllOfOfficesSearch();
        }

        private void lblSOZ_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseOnLabel(sender);
        }

        private void lblSOZ_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseOffLabel(sender);
        }

        private void lblSOZ_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MouseDoubleClicked(sender);
        }

        private void lbxOfficeName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowOfficeInformation();
        }

        private void lbxOfficeName_MouseEnter(object sender, MouseEventArgs e)
        {
            lbxOfficeName.Focus();
        }

        private void btnSONameSearch_Click(object sender, RoutedEventArgs e)
        {
            OfficeSearch();
        }

        private void cbxInstallationName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetInstallationInformation(cbxInstallationName.SelectedValue.ToString());
            OfficeSearchByInstallation();
        }

        private void rdbSOSearchByUnit_Click(object sender, RoutedEventArgs e)
        {
            toggleSearchOption(sender);
        }

        private void rdbSOSearchByName_Click(object sender, RoutedEventArgs e)
        {
            toggleSearchOption(sender);
        }

        private void txtBoxKey_Down(object sender, KeyEventArgs e)
        {
            OfficeSearch();
        }
    }
}
