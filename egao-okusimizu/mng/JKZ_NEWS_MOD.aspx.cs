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
    public string mselA="",munt= "NEWS", mmem="", monf="0",mtpc="",mtpcs="",mlnk_tgt="",mlnk="",mimg="",nsql="";
    public string mcrt_usr, mcrt_dat, mupd_usr, mupd_dat, mupd_usr2, mupd_dat2, mupd_usr3, mupd_dat3;
    public string mtotal = "0";
    public DataView dv1,dv2;
    public int mpage, mc;
    public SqlCommand msqlcom = new SqlCommand();

  protected void Page_Load(object sender, EventArgs e)
  {
    init_class(); //初始化class含db connection, email環境設定
    if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));
    msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref mmsg); if (msno == "0") msno = aa.chktyp(Request.QueryString["sno"], 0, "I", 0, 0, "", ref mmsg);
    if (!chkath()) { mng_main.Visible = false; }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
    //以下開始coding...

    mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 5, "", ref mmsg);
    mpage = Int32.Parse(aa.chktyp(Request.Form["page"], 1, "I", 0, 0, "", ref mmsg));
    mc = Int32.Parse(aa.chktyp(Request.Form["mc"], 1, "I", 0, 0, "", ref mmsg));
    xord = aa.chktyp(Request.Form["xord"], 0, "C", 0, 20, "", ref mmsg);
    xsot = aa.chktyp(Request.Form["xsot"], 0, "C", 0, 20, "", ref mmsg);
    mt1 = aa.chktyp(Request.Form["t1"], 0, "C", 0, 50, "", ref mmsg);
    msno = aa.chktyp(Request.QueryString["sno"], 0, "I", 0, 0, "", ref mmsg);
    if (msno == "0") msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref mmsg);

        if (mb1 == "SAV" || mb1 == "CMSA" )
    {
      msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref mmsg);
      mtyp = aa.chktyp(Request.Form["typ"], 1, "I", 0, 20, "分類", ref mmsg);
      mtpc = aa.chktyp(Request.Form["tpc"], 1, "C", 0, 50, "標題", ref mmsg);
            mtpcs = aa.chktyp(Request.Form["tpcs"], 0, "C", 0, 50, "次標題", ref mmsg);
            mlnk = aa.chktyp(Request.Form["lnk"], 0, "C", 0, 250, "連結", ref mmsg);
      mlnk_tgt = aa.chktyp(Request.Form["lnk_tgt"], 0, "C", 0, 20, "", ref mmsg);
      monf = aa.chktyp(Request.Form["onf"], 0, "I", 0, 0, "上架", ref mmsg);
      msdt = aa.chktyp(Request.Form["sdt"], 0, "D", 0, 10, "有效起日", ref mmsg);
      medt = aa.chktyp(Request.Form["edt"], 0, "D", 0, 10, "有效止日", ref mmsg);
      mmem = aa.chktyp(Request.Form["mem"], 0, "BHTML", 0, 0, "內文說明", ref mmsg);
            
      if (mmsg.Length == 0)
      {
        string[] mfiles = { };
        string[] msizes = { };

        aa.UPLOAD("file1", 250000, "", true, true, "/upload/banner/", ref mfiles, ref msizes, ref mmsg);

        if (mmsg != "") mmsg2 = mmsg;
        
        for (int i = 0; i < mfiles.Length; i++)
        {
                    Response.Write(mfiles[i]);
            switch (i)
            {
                case 0:
                    mimg = mfiles[i];
                    if (mimg.Length > 0) nsql = ",img='" + mfiles[i] + "'";
                    break;
                        
            }
        }
        msqlcom.Parameters.Add("@tpc", SqlDbType.NVarChar, 50).Value = mtpc;
                msqlcom.Parameters.Add("@tpcs", SqlDbType.NVarChar, 50).Value = mtpcs;
                msqlcom.Parameters.Add("@lnk", SqlDbType.NVarChar, 250).Value = mlnk;
        msqlcom.Parameters.Add("@img", SqlDbType.NVarChar, 250).Value = mimg;
        msqlcom.Parameters.Add("@lnk_tgt", SqlDbType.NVarChar, 20).Value = mlnk_tgt;
        msqlcom.Parameters.Add("@typ", SqlDbType.Int, 10).Value = mtyp;
        msqlcom.Parameters.Add("@mem", SqlDbType.NVarChar, -1).Value = mmem;
        msqlcom.Parameters.Add("@onf", SqlDbType.Int, 10).Value = monf;
        if (aa.IsDate(msdt)) msqlcom.Parameters.Add("@sdt", SqlDbType.DateTime, 10).Value = msdt;
        else msqlcom.Parameters.Add("@sdt", SqlDbType.DateTime, 10).Value = DBNull.Value;
        if (aa.IsDate(medt)) msqlcom.Parameters.Add("@edt", SqlDbType.DateTime, 10).Value = medt;
        else msqlcom.Parameters.Add("@edt", SqlDbType.DateTime, 10).Value = DBNull.Value;
        msqlcom.Parameters.Add("@sno", SqlDbType.Int, 10).Value = msno;
        //msqlcom.Parameters.Add("@pnt_typ", SqlDbType.NVarChar, 10).Value = mpnt_typ;
        msqlcom.Parameters.Add("@crt_uno", SqlDbType.Int, 10).Value = Session["mmuno"];
        msqlcom.Parameters.Add("@crt_usr", SqlDbType.NVarChar, 50).Value = Session["mmnam"];

        if (msno == "0")
        {

            mSql = "insert into CA20_NEWS (img,typ,tpc,tpcs,sdt,edt,mem,onf,lnk,lnk_tgt,crt_usr,crt_uno,crt_dat)";
            mSql = mSql + " values (@img,@typ,@tpc,@tpcs,@sdt,@edt,@mem,@onf,@lnk,@lnk_tgt,@crt_usr,@crt_uno,getdate()); select scope_identity();";

            msqlcom.CommandText = mSql;
          if (!aa.exec_param(msqlcom, ref mmsg))
          {

            Response.Write(aa.alert_back(mmsg));
            Response.End();
          }
          else
          {
            msno = mmsg;
            aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:新增][資料ID:" + msno + "]", "");
          }
        }
        else
        {

            mSql = "update CA20_NEWS set tpcs=@tpcs, typ=@typ,onf=@onf,tpc=@tpc,sdt=@sdt,edt=@edt,mem=@mem,lnk=@lnk,lnk_tgt=@lnk_tgt,upd_Usr=@crt_usr,upd_uno=@crt_uno,upd_dat=getdate()"+nsql+" where sno=@sno";
            msqlcom.CommandText = mSql;
            if (!aa.exec_param(msqlcom, ref mmsg))
            {

                Response.Write(aa.alert_back("ERROR3:" + mmsg + mSql));
                Response.End();
            }
            else
            {

                        if (!aa.cms_save(this, "新聞", "", int.Parse(msno), 1, Server.MapPath("../upload/cms"), "upload/cms/", ref mmsg, false))
                        { mmsg2 = "CMS儲存失敗2" + mmsg; }
                        aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:修改][資料" + mtpc + ",ID:" + msno + "]", "");
            }
        }
                mmsg2 += "儲存完畢";
                //若上層單元是用參數時,如: CA2019_ROLE.aspx?typ=1 時, 請勿用下列URL方式  
                //if (mmpara.Length>0) { Response.Redirect(YYurl + "&xord=" + xord + "&xsot=" + xsot + "&mc=" + mc + "&page=" + mpage + "&t1=" + mt1 + "&typ=" + mtyp);}
                //else { Response.Redirect(YYurl + "?xord=" + xord + "&xsot=" + xsot + "&mc=" + mc + "&page=" + mpage + "&t1=" + mt1 + "&typ=" + mtyp);}

            }
            else mmsg2 += mmsg; //b1=SAV

    }

        if (mb1 == "DEL1")
        {
            msqlcom.Parameters.Clear();
            msqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
            msqlcom.CommandText = "update CA20_NEWS set img=null where sno=@sno";
            aa.exec_param(msqlcom, ref mmsg);
        }

        //讀出內容

        if (msno!="0")
    {
      msqlcom.Parameters.Clear();
      msqlcom.Parameters.Add("@sno", SqlDbType.Int, 8).Value = msno;
      msqlcom.CommandText = "select * from CA20_NEWS a where a.sno=@sno";
      dv1 = aa.dv_param(msqlcom, ref mmsg);
      if (dv1 != null)
      {
        if (dv1.Count > 0)
        {

            msdt = aa.showdate(aa.chkdbnull(dv1[0]["sdt"]).Trim(),"yyyy-MM-dd");
            medt = aa.showdate(aa.chkdbnull(dv1[0]["edt"]).Trim(),"yyyy-MM-dd");
                    mtpcs = aa.chkdbnull(dv1[0]["tpcs"]).Trim();
                    mtpc = aa.chkdbnull(dv1[0]["tpc"]).Trim();
            mtyp = aa.chkdbnull(dv1[0]["typ"]);
            monf= aa.chkdbnull(dv1[0]["onf"]);
                    mimg = aa.chkdbnull(dv1[0]["img"]);
                    mmem = aa.chkdbnull(dv1[0]["mem"]);
            mlnk = aa.chkdbnull(dv1[0]["lnk"]);
            mlnk_tgt = aa.chkdbnull(dv1[0]["lnk_tgt"]);
            mcrt_usr = aa.chkdbnull(dv1[0]["crt_usr"]);
            mcrt_dat = aa.chkdbnull(dv1[0]["crt_dat"]);
            mupd_usr = aa.chkdbnull(dv1[0]["upd_usr"]);
            mupd_dat = aa.chkdbnull(dv1[0]["upd_dat"]);

                    
        }
        dv1.Dispose();

      }
      
    }
        msqlcom.Parameters.Clear();
        msqlcom.CommandText = "select * from ca20_news_typ ";
        dv1 = aa.dv_param(msqlcom, ref mmsg);
        if (dv1 != null)
        {
            for (int i = 0; i < dv1.Count; i++)
            {
                if (mtyp == aa.chkdbnull(dv1[i]["sno"])) mselA += "<option value=\"" + aa.chkdbnull(dv1[i]["sno"]) + "\" selected>" + aa.chkdbnull(dv1[i]["nam"]) + "</option>";
                else mselA += "<option value=\"" + aa.chkdbnull(dv1[i]["sno"]) + "\">" + aa.chkdbnull(dv1[i]["nam"]) + "</option>";
            }
            dv1.Dispose();
        }

    
    }


} // sno>0




