<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentHome.aspx.cs" Inherits="finalM3.StudentHome" %>

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
            background-color: #ec3f3f;
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
        .warning{
            color: red;
            font-size:15px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" class="frmalg">

        <div class="container">
           
            <center>
                <h2>Student homepage</h2>
                 <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
            </center>
           
            <asp:Button runat="server" ID="Button1" CssClass="lgnbtn" Text="Add phone number" OnClick="Button1_Click" style="width:100%"/>
            <br />
             <asp:Button runat="server" ID="Button2" CssClass="lgnbtn" Text="View my profile" OnClick="Button2_Click" style="width:100%"/>
            <br />
             <asp:Button runat="server" ID="Button3" CssClass="lgnbtn" Text="List all my thesis" OnClick="Button3_Click" style="width:100%"/>
            <br />
             <asp:Button runat="server" ID="Button4" CssClass="lgnbtn" Text="List all my courses" OnClick="Button4_Click" style="width:100%"/>
            <br />
             <asp:Button runat="server" ID="Button5" CssClass="lgnbtn" Text="Add progress report" OnClick="Button5_Click" style="width:100%"/>
            <br />
             <asp:Button runat="server" ID="Button6" CssClass="lgnbtn" Text="Fill progress report" OnClick="Button6_Click" style="width:100%"/>
            <br />
             <asp:Button runat="server" ID="Button7" CssClass="lgnbtn" Text="Add publication" OnClick="Button7_Click" style="width:100%"/>
            <br />
             <asp:Button runat="server" ID="Button8" CssClass="lgnbtn" Text="Link publication to thesis" OnClick="Button8_Click" style="width:100%"/>
            <br />
            <asp:Button runat="server" ID="btn_cancel" Text="Log out" class="cnbtn"  style="width:100%" OnClick="btn_cancel_Click"/>
        </div>
    </form>
</body>
</html>
