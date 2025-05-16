<%@ Page Title="Giỏ Hàng" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="DACN_IS385BB_TruongTienDat_6725.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .cart-container {
            background: #ffffff;
            border-radius: 10px;
            padding: 30px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        .cart-img {
            width: 80px;
            height: 80px;
            object-fit: cover;
            border-radius: 8px;
        }
        .cart-table th {
            background-color: #f8f9fa;
            text-align: center;
        }
        .cart-table td {
            vertical-align: middle;
            text-align: center;
        }
        .total-section {
            font-size: 1.25rem;
            font-weight: 600;
            color: #198754;
            margin-top: 20px;
        }
        .btn-cart {
            width: 100%;
            margin-top: 10px;
        }
        .empty-cart {
            padding: 40px 0;
            text-align: center;
            font-size: 1.2rem;
            color: #6c757d;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <div class="cart-container">
            <h2 class="text-center mb-4">🛒 Giỏ Hàng</h2>

            <asp:Panel ID="AlertPanel" runat="server" CssClass="alert-container d-none">
                <asp:Label ID="lblAlert" runat="server" CssClass="alert alert-warning d-block text-center" />
            </asp:Panel>

            <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False" CssClass="table table-hover cart-table" OnRowCommand="gvCart_RowCommand" GridLines="None">
                <Columns>
                    <asp:TemplateField HeaderText="Hình Ảnh">
                        <ItemTemplate>
                            <asp:Image ID="imgProduct" runat="server" ImageUrl='<%# Eval("Img", "~/Image/{0}") %>' CssClass="cart-img" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Tittle" HeaderText="Tên Sản Phẩm" />
                    <asp:BoundField DataField="Price" HeaderText="Giá" DataFormatString="{0:N0} VNĐ" />
                    <asp:BoundField DataField="Number" HeaderText="Số Lượng" />
                    <asp:BoundField DataField="Total_Money" HeaderText="Tổng Tiền" DataFormatString="{0:N0} VNĐ" />
                    <asp:TemplateField HeaderText="Hành Động">
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" runat="server" Text="Xoá" CssClass="btn btn-outline-danger btn-sm" CommandName="DeleteProduct" CommandArgument='<%# Eval("id_Product") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="empty-cart">
                        <i class="bi bi-cart-x fs-1 mb-3"></i><br />
                        Giỏ hàng của bạn đang trống!
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>

            <div class="row justify-content-end">
                <div class="col-md-6">
                    <div class="total-section text-end">
                        Tổng Tiền: <asp:Label ID="lblTotalMoney" runat="server" Text="0 VNĐ" />
                    </div>
                </div>
            </div>

            <div class="row mt-4">
                <div class="col-md-6">
                    <asp:Button ID="btnBackHome" runat="server" Text="⬅️ Tiếp tục mua sắm" CssClass="btn btn-outline-primary btn-cart" OnClick="btnBackHome_Click" />
                </div>
                <div class="col-md-6">
                    <asp:Button ID="btnCheckout" runat="server" Text="💳 Tiến hành thanh toán" CssClass="btn btn-success btn-cart" OnClick="btnCheckout_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
