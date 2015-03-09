<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="form1" runat="server">
    <h2>
        <%: ViewData["Message"] %></h2>
    <script src="../../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-2.1.3.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#btValidate").dblclick(function () {

                if ($("#btFile").val() != "") {
                    var Gameurl = { info: $("#btFile").val() }
                  
                    $.ajax({
                        type: "GET",
                        url: "/Home/ExecuteGame",
                        data: Gameurl,
                        success: function (data) {
                            if (data.InfoUserGame != "") {
                                $.each(data.InfoUserGame, function (i, item) {
                                    $("#gdUserGame").append("<tr><td>" + item.User + "</td></td><td>" + item.Result + "</td></tr>");
                                });
                            }
                            else {
                                alert("PROBLEM WITH YOUR INFORMATION");
                            }
                        }
                    });
                }
                else {
                    alert("File Invalid");
                }
            })


//            $("#btPlay").dblclick(function () {
//                alert("Hola");
//                var user = {
//                    User: $("#ctUser").val(),
//                    FirstName: $("#ctFirstName").val(),
//                    SecondName: $("#ctSecondName").val()
//                }

//                alert("Hola1");
//                $.ajax({
//                    type: "POST",
//                    url: "/Home/ServicesPage",
//                    data: user,
//                    success: function (data) {

//                        alert("USER REGISTERED");
//                    }
//                });
//            });
        });
    
    </script>
    <table>
    <tr>
    <td>
    <p>Format File Champoins [Create a file extension xml]</p>
    <p>The file must contain minimum 10 players</p>
    </td>
    </tr>
    <tr>
    <td>
    <p>
       <%: ViewData["INFO"] %>
    </p>
 
    </td>
    </tr>
    </table>
   <table>
   <tr>
   <td>
   <p>Select your file to champions</p>
   </td>
   <td>
   <input id="btFile" type="file"  />
   </td>
   </tr>
   <tr>
   <td>
   PRESS "PLAY" START GAME
   </td>
   <td>
   <input id="btValidate" type="button" value="PLAY" name="Validate" />
   </td>
   </tr>
   </table>
   <hr />
   <hr />
   <table id="gdUserGame">
    <tr>
    <td>USER</td>
    <td>POINTS</td>    
    </tr>
    </table>
    </form>
</asp:Content>
