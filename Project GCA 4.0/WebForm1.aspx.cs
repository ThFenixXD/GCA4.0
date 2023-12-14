using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_GCA_4._0
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtenha o valor da sua variável (substitua 'suaVariavel' pelo valor real)
                string suaVariavel = ObtenhaSuaVariavel();

                // Execute uma consulta dinâmica usando sua variável
                DataTable dataTable = ObtenhaDadosDinamicos(suaVariavel);

                // Popule a DropDownList com os resultados da consulta
                ddlOpcoes.DataSource = dataTable;
                ddlOpcoes.DataTextField = "Nome"; // Substitua "Nome" pelo nome da coluna que deseja exibir
                ddlOpcoes.DataValueField = "ID";  // Substitua "ID" pelo nome da coluna que deseja usar como valor
                ddlOpcoes.DataBind();

                // Adicione um item inicial à DropDownList
                ddlOpcoes.Items.Insert(0, new ListItem("Selecione", "0"));
            }
        }

        private string ObtenhaSuaVariavel()
        {
            // Lógica para obter o valor da sua variável
            return "ValorDinamico"; // Substitua por sua lógica real
        }

        private DataTable ObtenhaDadosDinamicos(string suaVariavel)
        {
            // Lógica para obter dados dinâmicos com base na variável
            // Substitua pelo seu código real para obter dados do banco de dados ou de outra fonte
            DataTable dataTable = new DataTable();
            // Popule o dataTable com os dados necessários
            // Exemplo: dataTable = SeuMetodoParaObterDados(suaVariavel);
            return dataTable;
        }

    }
}