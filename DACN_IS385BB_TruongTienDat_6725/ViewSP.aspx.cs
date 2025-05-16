using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace DACN_IS385BB_TruongTienDat_6725
{
    public partial class ViewSP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTen.Text = "";
                lblmota.Text = "";
                lblgia.Text = "";
                lblgiagiam.Text = "";
                string param = Request.QueryString["id"]?.ToString();
                if (!string.IsNullOrEmpty(param) && int.TryParse(param, out int spID))
                {
                    SqlDataReader myDr = new dbClass().GetRecord("SELECT Img, Tittle, Description, Price, Disscount FROM Product WHERE id_Product=" + spID);
                    if (myDr.HasRows)
                    {
                        myDr.Read();
                        Image1.ImageUrl = "~/Image/" + myDr.GetValue(0).ToString();
                        lblTen.Text = myDr.GetValue(1).ToString();
                        lblmota.Text = myDr.GetValue(2).ToString();
                        decimal dg = Convert.ToDecimal(myDr.GetValue(3));
                        lblgia.Text = dg.ToString("###,###,###");
                        decimal dgg = Convert.ToDecimal(myDr.GetValue(4));
                        lblgiagiam.Text = dgg.ToString("###,###,###");
                    }
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vui lòng đăng nhập để xem giỏ hàng')", true);
            }
            else
            {
                Response.Redirect("~/Cart.aspx"); // Assumes you have a Cart.aspx page to view the cart
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Vui lòng đăng nhập để mua hàng')", true);
                return;
            }

            if (!int.TryParse(TextBox1.Text, out int soluong) || soluong <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Nhập sai số lượng')", true);
                return;
            }

            int userId = Convert.ToInt32(Session["UserId"]);
            int productId = Convert.ToInt32(Request.QueryString["id"]);
            decimal price = 0;
            dbClass db = new dbClass();

            try
            {
                // Lấy giá sản phẩm (ưu tiên giá giảm nếu có)
                using (SqlDataReader reader = db.GetRecord("SELECT ISNULL(NULLIF(Disscount, 0), Price) FROM Product WHERE id_Product = @id_Product AND Deleted IS NULL",
                    new SqlParameter("@id_Product", productId)))
                {
                    if (reader.Read())
                        price = Convert.ToDecimal(reader.GetValue(0));
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sản phẩm không tồn tại')", true);
                        return;
                    }
                }

                // Kiểm tra đơn hàng Pending (Status = 0)
                int orderId = 0;
                using (SqlDataReader reader = db.GetRecord("SELECT id_Order FROM Orders WHERE id_User = @id_User AND Status = 0",
                    new SqlParameter("@id_User", userId)))
                {
                    if (reader.Read())
                    {
                        orderId = Convert.ToInt32(reader.GetValue(0));
                    }
                    else
                    {
                        // Tạo đơn mới Pending (Status = 0)
                        db.RunQuery("INSERT INTO Orders (id_User, Order_date, Status, Total_Money) VALUES (@id_User, GETDATE(), 0, 0)",
                            new SqlParameter("@id_User", userId));
                        orderId = Convert.ToInt32(db.ExecuteScalar("SELECT TOP 1 id_Order FROM Orders WHERE id_User = @id_User AND Status = 0 ORDER BY id_Order DESC",
                            new SqlParameter("@id_User", userId)));
                    }
                }

                // Kiểm tra sản phẩm đã có trong giỏ hàng
                object result = db.ExecuteScalar("SELECT Number FROM Order_Details WHERE id_Order = @id_Order AND id_Product = @id_Product",
                    new SqlParameter("@id_Order", orderId),
                    new SqlParameter("@id_Product", productId));

                if (result != null)
                {
                    int currentQty = Convert.ToInt32(result);
                    int newQty = currentQty + soluong;
                    db.RunQuery("UPDATE Order_Details SET Number = @Number, Total_Money = @TotalMoney WHERE id_Order = @id_Order AND id_Product = @id_Product",
                        new SqlParameter("@Number", newQty),
                        new SqlParameter("@TotalMoney", newQty * price),
                        new SqlParameter("@id_Order", orderId),
                        new SqlParameter("@id_Product", productId));
                }
                else
                {
                    db.RunQuery("INSERT INTO Order_Details (id_Order, id_Product, Price, Number, Total_Money) VALUES (@id_Order, @id_Product, @Price, @Number, @TotalMoney)",
                        new SqlParameter("@id_Order", orderId),
                        new SqlParameter("@id_Product", productId),
                        new SqlParameter("@Price", price),
                        new SqlParameter("@Number", soluong),
                        new SqlParameter("@TotalMoney", soluong * price));
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Đã thêm vào giỏ hàng')", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('Lỗi: {ex.Message}')", true);
            }
        }



    }
}