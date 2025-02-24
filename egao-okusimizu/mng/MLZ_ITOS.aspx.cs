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
    public string mfsno, mmode, mweb, msot, mweb_e, murl, murl2, munt_loc;
    public string mcrt_usr, mcrt_dat, mupd_usr, mupd_dat, mupd_usr2, mupd_dat2, mupd_usr3, mupd_dat3;
    public string monf, mbno;
    public string xsdt, xedt;
    public DataView dv1,dv2;
    public int mpage, mc;
    public SqlCommand msqlcom = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));   
        if (!chkath()) { mng_main.Visible = false; }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...

        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 5, "", ref mmsg);


        if (mb1 == "") mfsno = aa.chktyp(Request.QueryString["fsno"], 0, "I", 0, 0, "", ref mmsg);
        else mfsno = aa.chktyp(Request.Form["fsno"], 0, "I", 0, 0, "", ref mmsg);


        if (mfsno == "0") mmsg2 = "LAYER錯誤";

        whr = " and sno not in (select fsno from CABASE_ito where sno=@mfsno)";
        if (mb1 == "SAV" && mmsg.Length == 0)
        {
            msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "層級編號", ref mmsg);
            if (mmsg.Length == 0)
            {

                msqlcom.Parameters.Add("@msno", SqlDbType.Int, 10).Value = msno;
                msqlcom.Parameters.Add("@mfsno", SqlDbType.Int, 10).Value = mfsno;
                msqlcom.CommandText = "update CABASE_ito set fsno=@msno where sno=@mfsno";
                if (aa.exec_param(msqlcom, ref mmsg))
                {
                    Response.Write(aa.alert_script("alert('設定成功!');window.top.closeme();window.top.location.reload();"));
                    aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:修改][資料ID:" + msno + "]", "單元層級");
                    Response.End();
                }
                else
                {
                    Response.Write(aa.alert_back("ERROR"));

                    Response.End();
                }
            }
            else
            {
                Response.Write(aa.alert_back(mmsg));

                Response.End();
            }



        }
        msqlcom.Parameters.Clear();
        msqlcom.Parameters.Add("@mfsno", SqlDbType.Int, 10).Value = mfsno;
        msqlcom.CommandText = "select sno from CABASE_ito a where fsno=0 " + whr + " order by sot";


    }
    protected string menu1(int ito, int mcc, int mcnt, string maa, int msts, int msts2)  //mcnt,本層筆數,msts父層筆數,msts2=目前的父層為第几筆
    {
        int j = 1;
        string mstr, mbb, mmsg;
        DataView rs1;
        mstr = "";
        mmsg = "";
        mbb = "";
        msqlcom.Parameters.Clear();
        msqlcom.Parameters.Add("@mfsno", SqlDbType.Int, 10).Value = mfsno;
        msqlcom.Parameters.Add("@mito", SqlDbType.Int, 10).Value = ito;
        msqlcom.CommandText = "select* from CABASE_ito a where fsno = @mito" + whr + " order by sot";
        rs1 = aa.dv_param(msqlcom, ref mmsg);

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

                //mstr = mstr & "<td  colspan=""" & 10 - mcc & """ style=""border-bottom-style:dashed; border-bottom-color:#666600; border-bottom-width:1px; "">　</td>";
                //mstr = mstr + @"</td><td colspan=""" + (7 - mcc) + @""" style=""background-image:url(theme/images/dots_hr.gif); background-repeat:repeat-x""> </td>";
                //以下呈現編輯功能
                mstr = mstr + @"<input type=""button"" class="""" onclick=""form2.sno.value='" + rs1[i]["sno"] + @"';form2.b1.value='SAV';form2.submit();"" value=""這裡"" />";



                mstr = mstr + "" + "</span></li>";
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
        return mstr;
    }
}