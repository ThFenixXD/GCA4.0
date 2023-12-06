using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectGCA4._0.Útil;

namespace Project_GCA_4._0.WebForms
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void EscondePaineis()
        {
            PnlCadastroUsuario.Visible =
            PnlConsultarUsuarios.Visible = false;
        }

        protected void LimpaCampos()
        {
            txtNomeUsuario.Text =
            txtFuncaoUsuario.Text =
            HdfID.Value =
            string.Empty;
        }

        protected void AtualizaGridUsuarios()
        {
            GridUsuarios.DataSource = Framework.GetDataTable("SELECT ID_Usuario, ID_Usuario, NomeUsuario, FuncaoUsuario, ID_Setor, SetorUsuario FROM tb_Usuarios WHERE Deleted = 0 Order By NomeUsuario");
            GridUsuarios.DataBind();
        }

        protected void PopulaCamposCadastroUsuario(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_Usuarios Usuario = new tb_Usuarios();

                var Query = (from objUsuario in ctx.tb_Usuarios where objUsuario.ID_Usuario == ID select objUsuario).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtNomeUsuario.Text = Query.NomeUsuario;
                    txtFuncaoUsuario.Text = Query.FuncaoUsuario;
                    ddlSetorUsuario.SelectedValue = Query.ID_Setor.ToString();
                }
            }
        }

        protected void PopulaDdlSetorUsuario()
        {
            ddlSetorUsuario.DataSource = Framework.GetDataTable("SELECT ID_Setor, NomeSetor FROM tb_Setores WHERE Deleted = 0");
            ddlSetorUsuario.DataBind();
            ddlSetorUsuario.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void LnkConsultaUsuario_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarUsuarios.Visible = true;
            AtualizaGridUsuarios();
        }

        protected void BtSalvarUsuario_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_Usuarios Usuario = new tb_Usuarios();
                try
                {
                    if (!string.IsNullOrEmpty(HdfID.Value))
                    {
                        int _id = Convert.ToInt32(HdfID.Value);

                        var Query = (from objUsuario in ctx.tb_Usuarios select objUsuario);

                        Usuario = Query.FirstOrDefault();
                    }
                    else
                    {
                        Usuario.NomeUsuario = txtNomeUsuario.Text;
                        Usuario.FuncaoUsuario = txtFuncaoUsuario.Text;
                        Usuario.SetorUsuario = ddlSetorUsuario.SelectedItem.ToString();
                        Usuario.ID_Setor = Convert.ToInt32(ddlSetorUsuario.SelectedValue);
                        Usuario.Deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_Usuarios.Add(Usuario);
                        }
                    }
                    ctx.SaveChanges();
                    EscondePaineis();
                    LimpaCampos();
                    PnlConsultarUsuarios.Visible = true;
                    AtualizaGridUsuarios();
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
        }

        protected void BtCancelarUsuario_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarUsuarios.Visible = true;
            LimpaCampos();
        }

        protected void GridUsuarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridUsuarios.DataSource = Framework.GetDataTable("SELECT ID_Usuario, ID_Usuario, NomeUsuario, FuncaoUsuario, ID_Setor, SetorUsuario FROM tb_Usuarios WHERE Deleted = 0");
        }

        protected void GridUsuarios_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID_Usuario"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        LimpaCampos();
                        PopulaCamposCadastroUsuario(_cdID);
                        PnlCadastroUsuario.Visible = true;
                        break;

                    case "opExcluir":
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_Usuarios Usuario = new tb_Usuarios();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objUsuario in ctx.tb_Usuarios where objUsuario.ID_Usuario == ID select objUsuario).FirstOrDefault();

                            Query.Deleted = 1;
                            ctx.SaveChanges();
                            AtualizaGridUsuarios();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Response.Write("Erro, " + ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulaDdlSetorUsuario();
            }
        }

        protected void btCadastroUsuarios_Click1(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroUsuario.Visible = true;
        }
    }
}