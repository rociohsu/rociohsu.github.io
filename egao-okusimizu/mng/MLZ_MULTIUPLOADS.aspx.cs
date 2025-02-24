using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;


public partial class jkz_multiupload : MLBasePage
{   public string munt,mfsno,monf,mmem,mimg,msot;
    public string mpth = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM");
    protected void Page_Load(object sender, EventArgs e)
    {
        
        init_class(); //初始化class含db connection, email環境設定
        if (!chkath()) { Response.End(); }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...

        mmsg = "";
        msot="0";
        monf="1";        
        munt = "";

        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 5, "", ref mmsg);



        mfsno = aa.chktyp(Request.QueryString["fsno"], 0, "I", 0, 0, "", ref mmsg);    
        if (mfsno=="0") mfsno = aa.chktyp(Request.Form["fsno"], 0, "I", 0, 0, "", ref mmsg);
        
        munt = aa.chktyp(Request.QueryString["unt"], 0, "C", 0, 50, "", ref mmsg);
        if (munt == "") munt = aa.chktyp(Request.Form["unt"], 0, "C", 0, 50, "", ref mmsg);

        //if (mfsno == "0") Session["multiupload_fsno"] = mfsno;
        //else mfsno = Session["multiupload_fsno"].ToString();
        

        if (munt.Length > 0) Session["fsno"] = mfsno;
        if (mfsno == "0") mfsno = Session["fsno"].ToString();
        if (munt.Length > 0) Session["multiupload_unt"] = munt;
        else munt = Session["multiupload_unt"].ToString();
        
        if (munt.Length == 0) munt = "NEWS2";
        //Response.Write(mfsno);
        //if (mfsno == "0") { Response.Write(aa.alert_script("alert('Layer有誤');window.top.location='closeme()'")); Response.End(); }




        if (mb1=="SAV")  //第一次進來時, 把之前的圖片記錄清空
        {

            string[] mfiles = { };
            string[] msizes = { };

            
            aa.UPLOAD("", 2000000, "", true, true, munt, ref mfiles, ref msizes, ref mmsg);//最後一個不寫blob路徑, 則抓web.config裡StorageURL參數
            if (mmsg != "") mmsg2 = mmsg;
            for (int i = 0; i < mfiles.Length; i++)
            {
                if (mfiles[i] != "")
                    using (SqlCommand msqlcom = new SqlCommand())
                    {
                        msqlcom.Parameters.Add("@munt", SqlDbType.NVarChar, 50).Value = munt;
                        msqlcom.Parameters.Add("@mfsno", SqlDbType.Int, 10).Value = mfsno;
                        msqlcom.Parameters.Add("@monf", SqlDbType.Int, 10).Value = monf;
                        msqlcom.Parameters.Add("@mmem", SqlDbType.NVarChar, 250).Value = mmem;
                        msqlcom.Parameters.Add("@msot", SqlDbType.Int, 10).Value = msot;
                        msqlcom.Parameters.Add("@mimg", SqlDbType.NVarChar, 250).Value = mfiles[i];
                        msqlcom.Parameters.Add("@mcrt_uno", SqlDbType.Int, 10).Value = Session["mmuno"].ToString();
                        msqlcom.Parameters.Add("@mcrt_usr", SqlDbType.NVarChar, 250).Value = Session["mmnam"].ToString(); ;
                        msqlcom.CommandText = "insert into CABASE_img (unt,fsno,onf,mem,sot,img,crt_usr,crt_uno) values (N@munt,@mfsno,@monf,N@mmem,@msot,@mimg,@mcrt_usr,@mcrt_uno)";
                        aa.exec_param(msqlcom, ref mmsg);
                    }

                

            }
            if (mfiles.Length>0) aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:上傳][資料ID:" + munt + ","+ mfsno + "]", "多圖上傳");

            if (mmsg2=="")  Response.Redirect("MLZ_MULTIUPLOAD.aspx?fsno=" + mfsno + "&unt=" + munt);
            


        }
        //if (Session["UploadFiles"] != null)
        //{
        //    fileUpload.Value = Session["UploadFiles"].ToString();
        //}
    }

 
}