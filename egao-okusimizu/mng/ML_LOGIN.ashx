<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
public class Handler : MLBaseAshx, IHttpHandler
{

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        init_class(context);

        DataView dv1;
        string mb1 = "",mauto_login="",maaa="",mbbb="",mccc="",mtoken="",xpara="",mcccnam="",matg="",mggg="";
        string[] xxips;
        int xxips_sts;
        System.Data.SqlClient.SqlCommand sqlcom = new System.Data.SqlClient.SqlCommand();

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
            context.Response.Write(xxips.Length.ToString()+"IP不允許");
            context.Response.End();
        }
        string xurl = aa.chktyp(context.Request["xurl"], 0, "C", 0, 250, "", ref mmsg);

        mb1 = aa.chktyp(context.Request.Form["b1"], 0, "C", 0, 3, "", ref mmsg);



        mtoken = aa.decrypt(aa.chktyp(context.Request.QueryString["token"], 0, "C", 0, 500, "", ref mmsg));
        string[] mtokens = mtoken.Split(',');


        if (mtokens.Length == 3 )
        {

            matg = "on";
            maaa = mtokens[1];
            mbbb = mtokens[2];
            mccc = mtokens[0];

            sqlcom.Parameters.Clear();
            sqlcom.CommandText = "select * from pos_store where stk=@stk";
            sqlcom.Parameters.Add("@stk", SqlDbType.NVarChar, 10).Value = mccc;
            dv1 = aa.dv_param(sqlcom, ref mmsg);
            if (dv1 != null)
            {
                if (dv1.Count == 0) mmsg = "門市有誤,請洽IT人員~" ;
                else { mcccnam = aa.chkdbnull(dv1[0]["stk_nam"]); }
                dv1.Dispose();
            }

            if (mmsg.Length > 0)
            {
                context.Response.Write(mmsg);
            }
            else
            {


                sqlcom.Parameters.Add("@aaa", SqlDbType.NVarChar, 30).Value = maaa;
                sqlcom.CommandText = "select case when datediff(d,chg_pwd,getdate())>180 then 1 else 0 end as ggg,* from CABASE_user where uid=@aaa";
                dv1 = aa.dv_param(sqlcom, ref mmsg);
                if (dv1 != null)
                {
                    if (dv1.Count > 0)
                    {
                        if ((int)dv1[0]["onf"] == 1)
                        {
                            if (dv1[0]["pwd"].ToString().Trim() == aa.encrypt(mbbb.Trim()) || mauto_login == "OK")
                            {
                                //context.Response.Write("["+mcccnam+"]");

                                //aa.exec("insert into CABASE_sys_log (unt,crt_uno,crt_usr,mem) values ('登入成功'," + aa.chkdbnull(dv1[0]["uno"]) + ",'" + mmfrm + "','')", ref mmsg);
                                mggg = aa.chkdbnull(dv1[0]["ggg"]);
                                context.Session["mmlogin_time"] = dv1[0]["login_time"];
                                context.Session["mmuno"] = aa.chkdbnull(dv1[0]["uno"]);
                                context.Session["mmuid"] = aa.chkdbnull(dv1[0]["uid"]);
                                context.Session["mmadm"] = aa.chkdbnull(dv1[0]["adm"]);
                                context.Session["mmemail"] = aa.chkdbnull(dv1[0]["email"]);
                                context.Session["mmnam"] = aa.chkdbnull(dv1[0]["nam"]);
                                context.Session["mmunt"] = aa.chkdbnull(dv1[0]["unt"]);
                                context.Session["mmunt_mng"] = aa.chkdbnull(dv1[0]["unt_mng"]);
                                context.Session["storeid"] = mccc;
                                context.Session["store"] = mcccnam;

                                context.Session["admpass"] = "youhavedoit";
                                //aa.system_log("ml_login.aspx", mmfrm, int.Parse(Session["mmuno"].ToString()), "帳號:" + maaa + ",登入成功");
                                aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]","[" +  context.Session["mmnam"] + "]", "[動作:POS登入]","POS登入");

                                dv1.Dispose();
                                if (matg != "")
                                {

                                    HttpCookie cookie = new HttpCookie("atg");
                                    cookie.Secure = false; //上線可能要加回去, 並強制ssl
                                    cookie.HttpOnly = true;
                                    cookie.Value = aa.encrypt(maaa+ ";"+mccc+";"+ mcccnam);
                                    cookie.Expires = DateTime.Now.AddDays(30);
                                    context.Response.AppendCookie(cookie);
                                }
                                else
                                {
                                    if (context.Request.Cookies["atg"] != null)
                                    {
                                        context.Response.Cookies["atg"].Expires = DateTime.Now.AddDays(-1);
                                    }
                                    maaa = "";
                                }


                                sqlcom.Parameters.Clear();
                                sqlcom.Parameters.Add("@aaa", SqlDbType.NVarChar, 250).Value = maaa;
                                sqlcom.Parameters.Add("@ip", SqlDbType.NVarChar, 250).Value = mmfrm;
                                sqlcom.CommandText = "update CABASE_user set login_fail=0,login_cnt=login_cnt+1,login_ip=@ip,login_time=getdate() where uid=@aaa";
                                if (!aa.exec_param(sqlcom, ref mmsg))
                                {
                                    context.Response.Write("Update error1");
                                }
                                else
                                {
                                    if (xurl.Length > 0)
                                    {
                                        if (xpara.Length > 0) context.Response.Redirect(xurl + "?" + xpara.Replace("***", "&"));
                                        else context.Response.Redirect(xurl);

                                    }
                                    else
                                    {
                                        context.Response.Write(".");
                                    }

                                }
                            }
                            else
                            {
                                sqlcom.Parameters.Clear();
                                sqlcom.Parameters.Add("@aaa", SqlDbType.NVarChar, 250).Value = maaa;
                                sqlcom.CommandText = "update CABASE_user set login_fail=login_fail+1 where uid=@aaa";
                                aa.exec_param(sqlcom, ref mmsg);

                                aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]", "[" + context.Session["mmnam"] + "]", "[動作:POS登入密碼錯誤][資料ID:" + maaa + "]","POS登入");
                                if (int.Parse(aa.chkdbnull(dv1[0]["login_fail"])) < 2)
                                {
                                    context.Response.Write("此帳號密碼有誤,剩" + (2 - int.Parse(aa.chkdbnull(dv1[0]["login_fail"]))) + "次機會,3次失敗即停權");
                                }
                                else
                                {
                                    context.Response.Write("此帳號密碼有誤3次,已停權並通知相關人員");

                                    sqlcom.CommandText = "update CABASE_user set login_fail=0,onf=0 where email=@aaa and uno>1";
                                    aa.exec_param(sqlcom, ref mmsg);

                                    //抓程式人員email
                                    string mto2 = aa.init(context.Request.ServerVariables["PATH_INFO"].ToUpper(), "後台管理程式人員E-mail");
                                    if (mto2.Length == 0) mto2 = "asp_debug@medialand.tw";
                                    //抓帳號email
                                    string mto = aa.chkdbnull(dv1[0]["email"]);
                                    if (mto.Length == 0) mto = mto2; //無就=程式人員email
                                    //aa.exec("insert into CABASE_sys_log (unt,crt_uno,crt_usr,mem) values ('登入失敗'," + aa.chkdbnull(dv1[0]["uno"]) + ",'" + mmfrm + "','累計3次密碼錯誤已停用')", ref mmsg);
                                    //aa.system_log("ml_login.aspx", mmfrm, 0, "登入失敗,帳號:" + maaa + ",密碼輸入3次錯誤停用");
                                    aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]", "[" + context.Session["mmnam"] + "]", "[動作:POS登入失敗][資料ID:" + maaa + "]","POS登入");
                                    if (aa.sendemail(aa.xxemail, mto, "", mto2, aa.xxemail, "帳號登入3次失敗通知", "輸入帳號:" + maaa + @"<p align=""right"">來自:" + context.Request.ServerVariables["HTTP_HOST"].ToString() + context.Request.ServerVariables["Path_Info"].ToString() + @"</p><p align=""right"">IP:" + mmfrm + @"</p>", 1, 3, "") == "OK") { }
                                }
                                dv1.Dispose();
                            }
                        }
                        else
                        {
                            dv1.Dispose();

                            context.Response.Write("此帳號已停用");

                        }
                    }
                    else
                    {
                        dv1.Dispose();
                        //aa.exec("insert into CABASE_sys_log (unt,crt_uno,crt_usr,mem) values ('登入失敗',0,'" + mmfrm + "','帳號錯誤:" + maaa + ",密碼:" + mbbb + "')", ref mmsg);
                        //aa.system_log("ml_login.aspx", mmfrm, 0, "登入失敗,帳號錯誤:" + maaa + ",密碼" + mbbb);
                        aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]", "[" + context.Session["mmnam"] + "]", "[動作:登入失敗][資料ID:" + maaa + "," + mbbb +"]","登入");
                        context.Response.Write(maaa+"此帳號密碼不符");

                    }
                }
                else
                {

                    context.Response.Write("mmsg");
                }
            }


        }
        else
        {

            context.Response.Write("ERROR01");

        }
        if (sqlcom != null) sqlcom.Dispose();
        //Session("vcode") = mid(cstr(10000+fix(rnd()*10000)),2,4);
        //Session["vcode"] = string.Format("{0:0000}", aa.rnd(10000)); // aa.generatevcode(4);


    }


}