<%@ Page Language="VB" MasterPageFile="~/Sample.master" AutoEventWireup="False" CodeFile="mTDSAttachments.aspx.vb" Inherits="mTDSAttachments" title="List: Attachments" %>
<asp:Content ID="None" ContentPlaceHolderID="cphMain" runat="server">
<style>
.switch {
  position: relative;
  display: inline-block;
  width: 50px;
  height: 24px;
}

.switch input {display:none;}

.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #ccc;
  -webkit-transition: .4s;
  transition: .4s;
}

.slider:before {
  position: absolute;
  content: "";
  height: 16px;
  width: 16px;
  left: 4px;
  bottom: 4px;
  background-color: white;
  -webkit-transition: .4s;
  transition: .4s;
}

input:checked + .slider {
  background-color: #2196F3;
}

input:focus + .slider {
  box-shadow: 0 0 1px #2196F3;
}

input:checked + .slider:before {
  -webkit-transform: translateX(26px);
  -ms-transform: translateX(26px);
  transform: translateX(26px);
}

/* Rounded sliders */
.slider.round {
  border-radius: 24px;
}

.slider.round:before {
  border-radius: 50%;
}
</style>
<style>
    a.transparent-input{
       background-color:rgba(0,0,0,0) !important;
       border:none !important;
    }
    span.transparent-input{
       background-color:rgba(0,0,0,0) !important;
       border:none !important;
    }
</style>
</asp:Content>
<asp:Content ID="CPHdmisg121" ContentPlaceHolderID="cph1" runat="Server">
  <div class="container-fluid">
    <div class="row">
      <div class="col-sm-12 text-center">
        <h3>
          <asp:Label ID="Labeldmisg121" runat="server" Text="TDS Attachments List"></asp:Label></h3>
      </div>
    </div>
    <asp:Button ID="cmdDelete" ClientIDMode="static" runat="server" Style="display: none;" />
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
                <asp:Button ID="cmdSearch" runat="server" Enabled="false" CssClass="btn btn-dark" Text="Search" />
              </div>
            </div>
            <iframe id="xFrame" name="xFrame" style="height: 20px; width: 20px; display: none;"></iframe>
            <div class="container-fluid chartDiv">
              <div class="row">
                <div class="col-sm-12 text-center">
                  <h5>Selected Documents for Download
                  </h5>
                </div>
              </div>
              <div class="row">
                <div class="col-sm-1"></div>
                <div class="col-sm-10 text-center" style="overflow-y: scroll; height: 100px;">
                  <asp:CheckBoxList
                    ID="lstSelected"
                    ClientIDMode="static"
                    Width="100%"
                    RepeatColumns="3"
                    RepeatDirection="Horizontal"
                    runat="server">
                  </asp:CheckBoxList>
                </div>
                <div class="col-sm-1"></div>
              </div>
              <div class="row">
                <div class="col-sm-3"></div>
                <div class="col-sm-2">
                  <asp:Button ID="cmdRemove" runat="server" CssClass="btn-danger" Text="Remove" />
                </div>
                <div class="col-sm-2"></div>
                <div class="col-sm-2">
                  <asp:Button ID="cmdDownload" runat="server" CssClass="btn-success" Text="Download" />
                </div>
                <div class="col-sm-3"></div>
              </div>
            </div>
            <asp:GridView
              ID="GVdmisg121"
              Width="98%"
              runat="server"
              AllowPaging="True"
              AllowSorting="True"
              PagerSettings-Position="TopAndBottom"
              DataSourceID="ODSdmisg121"
              DataKeyNames="t_indx"
              HeaderStyle-BackColor="Black"
              HeaderStyle-ForeColor="White"
              AutoGenerateColumns="False">
              <Columns>
                <asp:TemplateField HeaderText="Attachment" SortExpression="t_fnam">
                  <ItemTemplate>
                    <a id="lnk" runat="server" href='<%# Eval("GetDownloadLink") %>' target="xFrame"><%# EVal("t_fnam") %></a>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Card No">
                  <ItemTemplate>
                    <asp:Label ID="Label" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text='<%# Eval("cardno") %>'></asp:Label>
                  </ItemTemplate>
                  <ItemStyle CssClass="alignCenter" />
                  <HeaderStyle CssClass="alignCenter" />
                </asp:TemplateField>
                <asp:TemplateField>
                  <HeaderTemplate>
                    <asp:Button ID="L_SelectAll" runat="server" Text="SelectAll" CommandArgument="SelectAll" CommandName="Sort"></asp:Button>
                  </HeaderTemplate>
                  <ItemTemplate>
                    <asp:Button ID="cmdSelect" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text="Select" CommandName="cmdSelect" CommandArgument='<%# Container.DataItemIndex %>'></asp:Button>
                  </ItemTemplate>
                  <ItemStyle CssClass="alignCenter" />
                  <HeaderStyle CssClass="alignCenter" />
                </asp:TemplateField>
                <asp:TemplateField>
                  <HeaderTemplate>
                    <div class="row">
                      <div class="col-sm-6 text-left">
                        <asp:Label ID="L_Desc" runat="server" Text="Description"></asp:Label>
                      </div>
                      <div class="col-sm-4 text-right">
                        <h6>Page Size:</h6>
                      </div>
                      <div class="col-sm-2 text-right">
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
                    <asp:Label ID="Labelt_dsca" runat="server" ForeColor='<%# Eval("ForeColor") %>' Text='<%# EVal("EmployeeName") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
              </Columns>
              <EmptyDataTemplate>
                <asp:Label ID="LabelEmpty" runat="server" Font-Size="Small" ForeColor="Red" Text="No record found !!!"></asp:Label>
              </EmptyDataTemplate>
            </asp:GridView>
            <asp:ObjectDataSource
              ID="ODSdmisg121"
              runat="server"
              DataObjectTypeName="SIS.TDS.tdsAttachment"
              OldValuesParameterFormatString="original_{0}"
              SelectMethod="SelectList"
              SelectCountMethod="SelectCount"
              TypeName="SIS.TDS.tdsAttachment"
              SortParameterName="OrderBy"
              EnablePaging="True">
              <SelectParameters>
                <asp:ControlParameter ControlID="F_SearchText" PropertyName="Text" Name="SearchText" Type="String" DefaultValue="" Size="250" />
                <asp:Parameter Name="SearchState" Type="Boolean" Direction="Input" DefaultValue="false" />
              </SelectParameters>
            </asp:ObjectDataSource>
          </ContentTemplate>
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cmdSearch" />
            <asp:PostBackTrigger ControlID="cmdDownload" />
          </Triggers>
        </asp:UpdatePanel>

      </div>
    </div>
  </div>
</asp:Content>
