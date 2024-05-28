/****************************************************************************
**          SAKARYA ÜNİVERSİTESİ
** BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**      BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
**     NESNEYE DAYALI PROGRAMLAMA DERSİ
**          2021-2022 BAHAR DÖNEMİ
**
**      ÖDEV NUMARASI..........: 2. ÖDEV
**      ÖĞRENCİ ADI............: MERVE
**      ÖĞRENCİ NUMARASI.......: GÜNAY
**      DERSİN ALINDIĞI GRUP...: C GURUBU
****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDPOdev2
{
    public class Form1 : Form
    {
        private Button btnHesapla;
        private Label labelX;
        private Label labelY;
        private Label lblYazi;
        private TextBox txtSayi;

        public Form1()
        {
            // form elemanlarını oluştur ve özelliklerini belirle
            btnHesapla = new System.Windows.Forms.Button();
            labelX = new System.Windows.Forms.Label();
            labelY = new System.Windows.Forms.Label();
            lblYazi = new System.Windows.Forms.Label();
            txtSayi = new System.Windows.Forms.TextBox();

            btnHesapla.Location = new System.Drawing.Point(220, 310);
            btnHesapla.Size = new System.Drawing.Size(200, 70);
            btnHesapla.Name = "btnHesapla";
            btnHesapla.Text = "HESAPLA";
            btnHesapla.Click += new System.EventHandler(this.hesapla);

            labelX.AutoSize = true;
            labelX.Location = new System.Drawing.Point(130, 75);
            labelX.Name = "labelX";
            labelX.Text = "X";

            labelY.AutoSize = true;
            labelY.Location = new System.Drawing.Point(130, 180);
            labelY.Name = "labelY";
            labelY.Text = "Y";

            lblYazi.AutoSize = true;
            lblYazi.Location = new System.Drawing.Point(250, 190);
            lblYazi.Name = "lblYazi";
            lblYazi.MaximumSize = new System.Drawing.Size(290, 150);

            txtSayi.Location = new System.Drawing.Point(250, 80);
            txtSayi.Name = "txtSayi";
            txtSayi.Size = new System.Drawing.Size(290, 32);

            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ClientSize = new System.Drawing.Size(650, 450);
            this.Name = "Form";
            this.Text = "Yazıya Çevir";

            this.Controls.Add(btnHesapla);
            this.Controls.Add(labelX);
            this.Controls.Add(labelY);
            this.Controls.Add(lblYazi);
            this.Controls.Add(txtSayi);
        }

        private void hesapla(object sender, EventArgs e)
        {
            string input = txtSayi.Text;

            string[] tlKurus = new string[2] { "", "" };
            bool hata = false;
            int say = 0;
            // textboxa girilen sayı geziliyor ve formatı doğrumu diye kontrol ediliyor
            // ayrıca virgülden önce ve sonraki kısımları ayrılıyor
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsNumber(input[i]))
                {
                    tlKurus[say] += input[i].ToString();
                }
                else if (input[i] == '.' && say == 0)
                {
                    say++;
                }
                else hata = true;
            }

            // Format doğru değilse hata ver
            if (hata || tlKurus[0].Length > 5 || tlKurus[1].Length > 2)
            {
                MessageBox.Show("Hatalı Format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                lblYazi.Text = yaziyaCevir(tlKurus[0]) + " TL " + yaziyaCevir(tlKurus[1]) + " KURUŞ";
            }
        }

        // rakamlar içeren string alıp türkçe olarak yazıya çeviriyor
        private string yaziyaCevir(string text)
        {
            string yazi = "";

            string[] birler = { "", " BİR", " İKİ", " ÜÇ",
            " DÖRT", " BEŞ", " ALTI", " YEDİ", " SEKİZ", " DOKUZ" };
            string[] onlar = { "", " ON", " YİRMİ",
            " OTUZ", " KIRK", " ELLİ", " ALTMIŞ", " YETMİŞ", " SEKSEN",
            " DOKSAN" };

            // alınan string i gezip birler onlar ve yüzler basamaklarına göre uygun yazıyı ekliyor
            int sondan = 1;
            for (int i = text.Length - 1; i >= 0; i--)
            {
                int rakam = text[i] - '0';
                // birler basamağı
                if (sondan % 3 == 1)
                {
                    if (sondan == 1) yazi = birler[rakam] + yazi;
                    else if (rakam == 1 && sondan != text.Length) yazi = birler[rakam] + " BİN" + yazi;
                    else if (rakam == 1) yazi = " BİN" + yazi;
                    else if (rakam == 0 && sondan != text.Length) yazi = " BİN" + yazi;
                    else if (rakam > 1) yazi = birler[rakam] + " BİN" + yazi;
                }
                // onlar basamağı
                else if (sondan % 3 == 2)
                {
                    yazi = onlar[rakam] + yazi;
                }
                // yüzler basamağı
                else if (sondan % 3 == 0)
                {
                    if (rakam == 1) yazi = " YÜZ" + yazi;
                    else if (rakam > 1) yazi = birler[rakam] + " YÜZ" + yazi;
                }
                sondan++;
            }

            if (yazi == "") yazi = "SIFIR";

            return yazi;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}