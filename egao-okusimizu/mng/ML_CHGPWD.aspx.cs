using System;
using System.Collections.Generic;

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
    public string msot, mfsno, mmode, mweb, murl, murl2;
    public string muid, mpwd, mnam, mtel, monf, memail, munt, mtel_a, mfax_a, mgrs, mgns, mfax, madm, mgnos, mgrnos, mcrt_usr, mcrt_dat, mupd_usr, mupd_dat, mupd_usr2, mupd_dat2, mupd_usr3, mupd_dat3, magn1, magn2, magn2_onf, magn1_onf, munt_mng;
    public SqlCommand msqlcom = new SqlCommand();
    public DataView dv1, dv2;
    public int mpage, mc;

    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));
        //if (!chkath()) {  }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...


        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 5, "", ref mmsg);
        if (mb1 == "SAV")
        {
            mnam = aa.chktyp(Request.Form["nam"], 1, "C", 0, 20, "姓名", ref mmsg);
            memail = aa.chktyp(Request.Form["email"], 1, "E", 0, 200, "信箱", ref mmsg);
            munt = aa.chktyp(Request.Form["unt"], 0, "C", 0, 100, "單位", ref mmsg);

            string mpwd = aa.chktyp(Request.Form["opwd"], 0, "C", 8, 15, "舊密碼", ref mmsg);
            string npwd = aa.chktyp(Request.Form["npwd"], 0, "PWDSET", 8, 15, "新密碼", ref mmsg);
            string npwd2 = aa.chktyp(Request.Form["npwd2"], 0, "PWDSET", 8, 15, "新密碼確認", ref mmsg);

            if (mpwd != "" || npwd != "" || npwd2 != "")
            {
                if (mpwd == "" || npwd == "" || npwd2 == "") mmsg = "若要修改密碼，需填齊舊密碼，新密碼及新密碼確認三個欄位!長度8~15碼";
                if (npwd != npwd2) { mmsg = "新密碼二次輸入不符,請重新輸入"; }
                if (npwd != null && npwd != "") { if (npwd.ToUpper().IndexOf(Session["mmuid"].ToString().ToUpper()) >= 0) mmsg = "密碼不可包含帳號"; };
            }
            if (mmsg.Length == 0)
            {
                msqlcom.Parameters.Add("@muno", SqlDbType.Int, 8).Value = Session["mmuno"].ToString();
                msqlcom.CommandText = "select * from CABASE_user where uno=@muno and onf=1";
                dv1 = aa.dv_param(msqlcom, ref mmsg);
                if (dv1 != null)
                {
                    if (dv1.Count == 1)
                    {
                        string nsql = "";
                        if (mpwd != "")
                        {
                            if (aa.chkdbnull(dv1[0]["pwd"]) == aa.encrypt(mpwd)) { nsql = ",pwd=@mpwd,chg_pwd=getdate()"; }
                            else mmsg2 = "舊密碼有誤";
                        }

                        
                        msqlcom.Parameters.Add("@muno", SqlDbType.Int, 8).Value = aa.chkdbnull(dv1[0]["uno"]);
                        msqlcom.Parameters.Add("@mnam", SqlDbType.NVarChar, 50).Value = mnam;
                        msqlcom.Parameters.Add("@memail", SqlDbType.NVarChar,100).Value = memail;
                        msqlcom.Parameters.Add("@munt", SqlDbType.NVarChar, 20).Value = munt;

                        if (nsql != "")
                        {
                            msqlcom.Parameters.Add("@mpwd", SqlDbType.NVarChar, 50).Value = aa.encrypt(mpwd);
                        }
                        msqlcom.CommandText = "update CABASE_user set nam=@mnam,unt=@munt,email=@memail" + nsql + " where uno=@muno";


                        if (aa.exec_param(msqlcom, ref mmsg)) { if (aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:修改][資料ID:" + Session["mmuno"] + "]", "")) mmsg2 = "更新成功"; else mmsg2 = "LOG失敗"; }
                        else { mmsg2 = "更新失敗"; }



                    }
                    else { mmsg2 = "帳號有誤"; }
                    dv1.Dispose();
                }
                else { mmsg2 = "ERROR1"; }



            }
            else mmsg2 = mmsg;



        }
        msqlcom.Parameters.Clear();
        msqlcom.Parameters.Add("@muno", SqlDbType.Int, 8).Value = Session["mmuno"].ToString();
        msqlcom.CommandText = "select * from CABASE_user where uno=@muno and onf=1";

        dv1 = aa.dv_param(msqlcom, ref mmsg);
        if (dv1 != null)
        {
            if (dv1.Count > 0)
            {
                //Response.Write("OK");
                memail = aa.chkdbnull(dv1[0]["email"]);
                mnam = aa.chkdbnull(dv1[0]["nam"]);
                munt = aa.chkdbnull(dv1[0]["unt"]);
            }
            dv1.Dispose();
        }

        if (msqlcom != null) msqlcom.Dispose();

    }




}