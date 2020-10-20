using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace movies
{
    public partial class Manage : System.Web.UI.Page
    {
        public static string myConnection = "server=localhost;database=moviesproject;user=root;password=ItKo!123";
        public static MySqlConnection conn = new MySqlConnection(myConnection);

        protected void Page_Init(object sender, EventArgs e)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            checkUser();
        }

        protected void loadTableClick(object sender, EventArgs e)
        {
            string table;
            if (userSelect.Checked) { table = "users"; }
            else if (movieSelect.Checked) { table = "movies"; }
            else { return; }

            conn.Open();
            MySqlCommand getTable = new MySqlCommand(String.Format("SELECT * FROM {0};", table).ToString(), conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(getTable);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            TableHeaderRow headerRow = new TableHeaderRow();
            headerRow.ID = "header-row";
            foreach (DataColumn column in ds.Tables[0].Columns)
            {
                TableHeaderCell header = new TableHeaderCell();
                header.Text = String.Join(" ", Regex.Split(column.ColumnName, @"(?<!^)(?=[A-Z])"));
                if (header.Text == "I D") header.Text = "ID";
                headerRow.Controls.Add(header);
            }
            loadTable.Controls.Add(headerRow);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                TableRow rowEl = new TableRow();
                rowEl.ID = row["ID"].ToString();
                loadTable.Controls.Add(rowEl);

            }
            foreach (DataColumn column in ds.Tables[0].Columns)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {


                    if (column.ColumnName == "ID")
                    {
                        TableCell cell = new TableCell();
                        cell.Text = row["ID"].ToString();
                        cell.ID = "ID" + '-' + row["ID"];
                        FindControl(row["ID"].ToString()).Controls.Add(cell);
                    }
                    else if (column.ColumnName != "Admin")
                    {
                        TableCell cell = new TableCell();
                        TextBox inputCell = new TextBox();
                        inputCell.Text = row[column.ColumnName].ToString();
                        cell.ID = column.ColumnName + '-' + row["ID"];
                        inputCell.ID = cell.ID + '-' + "input";
                        cell.Controls.Add(inputCell);
                        FindControl(row["ID"].ToString()).Controls.Add(cell);
                    }
                    else
                    {
                        TableCell cell = new TableCell();
                        CheckBox inputCell = new CheckBox();
                        if (row[column.ColumnName].ToString() == "True")
                        {
                            inputCell.Checked = true;
                        }
                        else
                        {
                            inputCell.Checked = false;
                        }
                        cell.ID = column.ColumnName + '-' + row["ID"];
                        inputCell.ID = cell.ID + '-' + "input";
                        cell.Controls.Add(inputCell);
                        FindControl(row["ID"].ToString()).Controls.Add(cell);
                    }


                }
            }
            conn.Close();
        }
        protected void submitChangesClick(object sender, EventArgs e)
        {
            string tableDB;
            if (userSelect.Checked) { tableDB = "users"; }
            else { tableDB = "movies"; }
            if (changedElements.Value == "") return;
            string[] toChange = changedElements.Value.Split('*');
            if (conn.State != ConnectionState.Open)
            {
                OpenConnection();
            }
            foreach (string element in toChange)
            {
                string rowID = element.Split('^')[0].Split('-')[1];
                string columnID = element.Split('^')[0].Split('-')[0];
                string value = element.Split('^')[1];

                string updateQuery = string.Format("UPDATE {0} SET {1} = @2 WHERE ID = @3;", tableDB, columnID);
                MySqlCommand updateCommand = new MySqlCommand(updateQuery, conn);

                if (value == "false" || value == "true")
                {
                    updateCommand.Parameters.AddWithValue("@2", bool.Parse(value));
                }
                else 
                {
                    updateCommand.Parameters.AddWithValue("@2", value);
                }
                updateCommand.Parameters.AddWithValue("@3", int.Parse(rowID));
                updateCommand.ExecuteNonQuery();


            }


            conn.Close();
        }




        public void checkUser()
        {
            if (Session.Keys.Count > 0 && Session["Admin"].ToString() == "True")
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
    }
}