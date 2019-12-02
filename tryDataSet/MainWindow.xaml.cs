using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace tryDataSet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string dbConnection = ConfigurationManager.ConnectionStrings["tryDataSet.Properties.Settings.CustomerDBConnectionString"].ConnectionString;
        public MainWindow()
        {
            InitializeComponent();
            PopulateList();
        }

        public void PopulateList() 
        {
            string command = @"SELECT * From Customers";
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(dbConnection)) 
            {
                SqlCommand cmd = new SqlCommand(command, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "mike");
                listEmployeesName.ItemsSource = ds.Tables["mike"].Rows;
                listEmployeesName.DisplayMemberPath =".[Name]";
                listEmployeesName.SelectedValuePath = ".[Id]";

                
            }
            
        }

        private void listEmployeesName_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = int.Parse(listEmployeesName.SelectedValue.ToString());
            string command = @"SELECT * From Customers where Id=" + id;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new SqlCommand(command, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "MyCustomer");

                foreach (DataRow data in ds.Tables["MyCustomer"].Rows)
                {
                    txtSalary.Text = data["Salary"].ToString();
                    txtGetTitle.Text = data["Title"].ToString();
                }




            }
        }

        private void btnAddEmp_Click(object sender, RoutedEventArgs e)
        {
            NewEmployeeWindow newEmpWindow = new NewEmployeeWindow();
            if (newEmpWindow.ShowDialog().Value)
            {
                listEmployeesName.ItemsSource = null;
                PopulateList();
                listEmployeesName.Items.Refresh();
            }
        }

        private void btnDeleteEmp_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(listEmployeesName.SelectedValue.ToString());
            if(MessageBox.Show("Are you sure", "Confirm", MessageBoxButton.YesNoCancel)==MessageBoxResult.Yes)
            {
                string command = @"delete From Customers where Id=" + id;
                try
                {
                    using (SqlConnection con = new SqlConnection(dbConnection))
                    {
                        SqlCommand cmd = new SqlCommand(command, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Emp ID" + id + " Deleted");

                        listEmployeesName.ItemsSource = null;
                        PopulateList();
                        listEmployeesName.Items.Refresh();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message);
                }
            }
            else
            {
                return;
            }
           
        }
    }
}
