using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


/* John Morrissey Lab 2
 * I followed the honor code:
 *          John Morrissey
 * 
*/
public partial class Default3 : System.Web.UI.Page
{
    protected static int projectID;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
    }





    protected void projectGridView_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow row = projectGridView.SelectedRow;
        projectID = Int32.Parse(row.Cells[1].Text);

        editProjName.Text = row.Cells[2].Text;
        editProjDesc.Text = row.Cells[3].Text;

        Control[] ctrlArray = new Control[] { lblEditProjectName, lblEditProjectDesc, editProjDesc, editProjName, btnUpdateProj };
        foreach(Control element in ctrlArray)
        {
            element.Visible = true;
        }
        btnUpdateProj.Enabled = true;

        
        
    }

    protected void btnUpdateProj_Click(object sender, EventArgs e)
    {
        Boolean ensureEntries = true;
        if (editProjName.Text == "")
        {
            ensureEntries = false;
            lblAlert.Text = "Please enter a project name in the field provided.";
            editProjName.Focus();
        }
        if (editProjDesc.Text == "")
        {
            ensureEntries = false;
            lblAlert.Text = "Please enter a project description in the field provided.";
            editProjDesc.Focus();
        }

        if (ensureEntries)
            try
            {
                SqlConnection sqlc = connectToDB();
                string commandText = "UPDATE [dbo].[Project] SET ProjectName = @ProjectName, ProjectDescription = @ProjectDescription, LastUpdated = @LastUpdated, LastUpdatedBy = @LastUpdatedBy " +
                    "WHERE ProjectID =" + projectID;

                SqlCommand update = new SqlCommand(commandText, sqlc);

                update.Parameters.AddWithValue("@ProjectName", editProjName.Text);
                update.Parameters.AddWithValue("@ProjectDescription", editProjDesc.Text);
                update.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
                update.Parameters.AddWithValue("@LastUpdatedBy", (string)Session["user"]);

                update.ExecuteNonQuery();
                sqlc.Close();

                Control[] ctrlArray = new Control[] { lblEditProjectName, lblEditProjectDesc, editProjDesc, editProjName, btnUpdateProj };
                foreach (Control element in ctrlArray)
                {
                    element.Visible = false;
                }

                projectGridView.DataBind();
                lblAlert.Text = "The Project was updated.";
                btnUpdateProj.Enabled = false;
            }
            catch (Exception ex)
            {
            
                lblAlert.Text += "Please ensure your update is entered properly.\n\n" + ex;
            }
    }
}
