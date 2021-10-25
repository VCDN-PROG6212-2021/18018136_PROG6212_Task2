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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG6212_Task1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int userID;
        public MainWindow(int userID)
        {
            InitializeComponent();
            this.userID = userID; // Determining the loged in user
        }

        private void btnModules_Click(object sender, RoutedEventArgs e)
        {
            // Opening the Module windows to view or add modules
            AddModulesWindow window = new AddModulesWindow(userID);
            this.Hide();
            window.ShowDialog();
            this.Close();
        }
    }
}
