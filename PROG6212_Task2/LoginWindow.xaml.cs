using PROG6212_Task2;
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
using System.Data.Entity;

namespace PROG6212_Task1
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        HyperOrganizerDBEntities db = new HyperOrganizerDBEntities();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UserLogin();
        }

        // Method used to login and use the application's functionalities
        public async void UserLogin()
        {
            string userName = txbUsername.Text;
            string password = HelperClass.EncodePasswordToBase64(txbPassword.Password);

            if (txbUsername.Text == "" || txbPassword.Password == "")
            {
                MessageBox.Show("Please fill in all the fields");
            }
            else
            {
                try
                {
                    User user = await db.Users.Where(u => u.Username.Equals(userName) && u.Password.Equals(password)).FirstAsync();
                    MessageBox.Show("Logged in successfully !");
                    MainWindow window = new MainWindow(user.UserID);
                    ClearFields();
                    this.Hide();
                    window.ShowDialog();
                    this.ShowDialog();
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid login credentials");
                }
            }
        }

        // Method used to Create a new user in the database
        public async void UserRegistration()
        {
            if (txbUsername.Text != null || txbPassword.Password != null)
            {
                try
                {
                    User newUser = new User();
                    newUser.Username = txbUsername.Text;
                    newUser.Password = HelperClass.EncodePasswordToBase64(txbPassword.Password);
                    db.Users.Add(newUser);
                    await db.SaveChangesAsync();
                    MessageBox.Show("User successfully Registered\nYou may proceed with login", "Message");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Check if all the fields have values", "Error Message");
            }
        }

        // Method used to clear the text fields
        public void ClearFields()
        {
            txbUsername.Clear();
            txbPassword.Clear();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            UserRegistration();
        }
    }
}
