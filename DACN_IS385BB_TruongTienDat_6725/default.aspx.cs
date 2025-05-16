using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DACN_IS385BB_TruongTienDat_6725
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ListLoai.TypeName = "DACN_IS385BB_TruongTienDat_6725.dbClass";
            ListLoai.SelectMethod = "GetData";
            Parameter parameter = new Parameter
            {
                Name = "sql",
            DefaultValue = "Select * From [Category]"
            };
            ListLoai.SelectParameters.Add(parameter);

            ListSP.TypeName = "DACN_IS385BB_TruongTienDat_6725.dbClass";
            ListSP.SelectMethod = "GetData";
            Parameter parameter1 = new Parameter
            {
                Name = "sql",
                DefaultValue = "Select [id_Product], [id_Category], [Tittle], [Price], [Disscount], [Img] From [Product]"
            };
            ListSP.SelectParameters.Add(parameter1);

        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListBox1.SelectedIndex > -1)
            {
                int id_category = Convert.ToInt32(ListBox1.SelectedItem.Value);
                String sql = "SELECT [id_Product], [id_Category], [Tittle], [Price], [Disscount], [Img] FROM [Product] where id_Category=" + id_category;
                ListSP.TypeName = "DACN_IS385BB_TruongTienDat_6725.dbClass";
                ListSP.SelectMethod = "GetData";
                Parameter parameter1 = new Parameter
                {
                    Name = "sql",
                    DefaultValue = sql
                };
                ListSP.SelectParameters.Clear();
                ListSP.SelectParameters.Add(parameter1);
                ListView1.DataBind();
            }
        }

        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if(e.CommandName == "xem")
            {
                int masp = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("ViewSP.aspx?id=" + masp);
            }
        }
    }
}