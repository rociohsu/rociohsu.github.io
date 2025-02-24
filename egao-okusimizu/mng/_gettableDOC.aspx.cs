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
using System.Text;

public partial class _default : MLBasePage
{
    public DataView dv1;
    public int mpage, mc;
    public StringBuilder sp = new StringBuilder();
    public StringBuilder sp2 = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {

        init_class(); //初始化class含db connection, email環境設定
        if (Session["mmuno"] == null) Response.Redirect("ml_login.aspx?xurl=" + Request.ServerVariables["PATH_INFO"] + "&xpara=" + Request.ServerVariables["QUERY_STRING"].ToString().Replace("&", "***"));
        string[] mara = { "CABASE_AREA_CODE", "CABASE_CMSDTL","CABASE_GROUP", "CABASE_GROUP_ITO", "CABASE_IMG","CABASE_ITO","CABASE_SETUP","CABASE_SYS_LOG","CABASE_UNIT_TEST","CABASE_USER","CABASE_USER_GROUP","CABASE_USER_ITO" };
        string[] marb = { "後台-縣市區所資料", "後台-CMS內容資料", "後台-群組資料", "後台-群組之單元權限資料", "後台-圖片上傳舌管理", "後台-後台單元", "後台-系統參數設定", "後台-系統LOG記錄", "後台-單元測試", "後台-人員資料", "後台-人員所屬群組", "後台-人員之單元權限" };

        if (mara.Length != marb.Length) Response.Write("Array數量不相等");

        if (!chkath()) { Response.End(); }; //檢查是否有此頁面權限, 不足時 masterpage. content visable=false
        //以下開始coding...
        //Response.Write(YYurl + ";" );
        Response.Clear();
        Response.Buffer = true;
        Response.HeaderEncoding = Encoding.GetEncoding("UTF-8");
        Response.Charset = "UTF-8";
        Response.AppendHeader("Content-Disposition", "attachment;filename=schema.doc");

        string sql = string.Format("execute sp_tables @table_type = \"'TABLE'\"");
        DataSet ds = null;
        int mi = 1;
        SqlCommand msqlcom = new SqlCommand(), msqlcom2=new SqlCommand();
        msqlcom.CommandText = sql;
        using (DataView dv1 = aa.dv_param(msqlcom, ref mmsg))
        {

            ds = dv1.Table.DataSet;
            ds.Tables[0].TableName = "ALL";
            sp2.AppendLine("<table width=\"100%\" border=\"1\" style=\"margin-right:15px; border-collapse:collapse;line-height:150%;\"><tr style=\"background-color:#eeeeff\"><td>項次</td><td>Table 名稱</td><td>說明</td></tr>");
            foreach (DataRowView x in dv1)
            {
                string TABLE_NAME = x["TABLE_NAME"].ToString();
                sp.AppendLine("<a name=\"" + TABLE_NAME + "\"></a>TABLE名稱 :<b><font color=\"#0000ff\">" + TABLE_NAME + "</font></b>");  //書籤
                sp2.AppendLine("<tr><td>" + mi.ToString("#00") + "</td><td><a href=\"#" + TABLE_NAME + "\">" + TABLE_NAME + "</a></td><td style=\"width:50%\">");
                int gg = 0;
                for (int kk = 0; kk < mara.Length; kk++)
                {
                     if (TABLE_NAME.ToUpper().Trim() == mara[kk].ToUpper().Trim()) { sp2.AppendLine(marb[kk]); break; }
                     
                    
                }

                sp2.AppendLine("</td></tr>");  //索引

                mi++;
                sql = string.Format("sp_columns {0}", TABLE_NAME);
                msqlcom2.CommandText = sql;
                DataView dv2 = aa.dv_param(msqlcom2, ref mmsg);
                if (dv2 != null)
                {
                    if (dv2.Count > 0) sp.AppendLine("<table width=\"100%\" border=\"1\" style=\"margin-right:15px; border-collapse:collapse;line-height:150%;\"><tr style=\"background-color:#eeeeff\"><td valign=\"top\" nowrap=\"nowrap\"><p align=\"center\"><strong>欄位名稱</strong></p></td><td valign=\"top\" nowrap=\"nowrap\"><p align=\"center\"><strong>資料型態</strong></p></td><td valign=\"top\" nowrap=\"nowrap\"><p align=\"center\"><strong>資料長度</strong></p></td><td valign=\"top\" nowrap=\"nowrap\"><p align=\"center\"><strong>允許NULL</strong></p></td><td valign=\"top\" nowrap=\"nowrap\"><p align=\"center\"><strong>預設值</strong></p></td><td valign=\"top\" nowrap=\"nowrap\"><p align=\"center\"><strong>欄位說明</strong></p></td></tr>");
                    
                    for (int i = 0; i < dv2.Count; i++)
                    {
                        

                        sp.AppendLine("<tr>");
                        sp.AppendLine("<td style=\"width:15%\">" + aa.chkdbnull(dv2[i]["COLUMN_NAME"]) + "</td>");
                        sp.AppendLine("<td style=\"width:15%\">" + aa.chkdbnull(dv2[i]["TYPE_NAME"]) + "</td>");
                        sp.AppendLine("<td style=\"width:10%\">" + aa.chkdbnull(dv2[i]["PRECISION"]) + "</td>");
                        if (aa.chkdbnull(dv2[i]["NULLABLE"])=="1") sp.AppendLine("<td style=\"width:5%\">Y</td>");
                        else sp.AppendLine("<td style=\"width:5%\">否</td>");
                        sp.AppendLine("<td style=\"width:15%\">" + aa.chkdbnull(dv2[i]["COLUMN_DEF"]).Replace("(getdate())","getdate()").Replace("((","(").Replace("))",")") + " </td>");

                        if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("crt_dat") >= 0 || aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("crt_usr") >= 0 || aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("crt_uno") >= 0 || aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("upd_dat") >= 0 || aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("upd_usr") >= 0 || aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("upd_uno") >= 0)
                        {
                            if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("crt_dat") >= 0) sp.AppendLine("<td>建 檔 日 期</td></tr>");
                            else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("crt_uno") >= 0) sp.AppendLine("<td>建檔人編號</td></tr>");
                            else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("crt_usr") >= 0) sp.AppendLine("<td>建 檔 人</td></tr>");
                            else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("upd_dat2") >= 0) sp.AppendLine("<td>前二次異動日期</td></tr>");
                            else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("upd_uno2") >= 0) sp.AppendLine("<td>前二次異動人編號</td></tr>");
                            else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("upd_usr2") >= 0) sp.AppendLine("<td>前二次異動人姓名</td></tr>");
                            else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("upd_dat3") >= 0) sp.AppendLine("<td>前三次異動日期</td></tr>");
                            else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("upd_uno3") >= 0) sp.AppendLine("<td>前三次異動人編號</td></tr>");
                            else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("upd_usr3") >= 0) sp.AppendLine("<td>前三次異 動 人</td></tr>");
                            else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("upd_dat") >= 0) sp.AppendLine("<td>最後異動日期</td></tr>");
                            else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("upd_uno") >= 0) sp.AppendLine("<td>最後異動人編號</td></tr>");
                            else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("upd_usr") >= 0) sp.AppendLine("<td>最後異動人姓名</td></tr>");

                        }
                        else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]).ToLower().IndexOf("sno") >= 0) { sp.AppendLine("<td>系統主要流水編號</td></tr>"); }
                        else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"])=="ONF") { sp.AppendLine("<td>前台呈現 1=是, 0=否</td></tr>"); }
                        else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]) == "SDT") { sp.AppendLine("<td>起始日期</td></tr>"); }
                        else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]) == "EDT") { sp.AppendLine("<td>結束日期</td></tr>"); }
                        else if (aa.chkdbnull(dv2[i]["COLUMN_NAME"]) == "TYP") { sp.AppendLine("<td>分類</td></tr>"); }

                        else sp.AppendLine("<td>" + aa.chkdbnull(dv2[i]["REMARKS"]) + "</td></tr>");
                    }
                    dv2.Dispose();
                    sp.AppendLine("</table><br>");
                }

            }
            ds.Dispose();
            sp2.AppendLine("</table>");
        }
        if (msqlcom != null) msqlcom.Dispose();
        if (msqlcom2 != null) msqlcom2.Dispose();
    }
    
    
}