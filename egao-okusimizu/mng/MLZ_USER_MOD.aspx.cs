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
{   //以下為模組共用的宣告變數,勿動
    public DataView dv1,dv2;
    public int mpage, mc;
    //以下為自行使用宣告
    public string muid, mpwd="", mnam, mtel, monf, memail, munt="", mtel_a, mfax_a, mgrs, mgns, mfax, madm, mgnos, mgrnos, mcrt_usr, mcrt_dat, mupd_usr, mupd_dat, mupd_usr2, mupd_dat2, mupd_usr3, mupd_dat3, magn1, magn2, magn2_onf, magn1_onf, munt_mng;
    public string mtablename, mfieldsno, mfieldfsno, mfieldweb, mfieldweb_e, mfieldurl, mfieldurl2, mfieldsot, maspfile;
    public string msel_unt = "", mstk="";
    public int ii = 0;
    public string[] bb;
    public SqlCommand sqlcom = new SqlCommand(), sqlcom_s = new SqlCommand();


    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));
        msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref mmsg); if (msno == "0") msno = aa.chktyp(Request.QueryString["sno"], 0, "I", 0, 0, "", ref mmsg); //_mod.aspx修改頁時, chkath會固定用msno (資料key=0 or !=0)來判斷是新增或修改模式, 請放這行才能正常呈現權限
        if (!chkath()) { mng_main.Visible = false; }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...


        msno = "0";
        mmsg = "";
        mgns = "0";
        mgrs = "0";
        madm = "1";
        magn1 = "0";
        magn1_onf = "0";
        magn2 = "0";
        magn2_onf = "0";

        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 5, "", ref mmsg);

        mpage = Int32.Parse(aa.chktyp(Request.Form["page"], 1, "I", 0, 0, "", ref mmsg));
        mc = Int32.Parse(aa.chktyp(Request.Form["mc"], 1, "I", 0, 0, "", ref mmsg));
        msno = aa.chktyp(Request.QueryString["sno"], 0, "I", 0, 0, "", ref mmsg);
        if (msno == "0") msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref mmsg);
        xord = aa.chktyp(Request.Form["xord"], 0, "C", 0, 20, "", ref mmsg);
        xsot = aa.chktyp(Request.Form["xsot"], 0, "C", 0, 20, "", ref mmsg);
        mt1 = aa.chktyp(Request.Form["t1"], 0, "C", 0, 50, "", ref mmsg);


        if (mb1 != "")
        {

            msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref mmsg);

            if (mb1 == "SAV")
            {

                muid = aa.chktyp(Request.Form["uid"], 1, "C", 3, 15, "門市人員帳號", ref mmsg);
                if (msno == "0") mpwd = aa.chktyp(Request.Form["pwd"], 1, "PWDSET", 8, 15, "密碼", ref mmsg);
                else mpwd = aa.chktyp(Request.Form["pwd"], 0, "PWDSET", 8, 15, "密碼", ref mmsg);

                mnam = aa.chktyp(Request.Form["nam"], 0, "C", 0, 20, "姓名", ref mmsg);
                memail = aa.chktyp(Request.Form["email"], 1, "E", 0, 200, "信箱", ref mmsg);

            }
            mstk = aa.chktyp(Request.Form["stk"], 0, "C", 0, 500, "門市", ref mmsg);
            munt = aa.chktyp(Request.Form["unt"], 0, "C", 0, 100, "單位", ref mmsg);
            munt_mng = aa.chktyp(Request.Form["unt_mng"], 0, "I", 0, 100, "主管或職員", ref mmsg);
            mtel_a = aa.chktyp(Request.Form["tel_a"], 0, "C", 0, 10, "電話區碼", ref mmsg);
            mtel = aa.chktyp(Request.Form["tel"], 0, "TEL", 0, 30, "電話", ref mmsg);
            mfax_a = aa.chktyp(Request.Form["fax_a"], 0, "C", 0, 10, "傳真區碼", ref mmsg);
            mfax = aa.chktyp(Request.Form["fax"], 0, "C", 0, 30, "傳真", ref mmsg);


            monf = aa.chktyp(Request.Form["onf"], 0, "I", 0, 100, "帳號狀態", ref mmsg);
            magn1 = aa.chktyp(Request.Form["agn1"], 0, "I", 0, 100, "第1代理人", ref mmsg);
            magn2 = aa.chktyp(Request.Form["agn2"], 0, "I", 0, 100, "第2代理人", ref mmsg);
            magn1_onf = aa.chktyp(Request.Form["agn1_onf"], 0, "I", 0, 100, "第1代理人啟用", ref mmsg);
            magn2_onf = aa.chktyp(Request.Form["agn2_onf"], 0, "I", 0, 100, "第2代理人啟用", ref mmsg);

            mgrs = aa.chktyp(Request.Form["grs"], 0, "I", 0, 0, "", ref mmsg);
            mgns = aa.chktyp(Request.Form["gns"], 0, "I", 0, 0, "", ref mmsg);
            madm = aa.chktyp(Request.Form["adm"], 0, "I", 0, 0, "", ref mmsg);
            mgnos = aa.chktyp(Request.Form["gnos"], 0, "C", 0, 200, "", ref mmsg);
            mgrnos = aa.chktyp(Request.Form["grnos"], 0, "C", 0, 500, "", ref mmsg);

            if (mpwd!=null && mpwd != "") { if (mpwd.ToUpper().IndexOf(muid.ToUpper()) >= 0) mmsg = "密碼不可包含帳號"; };

            if (mmsg != "") mmsg2 = mmsg;
        }



        if ((mb1 == "SAV" || mb1=="RES" ) && mmsg2.Length == 0)
        {
            //sqlcom = new SqlCommand();
            sqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
            sqlcom.Parameters.Add("@nam", SqlDbType.NVarChar, 50).Value = mnam;
            sqlcom.Parameters.Add("@uid", SqlDbType.NVarChar, 50).Value = muid;
            sqlcom.Parameters.Add("@email", SqlDbType.NVarChar, 200).Value = memail;
            if (msno == "1") madm = "2";
            sqlcom.Parameters.Add("@adm", SqlDbType.Int, 8).Value = madm;
            sqlcom.Parameters.Add("@grs", SqlDbType.Int, 8).Value = mgrs;
            if (mpwd!="") sqlcom.Parameters.Add("@pwd", SqlDbType.NVarChar, 50).Value = aa.encrypt(mpwd);

            sqlcom.Parameters.Add("@unt", SqlDbType.NVarChar, 50).Value = munt;
            sqlcom.Parameters.Add("@crt_usr", SqlDbType.NVarChar, 50).Value = Session["mmnam"];
            sqlcom.Parameters.Add("@crt_uno", SqlDbType.Int, 8).Value = Session["mmuno"];
            sqlcom.Parameters.Add("@unt_mng", SqlDbType.Int, 8).Value = munt_mng;
            sqlcom.Parameters.Add("@onf", SqlDbType.Int, 8).Value = monf;
            
            mmsg = "";
            if (msno == "0")
            {
                sqlcom.CommandText = "select * from CABASE_user where uid=@uid";
                dv1 = aa.dv_param(sqlcom, ref mmsg);
                
                if (dv1 != null)
                {
                    if (dv1.Count > 0)
                    {
                        Response.Write(aa.alert_back("帳號重覆使用,請改其他"));
                        Response.End();
                    }

                    dv1.Dispose();
                }



                
                sqlcom.CommandText = @"insert into CABASE_user (nam,uid,email,adm,grs,pwd,crt_usr,crt_uno,crt_dat,unt,unt_mng,onf) values(@nam,@uid,@email,@adm,@grs,@pwd,@crt_usr,@crt_uno,getdate(),@unt,@unt_mng,@onf);select scope_identity(); ";
                //sqlcom.CommandText = @"insert into CABASE_user (nam,uid,email,adm,grs,crt_usr,crt_uno,crt_dat,unt,unt_mng,onf) values(@nam,@uid,@email,@adm,@grs,@crt_usr,@crt_uno,getdate(),@unt,@unt_mng,@onf);select scope_identity(); ";
                if (!aa.exec_param(sqlcom, ref mmsg))

                {

                    Response.Write(aa.alert_back(mmsg));
                    Response.End();
                }                
                else { msno = mmsg; aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]","[" +  Session["mmnam"] + "]", "[動作:新增][資料ID:" + msno + "]",""); }

            }
            else
            {
                if (Session["mmadm"].ToString() == "2")
                {
                    if (mpwd != "") mSql = "update CABASE_user set nam=@nam,uid=@uid,email=@email,adm=@adm,grs=@grs,pwd=@pwd,upd_Usr=@crt_usr,upd_uno=@crt_uno,upd_dat=getdate(),unt=@unt,unt_mng=@unt_mng,onf=@onf where uno=@sno";
                    else mSql = "update CABASE_user set nam=@nam,uid=@uid,email =@email,adm=@adm,grs=@grs,upd_Usr=@crt_usr,upd_uno=@crt_uno,upd_dat=getdate(),unt=@unt,unt_mng=@unt_mng,onf=@onf where uno=@sno";

                }
                else
                {
                    if (mpwd != "") mSql = "update CABASE_user set nam=@nam,uid=@uid,email=@email,grs=@grs,pwd=@pwd,upd_Usr=@crt_usr,upd_uno=@crt_uno,upd_dat=getdate(),unt=@unt,unt_mng=@unt_mng,onf=@onf where uno=@sno";
                    else mSql = "update CABASE_user set nam=@nam,uid=@uid,email =@email,grs=@grs,upd_Usr=@crt_usr,upd_uno=@crt_uno,upd_dat=getdate(),unt=@unt,unt_mng=@unt_mng,onf=@onf where uno=@sno";

                }




                sqlcom.CommandText = mSql;

                if (!aa.exec_param(sqlcom, ref mmsg))
                {

                    Response.Write(aa.alert_back("b:"+mmsg+mSql));
                    Response.End();
                }
                else aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]","[" +  Session["mmnam"] + "]", "[動作:修改][資料ID:" + msno + "]","");
            }
            //倉庫
            string mgraps = aa.chktyp(Request.Form["stk"], 0, "C", 0, 1000, "", ref mmsg);
            if (mgraps != "")
            {
                mgraps = mgraps.Replace(";", ",");
                string[] mara = mgraps.Split(',');
                string mstr = "";
                for (int i = 0; i < mara.Length; i++)
                {

                    
                    
                        if (i > 0) mstr += "','";
                        mstr += mara[i];
                    
                    
                }
                mgraps = mstr.Replace(",''","");

                sqlcom.Parameters.Clear();
                sqlcom.CommandText = "delete from pos_user_stk where uno=@sno and soo not in ('" + mgraps + "')";
                sqlcom.Parameters.Add("@uno", SqlDbType.Int, 8).Value = msno;
                if (!aa.exec_param(sqlcom, ref mmsg)) Response.Write("E:" + mmsg);

                sqlcom.Parameters.Clear();
                sqlcom.CommandText = "insert into POS_user_stk (uno,soo) select @uno,soo from POS_STK where soo in ('" + mgraps + "') and soo not in (select soo from pos_user_stk where uno=@uno)";
                sqlcom.Parameters.Add("@uno", SqlDbType.Int, 8).Value = msno;
                if (!aa.exec_param(sqlcom, ref mmsg)) Response.Write("E2:" + mmsg);
            }
            //存主網站使用者權限
            if (dv1 != null) dv1 = null;
            sqlcom.Parameters.Clear();
            sqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
            sqlcom.CommandText = "delete from CABASE_user_ito where uno=@sno";
            if (!aa.exec_param(sqlcom, ref mmsg))
            {
                Response.Write(aa.alert_back("a:"+mmsg));
                Response.End();
            }
            sqlcom.Parameters.Clear();
            sqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
            sqlcom.CommandText = "insert into CABASE_user_ito (uno,ito,ade) select @sno,sno,'' from CABASE_ito";
            if (!aa.exec_param(sqlcom, ref mmsg))
            {
                Response.Write(aa.alert_back("a2:" + mmsg));
                Response.End();
            }
            sqlcom.Parameters.Clear();
            sqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
            sqlcom.CommandText = "select * from CABASE_user_ito a where a.uno=@sno";
            dv1 = aa.dv_param(sqlcom, ref mmsg);
            if (dv1 != null)
            {
                for (int i = 0; i <= dv1.Count - 1; i++)
                {
                    string _ade = aa.chktyp(Request.Form["ade" + dv1[i]["ito"]], 0, "C", 0, 50, "", ref mmsg);

                    sqlcom_s = new SqlCommand();
                    sqlcom_s.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
                    sqlcom_s.Parameters.Add("@ito", SqlDbType.Int, 8).Value = dv1[i]["ito"];
                    sqlcom_s.Parameters.Add("@ade", SqlDbType.NVarChar, 50).Value = _ade;
                    sqlcom_s.Parameters.Add("@crt_usr", SqlDbType.NVarChar, 50).Value = Session["mmnam"];
                    sqlcom_s.Parameters.Add("@crt_uno", SqlDbType.Int, 8).Value = Session["mmuno"];
                    sqlcom_s.CommandText = "update CABASE_user_ito set ade=@ade,crt_usr=@crt_usr,crt_uno=@crt_uno,crt_dat=getdate() where uno=@sno and ito=@ito";
                    if (!aa.exec_param(sqlcom_s, ref mmsg))
                    {

                        Response.Write(aa.alert_back("34"+mmsg));
                        Response.End();
                    }

                }
                dv1.Dispose();
            }

            //end 存使用者權限
           
            

            if (mgrnos != null && mgrnos != "")
            {
                string[] dd2;

                dd2 = mgrnos.Split(',');
                for (int i = 0; i < dd2.Length; i++)
                {
                    sqlcom_s = new SqlCommand();
                    sqlcom_s.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
                    sqlcom_s.Parameters.Add("@grno", SqlDbType.Int, 8).Value = dd2[i];
                    sqlcom_s.Parameters.Add("@crt_usr", SqlDbType.NVarChar, 50).Value = Session["mmnam"];
                    sqlcom_s.Parameters.Add("@crt_uno", SqlDbType.Int, 8).Value = Session["mmuno"];
                    sqlcom_s.CommandText = "insert into CABASE_user_group (uno,grno,crt_usr,crt_uno,crt_dat) values (@sno,@grno,@crt_usr,@crt_uno,getdate())";
                    if (!aa.exec_param(sqlcom_s, ref mmsg))
                    {
                        Response.Write(aa.alert_back("1:"+mmsg));
                        Response.End();
                    }

                }
                string[] wara = mgrnos.Split(',');
                var parameters = new string[wara.Length];
                for (int mi = 0; mi < wara.Length; mi++)
                {
                    parameters[mi] = string.Format("@Age{0}", mi);
                    sqlcom.Parameters.Add(parameters[mi], SqlDbType.Int, 8).Value = wara[mi];
                }
                sqlcom.CommandText = string.Format("delete from CABASE_user_group where uno=@sno and grno not in ({0})", string.Join(", ", parameters));


                //sqlcom.CommandText = "";            
                if (!aa.exec_param(sqlcom, ref mmsg))
                {
                    Response.Write(aa.alert_back("2:"+mmsg));
                    Response.End();
                }
            }
            else
            {
                if (Int32.Parse(mgrs) == 1)
                {
                    sqlcom.CommandText = "insert into CABASE_user_group (uno,grno,crt_usr,crt_uno,crt_dat) select @sno,sno,@crt_usr,@crt_uno,getdate() from CABASE_group";
                    if (!aa.exec_param(sqlcom, ref mmsg))
                    {
                        Response.Write(aa.alert_back(mmsg));
                        Response.End();
                    }
                }
                else
                {
                    sqlcom.CommandText = "delete from CABASE_user_group where uno=@sno";
                    if (!aa.exec_param(sqlcom, ref mmsg))
                    {
                        Response.Write(aa.alert_back(mmsg));
                        Response.End();
                    }
                }
            }

            if (mb1 == "RES")
            {
                if (memail.Length > 0)
                {
                    if (aa.sendemail(aa.xxemail, memail, "", "", aa.xxemail, "後台帳號/密碼通知(" + Session["後台管理標題"].ToString() + ")", "您好:<br>請利用 帳號:" + muid + ",密碼:" + mpwd + " 登入後台管理系統,<br>謝謝.", 1, 3, "") == "OK") mmsg2 = "發送密碼重設信成功";
                    else mmsg2 = "發信失敗";
                }
                else mmsg2 = "請填E-mail";
            }
            else mmsg2 = "儲存完畢";
            //若上層單元是用參數時,如: CABASE_news.aspx?typ=1 時, 請勿用下列URL方式  
            //Response.Redirect(YYurl + "?xord=" + xord + "&xsot=" + xsot + "&mc=" + mc + "&page=" + mpage + "&t1=" + mt1 + "&typ=" + mtyp);

        }

        

        if (msno != "0" || Int32.Parse(msno) > 0)
        {
            sqlcom.Parameters.Clear();
            sqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
            sqlcom.CommandText = "select * from CABASE_user a where a.uno=@sno";
            dv1 = aa.dv_param(sqlcom, ref mmsg);
            {
                if (dv1.Count > 0)
                {
                    mnam = aa.chkdbnull(dv1[0]["nam"]);
                    munt = aa.chkdbnull(dv1[0]["unt"]);
                    memail = aa.chkdbnull(dv1[0]["email"]);
                    
                    madm = aa.chkdbnull(dv1[0]["adm"]);
                    mgrs = aa.chkdbnull(dv1[0]["grs"]);
                    mpwd = aa.chkdbnull(dv1[0]["pwd"]);
                    muid = aa.chkdbnull(dv1[0]["uid"]);
                    munt_mng = aa.chkdbnull(dv1[0]["unt_mng"]);
                    
                    
                    monf = aa.chkdbnull(dv1[0]["onf"]);
                    mcrt_usr = aa.chkdbnull(dv1[0]["crt_usr"]);
                    mcrt_dat = aa.chkdbnull(dv1[0]["crt_dat"]);
                    mupd_usr = aa.chkdbnull(dv1[0]["upd_usr"]);
                    mupd_dat = aa.chkdbnull(dv1[0]["upd_dat"]);
                    mupd_usr2 = aa.chkdbnull(dv1[0]["upd_usr2"]);
                    mupd_dat2 = aa.chkdbnull(dv1[0]["upd_dat2"]);
                    mupd_usr3 = aa.chkdbnull(dv1[0]["upd_usr3"]);
                    mupd_dat3 = aa.chkdbnull(dv1[0]["upd_dat3"]);
                }
                dv1.Dispose();
            }
        }
        
      
        
    }
   
    protected string menu1(int ito, int mcc, int mcnt, string maa, int msts, int msts2)  //mcnt,本層筆數,msts父層筆數,msts2=目前的父層為第几筆
    {
        int j = 1;
        string mstr, mbb, mmsg;
        DataView rs1;
        mstr = "";
        mmsg = "";
        mbb = "";



        sqlcom = new SqlCommand();
        sqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
        sqlcom.Parameters.Add("@ito", SqlDbType.Int, 8).Value = ito;

        sqlcom.CommandText = "select a.*,case when b.ade is null then '' else b.ade end as ade from CABASE_ito a left outer join CABASE_user_ito b on a.sno=b.ito and b.uno=@sno where a.fsno=@ito  order by sot";
        rs1 = aa.dv_param(sqlcom, ref mmsg);
        if (rs1 != null)
        {
            for (int i = 0; i <= rs1.Count - 1; i++)
            {
                mbb = maa;
                mstr = mstr + @"<li class=""list-group-item node-treeview3""  style="" display: table; width:100%"">";


                for (int ii = 0; ii < mcc; ii++)
                {
                    mstr += @"<span class=""indent"" class=""hidden-sm-down  not-visible""></span>";
                }

                if (mcc == 0) mstr += @" <span class=""icon expand-icon glyphicon glyphicon-minus""></span>";

                mstr = mstr + @"<span class=""icon node-icon""></span>";

                //mstr+= @"<input class=""form-control"" type=""text"" size=""1"" name=""sot" + rs1[i]["sno"] + @""" value=""" + rs1[i]["sot"].ToString().Trim() + "\" style=\"min-width:40px\">";


            
                mstr = mstr + @"<span class=""icon node-icon""></span>" + rs1[i]["nam"].ToString().Trim();

                mstr = mstr + @"<span class=""icon node-icon""></span><span style=""float: right;"">　";
                if (aa.chkdbnull(rs1[i]["fsno"])=="0") mstr = mstr + @"<input type=""checkbox""  class=""largerCheckbox"" id=""parent" + rs1[i]["sno"] + @""" value=""ON"" onclick=""selAll('" + rs1[i]["fsno"] + @"','" + rs1[i]["sno"] + @"')"">全選　";
                else mstr = mstr + @"<input type=""checkbox""  class=""largerCheckbox"" id=""self" + rs1[i]["sno"] + @""" value=""ON"" onclick=""selAll('" + rs1[i]["fsno"] + @"','" + rs1[i]["sno"] + @"')"">全選　";

                //mstr = mstr + Trim(rs1(i)("ade"))
                //以下呈現編輯功能
                //mstr=mstr + "...[<span style=""curs1(i)or:hand"" onclick=""javascript:form2.mode.value='A';form2.fsno.value='" + rs1(i)(mfieldsno) + "';form2.submit();""><font color=""#0000FF"">新增</font></span>][<span style=""curs1(i)or:hand"" onclick=""javascript:form2.mode.value='M';form2.sno.value='" + rs1(i)(mfieldsno) + "';form2.submit();""><font color=""#0000FF"">修改</font></span>][<span style=""curs1(i)or:hand"" onclick=""javascript:if (confirm('確定刪除" + trim(rs1(i)(mfieldweb)) + "嗎?')) {form2.mode.value='D';form2.sno.value='" + rs1(i)(mfieldsno) + "';form2.submit();}""><font color=""#0000FF"">刪除</font></span>]"
                if (rs1[i]["ade"].ToString().Trim().IndexOf("B") >= 0) mstr = mstr + @"<input type=""checkbox""  class=""largerCheckbox parent" + rs1[i]["fsno"] + @" self" + rs1[i]["sno"] + @""" value=""B""  name=""ade" + rs1[i]["sno"] + @""" checked>瀏覽　";
                else mstr = mstr + @"<input type=""checkbox"" class=""largerCheckbox parent"+ rs1[i]["fsno"] + @" self" + rs1[i]["sno"] + @""" value=""B"" name=""ade" + rs1[i]["sno"] + @""">瀏覽　";
                if (aa.chkdbnull(rs1[i]["url"]) != "")
                {
                    if (rs1[i]["ade"].ToString().Trim().IndexOf("A") >= 0) mstr = mstr + @"<input type=""checkbox""  class=""largerCheckbox parent" + rs1[i]["fsno"] + @" self" + rs1[i]["sno"] + @""" value=""A""  name=""ade" + rs1[i]["sno"] + @""" checked>新增　";
                    else mstr = mstr + @"<input type=""checkbox"" class=""largerCheckbox parent" + rs1[i]["fsno"] + @" self" + rs1[i]["sno"] + @""" value=""A"" name=""ade" + rs1[i]["sno"] + @""">新增　";

                    if (rs1[i]["ade"].ToString().Trim().IndexOf("E") >= 0) mstr = mstr + @"<input type=""checkbox""  class=""largerCheckbox parent" + rs1[i]["fsno"] + @" self" + rs1[i]["sno"] + @""" value=""E""  name=""ade" + rs1[i]["sno"] + @""" checked>修改　";
                    else mstr = mstr + @"<input type=""checkbox"" class=""largerCheckbox parent" + rs1[i]["fsno"] + @" self" + rs1[i]["sno"] + @""" value=""E"" name=""ade" + rs1[i]["sno"] + @""">修改　";

                    if (rs1[i]["ade"].ToString().Trim().IndexOf("D") >= 0) mstr = mstr + @"<input type=""checkbox""  class=""largerCheckbox parent" + @" self" + rs1[i]["sno"] + @""" value=""D""  name=""ade" + rs1[i]["sno"] + @""" checked>刪除　";
                    else mstr = mstr + @"<input type=""checkbox"" class=""largerCheckbox parent" + rs1[i]["fsno"] + @" self" + rs1[i]["sno"] + @""" value=""D"" name=""ade" + rs1[i]["sno"] + @""">刪除　";

                    if (rs1[i]["ade"].ToString().Trim().IndexOf("C") >= 0) mstr = mstr + @"<input type=""checkbox""  class=""largerCheckbox parent" + rs1[i]["fsno"] + @" self" + rs1[i]["sno"] + @""" value=""C""  name=""ade" + rs1[i]["sno"] + @""" checked>匯出　";
                    else mstr = mstr + @"<input type=""checkbox"" class=""largerCheckbox parent" + rs1[i]["fsno"] + @" self" + rs1[i]["sno"] + @""" value=""C"" name=""ade" + rs1[i]["sno"] + @""">匯出　";


                    //if (rs1[i]["ade"].ToString().Trim().IndexOf("C") >= 0) mstr = mstr + @"<input type=""checkbox"" value=""C""  name=""ade" + rs1[i]["sno"] + @""" checked>個資";
                    //else mstr = mstr + @"<input type=""checkbox"" value=""C"" name=""ade" + rs1[i]["sno"] + @""">個資";
                }
                mstr = mstr + "" + "</span></li>";
                mstr = mstr + menu1(Int32.Parse(rs1[i]["sno"].ToString()), mcc + 1, Int32.Parse(rs1[i]["cnt"].ToString()), mbb, rs1.Count, j);
                j = j + 1;



            }
            //return mstr;
            rs1.Dispose();
        }
        else
        {
            Response.Write(aa.alert(mmsg));
            Response.End();
        }
        return mstr;
    }
    
}