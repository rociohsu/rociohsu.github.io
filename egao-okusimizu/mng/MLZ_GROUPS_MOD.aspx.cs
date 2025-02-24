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
    public string muno, muid, msor, mpwd, mnam, mtel, memail, munt, mtel_a, mfax_a, mgrs, mgns, mfax, madm, mgnos, mgrnos, mcrt_usr, mcrt_dat, mupd_usr, mupd_dat, mupd_usr2, mupd_dat2, mupd_usr3, mupd_dat3;
    public int ii = 0;
    public string mtablename, mfieldsno, mfieldfsno, mfieldweb, mfieldweb_e, mfieldurl, mfieldurl2, mfieldsot, maspfile;

    public string[] bb;

    public DataView dv1;
    public int mpage, mc;
    public SqlCommand sqlcom, sqlcom_s;

    protected void Page_Load(object sender, EventArgs e)
    {  
        
        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));   
        msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref mmsg); if (msno == "0") msno = aa.chktyp(Request.QueryString["sno"], 0, "I", 0, 0, "", ref mmsg); 
        if (!chkath()) { mng_main.Visible = false; }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...
        
        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 3, "", ref mmsg);

        mpage = Int32.Parse(aa.chktyp(Request.Form["page"], 1, "I", 0, 0, "", ref  mmsg));
        mc = Int32.Parse(aa.chktyp(Request.Form["mc"], 1, "I", 0, 0, "", ref  mmsg));
        
        
        xord = aa.chktyp(Request.Form["xord"], 0, "C", 0, 20, "", ref  mmsg);
        xsot = aa.chktyp(Request.Form["xsot"], 0, "C", 0, 20, "", ref  mmsg);
        mt1 = aa.chktyp(Request.Form["t1"], 0, "C", 0, 50, "", ref  mmsg);


        if (mb1 != "")
        {
            mnam = aa.chktyp(Request.Form["nam"], 0, "C", 0, 20, "群組名稱", ref mmsg);
            msor = aa.chktyp(Request.Form["sor"], 0, "C", 0, 20, "", ref mmsg);
            if (mmsg != null && mmsg != "") mmsg2 = mmsg;
        }
        else msor = "1";


        if (mb1 == "SAV" && mnam!="")
        {
            sqlcom = new SqlCommand();
            sqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
            sqlcom.Parameters.Add("@sot", SqlDbType.Int, 8).Value = msor;
            sqlcom.Parameters.Add("@nam", SqlDbType.NVarChar, 50).Value = mnam;
            sqlcom.Parameters.Add("@crt_usr", SqlDbType.NVarChar, 50).Value = Session["mmnam"];
            sqlcom.Parameters.Add("@crt_uno", SqlDbType.Int, 8).Value = Session["mmuno"];

            if (msno == "0")
            {
                 mSql = "insert into CABASE_group (nam,sot,crt_usr,crt_uno,crt_dat) values (@nam,@sot,@crt_usr,@crt_uno,getdate());  select scope_identity();";
                sqlcom.CommandText = mSql;
                if (!aa.exec_param(sqlcom, ref mmsg))
                {

                    Response.Write(aa.alert_back(mmsg));
                    Response.End();
                }
                else { msno = mmsg; aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]","[" +  Session["mmnam"] + "]", "[動作:新增][資料ID:" + msno + "]",""); }

            }
            else
            {
                mSql = "update CABASE_group set nam=@nam,sot=@sot,upd_Usr=@crt_usr,upd_uno=@crt_uno,upd_dat=getdate() where sno=@sno";
                sqlcom.CommandText = mSql;
                if (!aa.exec_param(sqlcom, ref mmsg))
                {

                    Response.Write(aa.alert_back(mmsg));
                    Response.End();
                }
                else aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]","[" +  Session["mmnam"] + "]", "[動作:修改][資料ID:" + msno + "]","");
            }

            //存主站權限
            sqlcom.Parameters.Clear();
            sqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
            mSql = "delete from CABASE_group_ito where gno=@sno";
            sqlcom.CommandText = mSql;
            if (!aa.exec_param(sqlcom, ref mmsg))
            {
                Response.Write(aa.alert_back(mmsg));
                Response.End();
            }

            mSql = "insert into CABASE_group_ito (gno,ito,ade) select @sno,sno,'' from CABASE_ito";
            sqlcom.CommandText = mSql;
            if (!aa.exec_param(sqlcom, ref mmsg))
            {
                Response.Write(aa.alert_back(mmsg));
                Response.End();
            }

            mSql = "select * from CABASE_group_ito a where a.gno=@sno";
            sqlcom.CommandText = mSql;
            dv1 = aa.dv_param(sqlcom, ref mmsg);
            if (dv1 != null)
            {
                for (int i = 0; i <= dv1.Count - 1; i++)
                {
                    //Response.Write(Trim(Request.Form("ade" & dv1(i)("ito"))) & "<br>")
                    string mtempx = aa.chktyp(Request.Form["ade" + dv1[i]["ito"]], 0, "C", 0, 10, "", ref mmsg);

                    sqlcom_s = new SqlCommand();
                    sqlcom_s.Parameters.Add("@sno", SqlDbType.Int, 8).Value = dv1[i]["sno"];
                    sqlcom_s.Parameters.Add("@ito", SqlDbType.Int, 8).Value = dv1[i]["ito"].ToString();
                    sqlcom_s.Parameters.Add("@ade", SqlDbType.NVarChar, 50).Value = mtempx;
                    sqlcom_s.Parameters.Add("@crt_usr", SqlDbType.NVarChar, 50).Value = Session["mmnam"];
                    sqlcom_s.Parameters.Add("@crt_uno", SqlDbType.Int, 8).Value = Session["mmuno"];
                    mSql = "update CABASE_group_ito set ade=@ade,crt_usr=@crt_usr,crt_uno=@crt_uno,crt_dat=getdate() where sno=@sno and ito=@ito";
                    sqlcom_s.CommandText = mSql;
                    if (!aa.exec_param(sqlcom_s, ref mmsg))
                    {

                        Response.Write(aa.alert_back(mmsg));
                        Response.End();
                    }

                }
                dv1.Dispose();
            }

            
            mmsg2 = "儲存完畢";
            
            //若上層單元是用參數時,如: CABASE_news.aspx?typ=1 時, 請勿用下列URL方式     
            // Response.Redirect(YYurl + "?xord=" + xord + "&xsot=" + xsot + "&mc=" + mc + "&page=" + mpage + "&t1=" + mt1 + "&typ=" + mtyp);

        }

        if (msno != "0" || Int32.Parse(msno) > 0)
        {
            sqlcom = new SqlCommand();
            sqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
            sqlcom.CommandText = "select * from CABASE_group a where a.sno=@sno";

            dv1 = aa.dv_param(sqlcom, ref mmsg);
            if (dv1 != null)
            {
                if (dv1.Count > 0)
                {

                    mnam = aa.chkdbnull(dv1[0]["nam"]);
                    msor = aa.chkdbnull(dv1[0]["sot"]);
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

        sqlcom.CommandText = "select a.*,case when b.ade is null then '' else b.ade end as ade from CABASE_ito a left outer join CABASE_group_ito b on a.sno=b.ito and b.gno=@sno where a.fsno=@ito  order by sot";
        rs1 = aa.dv_param(sqlcom, ref mmsg);
        //rs1 = aa.dv("select a.*,case when b.ade is null then '' else b.ade end as ade from " & mtablename & " a left outer join CABASE_group_ito b on a.sno=b.ito and b.gno=" & msno & " where a." & mfieldfsno & "=" & ito & "  order by " & mfieldsot, mmsg)

        //mcnt = rs1.Count

        if (rs1 != null)
        {
            for (int i = 0; i <= rs1.Count - 1; i++)
            {
                mbb = maa;
                mstr = mstr + @"<li class=""list-group-item node-treeview3"" style="" display: table; width:100%"">";


                for (int ii = 0; ii < mcc; ii++)
                {
                    
                }

                if (mcc == 0) mstr += @" <span class=""icon expand-icon glyphicon glyphicon-minus""></span>";
                else mstr += @"<span class=""indent"" > </span><span class=""indent"" > </span>";
                mstr = mstr + @"<span class=""icon node-icon""></span>";

                //mstr+= @"<input class=""form-control"" type=""text"" size=""1"" name=""sot" + rs1[i]["sno"] + @""" value=""" + rs1[i]["sot"].ToString().Trim() + "\" style=\"min-width:40px\">";


            
                 mstr = mstr + @"<span class=""icon node-icon""></span>" + rs1[i]["nam"].ToString().Trim();


                mstr = mstr + @"<span class=""icon node-icon""></span><span style=""float: right;"">　";
                if (aa.chkdbnull(rs1[i]["fsno"]) == "0") mstr = mstr + @"<input type=""checkbox""  class=""largerCheckbox"" id=""parent" + rs1[i]["sno"] + @""" value=""ON"" onclick=""selAll('" + rs1[i]["fsno"] + @"','" + rs1[i]["sno"] + @"')"">全選　";
                else mstr = mstr + @"<input type=""checkbox""  class=""largerCheckbox"" id=""self" + rs1[i]["sno"] + @""" value=""ON"" onclick=""selAll('" + rs1[i]["fsno"] + @"','" + rs1[i]["sno"] + @"')"">全選　";

                //mstr = mstr + Trim(rs1(i)("ade"))
                //以下呈現編輯功能
                //mstr=mstr + "...[<span style=""curs1(i)or:hand"" onclick=""javascript:form2.mode.value='A';form2.fsno.value='" + rs1(i)(mfieldsno) + "';form2.submit();""><font color=""#0000FF"">新增</font></span>][<span style=""curs1(i)or:hand"" onclick=""javascript:form2.mode.value='M';form2.sno.value='" + rs1(i)(mfieldsno) + "';form2.submit();""><font color=""#0000FF"">修改</font></span>][<span style=""curs1(i)or:hand"" onclick=""javascript:if (confirm('確定刪除" + trim(rs1(i)(mfieldweb)) + "嗎?')) {form2.mode.value='D';form2.sno.value='" + rs1(i)(mfieldsno) + "';form2.submit();}""><font color=""#0000FF"">刪除</font></span>]"
                if (rs1[i]["ade"].ToString().Trim().IndexOf("B") >= 0) mstr = mstr + @"<input type=""checkbox""  class=""largerCheckbox parent" + rs1[i]["fsno"] + @" self" + rs1[i]["sno"] + @""" value=""B""  name=""ade" + rs1[i]["sno"] + @""" checked>瀏覽　";
                else mstr = mstr + @"<input type=""checkbox"" class=""largerCheckbox parent" + rs1[i]["fsno"] + @" self" + rs1[i]["sno"] + @""" value=""B"" name=""ade" + rs1[i]["sno"] + @""">瀏覽　";
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