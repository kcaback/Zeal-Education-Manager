using System;
using System.Data;
using System.Web.UI.WebControls;
using static ZealEducationManager.Models.CommonFn;

namespace ZealEducationManager.Admin
{
    public partial class ClassFees : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                GetClass();
                GetFees();
            }
        }
        private void GetClass()
        {
            DataTable dt = fn.Fletch("Select * from Class");
            ddlClass.DataSource = dt;
            ddlClass.DataTextField = "ClassName";
            ddlClass.DataValueField = "ClassId";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, "Select class");
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string classVal = ddlClass.SelectedItem.Text;
                DataTable dt = fn.Fletch("Select * from Fees where ClassId = '" + ddlClass.SelectedItem.Value + "'");
                long feeAmount = Convert.ToInt64(txtFeeAmounts.Text.Trim());
                if (dt.Rows.Count == 0)
                {
                    if (feeAmount > 999999999)
                    {
                        lblMsg.Text = "Fee must be <= 999999999!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    else if (feeAmount < 0)
                    {
                        lblMsg.Text = "Fee must be >= 0!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    else
                    {
                        string query = "Insert into Fees values('" + ddlClass.SelectedItem.Value + "','" + txtFeeAmounts.Text.Trim() + "')";
                        fn.Query(query);
                        lblMsg.Text = "Inserted successfully!";
                        lblMsg.CssClass = "alert alert-success";
                        ddlClass.SelectedIndex = 0;
                        txtFeeAmounts.Text = string.Empty;
                        GetFees();
                    }
                }
                else
                {
                    lblMsg.Text = "Entered fees already exists for <b>'" + classVal + "'</b>!";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message + "!";
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        private void GetFees()
        {
            DataTable dt = fn.Fletch(@"Select Row_NUMBER() over(Order by (Select 1)) as [Sr.No], f.FeeId, f.ClassId, c.ClassName, f.FeeAmount from Fees f inner join Class c on c.ClassId = f.ClassId");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetFees();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetFees();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int feesId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                fn.Query("Delete from Fees where FeeId = '" + feesId + "'");
                lblMsg.Text = "Fees deleted successfully!";
                lblMsg.CssClass = "alert alert-success";
                GridView1.EditIndex = -1;
                GetFees();
            } catch (Exception ex)
            {
                lblMsg.Text = ex.Message + "!";
                lblMsg.CssClass = "alert alert-danger";
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetFees();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int feeId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                string feeAmt = (row.FindControl("TextBox1") as TextBox).Text.Trim();
                if (feeAmt == null || feeAmt == "")
                {
                    lblMsg.Text = "Please enter fee amount!";
                    lblMsg.CssClass = "alert alert-danger";
                }
                else
                {
                    long feeAtmCheck = Convert.ToInt64(feeAmt);
                    if (feeAtmCheck > 999999999)
                    {
                        lblMsg.Text = "Fee must be <= 999999999!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    else if (feeAtmCheck < 0)
                    {
                        lblMsg.Text = "Fee must be >= 0!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                    else
                    {
                        fn.Query("Update Fees set FeeAmount = '" + feeAmt.Trim() + "' where FeeId = '" + feeId + "'");
                        lblMsg.Text = "Fees updated successfully!";
                        lblMsg.CssClass = "alert alert-success";
                        GridView1.EditIndex = -1;
                        GetFees();
                    }
                }
            } catch (Exception ex)
            {
                lblMsg.Text = ex.Message + "!";
                lblMsg.CssClass = "alert alert-danger";
            }
        }
    }
}