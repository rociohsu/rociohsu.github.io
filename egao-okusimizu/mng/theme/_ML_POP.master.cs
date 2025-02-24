using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _default :  System.Web.UI.MasterPage
{
    public string mmpara; //2012/2/22新增,CMS後台共用.aspx單元用
    public string mmtitle, mb1, conn_str, mito, xord, ord, msno, mtyp, xsot, mt1, msdt, medt, mmsg, mmsg2, whr, msel, XXurl, YYurl, ZZurl, XXauth, XXunt_loc, mSql, mmfrm, mmmpath, mmmng, mmroot,mmcoot;

  
    //此版型為空白無定位DIV
    public MLclass aa = new MLclass();
    protected void Page_Load(object sender, EventArgs e)
    {
        mmmpath = "";
        mmmng = "";
        mmroot = "";
        init_class();
        
        mmsg = "";
        mmmpath = Request.ServerVariables["Path_Info"].ToString();
        mmmng = mmmpath;
        if (mmmpath.ToLower().IndexOf("/" + aa.xxmng.ToLower()) >= 0) { mmmng = mmmpath.Substring(0, mmmpath.ToLower().IndexOf("/" + aa.xxmng.ToLower())) + "/" + aa.xxmng + "/"; mmroot = mmmpath.Substring(0, mmmpath.ToLower().IndexOf("/" + aa.xxmng.ToLower())); mmcoot = mmroot; }
        
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
        if (Session["後台管理標題"] == null) Session["後台管理標題"] = aa.init(Request.ServerVariables["PATH_INFO"].ToUpper().Trim(), "後台管理標題");
    }
   
    
    
}
