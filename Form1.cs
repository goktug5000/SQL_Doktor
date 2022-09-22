using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

namespace SQL_Doktor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panel1.Visible=false;
            baglan();
        }
        bool panelB = false;
        private void button6_Click(object sender, EventArgs e)
        {
            panelB = !panelB;
            panel1.Visible = panelB;
        }
        SqlConnection baglantı;
        SqlCommand komut;


        private void button5_Click(object sender, EventArgs e)
        {
            doktorGoster();
        }
        void doktorGoster()
        {
            if (baglantı.State.ToString() != "Open")
            {
                baglantı.Open();
            }

            SqlCommand komut = new SqlCommand("SELECT * FROM [Doctors]", baglantı);

            SqlDataAdapter adap = new SqlDataAdapter(komut);

            DataTable tablo = new DataTable();

            adap.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        string baglantıAdresi;
        private void button4_Click(object sender, EventArgs e)
        {

            StreamReader oku = new StreamReader("C:\\Adres.txt");
            baglantıAdresi = oku.ReadLine();
            MessageBox.Show(baglantıAdresi);
            baglantı = new SqlConnection(baglantıAdresi);

        }
        void baglan()
        {
            baglantı = new SqlConnection(@"Data Source=DESKTOP-UMRGVU7\TEW_SQLEXPRESS;Initial Catalog=Doctor;Integrated Security=True");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            hastaGoster();
        }
        void hastaGoster()
        {
            if (baglantı.State.ToString() != "Open")
            {
                baglantı.Open();
            }

            komut = new SqlCommand("SELECT * FROM [Hasta]", baglantı);

            SqlDataAdapter adap = new SqlDataAdapter(komut);

            DataTable tablo = new DataTable();

            adap.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (baglantı.State.ToString() != "Open")
            {
                baglantı.Open();
            }
            doktor dok = new doktor();
            SqlCommand cmd = new SqlCommand("insert into Doctors (unvan,ad,soyad) VALUES (@unvan,@ad,@soyad)", baglantı);
            cmd.Parameters.AddWithValue("@unvan", textBox2.Text);
            cmd.Parameters.AddWithValue("@ad", textBox3.Text);
            cmd.Parameters.AddWithValue("@soyad", textBox4.Text);
            cmd.ExecuteNonQuery();
            doktorGoster();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (baglantı.State.ToString() != "Open")
            {
                baglantı.Open();
            }
            
            SqlCommand cmd = new SqlCommand("insert into Hasta (TC,Ad,Soyad) VALUES (@TC,@Ad,@Soyad)", baglantı);
            cmd.Parameters.AddWithValue("@TC", textBox5.Text);
            cmd.Parameters.AddWithValue("@Ad", textBox6.Text);
            cmd.Parameters.AddWithValue("@Soyad", textBox7.Text);
            cmd.ExecuteNonQuery();
            hastaGoster();
        }

        public abstract class kisiler
        {
            public string add, soyadd;
            abstract public void bilgiler();
        }
        public class doktor:kisiler
        {
           public string unvann;
           public override void bilgiler()
            {
                throw new NotImplementedException();
            }
        }
        public class hastaa:kisiler
        {
            public string TCC;
            public override void bilgiler()
            {
                throw new NotImplementedException();
            }
        }
    }
}
