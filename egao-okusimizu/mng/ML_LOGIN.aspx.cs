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
using System.Web.Services;

public partial class _default : MLBasePage 
{
    public DataView dv1;
    public int mpage, mc;
    public string xurl, xpara="";
    public string mggg="",maaa="",mbbb="",matg="", mauto_login = "",msel_store="",mccc="",mcccnam="";
    public SqlCommand sqlcom = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {  
        init_class(); //初始化class含db connection, email環境設定
        DataView dv1;
        string mb1 = "";
        string[] xxips;
        int xxips_sts;
        string mtoken =aa.decrypt(aa.chktyp(Request.QueryString["token"],0,"C",0,350,"",ref mmsg));
        
        maaa = "";
        Label1.Text = "";

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
            Response.Write(xxips.Length.ToString()+"IP不允許");
            Response.End();
        }
        xurl = aa.chktyp(Request["xurl"], 0, "C", 0, 250, "", ref mmsg);
        
        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 3, "", ref mmsg);
        

        if (Request.Cookies["atg"] != null && aa.decrypt(Request.Cookies["atg"].Value).IndexOf(";")>=0) { matg = "on"; 
            string[] nara = aa.decrypt(Request.Cookies["atg"].Value).Split(';'); maaa = nara[0];mccc = nara[1]; mcccnam = nara[2];

            mauto_login = "OK"; }
        
        if ((mb1 == "SAV" ) || mauto_login == "OK")
        {
            
            if (mauto_login=="")
            {
               
                matg = aa.chktyp(Request.Form["atg"], 0, "C", 0, 5, "", ref mmsg);
                
                xurl = aa.chktyp(Request.Form["xurl"], 0, "C", 0, 250, "", ref mmsg);
                xpara = aa.chktyp(Request.Form["xpara"], 0, "C", 0, 250, "", ref mmsg);
                maaa = aa.chktyp(Request.Form["aaa"], 1, "C", 0, 15, "帳號", ref mmsg);
                mbbb = aa.chktyp(Request.Form["bbb"], 1, "C", 0, 15, "密碼", ref mmsg);
                
               


               
            } 


            if (mmsg.Length > 0)
            {
                Label1.Text = mmsg;
            }
            else
            {
                


                //sqlcom.CommandText = "alter table CABASE_user add login_fail int null default(0)";
                //aa.exec_param(sqlcom, ref mmsg);
                //sqlcom.CommandText = "update CABASE_user set login_fail=0 where login_fail is null";
                //aa.exec_param(sqlcom, ref mmsg);
                //Response.Write("...." + maaa);

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

                                //aa.exec("insert into CABASE_sys_log (unt,crt_uno,crt_usr,mem) values ('登入成功'," + aa.chkdbnull(dv1[0]["uno"]) + ",'" + mmfrm + "','')", ref mmsg);
                                mggg = aa.chkdbnull(dv1[0]["ggg"]);
                                Session["mmlogin_time"] = dv1[0]["login_time"];
                                Session["mmuno"] = aa.chkdbnull(dv1[0]["uno"]);
                                Session["mmuid"] = aa.chkdbnull(dv1[0]["uid"]);
                                Session["mmadm"] = aa.chkdbnull(dv1[0]["adm"]);
                                Session["mmemail"] = aa.chkdbnull(dv1[0]["email"]);
                                Session["mmnam"] = aa.chkdbnull(dv1[0]["nam"]);
                                Session["mmunt"] = aa.chkdbnull(dv1[0]["unt"]);
                                Session["mmunt_mng"] = aa.chkdbnull(dv1[0]["unt_mng"]);
                              
                                
                                Session["admpass"] = "youhavedoit";
                                //aa.system_log("ml_login.aspx", mmfrm, int.Parse(Session["mmuno"].ToString()), "帳號:" + maaa + ",登入成功");
                                aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]","[" +  Session["mmnam"] + "]", "[動作:登入]","登入");

                                dv1.Dispose();
                                if (matg != "")
                                {

                                    HttpCookie cookie = new HttpCookie("atg");
                                    cookie.Secure = false; //上線可能要加回去, 並強制ssl
                                    cookie.HttpOnly = true;
                                    cookie.Value = aa.encrypt(maaa);
                                    cookie.Expires = DateTime.Now.AddDays(30);
                                    Response.AppendCookie(cookie);
                                }
                                else
                                {
                                    if (Request.Cookies["atg"] != null)
                                    {
                                        Response.Cookies["atg"].Expires = DateTime.Now.AddDays(-1);
                                    }
                                    maaa = "";
                                }


                                sqlcom = new SqlCommand();
                                sqlcom.Parameters.Add("@aaa", SqlDbType.NVarChar, 250).Value = maaa;
                                sqlcom.Parameters.Add("@ip", SqlDbType.NVarChar, 250).Value = mmfrm;
                                sqlcom.CommandText = "update CABASE_user set login_fail=0,login_cnt=login_cnt+1,login_ip=@ip,login_time=getdate() where uid=@aaa";
                                if (!aa.exec_param(sqlcom, ref mmsg))
                                {
                                    Label1.Text = mmsg;
                                }
                                else
                                {
                                    if (xurl.Length > 0)
                                    {
                                        if (xpara.Length > 0) Response.Redirect(xurl + "?" + xpara.Replace("***", "&"));
                                        else Response.Redirect(xurl);

                                    }
                                    else
                                    {
                                        if (mggg == "1") { Response.Write(aa.alert_to("您已6個月未更換密碼,建議您定期更換密碼~", "ml_chgpwd.aspx")); Response.End(); }
                                        else Response.Redirect("ML_INDEXMAIN.ASPX");
                                    }

                                }
                            }
                            else
                            {
                                sqlcom = new SqlCommand();
                                sqlcom.Parameters.Add("@aaa", SqlDbType.NVarChar, 250).Value = maaa;
                                sqlcom.CommandText = "update CABASE_user set login_fail=login_fail+1 where uid=@aaa";
                                aa.exec_param(sqlcom, ref mmsg);

                                
                                //aa.exec("insert into CABASE_sys_log (unt,crt_uno,crt_usr,mem) values ('登入失敗'," + aa.chkdbnull(dv1[0]["uno"]) + ",'" + mmfrm + "','登入密碼錯誤')", ref mmsg);
                                //aa.system_log("ml_login.aspx", mmfrm, 0, "登入失敗,帳號:" + maaa + ",密碼輸入錯誤");
                                aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:登入密碼錯誤][資料ID:" + maaa + "]","登入");
                                if (int.Parse(aa.chkdbnull(dv1[0]["login_fail"])) < 2)
                                {
                                    Label1.Text = "此帳號密碼有誤,剩" + (2 - int.Parse(aa.chkdbnull(dv1[0]["login_fail"]))) + "次機會,3次失敗即停權";
                                }
                                else
                                {
                                    Label1.Text = "此帳號密碼有誤3次,已停權並通知相關人員";

                                    sqlcom.CommandText = "update CABASE_user set login_fail=0,onf=0 where email=@aaa and uno>1";
                                    aa.exec_param(sqlcom, ref mmsg);
                                    
                                    //抓程式人員email
                                    string mto2 = aa.init(Request.ServerVariables["PATH_INFO"].ToUpper(), "後台管理程式人員E-mail");
                                    if (mto2.Length == 0) mto2 = "asp_debug@medialand.tw";
                                    //抓帳號email
                                    string mto = aa.chkdbnull(dv1[0]["email"]);
                                    if (mto.Length == 0) mto = mto2; //無就=程式人員email
                                    //aa.exec("insert into CABASE_sys_log (unt,crt_uno,crt_usr,mem) values ('登入失敗'," + aa.chkdbnull(dv1[0]["uno"]) + ",'" + mmfrm + "','累計3次密碼錯誤已停用')", ref mmsg);
                                    //aa.system_log("ml_login.aspx", mmfrm, 0, "登入失敗,帳號:" + maaa + ",密碼輸入3次錯誤停用");
                                    aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:登入失敗][資料ID:" + maaa + "]","登入");
                                    if (aa.sendemail(aa.xxemail, mto, "", mto2, aa.xxemail, "帳號登入3次失敗通知", "輸入帳號:" + maaa + @"<p align=""right"">來自:" + Request.ServerVariables["HTTP_HOST"].ToString() + Request.ServerVariables["Path_Info"].ToString() + @"</p><p align=""right"">IP:" + mmfrm + @"</p>", 1, 3, "") == "OK") { }
                                }
                                dv1.Dispose();
                            }
                        }
                        else
                        {
                            dv1.Dispose();

                            Label1.Text = "此帳號已停用";

                        }
                    }
                    else
                    {
                        dv1.Dispose();
                        //aa.exec("insert into CABASE_sys_log (unt,crt_uno,crt_usr,mem) values ('登入失敗',0,'" + mmfrm + "','帳號錯誤:" + maaa + ",密碼:" + mbbb + "')", ref mmsg);
                        //aa.system_log("ml_login.aspx", mmfrm, 0, "登入失敗,帳號錯誤:" + maaa + ",密碼" + mbbb);
                        aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:登入失敗][資料ID:" + maaa + "," + mbbb +"]","登入");
                        Label1.Text = maaa+"此帳號密碼不符";

                    }
                }
                else
                {

                    Label1.Text = mmsg;
                }
            }
            if (matg == "")
            {
                //Response.Write(matg + "," + maaa);
                if (Request.Cookies["atg"] != null)
                {
                    Response.Cookies["atg"].Expires = DateTime.Now.AddDays(-1);
                }
                maaa = "";
            }

        }

        else
        {
            xpara = aa.chktyp(Request["xpara"], 0, "C", 0, 250, "", ref mmsg);
            xurl = aa.chktyp(Request["xurl"], 0, "C", 0, 250, "", ref mmsg);
            if (Request.Cookies["atg"] != null && aa.decrypt(Request.Cookies["atg"].Value) != "") { matg = "on"; maaa = aa.decrypt(Request.Cookies["atg"].Value);  }

        }
        if (sqlcom != null) sqlcom.Dispose();
        //Session("vcode") = mid(cstr(10000+fix(rnd()*10000)),2,4);
        //Session["vcode"] = string.Format("{0:0000}", aa.rnd(10000)); // aa.generatevcode(4);


        xxips = aa.init(Request.ServerVariables["PATH_INFO"].ToUpper().Trim(), "後台管理IP").Split(',');
        xxips_sts = 0;

        for (int i = 0; i < xxips.Length; i++)
        {
            if (xxips[i] == "*" || Request.ServerVariables["remote_addr"].IndexOf(xxips[i]) >= 0) xxips_sts = 1;
        }

        if (xxips_sts == 0) Response.Redirect("../");


        if (Session["後台管理標題"] != null) Session["後台管理標題"] = aa.init(Request.ServerVariables["PATH_INFO"].ToUpper().Trim(), "後台管理標題");

        if (Session["後台管理版權"] != null) Session["後台管理版權"] = aa.init(Request.ServerVariables["PATH_INFO"].ToUpper().Trim(), "後台管理版權");

       

        
    }
    
    
}