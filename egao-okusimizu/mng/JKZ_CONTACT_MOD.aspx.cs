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

using System.Drawing.Imaging;

public partial class _default : MLBasePage
{
    public string mselA="",mmem="",mnam="",mcnam="",madr_code,mara,mcty,madr="",mtel="",mfax="",memail="",msts="0",mrtn_typ="0",mrtn_mem="";
　　public string nsql = "";
    public string mcrt_usr, mcrt_dat, mupd_usr, mupd_dat, mupd_usr2, mupd_dat2, mupd_usr3, mupd_dat3;
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

    mpage = Int32.Parse(aa.chktyp(Request.Form["page"], 1, "I", 0, 0, "", ref  mmsg));
    mc = Int32.Parse(aa.chktyp(Request.Form["mc"], 1, "I", 0, 0, "", ref  mmsg));
        
    xord = aa.chktyp(Request.Form["xord"], 0, "C", 0, 20, "", ref  mmsg);
    xsot = aa.chktyp(Request.Form["xsot"], 0, "C", 0, 20, "", ref  mmsg);
    mt1 = aa.chktyp(Request.Form["t1"], 0, "C", 0, 50, "", ref  mmsg);


    if (mb1 == "EML")
    {


    }
        
    if (mb1 == "SAV" || mb1=="EML" )
    {
      msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref mmsg);
      mtyp = aa.chktyp(Request.Form["typ"], 0, "I", 0, 50, "類型", ref mmsg);
      memail = aa.chktyp(Request.Form["email"], 0, "C", 0, 250, "信箱", ref mmsg);
      mtel = aa.chktyp(Request.Form["tel"], 0, "C", 0, 250, "電話", ref mmsg);
      mfax = aa.chktyp(Request.Form["fax"], 0, "C", 0, 250, "傳真", ref mmsg);
      madr_code = aa.chktyp(Request.Form["adr_code"], 0, "C", 0, 20, "區號", ref mmsg);
      mcty = aa.chktyp(Request.Form["cty"], 0, "C", 0, 250, "縣市", ref mmsg);
      mara = aa.chktyp(Request.Form["ara"], 0, "C", 0, 250, "地區", ref mmsg);
      madr = aa.chktyp(Request.Form["adr"], 0, "C", 0, 250, "地址", ref mmsg);

      msts = aa.chktyp(Request.Form["sts"], 0, "I", 0, 250, "狀態", ref mmsg);
      mmem = aa.chktyp(Request.Form["mem"], 0, "BHTML", 0, 0, "留言內容", ref mmsg);
      mrtn_mem = aa.chktyp(Request.Form["rtn_mem"], 0, "BHTML", 0, 0, "回覆內容", ref mmsg);
      mrtn_typ = aa.chktyp(Request.Form["rtn_typ"], 0, "I", 0, 50, "回覆方式", ref mmsg);


      if (mmsg.Length == 0)
      {
        msqlcom.Parameters.Add("@msno", SqlDbType.Int, 50).Value = msno;
        if (mmsg.Length == 0)
        {
          msqlcom.Parameters.Add("@typ", SqlDbType.Int, 10).Value = mtyp;
          msqlcom.Parameters.Add("@sts", SqlDbType.Int, 10).Value = msts;
          msqlcom.Parameters.Add("@mrtn_typ", SqlDbType.Int, 10).Value = mrtn_typ;
          msqlcom.Parameters.Add("@adr_code", SqlDbType.NVarChar, 250).Value = madr_code;
          msqlcom.Parameters.Add("@tel", SqlDbType.NVarChar, 200).Value = aa.encrypt(mtel);
          msqlcom.Parameters.Add("@email", SqlDbType.NVarChar, 200).Value = aa.encrypt(memail);
          msqlcom.Parameters.Add("@fax", SqlDbType.NVarChar, 200).Value = aa.encrypt(mfax);
          msqlcom.Parameters.Add("@cty", SqlDbType.NVarChar, 200).Value = mcty;
          msqlcom.Parameters.Add("@ara", SqlDbType.NVarChar, 200).Value = mara;
          msqlcom.Parameters.Add("@adr", SqlDbType.NVarChar, 200).Value = aa.encrypt(madr);
            msqlcom.Parameters.Add("@nam", SqlDbType.NVarChar, 200).Value = aa.encrypt(mnam);
            msqlcom.Parameters.Add("@cnam", SqlDbType.NVarChar, 200).Value = aa.encrypt(mcnam);
            msqlcom.Parameters.Add("@mem", SqlDbType.NVarChar, -1).Value = mmem;
          msqlcom.Parameters.Add("@rtn_mem", SqlDbType.NVarChar, -1).Value = mrtn_mem;
          msqlcom.Parameters.Add("@rtn_typ", SqlDbType.Int,10).Value = mrtn_typ;
          msqlcom.Parameters.Add("@crt_uno", SqlDbType.Int, 10).Value = Session["mmuno"];
          msqlcom.Parameters.Add("@crt_usr", SqlDbType.NVarChar, 50).Value = Session["mmnam"];
                        
          if (msno == "0")
          {
                           
            mSql = "insert into CA20_FORM (nam,cnam,adr_code,cty,ara,adr,tel,fax,email,typ,mem,sts,rtn_typ,rtn_mem,crt_usr,crt_uno,crt_dat)";
            mSql = mSql + " values (@nam,@cnam,@adr_code,@cty,@ara,@adr,@tel,@fax,@email,@typ,@mem,@sts,@rtn_typ,@rtn_mem,@crt_usr,@crt_uno,getdate()); select scope_identity();";

            msqlcom.CommandText = mSql;
            if (!aa.exec_param(msqlcom, ref mmsg))
            {
                                
                Response.Write(aa.alert_back("ERROR2:" + mmsg));
                Response.End();
            }
            else
            {
                                
                msno = mmsg;
                              
                aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:新增][資料:ID:" + msno + "]", "");
            }
          }
          else
          {
                            
              mSql = "update ca20_form set nam=@nam,cnam=@cnam,adr_code=@adr_code,cty=@cty,ara=@ara,adr=@adr,tel=@tel,fax=@fax,email=@email,typ=@typ,mem=@mem,sts=@sts,rtn_typ=@rtn_typ,rtn_mem=@rtn_mem,upd_Usr=@crt_usr,upd_uno=@crt_uno,upd_dat=getdate() where sno=@msno";
              msqlcom.CommandText = mSql;
              if (!aa.exec_param(msqlcom, ref mmsg))
              {

                  Response.Write(aa.alert_back("ERROR3:" + mmsg + mSql));
                  Response.End();
              }
              else
              {
                  
                  aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:修改][資料ID:" + msno + "]", "");
              }
          }




          if (mb1 == "EML")
          {

            string xbody = mrtn_mem;
            if (aa.sendemail(aa.xxemail, memail, "", "", aa.xxemail, "聯絡我們回覆信", xbody,1, 3, "") == "OK")
            {
              mmsg2 = "寄送成功";
            }
            else mmsg2 = "寄送失敗";
          }
          else mmsg2 = "儲存完畢";
                 
        }
        else
        {
            mmsg2 = mmsg;
        }
      }
      else mmsg2 = mmsg;

    } //b1=SAV




    //讀出內容

    if (Int32.Parse(msno) > 0)
    {
      msqlcom.Parameters.Clear();
      msqlcom.Parameters.Add("@msno", SqlDbType.Int, 8).Value = msno;

      msqlcom.CommandText = "select a.* from CA20_FORM a where a.sno=@msno";
      dv1 = aa.dv_param(msqlcom, ref mmsg);
      if (dv1 != null)
      {
        if (dv1.Count > 0)
        {
                    mtyp = aa.decrypt(aa.chkdbnull(dv1[0]["typ"]));
                    mnam = aa.decrypt(aa.chkdbnull(dv1[0]["nam"]));
          mcnam = aa.decrypt(aa.chkdbnull(dv1[0]["cnam"]));
          mtel = aa.decrypt(aa.chkdbnull(dv1[0]["tel"]));
          mfax = aa.decrypt(aa.chkdbnull(dv1[0]["fax"]));
            madr = aa.decrypt(aa.chkdbnull(dv1[0]["adr"]));
            mara = aa.chkdbnull(dv1[0]["ara"]);
            mcty = aa.chkdbnull(dv1[0]["cty"]);
            madr_code = aa.chkdbnull(dv1[0]["adr_code"]);

            mcrt_usr = aa.chkdbnull(dv1[0]["crt_usr"]);
          mcrt_dat = aa.chkdbnull(dv1[0]["crt_dat"]);
          mupd_usr = aa.chkdbnull(dv1[0]["upd_usr"]);
          mupd_dat = aa.chkdbnull(dv1[0]["upd_dat"]);
          mmem = aa.chkdbnull(dv1[0]["mem"]);
          mrtn_mem = aa.chkdbnull(dv1[0]["rtn_mem"]);
          mrtn_typ = aa.chkdbnull(dv1[0]["rtn_typ"]);
        }

        dv1.Dispose();

      }

    } // sno>0
        msqlcom.Parameters.Clear();
        msqlcom.CommandText = "select * from ca20_form_typ ";
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
    


}
