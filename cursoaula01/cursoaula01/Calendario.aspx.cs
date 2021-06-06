using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cursoaula01
{
    public partial class Calendario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Calendar2.SelectedDate = DateTime.Now;
        }

        protected void ButtonDias_Click(object sender, EventArgs e)
        {
            //lbResultado.Text = Calendar2.SelectedDate.Date.ToString(); 
            int dian = 0;
            int anon = 0;
            int mesn = 0;
            int diaa = 0;
            int anoa = 0;
            int mesa = 0;
            dian = Calendar1.SelectedDate.Day;
            mesn = Calendar1.SelectedDate.Month*30;
            anon = Calendar1.SelectedDate.Year*365;

            diaa = Calendar2.SelectedDate.Day;
            mesa = Calendar2.SelectedDate.Month*30;
            anoa = Calendar2.SelectedDate.Year*365;
            int total = (diaa + mesa + anoa) - (dian + mesn + anon);
            lbResultado.Text= total.ToString();

        }
    }
}