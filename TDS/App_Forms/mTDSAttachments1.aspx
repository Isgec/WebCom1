<%@ Page Language="VB" MasterPageFile="~/Sample.master" AutoEventWireup="False" CodeFile="mTDSAttachments1.aspx.vb" Inherits="mTDSAttachments1" title="List: Attachments" %>
<asp:Content ID="None" ContentPlaceHolderID="cphMain" runat="server">
</asp:Content>
<asp:Content ID="CPHdmisg121" ContentPlaceHolderID="cph1" runat="Server">
  <div class="container-fluid">
    <div class="row">
      <div class="col-sm-12 text-center">
        <h3>
          <asp:Label ID="Labeldmisg121" runat="server" Text="TDS Attachments List"></asp:Label></h3>
      </div>
    </div>
    <asp:Button ID="cmdRefresh" ClientIDMode="static" Text="Refresh" runat="server" />
    <div class="row">
      <div class="col-sm-12">
        <asp:UpdatePanel ID="UPNLdmisg121" runat="server">
          <ContentTemplate>
            <LGM:ToolBar0
              ID="TBLdmisg121200"
              ToolType="lgNMGrid"
              SVisible="false"
              runat="server" />
            <div class="form-group">
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">Search :</span>
                </div>
                <asp:TextBox
                  ID="F_SearchText"
                  CssClass="form-control"
                  onfocus="return this.select();"
                  runat="Server" />
                <asp:Button ID="cmdSearch" runat="server" CssClass="btn btn-dark" Text="Search" />
              </div>
              <div class="input-group mb-3">
                <div class="input-group-prepend">
                  <span class="input-group-text">Show Locked Records :</span>
                </div>
                <asp:CheckBox
                  ID="F_Freezed"
                  CssClass="form-control"
                  AutoPostBack="true"
                  runat="Server" />
                <div class="input-group-prepend">
                  <span class="input-group-text">Fin. Year :</span>
                </div>
                <asp:Label ID="F_FinYear" runat="server" CssClass="btn btn-outline-warning" Text="2018-19" />
              </div>
            </div>
            <iframe id="xFrame" name="xFrame" style="height: 20px; width: 20px; display: none;"></iframe>
            <asp:GridView
              ID="GVdmisg121"
              Width="98%"
              runat="server"
              AllowPaging="True"
              AllowSorting="True"
              PagerSettings-Position="TopAndBottom"
              DataSourceID="ODSdmisg121"
              DataKeyNames="FinYear,CardNo"
              HeaderStyle-BackColor="Black"
              HeaderStyle-ForeColor="White"
              AutoGenerateColumns="False">
              <Columns>
                <asp:TemplateField HeaderText="Card No">
                  <ItemTemplate>
                    <asp:Label ID="Label" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text='<%# Eval("cardno") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle CssClass="alignCenter" />
                  <HeaderStyle CssClass="alignCenter" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Employee Name">
                  <ItemTemplate>
                    <asp:Label ID="LabelEmployeeName" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text='<%# EVal("HRM_Employees1_EmployeeName") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                  <HeaderTemplate>
                    <div class="row">
                      <div class="col-sm-4 text-right">
                        <h6>Page Size:</h6>
                      </div>
                      <div class="col-sm-8 text-right">
                        <asp:DropDownList ID="D_PageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PageSizeChanged">
                          <asp:ListItem Text="--" Value="0"></asp:ListItem>
                          <asp:ListItem Text="10" Value="10"></asp:ListItem>
                          <asp:ListItem Text="25" Value="25"></asp:ListItem>
                          <asp:ListItem Text="50" Value="50"></asp:ListItem>
                          <asp:ListItem Text="75" Value="75"></asp:ListItem>
                          <asp:ListItem Text="100" Value="100"></asp:ListItem>
                        </asp:DropDownList>
                      </div>
                    </div>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Button ID="cmdDownload" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text="Download" CommandName="cmdDownload" CommandArgument='<%# Container.DataItemIndex %>'></asp:Button>
                  </ItemTemplate>
                  <ItemStyle CssClass="alignCenter" />
                  <HeaderStyle CssClass="alignCenter" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Lock">
                  <ItemTemplate>
                    <asp:ImageButton ID="cmdInitiateWF" ValidationGroup='<%# "Initiate" & Container.DataItemIndex %>' CausesValidation="true" runat="server" Visible='<%# EVal("InitiateWFVisible") %>' Enabled='<%# EVal("InitiateWFEnable") %>' AlternateText='<%# EVal("PrimaryKey") %>' ToolTip="Lock" SkinID="forward" OnClientClick='<%# "return Page_ClientValidate(""Initiate" & Container.DataItemIndex & """) && confirm(""Lock record ?"");" %>' CommandName="InitiateWF" CommandArgument='<%# Container.DataItemIndex %>' />
                  </ItemTemplate>
                  <ItemStyle CssClass="alignCenter" />
                  <HeaderStyle CssClass="alignCenter"/>
                </asp:TemplateField>
              </Columns>
              <EmptyDataTemplate>
                <asp:Label ID="LabelEmpty" runat="server" Font-Size="Small" ForeColor="Red" Text="No record found !!!"></asp:Label>
              </EmptyDataTemplate>
            </asp:GridView>
            <asp:ObjectDataSource
              ID="ODSdmisg121"
              runat="server"
              DataObjectTypeName="SIS.TDS.tdsAttachmentUser"
              OldValuesParameterFormatString="original_{0}"
              SelectMethod="tdsAttachmentUserSelectList"
              SelectCountMethod="tdsAttachmentUserSelectCount"
              TypeName="SIS.TDS.tdsAttachmentUser"
              SortParameterName="OrderBy"
              EnablePaging="True">
              <SelectParameters>
                <asp:ControlParameter ControlID="F_FinYear" PropertyName="Text" Name="FinYear" Type="String" Size="10" />
                <asp:ControlParameter ControlID="F_Freezed" PropertyName="Checked" Name="Freezed" Type="Boolean" Size="2" />
                <asp:ControlParameter ControlID="F_SearchText" PropertyName="Text" Name="SearchText" Type="String" DefaultValue="" Size="250" />
                <asp:Parameter Name="SearchState" Type="Boolean" Direction="Input" DefaultValue="false" />
              </SelectParameters>
            </asp:ObjectDataSource>
          </ContentTemplate>
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cmdSearch" />
            <asp:PostBackTrigger ControlID="GVdmisg121" />
          </Triggers>
        </asp:UpdatePanel>

      </div>
    </div>
  </div>
</asp:Content>
