using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _default : MLBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect(mmmng + "jk_index.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));
        if (!chkath()) { return; }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...

        string sql = string.Format("execute sp_tables @table_type = \"'TABLE'\"");
        DataSet ds = null;
        SqlCommand msqlcom2 = new SqlCommand();
        using (SqlCommand msqlcom = new SqlCommand())
        {
            msqlcom.CommandText = sql;
            using (DataView dv1 = aa.dv_param(msqlcom, ref mmsg))
            {

                ds = dv1.Table.DataSet;
                ds.Tables[0].TableName = "ALL";
                DataTable dt = null;
                foreach (DataRowView x in dv1)
                {
                    string TABLE_NAME = x["TABLE_NAME"].ToString();
                    sql = string.Format("sp_columns {0}", TABLE_NAME);
                    msqlcom2.CommandText = sql;
                    using (DataView dv2 = aa.dv_param(msqlcom2, ref mmsg))
                    {
                        if (dt == null)
                        {
                            //初始化tb
                            dt = dv2.Table.Clone();
                        }
                        for (int i = 0; i < dv2.Count; i++)
                        {
                            dt.ImportRow(dv2.Table.Rows[i]);
                        }

                    }

                }


                ds.Tables.Add(dt);
                ds.Relations.Add("myrelation",
                 ds.Tables[0].Columns["TABLE_NAME"],
                 ds.Tables[1].Columns["TABLE_NAME"]);
                Repeater1.DataSource = ds.Tables[0];
                Repeater1.DataBind();
            }
        }
        if (msqlcom2 != null) msqlcom2.Dispose();

    }
}