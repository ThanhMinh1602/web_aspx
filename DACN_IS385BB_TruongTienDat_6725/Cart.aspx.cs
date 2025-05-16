//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Web.UI;

//namespace DACN_IS385BB_TruongTienDat_6725
//{
//    public partial class Cart : Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                int userId = 0;
//                if (Session["UserId"] != null)
//                {
//                    userId = Convert.ToInt32(Session["UserId"]);
//                    LoadCartData(userId);
//                }
//                else
//                {
//                    ShowAlert("Bạn chưa đăng nhập, vui lòng đăng nhập để xem giỏ hàng.");
//                }
//            }
//        }

//        private void LoadCartData(int userId)
//        {
//            dbClass db = new dbClass();

//            // Lấy đơn hàng mới nhất (hoặc trạng thái "chưa thanh toán") của user
//            string queryOrder = "SELECT TOP 1 id_Order FROM Orders WHERE id_User = @UserId AND Status = 0 ORDER BY Order_date DESC";
//            SqlParameter[] paramOrder = new SqlParameter[]
//            {
//        new SqlParameter("@UserId", userId)
//            };
//            object orderObj = db.ExecuteScalar(queryOrder, paramOrder);

//            if (orderObj != null)
//            {
//                int orderId = Convert.ToInt32(orderObj);

//                // Lấy chi tiết giỏ hàng theo orderId
//                string query = @"
//            SELECT od.id_Product, p.Tittle, od.Price, od.Number, od.Total_Money, p.Img
//            FROM Order_Details od
//            INNER JOIN Product p ON od.id_Product = p.id_Product
//            WHERE od.id_Order = @OrderId";
//                SqlParameter[] parameters = new SqlParameter[]
//                {
//            new SqlParameter("@OrderId", orderId)
//                };

//                DataTable dt = db.GetData(query, parameters);
//                gvCart.DataSource = dt;
//                gvCart.DataBind();

//                // Tính tổng tiền
//                int totalMoney = 0;
//                foreach (DataRow row in dt.Rows)
//                {
//                    totalMoney += Convert.ToInt32(row["Total_Money"]);
//                }
//                lblTotalMoney.Text = string.Format("{0:N0} VNĐ", totalMoney);
//            }
//            else
//            {
//                ShowAlert("Bạn chưa có đơn hàng nào trong giỏ.");
//                gvCart.DataSource = null;
//                gvCart.DataBind();
//                lblTotalMoney.Text = "0 VNĐ";
//            }
//        }

//        private void ShowAlert(string message)
//        {
//            AlertPanel.CssClass = "alert alert-warning alert-container";
//            lblAlert.Text = message;
//            AlertPanel.Visible = true;
//        }



//    }
//}
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DACN_IS385BB_TruongTienDat_6725
{
    public partial class Cart : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            gvCart.RowCommand += gvCart_RowCommand;

            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    int userId = Convert.ToInt32(Session["UserId"]);
                    LoadCartData(userId);
                }
                else
                {
                    ShowAlert("Bạn chưa đăng nhập, vui lòng đăng nhập để xem giỏ hàng.");
                }
            }
        }

        private void LoadCartData(int userId)
        {
            dbClass db = new dbClass();

            // Lấy đơn hàng hiện tại của user
            string queryOrder = "SELECT TOP 1 id_Order FROM Orders WHERE id_User = @UserId AND Status = 0 ORDER BY Order_date DESC";
            SqlParameter[] paramOrder = { new SqlParameter("@UserId", userId) };
            object orderObj = db.ExecuteScalar(queryOrder, paramOrder);

            if (orderObj != null)
            {
                int orderId = Convert.ToInt32(orderObj);

                // Lấy chi tiết giỏ hàng
                string query = @"
                    SELECT od.id_Product, p.Tittle, od.Price, od.Number, od.Total_Money, p.Img
                    FROM Order_Details od
                    INNER JOIN Product p ON od.id_Product = p.id_Product
                    WHERE od.id_Order = @OrderId";
                SqlParameter[] parameters = { new SqlParameter("@OrderId", orderId) };

                DataTable dt = db.GetData(query, parameters);
                gvCart.DataSource = dt;
                gvCart.DataBind();

                // Tính tổng tiền
                int totalMoney = 0;
                foreach (DataRow row in dt.Rows)
                {
                    totalMoney += Convert.ToInt32(row["Total_Money"]);
                }
                lblTotalMoney.Text = string.Format("{0:N0} VNĐ", totalMoney);
            }
            else
            {
                ShowAlert("Bạn chưa có đơn hàng nào trong giỏ.");
                gvCart.DataSource = null;
                gvCart.DataBind();
                lblTotalMoney.Text = "0 VNĐ";
            }
        }

        private void ShowAlert(string message)
        {
            AlertPanel.CssClass = "alert alert-warning alert-container";
            lblAlert.Text = message;
            AlertPanel.Visible = true;
        }

        protected void gvCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteProduct")
            {
                int productId = Convert.ToInt32(e.CommandArgument);
                int userId = Convert.ToInt32(Session["UserId"]);

                dbClass db = new dbClass();

                // Lấy đơn hàng hiện tại
                string queryOrder = "SELECT TOP 1 id_Order FROM Orders WHERE id_User = @UserId AND Status = 0 ORDER BY Order_date DESC";
                SqlParameter[] paramOrder = { new SqlParameter("@UserId", userId) };
                object orderObj = db.ExecuteScalar(queryOrder, paramOrder);

                if (orderObj != null)
                {
                    int orderId = Convert.ToInt32(orderObj);

                    // Xoá sản phẩm khỏi chi tiết đơn hàng
                    string deleteQuery = "DELETE FROM Order_Details WHERE id_Order = @OrderId AND id_Product = @ProductId";
                    SqlParameter[] deleteParams = {
                        new SqlParameter("@OrderId", orderId),
                        new SqlParameter("@ProductId", productId)
                    };
                    db.RunQuery(deleteQuery, deleteParams);

                    // Reload lại giỏ hàng
                    LoadCartData(userId);
                }
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            ShowAlert("Tính năng đang được phát triển");
        }
        protected void btnBackHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

    }
}
