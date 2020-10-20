using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;

namespace movies
{
    public partial class Index : System.Web.UI.Page
    {
        public string firstName, lastName, userName;
        public static Dictionary<string, string> formData;
        public static bool err;
        public static string myConnection = "server=localhost;database=moviesproject;user=root;password=ItKo!123";
        public static MySqlConnection conn = new MySqlConnection(myConnection);

        protected void Page_Load(object sender, EventArgs e)
        {
            checkUser();
            showMovies();
        }
        public void showMovies()
        {
            if (conn.State != ConnectionState.Open)
            {
                OpenConnection();
            }
            MySqlCommand getMovies = new MySqlCommand("SELECT * FROM movies;", conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(getMovies);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                HtmlGenericControl movie = new HtmlGenericControl("div");
                movie.Attributes.Add("class", "movie");
                HtmlGenericControl left = new HtmlGenericControl("section");
                left.Attributes.Add("class", "left");
                HtmlGenericControl right = new HtmlGenericControl("section");
                right.Attributes.Add("class", "right");
                HtmlGenericControl vidWrap = new HtmlGenericControl("div");
                vidWrap.Attributes.Add("class", "vid-wrap");
                HtmlGenericControl trailer = new HtmlGenericControl("iframe");
                trailer.Attributes.Add("class", "movie-trailer");
                trailer.Attributes.Add("controls", "0");
                trailer.Attributes.Add("height", "9");
                trailer.Attributes.Add("width", "16");
                vidWrap.Controls.Add(trailer);
                HtmlGenericControl rating = new HtmlGenericControl("div");
                rating.Attributes.Add("class", "movie-rating");
                rating.InnerText = "Rating:";
                HtmlGenericControl movieName = new HtmlGenericControl("h3");
                movieName.Attributes.Add("class", "movie-name");
                HtmlGenericControl movieDescription = new HtmlGenericControl("p");
                movieDescription.Attributes.Add("class", "movie-description");
                string src = ConvertYouTubeToEmbed(row["src"].ToString());
                trailer.Attributes.Add("src", src);
                int stars = int.Parse(row["Rating"].ToString());
                for (int i = 1; i < 6; i++)
                {
                    HtmlGenericControl star = new HtmlGenericControl("img");
                    if (i <= stars) { star.Attributes.Add("src", "Content/fullstar.png"); }
                    else { star.Attributes.Add("src", "Content/emptystar.png"); star.Attributes.Add("style", "opacity: 80%"); }
                    rating.Controls.Add(star);
                }
                movieName.InnerText = row["Name"].ToString();
                movieDescription.InnerText = row["Description"].ToString();
                left.Controls.Add(vidWrap);
                left.Controls.Add(rating);
                right.Controls.Add(movieName);
                right.Controls.Add(movieDescription);
                movie.Controls.Add(left);
                movie.Controls.Add(right);
                movies.Controls.Add(movie);
            }




        }
        private static string ConvertYouTubeToEmbed(string url)
        {
            string vidID = url.Split('=')[1];

            return "https://www.youtube.com/embed/" + vidID;
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
    }
}