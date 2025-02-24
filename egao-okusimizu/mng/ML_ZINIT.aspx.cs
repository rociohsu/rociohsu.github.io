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

public partial class _default : System.Web.UI.Page
{
    public DataView dv1;
    public int mpage, mc;
    public string xurl,xpara;
    public string mmpara; //2012/2/22新增,CMS後台共用.aspx單元用
    public string mmtitle, mb1, conn_str, mito, xord, ord, msno, mtyp, xsot, mt1, msdt, medt, mmsg, mmsg2, whr, msel, XXurl, YYurl, ZZurl, XXauth, XXunt_loc, mSql, mmfrm ,mmmpath,mmmng, mmroot,mmcoot;
    public SqlCommand msqlcom = new SqlCommand();
    public MLclass aa = new MLclass();   


    protected void Page_Load(object sender, EventArgs e)
    {

        mmmpath = "";
        mmmng = "";
        mmroot = "";
        mmsg2 = "";
        init_class();

        mmsg = "";
        mmmpath = Request.ServerVariables["Path_Info"].ToString();
        mmmng = mmmpath;
        //if (mmmpath.ToLower().IndexOf("/" + aa.xxmng.ToLower()) >= 0) { mmmng = mmmpath.Substring(0, mmmpath.ToLower().IndexOf("/" + aa.xxmng.ToLower())) + "/" + aa.xxmng + "/"; mmroot = mmmpath.Substring(0, mmmpath.ToLower().IndexOf("/" + aa.xxmng.ToLower())); mmcoot = mmroot; }
        //以下開始coding...
        DataView dv1;
        string mb1; string mMsg; string maaa; string mbbb; string mccc; string[] xxips;
        int xxips_sts;
        Label1.Text = "";
        mMsg = "";
        mb1 = "";
        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 3, "", ref mMsg);

        if (mb1 == "ENC")
        {
            msqlcom.CommandText = "update CABASE_user set pwd='" + aa.encrypt("sampleadm") + "' where uno=1 and pwd='sampleadm'";
            if (aa.exec_param(msqlcom, ref mmsg)) mmsg2 = "加密完成";
            else mmsg2 = "加密失敗"+mmsg;
        }
        if (mb1 == "SAV" && Session["vcode"] != null)
        {
            msqlcom.CommandText = "select * from CABASE_user";

            dv1 = aa.dv_param(msqlcom, ref mmsg);
            if (dv1 != null)
            {
                mmsg2 = "DB已建立過,不會再執行";
            }
            else
            {
                mccc = aa.chktyp(Request.Form["ccc"], 1, "C", 0, 6, "驗證碼", ref mMsg);
                if (mccc.Trim() != Session["vcode"].ToString().Trim())
                {
                    mMsg = "驗證碼錯誤,請注意大小寫";
                }
                if (mMsg.Length > 0)
                {
                    mmsg2= mMsg;
                }
                else
                {
                    if (aa.readfile(Server.MapPath("table_spl.sql"), ref mmsg))
                    {
                        msqlcom.CommandText = mmsg;
                        if (aa.exec_param(msqlcom, ref mmsg)) { mmsg2 += "執行完成Success1,"; }
                        else mmsg2 += "執行失敗11,"+mmsg;
                    }
                    else mmsg2 += "讀取失敗1,no file";
                    if (aa.readfile(Server.MapPath("table_spl_jk.sql"), ref mmsg))
                    {
                    msqlcom.CommandText = mmsg;
                    if (aa.exec_param(msqlcom, ref mmsg)) { mmsg2 += "執行完成Success2,"; }
                        else mmsg2+= "執行失贁22,"+mmsg;
                    }
                    else mmsg2+= "讀取失敗2,no file";
                   

                }

                }
            }

        Label1.Text = mmsg2;

            //Session("vcode") = mid(cstr(10000+fix(rnd()*10000)),2,4);
            Session["vcode"] = string.Format("{0:0000}", aa.rnd(10000)); // aa.generatevcode(4);
        if (msqlcom != null) msqlcom.Dispose();
        
    }

    protected void init_class() //初始化class含db connection, email環境設定
    {
        mmtitle = "";
        mb1 = "";
        mmpara = "";
        conn_str = System.Configuration.ConfigurationManager.ConnectionStrings["mssqlconnstr"].ConnectionString;

        string[] xxary;
        if (conn_str.IndexOf("dev") >= 0) aa.xxconn_str = conn_str;
        aa.xxhelp = "技術版權屬Jack所有,有技術問題請洽liang_jack@mail.medialand.com.tw";

        aa.encrypt_key1 = System.Configuration.ConfigurationManager.AppSettings["encryptkey"];
        if (Request.ServerVariables["QUERY_STRING"].ToString().Length > 0) { mmpara = "?" + Request.ServerVariables["QUERY_STRING"]; }


        string mstr = Request.ServerVariables["Path_Info"].ToString();

        if (Session["mmuno"] != null)
        {

            mmtitle = aa.init(mstr.ToUpper().Trim() + mmpara, "單元標題").ToString();
            mmpara = "";
            xxary = mmtitle.Split(',');

            if (xxary.Length >= 2)
            {
                mmtitle = xxary[0];
                XXunt_loc = xxary[1];
            }
            else
            {
                XXunt_loc = "0";
            }

            whr = "";
            mmfrm = Request.ServerVariables["remote_addr"].ToString();
            ZZurl = aa.ZZURL;
            YYurl = aa.YYURL;
            XXurl = aa.XXURL;
            if (Request.ServerVariables["QUERY_STRING"].ToString().Length > 0)
            {

                //modify by janice 20120303
                string[] smara = Request.ServerVariables["QUERY_STRING"].ToString().Split('&');
                if (smara.Length > 1)
                    mmpara = "?" + smara[0];
                else
                    mmpara = "?" + Request.ServerVariables["QUERY_STRING"];

                YYurl = YYurl + mmpara;
            }
            else
            { mmpara = ""; }

            XXauth = "";
            mmsg = "";
            mmsg2 = "";


        }
        aa.init(Request.ServerVariables["PATH_INFO"].ToString().ToUpper().Trim(), "後台管理");
    }
    protected bool chkath() //權限不足時 masterpage. content visable=false
    {
        string xxmenuMode = "";

        if (XXurl == YYurl)
        {
            xxmenuMode = "B";
        }
        else
        {

            if (!string.IsNullOrEmpty(msno) && Int32.Parse(msno) > 0)
            {
                xxmenuMode = "E";
            }
            else
            {
                xxmenuMode = "A";
            }
        }


        if (Session["mmuno"] == null || aa.chkath(YYurl, 0, xxmenuMode, Session["mmuno"].ToString()) != "OK")
        {
            if (Session["mmuno"] == null)
            {

                return false; //mng_main.Visible = false; //Response.End();
            }
            else
            {
                return false;// mng_main.Visible = false;//Response.End();
            }
        }
        else
        {
            if (aa.chkath(YYurl, 0, "A", Session["mmuno"].ToString()) == "OK") XXauth = XXauth + "A";
            if (aa.chkath(YYurl, 0, "B", Session["mmuno"].ToString()) == "OK") XXauth = XXauth + "B";
            if (aa.chkath(YYurl, 0, "D", Session["mmuno"].ToString()) == "OK") XXauth = XXauth + "D";
            if (aa.chkath(YYurl, 0, "E", Session["mmuno"].ToString()) == "OK") XXauth = XXauth + "E";
            if (aa.chkath(YYurl, 0, "C", Session["mmuno"].ToString()) == "OK") XXauth = XXauth + "C";
            return true;
        }

    }
    protected void close_app()
    {
        if (mmsg2.Length > 0) Response.Write(aa.alert_script(@"alert('" + mmsg2 + "')"));
        Response.Write(aa.alert_script("sGO()"));
        aa = null;

    }
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        if (ex is HttpRequestValidationException)
        {
            Response.Write(aa.alert_back("請您輸入合法字串。"));
            Server.ClearError(); // 如果不ClearError()這個異常會繼續傳到Application_Error()。
        }
    }   
}