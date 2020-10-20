<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="movies.Index" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
  
    <link rel="stylesheet" href="Content/style.css">
    <title>Movies</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <form id="form1" runat="server">
        <nav id="navbar" class="navbar" runat="server">
        <a id="logo-link" href="index"><img src="Content/logo.png" alt="logo"></a>
        <a href="index">Home <img src="Content/home.png" alt="Home"></a>
        <a id="loginRedirect" href="login" runat="server">Login <img src="Content/arrow-right.png" alt="Login" ></a>
        <a id="signupRedirect" runat="server" href="signup">Sign Up <img src="Content/arrow-up.png" alt="Sign Up"></a>
    </nav>
        <div class="main"> 
        <h1>Trailers, Descriptions &amp Ratings</h1>
        <section id='left'>
            <h2> Trailer </h2>
    </section>
    <section id='right'>
        <h2> Description</h2>
    </section>
    <div id="movies" runat="server">
    </div>
<footer>
<a id="to-top" href="#top">Back To Top</a>
</footer>
</div>


    </form>
    <script>
        document.onkeydown = function (e) {
            return false;
        }
        
    </script>
</body>
</html>
