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
    public SqlCommand msqlcom = new SqlCommand(), msqlcom2 = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));   
  
        
        if (!chkath()) { mng_main.Visible = false; }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...
        //Response.Write(YYurl + ";" );
        
        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 3, "", ref mmsg);

        if (mb1 == "")
        {
            mc = Int32.Parse(aa.chktyp(Request.QueryString["mc"], 1, "I", 0, 0, "", ref  mmsg));
            mpage = Int32.Parse(aa.chktyp(Request.QueryString["page"], 1, "I", 0, 0, "", ref  mmsg));
            msno = "0";
            xord = aa.chktyp(Request.QueryString["xord"], 0, "C", 0, 20, "", ref  mmsg);
            xsot = aa.chktyp(Request.QueryString["xsot"], 0, "C", 0, 20, "", ref  mmsg);
            mt1 = aa.chktyp(Request.QueryString["t1"], 0, "C", 0, 50, "", ref  mmsg);
        }
        else
        {
            mpage = Int32.Parse(aa.chktyp(Request.Form["page"], 1, "I", 0, 0, "", ref  mmsg));
            mc = Int32.Parse(aa.chktyp(Request.Form["mc"], 1, "I", 0, 0, "", ref  mmsg));
            msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref  mmsg);
            xord = aa.chktyp(Request.Form["xord"], 0, "C", 0, 20, "", ref  mmsg);
            xsot = aa.chktyp(Request.Form["xsot"], 0, "C", 0, 20, "", ref  mmsg);
            mt1 = aa.chktyp(Request.Form["t1"], 0, "C", 0, 50, "", ref  mmsg);
        }

        if (mt1 != "") whr = whr + " and (b.nam=@mt1 or a.mem like @mt1 )";

        if (mpage == 0) { mpage = 1; xsot = "U"; xord = "建立日期"; }
        if (mc == 0) mc = 50;

        msqlcom.Parameters.Add("@msno", SqlDbType.Int, 10).Value = msno;

        aa.page_ord(ref xord, xsot, "問題分類,@內容,@狀態,@建立日期,@異動日", " order by b.nam,@ order by tpcs,@ order by a.sts,@order by a.crt_dat,@order by a.upd_dat", ref ord);

        


        if (mb1 == "D")
        {
            if (msno != "0")
            {
                
                msqlcom.CommandText = "select *  from CA20_FORM where sno=@msno";
                dv1 = aa.dv_param(msqlcom, ref mmsg);
                if (dv1 != null)
                {
                    if (dv1.Count > 0)
                    {
                        msqlcom.CommandText = "delete from CA20_FORM where sno=@msno";
                        if (!aa.exec_param(msqlcom, ref mmsg))
                        {
                            Response.Write(aa.alert_back(mmsg));
                            Response.End();
                        }
                        else aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:刪除][資料:ID:" + msno + "]", "");
                    }
                    dv1.Dispose();
                }

                
            }
        }
        msqlcom = new SqlCommand("select count(*) as cnt from CA20_FORM a where 1=1 " + whr);
        msqlcom2 = new SqlCommand("select case when a.sts=0 then N'待處理' when a.sts=1 then N'處理中' when a.sts=2 then N'已回覆' end as stsnam,b.nam as typnam,convert(nvarchar(50),a.mem) as tpcs,a.* from CA20_FORM a left outer join CA20_FORM_TYP b on a.typ=b.sno where 1=1 " + whr + ord);
        msqlcom.Parameters.Add("@mt1", SqlDbType.NVarChar, 50).Value = "%" + mt1 + "%";
        msqlcom2.Parameters.Add("@mt1", SqlDbType.NVarChar, 50).Value = "%" + mt1 + "%";
    }

}
    
