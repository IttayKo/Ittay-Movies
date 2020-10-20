<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manage.aspx.cs" Inherits="movies.Manage" %>

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
        
                
            <div id="manage-options">
                <input type="button" id="manageU" value="Manage Users"/>
                <input type="button" id="manageM" value="Manage Movies"/>
            </div>
            <input style="display: none" id="submitChangesClient" type="button" value="Submit Changes"/>
         <form id="form1" method="post" runat="server">
            <div id="tableContainer">
                <asp:Table ID="loadTable" runat="server">
                    
                </asp:Table>
            </div>
            <input style="display: none" id="userSelect" type="radio" name="table-select" runat="server"/>
            <input style="display: none" id="movieSelect" type="radio" name="table-select" runat="server"/>
            <asp:Button ID="loadUsersBtn" style="display: none;" runat="server" OnClick="loadTableClick" />
            <asp:Button ID="loadMoviesBtn" style="display: none;" runat="server" OnClick="loadTableClick"/>
            <asp:Button ID="submitChangesBtn" style="display: none;" runat="server" OnClick="submitChangesClick"/>
             <asp:HiddenField ID="changedElements" runat="server" />
            </form>
            


        </div>
        <script>

            document.getElementById("submitChangesClient").addEventListener("click", submit)
            document.getElementById("manageU").addEventListener("click", loadUsers);
            document.getElementById("manageM").addEventListener("click", loadMovies);
            if (document.getElementById("userSelect").checked || document.getElementById("movieSelect").checked) {
                document.getElementById("submitChangesClient").removeAttribute("style");
            }
            document.getElementById("loadTable").addEventListener("change", event => {
                change(event.target);
            });
            var changed = []
            function change(a) {
                if (a.type == "checkbox") {
                    changed.push([a.id, a.checked].join('^'))
                }
                else {
                    changed.push([a.id, a.value].join('^'))
                }
                document.getElementById("changedElements").value = changed.join("*");
                console.log(document.getElementById("changedElements").value);

            }
            function submit() {
                var r = confirm("Are you sure?")
                if (r == true) {
                    document.getElementById("submitChangesBtn").click();
                }
                else { return;}
            }

            function loadUsers() {
                changed = [];
                document.getElementById("changedElements").value = "";
                document.getElementById("userSelect").click();
                document.getElementById("loadUsersBtn").click();
            }

            function loadMovies() {
                changed = [];
                document.getElementById("changedElements").value = "";
                document.getElementById("movieSelect").click();
                document.getElementById("loadMoviesBtn").click();
            }

        </script>
</body>
</html>
