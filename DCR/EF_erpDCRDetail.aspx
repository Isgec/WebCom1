<%@ Page Language="VB" MasterPageFile="~/Sample.master" AutoEventWireup="false" CodeFile="EF_erpDCRDetail.aspx.vb" Inherits="EF_erpDCRDetail" title="Download Latest Released Document" %>
<asp:Content ID="CPHediWTmtlH" ContentPlaceHolderID="cph1" runat="Server">
  <div class="container text-center" style="margin-top: 10px;">
      <div class="row">
        <div class="col-sm-2"></div>
        <div class="col-sm-8 boxDiv">
          <%--Row 1--%>
          <div class="row"  style="margin-top: 15px;">
            <div class="col-sm-12">
              <asp:Image ID="logo" runat="server" AlternateText="ISGEC" ImageUrl="~/Images/reportLogo.png" />

            </div>
          </div>

          <div class="row">
            <div class="col-sm-12">

            <h3>DOWNLOAD ISGEC DOCUMENT</h3>
              </div>
          </div>

          <%--Row 3--%>
          <div class="row">
            <div class="col-sm-12 text-center">
              <p>
                Click on link given below to download the document.
              </p>
              <p>
                The link will expire on <asp:Label ID="dt" runat="server" Font-Bold="true"></asp:Label>
              </p>
              <p>
                <asp:Button ID="cmdDownload" CssClass="btn btn-info" runat="server" ToolTip="Download"  />
              </p>
            </div>
          </div>
        </div>
        <div class="col-sm-2"></div>
      </div>

      </div>


</asp:Content>
