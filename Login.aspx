<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ZealEducationManager.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Zeal Education Manager</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/styles.css" rel="stylesheet" />

    <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/jquery-3.6.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row no-gutter bg-light">
                <div class="col-md-6 d-none d-md-flex bg-image shadow row-bg-radius"></div>

                <div class="col-md-6 bg-light">
                    <div class="login d-flex align-items-center py-5">
                        <div class="container">
                            <div class="row">
                                <div class="col-lg-10 col-xl-7 mx-auto">
                                    <h3 class="display-4 pb-3">Sign In</h3>

                                    <p class="text-muted mb-4">Login page for Admin & Teacher</p>

                                    <div class="form-group mb-3">
                                        <input id="inputEmail" type="text" placeholder="Email address" required="required" runat="server" autofocus="autofocus" class="form-control rounded-pill border-0 shadow-sm px-4"/>
                                    </div>

                                    <div class="form-group mb-3 mb-2">
                                        <input id="inputPassword" type="password" placeholder="Password" required="required" runat="server" class="form-control rounded-pill border-0 shadow-sm px-4 text-primary" />
                                    </div>

                                    <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="btn btn-primary btn-block text-uppercase mb-2 rounded-pill login-btn-rad-bg shadow-sm col-md-12" OnClick="btnLogin_Click" />

                                    <div class="font-size-x d-flex justify-content-between mt-4">
                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>