<%@ WebHandler Language="C#" Class="sql2csv" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public class sql2csv : MLBaseAshx, IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.Clear();
        context.Response.ContentType = "text/csv";
        context.Response.AppendHeader("Content-Disposition",
                        string.Format("attachment; filename={0:yyyyMMddHHmmssfff}.csv", DateTime.Now));
        //context.Response.HeaderEncoding = System.Text.Encoding.UTF8;
        context.Response.Charset = "BIG5";
        context.Response.ContentEncoding = System.Text.Encoding.GetEncoding(950);//950就是所謂的BIG5
        init_class(context);

        SqlCommand msqlcom = new SqlCommand();

        string rtn = "";
        string sql = string.Format("select foo from jk_event1704_sn");
        msqlcom.CommandText = sql;
        System.Collections.Generic.List<string> Columns = new List<string>();
        using (System.Data.DataView dv1 = aa.dv_param (msqlcom, ref mmsg))
        {
            int i = 0;
            foreach (DataColumn col in dv1.ToTable().Columns)
            {
                if (i > 0)
                    rtn += ",";
                rtn += string.Format("\"{0}\"", col.ColumnName);
                Columns.Add(col.ColumnName);
                i++;
            }
            rtn += "\n";


            foreach (DataRowView x in dv1)
            {
                i = 0;
                foreach (string col in Columns)
                {
                    if (i > 0)
                        rtn += ",";
                    //if (col == "mobile" && x[col] != null)
                    //    x[col] = context.Server.HtmlDecode(x[col].ToString());
                    rtn += string.Format("=\"{0}\"", x[col]);
                    i++;
                }
                rtn += "\n";
                context.Response.Write(rtn);
                context.Response.Flush();
                rtn = "";
            }
        }

        if (msqlcom != null) msqlcom.Dispose();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}