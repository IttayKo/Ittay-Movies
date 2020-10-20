using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.HtmlControls;



namespace movies
{
    public partial class Signup : System.Web.UI.Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            checkUser();
        }

        public static string firstNameSign, lastNameSign, emailSign, phoneSign, userSign, passSign;
        public static Dictionary<string, string> formData;
        public static bool err;
        public static string myConnection = "server=localhost;database=moviesproject;user=root;password=ItKo!123";
        public static MySqlConnection conn = new MySqlConnection(myConnection);
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
        public bool checkForm()
        {
            if (checkErr.Checked) { return false; }
            firstNameSign = fNameNew.Value;
            lastNameSign = lNameNew.Value;
            emailSign = emailNew.Value;
            phoneSign = phoneNew.Value;
            userSign = userNew.Value;
            passSign = passNew.Value;
            formData = new Dictionary<string, string>() {
                { "FirstName", firstNameSign}, { "LastName", lastNameSign}, {"Email", emailSign},
                {"Phone", phoneSign}, {"Username", userSign}, {"Password", passSign}};
            MySqlCommand selUsrEml = new MySqlCommand();
            selUsrEml.CommandText = "SELECT Username, Email FROM users;";
            selUsrEml.Connection = conn;
            if (conn.State != ConnectionState.Open)
            {
                OpenConnection();
            }
            MySqlDataAdapter adapter = new MySqlDataAdapter(selUsrEml);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            int length = ds.Tables[0].Rows.Count;
            for (int i = 0; i < length; i++)
            {
                if (userSign == ds.Tables[0].Rows[i][0] as string)
                {
                    userTaken.Checked = true;

                    return false;
                }
                if (emailSign == ds.Tables[0].Rows[i][1] as string)
                {
                    emailTaken.Checked = true;
                    return false;
                }
            }

            addUser.Checked = true;

            return true;


        }



        public static void insertUser()
        {
            using (conn)
            {
                string[] vals = formData.Values.ToArray();
                string query = "INSERT INTO users (FirstName, LastName, Email, Phone, Username, Password) VALUES (@0, @1, @2, @3, @4, @5);";
                MySqlCommand insertUser = new MySqlCommand(query, conn);
                for (int i = 0; i < formData.Count; i++)
                {
                    insertUser.Parameters.AddWithValue('@' + i.ToString(), vals[i]);
                }
                insertUser.ExecuteNonQuery();
            }

        }

        public void signUp(object sender, EventArgs e)
        {

            if (checkForm() == true)
            {
                insertUser();
                foreach (KeyValuePair<string, string> keyVal in formData)
                {
                    if (keyVal.Key != "Password")
                    {
                        Session.Add(keyVal.Key, keyVal.Value);
                    }
                }
            }
            conn.Close();
        }
    }
}
