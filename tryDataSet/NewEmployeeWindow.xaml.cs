using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;

namespace tryDataSet
{
    /// <summary>
    /// Interaction logic for NewEmployeeWindow.xaml
    /// </summary>
    public partial class NewEmployeeWindow : Window
    {
        string dbConnection = ConfigurationManager.ConnectionStrings["tryDataSet.Properties.Settings.CustomerDBConnectionString"].ConnectionString;

        public NewEmployeeWindow()
        {
            InitializeComponent();
        }

        private void btnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {

            string command=@"insert into Customers(Name,Title,Salary) values ('"+txtEmpName.Text+
                @"','"+txtTitle.Text+@"','"+txtSalary.Text+@"')";
            try
            {
                using(SqlConnection con=new SqlConnection(dbConnection))
                {
                    SqlCommand cmd = new SqlCommand(command, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    this.DialogResult = true;
                    MessageBox.Show("OK");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
               
            }
            this.Close();
        }
    }
}
