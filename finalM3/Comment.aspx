<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comment.aspx.cs" Inherits="finalM3.Comment" %>

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
                <h1>Add Comment</h1>
                <asp:Label runat ="server" Text =" " ID ="suc"></asp:Label>
                
            </center>
            <asp:Label runat ="server"><b>Thesis Serial Number</b></asp:Label>
            <asp:TextBox ID="Serial" runat="server" ></asp:TextBox>
            <asp:Label ID="serialWarning" runat="server" Text=" " class="warning"></asp:Label>
            <br />
            <asp:Label runat="server"><b>Defense Date</b></asp:Label>
            <div>
                <asp:Calendar ID="datepicker" runat="server" Visible="True" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px">
                    <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                    <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#ec3f3f" />
                    <SelectedDayStyle BackColor="#ec3f3f" ForeColor="White" />
                    <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor=" #4CAF50" />
                    <TodayDayStyle BackColor=" #6AC86E" />
                </asp:Calendar>
                <asp:Label ID="dateWrning" runat="server" Text=" " class="warning"></asp:Label>
            </div>
            <br />
            <asp:Label runat="server"><b>Comment</b></asp:Label>
            <asp:TextBox ID="Comments" runat="server" ></asp:TextBox>
            <asp:Label ID="commentWarning" runat="server" Text=" " class="warning"></asp:Label>
            <br />
            <asp:Button ID="Button1" runat="server" CssClass="lgnbtn" OnClick ="Add_Comment" Text="Add comment" />
            <asp:Button ID="HomeB" runat="server" CssClass ="cnbtn" Text="Back" OnClick="Home" />
        </div>
    </form>
</body>
</html>
