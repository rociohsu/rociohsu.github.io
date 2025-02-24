<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
public class Handler : MLBaseAshx, IHttpHandler
{

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        init_class(context);
        aa.system_log("" +YYurl.ToLower(), "[" + mmfrm + "]", "[" +  context.Session["mmnam"] + "]", "[動作:登出]","登出");
             context.Response.Cookies["atg"].Expires = DateTime.Now.AddDays(-1);
        context.Session.Abandon();
        context.Response.Redirect("ML_LOGIN.ASPX");
    }

    //private class ImgData
    //{
    //    public string TITLE { get; set; }
    //    public string DESC { get; set; }
    //    public string PHOTO { get; set; }
    //    public string THUMB { get; set; }
    //}

    public bool IsReusable {
        get {
            return false;
        }
    }
    public static string BinaryStringToHexString(string binary)
    {
        System.Text.StringBuilder result = new System.Text.StringBuilder(binary.Length / 8 + 1);

        // TODO: check all 1's or 0's... Will throw otherwise

        int mod4Len = binary.Length % 8;
        if (mod4Len != 0)
        {
            // pad to length multiple of 8
            binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
        }

        for (int i = 0; i < binary.Length; i += 8)
        {
            string eightBits = binary.Substring(i, 8);
            result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
        }

        return result.ToString();
    }
}