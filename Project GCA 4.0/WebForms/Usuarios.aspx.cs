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
            GridUsuarios.DataSource = Framework.GetDataTable("SELECT tb_usuarios.id_usuario, tb_usuarios.nomeUsuario, tb_usuarios.funcaoUsuario, tb_setores.id_setor, tb_setores.nomeSetor FROM tb_Usuarios INNER JOIN tb_setores on tb_usuarios.id_setor = tb_setores.id_setor WHERE tb_usuarios.deleted = 0 Order By NomeUsuario");
            GridUsuarios.DataBind();
        }

        protected void PopulaCamposCadastroUsuario(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_usuarios Usuario = new tb_usuarios();

                var Query = (from objUsuario in ctx.tb_usuarios where objUsuario.id_usuario == ID select objUsuario).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtNomeUsuario.Text = Query.nomeUsuario;
                    txtFuncaoUsuario.Text = Query.funcaoUsuario;
                    ddlSetorUsuario.SelectedValue = Query.id_setor.ToString();
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
                tb_usuarios Usuario = new tb_usuarios();
                tb_usuarios Usuario2 = new tb_usuarios();
                try
                {
                    string _nomeusuario = txtNomeUsuario.Text.Trim();
                    int _setor = Convert.ToInt32(ddlSetorUsuario.SelectedValue);
                    var strsql = (from objUsuario in ctx.tb_usuarios
                                  where objUsuario.nomeUsuario == _nomeusuario && 
                                  objUsuario.deleted == 0
                                  select objUsuario);

                    Usuario2 = strsql.FirstOrDefault();

                    //if (strsql.Count() > 0)
                    if (strsql.Any())
                    {
                        //Response.Write("Esse Usuário já foi registrado");
                        Framework.Alerta(this, "Registro já consta no Sistema!");
                    }
                    else
                    {
                        //if (!string.IsNullOrEmpty(HdfID.Value))
                        //{
                        //    int _id = Convert.ToInt32(HdfID.Value);
                        //    var Query = (from objUsuario in ctx.tb_usuarios select objUsuario);
                        //    Usuario = Query.FirstOrDefault();
                        //}

                        if (!string.IsNullOrEmpty(HdfID.Value))
                        {
                            int _id = Convert.ToInt32(HdfID.Value);
                            Usuario = ctx.tb_usuarios.FirstOrDefault(objUsuario => objUsuario.id_usuario == _id);
                        }

                        Usuario.nomeUsuario = txtNomeUsuario.Text;
                        Usuario.funcaoUsuario = txtFuncaoUsuario.Text;
                        Usuario.id_setor = Convert.ToInt32(ddlSetorUsuario.SelectedValue);
                        Usuario.deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_usuarios.Add(Usuario);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlConsultarUsuarios.Visible = true;
                        AtualizaGridUsuarios();
                        Framework.Alerta(this, "Salvo com Sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    //Response.Write("Erro, " + ex.Message);
                    Framework.AlertaErro(this, ex);
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
            GridUsuarios.DataSource = Framework.GetDataTable("SELECT tb_usuarios.id_usuario, tb_usuarios.nomeUsuario, tb_usuarios.funcaoUsuario, tb_setores.id_setor, tb_setores.nomeSetor FROM tb_Usuarios INNER JOIN tb_setores on tb_usuarios.id_setor = tb_setores.id_setor WHERE tb_usuarios.deleted = 0 Order By NomeUsuario");
        }

        protected void GridUsuarios_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["id_usuario"]);

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
                            tb_usuarios Usuario = new tb_usuarios();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objUsuario in ctx.tb_usuarios where objUsuario.id_usuario == ID select objUsuario).FirstOrDefault();

                            Query.deleted = 1;
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