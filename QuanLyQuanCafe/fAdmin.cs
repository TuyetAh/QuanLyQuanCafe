using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();

        BindingSource accountList = new BindingSource();

        public Account loginAccount;

        public fAdmin()
        {
            InitializeComponent();
            Load();
        }



        #region methods

        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);
            return listFood;
        } 
        void Load()
        {
            dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;

            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadAccount();
            LoadCategoryIntoComboBox(cbFoodCategory);
            AddFoodBinding();
            AddAccountBinding();
        }

        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            numericUpDown1.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAcount();
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }

        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name",true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryIntoComboBox(ComboBox cb)
        {
            cb.DataSource = null;         // xóa liên kết cũ
            cb.Items.Clear();             // xóa tất cả item đang hiển thị (dù đã null)
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }

        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }

        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }

            LoadAccount();

        }

        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }

            LoadAccount();

        }

        void DeleteAccount(string userName)
        {
            if(loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Vui lòng đừng xóa chính bạn chứ ");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }

            LoadAccount();

        }

        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassWord(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }


        }

        #endregion

        #region events

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;

            AddAccount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            DeleteAccount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)numericUpDown1.Value;

            EditAccount(userName, displayName, type);
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            ResetPass(userName);
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }


        #endregion

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category category = CategoryDAO.Instance.GetCategoryByID(id);

                    cbFoodCategory.SelectedItem = category;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == category.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cbFoodCategory.SelectedIndex = index;
                }
            }
            catch { }
            
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công");
                LoadListFood();
                if(insertFood != null)
                    insertFood(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm thức ăn");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món thành công");
                LoadListFood();
                if (deleteFood != null)
                    deleteFood(this,new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi xóa thức ăn");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListFood();
                if (updateFood != null)
                    updateFood(this,new EventArgs());
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn");
            }
        }

        private void btnFirstBillPage_Click(object sender, EventArgs e)
        {
            txbPageBill.Text = "1";
        }

        private void btnLastBillPage_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value);

            int lastPage = sumRecord / 10;

            if (sumRecord % 10 != 0)
                lastPage++;
            txbPageBill.Text = lastPage.ToString();
        }

        private void txbPageBill_TextChanged(object sender, EventArgs e)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDateAndPage(dtpkFromDate.Value, dtpkToDate.Value, Convert.ToInt32(txbPageBill.Text));
        }

        private void btnPreviuorsBillPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);
            if(page>1)
                page--;
            txbPageBill.Text = page.ToString();
        }

        private void btnNexBillPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkFromDate.Value, dtpkToDate.Value);
            if (page < sumRecord)
                page++;
            txbPageBill.Text = page.ToString();

        }
        //




        private void btnViewRevenue_Click_1(object sender, EventArgs e)
        {
            DateTime fromDate = dtpkFrom.Value;
            DateTime toDate = dtpkTo.Value;

            float totalRevenue = BillDAO.Instance.GetRevenueByDate(fromDate, toDate);
            DataTable dailyRevenue = BillDAO.Instance.GetDailyRevenue(fromDate, toDate);

            dtgvReport.Columns.Clear();
            dtgvReport.Rows.Clear();

            // Tạo cột
            dtgvReport.Columns.Add("MoTa", "Mô Tả");
            dtgvReport.Columns.Add("GiaTri", "Giá Trị");

            // Hàng đầu tiên: tổng doanh thu
            CultureInfo viVN = new CultureInfo("vi-VN");

            dtgvReport.Rows.Add(
                $"Doanh thu từ {fromDate.ToShortDateString()} đến {toDate.ToShortDateString()}",
                totalRevenue.ToString("c0", viVN)
                        );

            // Dòng trống để phân cách
            dtgvReport.Rows.Add("", "");

            // Các hàng tiếp theo: doanh thu từng ngày
            foreach (DataRow row in dailyRevenue.Rows)
            {
                DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                float doanhThu = Convert.ToSingle(row["DoanhThu"]);
                dtgvReport.Rows.Add(ngay.ToShortDateString(), doanhThu.ToString("c0", viVN));
            }

            dtgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;



        }//OK



        private void btnTopFoods_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dtpkFrom.Value;
            DateTime toDate = dtpkTo.Value;

            DataTable topFood = FoodDAO.Instance.GetBestSellingFood(fromDate, toDate);
            DataTable allFoods = FoodDAO.Instance.GetAllSoldFoods(fromDate, toDate);

            dtgvReport.Columns.Clear();
            dtgvReport.Rows.Clear();

            dtgvReport.Columns.Add("MoTa", "Mô Tả");
            dtgvReport.Columns.Add("GiaTri", "Giá Trị");

            if (topFood.Rows.Count > 0)
            {
                string name = topFood.Rows[0]["Name"].ToString();
                string count = topFood.Rows[0]["TotalSold"].ToString();
                dtgvReport.Rows.Add($"Món bán chạy nhất từ {fromDate.ToShortDateString()} đến {toDate.ToShortDateString()}:", $"{count} phần ({name})");
            }
            else
            {
                dtgvReport.Rows.Add("Không có dữ liệu trong khoảng thời gian này.", "");
            }

            // Dòng trống để phân cách
            dtgvReport.Rows.Add("", "");

            // Các món khác
            foreach (DataRow row in allFoods.Rows)
            {
                string foodName = row["Name"].ToString();
                string totalSold = row["TotalSold"].ToString();
                dtgvReport.Rows.Add(foodName, $"{totalSold} phần");
            }

            dtgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }



        private void btnTotalBills_Click_1(object sender, EventArgs e)
        {
            DateTime fromDate = dtpkFrom.Value;
            DateTime toDate = dtpkTo.Value;

            int total = BillDAO.Instance.GetTotalBills(fromDate, toDate);
            DataTable dailyBills = BillDAO.Instance.GetDailyTotalBills(fromDate, toDate);

            dtgvReport.Columns.Clear();
            dtgvReport.Rows.Clear();

            dtgvReport.Columns.Add("MoTa", "Mô Tả");
            dtgvReport.Columns.Add("SoLuong", "Số Lượng");

            dtgvReport.Rows.Add(
                $"Tổng số hóa đơn từ {fromDate.ToShortDateString()} đến {toDate.ToShortDateString()}",
                total.ToString()
            );

            // Dòng trống để phân cách
            dtgvReport.Rows.Add("", "");

            // Các hàng tiếp theo: từng ngày
            foreach (DataRow row in dailyBills.Rows)
            {
                DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                int soHoaDon = Convert.ToInt32(row["SoHoaDon"]);
                dtgvReport.Rows.Add(ngay.ToShortDateString(), soHoaDon.ToString());
            }

            dtgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dtgvReport.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV file (*.csv)|*.csv";
            sfd.FileName = "BaoCao.csv";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                    {
                        // Ghi tiêu đề cột
                        for (int i = 0; i < dtgvReport.Columns.Count; i++)
                        {
                            sw.Write(dtgvReport.Columns[i].HeaderText);
                            if (i < dtgvReport.Columns.Count - 1)
                                sw.Write(",");
                        }
                        sw.WriteLine();

                        // Ghi từng dòng dữ liệu
                        foreach (DataGridViewRow row in dtgvReport.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                for (int i = 0; i < dtgvReport.Columns.Count; i++)
                                {
                                    var cell = row.Cells[i].Value?.ToString().Replace(",", ""); // tránh lỗi dấu phẩy
                                    sw.Write(cell);
                                    if (i < dtgvReport.Columns.Count - 1)
                                        sw.Write(",");
                                }
                                sw.WriteLine();
                            }
                        }
                    }

                    MessageBox.Show("Xuất file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}
