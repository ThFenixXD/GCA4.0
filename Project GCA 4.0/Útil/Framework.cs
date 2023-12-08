using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectGCA4._0.Útil
{
    public class Framework
    {
        public static DataTable GetDataTable(string query)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conn);
            DataTable myDataTable = new DataTable();
            conn.Open();
            try
            {
                adapter.Fill(myDataTable);
            }
            finally
            {
                conn.Close();
            }
            return myDataTable;
        }

        public static void EscondePaineis(Control container)
        {
            if (container is Panel)
                container.Visible = false;

            foreach (Control ctrl in container.Controls)
                EscondePaineis(ctrl);
        }

        //public static void AlertaSucesso(Page Pagina)
        //{
        //    string script = "alert('Salvo com Sucesso!');";
        //    Pagina.ClientScript.RegisterStartupScript(Pagina.GetType(), "Alerta", script, true);
        //}

        public static void AlertaErro(Page Pagina, Exception ex)
        {
            string script = "alert('Erro: " + ex.Message + "');";
            Pagina.ClientScript.RegisterStartupScript(Pagina.GetType(), "Alerta", script, true);
        }

        //public static void AlertaRegistro(Page Pagina)
        //{
        //    string script = "alert('Registro já consta no sistema!');";
        //    Pagina.ClientScript.RegisterStartupScript(Pagina.GetType(), "Alerta", script, true);
        //}

        public static void Alerta(Page pagina, string mensagem)
        {
            string script = "alert('" + mensagem + "');";
            pagina.ClientScript.RegisterStartupScript(pagina.GetType(), "MensagemSucesso", script, true);
        }

    }
}