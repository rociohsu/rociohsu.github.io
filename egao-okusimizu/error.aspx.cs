using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _default : MLBasePage
{   //以下為模組會使用到的宣告變數
    public DataView dv1;
   
    //以上為模組會使用到的宣告變數

    protected void Page_Load(object sender, EventArgs e)
    {
        
        init_class(); //初始化class含db connection, email環境設定

        

        string mu = "";
        string mm = "";
        string mpath = "";
        string memail = "";
        string mto = "";
        string mlocation = "";
        int mpolicy = 3;
        try {

            mlocation = IP2LOCATION(mmfrm);
            if (mlocation != "TW") { mpolicy = 1; }
        
        }
        catch (Exception ex) {}


        mpath = aa.chktyp(Request.QueryString["aspxerrorpath"], 0, "C", 0, 250, "", ref mmsg);
        memail = aa.init(Request.ServerVariables["PATH_INFO"].ToUpper(), "後台管理電子報發信E-mail");
        mto = aa.init(Request.ServerVariables["PATH_INFO"].ToUpper(), "後台管理程式人員E-mail");
        if (mto.Length == 0) mto = "asp_debug@medialand.tw";

        mu = aa.init(Request.ServerVariables["PATH_INFO"].ToUpper(), "後台管理標題");

        //mmsg = @"<table style=""font-size:12px""><tr><td>後台管理標題:</td><td>" + mu + "</td></tr>";
        //mmsg += "<tr><td>主 機 名 稱:</td><td>" + Request.ServerVariables["HTTP_HOST"].ToString() + "</td></tr>";
        //mmsg += "<tr><td>程 式 名 稱:</td><td>" + mpath + "</td></tr>";
        //mmsg += "<tr><td>使 用 者 IP:</td><td>" + mmfrm + "</td></tr>";
        //mmsg += "<tr><td>使 用 者 地區:</td><td>" + mlocation + "</td></tr>";
        //if (Session["Page_Error_MSG"] != null) mmsg += @"<tr><td colspan=""2"">錯 誤 原 因:<br>" + Session["Page_Error_MSG"] + "</td></tr>";
        //if (Session["mmnam"] != null) mmsg += "<tr><td>後台操作人員:</td><td>" + Session["mmnam"].ToString() + "</td></tr>";
        //mmsg += "<tr><td></td></tr>";
        //mmsg += "</table><hr>備註說明:";
        //mmsg += @"<ul><li style=""font-size:12px; "">此信是由ERROR.ASPX寄出,攔截由.NET發生的錯誤訊息,如:不存在的ASPX網頁,及.NET 程式邏輯有BUG 時。</li>";
        //mmsg += @"<li style=""font-size:12px; "">若要修改通知信地址,請至後台【系統設定】=> 【後台管理程式人員E-mail】更改即可</li>";
        //mmsg += @"</ul>";
        //mm = aa.sendemail(memail, mto, "", "", memail, "【" + mlocation + "】【" + Request.ServerVariables["HTTP_HOST"].ToString() + "】.NET 網頁程式錯誤通知",mmsg,1, mpolicy, "");
    }

    public string IP2LOCATION(string mip)
    {
        string mmsg = "";
        if (aa.readhtmlcode("http://www.websitelooker.com/ip/" + mip, ref mmsg))
        {
            
            string mstr = mmsg.Substring(mmsg.IndexOf("<strong>Country Name") + 31, 50);
            
            try
            {
                if (mstr.IndexOf("</td>")>=0) 
                {

                    mstr = mstr.Substring(0, mstr.IndexOf("</td>") + 1);
            
                }
                //mstr = mstr.Substring(0, mstr.IndexOf("</td>"));

                mmsg = mmsg.Substring(mmsg.IndexOf("<strong>Country Code") + 31, 2) + "/" + mstr;
            }
            catch (Exception ex) {
                mmsg = "ERRORx";
            }
            
        }

        return mmsg;

    }
}