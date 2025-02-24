using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using Microsoft.Win32;
using System.IO.Compression;

public partial class _default : MLBasePage
{
    public DataView dv1,dv2,dv3;
    public string mpdt_list1 = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));     
        if (!chkath()) { }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
                            //以下開始coding...
        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 5, "", ref mmsg);

          


      

    }
   
}
