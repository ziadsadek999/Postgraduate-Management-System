<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Examiner.aspx.cs" Inherits="finalM3.Examiner" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <form id="form1" runat="server" class ="frmalg">
        
        <div class ="container">
            <center>
                <h2>Examiner Home Page</h2>
                <asp:Label Text =" " ID ="EID" runat ="server"></asp:Label>
            </center>
            <br />
            <asp:Button ID="Button1" runat="server" CssClass ="lgnbtn" Text="Update Name" OnClick="Update_name" Style ="width:100%"/>
            
            <asp:Button ID="Button2" runat="server" CssClass ="lgnbtn" Text="Update Field of Work" OnClick="Update_fieldOfWork" Style="width: 100%" w/>

            <asp:Button ID="Button3" runat="server" CssClass ="lgnbtn" Text="List Thesis, Supervisors & defenses" OnClick="List_ThesisSupDefense" Style="width: 100%" />
            
            <asp:Button ID="Button4" runat="server" CssClass ="lgnbtn" Text="Add Comment" OnClick="Add_comment" Style="width: 100%" />
            
            <asp:Button ID="Button5" runat="server" CssClass ="lgnbtn" Text="Add Grade" OnClick="Add_Grade" Style="width: 100%" />

            <asp:Button ID="Button6" runat="server" CssClass="lgnbtn" Text="Search for Thesis" OnClick="Search_thesis" Style="width: 100%" />
            <asp:Button ID="Button7" runat="server" CssClass="cnbtn" Text="Log out"  Style="width: 100%" OnClick="Button7_Click" />
        </div>
    </form>
</body>
</html>
