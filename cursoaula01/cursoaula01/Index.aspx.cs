using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cursoaula01
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Executar_Click(object sender, EventArgs e)
        {
            if (txtMensagem.Text == "")
            {
                txtvisivel.Text ="Prencha o campo acima.";
            }
            else
            {
                txtvisivel.Text = "Olá "+ txtMensagem.Text; 
            }
        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            ListItem lista = new ListItem(Txtsite.Text,Txtendereco.Text);
            ListBoxLink.Items.Add(lista);
            Txtendereco.Text = "";
            Txtsite.Text = "";
           
        }
        protected void btnInserirList_Click(object sender, EventArgs e)
        {
           // ListItem item = ListSite.SelectedItem;
           // Txtsite.Text = item.Text;
           //item = ListBoxLink.SelectedItem;
           //Txtendereco.Text = item.Text;
            ListSite.Items.Clear();
            ListItem li;

            for(int i = 0; 1 < ListBoxLink.Items.Count;i ++)
            {
                li = ListBoxLink.Items[i];
                if(li.Selected)
                {
                    li.Selected = false;
                    ListSite.Items.Add(li);
                }
            }
            
        }
    }
}