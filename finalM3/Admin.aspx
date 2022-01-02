<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="finalM3.Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
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

<body>
    <form id="form1" runat="server" class ="frmalg">
        
        <div class="container">
            <center>
                <h2>Admin Home Page</h2>
                <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
            </center>
            <asp:Button ID="Button1" runat="server" CssClass="lgnbtn" Text="List all supervisors" OnClick="List_all_supervisors" Style="width: 100%" />
            <br />
            <asp:Button ID="Button2" runat="server" CssClass="lgnbtn" Text="All Thesis" OnClick="List_all_Thesis" Style ="width: 100%" />
            <br />
            <asp:Button ID="Button3" runat="server" CssClass="lgnbtn" Text="Issue Thesis Payment" OnClick="Issue_Payment" Style="width: 100%" />
            <br />
            <asp:Button ID="Button4" runat="server" CssClass="lgnbtn" Text="Issue Installment" OnClick="Issue_Installment" Style="width: 100%" />
            <br />
            <asp:Button ID="Button5" runat="server" CssClass="lgnbtn" Text="Update The number of extensions" OnClick="Update_ExtensionNO" Style="width: 100%" />
            <br />
             <asp:Button ID="Button6" runat="server" CssClass="cnbtn" Text="Log out" Style="width: 100%" OnClick="Button6_Click" />
            <br />
        </div>
    </form>
</body>
</html>
