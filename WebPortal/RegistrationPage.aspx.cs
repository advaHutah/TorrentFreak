using DatabaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal
{
    public partial class RegistrationPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void register_Button_Click(object sender, EventArgs e)
        {
            if(UsersDBClass.Instance.addNewUser(userName_TextBox.Text, password_TextBox.Text))
            {
                userName_TextBox.Text = "";
                password_TextBox.Text = "";
                statusRegestraionLable.Text = "User was created successfully";
            }
            else
            {
                statusRegestraionLable.Text = "User is already exist";
            }
        }
    }
}