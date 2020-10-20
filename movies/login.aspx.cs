using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;

namespace movies
{
    public partial class Login : System.Web.UI.Page
    {
        public static Dictionary<string, string> formData;
        public static bool err;
        public static string myConnection = "server=localhost;database=moviesproject;user=root;password=ItKo!123";
        public static MySqlConnection conn = new MySqlConnection(myConnection);
        protected void Page_Load(object sender, EventArgs e)
        {
            checkUser();

        }
        public void login(object sender, EventArgs e)
        {
            if (conn.State != ConnectionState.Open)
            {
                OpenConnection();
            }
            MySqlCommand userPass = new MySqlCommand("SELECT * FROM users;", conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(userPass);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row["Username"] as string == user.Value)
                {
                    if (row["Password"] as string == pass.Value)
                    {
                        foreach (DataColumn column in ds.Tables[0].Columns)
                        {
                            Session.Add(column.ColumnName, row[column.ColumnName]);
                        }
                        successLog.Checked = true;
                        return;
                    }
                    else
                    {
                        wrongPass.Checked = true;
                        return;
                    }
                }

            }
            conn.Close();
            noUser.Checked = true;

        }
        public static bool OpenConnection()
        {

            conn.Open();
            return true;

        }

        public static bool CloseConnection()
        {
            conn.Close();
            return true;
        }
        public void checkUser()
        {
            if (Session.Keys.Count > 0)
            {
                loginRedirect.Visible = false;
                signupRedirect.Visible = false;
                HtmlGenericControl[] els;
                if (Session["Admin"].ToString() == "True")
                { els = loggedInAdmin(); }
                else { els = loggedIn(); }
                foreach (var el in els)
                {
                    navbar.Controls.Add(el);
                }
            }
        }
        public static HtmlGenericControl[] loggedIn()
        {
            var addBtn = new HtmlGenericControl("a");
            addBtn.InnerHtml = "Add Movie";
            addBtn.Attributes.Add("href", "add");
            var plusImg = new HtmlGenericControl("img");
            plusImg.Attributes.Add("src", "Content/plus.png");
            addBtn.Controls.Add(plusImg);
            var accBtn = new HtmlGenericControl("a");
            accBtn.Attributes.Add("href", "account");
            accBtn.Attributes.Add("style", "float: right; width: 16%; text-align: center;");
            accBtn.InnerHtml = "Account";
            return new HtmlGenericControl[] { addBtn, accBtn };

        }
        public static HtmlGenericControl[] loggedInAdmin()
        {
            var loggedButtons = loggedIn();
            var addBtn = loggedButtons[0];
            var accBtn = loggedButtons[1];
            var manageBtn = new HtmlGenericControl("a");
            manageBtn.Attributes.Add("href", "manage");
            manageBtn.Attributes.Add("id", "manageButton");
            manageBtn.InnerHtml = "Manage Site";
            var toolImg = new HtmlGenericControl("img");
            toolImg.Attributes.Add("src", "Content/tool.png");
            manageBtn.Controls.Add(toolImg);
            return new HtmlGenericControl[] { addBtn, accBtn, manageBtn };
        }
    }
}
