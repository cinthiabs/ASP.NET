using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Calendario
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Logar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string senha = txtSenha.Text;

            try
            {
                if (txtEmail.Text != "" && txtSenha.Text != "")
                {
                    //capturar a string de conexao
                    System.Configuration.Configuration root = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                    System.Configuration.ConnectionStringSettings connString;
                    connString = root.ConnectionStrings.ConnectionStrings["ConnectionString"];

                    SqlConnection conexao = new SqlConnection();
                    conexao.ConnectionString = connString.ToString();

                    SqlCommand comando = new SqlCommand();
                    comando.Connection = conexao;
                    comando.CommandText = "select * from usuario where email = @email and senha = @senha";


                    comando.Parameters.AddWithValue("@email", email);
                    comando.Parameters.AddWithValue("@senha", senha);

                    conexao.Open();

                    SqlDataReader registro = comando.ExecuteReader();
                    if (registro.HasRows)
                    {
                        HttpCookie login = new HttpCookie("login", txtEmail.Text);
                        Response.Cookies.Add(login);

                        Response.Redirect("~/Index.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('E - mail ou senha incorretos.')</script>");
                        
                    }
                }
            }
            catch (Exception erro)
            {
                Response.Write("<script> alert('" + erro.Message + "') </script>");
            }

            
        }
    }
}