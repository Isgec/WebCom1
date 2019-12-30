<%@ Page Title="" Language="VB" MasterPageFile="~/Sample.master" AutoEventWireup="false" CodeFile="GF_dmsEntry.aspx.vb" Inherits="DMS_GF_dmsEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">

  <div class="container">
    <div class="row" style="margin-top: 10px;">
      <div class="col-sm-12 text-center">
        <h3>Configure DMS for Transmittal</h3>
      </div>
    </div>
    <div class="row btn-dark" style="margin: 10px;">
      <div class="col-sm-12">
        <h4>Projects</h4>
      </div>
    </div>
    <asp:UpdatePanel ID="upd1" runat="server">
      <ContentTemplate>
    <div class="row" style="margin: 10px; background-color:lavender;border:1pt solid #0094ff;">
      <div class="col-sm-5">
        <h5>Add New Project:</h5>
      </div>
      <div class="col-sm-1">
        Transmittal :
      </div>
      <div class="col-sm-2">
        <select id="X_t_type" style="width: 150px;" runat="server"  ClientIDMode="Static">
          <option value="1">1-Customer</option>
          <option value="2">2-Internal</option>
          <option value="3">3-Site</option>
          <option value="4">4-Vendor</option>
        </select>
      </div>
      <div class="col-sm-1 text-right">
        Project :
      </div>
      <div class="col-sm-1">
        <input type="text" id="X_t_cprj" style="width: 100px;" maxlength="6" runat="server"  ClientIDMode="Static" />
      </div>
      <div class="col-sm-2 text-right">
        <input type="submit" class="btn btn-sm btn-primary" id="cmdInsertProject" value="SAVE" runat="server"  ClientIDMode="Static" />
      </div>
    </div>
        <div class="row">
          <div class="col-sm-12">
            <asp:GridView ID="gvDmsH" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="ODSdmsH" ForeColor="#333333" GridLines="Horizontal" Width="100%" DataKeyNames="t_type,t_cprj" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
              <Columns>
                <asp:CommandField ShowSelectButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-warning" >
                <ControlStyle CssClass="btn btn-sm btn-warning" />
                </asp:CommandField>
                <asp:BoundField DataField="t_type" ItemStyle-HorizontalAlign="Center" HeaderText="Transmittal Type" SortExpression="t_type" ReadOnly="True" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="t_cprj" HeaderText="Project" SortExpression="t_cprj" ReadOnly="True" />
                <asp:BoundField DataField="t_rfld" HeaderText="Root Folder" SortExpression="t_rfld" />
                <asp:BoundField DataField="t_rfid" ItemStyle-HorizontalAlign="Center" HeaderText="Root Folder ID" SortExpression="t_rfid" InsertVisible="False" ReadOnly="True" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="t_pfld" HeaderText="Project Folder" SortExpression="t_pfld" />
                <asp:BoundField DataField="t_pfid" HeaderText="Project Folder ID" InsertVisible="False" ReadOnly="True" SortExpression="t_pfid" />
                <asp:CommandField ShowEditButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-info" >
                <ControlStyle CssClass="btn btn-sm btn-info" />
                </asp:CommandField>
                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-danger" >
                <ControlStyle CssClass="btn btn-sm btn-danger" />
                </asp:CommandField>
                <asp:TemplateField ShowHeader="False">
                  <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-sm btn-success" CausesValidation="false" CommandName="UpdateInDMS" Text="Update in DMS" CommandArgument='<%# Container.DataItemIndex %>'  />
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
              <EditRowStyle BackColor="#999999" />
              <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
              <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
              <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
              <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
              <SortedAscendingCellStyle BackColor="#E9E7E2" />
              <SortedAscendingHeaderStyle BackColor="#506C8C" />
              <SortedDescendingCellStyle BackColor="#FFFDF8" />
              <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ODSdmsH" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectList" TypeName="SIS.DMS.erpDMSH" DataObjectTypeName="SIS.DMS.erpDMSH" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update"></asp:ObjectDataSource>
            <span style="font-style:italic;">[Click on SELECT button for Project Folders]</span>
          </div>
        </div>
            <br />
            <br />
        <div class="row btn-dark" style="margin:10px;">
          <div class="col-sm-6">
            <h4>Project Folders</h4>
          </div>
          <div class="col-sm-2">
            Selected Type
          </div>
          <div class="col-sm-1">
            <asp:Label ID="F_t_type" runat="server"  ClientIDMode="Static" Text="0"></asp:Label>
          </div>
          <div class="col-sm-2">
            Selected Project
          </div>
          <div class="col-sm-1">
            <asp:Label ID="F_t_cprj" runat="server"  ClientIDMode="Static"></asp:Label>
          </div>
        </div>
    <div class="row" style="margin: 10px; background-color:lavender;border:1pt solid #0094ff;">
      <div class="col-sm-6">
        <h5>Add New Folder:</h5>
      </div>
      <div class="col-sm-1 text-right">
        Folder :
      </div>
      <div class="col-sm-3">
        <input type="text" id="X_t_tfld" style="width: 300px;" maxlength="250" runat="server"  ClientIDMode="Static" />
      </div>
      <div class="col-sm-2 text-right">
        <input type="submit" class="btn btn-sm btn-primary" id="cmdInsertFolder" value="SAVE" runat="server"  ClientIDMode="Static" />
      </div>
    </div>
        <div class="row">
          <div class="col-sm-12">
            <asp:GridView ID="gvDmsD" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="ODSdmsD" ForeColor="#333333" GridLines="None" Width="100%" ClientIDMode="Static" DataKeyNames="t_type,t_cprj,t_srno" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" EmptyDataText="NO Record Found" ShowHeaderWhenEmpty="True">
              <AlternatingRowStyle BackColor="White" />
              <Columns>
                <asp:BoundField DataField="t_type" HeaderText="Transmittal Type" SortExpression="t_type" InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="t_cprj" HeaderText="Project ID" SortExpression="t_cprj" ReadOnly="True" />
                <asp:BoundField DataField="t_srno" HeaderText="Serial No" SortExpression="t_srno" ReadOnly="True" />
                <asp:BoundField DataField="t_tfld" HeaderText="Transmittal Folder" SortExpression="t_tfld" />
                <asp:BoundField DataField="t_tfid" HeaderText="Transmittal Folder ID" SortExpression="t_tfid" ReadOnly="True" />
                <asp:CommandField ShowEditButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-info" />
                <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-danger" />
              </Columns>
              <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
              <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
              <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
              <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
              <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
              <SortedAscendingCellStyle BackColor="#FDF5AC" />
              <SortedAscendingHeaderStyle BackColor="#4D0000" />
              <SortedDescendingCellStyle BackColor="#FCF6C0" />
              <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ODSdmsD" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="SelectList" TypeName="SIS.DMS.erpDMSD" DataObjectTypeName="SIS.DMS.erpDMSD" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
              <SelectParameters>
                <asp:ControlParameter ControlID="F_t_type" DefaultValue="0" Name="t_type" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="F_t_cprj" DefaultValue="" Name="t_cprj" PropertyName="Text" Type="String" />
              </SelectParameters>
            </asp:ObjectDataSource>
          </div>
        </div>
      </ContentTemplate>
      <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gvDmsH" EventName="SelectedIndexChanged" />
      </Triggers>
    </asp:UpdatePanel>
  </div>
  <br />
  <br />
  <br />
</asp:Content>

