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
    public int mpage, mc;
    public string munt, mfsno,mmem, mimg ,monf,msot, mpth,mimgstyp,nsql;
    public string mcrt_usr, mcrt_dat, mupd_usr, mupd_dat, mupd_usr2, mupd_dat2, mupd_usr3, mupd_dat3;

    

    public DataView dv1, dv2;
    public SqlCommand msqlcom = new SqlCommand(), msqlcom2 = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        munt = aa.chktyp(Request.QueryString["unt"],1,"C",0,20,"",ref mmsg);
        if (munt.Length==0) munt = aa.chktyp(Request.Form["unt"], 1, "C", 0, 20, "", ref mmsg);
        init_class(); //初始化class含db connection, email環境設定
        if (!chkath()) { Response.End(); }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...


        if (aa.chktyp(Request.QueryString["rtn"], 0, "I", 0, 0, "", ref mmsg)=="0") if (Session["UploadFiles"] != null) Session["UploadFiles"] = null;

        mmsg = "";
        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 5, "", ref mmsg);


        if (mb1 == "") mfsno = aa.chktyp(Request.QueryString["fsno"], 0, "I", 0, 0, "", ref mmsg);
        else mfsno = aa.chktyp(Request.Form["fsno"], 0, "I", 0, 0, "", ref mmsg);

        if (mfsno == "0") { Response.Write(aa.alert_script("alert('Layer有誤');window.top.location='closeme()'")); Response.End(); }



        if (mb1 == "SAV" && mmsg.Length == 0)
        {

            msqlcom.Parameters.Add("@mfsno", SqlDbType.Int, 10).Value = mfsno;
            msqlcom.Parameters.Add("@munt", SqlDbType.NVarChar, 50).Value = munt;
            msqlcom.CommandText = "select * from CABASE_img where unt=@munt and fsno=@mfsno";
            dv1 = aa.dv_param(msqlcom, ref mmsg);
            if (dv1 != null)
            {
              
                
                for (int i = 0; i < dv1.Count; i++)
                {
                    mimg = "";
                    nsql = "";
                    if (Request.Files[i].InputStream.Length < 1000000) //限大小為1MB
                    {
                        HttpPostedFile file = Request.Files["file" + dv1[i]["sno"]];
                        if (file != null)
                        {
                            if (file.ContentLength > 0)
                            {

                                if (file.FileName.Substring(file.FileName.LastIndexOf(".")).ToUpper().IndexOf(".JPG") >= 0 || file.FileName.Substring(file.FileName.LastIndexOf(".")).ToUpper().IndexOf(".JPEG") >= 0 || file.FileName.Substring(file.FileName.LastIndexOf(".")).ToUpper().IndexOf(".GIF") >= 0 || file.FileName.Substring(file.FileName.LastIndexOf(".")).ToUpper().IndexOf(".PNG") >= 0)
                                {
                                    string mpth = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM");
                                    if (!aa.dir_create(Server.MapPath(@"../upload/" + munt + "\\" + mpth), ref mmsg))
                                    {

                                        Response.Write(aa.alert_back("ERROR IMAGE:" + mmsg));
                                        Response.End();
                                    }
                                    string[] filesplit = file.FileName.Split('\\');
                                    string filename = filesplit[filesplit.Length - 1];
                                    filename = aa.randomname(filename);
                                    filename = aa.renamefile(Server.MapPath("../upload/" + munt + "/" + mpth), filename); //'重覆上傳重新命名
                                    if (file.ContentType.ToString().ToLower().IndexOf("image") >= 0)                //判斷是否為圖片檔
                                    {
                                        try
                                        {
                                            file.SaveAs(Server.MapPath("../upload/"+ munt + "/" + mpth) + @"\" + filename);
                                        }
                                        catch (Exception ex)
                                        {
                                            mmsg = "ERROR: " + ex.Message.ToString();
                                        }
                                        finally
                                        {
                                           
                                          mimg = "upload/" + munt + "/" + mpth + "/" + filename;
                                          nsql = ",img='upload/" + munt + "/" + mpth + "/" + filename + "'";
                                        }
                                    }
                                    else
                                    {
                                        mmsg += "只限上傳圖片";
                                    }
                                }
                                else
                                {
                                    mmsg += "您上傳的檔案格式" + file.FileName.Substring(file.FileName.LastIndexOf(".") + 1) + "被限制,請上傳其他格式如.JPG...";
                                }
                            }
                        } //length>0
                    }
                    else
                    {
                        mmsg += "第" + (i + 1) + "張圖片超出1MB限制";
                    }

                      msot = aa.chktyp(Request.Form["sot" + dv1[i]["sno"]], 0, "I", 0, 0, "",ref mmsg);
                      mmem = aa.chktyp(Request.Form["mem" + dv1[i]["sno"]], 0, "C", 0, 50, "",ref mmsg);
                      monf = aa.chktyp(Request.Form["onf" + dv1[i]["sno"]], 0, "I", 0, 250, "", ref mmsg);
    
                        msqlcom2.Parameters.Clear();
                      //msqlcom2.Parameters.Add("@mimg", SqlDbType.NVarChar, 250).Value = mimg;
                    msqlcom2.Parameters.Add("@msno", SqlDbType.Int, 10).Value = dv1[i]["sno"];
                      msqlcom2.Parameters.Add("@monf", SqlDbType.Int, 10).Value = msot;
                      msqlcom2.Parameters.Add("@mmem", SqlDbType.NVarChar,-1).Value = mmem;
                      msqlcom2.Parameters.Add("@mcrt_uno", SqlDbType.Int, 10).Value =Session["mmuno"];
                      msqlcom2.Parameters.Add("@mcrt_usr", SqlDbType.NVarChar, 50).Value = Session["mmnam"];
                     
                      msqlcom2.CommandText = "update CABASE_img set onf=@monf,mem=N@mmem,sot=@msot" + nsql + ",upd_usr3=upd_usr2,upd_uno3=upd_uno2,upd_dat3=upd_dat2,upd_usr2=upd_usr,upd_uno2=upd_uno,upd_dat2=upd_dat,upd_usr=@mcrt_usr,upd_uno=@mcrt_uno,upd_dat=getdate() where sno=@msno" ;

                      if (aa.exec_param(msqlcom2, ref mmsg)) aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:修改][資料ID:" + munt  + "," + mfsno +  ","+ dv1[i]["sno"] + "]", "多圖維護");


                }
                dv1.Dispose();
            
            }
            //若需自動縮小圖時可呼叫這支function, 小圖檔名為 small_xxx.jpg"
            //if (mimg.Length > 0) aa.image_resize(Replace(mimg1, "upload/museum/" & mpth & "/", ""), Server.MapPath("/upload/museum/" & mpth & "/"), Server.MapPath("/upload/news/" & mpth & "/"), "small_", 81, 0, mimgstyp, mmsg);
            for (int i = 0; i <=4; i++)
            {
                mimg = "";
                nsql = "";
                if (Request.Files[i].InputStream.Length < 1000000) //限大小為1MB
                {
                    HttpPostedFile file = Request.Files["nfile" + i];
                    if (file != null)
                    {
                        if (file.ContentLength > 0)
                        {

                            if (file.FileName.Substring(file.FileName.LastIndexOf(".")).ToUpper().IndexOf(".JPG") >= 0 || file.FileName.Substring(file.FileName.LastIndexOf(".")).ToUpper().IndexOf(".JPEG") >= 0 || file.FileName.Substring(file.FileName.LastIndexOf(".")).ToUpper().IndexOf(".GIF") >= 0 || file.FileName.Substring(file.FileName.LastIndexOf(".")).ToUpper().IndexOf(".PNG") >= 0)
                            {
                                string mpth = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM");
                                if (!aa.dir_create(Server.MapPath(@"../upload/" + munt  + "//" + mpth), ref mmsg))
                                {

                                    Response.Write(aa.alert_back("ERROR IMAGE:" + mmsg));
                                    Response.End();
                                }
                                string[] filesplit = file.FileName.Split('\\');
                                string filename = filesplit[filesplit.Length - 1];
                                filename = aa.randomname(filename);
                                filename = aa.renamefile(Server.MapPath("../upload/" + munt + "/" + mpth), filename); //'重覆上傳重新命名
                                if (file.ContentType.ToString().ToLower().IndexOf("image") >= 0)                //判斷是否為圖片檔
                                {
                                    try
                                    {
                                        file.SaveAs(Server.MapPath("../upload/" + munt +"/" + mpth) + @"\" + filename);
                                    }
                                    catch (Exception ex)
                                    {
                                        mmsg = "ERROR: " + ex.Message.ToString();
                                    }
                                    finally
                                    {

                                        mimg = "upload/" + munt + "/" + mpth + "/" + filename;
                                        nsql = nsql + ",img='upload/" + munt + "/" + mpth + "/" + filename + "'";
                                    }
                                }
                                else
                                {
                                    mmsg += "只限上傳圖片";
                                }
                            }
                            else
                            {
                                mmsg += "您上傳的檔案格式" + file.FileName.Substring(file.FileName.LastIndexOf(".") + 1) + "被限制,請上傳其他格式如.JPG...";
                            }
                        }
                    } //length>0
                }
                else
                {
                    mmsg += "第" + (i + 1) + "張圖片超出1MB限制";
                }


                msot = aa.chktyp(Request.Form["nsot" + i], 0, "I", 0, 0, "", ref mmsg);
                mmem = aa.chktyp(Request.Form["nmem" + i], 0, "C", 0, 50, "", ref mmsg);
                monf = aa.chktyp(Request.Form["nonf" + i], 0, "I", 0, 250, "", ref mmsg);

                //Response.Write(i.ToString() + mtpc + mlnk + msot + "<br>");

                if (mimg != "")
                {

                    msqlcom2.Parameters.Clear();
                    msqlcom2.Parameters.Add("@munt", SqlDbType.NVarChar, 50).Value = munt;
                    msqlcom2.Parameters.Add("@mfsno", SqlDbType.Int, 10).Value =mfsno;
                    msqlcom2.Parameters.Add("@monf", SqlDbType.Int, 10).Value = msot;
                    msqlcom2.Parameters.Add("@mmem", SqlDbType.NVarChar, -1).Value = mmem;
                    msqlcom2.Parameters.Add("@mimg", SqlDbType.NVarChar, 250).Value = mimg;
                    msqlcom2.Parameters.Add("@mcrt_uno", SqlDbType.Int, 10).Value = Session["mmuno"];
                    msqlcom2.Parameters.Add("@mcrt_usr", SqlDbType.NVarChar, 50).Value = Session["mmnam"];
                    msqlcom2.CommandText = "insert into CABASE_img (unt,fsno,onf,mem,sot,img,crt_usr,crt_uno) values (N@munt,@mfsno,@monf,N@mmem,@msot,@mimg,N@mcrt_usr,@mcrt_uno); select scope_identity();";
                    if (!aa.exec_param(msqlcom2, ref mmsg))
                    { Response.Write(aa.alert(mmsg)); }
                    else aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:新增][資料ID:" + munt + "," + mfsno + "," + msno + "]", "多圖維護");


                }
                

            }

            mmsg2 = mmsg;
        } //save


        if (mb1=="D")
        {
            msno = aa.chktyp(Request.Form["sno"], 0, "I", 0, 0, "", ref mmsg);
            if (msno != "0")
            {
                msqlcom.Parameters.Clear();
                msqlcom.Parameters.Add("@msno", SqlDbType.Int, 10).Value = msno;
                msqlcom.CommandText = "delete from CABASE_img where sno=@msno";
                if (aa.exec_param(msqlcom, ref mmsg)) { aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]","[" +  Session["mmnam"] + "]", "[動作:刪除][資料ID:"  + munt + "," + mfsno + "," + msno + "]","多圖維護"); }

            }

        
        }
        msqlcom.Parameters.Clear();
        msqlcom.Parameters.Add("@mfsno", SqlDbType.Int, 10).Value = mfsno;
        msqlcom.Parameters.Add("@munt", SqlDbType.NVarChar, 50).Value = munt;
    }
 
}