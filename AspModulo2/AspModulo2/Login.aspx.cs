using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspModulo2
{
    public partial class Login : System.Web.UI.Page
    {
        List<string> Usuarios;
        string senhaPadrao = "curso";
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuarios = new List<string>();
            Usuarios.Add("cinthiabs");
            Usuarios.Add("Aluno");
            Usuarios.Add("Aula");


           
            if (Request.Cookies["login"] != null)
            {
                txtLogin.Text = Request.Cookies["login"].Value;
                txtSenha.Text = Request.Cookies["senha"].Value;
                btExecutar_Click(sender, e);
            }
        }

        protected void btExecutar_Click(object sender, EventArgs e)
        {
            Boolean flag = false;
            foreach(var item in Usuarios)
            {
                if(item == txtLogin.Text && senhaPadrao == txtSenha.Text )
                {
                    //trabalhando com cookies
                    //salvar login e senha no navegador
                    HttpCookie login = new HttpCookie("login", txtLogin.Text);
                    Response.Cookies.Add(login);

                    HttpCookie senha = new HttpCookie("senha", txtSenha.Text);
                    Response.Cookies.Add(senha);


                    Response.Redirect("~/Principal.aspx");
                }
                
            }
        }
    }
}