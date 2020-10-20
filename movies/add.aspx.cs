using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI.HtmlControls;

namespace movies_project
{
    public partial class add : System.Web.UI.Page
    {
        public static string myConnection = "server=localhost;database=moviesproject;user=root;password=ItKo!123";
        public static MySqlConnection conn = new MySqlConnection(myConnection);
        protected void Page_Load(object sender, EventArgs e)
        {
            checkUser();
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
            else
            {
                Response.Redirect("index.aspx");

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

        public void submitMovie(object sender, EventArgs e)
        {
            if (conn.State != ConnectionState.Open)
            {
                OpenConnection();
            }
            MySqlCommand getMovies = new MySqlCommand("SELECT Name, src, Username FROM movies;", conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(getMovies);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (row["Name"].ToString() == movieName.Value || row["src"].ToString() == movieURL.Value)
                {
                    Response.Write(String.Format("<script>alert('Movie already added (by user: {0})')</script>", row["Username"].ToString()));
                    movieForm.Dispose();

                    return;
                }
            }

            string query = "INSERT INTO movies (Name, Description, Rating, src, Username) VALUES (@0, @1, @2, @3, @4);";
            string[] vals = new string[] { movieName.Value, movieDescription.Value, countStars.Value, movieURL.Value, Session["Username"].ToString() };
            MySqlCommand insertMovie = new MySqlCommand(query, conn);
            for (int i = 0; i < 5; i++)
            {
                insertMovie.Parameters.AddWithValue('@' + i.ToString(), vals[i]);
            }
            insertMovie.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("index");
        }
    }
}
