using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cursoaula01
{
    public partial class ParouImpar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BlLista_Click(object sender, BulletedListEventArgs e)
        {
            switch (e.Index)
            {
                case 0:
                    pnParouImpar.Visible = true;
                    break;
                case 1:
                    PnFatorial.Visible = true;
                    break;
            }
        }

        protected void BtnVerifica_Click(object sender, EventArgs e)
        {
         
            int n = Convert.ToInt32(txtValorpn1.Text);
            string msg = "O número é par.";
            if (n % 2!=0)
            {
                msg = "O numero é impar.";
            }
            Lresp1.Text = msg;
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedIndex == 0)
            {
                pnParouImpar.Visible = true;

            }
            else
            {
                PnFatorial.Visible = true;
            }

        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(txtValorpn2.Text);
            if (n < 0 || n == null)
            {
                Lresp2.Text = "Informe apenas numeros positivos.";
            }
            else if (n == 0)
            {
                Lresp2.Text = "O fatorial de zero é 1";
            }
            else if(n > 0)
            {
                int t = n;
                for(int i = n-1; i >0; i --)
                {
                    t = t * i;
                }
                Lresp2.Text = n.ToString() + "! = " + t.ToString();
            }
            else
            {
                Lresp2.Text = "Permitido apenas numeros positivos.";
            }
        }
    }
}