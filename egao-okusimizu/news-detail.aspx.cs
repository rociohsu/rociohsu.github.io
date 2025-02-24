using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : MLBasePage
{
    public DataView dv1;
    public int mpage, mc;
    public string mtypnam="",nsno="0",psno="0",mtpc = "", mtpcs = "", mmem = "", mimg = "", mlnk = "", mlnk_tgt = "",mmems="",mlnks="",mpics="";
    public SqlCommand msqlcom = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        init_class();

        msno = aa.chktyp(Request.QueryString["newsid"], 1, "I", 0, 0, "", ref mmsg);
        if (msno == "0") {Response.Write(aa.alert_back("文章不存在或以下架~"+msno)); Response.End(); }

        
        msqlcom.Parameters.Clear();
        
        msqlcom.CommandText = "select substring(mem,1,70) as mems,b.nam as typnam,a.* from CA20_NEWS a left outer join ca20_news_typ b on a.typ=b.sno where a.onf=1 and datediff(d,iif(a.sdt is null,getdate(),a.sdt),getdate())>=0  and datediff(d, iif(a.edt is null,'2050/1/1',a.edt),getdate())<= 0 order by a.sdt desc";
        dv1 = aa.dv_param(msqlcom, ref mmsg);
        if (dv1 != null)
        {
            if (dv1.Count > 0)
            {

                for (int i = 0; i < dv1.Count; i++)
                {
                    if (i>0) psno = aa.chkdbnull(dv1[i-1]["sno"]);

                    if (aa.chkdbnull(dv1[i]["sno"]) == msno)
                    {
                        msdt = aa.showdate(aa.chkdbnull(dv1[i]["sdt"]).Trim(), "yyyy.MM.dd");
                        mtpcs = aa.chkdbnull(dv1[i]["tpcs"]).Trim();
                        mtpc = aa.chkdbnull(dv1[i]["tpc"]).Trim();
                        mtypnam = aa.chkdbnull(dv1[i]["typnam"]);
                        mimg = aa.chkdbnull(dv1[i]["img"]);
                        mmem = aa.chkdbnull(dv1[i]["mem"]);
                        mmems = aa.chkdbnull(dv1[i]["mems"]);
                        mlnk = aa.chkdbnull(dv1[i]["lnk"]);
                        mlnk_tgt = aa.chkdbnull(dv1[i]["lnk_tgt"]);

                        if (i < dv1.Count - 1) nsno = aa.chkdbnull(dv1[i + 1]["sno"]);

                        break;
                    }
                }


            } else { Response.Write(aa.alert_back("文章不存在或以下架~~")); Response.End(); }
            dv1.Dispose();

        }
        else
        { Response.Write(aa.alert_back("文章不存在或以下架!"+mmsg)); Response.End(); }

    }
}