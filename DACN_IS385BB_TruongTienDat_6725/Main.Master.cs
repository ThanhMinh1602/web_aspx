using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace DACN_IS385BB_TruongTienDat_6725
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Không cần thay đổi
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào
            if (string.IsNullOrWhiteSpace(tk.Text) || string.IsNullOrWhiteSpace(pass.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
                    "alert('Vui lòng nhập email và mật khẩu');", true);
                return;
            }

            try
            {
                // Sử dụng dbClass để lấy dữ liệu
                dbClass db = new dbClass();
                string query = "SELECT id_User, Email, Password, id_Role FROM [User] WHERE Email = '" + tk.Text.Trim() + "'";
                using (SqlDataReader reader = db.GetRecord(query))
                {
                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        string storedPassword = reader["Password"].ToString();
                        string enteredPassword = pass.Text;
                        int roleId = Convert.ToInt32(reader["id_Role"]);

                        // Kiểm tra mật khẩu (so sánh trực tiếp)
                        if (enteredPassword == storedPassword)
                        {
                            // Kiểm tra vai trò
                            if (roleId == 3) // Customer
                            {
                                // Lưu thông tin vào phiên
                                Session["UserId"] = reader["id_User"].ToString();
                                Session["Email"] = reader["Email"].ToString();
                                Session["RoleId"] = roleId.ToString();

                                // Chuyển hướng đến trang chính
                                Response.Redirect("~/default.aspx");
                            }
                            else if (roleId == 1 || roleId == 2) // Admin hoặc Manager
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
                                    "alert('Chức năng dành cho " + (roleId == 1 ? "Admin" : "Manager") + " đang được phát triển');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
                                    "alert('Vai trò không hợp lệ');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
                                "alert('Sai mật khẩu');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
                            "alert('Email không tồn tại');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi (khuyến nghị dùng Serilog hoặc NLog)
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
                    "alert('Đã xảy ra lỗi. Vui lòng thử lại sau.');", true);
                Console.WriteLine("Error during login: " + ex.Message);
            }
        }
        protected void ButtonCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Cart.aspx");
        }
        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            // Xóa session và chuyển hướng về trang chính
            Session.Clear();
            Session.Abandon();
            Response.Redirect(Request.RawUrl);
        }
    }
}