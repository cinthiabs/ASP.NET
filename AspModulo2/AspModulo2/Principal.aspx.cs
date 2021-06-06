using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspModulo2
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //acessando o cookie
            LbLogin.Text = "";
            if(Request.Cookies["login"] != null)
            {
                LbLogin.Text = Request.Cookies["login"].Value;
            }

        }

        protected void BtExecutar_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["login"]!= null)
            {
                //removendo o cookie
               // Request.Cookies.Clear();
                 Request.Cookies.Remove("login");
                int n = Request.Cookies.Count;

            }
        }

        protected void BtListar_Click(object sender, EventArgs e)
        {
            //pegar todos os cookies armazenados da pagina do cliente
             var keys = Request.Cookies.AllKeys;
            string texto = "<p>";
            string pos = "";
            for (int i = 0; i <Request.Cookies.Count; i++)
            {
                pos = keys[i];
                texto += Request.Cookies[pos].Value;
            }
            texto += "</p>";
            Response.Write(texto);
        }
    }
}