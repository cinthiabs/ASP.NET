using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspModulo2
{
    public partial class Resposta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write("<h1>teste</h1>");
            //Response.Write("<h3>Salario: "+Request.Form["txtsalario"]+"</h3>");
            //Response.Write("<h3>Desconto: " + Request.Form["txtdesconto"] + "</h3>");
            //Response.Write("<h3>Nome: " + Request.QueryString["Nome"] + "</h3>");


            if (Request["txtsalario"] == null ) 
            {
                Response.Redirect("~/Salario.aspx");
            }

            Double Salario = Convert.ToDouble(Request.Form["txtsalario"]);
            Double desconto = 0;
            RadioButtonList rb = (RadioButtonList) Page.PreviousPage.FindControl("RBDesconto");
            if (rb.SelectedIndex != 3)
            {
                switch (rb.SelectedIndex)
                {
                    case 0:
                        desconto = 10;
                        break;
                    case 1:
                        desconto = 20;
                        break;
                    case 2:
                        desconto = 30;
                        break;

                }


            }
            else
            {
                 desconto = Convert.ToDouble(Request["txtdesconto"]);
            }
           
            Double resultado = (Salario * desconto) / 100;
            Double salarioLiquido = Salario - resultado;

            Table tabela = new Table();
            //Salario Bruto
            TableRow linha = new TableRow();

            TableCell coluna = new TableCell();

            coluna.Text = "Salario Bruto: ";
            linha.Cells.Add(coluna);

            coluna = new TableCell();
            coluna.Text = Salario.ToString();
            linha.Cells.Add(coluna);
            tabela.Rows.Add(linha);

            //Desconto
             linha = new TableRow();

             coluna = new TableCell();

            coluna.Text = "Percentual de desconto: ";
            linha.Cells.Add(coluna);

            coluna = new TableCell();
            coluna.Text = desconto.ToString();
            linha.Cells.Add(coluna);
            tabela.Rows.Add(linha);


            //Salario liquido
            linha = new TableRow();

            coluna = new TableCell();

            coluna.Text = "Salario Liquido: ";
            linha.Cells.Add(coluna);

            coluna = new TableCell();
            coluna.Text = salarioLiquido.ToString();
            linha.Cells.Add(coluna);
            tabela.Rows.Add(linha);



            PlaceHolder1.Controls.Add(tabela);
        }
    }
}