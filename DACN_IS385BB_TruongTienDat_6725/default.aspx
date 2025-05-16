<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="DACN_IS385BB_TruongTienDat_6725._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .product-card {
            border: 1px solid #dee2e6;
            border-radius: 8px;
            padding: 15px;
            text-align: center;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            background-color: #ffffff;
            transition: transform 0.3s ease;
        }

            .product-card:hover {
                transform: translateY(-5px);
            }

        .product-image {
            width: 100%;
            height: auto;
            object-fit: cover;
            border-radius: 5px;
        }

        .product-title {
            font-weight: bold;
            margin-top: 10px;
            font-size: 1.1rem;
        }

        .product-price {
            color: #dc3545;
            font-size: 1rem;
        }

        .product-discount {
            color: #28a745;
            font-size: 1rem;
        }

        .category-box {
            margin-bottom: 20px;
        }

        .category-box {
            display: flex;
            justify-content: flex-start;
            align-items: center;
            gap: 15px;
        }

        .category-label {
            font-weight: bold;
            font-size: 1.2rem;
        }

        .category-select {
            width: 250px;
            padding: 8px 12px;
            border: 1px solid #ced4da;
            border-radius: 5px;
            background-color: #ffffff;
            transition: box-shadow 0.3s ease;
        }

            .category-select:focus {
                box-shadow: 0 0 0 0.2rem rgba(13, 110, 253, 0.25);
                border-color: #86b7fe;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-4">

        <!-- Danh sách loại sản phẩm -->
        <div class="category-box">
            <span class="category-label">Chọn loại sản phẩm:</span>
            <asp:ListBox ID="ListBox1" runat="server" AutoPostBack="True" DataSourceID="ListLoai" DataTextField="Name" DataValueField="id_Category" CssClass="category-select" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged"></asp:ListBox>
            <asp:ObjectDataSource ID="ListLoai" runat="server"></asp:ObjectDataSource>
        </div>


        <!-- Danh sách sản phẩm -->
        <div class="row g-4">
            <asp:ListView ID="ListView1" runat="server" DataSourceID="ListSP" DataKeyNames="id_Category" GroupItemCount="3" OnItemCommand="ListView1_ItemCommand" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged">

                <ItemTemplate>
                    <div class="col-md-4">
                        <div class="product-card h-100">
                            <asp:Image ID="ImgLabel" runat="server" ImageUrl='<%# "~/Image/"+ Eval("Img") %>' CssClass="product-image" />
                            <asp:Label ID="id_CategoryLabel" runat="server" Visible="false" Text='<%# Eval("id_Category") %>' />
                            <div class="product-title">
                                <asp:Label ID="TittleLabel" runat="server" Text='<%# Eval("Tittle") %>' />
                            </div>
                            <div class="product-price">
                                Giá:
                                <asp:Label ID="PriceLabel" runat="server" Text='<%# Eval("Price") %>' />
                            </div>
                            <div class="product-discount">
                                Giảm còn:
                                <asp:Label ID="DisscountLabel" runat="server" Text='<%# Eval("Disscount") %>' />
                            </div>
                            <asp:LinkButton runat="server" ID="Chitiet" Text="Chi Tiết Sản Phẩm" CommandName="xem" CommandArgument='<%# Eval("id_Product") %>' CssClass="btn btn-primary btn-sm mt-2" />
                        </div>
                    </div>
                </ItemTemplate>

                <LayoutTemplate>
                    <div class="row g-4" runat="server" id="groupPlaceholderContainer">
                        <asp:PlaceHolder runat="server" ID="groupPlaceholder" />
                    </div>
                </LayoutTemplate>

                <GroupTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                </GroupTemplate>

            </asp:ListView>
            <asp:ObjectDataSource ID="ListSP" runat="server"></asp:ObjectDataSource>
        </div>

    </div>
</asp:Content>
