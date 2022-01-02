<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EvaluateStudent.aspx.cs" Inherits="finalM3.EvaluateStudent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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
    <form id="form1" runat="server" class ="frmalg">
        <div class ="container">
            <center>
                <h2>Evaluate student's progress report</h2>
                <asp:Label runat ="server" ID ="suc" Text =" "></asp:Label>
            </center>
            <asp:Label runat ="server" ><b>Thesis serial number:</b></asp:Label>
            <br />
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Label ID="serailWarning" runat="server" Text=" " class="warning"></asp:Label>
            <br />
            <asp:Label runat="server"><b>Progress report number</b></asp:Label>
            <br />
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:Label ID="reportWarning" runat="server" Text=" " class="warning"></asp:Label>
            <br />
            <br />
            <asp:Label runat="server"><b>Evaluation</b></asp:Label>
            <br />
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <asp:Label ID="evalWarning" runat="server" Text=" " class="warning"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" CssClass ="lgnbtn" Text="Evaluate" OnClick="Button1_Click" />
            <asp:Button ID="HomeB" runat="server" CssClass ="cnbtn" Text="Back" OnClick="Home" />
            <br />

        </div>
    </form>
</body>
</html>
