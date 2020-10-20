<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="movies.Signup" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <link rel="stylesheet" href="Content/style.css">
    <title>Movies</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    
        <nav id="navbar" class="navbar" runat="server">
        <a id="logo-link" href="index"><img src="Content/logo.png" alt="logo"></a>
        <a href="index">Home <img src="Content/home.png" alt="Home"></a>
        <a id="loginRedirect" href="login" runat="server">Login <img src="Content/arrow-right.png" alt="Login" ></a>
        <a id="signupRedirect" runat="server" href="signup">Sign Up <img src="Content/arrow-up.png" alt="Sign Up"></a>
        
    </nav>
    <div class="main"> 
    <h1>Trailers, Descriptions &amp Ratings</h1>
    <h2 class="sign">Sign Up</h2>
    <form id="signupForm" runat="server">
        <div class="signup">
            <input id="fNameNew" class="text" type="text" name="firstName" placeholder="First Name" runat="server">
            <input id="lNameNew" class="text" type="text" name="lastName" placeholder="Last Name" runat="server">
            <input id="emailNew" class="text" type="email" name="email" placeholder="Email" runat="server">
            <input id="phoneNew" class="text" type="tel" name="phone" placeholder="Phone Number" runat="server">
            <input id="userNew" class="text" type="text" name="username" placeholder="Username" runat="server">
            <input id="passNew" class="text" suggested="new-password" type="password" name="password" placeholder="Password" runat="server">
            <input id="passNew2" class="text" suggested="new-password" type="password" name="password2" placeholder="Confirm Password" runat="server">
            <input id="signupBtn" class="btn" type="button" value="Sign Up"/> 
        </div>
    
        <asp:Button style="display:none" ID="trySignup" runat="server" OnClick="signUp"/>
        <input type="checkbox" style="display: none" id="userTaken" runat ="server"/>
        <input type="checkbox" style="display: none" id="emailTaken" runat ="server"/>
        <input type="checkbox" style="display: none" id="addUser" runat ="server" />
        <input style="display: none" id="checkErr" type="checkbox" runat="server"/>
    </form>
        </div>
    <script type="module">
        if (sessionStorage.length > 0) { window.location("index") }
        document.getElementById("signupBtn").addEventListener("click", newUser);
        var userT = document.getElementById("userTaken");
        var emailT = document.getElementById("emailTaken");
        var addU = document.getElementById("addUser");
        var isErr = function () { return document.getElementById("checkErr").hasAttribute("checked"); }
        var inputs = function () { return document.querySelectorAll(".signup .text"); }
        window.addEventListener('load', checkEmailUser);

        function err(bool) {
            if (bool === true){
                document.getElementById("checkErr").setAttribute("checked", "checked");
        }
            else if(document.getElementById("checkErr").hasAttribute("checked")) {
            document.getElementById("checkErr").removeAttribute("checked");
            }
        }
        
        function checkEmailUser() {
            if (userT.hasAttribute("checked")) {
                userT.removeAttribute("checked");
                usernameTakenAlert();
                return;
            }
            else if (emailT.hasAttribute("checked")) {
                emailT.removeAttribute("checked");
                emailTakenAlert();
                return;
            }
            else if (addU.hasAttribute("checked")) {
                addU.removeAttribute("checked");
                window.location = "index";
                return;
            }
        }

        
        function newUser() {
            err(false);

            checkFull();
            if (isErr()) {
                return;
            }
            checkPass();
            if (isErr()) {
                return;
            }

            document.getElementById("trySignup").click();

        }
        function checkFull() {
            for (var input of inputs()) {
                if (input.value === "" || input.value === null) {
                    input.setAttribute("style", "border-color: #f000004d");
                    err(true);
                }
            }
            if (isErr()) {
                alert("Fill form!");
          
            }
        }


        function checkPass() {
            if (document.getElementById("passNew").value != document.getElementById("passNew2").value) {
                err(true);
                alert("Passwords dont match. Try again.");
                document.getElementById("passNew").value = "";
                document.getElementById("passNew2").value = "";
            }
        }

        function usernameTakenAlert() {
            err(true);
            alert("Username already in use. Try using a different one.");
        }
        function emailTakenAlert() {
            err(true);
            alert("An account with this Email already exists.");
        }
        

    </script>
    
</body>
</html>
