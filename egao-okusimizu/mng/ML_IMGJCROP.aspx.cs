using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class mng_test : MLBasePage
{
    public string imgSrc, pWdh, pHdh;
    public string mx1, mx2, my1, my2, mw, mh;
    public int ow = 0, oh = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        Response.AppendHeader("Cache-Control", "no-cache");
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));   
        if (Request.QueryString["imgSrc"] != null)
        {
            imgSrc = Request.QueryString["imgSrc"];
            if (imgSrc.IndexOf('?') > 0)
            {
                imgSrc = imgSrc.Substring(0, imgSrc.IndexOf('?'));
            }
        }

        ow = int.Parse(aa.chktyp(Request.Form["ow"], 0, "I", 0, 0, "", ref mmsg));
        oh = int.Parse(aa.chktyp(Request.Form["oh"], 0, "I", 0, 0, "", ref mmsg));
        if (ow == 0 && oh == 0)
        {
            string[] mara = aa.chktyp(Request.QueryString["reSize"], 0, "C", 0, 50, "", ref mmsg).Split(',');
            if (mara.Length >= 2)
            {
                ow = int.Parse(mara[0].Substring(1, mara[1].Length - 1));
                oh = int.Parse(mara[1].Substring(0, mara[1].Length - 1));
            }
        }


        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 5, "", ref mmsg);
        if (mb1 == "SAV")
        {
            imgSrc = aa.chktyp(Request.Form["imgSrc"], 0, "C", 0, 100, "", ref mmsg);
            if (mb1 == "SAV")
            {
                mx1 = aa.chktyp(Request.Form["x1"], 0, "I", 0, 0, "", ref mmsg);
                mx2 = aa.chktyp(Request.Form["x2"], 0, "I", 0, 0, "", ref mmsg);
                my1 = aa.chktyp(Request.Form["y1"], 0, "I", 0, 0, "", ref mmsg);
                my2 = aa.chktyp(Request.Form["y2"], 0, "I", 0, 0, "", ref mmsg);

                mw = aa.chktyp(Request.Form["w"], 0, "I", 0, 0, "", ref mmsg);
                mh = aa.chktyp(Request.Form["h"], 0, "I", 0, 0, "", ref mmsg);
                mmsg = "";
                if (mw != "0" && mw != "0")
                {
                    aa.Image_cut(GetBasename(imgSrc), Server.MapPath(imgSrc.Replace(GetBasename(imgSrc), "")) + "/", Server.MapPath(imgSrc.Replace(GetBasename(imgSrc), "")) + "/", ""
                        , long.Parse(mx1), long.Parse(my1), long.Parse(mx2), long.Parse(my2), "", ref mmsg);
                    if (ow>0 || oh>0)
                    {   
                        if (oh==0) aa.image_resize(GetBasename(imgSrc), Server.MapPath(imgSrc.Replace(GetBasename(imgSrc), "")) + "/", Server.MapPath(imgSrc.Replace(GetBasename(imgSrc), "")) + "/", "", ow,0 , "", ref mmsg);
                        else aa.image_resize(GetBasename(imgSrc), Server.MapPath(imgSrc.Replace(GetBasename(imgSrc), "")) + "/", Server.MapPath(imgSrc.Replace(GetBasename(imgSrc), "")) + "/", "", 0, oh, "", ref mmsg);
                        //Response.Write(aa.alert_script("alert('ReZize成功!" + ow.ToString() + "x" + oh.ToString() + "')"));
                    }
                    using (SqlCommand msqlcom = new SqlCommand())
                    {
                        msqlcom.Parameters.Add("@munm", SqlDbType.NVarChar, 50).Value = Session["unm"];
                        msqlcom.Parameters.Add("@mimgSrc", SqlDbType.NVarChar, 250).Value = imgSrc;
                        msqlcom.CommandText = "insert into CABASE_IMG (UNT,TYP,IMG,SIZ,CRT_USR) values ('pdt',1,@mimgSrc,100000,N@munm)";
                        aa.exec_param(msqlcom, ref mmsg);
                    }

                    
                    aa.system_log("" + YYurl.ToLower(), "[" + mmfrm + "]", "[" + Session["mmnam"] + "]", "[動作:修改][資料ID:" + imgSrc + "]", "圖片裁切");
                    Response.Write(aa.alert_script("window.parent.closeme();window.top.relpadImg();"));
                }
                else
                {
                    mmsg = "請選擇剪裁範圍";
                }
                Response.Write(aa.alert_back(mmsg));

            }
        }
    }

    // 取得檔名(去除路徑)
    public static string GetBasename(string fullName)
    {
        string result;
        int lastBackSlash = fullName.LastIndexOf("/");
        result = fullName.Substring(lastBackSlash + 1);

        return result;
    }
}