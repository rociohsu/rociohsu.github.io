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
    public string mselA="";
    
    public SqlCommand msqlcom = new SqlCommand(), msqlcom2 = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));   
  
        
        if (!chkath()) { mng_main.Visible = false; }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...
        //Response.Write(YYurl + ";" );
        
        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 5, "", ref mmsg);

        if (mb1 == "")
        {
            mc = Int32.Parse(aa.chktyp(Request.QueryString["mc"], 1, "I", 0, 0, "", ref  mmsg));
            mpage = Int32.Parse(aa.chktyp(Request.QueryString["page"], 1, "I", 0, 0, "", ref  mmsg));
            msno = "0";
            xord = aa.chktyp(Request.QueryString["xord"], 0, "C", 0, 20, "", ref  mmsg);
            xsot = aa.chktyp(Request.QueryString["xsot"], 0, "C", 0, 20, "", ref  mmsg);
            mt1 = aa.chktyp(Request.QueryString["t1"], 0, "C", 0, 50, "", ref  mmsg);
            mtyp = aa.chktyp(Request.QueryString["typ"], 0, "C", 0, 20, "", ref mmsg);
        }
        else
        {
            mpage = Int32.Parse(aa.chktyp(Request.Form["page"], 1, "I", 0, 0, "", ref  mmsg));
            mc = Int32.Parse(aa.chktyp(Request.Form["mc"], 1, "I", 0, 0, "", ref  mmsg));
            msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref  mmsg);
            xord = aa.chktyp(Request.Form["xord"], 0, "C", 0, 20, "", ref  mmsg);
            xsot = aa.chktyp(Request.Form["xsot"], 0, "C", 0, 20, "", ref  mmsg);
            mt1 = aa.chktyp(Request.Form["t1"], 0, "C", 0, 50, "", ref  mmsg);
            mtyp = aa.chktyp(Request.Form["typ"], 0, "C", 0, 20, "", ref mmsg);
        }
        
        if (mt1 != "") whr = whr + " and (a.tpc like @mt1 )";
        if (mtyp !="") whr += " and a.typ=@typ";
        
        if (mpage == 0) { mpage = 1; xsot = "U"; }
        if (mc == 0) mc = 50;

       

        aa.page_ord(ref xord, xsot, "圖片,@分類,@新聞標題,@啟用,@起始日期,@結束日期,@異動日期", "order by a.img,@ order by a.typ,@ order by a.tpc,@ order by a.onf,@ order by sdt,@ order by edt,@ order by a.upd_dat", ref ord);

        if (mb1 == "DEL")
        {

            string mdels = aa.chktyp(Request.Form["c_del"], 0, "C", 0, 2000, "", ref mmsg);
            string[] mara = mdels.Split(',');
            if (mdels != "")
            {

                var parameters = new string[mara.Length];
                for (int mi = 0; mi < mara.Length; mi++)
                {
                    parameters[mi] = string.Format("@Age{0}", mi);
                    msqlcom.Parameters.Add(parameters[mi], SqlDbType.Int, 8).Value = mara[mi];
                }
                msqlcom.CommandText = string.Format("delete from CA20_NEWS  where sno in ({0})" , string.Join(", ", parameters));
                aa.exec_param(msqlcom, ref mmsg);
                aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:刪除][資料ID:" + mdels + "]","");
            }
            
        }


        if (mb1 == "D")
        {
            if (msno != "0")
            {
                
                msqlcom.CommandText = "select *  from CA20_NEWS a where sno=@msno"+whr;
                msqlcom.Parameters.Add("@msno", SqlDbType.Int, 8).Value = msno;
                dv1 = aa.dv_param(msqlcom, ref mmsg);
                if (dv1 != null)
                {
                    if (dv1.Count > 0)
                    {
                        msqlcom.Parameters.Clear();
                        msqlcom.CommandText = "delete from CA20_NEWS where sno=@msno";
                        msqlcom.Parameters.Add("@msno", SqlDbType.Int, 8).Value = msno;
                        if (!aa.exec_param(msqlcom, ref mmsg))
                        {
                            Response.Write(aa.alert_back(mmsg));
                            Response.End();
                        }
                        else aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:刪除][資料:"+aa.chkdbnull(dv1[0]["tpc"])+",ID:" + msno + "]", "");
                    }
                    dv1.Dispose();
                }

                
            }
        }

        msqlcom.Parameters.Clear();
        msqlcom.CommandText = "select * from ca20_news_typ ";
        dv1 = aa.dv_param(msqlcom, ref mmsg);
        if (dv1 != null)
        {   for (int i = 0; i < dv1.Count; i++)
            {
                if (mtyp == aa.chkdbnull(dv1[i]["sno"])) mselA += "<option value=\"" + aa.chkdbnull(dv1[i]["sno"]) + "\" selected>" + aa.chkdbnull(dv1[i]["nam"])+"</option>";
                else mselA += "<option value=\"" + aa.chkdbnull(dv1[i]["sno"]) + "\">" + aa.chkdbnull(dv1[i]["nam"]) + "</option>";
            }
            dv1.Dispose();
        }


        msqlcom.Parameters.Clear();
        msqlcom2.Parameters.Clear();
        msqlcom.CommandText="select count(*) as cnt from CA20_NEWS a where 1=1" + whr;
        msqlcom2.CommandText="select case when a.onf=1 then '<font color=#0000ff>Y</font>' else 'N' end as onfnam,b.nam as typnam,a.* from CA20_NEWS a left outer join ca20_news_typ b on a.typ=b.sno  where 1=1 " + whr + ord;
        msqlcom.Parameters.Add("@mt1", SqlDbType.NVarChar, 50).Value = "%" + mt1 + "%";
        msqlcom2.Parameters.Add("@mt1", SqlDbType.NVarChar, 50).Value = "%" + mt1 + "%";
        msqlcom.Parameters.Add("@typ", SqlDbType.NVarChar, 50).Value = mtyp;
        msqlcom2.Parameters.Add("@typ", SqlDbType.NVarChar, 50).Value = mtyp;
  }

}
    
