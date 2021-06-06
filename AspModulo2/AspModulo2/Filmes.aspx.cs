using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspModulo2
{
    public partial class Filmes : System.Web.UI.Page
    {
        public List <String> Filme { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            //inserindo valores da textbox dentro do dropdrowlist
            DDLFIlmes.Items.Add(new ListItem(txtvalor.Text));
        }

        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            Filme = new List<string>();
                foreach (ListItem item in DDLFIlmes.Items)
            {
                Filme.Add(item.Text);
               
            }

        }
    }
}