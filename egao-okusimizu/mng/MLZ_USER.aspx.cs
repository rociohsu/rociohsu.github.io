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
    public SqlCommand sqlcom, sqlcom_b;

    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));   
        if (!chkath()) { mng_main.Visible = false; }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...

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
        if (Session["mmadm"].ToString() != "2") whr += " and a.adm<" + Session["mmadm"].ToString();
        if (mt1 != "") { whr = whr + " and (a.nam like @mt1 or a.unt like @mt1 or a.email like @mt1 or a.uid like @mt1)"; }

        if (mpage == 0) { mpage = 1; xsot = "U"; }
        if (mc == 0) mc = 50;

        aa.page_ord(ref xord, xsot, "帳號,@E-mail,@姓名,@最後登入日期,@狀態,@異動日", "order by a.uid,@ order by a.email,@ order by a.nam,@ order by a.login_cnt,@ order by a.onf,@ order by a.upd_dat", ref ord);




        if (mb1 == "D")
        {
            if (msno != "0")
            {
                using (sqlcom = new SqlCommand("delete from CABASE_user where uno=@sno"))
                {
                    sqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
                    if (!aa.exec_param(sqlcom, ref mmsg))
                    {
                        Response.Write(aa.alert_back(mmsg));
                        Response.End();
                    }
                    else
                    {
                        aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:刪除][資料ID:" + msno + "]", "");
                    }
                }

                using (sqlcom = new SqlCommand("delete from CABASE_user_group where uno=@sno"))
                {
                    sqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
                    if (!aa.exec_param(sqlcom, ref mmsg))
                    {
                        Response.Write(aa.alert_back(mmsg));
                        Response.End();
                    }
                }

                using (sqlcom = new SqlCommand("delete from CABASE_user_ito where uno=@sno"))
                {
                    sqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
                    if (!aa.exec_param(sqlcom, ref mmsg))
                    {
                        Response.Write(aa.alert_back(mmsg));
                        Response.End();
                    }
                }


            }
        }
        
        sqlcom = new SqlCommand("select count(*) as cnt from CABASE_user a where 1=1 " + whr);
        sqlcom_b = new SqlCommand("select case when a.onf = 1 then N'啟用' else N'<font color=#ff0000>停用</font>' end as onfnam , a.* from CABASE_user a  where 1 = 1 " + whr + ord);
        sqlcom.Parameters.Add("@mt1", SqlDbType.NVarChar, 50).Value = "%" + mt1 + "%";
        sqlcom_b.Parameters.Add("@mt1", SqlDbType.NVarChar, 50).Value = "%" + mt1 + "%";
    }


    
}