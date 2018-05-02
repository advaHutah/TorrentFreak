using DatabaseLibrary;
using objectClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal
{
    public partial class FileManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            activeUsersText.Text = UsersDBClass.Instance.getNumberOfUsers(true).ToString();
            totalUsersText0.Text = UsersDBClass.Instance.getNumberOfUsers(false).ToString();
            totalFilestext.Text = FilesGrid.Rows.Count.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            List<SearchResult> result = FilesDBClass.Instance.findListOfFilesByFileName(searchTxt.Text);
            GridView1.DataSource = result;
            GridView1.DataBind();
        }
    }
}