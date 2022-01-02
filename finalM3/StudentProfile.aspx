<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentProfile.aspx.cs" Inherits="finalM3.StudentProfile" %>

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
                <h3>
                    Student profile</h3>
               
            </center>
            <label for="uname"><b>ID:</b></label>
            <br/>
            <asp:Label ID="Label1" runat="server" Text=" " ></asp:Label>
            <br />
            <br />
           <label for="uname"><b>First name:</b></label>
            <br/>
            <asp:Label ID="Label2" runat="server" Text=" " ></asp:Label>
            <br />
            <br />
            <label for="uname"><b>Last name:</b></label>
            <br/>
            <asp:Label ID="Label3" runat="server" Text=" " ></asp:Label>
            <br />
            <br />
            <label for="uname"><b>Email:</b></label>
            <br/>
            <asp:Label ID="Label4" runat="server" Text=" " ></asp:Label>
            <br />
            <br />
            <label for="uname"><b>Password:</b></label>
            <br/>
            <asp:Label ID="Label5" runat="server" Text=" " ></asp:Label>
            <br />
            <br />
            <label for="uname"><b>Type:</b></label>
            <br/>
            <asp:Label ID="Label6" runat="server" Text=" " ></asp:Label>
            <br />
            <br />
            <label for="uname"><b>Faculty:</b></label>
            <br/>
            <asp:Label ID="Label7" runat="server" Text=" " ></asp:Label>
            <br />
            <br />
            <label for="uname"><b>Address:</b></label>
            <br/>
            <asp:Label ID="Label8" runat="server" Text=" " ></asp:Label>
            <br />
            <br />
            <label for="uname"><b>GPA:</b></label>
            <br/>
            <asp:Label ID="Label9" runat="server" Text=" " ></asp:Label>
            <br />
            <br />
            
            <label for="uname"><b>Phone number(s):</b></label>
            <br/>
            <asp:Label ID="Label10" runat="server" Text=" " ></asp:Label>
            <br />
            <br />
           
            <asp:Button runat="server" ID="back" Text="Back" class="cnbtn" OnClick="Home" style="width:100%"/>
        </div>
    </form>
</body>
</html>
