using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace cursoaula01
{
    public partial class Dados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtCadastrar_Click(object sender, EventArgs e)
        {
            //capturar a string de conexao
            System.Configuration.Configuration root = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
            System.Configuration.ConnectionStringSettings connString;
            connString = root.ConnectionStrings.ConnectionStrings["ConnectionString"];
            
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = connString.ToString();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "insert into contato (nome,email) values(@nome,@email)";
           
            comando.Parameters.AddWithValue("nome",txtNome.Text);
            comando.Parameters.AddWithValue("email", txtEmail.Text);
            
            conexao.Open();
            comando.ExecuteNonQuery();
            
            conexao.Close();
            DataList1.DataBind();
        }
    }
}