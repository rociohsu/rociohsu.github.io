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
    public DataView dv1;
    public int mpage, mc;

    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));   
        //如果是 _mod.aspx 請加下面一行,//_mod.aspx修改頁時, chkath會固定用msno (資料key=0 or !=0)來判斷是新增或修改模式, 請放這行才能正常呈現權限
        //msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref mmsg); if (msno == "0") msno = aa.chktyp(Request.QueryString["sno"], 0, "I", 0, 0, "", ref mmsg); 
        if (!chkath()) { mng_main.Visible = false; }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...

        
    }
 
}