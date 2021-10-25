using System;
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
using System.Windows.Shapes;
using ClassLibrary;
using PROG6212_Task2;

namespace PROG6212_Task1
{
    /// <summary>
    /// Interaction logic for AddModulesWindow.xaml
    /// </summary>
    public partial class AddModulesWindow : Window
    {
        int userID;
        HyperOrganizerDBEntities db = new HyperOrganizerDBEntities();
        public AddModulesWindow(int userID)
        {
            InitializeComponent();
            this.userID = userID; // Determining the loged in user
        }

        // Adding Modules to a list of module objects
        private void btnSaveModules_Click(object sender, RoutedEventArgs e)
        {
            CreateModule();
        }

        // This method allows the user to go back to the previous window
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ModuleWindow window = new ModuleWindow(userID);
            this.Hide();
            window.ShowDialog();
            this.Close();
        }

        // This method clears the form for new user entry
        public void clearFields()
        {
            txbModuleCode.Clear();
            txbModuleName.Clear();
            txbModuleCredits.Clear(); ;
            txbHoursPerWeek.Clear(); ;
            txbSemesterWeeks.Clear(); ;
            dtpStartDate.SelectedDate = DateTime.Now;
        }

        // This method Creates a new module entry into the database
        public async void CreateModule()
        {
            if (txbModuleCode.Text == null || txbModuleName.Text == null || txbModuleCredits.Text == null || txbHoursPerWeek.Text == null || txbSemesterWeeks.Text == null || dtpStartDate.Text == null)
            {
                MessageBox.Show("All fields are required ! ", "Error Message");
            }
            else
            {
                try
                {
                    // Taking user input from the GUI
                    string mCode = txbModuleCode.Text;
                    string mName = txbModuleName.Text;
                    int mCredits = Convert.ToInt32(txbModuleCredits.Text);
                    int hours = Convert.ToInt32(txbHoursPerWeek.Text);
                    int weeks = Convert.ToInt32(txbSemesterWeeks.Text);
                    DateTime date = Convert.ToDateTime(dtpStartDate.Text);

                    Module newModule = new Module();
                    newModule.Module_Code = mCode;
                    newModule.Module_Name = mName;
                    newModule.Credits = mCredits;
                    newModule.Weekly_Hours = hours;
                    newModule.Semester_Weeks = weeks;
                    newModule.StartDate = date;

                    newModule.UserID = userID;
                    db.Modules.Add(newModule);
                    await db.SaveChangesAsync();
                    MessageBox.Show("Module successfully added !");
                    clearFields();
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid Input entry ! ", "Error Message");
                }

            }
        }
    }
}
