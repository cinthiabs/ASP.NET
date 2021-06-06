using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Calendario
{
    public partial class Contatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnCadastrar_Click(object sender, EventArgs e)
        {


                //capturar a string de conexao
                System.Configuration.Configuration root = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
                System.Configuration.ConnectionStringSettings connString;
                connString = root.ConnectionStrings.ConnectionStrings["ConnectionString"];

                SqlConnection conexao = new SqlConnection();
                conexao.ConnectionString = connString.ToString();

                SqlCommand comando = new SqlCommand();
                comando.Connection = conexao;
                comando.CommandText = "insert into contato (nome,email,telefone) values(@nome,@email,@telefone)";

                comando.Parameters.AddWithValue("nome", txtNome.Text);
                comando.Parameters.AddWithValue("email", txtEmail.Text);
                comando.Parameters.AddWithValue("telefone", txtTelefone.Text);

                conexao.Open();
                comando.ExecuteNonQuery();

                conexao.Close();

                GridView1.DataBind();
           
        }
    }
}