namespace _20200305013
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            // Kullan�c� ad� ve �ifre kontrol�
            if (txtKullaniciAdi.Text == "sena" && txtSifre.Text == "123")
            {
                
                Form2 form2 = new Form2();
                this.Hide();
                form2.Show();
            }
            else
            {
                // Hatal� giri�
                MessageBox.Show("Username or Password is incorrect", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtKullaniciAdi_TextChanged(object sender, EventArgs e)
        {

        }
    }
}