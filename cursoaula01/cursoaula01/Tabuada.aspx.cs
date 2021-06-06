using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cursoaula01
{
    public partial class Tabuada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //retornado a mesma pagina, ou seja, já carregou uma vez 
            {
                for (int i = 2; i < 11; i++)
                {
                    dlNumeros.Items.Add(i.ToString());
                }
            }
        }

        protected void BtnExecutar_Click(object sender, EventArgs e)
        {
            //listbox
            LbDados.Items.Clear();
            ListItem li = dlNumeros.SelectedItem;
            int n = Convert.ToInt32(li.Value);
            int resultado = 0;

            for (int i = 0; i < 11; i++)
            {
                resultado = i * n; 
                LbDados.Items.Add(i.ToString()+" X "+n.ToString()+" = "+ resultado.ToString());
                //listbox
                tbDados.Rows[i].Cells[0].Text = n.ToString();
                tbDados.Rows[i].Cells[4].Text = resultado.ToString();
                
            } 
           
            
            

        }
    }
}