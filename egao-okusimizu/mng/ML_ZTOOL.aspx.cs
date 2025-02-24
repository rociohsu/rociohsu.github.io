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
    public SqlCommand msqlcom = new SqlCommand();

    protected void Page_Load(object sender, EventArgs e)
    {
        init_class(); //初始化class含db connection, email環境設定
        //Session["mmuno"] = "1";
        //Session["mmnam"] = "admin";
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***") + "&rnd="+aa.rnd(10000));   
        if (!chkath()) { mng_main.Visible = false; }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...

        Label1.Text = "";


        
    }

    protected void Button1_Click(Object sender, System.EventArgs e)
    {
        if (Page.IsPostBack)
        {

            int cnt;

            string mstr = "";
            cnt = 0;
            //Response.Write(aa.alert(TextBox1.Text.Length.ToString() + "," + TextBox1.Text.Substring(0, 6)));
            if (TextBox1.Text.Length > 0 && TextBox1.Text.Trim().IndexOf("select") == 0)
            {
                msqlcom.Parameters.Clear();
                msqlcom.CommandText = TextBox1.Text;
                dv1 = aa.dv_param(msqlcom, ref mmsg);

                if (dv1 != null)
                {   //Response.Write(aa.alert(mmsg));
                    mstr = @"<table border=""1"" cellspacing=""0"" cellpadding=""0"" width=""100%"" ><tr>";
                    foreach (DataColumn colnam in dv1.Table.Columns)
                    {   //Response.Write(aa.alert(colnam.ColumnName));
                        mstr = mstr + @"<td>" + colnam.ColumnName + @"</td>";
                    }
                    mstr = mstr + "</tr>";

                    for (int i = 0; i <= dv1.Count - 1; i++)
                    {
                        mstr = mstr + @"<tr>";
                        foreach (DataColumn colnam in dv1.Table.Columns)
                        {
                            mstr = mstr + @"<td>" + aa.chkdbnull(dv1[i][colnam.ColumnName]) + @"</td>";
                        }

                        mstr = mstr + @"</tr>";
                        cnt = cnt + 1;
                    }

                    dv1.Dispose();
                    mstr = mstr + @"</table>";



                }
                else mstr = @"<table><tr><td>1." + mmsg + @"</td></tr></table>";

                Label1.Text = "共:" + cnt + @"筆<br />" + mstr;
            }
            else
            {
                if (TextBox1.Text.Length > 0)
                {
                    msqlcom.Parameters.Clear();
                    msqlcom.CommandText = TextBox1.Text;
                    if (!aa.exec_param(msqlcom, ref mmsg)) Label1.Text = mmsg;

                }
                else Label1.Text = "";

            }


            cnt = 0;
            if (TextBox2.Text.Length > 0 && TextBox2.Text.Trim().IndexOf("select") == 0)
            {
                msqlcom.Parameters.Clear();
                msqlcom.CommandText = TextBox2.Text;
                dv1 = aa.dv_param(msqlcom, ref mmsg);
                if (dv1 != null)
                {
                    mstr = @"<table border=""1"" cellspacing=""0"" cellpadding=""0"" width=""100%"" ><tr>";
                    foreach (DataColumn colnam in dv1.Table.Columns)
                    {
                        mstr = mstr + "<td>" + colnam.ColumnName + "</td>";
                    }
                    mstr = mstr + "</tr>";

                    for (int i = 0; i <= dv1.Count - 1; i++)
                    {
                        mstr = mstr + "<tr>";
                        foreach (DataColumn colnam in dv1.Table.Columns)
                        {
                            mstr = mstr + "<td>" + dv1[i][colnam.ColumnName] + "</td>";
                        }

                        mstr = mstr + "</tr>";
                        cnt = cnt + 1;
                    }
                    dv1.Dispose();
                    mstr = mstr + "</table>";
                }
                else mstr = "<table><tr><td>1." + mmsg + "</td></tr></table>";

                Label2.Text = "共:" + cnt + "筆<br />" + mstr;
            }
            else
            {
                if (TextBox2.Text.Length > 0)
                {
                    msqlcom.Parameters.Clear();
                    msqlcom.CommandText = TextBox2.Text;
                    if (!aa.exec_param(msqlcom, ref mmsg)) Label2.Text = mmsg;
                }
                else Label2.Text = "";

            }

            cnt = 0;
            if (TextBox3.Text.Length > 0 && TextBox3.Text.ToString().Trim().IndexOf("select") == 0)
            {
                msqlcom.Parameters.Clear();
                msqlcom.CommandText = TextBox3.Text;
                dv1 = aa.dv_param(msqlcom, ref mmsg);
                if (dv1 != null)
                {
                    mstr = @"<table border=""1"" cellspacing=""0"" cellpadding=""0"" width=""100%"" ><tr>";
                    foreach (DataColumn colnam in dv1.Table.Columns)
                    {
                        mstr = mstr + "<td>" + colnam.ColumnName + "</td>";
                    }
                    mstr = mstr + "</tr>";

                    for (int i = 0; i <= dv1.Count - 1; i++)
                    {
                        mstr = mstr + "<tr>";
                        foreach (DataColumn colnam in dv1.Table.Columns)
                        {
                            mstr = mstr + "<td>" + dv1[i][colnam.ColumnName] + "</td>";
                        }

                        mstr = mstr + "</tr>";
                        cnt = cnt + 1;
                    }
                    dv1.Dispose();
                    mstr = mstr + "</table>";
                }
                else mstr = "<table><tr><td>1." + mmsg + "</td></tr></table>";

                Label3.Text = "共:" + cnt + "筆<br />" + mstr;
            }
            else
            {
                if (TextBox3.Text.Length > 0)
                {

                    msqlcom.Parameters.Clear();
                    msqlcom.CommandText = TextBox3.Text;
                    if (!aa.exec_param(msqlcom, ref mmsg)) Label3.Text = mmsg;

                }
                else Label3.Text = "";

            }
        }
    }


    protected void Button2_Click(Object sender, System.EventArgs e)
    {
        string mstr;
        int cnt = 0;
        msqlcom.Parameters.Clear();
        msqlcom.CommandText = "execute sp_tables @table_type = \"'TABLE'\"";
        if (dv1 != null)
        {
            mstr = @"<table border=""1"" cellspacing=""0"" cellpadding=""0"" width=""100%""  ><tr>";
            foreach (DataColumn colnam in dv1.Table.Columns) mstr = mstr + @"<td>" + colnam.ColumnName + @"</td>";

            mstr = mstr + "</tr>";

            for (int i = 0; i <= dv1.Count - 1; i++)
            {
                mstr = mstr + "<tr>";
                foreach (DataColumn colnam in dv1.Table.Columns)
                {
                    mstr = mstr + "<td>" + dv1[i][colnam.ColumnName] + "</td>";
                }
                mstr = mstr + "</tr>";
                cnt = cnt + 1;
            }
            dv1.Dispose();
            mstr = mstr + @"</table>";
        }
        else
        {
            mstr = "<table><tr><td>3." + mmsg + "</td></tr></table>";
        }

        Label4.Text = "共:" + cnt + "筆<br />" + mstr;
    }


    protected void Button3_Click(Object sender, System.EventArgs e)
    {
        string mstr;
        int cnt = 0;
        Label4.Text = "";
        msqlcom.Parameters.Clear();
        msqlcom.CommandText = "SELECT name FROM sys.triggers WHERE is_ms_shipped = 0";
        dv1 = aa.dv_param(msqlcom, ref mmsg);

        if (dv1 != null)
        {
            mstr = @"<table border=""1"" cellspacing=""0"" cellpadding=""0"" width=""100%""  ><tr>";
            foreach (DataColumn colnam in dv1.Table.Columns) mstr = mstr + "<td>" + colnam.ColumnName + "</td>";

            mstr = mstr + "</tr>";

            for (int i = 0; i <= dv1.Count - 1; i++)
            {
                mstr = mstr + "<tr>";
                foreach (DataColumn colnam in dv1.Table.Columns)
                {
                    mstr = mstr + "<td>" + dv1[i][colnam.ColumnName] + "</td>";
                }

                mstr = mstr + "</tr>";
                cnt = cnt + 1;
            }
            dv1.Dispose();
            mstr = mstr + "</table>";
        }
        else
        {
            mstr = "<table><tr><td>3." + mmsg + "</td></tr></table>";

        }
        Label4.Text = "共:" + cnt + "筆<br />" + mstr;
    }


    protected void Button4_Click(Object sender, System.EventArgs e)
    {
        string mstr;
        int cnt = 0;
        Label4.Text = "";
        msqlcom.Parameters.Clear();
        msqlcom.CommandText = "SELECT name FROM sys.procedures";
        dv1 = aa.dv_param(msqlcom, ref mmsg);
        if (dv1 != null)
        {
            mstr = @"<table border=""1"" cellspacing=""0"" cellpadding=""0"" width=""100%""  ><tr>";
            foreach (DataColumn colnam in dv1.Table.Columns)
            {
                mstr = mstr + "<td>" + colnam.ColumnName + "</td>";
            }
            mstr = mstr + "</tr>";

            for (int i = 0; i <= dv1.Count - 1; i++)
            {
                mstr = mstr + "<tr>";
                foreach (DataColumn colnam in dv1.Table.Columns)
                {
                    mstr = mstr + "<td>" + dv1[i][colnam.ColumnName] + "</td>";
                }

                mstr = mstr + "</tr>";
                cnt = cnt + 1;
            }
            dv1.Dispose();
            mstr = mstr + "</table>";
        }
        else
        {
            mstr = "<table><tr><td>3." + mmsg + "</td></tr></table>";
        }
        Label4.Text = "共:" + cnt + "筆<br />" + mstr;

    }


    protected void Button5_Click(Object sender, System.EventArgs e)
    {
        string mstr;
        int cnt = 0;
        Label4.Text = "";
        mstr = aa.chktyp(Request.Form["tables"], 1, "C", 0, 50, "Table名", ref mmsg);
        if (mmsg.Length > 0) mmsg2 = mmsg;
        else
        {
            msqlcom.Parameters.Clear();
            msqlcom.CommandText = "truncate table " + mstr;
            aa.exec_param(msqlcom, ref mmsg);
            mstr = "<table  class=\"table table-responsive\"><tr><td>3." + mmsg + "</td></tr></table>";
        }



    }


    protected void Button6_Click(Object sender, System.EventArgs e)
    {
        string mstr;
        int cnt = 0;
        Label4.Text = "";
        mstr = aa.chktyp(Request.Form["tables"], 1, "C", 0, 50, "Table名", ref mmsg);

        if (mmsg.Length > 0) mmsg2 = mmsg;
        else
        {
            msqlcom.Parameters.Clear();
            msqlcom.CommandText = "exec sp_changeobjectowner '" + mstr + "','dbo'";
            aa.exec_param(msqlcom, ref mmsg);
            mstr = "<table class=\"table table-responsive\"><tr><td>3." + mmsg + "</td></tr></table>";
        }

    }
    
}