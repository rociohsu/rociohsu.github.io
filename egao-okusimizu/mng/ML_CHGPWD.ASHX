<%@ WebHandler Language="C#" Class="sql2csv" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class sql2csv : MLBaseAshx, IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        init_class(context);

        JObject jo = new JObject();

        if (context.Session["mmuno"] == null) { jo["RS"] = "登入逾時";context.Response.Write(jo.ToString()); return; }

        DataView dv1;
        SqlCommand msqlcom = new SqlCommand();
        SqlCommand msqlcom2 = new SqlCommand();

        string mnam = aa.chktyp(context.Request.Form["nam"], 0, "C", 0, 20, "姓名", ref mmsg);
        string memail = aa.chktyp(context.Request.Form["email"], 1, "E", 0, 200, "信箱", ref mmsg);
        string mb1 = aa.chktyp(context.Request.Form["b1"], 0, "C", 0, 10, "", ref mmsg);

        string mpwd = aa.chktyp(context.Request.Form["pwd0"], 0, "C", 8, 15, "舊密碼", ref mmsg);
        string npwd = aa.chktyp(context.Request.Form["pwd1"], 0, "PWDSET", 8, 15, "新密碼", ref mmsg);
        string npwd2 = aa.chktyp(context.Request.Form["pwd2"], 0, "PWDSET", 8, 15, "新密碼確認", ref mmsg);

        if (mb1 == "chg_pwd")
        {
            if (mpwd != "" || npwd != "" || npwd2 != "")
            {
                if (mpwd == "" || npwd == "" || npwd2 == "") mmsg = "若要修改密碼，需填齊舊密碼，並且要英數+特殊字元新密碼及新密碼確認三個欄位!長度8~15碼";
                if (npwd != npwd2) { mmsg = "新密碼二次輸入不符,請重新輸入"; }
                if (npwd!=null && npwd != "") { if (npwd.ToUpper().IndexOf(context.Session["mmuid"].ToString().ToUpper()) >= 0) mmsg = "密碼"+npwd+"不可包含帳號"+context.Session["mmuid"].ToString(); };
            }
            if (mmsg.Length == 0)
            {
                msqlcom.Parameters.Add("@muno", SqlDbType.Int, 250).Value = context.Session["mmuno"];
                msqlcom.CommandText = "select * from CABASE_user where uno=@muno and onf=1";
                dv1 = aa.dv_param(msqlcom, ref mmsg);
                if (dv1 != null)
                {
                    if (dv1.Count == 1)
                    {
                        msqlcom2.Parameters.Clear();

                        string nsql = "";
                        if (mpwd != "")
                        {


                            if (aa.chkdbnull(dv1[0]["pwd"]) == aa.encrypt(mpwd))
                            {
                                msqlcom2.Parameters.Add("@mpwd", SqlDbType.NVarChar,50).Value = aa.encrypt(npwd);
                                nsql = ",pwd=@mpwd,chg_pwd=getdate()";
                            }

                            else mmsg = "舊密碼有誤";
                        }

                        if (mmsg=="")
                        {
                            msqlcom2.Parameters.Add("@muno", SqlDbType.Int, 250).Value = dv1[0]["uno"];
                            msqlcom2.Parameters.Add("@memail", SqlDbType.NVarChar, 250).Value = memail;
                            msqlcom2.CommandText = "update CABASE_user set email=@memail" + nsql + " where uno=@muno";
                            if (aa.exec_param(msqlcom2, ref mmsg))
                            {
                                if (aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + context.Session["mmnam"] + "]", "[動作:修改][資料ID:" + context.Session["mmuno"] + "]", "")) mmsg = "OK"; else mmsg = "LOG失敗";
                            }
                            else { mmsg = "更新失敗"; }

                        }




                    }
                    else { mmsg = "帳號有誤"; }
                    dv1.Dispose();
                }
                else { mmsg = "ERROR1"; }


            }
        }
        else mmsg = "OK";


        jo["RS"] = mmsg;
        msqlcom.Parameters.Clear();
        msqlcom.Parameters.Add("@muno", SqlDbType.Int, 250).Value = context.Session["mmuno"];
        msqlcom.CommandText = "select * from CABASE_user where uno=@muno and onf=1";
        dv1 = aa.dv_param(msqlcom, ref mmsg);
        if (dv1 != null)
        {
            if (dv1.Count > 0) jo["EMAIL"] = aa.chkdbnull(dv1[0]["email"]);
            dv1.Dispose();
        }

        if (msqlcom != null) msqlcom.Dispose();
        if (msqlcom2 != null) msqlcom2.Dispose();

        context.Response.Write(jo.ToString());


    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}