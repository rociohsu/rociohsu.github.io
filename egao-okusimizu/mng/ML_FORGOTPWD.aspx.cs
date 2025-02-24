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
    public string xurl, xpara;
    public SqlCommand sqlcom = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["mmuno"] != null) Response.Redirect(mmmng+"CABASE_indexmain.aspx");        
        init_class(); //初始化class含db connection, email環境設定
        //以下開始coding...
      
            DataView dv1;
            string mb1; string mMsg; string maaa; string mccc; string[] xxips;
            int xxips_sts;


            Label1.Text = "";
            string mtemp_str = aa.init("", "後台管理IP");
            xxips = mtemp_str.Split(',');
            xxips_sts = 0;
            for (int i = 0; i < xxips.Length; i++)
            {
                if (xxips[i].ToString().IndexOf(mmfrm) >= 0 || xxips[i].Trim() == "*") xxips_sts = 1;
                //Response.Write( xxips[i].Trim()+"<br>");
            }


            if (xxips.Length < 0 || xxips_sts == 0)
            {
                // Response.Write(xxips.Length.ToString());
                Response.End();
            }


            mMsg = "";
            mb1 = "";
            mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 3, "", ref mMsg);

            if (mb1 == "SAV" && Session["vcode"] != null)
            {

               

                xurl = aa.chktyp(Request.Form["xurl"], 0, "C", 0, 250, "", ref mMsg);
                xpara = aa.chktyp(Request.Form["xpara"], 0, "C", 0, 250, "", ref mMsg);
                maaa = aa.chktyp(Request.Form["aaa"], 1, "E", 0, 50, "E-mail", ref mMsg);
                
                mccc = aa.chktyp(Request.Form["ccc"], 1, "C", 0, 6, "驗證碼", ref mMsg);

                if (mccc.Trim() != Session["vcode"].ToString().Trim())
                {
                    mMsg = "驗證碼錯誤,請注意大小寫";
                }
                if (mMsg.Length > 0)
                {
                    Label1.Text = mMsg;
                }
                else
                {
                sqlcom.Parameters.Add("@aaa", SqlDbType.NVarChar, 250).Value = maaa;
                sqlcom.CommandText = "select * from CABASE_user where email=@aaa";
                dv1 = aa.dv_param(sqlcom, ref mMsg);

                

                    if (dv1 != null)
                    {
                        if (dv1.Count > 0)
                        {
                            if ((int)dv1[0]["onf"] == 1)
                            { string rtn = aa.sendemail(aa.xxemail, aa.chkdbnull(dv1[0]["email"]), "", "", aa.xxemail, "忘記密碼通知", "您的密碼:" + aa.decrypt(aa.chkdbnull(dv1[0]["pwd"])) + @"<p align=""right"">來自:" + Request.ServerVariables["HTTP_HOST"].ToString() + Request.ServerVariables["Path_Info"].ToString() + @"</p>", 1, 3, "");
                                if ( rtn == "OK") { Label1.Text = "密碼已寄至您的信箱,請收信"; aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]","[" +  Session["mmnam"] + "]", "[動作:查詢][資料ID:" + maaa + "]", "忘記密碼"); }
                                else { Label1.Text = "寄送失敗,請洽資訊人員"+rtn; aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]","[" +  Session["mmnam"] + "]", "[動作:查詢][資料ID:" + maaa + "][寄送失敗]"+rtn,"忘記密碼"); }
                        }
                            else
                            {
                                dv1.Dispose();

                                Label1.Text = "此帳號已停用";

                            }
                        }
                        else
                        {
                            dv1.Dispose();

                            Label1.Text = "查無此E-mail";

                        }
                    }
                    else
                    {

                        Label1.Text = mMsg;
                    }
                }


            }

            else
            {
                xpara = aa.chktyp(Request.QueryString["xpara"], 0, "C", 0, 250, "", ref mMsg);
                xurl = aa.chktyp(Request.QueryString["xurl"], 0, "C", 0, 250, "", ref mMsg);


            }

        //Session("vcode") = mid(cstr(10000+fix(rnd()*10000)),2,4);
        //Session["vcode"] = string.Format("{0:0000}", aa.rnd(10000)); // aa.generatevcode(4);
        if (sqlcom != null) sqlcom.Dispose();

            xxips = aa.init(Request.ServerVariables["PATH_INFO"].ToUpper().Trim(), "後台管理IP").Split(',');
            xxips_sts = 0;

            for (int i = 0; i < xxips.Length; i++)
            {
                if (xxips[i] == "*" || Request.ServerVariables["remote_addr"].IndexOf(xxips[i]) >= 0) xxips_sts = 1;
            }

            if (xxips_sts == 0) Response.Redirect("../");


            if (Session["後台管理標題"] != null) Session["後台管理標題"] = aa.init(Request.ServerVariables["PATH_INFO"].ToUpper().Trim(), "後台管理標題");

            if (Session["後台管理版權"] != null) Session["後台管理版權"] = aa.init(Request.ServerVariables["PATH_INFO"].ToUpper().Trim(), "後台管理版權");

        
    }
    
}