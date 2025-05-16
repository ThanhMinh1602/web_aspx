<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ViewSP.aspx.cs" Inherits="DACN_IS385BB_TruongTienDat_6725.ViewSP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .product-image {
            width: 100%;
            max-width: 200px;
            height: auto;
            object-fit: cover;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card shadow-sm">
        <div class="row g-0 p-3">
            <div class="col-md-4 d-flex align-items-center justify-content-center">
                <asp:Image ID="Image1" runat="server" CssClass="product-image" />
            </div>
            <div class="col-md-8">
                <div class="card-body">
                    <h3 class="card-title">
                        <asp:Label ID="lblTen" runat="server" Font-Bold="True" Text="Tên sản phẩm"></asp:Label>
                    </h3>
                    <p class="card-text text-muted fst-italic">
                        <asp:Label ID="lblmota" runat="server" Text="Mô tả sản phẩm"></asp:Label>
                    </p>
                    <p class="card-text text-decoration-line-through text-secondary">
                        Giá gốc: <asp:Label ID="lblgia" runat="server" Text="0đ"></asp:Label>
                    </p>
                    <p class="card-text fw-bold text-danger">
                        Giá khuyến mãi: <asp:Label ID="lblgiagiam" runat="server" Text="0đ"></asp:Label>
                    </p>
                    <div class="mb-3">
                        <label for="TextBox1" class="form-label">Số lượng</label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Text="1"></asp:TextBox>
                    </div>
                    <div class="d-flex gap-2">
                        <asp:Button ID="Button2" runat="server" Text="Mua" OnClick="Button2_Click" CssClass="btn btn-success flex-fill" />
                        <asp:Button ID="Button3" runat="server" Text="Xem Giỏ Hàng" OnClick="Button3_Click" CssClass="btn btn-warning flex-fill" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
