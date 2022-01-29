using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969Scheduler
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        // load user data for login form ------------------------------------------------------------------------------------------------------------
        private void Login_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.user' table. You can move, or remove it, as needed.
            this.userTableAdapter.Fill(this.DataSet1.user);


        }
        //-------------------------------------------------------------------------------------------------------------------------------------------

        // login button behaviour -------------------------------------------------------------------------------------------------------------------
        private void logInBtn_Click(object sender, EventArgs e)
        {
            string userName = usernameTxt.Text;
            string passWord = passwordTxt.Text;
            // lambdas are used here to speed the processes for logging in and checking that user inputs matched the proper login information.
            if (DataSet1.user.Any(a => a.userName.Equals(userName)) == true)
            {
                var newUser = DataSet1.user.Where(a => a.userName == userName);
                if(newUser.Any(a => a.password.Equals(passWord)) == true)
                {
                    GVariables.userId = newUser.FirstOrDefault().userId;
                    GVariables.userName = newUser.FirstOrDefault().userName;
                    var appWin = new Main();
                    appWin.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show(Properties.Resources.LoginError, Properties.Resources.LoginFail, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(Properties.Resources.LoginError, Properties.Resources.LoginFail, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------------------
        

        

        
    }
}
