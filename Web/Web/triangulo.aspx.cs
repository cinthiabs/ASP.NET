using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class triangulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public Boolean VerificaLados(int a, int b, int c)
        {
            Boolean retorno = false;
                if ((a < b + c) && (b < a + c) && (c < a + b))
                {
                    retorno = true;
                }
            return retorno;
        }
        protected void BtnVerificar_Click(object sender, EventArgs e)
        {
            int la = Convert.ToInt32(txtladoA.Text);
            int lb = Convert.ToInt32(txtladoB.Text);
            int lc = Convert.ToInt32(txtladoC.Text);

            Boolean verifica = VerificaLados(la, lb, lc);
             
            if (verifica == true)
            {
                Resposta.Text = "Os valores informados representam os lados de um triângulo";
            }
            else
            {
                Resposta.Text = "Os valores informados não representam os lados de um triângulo";
            }

        }
    }
}