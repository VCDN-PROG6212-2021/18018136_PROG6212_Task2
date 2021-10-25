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
    /// Interaction logic for ModuleWindow.xaml
    /// </summary>
    public partial class ModuleWindow : Window
    {
        int userID;
        HyperOrganizerDBEntities db = new HyperOrganizerDBEntities();
        public static List<Module> filtered;
        public static List<Module> filterCode;
        public ModuleWindow(int userID)
        {
            InitializeComponent();
            this.userID = userID; // Determining the loged in user
        }

        private void btnModules_Click(object sender, RoutedEventArgs e)
        {
            // Redirecting the user to the add modules windows to Add a module to the list
            AddModulesWindow window = new AddModulesWindow(userID);
            this.Hide();
            window.ShowDialog();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                filtered = db.Modules.Where(m => m.UserID == userID).ToList();

                foreach (Module item in filtered)
                {
                    lbModules.Items.Add(item.Module_Code);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No Modules Found !");
            }
        }

        private void lbModules_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedModule = lbModules.SelectedItem.ToString();
            // Link query retrieving the selected list box module item from the list
            filterCode = db.Modules.Where(m => m.Module_Code.Equals(selectedModule) && m.UserID == userID).OrderBy(mc => mc.Module_Code).ToList();

            foreach (Module item in filterCode)
            {
                if (item.Module_Code.Equals(selectedModule))
                {
                    txbModuleDetails.Clear();
                    // Displaying the data of a Module to the user
                    txbModuleDetails.Text += "Module Code : " + item.Module_Code + Environment.NewLine;
                    txbModuleDetails.Text += "Module Name : " + item.Module_Name + Environment.NewLine;
                    txbModuleDetails.Text += "Module Credits : " + item.Credits + Environment.NewLine;
                    txbModuleDetails.Text += "Hours Per Week : " + item.Weekly_Hours + Environment.NewLine;
                    txbModuleDetails.Text += "Semester Weeks : " + item.Semester_Weeks + Environment.NewLine;
                    txbModuleDetails.Text += "Start Date : " + item.StartDate + Environment.NewLine;
                    txbModuleDetails.Text += "Self Study hours : " + SelfStudy() + Environment.NewLine;
                    txbModuleDetails.Text += "Remaining Self Study hours : " + remainingStudyHours() + Environment.NewLine;

                }
            }
        }

        // Method used to add weekly study hours
        private void btnAddStudyHours_Click(object sender, RoutedEventArgs e)
        {
            // Redirecting the user to the add modules hours window
            AddHoursWindow window = new AddHoursWindow(userID);
            this.Hide();
            window.ShowDialog();
            this.Close();
        }

        // Method calculation for Self study hours
        public double SelfStudy()
        {
            string selectedModule = lbModules.SelectedItem.ToString();
            Module module = db.Modules.Where(x => x.UserID == userID && x.Module_Code.Equals(selectedModule)).FirstOrDefault();
            return Modules.selfStudyHours(module.Credits, module.Semester_Weeks, module.Weekly_Hours);

        }

        // Method calculation for remaining study hours
        public double remainingStudyHours()
        {
            string selectedModule = lbModules.SelectedItem.ToString();
            Module module = db.Modules.Where(x => x.UserID == userID && x.Module_Code.Equals(selectedModule)).FirstOrDefault();

            double sumOfStudyHours = db.Completed_Hours.Where(x => x.ID == module.ID).Select(h => h.hours).Sum();

            // Calculations
            double result = 0;
            result = SelfStudy() - sumOfStudyHours;
            return Math.Round(result, 2);
        }
    }
}
