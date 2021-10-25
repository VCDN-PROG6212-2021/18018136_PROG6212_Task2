using PROG6212_Task1;
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

namespace PROG6212_Task2
{
    /// <summary>
    /// Interaction logic for AddHoursWindow.xaml
    /// </summary>
    public partial class AddHoursWindow : Window
    {
        int userID;
        HyperOrganizerDBEntities db = new HyperOrganizerDBEntities();
        public static List<Module> filterCode;
        public AddHoursWindow(int userID)
        {
            InitializeComponent();
            this.userID = userID; // Determining the loged in user
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Redirecting the user to the modules window
            ModuleWindow window = new ModuleWindow(userID);
            this.Hide();
            window.ShowDialog();
            this.Close();
        }

        // Displaying the existing modules from the database to the user
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                filterCode = db.Modules.Where(m => m.UserID == userID).OrderBy(mc => mc.Module_Code).ToList();
                foreach (var item in filterCode)
                {
                    cmbModules.Items.Add(item.Module_Code);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            AddModuleHours();
        }

        // Method used to Clear controls to get new user input
        public void clearFields()
        {
            cmbModules.Items.Clear();
            dtpDate.Text = "";
            txbModuleHours.Clear();
        }

        // Method used to add Module hours to the database.
        public async void AddModuleHours()
        {
            string selectedModule = cmbModules.SelectedItem.ToString();
            DateTime date = Convert.ToDateTime(dtpDate.Text);
            int hours = Convert.ToInt32(txbModuleHours.Text);
            Module module = db.Modules.Where(x => x.UserID == userID && x.Module_Code.Equals(selectedModule)).FirstOrDefault();

            Completed_Hours completed_Hours = new Completed_Hours();
            completed_Hours.date = date;
            completed_Hours.hours = hours;
            completed_Hours.ID = module.ID;
            db.Completed_Hours.Add(completed_Hours);
            await db.SaveChangesAsync();
            MessageBox.Show("Module successfully added !");
            clearFields();
        }
    }
}
