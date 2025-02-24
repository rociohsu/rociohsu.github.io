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
    public string mfsno, mnam = "", monf = "0", mmode = "",msot="0";

    public DataView dv1,rs1;
    public int mpage, mc;
    public SqlCommand msqlcom = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));   
        if (!chkath()) { mng_main.Visible = false; }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...


        mmsg = "";


        mmode = aa.chktyp(Request.Form["mode"], 0, "C", 0, 3, "", ref mmsg);
        msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref mmsg);
        mnam = aa.chktyp(Request.Form["nam"], 0, "C", 0, 100, "", ref mmsg);
        


        msot = aa.chktyp(Request.Form["sot"], 0, "I", 0, 500, "", ref mmsg);
        mfsno = aa.chktyp(Request.Form["fsno"], 0, "I", 0, 0, "", ref mmsg);

        msqlcom.Parameters.Add("@sno", SqlDbType.Int, 10).Value = msno;
        msqlcom.Parameters.Add("@fsno", SqlDbType.Int, 10).Value = mfsno;
        msqlcom.Parameters.Add("@nam", SqlDbType.NVarChar, 20).Value = mnam;
        msqlcom.Parameters.Add("@sot", SqlDbType.Int, 10).Value = mc;
        msqlcom.Parameters.Add("@crt_usr", SqlDbType.NVarChar, 50).Value = Session["mmnam"];
        msqlcom.Parameters.Add("@crt_uno", SqlDbType.Int, 8).Value = Session["mmuno"];

        switch (mmode)
        {
            case "A":
                mmode = "A2";
                break;
            case "A2":
                msqlcom.CommandText = "select * from CA20_NEWS_TYP a where fsno=@fsno";
                dv1 = aa.dv_param(msqlcom, ref mmsg);
                if (dv1 != null) mc = dv1.Count + 1;
                else mc = 1;

                mSql = "insert into CA20_NEWS_TYP (nam,fsno,sot,crt_uno,crt_usr) ";
                mSql = mSql + " values (@nam,@fsno,@sot,@crt_uno,@crt_usr); select scope_identity();";
                msqlcom.CommandText = mSql;
                if (!aa.exec_param(msqlcom, ref mmsg))
                {

                    Response.Write(aa.alert_back(mmsg));
                    Response.End();
               }
                else { msno = mmsg; aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]","[" +  Session["mmnam"] + "]", "[動作:新增][資料ID:" + msno + "]",""); }

                mmsg2 = "儲存完畢";
                msno = "0";
                mmode = "";
                mfsno = "0";
                break;
            case "M":
                mmode = "M2";

                msqlcom.CommandText = "select * from CA20_NEWS_TYP where sno=@sno";
                rs1 = aa.dv_param(msqlcom, ref mmsg);
                if (rs1 != null)
                {
                    if (rs1.Count > 0)
                    {
                        mnam = aa.chkdbnull(rs1[0]["nam"]);
                        msot = aa.chkdbnull(rs1[0]["sot"]);
                        
                        //rs(mfieldfsno)=clng(mfsno);

                    }
                    rs1.Dispose();
                }
                break;


            case "M2":

                mSql = "update CA20_NEWS_TYP set nam=@nam where sno=@sno";
                msqlcom.CommandText = mSql;
                if (!aa.exec_param(msqlcom, ref mmsg))
                {

                    Response.Write(aa.alert_back(mmsg + mSql));
                    Response.End();
                }
                else { aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]","[" +  Session["mmnam"] + "]", "[動作:修改][資料ID:" + msno + "]",""); }
                mmsg2 = "儲存完畢";

                msno = "0";
                mmode = "";
                mfsno = "0";

                break;

            case "D":
                del(Int32.Parse(msno));
                //msqlcom.Parameters.Add("@sno", SqlDbType.Int, 10).Value = msno;
                msqlcom.CommandText= "delete from CA20_NEWS_TYP where sno=@sno";
                if (!aa.exec_param(msqlcom, ref mmsg))
                {

                    Response.Write(aa.alert_back(mmsg));
                    Response.End();
                }
                else aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]","[" +  Session["mmnam"] + "]", "[動作:刪除][資料ID:" + msno + "]","");
                mfsno = "0";
                mmode = "";
                msno = "0";
                break;
            case "SO":
                savsot(0);
                mmode = "";
                msno = "0";
                mfsno = "0";
                mmsg2 = "儲存完畢";
                aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]","[" +  Session["mmnam"] + "]", "[動作:儲存排序]", "");
                break;
            default:
                mmode = "";
                msno = "0";
                mfsno = "0";
                break;

        }


    }

    protected void del(int ito)
    {

        DataView dv1;
        string mmsg = "";
        using (System.Data.SqlClient.SqlCommand mndm = new System.Data.SqlClient.SqlCommand())
        {
            mndm.Parameters.Add("@mito", SqlDbType.Int, 8).Value = ito;
            mndm.CommandText = "select * from CA20_NEWS_TYP where fsno=@mito";
            dv1 = aa.dv_param(mndm, ref mmsg);
            if (dv1 != null)
            {
                for (int i = 0; i <= dv1.Count - 1; i++)
                {
                    del(Int32.Parse(dv1[i]["sno"].ToString()));
                    mndm.CommandText = "delete from CA20_NEWS_TYP where fsno=@mito";
                    if (!aa.exec_param(mndm, ref mmsg))
                    {

                        Response.Write(aa.alert_back(mmsg));
                        Response.End();
                    }
                }


                dv1.Dispose();
            }
        }

    }

    protected void savsot(int xfsno)
    {

        DataView dv1;
        string mmsg = "";
        using (System.Data.SqlClient.SqlCommand mndm = new System.Data.SqlClient.SqlCommand())
        {
            mndm.Parameters.Add("@fsno", SqlDbType.Int, 8).Value = xfsno;
            mndm.CommandText = "select sno,sot from CA20_NEWS_TYP a where fsno=@fsno order by sot";
            dv1 = aa.dv_param(mndm, ref mmsg);
            if (dv1 != null)
            {
                for (int i = 0; i <= dv1.Count - 1; i++)
                {
                    savsot(Int32.Parse(dv1[i]["sno"].ToString()));
                    mndm.Parameters.Clear();
                    mndm.Parameters.Add("@msno", SqlDbType.Int, 8).Value = dv1[i]["sno"];
                    string gsot = aa.chktyp(Request.Form["sot" + dv1[i]["sno"]], 0, "I", 0, 12, "", ref mmsg);
                    if (gsot== "0") mndm.Parameters.Add("@msot", SqlDbType.Int, 8).Value = i + 1;
                    else mndm.Parameters.Add("@msot", SqlDbType.Int, 8).Value = gsot;
                    //mndm.Parameters.Add("@munt_loc", SqlDbType.Int, 8).Value = aa.chktyp(Request.Form["unt_loc" + dv1[i]["sno"]], 0, "I", 0, 12, "", ref mmsg);
                    mndm.CommandText = "update CA20_NEWS_TYP set sot=@msot where sno=@msno";
                    if (!aa.exec_param(mndm, ref mmsg))
                    {
                        
                        Response.Write(aa.alert_back(mmsg));
                        Response.End();
                    } else resort(xfsno, Int32.Parse(dv1[i]["sno"].ToString()), int.Parse(gsot));
            }
                dv1.Dispose();

            }
        }

    }

    protected void resort(int fsno,int sno,int sot)
    {
        using (SqlCommand msql = new SqlCommand("select * from CA20_NEWS_TYP where fsno=@fsno and sno<>@sno and sot>=@sot order by sot"))
        {
            msql.Parameters.Add("@fsno", SqlDbType.Int, 8).Value = fsno;
            msql.Parameters.Add("@sno", SqlDbType.Int, 8).Value = sno;
            msql.Parameters.Add("@sot", SqlDbType.Int, 8).Value = sot;
            dv1 = aa.dv_param(msql, ref mmsg);
            if (dv1 != null)
            {
                for (int i = 0; i < dv1.Count; i++)
                {
                   
                    int xsot = int.Parse(aa.chkdbnull(dv1[i]["sot"])) + 1;
                    msql.Parameters.Clear();
                    msql.CommandText = "update CA20_NEWS_TYP set sot=@sot where sno=@sno";
                    msql.Parameters.Add("@sot", SqlDbType.Int, 8).Value = xsot;
                    msql.Parameters.Add("@sno", SqlDbType.Int, 8).Value = aa.chkdbnull(dv1[i]["sno"]);
                    
                    if (!aa.exec_param(msql, ref mmsg)) Response.Write("error2"+mmsg);
                }
                dv1.Dispose();

            }
            else Response.Write("error");
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

        using (System.Data.SqlClient.SqlCommand sqlcom = new System.Data.SqlClient.SqlCommand())
        {
            //sqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
            sqlcom.Parameters.Add("@ito", SqlDbType.Int, 8).Value = ito;

            sqlcom.CommandText = "select a.sno,a.nam,a.sot,a.cnt,0 as cnt2 from CA20_NEWS_TYP(nolock) a where a.fsno=@ito group by a.sno,a.nam,a.sot,a.cnt order by a.sot";
            rs1 = aa.dv_param(sqlcom, ref mmsg);


            if (rs1 != null)
            {
                for (int i = 0; i <= rs1.Count - 1; i++)
                {
                    mbb = maa;
                    mstr = mstr + @"<li class=""list-group-item node-treeview-selectable""  style ="" display: table; width:100%"">";
                    

                    for (int ii = 0; ii < mcc; ii++)
                    {
                        mstr += @"<span class=""indent"" class=""hidden-sm-down  not-visible""></span>";
                    }

                    if (mcc==0) mstr += @" <span class=""icon expand-icon glyphicon glyphicon-minus""></span>";

                    mstr = mstr + @"<span class=""icon node-icon""></span>";

                    //mstr+= @"<input class=""form-control"" type=""text"" size=""1"" name=""sot" + rs1[i]["sno"] + @""" value=""" + rs1[i]["sot"].ToString().Trim() + "\" style=\"min-width:40px\">";


                    
                     mstr = mstr + @"<span class=""icon node-icon""></span>" + rs1[i]["nam"].ToString().Trim() + "<span class=\"text-primary\">　(" + rs1[i]["cnt2"].ToString().Trim() + ")</span>"; 


                   // if (rs1[i]["unt_loc"].ToString() == "1") mstr = mstr + "***";

                    //mstr = mstr & "<td  colspan=""" & 10 - mcc & """ style=""border-bottom-style:dashed; border-bottom-color:#666600; border-bottom-width:1px; "">　</td>";
                    mstr = mstr + @"<span style=""    float: right;"">";

                    //以下呈現編輯功能
                    //if (XXauth.IndexOf("A") >= 0) mstr = mstr + @"<span class=""icon node-icon""></span><input type=""button"" class=""btn btn-sm btn-default"" onclick=""form2.mode.value='A';form2.fsno.value='" + rs1[i]["sno"] + @"';form2.submit();"" value=""增"">";

                    if (XXauth.IndexOf("E") >= 0) mstr = mstr + @"<span class=""icon node-icon""></span><input type=""button"" class=""btn btn-sm btn-default"" onclick=""form2.mode.value='M';form2.sno.value='" + rs1[i]["sno"] + @"';form2.submit();"" value=""修"">";

                    if (XXauth.IndexOf("D") >= 0) mstr = mstr + @"<span class=""icon node-icon""></span><input type=""button"" class=""btn btn-sm btn-danger"" onclick=""if (confirm('確定刪除" + rs1[i]["nam"].ToString() + @"嗎?')) {form2.mode.value='D';form2.sno.value='" + rs1[i]["sno"] + @"';form2.submit();}"" value=""刪"">";

                    //mstr = mstr + @"<span class=""icon node-icon""></span><a class=""fancybox fancybox.iframe btn btn-default  btn-sm"" href=""JKZ_PDTTYP.aspx?fsno=" + rs1[i]["sno"] + "&rnd=" + aa.generatevcode(10) + @""">層級</a>";
                    //If j > 1 And CLng(rs1(i)(mfieldsot)) <= mcnt Then '目前第二筆以上,而且順序2以上
                    //    'mstr = mstr & "<font color=""#AAAAAA"">[<span style=""cursor:hand"" onclick=""form2.mode.value='UP';form2.sno.value='" & rs1(i)(mfieldsno) & "';form2.submit();""><font color=""#AA0000"">△</font></span>]</font>"
                    //Else
                    //    'mstr = mstr & "<font color=""#AAAAAA"">[　]</font>"
                    //End If

                    //If j >= 1 And CLng(rs1(i)(mfieldsot)) < mcnt Then  '不是最後一筆,而且
                    //   'mstr = mstr & "<font color=""#AAAAAA"">[<span style=""cursor:hand"" onclick=""form2.mode.value='DN';form2.sno.value='" & rs1(i)(mfieldsno) & "';form2.submit();""><font color=""#00AA00"">▽</font></span>]</font>"
                    //Else
                    //    ' mstr = mstr & "<font color=""#AAAAAA"">[　]</font>"
                    //End If

                    //if (aa.chkdbnull(rs1[i]["unt_loc"]) == "1") mstr += @"<input type=""checkbox"" name=""unt_loc" + rs1[i]["sno"] + @""" value=""1"" checked>";
                    //else mstr += @"<span class=""icon node-icon""></span><input type=""checkbox""   name = ""unt_loc" + rs1[i]["sno"] + @""" value = ""1"" >";
                    //mstr += "<span style=\"font-size:9pt\">停用</span>";
                    for (int ii = 0; ii < 2-mcc; ii++)
                    {
                        //mstr += @"<span class=""indent""></span>";
                    }
                    mstr += @"<span class=""icon node-icon""></span><input type=""text"" size=""1"" name=""sot" + rs1[i]["sno"] + @""" value=""" + rs1[i]["sot"].ToString().Trim() + "\" style=\"min-width:40px\">";

                    mstr = mstr + "</span>";

                    for (int ii = 0; ii < 2 - mcc; ii++)
                    {
                    //   mstr += @"<span class=""icon node-icon""></span>";
                    }

                    mstr +="</li>";

                    mstr = mstr + menu1(Int32.Parse(rs1[i]["sno"].ToString()), mcc + 1, Int32.Parse(rs1[i]["cnt"].ToString()), mbb, rs1.Count, j);

                    j = j + 1;

                }
                rs1.Dispose();
            }
            else
            {
                Response.Write(aa.alert(mmsg));
                Response.End();
            }
        }
        return mstr;
    }
    
}