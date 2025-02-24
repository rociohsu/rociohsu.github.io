<%@ Page Language="C#" MasterPageFile="_ML.master" CodeFile="_getTable.aspx.cs" Inherits="_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="mng_main" class="forPrinted" runat="server">
        
       <form id="form1" runat="server">
    <asp:Repeater ID="Repeater1" runat="server">
        
        <ItemTemplate>
            TABLE NAME : <%#Eval("TABLE_NAME") %>
            <asp:Repeater ID="Repeater2" runat="server" DataSource='<%#  ((System.Data.DataRowView)Container.DataItem).Row.GetChildRows("myrelation") %>'>
                <HeaderTemplate>
                <table width="100%" border="1">
              <tr>
                <td valign="top" nowrap="nowrap"><p align="center"><strong>Field Name</strong></p></td>
                <td valign="top" nowrap="nowrap"><p align="center"><strong>Data Type</strong></p></td>
                <td valign="top" nowrap="nowrap"><p align="center"><strong>length</strong></p></td>
                <td valign="top" nowrap="nowrap"><p align="center"><strong>Null</strong></p></td>
                <td valign="top" nowrap="nowrap"><p align="center"><strong>Default</strong></p></td>
                <td valign="top" nowrap="nowrap"><p align="center"><strong>Note</strong></p></td>
              </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                <td><%#(DataBinder.Eval(Container.DataItem, "[\"COLUMN_NAME\"]"))%></td>
                <td><%#DataBinder.Eval(Container.DataItem, "[\"TYPE_NAME\"]")%></td>
                <td><%#DataBinder.Eval(Container.DataItem, "[\"PRECISION\"]")%></td>
                <td><%#(DataBinder.Eval(Container.DataItem, "[\"NULLABLE\"]").ToString()=="1")?"Y":"N"%></td>
                <td><%#DataBinder.Eval(Container.DataItem, "[\"COLUMN_DEF\"]")%></td>
                <td><%#DataBinder.Eval(Container.DataItem, "[\"REMARKS\"]")%></td>
                </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                    <br />
                </FooterTemplate>

            </asp:Repeater>
              

        </ItemTemplate>
        <FooterTemplate>

            </table>
        </FooterTemplate>
    </asp:Repeater>
           </form>
        </div>
</asp:Content>

