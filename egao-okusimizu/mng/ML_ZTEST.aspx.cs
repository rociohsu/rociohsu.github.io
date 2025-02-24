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
    public DataView dv1;
    public int mpage, mc;
    public string mdomain = "";
    public string medm = "";
    public SqlCommand msqlcom = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));   
        if (!chkath()) { mng_main.Visible = false; }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
                                                      //以下開始coding...
        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 10, "", ref mmsg);
        msqlcom.CommandText = "select url from CABASE_setup where  nam=N'後台管理HTTP網址'";
        dv1 = aa.dv_param(msqlcom, ref mmsg);
        if (dv1 != null)
        {
            if (dv1.Count > 0) mdomain = aa.chkdbnull(dv1[0]["url"]);
        }
        if (mb1 == "TEST")
        {
            msqlcom.CommandText = "select * from CABASE_unit_test";



            dv1 = aa.dv_param(msqlcom, ref mmsg);
            if (dv1 != null)
            {
                //if (dv1.Count > 0) medm = "<table border=\"1\">";
                for (int i = 0; i < dv1.Count; i++)
                {
                    string msts = "";
                    if (aa.chkdbnull(dv1[i]["method"]) == "GET") {
                        if (aa.readhtmlcode(mdomain + aa.chkdbnull(dv1[i]["lnk"]) + aa.chkdbnull(dv1[i]["para"]), ref mmsg))
                        {
                            msts = "OK";                            
                        }
                        else
                        {
                            msts = "ERROR";
                        }
                       
                    }
                    if (aa.chkdbnull(dv1[i]["method"]) == "POST")
                    {
                        if (aa.readhtmlcode(mdomain + aa.chkdbnull(dv1[i]["lnk"]), ref mmsg, aa.chkdbnull(dv1[i]["para"]).Replace("?","&"), "utf-8"))
                        {
                            msts = "OK";
                        }
                        else
                        {
                            msts = "ERROR";
                        }
                    }
                    msqlcom.CommandText = "update CABASE_unit_test set sts='OK',log=N'" + mmsg + "' where sno=" + dv1[i]["sno"];
                    aa.exec_param(msqlcom, ref mmsg);


                    medm += "<table border=\"1\"><tr><td width=\"150\" nowrap>程式名稱 (" + (i+1) + ")</td><td>" + aa.chkdbnull(dv1[i]["lnk"]) + "</td></tr>";
                    medm += "<tr><td>功能說明:</td><td>" + aa.chkdbnull(dv1[i]["tpc"]) + "</td></tr>";
                    medm += "<tr><td>傳輸方式:</td><td>" + aa.chkdbnull(dv1[i]["method"]) + "</td></tr>";
                    medm += "<tr><td>傳入參數:</td><td>" + aa.chkdbnull(dv1[i]["para"]) + "</td></tr>";
                    medm += "<tr><td>參數說明:</td><td>" + aa.chkdbnull(dv1[i]["para_mem"]).Replace("\n","<br>") + "</td></tr>";
                    medm += "<tr><td valign=\"top\">回傳結果:</td><td><div style=\"width:500px;height:250px;overflow:auto;\">" + aa.chkdbnull(dv1[i]["log"]).Replace("[", "[<br>").Replace("]", "]<br>").Replace("}", "}<br>").Replace(",", ",<br>") + "</div></td></tr>";
                    medm +="</table><hr>";

                }
                //if (dv1.Count > 0) medm += "</table>";
                dv1.Dispose();
            }

        }


        if (mb1 == "SAV")
        {

            mdomain = aa.chktyp(Request.Form["domain"], 1, "C", 0, 150, "網域名稱", ref mmsg);
            msqlcom.Parameters.Add("@mdomain", SqlDbType.NVarChar, 250).Value = mdomain;
            msqlcom.CommandText = "update CABASE_setup set url=@mdomain where nam=N'後台管理HTTP網址'";

            aa.exec_param(msqlcom, ref mmsg);


            msqlcom.CommandText = "select * from CABASE_unit_test";
            dv1 = aa.dv_param(msqlcom, ref mmsg);
            if (dv1!=null)
            {
                for (int i = 0; i < dv1.Count; i++)
                {
                    string mtpc = aa.chktyp(Request.Form["tpc" + dv1[i]["sno"]], 0, "HTML", 0, 0, "", ref mmsg);
                    string mlnk = aa.chktyp(Request.Form["lnk" + dv1[i]["sno"]], 0, "C", 0, 250, "", ref mmsg);
                    string mmethod = aa.chktyp(Request.Form["method" + dv1[i]["sno"]], 0, "C", 0, 20, "", ref mmsg);
                    string mpara = aa.chktyp(Request.Form["para" + dv1[i]["sno"]], 0, "HTML", 0, 0, "", ref mmsg);
                    string mpara_mem = aa.chktyp(Request.Form["para_mem" + dv1[i]["sno"]], 0, "HTML", 0, 0, "", ref mmsg);
                    if (mlnk != "")
                    {
                        msqlcom.Parameters.Clear();
                        msqlcom.Parameters.Add("@mtpc", SqlDbType.NVarChar, 250).Value = mtpc;
                        msqlcom.Parameters.Add("@mlnk", SqlDbType.NVarChar, 350).Value = mlnk;
                        msqlcom.Parameters.Add("@mmethod", SqlDbType.NVarChar, 10).Value = mmethod;
                        msqlcom.Parameters.Add("@mpara", SqlDbType.NVarChar, 500).Value = mpara;
                        msqlcom.Parameters.Add("@mpara_mem", SqlDbType.NVarChar, -1).Value = mpara_mem;
                        msqlcom.Parameters.Add("@msno", SqlDbType.Int, 0).Value = dv1[i]["sno"];
                        msqlcom.CommandText = "update CABASE_unit_test set tpc=N@mtpc,lnk=N@mlnk,method=@mmethod,para=N@mpara,para_mem=N@mpara_mem where sno=@msno";

                        aa.exec_param(msqlcom, ref mmsg);

                    }
                }
                dv1.Dispose();
            }

        }


        if (mb1 == "DELALL")
        {
            msqlcom.CommandText = "truncate table CABASE_unit_test";
            aa.exec_param(msqlcom, ref mmsg);
        }
        if (mb1 == "lst")
        {
            string mfolder = aa.chktyp(Request.Form["dir"], 1, "C", 0, 50, "", ref mmsg);
            if (mfolder != "")
            {
                String[] FileCollection;
                String FilePath = Server.MapPath(mfolder);
                FileInfo theFileInfo;

                try {

                    FileCollection = Directory.GetFiles(FilePath, "*.aspx");
                    for (int i = 0; i < FileCollection.Length; i++)
                    {
                        theFileInfo = new FileInfo(FileCollection[i]);
                        //Response.Write(theFileInfo.Name.ToString() + "<BR>");
                        msqlcom.Parameters.Clear();
                        msqlcom.Parameters.Add("@mfold", SqlDbType.NVarChar, 500).Value = (mfolder == "/" ? "" : mfolder) + "/" + theFileInfo.Name.ToString();
                        msqlcom.Parameters.Add("@mcrt_usr", SqlDbType.NVarChar, 50).Value = Session["mmnam"];
                        msqlcom.Parameters.Add("@mcrt_uno", SqlDbType.NVarChar, 50).Value = Session["mmuno"];
                        msqlcom.CommandText = "insert into CABASE_UNIT_TEST (lnk,method,para,crt_usr,crt_uno) values (N@mfold,'POST',NULL,N@mcrt_usr,@mcrt_uno)";
                        aa.exec_param(msqlcom, ref mmsg);

                    }

                    FileCollection = Directory.GetFiles(FilePath, "*.ashx");
                    for (int i = 0; i < FileCollection.Length; i++)
                    {
                        theFileInfo = new FileInfo(FileCollection[i]);
                        //Response.Write(theFileInfo.Name.ToString() + "<BR>");
                        msqlcom.Parameters.Clear();
                        msqlcom.Parameters.Add("@mfold", SqlDbType.NVarChar, 500).Value = (mfolder == "/" ? "" : mfolder) + "/" + theFileInfo.Name.ToString();
                        msqlcom.Parameters.Add("@mcrt_usr", SqlDbType.NVarChar, 50).Value = Session["mmnam"];
                        msqlcom.Parameters.Add("@mcrt_uno", SqlDbType.NVarChar, 50).Value = Session["mmuno"];
                        msqlcom.CommandText = "insert into CABASE_UNIT_TEST (lnk,method,para,crt_usr,crt_uno) values (N@mfold,'POST',NULL,N@mcrt_usr,@mcrt_uno)";
                        aa.exec_param(msqlcom, ref mmsg);

                    }
                }
                catch (Exception ex) { mmsg2 = ex.Message.Replace("'", "").Replace("\\","/"); }

                
            }
        }
    }

    


    


    


    


    


    
    
}