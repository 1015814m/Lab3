using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


/* John Morrissey Lab 2
 * I followed the honor code:
 *          John Morrissey
 * 
*/
public partial class Default3 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmessage", "alert('Please enter your name')", true);
        
        
    }

    protected void btnCommitProject_Click(object sender, EventArgs e)
    {
        Boolean ensureDB = true;
        
        

        if (ensureDB == true)
        {
            Project newProject = new Project(txtProjectName.Text, txtProjectDescription.Text, (string)Session["user"], System.DateTime.Now);

            try
            {
                System.Data.SqlClient.SqlConnection sqlc = connectToDB();

                //Creates the sql insert statement 
                string commandText = "insert into [dbo].[PROJECT] values (@projectName, @projectDescription, @lastUpdatedBy, @lastUpdated)";

                System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand(commandText, sqlc);

                insert.Parameters.AddWithValue("@projectName", newProject.ProjectName);
                insert.Parameters.AddWithValue("@projectDescription", newProject.ProjectDescription);
                insert.Parameters.AddWithValue("@lastUpdatedBy", newProject.LastUpdatedBy);
                insert.Parameters.AddWithValue("@lastUpdated", newProject.LastUpdated);

                lblAlert.Text = newProject.ProjectName + " was added to the database.";
                insert.ExecuteNonQuery();
                sqlc.Close();

            }
            catch (Exception )
            {
                lblAlert.Text = "There was an error adding to the database.";
            }
        }

    }

    protected void btnClearProject_Click(object sender, EventArgs e)
    {
        txtProjectDescription.Text = "";
        txtProjectName.Text = "";
    }

    protected System.Data.SqlClient.SqlConnection connectToDB()
    {
        try
        {
            //Connects to the database and returns the connection
            System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
            sc.ConnectionString = @"Server =Localhost; Database=Lab3;Trusted_Connection=Yes";
            sc.Open();
            return sc;
        }
        catch (Exception)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertmessage", "alert('There was an error connecting to the Database')", true);
            return null;
        }
    }

    private Boolean checkDB(string a, string colName)
    {
        
        try
        {
            System.Data.SqlClient.SqlConnection sqlc = connectToDB();

            //Creates the sql insert statement 
            System.Data.SqlClient.SqlCommand select = new System.Data.SqlClient.SqlCommand();
            select.Connection = sqlc;

            select.CommandText += "SELECT COUNT (*) [" + colName + "] FROM [DBO].[PROJECT] WHERE [" + colName
                + "] = '" + a + "'";
            lblAlert.Text += select.CommandText;

            int i = (int)select.ExecuteScalar();
            sqlc.Close();
            
            if (i == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        catch (Exception c)
        {
            lblAlert.Text += c;
            return false;
        }
    }
}
