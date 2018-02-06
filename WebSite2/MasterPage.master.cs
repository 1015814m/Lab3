using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// John Morrissey Lab 2
// I followed the honor code:
//              John Morrissey
public partial class MasterPage : System.Web.UI.MasterPage
{
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Session["user"] = "John Morrissey";
    }

    

    
}
