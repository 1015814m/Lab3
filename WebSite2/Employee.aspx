<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Employee.aspx.cs" Inherits="EmployeeDefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- John Morrissey Lab 2
        I have followed the honor code:
                John Morrissey -->
    
    <section>
        <section style="width: 25%; float: left;" >
                <p>
        
                <br />
                    <asp:Label ID="lblFirstName" runat="server" Text="First Name: " Width="100px" ></asp:Label>
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="longInputText" required="" MaxLength="20"></asp:TextBox>
        
                </p>
                <p>
                    <asp:Label ID="lblLastName" runat="server" Text="Last Name: " Width="100px"></asp:Label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="longInputText" required="" MaxLength="30"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="lblMiddleInitial" runat="server" Text="Middle Initial*: " Width="100px"></asp:Label>
                    <asp:TextBox ID="txtMiddleInitial" runat="server" CssClass="shortInputText" MaxLength="1"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="lblDateOfBirth" runat="server" Text="Date of Birth: " Width="100px"></asp:Label>
                    <asp:TextBox ID="txtDateOfBirth" placeholder="YYYY-MM-DD" runat="server" CssClass="mediumInputText" required="" MaxLength="10"></asp:TextBox>
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Label ID="lblHouseNum" runat="server" Text="House Number: " Width="100px"></asp:Label>
                    <asp:TextBox ID="txtHouseNum" runat="server" CssClass="mediumInputText" required="" MaxLength="10"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="lblStreet" runat="server" Text="Street: " Width="100px"></asp:Label>
                    <asp:TextBox ID="txtStreet" runat="server" CssClass="longInputText" required="" MaxLength="20"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="lblCity" runat="server" Text="City: " Width="100px"></asp:Label>
                    <asp:TextBox ID="txtCity" runat="server" CssClass="longInputText" required="" MaxLength="25"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="lblStateAbb" runat="server" Text="State Abbreviation*: " Width="100px"></asp:Label>
                    <asp:TextBox ID="txtState" runat="server" CssClass="shortInputText" MaxLength="2"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="lblCountry" runat="server" Text="Country Abbreviation: " Width="100px"></asp:Label>
                    <asp:TextBox ID="txtCountry" runat="server" CssClass="shortInputText" required="" MaxLength="2"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="lblZip" runat="server" Text="Zip Code: " Width="100px"></asp:Label>
                    <asp:TextBox ID="txtZip" runat="server" CssClass="mediumInputText" required="" MaxLength="5"></asp:TextBox>
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Label ID="lblHireDate" runat="server" Text="Hire Date: " Width="100px"></asp:Label>
                    <asp:TextBox  placeholder="YYYY-MM-DD" ID="txtHireDate" runat="server" CssClass="mediumInputText" required="" MaxLength="10"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="lblTerminationDate" runat="server" Text="Termination Date*: " Width="100px"></asp:Label>
                    <asp:TextBox placeholder="YYYY-MM-DD" ID="txtTerminationDate" runat="server" CssClass="mediumInputText" maxlength="10"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="lblSalary" runat="server" Text="Salary: " Width="100px"></asp:Label>
                    <asp:TextBox placeholder="e.g. 45000" ID="txtSalary" runat="server" CssClass ="mediumInputText" required=""  ></asp:TextBox>
                </p>
                <p >
                    <asp:Label ID="lblManagerID" runat="server" Text="Manager ID*: " Width="100px"></asp:Label>
                    <asp:TextBox ID="txtManagerID" runat="server" CssClass="shortInputText" ></asp:TextBox>
                </p>
                <p >
                    <asp:Label ID="lblProjectSelection" runat="server" Text="Project: " Width="100px"></asp:Label>
                    <asp:DropDownList ID="projectDropDown" runat="server" >
                    </asp:DropDownList>
                </p>
                <p >
                    <asp:Label ID="lblProjectStart" runat="server" Text="Project Start Date: " Width="100px"></asp:Label>
                    <asp:TextBox ID="txtProjStart" runat="server" placeholder="YYYY-MM-DD" CssClass="mediumInputText" MaxLength="10"></asp:TextBox>
                </p>
                <p >
                    <asp:Label ID="lblProjEnd" runat="server" Text="Project End Date:" Width="100px"></asp:Label>
                    <asp:TextBox ID="txtProjEnd" runat="server" placeholder="YYYY-MM-DD" CssClass="mediumInputText" MaxLength="10"></asp:TextBox>
                </p>
                    <p >
                        <asp:Label ID="lblSkillSelection" runat="server" Text="Skills: " Width="100px"></asp:Label>
                        <asp:DropDownList ID="skillDropDown" runat="server">
                        </asp:DropDownList>
                </p>
                <p>
                    <asp:Button CssClass="btn" ID="btnCommitEmployee" runat="server" Text="Commit" OnClick="btnCommitEmployee_Click" />
                    <asp:Button CssClass="btn" ID="btnClear" runat="server" Text="Clear" formnovalidate="" OnClick="btnClear_Click" />
                    <asp:Button CssClass="btn" ID="btnExit" runat="server" Text="Exit" OnClick="btnExit_Click" formnovalidate=""/>
                    <asp:Button CssClass="btn" ID="btnShow" runat="server" Text="Show Gridview" formnovalidate="" OnClick="btnShow_Click" />
                </p>
                <p>
                    <asp:Label ID="resultMessage" runat="server" Text=""></asp:Label>
                    </p>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GridviewConnection %>" SelectCommand="SELECT [DBO].[EMPLOYEE].[EMPLOYEEID],[DBO].[EMPLOYEE].[FIRSTNAME],[DBO].[EMPLOYEE].[LASTNAME],[DBO].[PROJECT].[PROJECTNAME]
    FROM [DBO].[EMPLOYEE]
    LEFT JOIN [DBO].[EMPLOYEEPROJECT]
    ON [DBO].[EMPLOYEE].[EMPLOYEEID] = [DBO].[EMPLOYEEPROJECT].[EMPLOYEEID]
    LEFT JOIN [DBO].[PROJECT]
    ON [DBO].[PROJECT].[PROJECTID] = [DBO].[EMPLOYEEPROJECT].[PROJECTID]"></asp:SqlDataSource>
                    <!-- If there is an error with the datasource take code from backup -->
                <p>
                    <asp:Label ID="errorMessage" runat="server" Text="" Visible="true"></asp:Label>
                </p>
                <p>
                    &nbsp;</p>
            </section>
        
            
            
            
        
        <aside >
            <br />
            <asp:GridView  style="float: left;"   ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLOYEEID" DataSourceID="SqlGridView" AllowPaging="True" AllowSorting="True" Font-Size="Small" Width="50%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" >
                <Columns>
                    <asp:BoundField DataField="EMPLOYEEID" HeaderText="EMPLOYEEID" InsertVisible="False" ReadOnly="True" SortExpression="EMPLOYEEID" />
                    <asp:BoundField DataField="FIRSTNAME" HeaderText="FIRSTNAME" SortExpression="FIRSTNAME" />
                    <asp:BoundField DataField="LASTNAME" HeaderText="LASTNAME" SortExpression="LASTNAME" />
                    <asp:BoundField DataField="PROJECTNAME" HeaderText="PROJECTNAME" SortExpression="PROJECTNAME" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlGridView" runat="server" ConnectionString="<%$ ConnectionStrings:GridviewConnection %>" SelectCommand="SELECT [DBO].[EMPLOYEE].[EMPLOYEEID],[DBO].[EMPLOYEE].[FIRSTNAME],[DBO].[EMPLOYEE].[LASTNAME],[DBO].[PROJECT].[PROJECTNAME]
FROM [DBO].[EMPLOYEE]
LEFT JOIN [DBO].[EMPLOYEEPROJECT]
ON [DBO].[EMPLOYEE].[EMPLOYEEID] = [DBO].[EMPLOYEEPROJECT].[EMPLOYEEID]
LEFT JOIN [DBO].[PROJECT]
ON [DBO].[PROJECT].[PROJECTID] = [DBO].[EMPLOYEEPROJECT].[PROJECTID]"></asp:SqlDataSource>
            </aside>
            
        </section>
</asp:Content>












