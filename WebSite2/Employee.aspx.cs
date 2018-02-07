using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Net.Mail;



//John Morrissey Lab 2
/* I followed the Honor Code:
 *          John Morrissey
 * 
 * 
  */
public partial class EmployeeDefault : System.Web.UI.Page
{
    private static int selectSkill;
    private static int selectProject;
    
    
    

    protected void Page_Load(object sender, EventArgs e)
    {
        

        //Sets the connection string for the gridview
        try
        {
            SqlDataSource1.ConnectionString = @"Server =Localhost; Database=Lab3;Trusted_Connection=Yes";
            SqlDataSource1.SelectCommand = "SELECT * FROM[Employee] ORDER BY[EmployeeID]";
            
        }
        catch (Exception)
        {
            SqlDataSource1.ConnectionString = null;
            SqlDataSource1.SelectCommand = null;
        }

        //Set the variables for the users selections
        if (skillDropDown.SelectedIndex > 0)
        {
            selectSkill = skillDropDown.SelectedIndex + 1;
        }
        else
        {
            selectSkill = skillDropDown.SelectedIndex;
        }
        if (projectDropDown.SelectedIndex > 0)
        {
            selectProject = projectDropDown.SelectedIndex + 1;
        }
        else
        {
            selectProject = projectDropDown.SelectedIndex;
        }
        //Select from the database and add that to the drop down
        projectDropDown.Items.Clear();
        
        skillDropDown.Items.Clear();
        
        selectFromDB("SKILL","SkillName", skillDropDown);
        selectFromDB("PROJECT", "ProjectName", projectDropDown);
        //This is assuming that in his database a value of none will be at index of 0
        
    }

    protected void btnCommitEmployee_Click(object sender, EventArgs e)
    {
        resultMessage.Text = "";
        errorMessage.Text = "";
        Boolean ensureDB = true;
        

        try
        {
            //Perform validation to ensure that all user entries are correct
            if (!checkEntries())
            {
                //check to ensure that all textboxes values can be parsed
                errorMessage.Text += " 0001";
                ensureDB = false;
                resultMessage.Text += " Please ensure all entries are valid";
            }
            if (txtTerminationDate.Text != "")
            {
                if (!checkDate(DateTime.Parse(txtHireDate.Text), DateTime.Parse(txtTerminationDate.Text)))
                {
                    //check the hire date against the termination date
                    errorMessage.Text += " 0002";
                    ensureDB = false;
                    resultMessage.Text += " Please ensure the dates entered are correct.\nHire Date " +
                        "cannot be before the termination date.";
                }
            }
            if (selectProject > 0)
            {
                errorMessage.Text += " 0013";
                if (txtProjStart.Text != "" ) //check to make sure the project start date and end date are selected
                {
                    errorMessage.Text += " 0023";
                    if (txtProjEnd.Text != "")
                    {
                        errorMessage.Text += " 0033";
                        if (!checkDate(DateTime.Parse(txtProjStart.Text), DateTime.Parse(txtProjEnd.Text)))
                        {
                            //check the project start date against the project end date
                            errorMessage.Text += " 0043";
                            ensureDB = false;
                            resultMessage.Text += " The project start date must be before the project end date";

                        }
                    }
                }
                else
                {
                    resultMessage.Text += " When adding an employee with a project please enter a start date.";
                    ensureDB = false;
                }
                
            }
            if (selectProject > 0)
            {
                if (!checkDate(DateTime.Parse(txtHireDate.Text), DateTime.Parse(txtProjStart.Text)))
                {
                    errorMessage.Text += " 0004";
                    ensureDB = false;
                    resultMessage.Text += " Please ensure that the hire date is before the project start date.";
                }
            }
            if (!checkAge(DateTime.Parse(txtDateOfBirth.Text)))
            {
                //check to ensure that all users are atleast 18 years old
                errorMessage.Text += " 0005";
                ensureDB = false;
                resultMessage.Text += " Please ensure that all employees are 18 years old when hired.";
                txtHireDate.Focus();
            }
            if (txtState.Text != "")
            {
                if (!checkState(txtState.Text))
                {
                    //check that the user entered a state in the United States
                    errorMessage.Text += " 0006";
                    ensureDB = false;
                    resultMessage.Text += " Please enter a valid US state.";
                    txtState.Focus();
                }
            }
            if (txtManagerID.Text != "")
            {
                if (!findID(int.Parse(txtManagerID.Text)))
                {
                    //check to see if the manager ID already exists
                    errorMessage.Text += "0017";
                    ensureDB = false;
                    resultMessage.Text += " The manager ID you have entered does not exist.";
                    txtManagerID.Focus();
                }
                if(int.Parse(txtManagerID.Text) < 0)
                {
                    errorMessage.Text += " 0027";
                    ensureDB = false;
                    resultMessage.Text += " Manager ID must be positive.";
                    txtManagerID.Focus();
                }
            }
            if (int.Parse(txtSalary.Text) < 0)
            {
                //check to ensure all number entries are positive numbers
                errorMessage.Text += " 0008";
                ensureDB = false;
                resultMessage.Text += " Salary must be a positive number.";
            }
            
            if (ensureDB) //if the boolean passes all the checks then it is valid and can be entered
            {
                //
                string middleInitial = "";
                string state = "";
                DateTime terminationDate = DateTime.MinValue;
                int managerID = 0;

                //if the text fields are empty then set the values to NULL
                if(txtMiddleInitial.Text == "")
                {
                    //middle initial null?
                    middleInitial = "NULL";
                }
                else
                {
                    middleInitial = txtMiddleInitial.Text;
                }

                if(txtState.Text == "")
                {
                    //state null?
                    state = "NULL";
                }
                else
                {
                    state = txtState.Text;
                }

                if(txtTerminationDate.Text == "")
                {
                    //termination date null?
                    terminationDate = DateTime.MinValue;
                }
                else
                {
                    terminationDate = DateTime.Parse(txtTerminationDate.Text);
                }

                if (txtManagerID.Text == "")
                {
                    //manager ID null?
                    managerID = -1;
                }
                else
                {
                    managerID = int.Parse(txtManagerID.Text);
                }

                //Create the new Employee i.e. send to constructor
                Employee newEmployee = new Employee(txtFirstName.Text, txtLastName.Text, middleInitial,
                    txtHouseNum.Text, txtStreet.Text, txtCity.Text, state, txtCountry.Text, txtZip.Text,
                    DateTime.Parse(txtDateOfBirth.Text), DateTime.Parse(txtHireDate.Text), terminationDate,
                    double.Parse(txtSalary.Text), managerID, (string)Session["user"], System.DateTime.Now);

                //Insert the user into the database
                commitEmployeeToDB(newEmployee);

                resultMessage.Text += "User Created: ID# " + findMaxID() + " " + newEmployee.FirstName + " "
                    + newEmployee.LastName;

                if (selectSkill != 0)
                {
                    insertBridgeSkill();
                }
                if (selectProject > 0)
                {
                    
                    insertBridgeProject();
                }

                GridView1.DataBind();
            }
        }
        catch (Exception c)
        {
            resultMessage.Text += " User was not added to the database. Please check your entries.";
            errorMessage.Text += c;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Fields are cleared')", true);
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtMiddleInitial.Text = "";
        txtDateOfBirth.Text = "";
        txtHouseNum.Text = "";
        txtStreet.Text = "";
        txtCity.Text = "";
        txtState.Text = "";
        txtCountry.Text = "";
        txtZip.Text = "";
        txtHireDate.Text = "";
        txtTerminationDate.Text = "";
        txtSalary.Text = "";
        txtManagerID.Text = "";
        txtFirstName.Focus();
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {

        //Exits the web application
        Environment.Exit(0);
    }

    private void selectFromDB(string table, string column, Control cntrl)
    {
        try
        {
            //Connect to the DB
            System.Data.SqlClient.SqlConnection sqlc = connectToDB();

            //Creates a new sql select command to select the data from the skills table
            System.Data.SqlClient.SqlCommand select = new System.Data.SqlClient.SqlCommand();
            select.Connection = sqlc;
            select.CommandText = "select " + column + " from [dbo].[" + table + "]";
            System.Data.SqlClient.SqlDataReader reader;

            reader = select.ExecuteReader();


            while (reader.Read())
            {
                (cntrl as DropDownList).Items.Add(reader.GetString(0));
                
            }
            sqlc.Close();
        }
        catch (Exception c)
        {
            //Shows an error message if there is a problem connecting to the database
            resultMessage.Text += "DROP DOWN ERROR";
            resultMessage.Text += c.Message;
        }
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

    private void insertBridgeSkill()
    {
        try
        {
            //Connect to the DB
            System.Data.SqlClient.SqlConnection sqlc = connectToDB();

            //Creates a new sql insert statement to insert into the bridge table
            System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand();
            insert.Connection = sqlc;
            insert.CommandText = "insert into [dbo].[EMPLOYEESKILL] values (" + findMaxID() + ","
                + selectSkill + ",'" + (string)Session["user"] + "','" + System.DateTime.Now + "')";

            insert.ExecuteNonQuery();
            sqlc.Close();
        }
        catch (Exception c)
        {

            errorMessage.Text += c;
        }
    }

    private void insertBridgeProject()
    {
        try
        {
            //Connect to the DB
            System.Data.SqlClient.SqlConnection sqlc = connectToDB();

            //Creates a new sql insert statement to insert into the bridge table
            System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand();
            insert.Connection = sqlc;
            insert.CommandText = "insert into [dbo].[EMPLOYEEPROJECT] values (" + findMaxID() + ","
                + selectProject + ",'" + DateTime.Parse(txtProjStart.Text) + "',";
            if (txtProjEnd.Text == "")
            {
                insert.CommandText += "NULL,'";
            }
            else
            {
                insert.CommandText += "'" + DateTime.Parse(txtProjEnd.Text) + "','";
            }
            insert.CommandText += (string)Session["user"] + "','" + System.DateTime.Now + "')";
            
            insert.ExecuteNonQuery();
            sqlc.Close();
        }
        catch (Exception c )
        {
            errorMessage.Text += "\n 0009";
            errorMessage.Text += c;
        }
    }

    private int findMaxID()
    {//this finds that max employee id
        try
        {
            System.Data.SqlClient.SqlConnection sqlc = connectToDB();

            //Creates the sql select statement
            System.Data.SqlClient.SqlCommand select = new System.Data.SqlClient.SqlCommand();
            select.Connection = sqlc;

            select.CommandText += "SELECT MAX(EMPLOYEEID) FROM [DBO].[EMPLOYEE]";

            int i = (int)select.ExecuteScalar();
            
            sqlc.Close();
            return i;
        }
        catch (Exception c)
        {
            errorMessage.Text += c;
            return -1;
        }
    }

    private void commitEmployeeToDB(Employee person)
    {
        try
        {
            System.Data.SqlClient.SqlConnection sqlc = connectToDB();

            //Creates the employee insert statement

            //After the objects attributes are set to their values this will run and insert nulls where applicable
             string commandText = "INSERT INTO [dbo].[Employee] values (@firstName, @lastName, @mi, @houseNum, @street, @city, @state, @country, @zip, @dateOfBirth, @hireDate, " +
                "@terminationDate, @salary, @managerID, @lastUpdatedBy, @lastUpdated)";

            System.Data.SqlClient.SqlCommand insert = new System.Data.SqlClient.SqlCommand(commandText, sqlc);
            insert.Parameters.AddWithValue("@firstName", person.FirstName);
            insert.Parameters.AddWithValue("@lastName", person.LastName);

            if (person.MiddleName == "NULL")
            {
                insert.Parameters.AddWithValue("@mi", DBNull.Value);
                
            }
            else
            {
                insert.Parameters.AddWithValue("@mi", person.MiddleName);
            }

            
            insert.Parameters.AddWithValue("@houseNum", person.HouseNum);
            insert.Parameters.AddWithValue("@street", person.Street);
            insert.Parameters.AddWithValue("@city", person.Country);
            if (person.State == "NULL")
            {
                insert.Parameters.AddWithValue("@state", DBNull.Value);
            }
            else
            {
                insert.Parameters.AddWithValue("@state", person.State);
            }

            
            insert.Parameters.AddWithValue("@country", person.Country);
            insert.Parameters.AddWithValue("@zip", person.Zip);
            insert.Parameters.AddWithValue("@dateOfBirth", person.DateOfBirth);
            insert.Parameters.AddWithValue("@hireDate", person.HireDate);

            if (person.TerminationDate == DateTime.MinValue)
            {
                insert.Parameters.AddWithValue("@terminationDate", DBNull.Value);
            }
            else
            {
                insert.Parameters.AddWithValue("@terminationDate", person.TerminationDate);
            }

            insert.Parameters.AddWithValue("@salary", person.Salary);

            if (person.ManagerID == -1)
            {
                insert.Parameters.AddWithValue("@managerID", DBNull.Value);
            }
            else
            {
                insert.Parameters.AddWithValue("@managerID", person.ManagerID);
            }
            insert.Parameters.AddWithValue("@lastUpdatedBy", person.LastUpdatedBy);
            insert.Parameters.AddWithValue("@lastUpdated", person.LastUpdated);

            errorMessage.Text += insert;

            insert.ExecuteNonQuery();

            sqlc.Close();

        }
        catch (Exception c)
        {
            errorMessage.Text += c;
        }
    }

    private Boolean checkEntries()
    {//check that the entries can be parsed before doing any other validations
        try
        {
            DateTime.Parse(txtDateOfBirth.Text);
            DateTime.Parse(txtHireDate.Text);
            if (txtProjStart.Text != "")
            {
                DateTime.Parse(txtProjStart.Text);
                
            }
            if (txtProjEnd.Text != "")
            {
                DateTime.Parse(txtProjEnd.Text);
            }
            
            if (txtTerminationDate.Text != "")
            {
                DateTime.Parse(txtTerminationDate.Text);
            }
            Double.Parse(txtSalary.Text);
            if (txtManagerID.Text != "")
            {
                Int32.Parse(txtManagerID.Text);
            }
            return true;
        }
        catch (Exception c)
        {
            
            errorMessage.Text += c;
            return false;
        }
    }

    private Boolean checkDate(DateTime firstDate, DateTime secondDate)
    {
        try
        {
            //check whether the hire date is before the termination date
            int i = firstDate.CompareTo(secondDate);
            if (i < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        catch (Exception)
        {
            return false;
        }
    }

    private Boolean checkAge(DateTime dateOfBirth)
    {
        if ((dateOfBirth.AddYears(18) <= DateTime.Now))
        {
            if (dateOfBirth.AddYears(18) <= DateTime.Parse(txtHireDate.Text))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }
    }

    private Boolean checkState(string stateAbb)
    {//check that the users state field is a us state
        string[] stateArray = new string[] {"AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
            "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD","MA","MI","MN","MS","MO","MT","NE",
            "NV","NH","NJ","NM","NY","NC","ND","OH","OK","OR","PA","RI","SC","SD","TN","TX","UT",
            "VT","VA","WA","WV","WI","WY"};

        for (int i = 0; i < stateArray.Length; i++)
        {
            if (stateAbb.ToUpper() == stateArray[i])
            {
                return true;
            }

        }

        return false;
    }

    private Boolean findID(int a)
    {
        try
        {
            System.Data.SqlClient.SqlConnection sqlc = connectToDB();

            //Creates the sql select statement
            System.Data.SqlClient.SqlCommand select = new System.Data.SqlClient.SqlCommand();
            select.Connection = sqlc;

            select.CommandText += "SELECT [EMPLOYEEID] FROM [DBO].[EMPLOYEE] WHERE [EMPLOYEEID] = " + a;

            int i = (int)select.ExecuteScalar();

            
            sqlc.Close();
            if (i == a)
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
            errorMessage.Text += c;
            return false;
        }
    }

    





    protected void btnShow_Click(object sender, EventArgs e)
    {//display the gridview
        GridView1.DataBind();
        if (GridView1.Visible == true)
        {
            GridView1.Visible = false;
        }
        else
        {
            GridView1.Visible = true;
        }
        
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        resultMessage.Text += " * ";
    }
}