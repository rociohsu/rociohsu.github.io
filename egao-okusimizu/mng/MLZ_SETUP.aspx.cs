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
    public string msot, mfsno, mmode, mweb, murl, murl2;


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
        msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref mmsg);
        mweb = aa.chktyp(Request.Form["web"], 0, "HTML", 0, 100, "", ref mmsg);

        murl = aa.chktyp(Request.Form["xurl"], 0, "HTML", 0, 500, "", ref mmsg);
        murl2 = aa.chktyp(Request.Form["xurl2"], 0, "HTML", 0, 500, "", ref mmsg);
        mfsno = "0";

        switch (mb1)
        {
            case "ADD":
                using (System.Data.SqlClient.SqlCommand mndm = new System.Data.SqlClient.SqlCommand())
                {
                    mndm.Parameters.Add("@mfsno", SqlDbType.Int, 8).Value = mfsno;
                    mndm.CommandText = "select * from CABASE_setup where sno=@mfsno";
                    dv1 = aa.dv_param(mndm, ref mmsg);
                    if (dv1 != null) { mc = dv1.Count + 1; }
                    else mc = 1;

                    mndm.Parameters.Clear();
                    mndm.Parameters.Add("@mc", SqlDbType.Int, 8).Value = mc;
                    mndm.CommandText = "insert into CABASE_setup (nam,url,fsno,sot)  values ('','',0,@mc); select scope_identity();";
                    if (!aa.exec_param(mndm, ref mmsg))
                    {
                        Response.Write(aa.alert_back(mmsg));

                        Response.End();
                        break;
                    }
                    else { msno = mmsg; aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:新增][資料ID:" + msno + "]", ""); }
                }
                break;
            case "SAV":


                using (System.Data.SqlClient.SqlCommand mndm = new System.Data.SqlClient.SqlCommand())
                {
                    mndm.CommandText = "select * from CABASE_setup";
                    dv1 = aa.dv_param(mndm, ref mmsg);
                    if (dv1 != null)
                    {
                        for (int i = 0; i < dv1.Count; i++)
                        {

                            mweb = aa.chktyp(Request.Form["web" + dv1[i]["sno"].ToString()], 0, "C", 0, 100, "", ref mmsg);
                            murl = aa.chktyp(Request.Form["xurl" + dv1[i]["sno"].ToString()], 0, "HTML", 0, 500, "", ref mmsg);

                            if (mweb == "後台管理LOGO")
                            {
                                string[] mfiles = { };
                                string[] msizes = { };
                                
                                aa.UPLOAD("filelogo", 2000000, "", true, true, "/upload/LOGO", ref mfiles, ref msizes, ref mmsg);//最後一個不寫blob路徑, 則抓web.config裡StorageURL參數
                                if (mmsg == "" && mfiles.Length > 0 && mfiles[0] != "")
                                {
                                    murl = "" + mfiles[0];
                                }

                            }
                            if (mweb != "")
                            {
                                msot = aa.chktyp(Request.Form["sot" + dv1[i]["sno"].ToString()], 0, "I", 0, 2, "", ref mmsg);

                                mndm.Parameters.Clear();
                                mndm.Parameters.Add("@msno", SqlDbType.Int, 8).Value = dv1[i]["sno"].ToString();
                                mndm.Parameters.Add("@mweb", SqlDbType.NVarChar, 100).Value = mweb;
                                mndm.Parameters.Add("@murl", SqlDbType.NVarChar, 500).Value = murl;
                                mndm.Parameters.Add("@msot", SqlDbType.Int, 8).Value = msot;
                                mndm.Parameters.Add("@mupd_usr", SqlDbType.NVarChar, 50).Value = Session["mmnam"].ToString();
                                mndm.Parameters.Add("@mupd_uno", SqlDbType.Int, 8).Value = Session["mmuno"].ToString();

                                mndm.CommandText = "update CABASE_setup set nam=@mweb,url=@murl,sot=@msot,upd_usr=@mupd_usr,upd_uno=@mupd_uno,upd_dat=getdate() where sno=@msno";

                                //mmsg2=mmsg2 + mSql + "\n";
                                if (!aa.exec_param(mndm, ref mmsg))
                                {
                                    Response.Write(aa.alert_back(mmsg));

                                    Response.End();
                                    break;
                                }
                            }





                        }
                        dv1.Dispose();
                    }
                }
                mmsg2 = "儲存成功";
                aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:修改]", "");

                
                break;
            case "DEL":

                using (System.Data.SqlClient.SqlCommand mndm = new System.Data.SqlClient.SqlCommand())
                {
                    mndm.Parameters.Add("@msno", SqlDbType.Int, 8).Value = msno;
                    mndm.CommandText = "delete from CABASE_setup where sno = @msno";
                    if (!aa.exec_param(mndm, ref mmsg))
                    {
                        Response.Write(aa.alert_back(mmsg));

                    }
                    else aa.system_log(YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:刪除][資料ID:" + msno + "]", "");
                }
                break;

        }


        msqlcom.CommandText = "select * from CABASE_setup a where 1=1 " + whr + " order by sot";

    }



    
}