<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="movies_project.add" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>Movies</title>
    <link rel="stylesheet" href="Content/style.css">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <nav id="navbar" class="navbar" runat="server">
        <a id="logo-link" href="index"><img src="Content/logo.png" alt="logo"></a>
        <a href="index">Home <img src="Content/home.png" alt="Home"></a>
        <a id="loginRedirect" href="login" runat="server">Login <img src="Content/arrow-right.png" alt="Login"></a>
        <a id="signupRedirect" runat="server" href="signup">Sign Up <img src="Content/arrow-up.png" alt="Sign Up"></a>
    </nav>
    <div class="main">
    <form id="movieForm" runat="server">
        <h2 class="sign">Add Movie</h2>
        <div class="addMovie">
            <input id="movieName" class="text" type="text" placeholder="Name" runat="server"><br />
            <div id="rating">Rating: 
                <label><input id="star1" class="rate" type="radio" name="star"><img src="Content/emptystar.png" onmouseover="starHover(this)" onmouseout="endHover(this)" onclick="setStar(this)"/></label>
                 <label><input id="star2" class="rate" type="radio" name="star"><img src="Content/emptystar.png" onmouseover="starHover(this)"  onmouseout="endHover(this)" onclick="setStar(this)"/></label>
                 <label><input id="star3" class="rate" type="radio" name="star"><img src="Content/emptystar.png" onmouseover="starHover(this)"  onmouseout="endHover(this)" onclick="setStar(this)"/></label>
                 <label><input id="star4" class="rate" type="radio" name="star"><img src="Content/emptystar.png" onmouseover="starHover(this)"  onmouseout="endHover(this)" onclick="setStar(this)"/></label>
                 <label><input id="star5" class="rate" type="radio" name="star"><img src="Content/emptystar.png" onmouseover="starHover(this)"  onmouseout="endHover(this)" onclick="setStar(this)"/></label>
                </div>
            <input id="movieDescription" class="text" type="text" runat="server">
            <input id="movieURL" class="text" type="text" placeholder="Trailer URL" runat="server"><br />

        <input id="submitMovie" class="btn" type="button" value="Add" />
        </div>
        <asp:Button ID="submitMovieBtn" runat="server" style="display: none;" onClick="submitMovie"/>
        <asp:HiddenField ID="countStars" runat="server"/>
    </form>
        </div>
        <script>
            document.getElementById("submitMovie").addEventListener("click", addMovie);
            function addMovie() {
                if (document.querySelector(".rate:checked") == null || document.getElementById("movieName").value == "" ||
                    document.getElementById("movieDescription").value == "" || document.getElementById("movieURL").value == "") {
                    alert("Fill form!")
                }
                else { document.getElementById("submitMovieBtn").click() }
            }

            function starHover(imgEl) {
                s = imgEl.parentElement.children[0];
                for (var i = parseInt(s.id.slice(-1)); i > 0; i--) {
                    document.getElementById("star" + i).parentElement.children[1].setAttribute("style", "filter: brightness(100%);")
                    
                }

            }
            function endHover(imgEl) {
                s = imgEl.parentElement.children[0];
                for (var i = parseInt(s.id.slice(-1)); i > 0; i--) {
                    if (document.getElementById("star" + i).parentElement.children[1].getAttribute("src") != "Content/fullstar.png") {
                        document.getElementById("star" + i).parentElement.children[1].setAttribute("style", "filter: brightness(80%);");
                    }



                }

            }
            function setStar(imgEl) {
                var s = imgEl.parentElement.children[0];
                s.checked = true;
                for (var i = parseInt(s.id.slice(-1)); i > 0; i--) {
                    document.getElementById("star" + i).parentElement.children[1].setAttribute("src", "Content/fullstar.png");
                }
                for (var i = parseInt(s.id.slice(-1)) + 1; i < 6; i++) {
                    document.getElementById("star" + i).parentElement.children[1].setAttribute("src", "Content/emptystar.png");
                    document.getElementById("star" + i).parentElement.children[1].setAttribute("style", "filter: brightness(80%);");
                }
                document.getElementById("countStars").value = document.querySelector(".rate:checked").id.slice(-1);
            }
        
        </script>
</body>

</html>
