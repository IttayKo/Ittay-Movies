<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="movies.Login" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Movies</title>
    
    <link rel="stylesheet" href="Content/style.css">
</head>
<body>
    <form id="loginForm" runat="server">
        <nav id="navbar" class="navbar" runat="server">
        <a id="logo-link" href="index"><img src="Content/logo.png" alt="logo"></a>
        <a href="index">Home <img src="Content/home.png" alt="Home"></a>
        <a id="loginRedirect" href="login" runat="server">Login <img src="Content/arrow-right.png" alt="Login"></a>
        <a id="signupRedirect" runat="server" href="signup">Sign Up <img src="Content/arrow-up.png" alt="Sign Up"></a>
    </nav>
    <div class="main">
    <h1>Trailers, Descriptions &amp Ratings</h1>
    <h2 class="sign">Log in</h2>
        <div class="loginform">
        <input id="user" class="text" type="text" name="username" placeholder="Username" runat="server"><br>
        <input id="pass" class="text" type="password" placeholder="Password" runat="server"><br>
        <input id="loginBtn" class="btn" type="button" value="Log In">
        </div>
          <asp:Button ID="tryLogin" OnClick="login" style="display:none" runat="server" />
         <input type="checkbox" style="display: none" id="noUser" runat ="server"/>
         <input type="checkbox" style="display: none" id="wrongPass" runat ="server"/>
         <input type="checkbox" style="display: none" id="successLog" runat ="server"/>

    </div>
    </form>
    <script type="module">
        if (sessionStorage.length > 0) { window.location("index") }
        document.querySelector("#loginBtn").addEventListener("click", login);
        window.addEventListener('load', checkUserPass);

        function login() {
            for (var input of document.querySelectorAll(".loginform.text")) {
                if (input.value == "" || input.value == null) {
                    input.setAttribute("style", "border-color: #f000004d");
                    return;
                }
            }
            document.getElementById("tryLogin").click();

        }
        function checkUserPass() {
            if (document.getElementById("noUser").hasAttribute("checked")) {
                document.getElementById("noUser").removeAttribute("checked");
                alert("Username not registered. Check username or Sign Up");
                document.getElementById("pass").value = "";
            }
            else if (document.getElementById("wrongPass").hasAttribute("checked")) {
                document.getElementById("wrongPass").removeAttribute("checked");
                alert("Password does not match username. Try again.");
                document.getElementById("pass").value = "";
            }
            else if (document.getElementById("successLog").hasAttribute("checked")) {
                document.getElementById("successLog").removeAttribute("checked")
                window.location = "index"
            }
        }
    </script>
</body>
</html>
