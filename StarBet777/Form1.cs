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

namespace StarBet777
{
    public partial class Form1 : Form
    {
        int[] roleta;
        int[] ciclos;
        Label[] tela;
        Random r;
        public Form1()
        {
            InitializeComponent();
            roleta = new int[3];
            ciclos = new int[3];
            tela = new Label[] { label1, label2, label3 };
            r = new Random();
            for (int i = 0; i < roleta.Length; i++)
            {
                roleta[i] = r.Next(0, 10);
                Update(i);
            }
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
        }

        void Update(int i)
        {
            tela[i].Text = roleta[i].ToString();
        }

        private void btSpin_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ciclos.Length; i++)
            {
                ciclos[i] = r.Next(1, 21);
                tela[i].ForeColor = Color.Black;
            }
            btSpin.Enabled = false;
            tmrSpin.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tmrSpin_Tick(object sender, EventArgs e)
        {
            bool stop = true;
            for (int i = 0; i < ciclos.Length; i++)
            {
                if (ciclos[i] > 0)
                {
                    ciclos[i]--;
                    roleta[i]++;
                    if (ciclos[i] == 0)
                    {
                        tela[i].ForeColor = Color.Red;
                    }
                    if (roleta[i] == 10)
                    {
                        roleta[i] = 0;
                    }
                    Update(i);
                    stop &= false;
                }
                else
                {
                    stop &= true;
                }
            }
            if (stop)
            {
                tmrSpin.Enabled = false;
                btSpin.Enabled = true;
                if (roleta[0] == roleta[1] && roleta[1] == roleta[2])
                {
                    MessageBox.Show("Você ganhou!");
                }
                if(!chbVitorias.Checked || (roleta[0] == roleta[1] && roleta[1] == roleta[2]))
                   lbxUltimos.Items.Add($"{roleta[0]} - {roleta[1]} - {roleta[2]}");

            }


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        List<string> jogadas;
        private void chbVitorias_CheckedChanged(object sender, EventArgs e)
        {
            if (chbVitorias.Checked)
            {
                jogadas = new List<string>();
                foreach (string item in lbxUltimos.Items)
                {
                    jogadas.Add(item);
                }
                lbxUltimos.Items.Clear();
                foreach (string item in jogadas)
                {
                    string[] nums = item.Split('-');
                    if (nums[0] == nums[1] && nums[1] == nums[2])
                        lbxUltimos.Items.Add(item);

                }
            }
            else
            {
                lbxUltimos.Items.Clear();
                foreach(string item in jogadas)
                {
                    lbxUltimos.Items.Add(item);
                }
            }
        }

        private void lbxUltimos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

 

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ActiveControl == btSpin)
                {
                    btSpin.PerformClick();  
                }
                else if (ActiveControl == chbVitorias)
                {
                    chbVitorias.Checked = !chbVitorias.Checked;  
                }
            }
        }
    }
}
