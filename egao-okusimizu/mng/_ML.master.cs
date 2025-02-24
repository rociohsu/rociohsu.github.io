using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class _default : System.Web.UI.MasterPage
{

    public int mpage, mc;

    //此版型為後台介面版型, 上方選單+左選單+內容DIV三區

    //因為上選單及左選單在這裡呈現, 所以要呼叫資料庫
    //又因master page 是獨立頁, 沒有跟其他jkz_xxxx一樣有繼承 MLbasepage.cs裡的東西, 
    //所以本頁要放跟 app_code/MLbasepage.cs一樣的基本初始化function init_class(),chkath(),page_error(),close_app(),....四個function

    // string mfsno, mmode, mweb, msot, mweb_e, murl, murl2, munt_loc;

    public MLclass aa = new MLclass();
    public string mmpara; //2012/2/22新增,CMS後台共用.aspx單元用
    public string mmtitle, mb1, conn_str, mito, xord, ord, msno, mtyp, xsot, mt1, msdt, medt, mmsg, mmsg2, whr, msel, XXurl, YYurl, ZZurl, XXauth, XXunt_loc, mSql, mmfrm, mmmpath, mmmng, mmroot;
    public SqlCommand msqlcom = new SqlCommand(), msqlcom_b = new SqlCommand();
    System.Data.DataView rs1;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));
        init_class();

        mmsg = "";
        msno = aa.chktyp(Request.QueryString["sno"], 0, "I", 0, 0, "", ref mmsg);
        mmmpath = Request.ServerVariables["Path_Info"].ToString();
        mmmng = mmmpath;
        if (mmmpath.ToLower().IndexOf("/" + aa.xxmng.ToLower()) >= 0) { mmmng = mmmpath.Substring(0, mmmpath.ToLower().IndexOf("/" + aa.xxmng.ToLower())) + "/" + aa.xxmng + "/"; mmroot = mmmpath.Substring(0, mmmpath.ToLower().IndexOf("/" + aa.xxmng.ToLower())); }
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

            mmtitle = aa.init(mstr.ToUpper().Trim() , "單元標題").ToString();
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
            if (mmtitle == "") mmtitle = "首頁";
            aa.init(Request.ServerVariables["PATH_INFO"].ToString().ToUpper().Trim(), "後台管理");
            if (Session["後台管理標題"] == null) Session["後台管理標題"] = aa.init(Request.ServerVariables["PATH_INFO"].ToUpper().Trim(), "後台管理標題");
        }
    }

   




    protected void show_menuleft()
    {
        bool isopen = false;
        string mpath = Request.ServerVariables["Path_Info"].ToString().ToLower();
        if (mpath.IndexOf("/mng")>0) mpath = mpath.Substring(0, mpath.IndexOf("/mng")) + "/mng/";
        mpath = "";
        if (Session["mmuno"] == null || String.IsNullOrEmpty(YYurl))
        {

            Response.Write(aa.alert_to("您沒有此單元權限", "CABASE_index.aspx"));
            Response.End();
        }

        System.Text.StringBuilder mstr = new System.Text.StringBuilder();
        mstr.AppendLine(" <ul class=\"sidebar-menu\" data-widget=\"tree\">");// <li class=\"header\">主選單</li>");
        

        //呈現logo
        // mstr.Append(@"<span id=""mytopmenu"" style=""height:20px""><ul><li style="" width:60px"" class=""g9nw""><a href=""" + mpath + @"CABASE_indexmain.aspx""><img src=""theme/images/bg_top_home.gif"" border=""0"" title=""LOGO"" ></a></li>"); //add

        if (true) //if (aa.xxmenubar.IndexOf("左") >= 0)
        {
            int topi, topj;
            System.Data.DataView dvtop, dvtop2;
            string xxtopurl;

            msqlcom.CommandText = "select * from CABASE_ito where fsno=0 order by sot";
            msqlcom_b.CommandText = "select * from CABASE_ito where fsno>0 order by fsno,sot";

            dvtop = aa.dv_param(msqlcom, ref mmsg);
            dvtop2 = aa.dv_param(msqlcom_b, ref mmsg);

            if (dvtop != null && Session["mmuno"] != null)
            {
                for (topi = 0; topi <= dvtop.Count - 1; topi++)
                {
                    isopen = false;
                    //if (aa.chkath(dvtop[topi]["url"].ToString(), 0, "B", Int32.Parse(Session["mmuno"].ToString())) == "OK")
                    if (aa.chkath("", int.Parse(dvtop[topi]["sno"].ToString()), "B", Session["mmuno"].ToString()) == "OK")
                    {
                        if (aa.chkdbnull(dvtop[topi]["url"]) != "") //有連結時
                        {
                            string murl2 = "fa fa-file"; //預設圖
                            if (aa.chkdbnull(dvtop[topi]["url2"]) != "") murl2 = aa.chkdbnull(dvtop[topi]["url2"]); //有指定圖時
                            string mtg = "";
                            if (aa.chkdbnull(dvtop[topi]["url"]).ToLower().IndexOf("//") >= 0) mtg = "target=\"_blank\"";
                            if (aa.chkdbnull(dvtop[topi]["url"]).ToLower() == aa.YYURL.ToLower())                                
                            { mstr.Append("<li  class=\"active\" ><a href=\"" + mpath + aa.chkdbnull(dvtop[topi]["url"]).ToString() + "\" "+mtg+"><i class=\"" + murl2 + "\"></i> <span >" + aa.chkdbnull(dvtop[topi]["nam"]).ToString() + "</span></a></li>"); }
                            else
                            { mstr.Append("<li ><a href=\"" + mpath + aa.chkdbnull(dvtop[topi]["url"]).ToString() + "\"  " + mtg + "><i class=\"" + murl2 + "\"></i> <span >" + aa.chkdbnull(dvtop[topi]["nam"]).ToString() + "</span></a></li>"); }

                        }
                        else
                        {
                            xxtopurl = "#";
                            string xmenu = "";
                            if (dvtop2 != null)
                            {
                                dvtop2.RowFilter = " fsno=" + dvtop[topi]["sno"].ToString();

                                for (topj = 0; topj <= dvtop2.Count - 1; topj++)
                                {
                                    string murl3 = "fa fa-file"; //預設圖
                                    if (aa.chkdbnull(dvtop2[topj]["url2"]) != "") murl3 = aa.chkdbnull(dvtop2[topj]["url2"]);

                                    if (aa.chkdbnull(dvtop2[topj]["url"]) != "")
                                    {
                                        if (aa.chkath("", int.Parse(dvtop2[topj]["sno"].ToString()), "B", Session["mmuno"].ToString()) == "OK")
                                        {

                                            xxtopurl = mpath + aa.chkdbnull(dvtop2[topj]["url"]).ToString();


                                            //break;

                                            string micon = "theme/dist/img/tree_3w.gif";
                                            if (dvtop2.Count == topj + 1)
                                            {
                                                micon = "theme/dist/img/tree_endw.gif";
                                            }

                                            if (aa.chkdbnull(dvtop2[topj]["url"]).ToLower() == aa.YYURL.ToLower())
                                            {
                                                isopen = true;
                                                xmenu += @"<li class=""active""><a href=""" + xxtopurl + "\"><img src=" + micon + "><i class=\""+murl3+"\"></i> " + aa.chkdbnull(dvtop2[topj]["nam"]).ToString() + @"</a></li>";
                                            }
                                            else
                                            {
                                                xmenu += @"<li><a href=""" + xxtopurl + "\"><img src=" + micon + "><i class=\"" + murl3 + "\"></i> " + aa.chkdbnull(dvtop2[topj]["nam"]).ToString() + "</a></li>";
                                            }
                                        }
                                    }

                                }
                                //dvtop2.Dispose();
                            }
                            if (xmenu != "")
                            {
                                string murl2 = "fa fa-folder"; //url2=預設icon
                                if (aa.chkdbnull(dvtop[topi]["url2"]) != "") murl2 = aa.chkdbnull(dvtop[topi]["url2"]);

                                if (isopen || aa.xxmenubar != "左合") mstr.AppendLine(@" <li class=""treeview menu-open"" ><a  href=""#demo" + aa.chkdbnull(dvtop[topi]["sno"]).ToString() + @""" ><i class=""" + murl2 + "\"></i> <span><b>" + aa.chkdbnull(dvtop[topi]["nam"]).ToString() + "</b></span><span class=\"pull-right-container\"><i class=\"fa fa-angle-left pull-right\"></i></span></a>");
                                else mstr.AppendLine(@"<li class=""treeview"" ><a  href=""#demo" + aa.chkdbnull(dvtop[topi]["sno"]).ToString() + @""" ><i class=""" + murl2 + "\"></i> <span><b>" + aa.chkdbnull(dvtop[topi]["nam"]).ToString() + "</b></span><span class=\"pull-right-container\"><i class=\"fa fa-angle-left pull-right\"></i></span></a>");

                                if (isopen || aa.xxmenubar != "左合") mstr.AppendLine(@"<ul id=""demo" + aa.chkdbnull(dvtop[topi]["sno"]).ToString() + @""" class=""treeview-menu"" style=""display: block;"">");
                                else mstr.AppendLine(@"<ul id=""demo" + aa.chkdbnull(dvtop[topi]["sno"]).ToString() + @""" class=""treeview-menu"" >");
                                
                                // else mstr.Append(@"<li><a href=""javascript:;"">" + aa.chkdbnull(dvtop[topi]["nam"]).ToString() + @"</a></li>");
                                mstr.Append(xmenu);
                                mstr.Append(@"</ul></li>");
                            }


                            //mstr.Append(@"<span class=""g9nw"" nowrap=""nowrap"">　｜<a href=""" + xxtopurl + @""">" + aa.chkdbnull(dvtop[topi]["nam"]).ToString() + "</a></span>");


                        }
                    }

                    // break;  
                }
                //add
                dvtop.Dispose();
                if (dvtop2 != null) dvtop2.Dispose();
            }

        }


        //顯示登入者姓名,及Logout鈕
        //mstr.AppendLine(@"");
        //f (aa.XXURL == "ML_CHGPWD.ASPX") mstr.AppendLine(@"<li class=""on""><a href=""" + mpath + @"ML_CHGPWD.ASPX""><span class=""glyphicon glyphicon-cog""></span> Settings <span class=""glyphicon glyphicon-triangle-left"" style=""float:right;top: 0.3em; font-size: 18px;""><span></a></li>");
        //else mstr.AppendLine(@"<li class=""nav-item""><a href=""" + mpath + @"ML_CHGPWD.ASPX""><span class=""glyphicon glyphicon-cog""></span> Settings </a></li>");
        mstr.AppendLine(@"<li><a href=""" + mpath + @"ML_LOGOUT.ASHX""><span class=""glyphicon glyphicon-log-out""></span> Logout &nbsp; </a></li>");
        mstr.AppendLine(@"<li><a href=""" + mpath + @"ML_INDEXMAIN.aspx""><span class=""fa fa-home""></span> 首頁 </a></li>");
        mstr.AppendLine(@"</ul>");
        

        //mstr.Clear();


        //mstr.AppendLine(@"</div>");

        Response.Write(mstr);
        mstr = null;
        if (msqlcom != null) msqlcom.Dispose();
        if (msqlcom_b != null) msqlcom_b.Dispose();
    }

}
