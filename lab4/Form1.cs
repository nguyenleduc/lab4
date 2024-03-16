using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace lab4
{
    public partial class fMain : Form
    {
        private readonly string conString = "Data Source=LEDUC\\LEDUC16;Initial Catalog=QUANLYOTO;Integrated Security=True";
        private SqlConnection con = new SqlConnection();

        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadDataForComboBox();
        }

        private void LoadData()
        {
            try
            {
                using (con = new SqlConnection(conString))
                {
                    con.Open();
                    string qry = "SELECT * FROM Oto";
                    SqlDataAdapter da = new SqlDataAdapter(qry, con);
                    DataTable dtOto = new DataTable();
                    da.Fill(dtOto);
                    dataGridView1.DataSource = dtOto;
                    dataGridView1.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                 using (con = new SqlConnection(conString))
                   {
                        con.Open();
                        string qry = "INSERT INTO Oto (MaXe, TenXe, HangXe, GiaXe, NamSX) VALUES (@Id, @Name, @Brand, @Price, @Date)";
                        SqlCommand cmd = new SqlCommand(qry, con);
                        cmd.Parameters.AddWithValue("@Id", txtId.Text);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Brand", cbBrand.Text);
                        cmd.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text));
                        cmd.Parameters.AddWithValue("@Date", DateTime.Parse(txtDate.Text));

                        int rowAffected = cmd.ExecuteNonQuery();
                        if (rowAffected > 0)
                        {
                            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Thêm không thành công", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                
            } catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

       

      

        private void btnEdit_Click(object sender, EventArgs e)
        {
           

            try
            {
                using (con = new SqlConnection(conString))
                {
                    con.Open();
                    string qry = "UPDATE Oto SET TenXe = @Name, HangXe = @Brand, GiaXe = @Price, NamSX = @Date WHERE MaXe = @Id";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.Parameters.AddWithValue("@Id", txtId.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Brand", cbBrand.Text);
                    cmd.Parameters.AddWithValue("@Price", decimal.Parse(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@Date", DateTime.Parse(txtDate.Text));

                    int rowAffected = cmd.ExecuteNonQuery();
                    if (rowAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật không thành công", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {

            try
            {
                using (con = new SqlConnection(conString))
                {
                    con.Open();
                    string qry = "DELETE FROM Oto WHERE MaXe = @Id";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.Parameters.AddWithValue("@Id", txtId.Text);

                    int rowAffected = cmd.ExecuteNonQuery();
                    if (rowAffected > 0)
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void LoadDataForComboBox()
        {
            try
            {
                using (con = new SqlConnection(conString))
                {
                    con.Open();
                    string qry = "SELECT DISTINCT HangXe FROM Oto";
                    SqlCommand cmd = new SqlCommand(qry, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cbBrand.Items.Clear();
                    while (reader.Read())
                    {
                        cbBrand.Items.Add(reader["HangXe"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void cbBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.CurrentRow.Index;
            txtId.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            txtName.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtPrice.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtDate.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            cbBrand.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
        }
    }
}
