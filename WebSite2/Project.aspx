<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Project.aspx.cs" Inherits="Default3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- John Morrissey Lab 2
        I have followed the honor code:
                John Morrissey -->
    <section id="projectAreaOne">
            <br />
            <asp:Label ID="lblProjectName" runat="server" Text="Project Name: " Width="10%"></asp:Label>
            <asp:TextBox required="" MaxLength="35" ID="txtProjectName" runat="server" CssClass="inputText" formnovalidate="false" ></asp:TextBox>
            <br />
            <br />

            <asp:Label ID="lblProjectDescription" runat="server" Text="Project Description: " Width="10%" ></asp:Label>
            <asp:TextBox required="" MaxLength="100" ID="txtProjectDescription" runat="server" CssClass="inputText" formnovalidate="false"></asp:TextBox>
            <br />
            <br />
            <asp:Button CssClass="btn" ID="btnCommitProject" runat="server" Text="Commit" OnClick="btnCommitProject_Click" />

            <asp:Button CssClass="btn" ID="btnClearProject" runat="server" Text="Clear" OnClick="btnClearProject_Click" />
            <br />
            
        <br />
    </section>
    <section id="projectAreaTwo">


        <asp:Label ID="lblSearch" runat="server" Text="Please enter the name of the project you would like to edit: "></asp:Label>


        <br />
        <asp:TextBox ID="txtSearch" runat="server" CssClass="inputText" formnovalidate="true" MaxLength="100"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" formnovalidate="true" CssClass="btn"/>

        <br />

        <br />
        <asp:GridView ID="projectGridView" 
            runat="server"  
            AutoGenerateColumns="False" 
            DataKeyNames="ProjectID" 
            DataSourceID="ProjectSearchDataSource" 
            AutoGenerateEditButton="False"
            AllowPaging="True" OnSelectedIndexChanged="projectGridView_SelectedIndexChanged" 
            >
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" InsertVisible="False" ReadOnly="True" SortExpression="ProjectID"  />
                <asp:BoundField DataField="ProjectName" HeaderText="ProjectName" SortExpression="ProjectName" ReadOnly="false" />
                <asp:BoundField DataField="ProjectDescription" HeaderText="ProjectDescription" SortExpression="ProjectDescription" ReadOnly="false" />
                <asp:BoundField DataField="LastUpdatedBy" HeaderText="LastUpdatedBy" SortExpression="LastUpdatedBy" ReadOnly="true" />
                <asp:BoundField DataField="LastUpdated" HeaderText="LastUpdated" SortExpression="LastUpdated" ReadOnly="true"/>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource
            ID="ProjectSearchDataSource" 
            runat="server" 
            ConnectionString="<%$ ConnectionStrings:ProjectSearchConnectionString %>" 
            SelectCommand="SELECT * FROM [Project] WHERE ([ProjectName] LIKE '%' + @ProjectName + '%')"
            >
            <SelectParameters>
                <asp:ControlParameter ControlID="txtSearch" Name="ProjectName" PropertyName="Text" Type="String" />
            </SelectParameters>
            
            
        </asp:SqlDataSource>

        <asp:Label ID="lblEditProjectName" runat="server" Text="Change the Project Name: " Width="10%" Visible="false" ></asp:Label>
        <asp:TextBox ID="editProjName" runat="server" CssClass="inputText" Visible="false" formnovalidate="true" MaxLength="35"></asp:TextBox>
        <br />
        <asp:Label ID="lblEditProjectDesc" runat="server" Text="Change the Product Description: " Width="10%" Visible="false" ></asp:Label>
        <asp:TextBox ID="editProjDesc" runat="server" CssClass="inputText" Visible="false" formnovalidate="true" MaxLength="100"></asp:TextBox>

        <br />
        <asp:Button ID="btnUpdateProj" runat="server" Text="Update Project"  CssClass="btn" OnClick="btnUpdateProj_Click" Visible="false" formnovalidate=""/>

        <br />


    </section>
    <asp:Label ID="lblAlert" runat="server" Text="" ></asp:Label>
</asp:Content>

