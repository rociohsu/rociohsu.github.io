<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Drawing2D;
public class Handler : MLBaseAshx, IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "image/jpeg";
        init_class(context);

        context.Session["vcode"] = string.Format("{0:0000}", aa.rnd(10000));

        draw_vcode(context.Session["vcode"].ToString());
        //另一種 
        //List<object> data = new List<object>();
        //for()....
        //data.Add(new
        //{
            //id = aa.chkdbnull(dv1[i]["sno"]),
            //img = "/" + aa.chkdbnull(dv1[i]["img"]),
            //floor = aa.chkdbnull(dv1[i]["flr"]),
            //brand = aa.chkdbnull(dv1[i]["tpc_" + mlang_no]),
            //title = aa.chkdbnull(dv1[i]["mem2_" + mlang_no]),
            //lnk = mlnk,
            //content = aa.chkdbnull(dv1[i]["mem3_" + mlang_no]),
            //card1 = aa.chkdbnull(dv1[i]["card1s"]),
            //card2 = aa.chkdbnull(dv1[i]["card2s"])

        //});
        //....
        //context.Response.Write( ( new System.Web.Script.Serialization.JavaScriptSerializer().Serialize( data ) ) );
        
        //另一種宣告式
        
        //String result = "";
        //String sSql = "select top 10 b.tpc_tw,a.mem_tw,a.img,a.imgs,a.crt_dat from CABASE_album_photo a join CABASE_album b on a.fsno=b.sno where b.onf=1 order by a.crt_dat desc";
        //using (DataView dv1 = aa.dv(sSql, ref mmsg))
        //{
        //    List<ImgData> dd = new List<ImgData>();
        //    if (dv1.Count > 0)
        //    {

        //        for (int i = 0; i < dv1.Count; i++)
        //        {
        //            ImgData d1 = new ImgData();
        //            d1.TITLE = aa.chkdbnull(dv1[i]["tpc_tw"]);
        //            d1.DESC = aa.chkdbnull(dv1[i]["mem_tw"]);
        //            d1.PHOTO = aa.chkdbnull(dv1[i]["img"]);
        //            d1.THUMB = aa.chkdbnull(dv1[i]["imgs"]);
        //            dd.Add(d1);
        //        }
        //    }
        //    result = JsonConvert.SerializeObject(new { RS = "OK", DATA = dd });
        //}
        //if (mmsg != "")
        //{
        //    result = JsonConvert.SerializeObject(new { RS = mmsg });
        //}
        //context.Response.Write(result);
    }

    //private class ImgData
    //{
    //    public string TITLE { get; set; }
    //    public string DESC { get; set; }
    //    public string PHOTO { get; set; }
    //    public string THUMB { get; set; }
    //}
    public void draw_vcode(string V_code)
    {
        System.Web.HttpContext mx = System.Web.HttpContext.Current;
        mx.Response.Clear();
        Bitmap image = new Bitmap(V_code.Length * 15, 0x18);
        Graphics graphics = Graphics.FromImage(image);
        string s = V_code;
        string familyName = "Courier New Bold";
        Color foreColor = Color.FromArgb(220, 220, 220);
        Color backColor = Color.FromArgb(190, 190, 190);
        HatchBrush brush = new HatchBrush(this.generateHatchStyle(), foreColor, backColor);
        SolidBrush brush2 = new SolidBrush(Color.Gray);
        graphics.FillRectangle(brush, 0, 0, V_code.Length * 15, 50);
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        Font font = new Font(familyName, 14f);
        PointF point = new PointF(5f, 4f);
        graphics.DrawString(s, font, brush2, point);
        mx.Response.ContentType = "image/jpeg";
        image.Save(mx.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        image.Dispose();
        mx.Response.End();

    }
    public bool IsReusable {
        get {
            return false;
        }
    }
    private HatchStyle generateHatchStyle()
    {
        System.Collections.IEnumerator enumerator;
        System.Collections.ArrayList list = new System.Collections.ArrayList();

        enumerator = System.Enum.GetValues(typeof(HatchStyle)).GetEnumerator();
        while (enumerator.MoveNext())
        {
            HatchStyle style2 = (HatchStyle)Convert.ToInt16(enumerator.Current);
            list.Add(style2);
        }


        int num = new Random().Next(list.Count - 1);
        return (HatchStyle)Convert.ToInt16(list[num]);
    }
}