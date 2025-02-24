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
    public SqlCommand msqlcom = new SqlCommand(), msqlcom2 = new SqlCommand();
    protected void Page_Load(object sender, EventArgs e)
    {
        init_class();
        mb1 = aa.chktyp(Request.Form["b1"], 0, "C", 0, 5, "", ref mmsg);

        if (mb1 == "")
        {
            mc = Int32.Parse(aa.chktyp(Request.QueryString["mc"], 1, "I", 0, 0, "", ref mmsg));
            mpage = Int32.Parse(aa.chktyp(Request.QueryString["page"], 1, "I", 0, 0, "", ref mmsg));
            
            
        }
        else
        {
            mpage = Int32.Parse(aa.chktyp(Request.Form["page"], 1, "I", 0, 0, "", ref mmsg));
            mc = Int32.Parse(aa.chktyp(Request.Form["mc"], 1, "I", 0, 0, "", ref mmsg));
            
            
        }
        
        if (mpage == 0) { mpage = 1;  }
        if (mc == 0) mc = 50;




        msqlcom.Parameters.Clear();
        msqlcom2.Parameters.Clear();
        msqlcom.CommandText = "select count(*) as cnt from CA20_NEWS a where a.onf=1 and datediff(d,iif(a.sdt is null,getdate(),a.sdt),getdate())>=0  and datediff(d, iif(a.edt is null,'2050/1/1',a.edt),getdate())<= 0  " + whr;
        msqlcom2.CommandText = "select b.nam as typnam,a.* from CA20_NEWS a left outer join ca20_news_typ b on a.typ=b.sno  where a.onf=1 and datediff(d,iif(a.sdt is null,getdate(),a.sdt),getdate())>=0  and datediff(d, iif(a.edt is null,'2050/1/1',a.edt),getdate())<= 0 " + whr + " order by a.sdt desc";

    }
}