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
    public string mselA = "", mselB = "";
    public string xshw = "0",xpop="", xappnam="", xurl="",xxid="0", xxunt="",xxcrt_usr="";
    public SqlCommand msqlcom=new SqlCommand(),  msqlcom_b=new SqlCommand();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        xpop = aa.chktyp(Request.QueryString["xpop"], 0, "C", 0, 20, "", ref mmsg);
        if (xpop=="") xpop = aa.chktyp(Request.Form["xpop"], 0, "C", 0, 20, "", ref mmsg);
        xappnam = aa.chktyp(Request.QueryString["xappnam"], 0, "C", 0, 50, "", ref mmsg);
        if (xappnam == "") xappnam = aa.chktyp(Request.Form["xappnam"], 0, "C", 0, 50, "", ref mmsg);
        xurl = aa.chktyp(Request.QueryString["xurl"], 0, "C", 0, 120, "", ref mmsg);
        if (xurl == "") xurl = aa.chktyp(Request.Form["xurl"], 0, "C", 0, 20, "", ref mmsg);
        xxid = aa.chktyp(Request.QueryString["xxid"], 0, "I", 0, 120, "", ref mmsg);
        if (xxid == "") xurl = aa.chktyp(Request.Form["xxid"], 0, "I", 0, 20, "", ref mmsg);

        xxcrt_usr = aa.chktyp(Request.QueryString["xxcrt_usr"], 0, "C", 0, 50, "", ref mmsg);
        if (xxcrt_usr == "") xxcrt_usr = aa.chktyp(Request.Form["xxcrt_usr"], 0, "C", 0, 50, "", ref mmsg);
        xxunt = aa.chktyp(Request.QueryString["xxunt"], 0, "C", 0, 50, "", ref mmsg);
        if (xxunt== "") xxunt = aa.chktyp(Request.Form["xxunt"], 0, "C", 0, 50, "", ref mmsg);




        if (xpop != "") this.MasterPageFile = "_ML_POP.master";

       // Response.Write(xpop);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));   
  
        
        if (!chkath()) { mng_main.Visible = false; }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...
        //Response.Write(YYurl + ";" );
        
        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 4, "", ref mmsg);

        if (mb1 == "")
        {
            mc = Int32.Parse(aa.chktyp(Request.QueryString["mc"], 1, "I", 0, 0, "", ref  mmsg));
            mpage = Int32.Parse(aa.chktyp(Request.QueryString["page"], 1, "I", 0, 0, "", ref  mmsg));
            msno = "0";
            xord = aa.chktyp(Request.QueryString["xord"], 0, "C", 0, 20, "", ref  mmsg);
            xsot = aa.chktyp(Request.QueryString["xsot"], 0, "C", 0, 20, "", ref  mmsg);
            xshw = aa.chktyp(Request.QueryString["xshw"], 0, "I", 0, 20, "", ref mmsg);
            xpop = aa.chktyp(Request.QueryString["xpop"], 0, "C", 0, 20, "", ref mmsg);

            mt1 = aa.chktyp(Request.QueryString["t1"], 0, "C", 0, 50, "", ref  mmsg);
            msdt = aa.chktyp(Request.QueryString["sdt"], 0, "D", 0, 50, "", ref mmsg);
            medt = aa.chktyp(Request.QueryString["edt"], 0, "D", 0, 50, "", ref mmsg);
        }
        else
        {
            mpage = Int32.Parse(aa.chktyp(Request.Form["page"], 1, "I", 0, 0, "", ref  mmsg));
            mc = Int32.Parse(aa.chktyp(Request.Form["mc"], 1, "I", 0, 0, "", ref  mmsg));
            msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref  mmsg);
            xord = aa.chktyp(Request.Form["xord"], 0, "C", 0, 20, "", ref  mmsg);
            xsot = aa.chktyp(Request.Form["xsot"], 0, "C", 0, 20, "", ref  mmsg);
            xshw = aa.chktyp(Request.Form["xshw"], 0, "I", 0, 20, "", ref mmsg);
            xpop = aa.chktyp(Request.Form["xpop"], 0, "C", 0, 20, "", ref mmsg);
            mt1 = aa.chktyp(Request.Form["t1"], 0, "C", 0, 50, "", ref  mmsg);
            msdt = aa.chktyp(Request.Form["sdt"], 0, "D", 0, 50, "", ref mmsg);
            medt = aa.chktyp(Request.Form["edt"], 0, "D", 0, 50, "", ref mmsg);
        }
        if (aa.IsDate(msdt)) whr += " and datediff(d,a.crt_dat,'" + msdt + "')<=0";
        if (aa.IsDate(medt)) whr += " and datediff(d,a.crt_dat,'" + medt + "')>=0";
        if (aa.IsDate(medt)) whr += "";
        if (xxunt != "") whr += " and a.unt=N'" + xxunt + "'";
        if (xxcrt_usr != "") whr += " and a.crt_usr=N'" + xxcrt_usr + "'";

        if (xappnam != "") whr += " and a.appnam='" + xappnam + "'";
        if (xxid!="0") whr+=" and a.mem like '%ID:" + xxid + "]%'";

        if (mt1 != "") whr = whr + " and (mem like '%" + mt1 + "%' or appnam like  '%" + mt1 + "%'  or a.unt like  '%" + mt1 + "%' or crt_ip  like  '%" + mt1 + "%' or a.crt_usr  like  '%" + mt1 + "%')";

        msqlcom.Parameters.Add("@mid", SqlDbType.NVarChar, 50).Value = "%ID:" + xxid + "]%";
        msqlcom.Parameters.Add("@mt1", SqlDbType.NVarChar, 50).Value = "%" + mt1 + "%";
        msqlcom.Parameters.Add("@appnam", SqlDbType.NVarChar, 50).Value = xappnam;
        msqlcom.Parameters.Add("@xxcrt_usr", SqlDbType.NVarChar, 50).Value = xxcrt_usr;
        msqlcom.Parameters.Add("@xxunt", SqlDbType.NVarChar, 50).Value = xxunt;
        msqlcom_b.Parameters.Add("@mid", SqlDbType.NVarChar, 50).Value = "%ID:" + xxid + "]%";
        msqlcom_b.Parameters.Add("@mt1", SqlDbType.NVarChar, 50).Value = "%" + mt1 + "%";
        msqlcom_b.Parameters.Add("@appnam", SqlDbType.NVarChar, 50).Value = xappnam;
        msqlcom_b.Parameters.Add("@xxcrt_usr", SqlDbType.NVarChar, 50).Value = xxcrt_usr;
        msqlcom_b.Parameters.Add("@xxunt", SqlDbType.NVarChar, 50).Value = xxunt;


        if (mpage == 0) { mpage = 1; xsot = "U"; }
        if (mc == 0) mc = 50;


        aa.page_ord(ref xord, xsot, "來源IP位置,@程式,@單元,@後台帳號,@事件內容,@日期", " order by a.crt_ip,@ order by a.appnam,@ order by a.unt,@order by uid,@order by convert(char,a.mem),@order by a.crt_dat", ref ord);

        
        if (mb1 == "CLR") {
            msqlcom = new SqlCommand("truncate table CABASE_sys_log");
            if (aa.exec_param(msqlcom, ref mmsg))
            {
                aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:清空]", "");
            }

            mmsg2 = "OK"; }

        if (mb1 == "D")
        {
            if (msno != "0")
            {
                msqlcom = new SqlCommand("delete from CABASE_SYS_LOG where sno=@sno");
                msqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
                if (!aa.exec_param(msqlcom, ref mmsg))
                {
                    Response.Write(aa.alert_back(mmsg));
                    Response.End();
                } else aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]","[" +  Session["mmnam"] + "]", "[動作:刪除][資料ID:" + msno + "]","");

            }
        }

        msqlcom.CommandText = "select unt from CABASE_sys_log group by unt";
        dv1 = aa.dv_param(msqlcom, ref mmsg);
        if (dv1 != null)
        {
            for (int i = 0; i < dv1.Count; i++)
            {
                if (xxunt == aa.chkdbnull(dv1[i]["unt"])) mselA+="<option selected>" + aa.chkdbnull(dv1[i]["unt"]) + "</option>";
                else mselA += "< option>" + aa.chkdbnull(dv1[i]["unt"]) + "</option>";
            }
            dv1.Dispose();
        }
        msqlcom.CommandText = "select crt_usr from CABASE_sys_log group by crt_usr";
        dv1 = aa.dv_param(msqlcom, ref mmsg);
        if (dv1 != null)
        {
            for (int i = 0; i < dv1.Count; i++)
            {
                if (xxcrt_usr == aa.chkdbnull(dv1[i]["crt_usr"])) mselB+="<option selected>" + aa.chkdbnull(dv1[i]["crt_usr"]) + "</option>";
                else mselB+="<option>" + aa.chkdbnull(dv1[i]["crt_usr"]) + "</option>";
            }
            dv1.Dispose();
        }


        msqlcom.CommandText = "select count(*) as cnt from CABASE_sys_log a left outer join CABASE_user b on a.crt_Uno=b.uno where 1=1  " + whr;
        msqlcom_b.CommandText = "select b.uid as uid,a.* from CABASE_sys_log a left outer join CABASE_user b on a.crt_Uno=b.uno where 1=1 " + whr + ord;
        Response.Write(whr);

    }

}