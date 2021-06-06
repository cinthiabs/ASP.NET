using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UploadArquivos
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtEnviar_Click(object sender, EventArgs e)
        {
            String nome = Arquivo.FileName; // pega o nome do arquivo
            String caminho = Server.MapPath(@"upload\"); // trabalhando com servidor
         
            string caminhoOrigem = @"C:\Users\DELL-EDESOFT\Desktop\Estudos\Curso Udemy ASP.NET\UploadArquivos\UploadArquivos\upload\"; // diretorio de origem desse computador
            txbNome.Text = nome;
            txbTamanho.Text = Arquivo.PostedFile.ContentLength.ToString(); //tamanho do arquivo

            Arquivo.PostedFile.SaveAs(caminho + nome); // salvar os arquivos na pasta upload
        }
    }
}