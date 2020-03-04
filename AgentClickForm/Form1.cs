using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgentClickForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Btn_satis_Click(object sender, EventArgs e)
        {

            DialogResult result = new DialogResult();

            result = MessageBox.Show("SATIŞ GİRMEK İSTEDİĞİNİZDEN EMİNMİSİNİZ ?", "ONAY", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                // DB Insert Code


                var activedirectoryname = System.Environment.UserName;

                Net.Code.ADONet.Db db = new Net.Code.ADONet.Db("server=10.26.1.21;database=Tempo_MillenicomSalesDashboard;user Id=Tempo_MillenicomSalesDashboard;password=Tempo_MillenicomSalesDashboard!;", "System.Data.SqlClient");

                string dateTime = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");

                string BaglantiAdresi = "server=10.26.1.21;database=Tempo_MillenicomSalesDashboard;user Id=Tempo_MillenicomSalesDashboard;password=Tempo_MillenicomSalesDashboard!;";
                SqlConnection con = new SqlConnection(BaglantiAdresi);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE AgentName = '" + activedirectoryname + "' and SalesDate = '" + dateTime + "'";
                con.Open();
                object userCount = cmd.ExecuteScalar();
                con.Close();


                if (Convert.ToInt32(userCount) == 0)
                {
                    var sproc = db.Sql($"INSERT INTO Users (AgentName,SalesCount,SalesDate) Values ('{activedirectoryname}', 1,'" + dateTime + "')");
                    int? dbresult = sproc.AsScalar<int?>();

                }
                else
                {
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT SalesCount FROM Users WHERE AgentName = '" + activedirectoryname + "' and SalesDate = '" + dateTime + "'";
                    con.Open();
                    object userSalesCount = cmd.ExecuteScalar();
                    con.Close();

                    var salesCount = Convert.ToInt32(userSalesCount);


                    con.Open();
                    string kayit = "UPDATE Users SET SalesCount=@salesCounts where AgentName = @userName and SalesDate = @date";
                    // müşteriler tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
                    SqlCommand komut = new SqlCommand(kayit, con);
                    //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                    komut.Parameters.AddWithValue("@salesCounts", (salesCount + 1));
                    komut.Parameters.AddWithValue("@userName", activedirectoryname);
                    komut.Parameters.AddWithValue("@date", dateTime);
                    
                    //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                    komut.ExecuteNonQuery();
                    //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                    con.Close();
                }

                MessageBox.Show("SATIŞINIZ EKLENDİ");
            }
            else
            {
                return;
            }

        }
    }
}
