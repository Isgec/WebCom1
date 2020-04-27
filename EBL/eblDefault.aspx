<%@ Page Language="VB" AutoEventWireup="false" CodeFile="eblDefault.aspx.vb" ClientIDMode="Static" Inherits="eblDefault" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
  <meta charset="utf-8"/>
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate">
  <meta http-equiv="Pragma" content="no-cache">
  <meta http-equiv="Expires" content="0">
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <title>ISGEC-E-BILL</title>
  <script src="../App_Resources/jq3.3/jquery-3.3.1.min.js"></script>
  <link href="../App_Resources/fa-5.9.0/css/all.css" rel="stylesheet" />
  <link href="../App_Resources/bs4.0/css/bootstrap.min.css" rel="stylesheet" />
  <script src="../App_Resources/Popper1.0/popper.min.js"></script>
  <script src="../App_Resources/bs4.0/js/bootstrap.min.js"></script>
  <script src="../App_Resources/bluebird.min.js"></script>
  <script src="../App_Resources/fetch.js"></script>
  <style>
    body{
      font-family:Tahoma;
      font-size:14px;
    }
  </style>
</head>
<body>
    <form id="form1" runat="server">
      <div class="container-fluid">
        <div style="display:flex;flex-direction:column;">
          <div class='row'>
            <div class='col'>
              <b>
                <asp:Label ID="Label1" ForeColor="#CC6633" runat="server" Text="Authenticate and Get Access Token :" /></b>
            </div>
            <div class='col'>
              <input type="button" class="btn btn-danger btn-sm" value="Access Token" onclick="myPost();" />
            </div>
            <div class='col'>
              <asp:Button ID="cmdPost" runat="server" class="btn btn-danger btn-sm" Text="Post on Server" OnClientClick="return document.getElementById('L_Response').value='';" />
            </div>
            <div class='col'>
              <asp:Button ID="cmdInv" runat="server" class="btn btn-danger btn-sm" Text="E-Invoice"  />
            </div>
          </div>
          <div class='row'>
            <div class='col'>
              <asp:Label ID="L_Response" runat="server" class="btn-warning btn-sm" Text="" />
            </div>
          </div>
          <div class='row'>
            <div class='col'>
              <asp:Label ID="L_IRN" runat="server" class="btn-warning btn-sm" Text="" />
            </div>
          </div>
          <div class='row'>
            <div class='col'>
              <asp:Button ID="cmdClearTax" runat="server" class="btn btn-danger btn-sm" Text="Check Clear Tax" />
            </div>
            <div class='col'>
              <asp:Label ID="Label2" runat="server" class="btn-warning btn-sm" Text="" />
            </div>
          </div>
          <div class='row'>
            <div class='col'>
              <asp:image ID="qrimg" runat="server" AlternateText="qrcode"  />
              <img id="strimg" alt="xxx" runat="server" src="a" />
            </div>
            <div class='col'>
              <asp:Label ID="Label3" runat="server" class="btn-warning btn-sm" Text="" />
            </div>
          </div>
        </div>
      </div>
    </form>
  <script>
    var settings = {
      "url": "https://clientbasic.mastersindia.co/oauth/access_token",
      "method": "POST",
      "timeout": 0,
      "headers": {
        "Content-Type": "application/json",
        "User-Agent": "PostmanRuntime/7.22.0",
        "Accept": "*/*",
        "Cache-Control": "no-cache",
        "Postman-Token": "19cb33ea-7711-40aa-8754-5e852ac5489a",
        "Host": "clientbasic.mastersindia.co",
        "Accept-Encoding": "gzip, deflate, br",
        "Content-Length": "176",
        "Cookie": "AWSALB=GYb4I54A9e2WZDS8fczcJKOKzNNePCjVUYUDfe01Sjdywu1E5dDUVYSZNHmfp+d6UGlaWQsJeJ1cGckY/tcKsmG+LKHY6yjpWagg8wq6jrU7nlrA6O/IKgCzhE94; AWSALBCORS=GYb4I54A9e2WZDS8fczcJKOKzNNePCjVUYUDfe01Sjdywu1E5dDUVYSZNHmfp+d6UGlaWQsJeJ1cGckY/tcKsmG+LKHY6yjpWagg8wq6jrU7nlrA6O/IKgCzhE94",
        "Connection": "keep-alive"
      },
      "data": JSON.stringify({ "username": "testeway@mastersindia.co", "password": "Test@1234", "client_id": "fIXefFyxGNfDWOcCWn", "client_secret": "QFd6dZvCGqckabKxTapfZgJc", "grant_type": "password" }),
    };

    //$.ajax(settings).done(function (response) {
    //  console.log(response);
    //});
  </script>
  <script>
    function myPost() {
      var json_upload = JSON.stringify({ "username": "testeway@mastersindia.co", "password": "Test@1234", "client_id": "", "client_secret": "", "grant_type": "" });
      var xhttp = new XMLHttpRequest();
      
      xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
          alert(this.responseText);
        }
      };
      xhttp.open("POST", "https://clientbasic.mastersindia.co/oauth/access_token");
      xhttp.setRequestHeader("Content-Type", "application/json");
      xhttp.setRequestHeader("User-Agent", "PostmanRuntime/7.22.0");
      xhttp.setRequestHeader("Accept", "*/*");
      xhttp.setRequestHeader("Cache-Control", "no-cache");
      xhttp.setRequestHeader("Postman-Token", "75623e9b-c746-4547-9d8f-bc9b018cb621");
      xhttp.setRequestHeader("Host", "clientbasic.mastersindia.co");
      xhttp.setRequestHeader("Accept-Encoding", "gzip, deflate, br");
      xhttp.setRequestHeader("Connection", "keep-alive");
      xhttp.send(json_upload);
    }
  </script>
  <script>
    function myFetch() {
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

</body>
</html>
