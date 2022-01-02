<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="finalM3.RegisterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Form</title>

    <meta name="viewport" content="width=device-width, initial-scale=1">
    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
        }
        form {
            border: 3px solid #f1f1f1;
        }
        input[type=text], input[type=password] {
            width: 100%;
            padding: 12px 20px;
            margin: 8px 0;
            display: inline-block;
            border: 1px solid #ccc;
            box-sizing: border-box;
        }
        button:hover {
            opacity: 0.8;
        }
        .cnbtn {
            background-color:#ec3f3f;
            color: white;
            padding: 14px 20px;
            margin: 8px 0;
            border: none;
            cursor: pointer;
            width: 49%;
        }
        .lgnbtn {
            background-color: #4CAF50;
            color: white;
            padding: 14px 20px;
            margin: 8px 0;
            border: none;
            cursor: pointer;
            width: 50%;
        }
        .imgcontainer {
            text-align: center;
            margin: 24px 0 12px 0;
        }
        img.avatar {
            width: 40%;
            border-radius: 50%;
        }
        .container {
            padding: 16px;
        }
        span.psw {
            float: right;
            padding-top: 16px;
        }
        /* Change styles for span and cancel button on extra small screens */
        @media screen and (max-width: 300px) {
            span.psw {
                display: block;
                float: none;
            }
            .cnbtn {
                width: 100%;
            }
        }
        .frmalg {
            margin: auto;
            width: 40%;
        }
    </style>

</head>
<body>
    <br />
        <br />
        <br />
        <br />
   
    <form id="form1" runat="server" class="frmalg">
         
        <div class="container">
            <center>
                <h2>Please select a type</h2>
            </center>
            <br />
        <br />
        <br />
        <br />
             <center>
            <asp:Button runat="server" ID="btn_Login" CssClass="lgnbtn" Text="Student" onClick="Button1_Click"/>
                  </center>
             <br />
            <center>
            <asp:Button runat="server" ID="Supervisor" Text="Supervisor" CssClass="lgnbtn" onClick="Button2_Click"/>
                 </center>
             <br />
            <center>
                 <center>
            <asp:Button runat="server" ID="Button2" Text="Examiner" CssClass="lgnbtn" OnClick="Button2_Click1" />
                 </center>
             <br />
            <center>
             <asp:Button runat="server" ID="Button1" Text="Cancel" class="cnbtn" OnClick="Button1_Click1" />
                </center>
        </div>
         <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>