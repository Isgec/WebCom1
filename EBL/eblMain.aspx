<%@ Page Language="VB" MasterPageFile="~/bs.master" AutoEventWireup="false" CodeFile="eblMain.aspx.vb" Inherits="eblMain" title="E-Bill Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" ClientIDMode="Static" runat="Server">


  <div class="container-fluid">
    <div style="display:flex;flex-direction:column;">
      <div class='row'>
        <div class='col'>
          <b>
            <asp:Label ID="Label1" ForeColor="#CC6633" runat="server" Text="Authenticate and Get Access Token :" /></b>
        </div>
        <div class='col'>
          <input type="button" class="btn btn-danger btn-sm" value="Access Token" onclick="myFetch();" />
        </div>
      </div>
    </div>
  </div>
  <div id="dmsAlert" style="display:none;padding:20px;border:1pt solid black; background-color:lightgray; border-radius:30px; box-shadow:10px 10px 10px 10px darkgray;color:black; font-weight:bold;font-size:14px;">
  </div>
  <%--Prototypes for E-Bill Request--%>
  <script>
    function eblRequest() {
      this.access_token = '';
      this.user_gstin = '';
      this.data_source = '';
      this.transaction_details = new transaction_details();
      this.document_details = new document_details();
      this.seller_details = new seller_details();
      this.buyer_details = new buyer_details();
      this.dispatch_details = new dispatch_details();
      this.ship_details = new ship_details();
      this.export_details = new export_details();
      this.payment_details = new payment_details();
      this.reference_details = new reference_details();
      this.value_details = new value_details();
      this.item_list = []; // new item_list();
    }
    function transaction_details() {
      this.category_of_transaction = '';
      this.charge_type = '';
      this.transaction_type = '';
      this.ecommerce_transaction = '';
      this.ecommerce_gstin = '';
    }
    function document_details() {
      this.document_type = '';
      this.document_number = '';
      this.document_date = '';
      this.original_document_number = '';
    }
    function seller_details() {
      this.gstin = '';
      this.trade_name = '';
      this.building_number = '';
      this.building_name = '';
      this.floor_number = '';
      this.location = '';
      this.district = '';
      this.pincode = '';
      this.state_code = '';
      this.phone_number = '';
      this.email = '';
    }
    function buyer_details() {
      this.gstin = '';
      this.trade_name = '';
      this.building_number = '';
      this.building_name = '';
      this.floor_number = '';
      this.location = '';
      this.district = '';
      this.pincode = '';
      this.state_code = '';
      this.phone_number = '';
      this.email = '';
    }
    function dispatch_details() {
      this.gstin = '';
      this.trade_name = '';
      this.building_number = '';
      this.building_name = '';
      this.floor_number = '';
      this.location = '';
      this.district = '';
      this.pincode = '';
      this.state_code = '';
      this.phone_number = '';
      this.email = '';
    }
    function ship_details() {
      this.gstin = '';
      this.trade_name = '';
      this.building_number = '';
      this.building_name = '';
      this.floor_number = '';
      this.location = '';
      this.district = '';
      this.pincode = '';
      this.state_code = '';
      this.phone_number = '';
      this.email = '';
    }
    function export_details() {
      this.export_category = '';
      this.ship_bill_number = '';
      this.ship_bill_date = '';
      this.country_code = '';
      this.foreign_currency = '';
      this.total_invoice_value_in_foreign_currency = '';
      this.port_code = '';
      this.export_payment = '';
    }
    function payment_details() {
      this.account_details = '';
      this.paid_balance_amount = '';
      this.credit_days = '';
      this.credit_transfer = '';
      this.direct_debit = '';
      this.branch_or_ifsc = '';
      this.payment_mode = '';
      this.payee_name = '';
      this.payment_due_date = '';
      this.payment_instruction = '';
      this.payment_term = '';
    }
    function reference_details() {
      this.contract_reference_number = '';
      this.other_reference = '';
      this.invoice_period_start_date = '';
      this.invoice_period_end_date = '';
      this.invoice_reference_number = '';
      this.invoice_remarks = '';
      this.vendor_po_reference_number = '';
      this.preceding_invoice_date = '';
      this.preceding_invoice_number = '';
      this.project_reference_number = '';
      this.receipt_advice_number = '';
      this.batch_reference_number = '';
    }
    function value_details() {
      this.total_assessable_value = '';
      this.total_cgst_value = '';
      this.total_sgst_value = '';
      this.total_igst_value = '';
      this.total_cess_value = '';
      this.total_cess_nonadvol_value = '';
      this.total_invoice_value = '';
      this.total_cess_value_of_state = '';
      this.discount = '';
      this.other_charge = '';
    }
    function item_list() {
      this.product_name = '';
      this.product_description = '';
      this.hsn_code = '';
      this.bar_code = '';
      this.quantity = '';
      this.free_quantity = '';
      this.unit = '';
      this.unit_price = '';
      this.total_amount = '';
      this.discount = '';
      this.other_charge = '';
      this.assessable_value = '';
      this.cgst_rate = '';
      this.sgst_rate = '';
      this.igst_rate = '';
      this.cess_rate = '';
      this.cess_nonadvol_value = '';
      this.state_cess_rate = '';
      this.total_item_value = '';
      this.batch_details = '';
      this.name = '';
      this.expiry_date = '';
      this.warranty_date = '';
    }
    function eblResponse() {
      this.message = new eblMessage();
      this.errorMessage = '';
      this.status = 'Success';
      this.code = '200';
      this.requestId = '';
    }
    function eblMessage() {
      this.AckNo = '';
      this.AckDt = '';
      this.Irn = '';
      this.SignedInvoice = '';
      this.SignedQRCode = '';
      this.Status = '';
      this.error = false;
    }
    function eblError() {
      this.message = '';
      this.errorMessage = '2150: Duplicate IRN';
      this.status = 'Failed';
      this.code = '204';
      this.requestId = '';
    }
  </script>
  <%--Prototypes for Access Token Request--%>
  <script>
    function acRequest() {
      this.username = 'testeway@mastersindia.co';
      this.password = 'Test@1234';
      this.client_id = 'fIXefFyxGNfDWOcCWn';
      this.client_secret = 'QFd6dZvCGqckabKxTapfZgJc';
      this.grant_type = 'password';
    }
    function acToken() {
      this.access_token = '';
      this.expires_in = 0;
      this.token_type = '';
    }
    function acError() {
      this.error = '';
      this.error_description = '';
    }
  </script>
  <%--Action Scripts--%>
  <script>
    var eblScript = {
      site: 'https://clientbasic.mastersindia.co',
      acurl: function() {
        return this.site + '/oauth/access_token';
      },
      eblurl: function () {
        return this.site + '/generateEinvoice';
      },
      failed: function (z) {
        if (z != '') {
          alert(z);
        }
      },
      token: new acToken(),
      getToken: function () {
        var act = new acRequest();
        var acr = JSON.stringify(act);
        var that = this;
        $.ajax({
          method: "POST",
          url: "https://clientbasic.mastersindia.co/oauth/access_token",
          dataType: "json",
          cache: false,
          headers: {
            "Content-Type": "application/json",
            "User-Agent": "PostmanRuntime/7.22.0",
            "Accept": "*/*",
            "Cache-Control": "no-cache",
            "Postman-Token": "75623e9b-c746-4547-9d8f-bc9b018cb621",
            "Host": "clientbasic.mastersindia.co",
            "Accept-Encoding": "gzip, deflate, br",
            "Content-Length": "176",
            "Cookie": "AWSALB=jb1DUXI9LxJadmtV0iZZOJ1Q+IXRaa/wmsIeugZl3eEbDyuSA3WPStQTsd3qCavWspWFYuFgVHocsbPMWAiISe2ZOTHawisiG0cj+kj4hDXsfW5zQ+qKh7iR7Oz3; AWSALBCORS=jb1DUXI9LxJadmtV0iZZOJ1Q+IXRaa/wmsIeugZl3eEbDyuSA3WPStQTsd3qCavWspWFYuFgVHocsbPMWAiISe2ZOTHawisiG0cj+kj4hDXsfW5zQ+qKh7iR7Oz3",
            "Connection": "keep-alive"
          },
          data: { "username": "testeway@mastersindia.co", "password": "Test@1234", "client_id": "", "client_secret": "", "grant_type": "" },
          contentType: "application/json",
        }).done(function (data, status, xhr) {
          alert('OK');
        }).fail(function (xhr,status,err) {
          alert('FAIL');
        });

      },
    }
//    data: {"username": "testeway@mastersindia.co","password": "Test@1234","client_id": "fIXefFyxGNfDWOcCWn","client_secret": "QFd6dZvCGqckabKxTapfZgJc","grant_type": "password"},

  </script>
  <script>
    function myPost() {
      var json_upload = "json_name=" + JSON.stringify({ name: "Ngoc Minh", time: "2:30 pm" });
      var xhttp = new XMLHttpRequest();
      xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
          alert(this.responseText);
        }
      };
      xhttp.open("POST", "https://clientbasic.mastersindia.co/oauth/access_token");
      xhttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
      xhttp.send(json_upload);
    }
  </script>
  <script>
    function myFetch() {
      var json_upload = "json_name=" + JSON.stringify({ name: "Ngoc Minh", time: "2:30 pm" });
      fetch("https://clientbasic.mastersindia.co/oauth/access_token",
        {
          method: 'post',
          mode: 'no-cors',
          headers: {
            "Content-Type": "application/json",
            "User-Agent": "PostmanRuntime/7.22.0",
            "Accept": "*/*",
            "Cache-Control": "no-cache",
            "Postman-Token": "75623e9b-c746-4547-9d8f-bc9b018cb621",
            "Host": "clientbasic.mastersindia.co",
            "Accept-Encoding": "gzip, deflate, br",
            "Content-Length": "176",
            "Cookie": "AWSALB=jb1DUXI9LxJadmtV0iZZOJ1Q+IXRaa/wmsIeugZl3eEbDyuSA3WPStQTsd3qCavWspWFYuFgVHocsbPMWAiISe2ZOTHawisiG0cj+kj4hDXsfW5zQ+qKh7iR7Oz3; AWSALBCORS=jb1DUXI9LxJadmtV0iZZOJ1Q+IXRaa/wmsIeugZl3eEbDyuSA3WPStQTsd3qCavWspWFYuFgVHocsbPMWAiISe2ZOTHawisiG0cj+kj4hDXsfW5zQ+qKh7iR7Oz3",
            "Connection": "keep-alive"
          },
          body: { "username": "testeway@mastersindia.co", "password": "Test@1234", "client_id": "", "client_secret": "", "grant_type": "" }
        }).then(function (response) {
          if (response.ok)
            return response.text();
        })
      .then(function (data) {
        alert(data);
      })
    }
  </script>
</asp:Content>

