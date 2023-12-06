using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectGCA4._0.Útil;

namespace Project_GCA_4._0.WebForms
{
    public partial class Cadastro : System.Web.UI.Page
    {
        #region Métodos

        protected void EscondePaineis()
        {
            PnlCadastroOpcoes.Visible =
            PnlCadastroUsuario.Visible =
            PnlCadastroMaquina.Visible =
            PnlCadastroSetor.Visible =
            PnlCadastroSoftware.Visible =
            PnlCadastroChaveAtivacao.Visible =
            PnlCadastroTipoLicenca.Visible =
            PnlConsultar.Visible =
            PnlConsultarUsuarios.Visible =
            PnlConsultarMaquinas.Visible =
            PnlConsultarChaves.Visible =
            PnlConsultarSoftware.Visible =
            PnlGerenciamento.Visible = false;
        }

        protected void LimpaCampos()
        {
            txtNomeUsuario.Text =
            txtFuncaoUsuario.Text =
            txtNomeMaquina.Text =
            txtNomeSetor.Text =
            txtNomeSoftware.Text =
            txtFabricante.Text =
            txtTipoLicenca.Text =
            txtPrazoLicenca.Text =
            HdfID.Value =
            string.Empty;
        }

        #region AtualizaGrid

        protected void AtualizaGridUsuarios()
        {
            GridUsuarios.DataSource = Framework.GetDataTable("SELECT ID_Usuario, ID_Usuario, NomeUsuario, FuncaoUsuario, ID_Setor, SetorUsuario FROM tb_Usuarios WHERE Deleted = 0 Order By NomeUsuario");
            GridUsuarios.DataBind();
        }

        protected void AtualizaGridMaquinas()
        {
            GridMaquinas.DataSource = Framework.GetDataTable("SELECT ID_Maquina, ID_Maquina, NomeMaquina, ID_Setor, SetorMaquina, Status FROM tb_Maquinas WHERE Deleted = 0 Order By NomeMaquina");
            GridMaquinas.DataBind();
        }

        protected void AtualizaGridChaves()
        {
            GridChaves.DataSource = Framework.GetDataTable("select tb_Software.ID_Software, tb_Software.NomeSoftware, tb_Chaves.ID_ChaveAtivacao, tb_Chaves.DataDeCompra, tb_Chaves.TipoDeLicenca, tb_Chaves.PrazoDeLicenca, tb_Chaves.ChaveDeAtivacao, tb_Chaves.Status from tb_Software Inner Join tb_Chaves on tb_Software.ID_Software = tb_Chaves.ID_ChaveAtivacao where tb_chaves.deleted = 0 Order By = ChaveDeAtivacao");
            GridChaves.DataBind();
        }

        protected void AtualizaGridSoftware()
        {
            GridSoftware.DataSource = Framework.GetDataTable("SELECT ID_Software, NomeSoftware, Fabricante FROM tb_Software WHERE Deleted = 0 Order By NomeSoftware");
            GridSoftware.DataBind();
        }

        protected void AtualizaGridRelacionar()
        {
            GridRelacionar.DataSource = Framework.GetDataTable("SELECT ID_Relacionar, UsuarioRelacionar, MaquinaRelacionar, ChaveAtivacaoRelacionar FROM tb_Relacionar WHERE Deleted = 0");
            GridRelacionar.DataBind();
        }


        #endregion

        #region PopulaCampos

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

        protected void PopulaCamposCadastroMaquina(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_Maquinas Maquina = new tb_Maquinas();

                var Query = (from objMaquina in ctx.tb_Maquinas where objMaquina.ID_Maquina == ID select objMaquina).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtNomeMaquina.Text = Query.NomeMaquina;
                    DdlSetorMaquina.SelectedValue = Query.ID_Setor.ToString();
                }
            }
        }

        protected void PopulaCamposCadastroChave(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_Chaves Chave = new tb_Chaves();

                var Query = (from objChave in ctx.tb_Chaves where objChave.ID_ChaveAtivacao == ID select objChave).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtDataDeCompra.Text = Query.DataDeCompra;
                }
            }
        }

        protected void PopulaCamposCadastroSoftware(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_Software Software = new tb_Software();

                var Query = (from objSoftware in ctx.tb_Software where objSoftware.ID_Software == ID select objSoftware).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtNomeSoftware.Text = Query.NomeSoftware;
                    txtFabricante.Text = Query.Fabricante;
                }
            }
        }

        protected void PopulaCamposRelacionar(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_Relacionar Chave = new tb_Relacionar();

                var Query = (from objRelacionar in ctx.tb_Relacionar where objRelacionar.ID_Relacionar == ID select objRelacionar).FirstOrDefault();


                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    DdlRelacionarUsuario.SelectedValue = Query.ID_Usuario.ToString();
                    DdlRelacionarMaquina.SelectedValue = Query.ID_Maquina.ToString();
                    DdlRelacionarSoftware.SelectedValue = Query.ID_ChaveAtivacao.ToString();
                    DdlRelacionarChaveAtivacao.SelectedValue = Query.ID_ChaveAtivacao.ToString();
                }
            }
        }

        #endregion

        #region PopulaDdl

        protected void PopulaDdlRelacionarChaveAtivacao()
        {
            DdlRelacionarChaveAtivacao.DataSource = Framework.GetDataTable("SELECT ID_ChaveAtivacao, ChaveAtivacao FROM tb_Chaves WHERE Deleted = 0");
            DdlRelacionarChaveAtivacao.DataBind();
            DdlRelacionarChaveAtivacao.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopulaDdlRelacionarChaveAtivacao(int _ID_ChaveAtivacao)
        {
            DdlRelacionarChaveAtivacao.DataSource = Framework.GetDataTable("SELECT ID_ChaveAtivacao, ChaveAtivacao FROM tb_Chaves WHERE ID_ChaveAtivacao = " + _ID_ChaveAtivacao + " AND Deleted = 0");
            DdlRelacionarChaveAtivacao.DataBind();
            DdlRelacionarChaveAtivacao.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopulaDdlSetorUsuario()
        {
            ddlSetorUsuario.DataSource = Framework.GetDataTable("SELECT ID_Setor, NomeSetor FROM tb_Setores WHERE Deleted = 0");
            ddlSetorUsuario.DataBind();
            ddlSetorUsuario.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopulaDdlSetorMaquina()
        {
            DdlSetorMaquina.DataSource = Framework.GetDataTable("SELECT ID_Setor, NomeSetor FROM tb_Setores WHERE Deleted = 0");
            DdlSetorMaquina.DataBind();
            DdlSetorMaquina.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopulaDdlTipoDeLicenca()
        {
            DdlTipoDeLicenca.DataSource = Framework.GetDataTable("SELECT ID_TipoDeLicenca, TipoDeLicenca FROM tb_TipoLicenca WHERE Deleted = 0");
            DdlTipoDeLicenca.DataBind();
            DdlTipoDeLicenca.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopulaDdlSoftware()
        {
            DdlSoftware.DataSource = Framework.GetDataTable("SELECT ID_Software, NomeSoftware FROM tb_Software WHERE Deleted = 0");
            DdlSoftware.DataBind();
            DdlSoftware.Items.Insert(0, new ListItem("Selecionar"));
        }

        #endregion

        #endregion

        #region OnClick

        #region Cadastro

        protected void lnkCadastroUsuario_Click(object sender, EventArgs e)
        {
            
            PnlCadastroUsuario.Visible = true;
        }

        protected void lnkCadastroMaquina_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroMaquina.Visible = true;
        }

        protected void lnkCadastroSetor_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroSetor.Visible = true;
        }

        protected void lnkCadastroSoftware_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroSoftware.Visible = true;
        }

        protected void lnkCadastroChaves_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroChaveAtivacao.Visible = true;
        }

        protected void lnkCadastroTipoLicenca_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroTipoLicenca.Visible = true;
        }

        protected void lnkRelacionar_Click(object sender, EventArgs e)
        {

        }

        protected void lnkCadastro_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroOpcoes.Visible = true;
        }

        protected void lnkConsulta_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultar.Visible = true;
        }

        protected void LnkConsultaUsuario_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarUsuarios.Visible = true;
            AtualizaGridUsuarios();
        }

        protected void LnkConsultaMaquina_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarMaquinas.Visible = true;
            AtualizaGridMaquinas();
        }

        protected void LnkConsultaChaves_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarChaves.Visible = true;
            AtualizaGridChaves();
        }

        protected void lnkConsultaSoftware_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarSoftware.Visible = true;
            AtualizaGridSoftware();
        }

        protected void DdlRelacionarSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulaDdlRelacionarChaveAtivacao(Convert.ToInt32(DdlRelacionarSoftware.SelectedValue));
        }

        #endregion

        #region Salvar

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

        protected void BtSalvarMaquina_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_Maquinas Maquina = new tb_Maquinas();
                try
                {
                    if (!string.IsNullOrEmpty(HdfID.Value))
                    {
                        int _id = Convert.ToInt32(HdfID.Value);

                        var Query = (from objMaquina in ctx.tb_Maquinas select objMaquina);

                        Maquina = Query.FirstOrDefault();
                    }
                    else
                    {
                        Maquina.NomeMaquina = txtNomeMaquina.Text;
                        Maquina.ID_Setor = Convert.ToInt32(DdlSetorMaquina.SelectedValue);
                        Maquina.SetorMaquina = DdlSetorMaquina.SelectedItem.ToString();
                        Maquina.Status = "INATIVA";
                        Maquina.Deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_Maquinas.Add(Maquina);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlConsultarMaquinas.Visible = true;
                        AtualizaGridMaquinas();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
        }

        protected void BtSalvarSetor_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_Setores Setor = new tb_Setores();
                try
                {
                    if (!string.IsNullOrEmpty(HdfID.Value))
                    {
                        int _id = Convert.ToInt32(HdfID.Value);

                        var Query = (from objSetor in ctx.tb_Setores select objSetor);

                        Setor = Query.FirstOrDefault();
                    }
                    else
                    {
                        Setor.NomeSetor = txtNomeSetor.Text;
                        Setor.Deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_Setores.Add(Setor);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlCadastroOpcoes.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
        }

        protected void BtSalvarSoftware_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_Software Software = new tb_Software();
                try
                {
                    if (!string.IsNullOrEmpty(HdfID.Value))
                    {
                        int _id = Convert.ToInt32(HdfID.Value);

                        var Query = (from objSoftware in ctx.tb_Software select objSoftware);

                        Software = Query.FirstOrDefault();
                    }
                    else
                    {
                        Software.NomeSoftware = txtNomeSoftware.Text;
                        Software.Fabricante = txtFabricante.Text;
                        Software.Deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_Software.Add(Software);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlConsultarSoftware.Visible = true;
                        AtualizaGridSoftware();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
        }

        protected void BtSalvarChaveAtivacao_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_Chaves Chave = new tb_Chaves();
                try
                {
                    if (!string.IsNullOrEmpty(HdfID.Value))
                    {
                        int _id = Convert.ToInt32(HdfID.Value);

                        var Query = (from objChave in ctx.tb_Chaves select objChave);

                        Chave = Query.FirstOrDefault();
                    }
                    else
                    {
                        var Query = (from objChaveAtivacao in ctx.tb_Chaves select objChaveAtivacao).FirstOrDefault();

                        string valor = txtChaveAtivacao.Text;

                        if (Query.ChaveDeAtivacao != valor)
                        {
                            Chave.DataDeCompra = txtDataDeCompra.Text;
                            Chave.TipoDeLicenca = DdlTipoDeLicenca.SelectedItem.Text;
                            Chave.PrazoDeLicenca = txtPrazoLicenca.Text;
                            Chave.ChaveDeAtivacao = txtChaveAtivacao.Text;
                            Chave.Status = 0;
                            Chave.Deleted = 0;

                            if (string.IsNullOrEmpty(HdfID.Value))
                            {
                                ctx.tb_Chaves.Add(Chave);
                            }
                            ctx.SaveChanges();
                            EscondePaineis();
                            LimpaCampos();
                            PnlConsultarChaves.Visible = true;
                            AtualizaGridChaves();
                        }
                    }

                }




                        
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
        }

        protected void BtSalvarTipoLicenca_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_TipoLicenca TipoLicenca = new tb_TipoLicenca();
                try
                {
                    if (!string.IsNullOrEmpty(HdfID.Value))
                    {
                        int _id = Convert.ToInt32(HdfID.Value);

                        var Query = (from objTipoLicenca in ctx.tb_TipoLicenca select objTipoLicenca);

                        TipoLicenca = Query.FirstOrDefault();
                    }
                    else
                    {
                        TipoLicenca.TipoDeLicenca = txtTipoLicenca.Text;
                        TipoLicenca.Deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_TipoLicenca.Add(TipoLicenca);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlCadastroOpcoes.Visible = true;
                        PopulaDdlTipoDeLicenca();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
        }

        protected void SalvarRelacionar_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_Relacionar Usuario = new tb_Relacionar();
                try
                {
                    if (!string.IsNullOrEmpty(HdfID.Value))
                    {
                        int _id = Convert.ToInt32(HdfID.Value);

                        var Query = (from objUsuario in ctx.tb_Relacionar select objUsuario);

                        Usuario = Query.FirstOrDefault();
                    }
                    else
                    {
                        Usuario.UsuarioRelacionar = DdlRelacionarUsuario.SelectedItem.ToString();
                        Usuario.ID_Usuario = Convert.ToInt32(DdlRelacionarUsuario.SelectedValue);
                        Usuario.MaquinaRelacionar = DdlRelacionarMaquina.SelectedItem.ToString();
                        Usuario.ID_Maquina = Convert.ToInt32(DdlRelacionarMaquina.SelectedValue);
                        Usuario.SoftwareRelacionar = DdlRelacionarSoftware.SelectedItem.ToString();
                        Usuario.ChaveAtivacaoRelacionar = DdlRelacionarChaveAtivacao.SelectedItem.ToString();
                        Usuario.ID_ChaveAtivacao = Convert.ToInt32(DdlRelacionarChaveAtivacao.SelectedValue);
                        Usuario.Deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_Relacionar.Add(Usuario);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlConsultarRelacionar.Visible = true;
                        AtualizaGridRelacionar();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_Maquinas Maquina = new tb_Maquinas();
                try
                {
                    if (!string.IsNullOrEmpty(HdfID.Value))
                    {
                        int _id = Convert.ToInt32(HdfID.Value);

                        var Query = (from objMaquina in ctx.tb_Maquinas select objMaquina);

                        Maquina = Query.FirstOrDefault();
                    }
                    else
                    {
                        Maquina.Status = "ATIVA";

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_Maquinas.Add(Maquina);
                        }
                        ctx.SaveChanges();
                        AtualizaGridRelacionar();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_Chaves Chave = new tb_Chaves();
                try
                {
                    if (!string.IsNullOrEmpty(HdfID.Value))
                    {
                        int _id = Convert.ToInt32(HdfID.Value);

                        var Query = (from objChave in ctx.tb_Chaves select objChave);

                        Chave = Query.FirstOrDefault();
                    }
                    else
                    {
                        Chave.Status = 1;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_Chaves.Add(Chave);
                        }
                        ctx.SaveChanges();
                        AtualizaGridRelacionar();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
        }

        #endregion

        #region Cancelar

        protected void BtCancelarUsuario_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroOpcoes.Visible = true;
            LimpaCampos();
        }

        protected void BtCancelarMaquina_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroOpcoes.Visible = true;
            LimpaCampos();
        }

        protected void BtCancelarSetor_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroOpcoes.Visible = true;
            LimpaCampos();
        }

        protected void BtCancelarSoftware_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroOpcoes.Visible = true;
            LimpaCampos();
        }

        protected void BtCancelarChaveAtivacao_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroOpcoes.Visible = true;
            LimpaCampos();
        }

        protected void BtCancelarTipoLicenca_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroOpcoes.Visible = true;
            LimpaCampos();
        }

        protected void CancelarRelacionar_Click(object sender, EventArgs e)
        {
            PnlRelacionar.Visible = true;
            LimpaCampos();
        }

        #endregion

        #endregion

        #region OnNeedDataSource
        protected void GridUsuarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridUsuarios.DataSource = Framework.GetDataTable("SELECT ID_Usuario, ID_Usuario, NomeUsuario, FuncaoUsuario, ID_Setor, SetorUsuario FROM tb_Usuarios WHERE Deleted = 0");
        }

        protected void GridMaquinas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridMaquinas.DataSource = Framework.GetDataTable("SELECT ID_Maquina, ID_Maquina, NomeMaquina, ID_Setor, SetorMaquina, Status FROM tb_Maquinas WHERE Deleted = 0");
        }

        protected void GridChaves_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridChaves.DataSource = Framework.GetDataTable("SELECT ID_ChaveAtivacao, DataDeCompra, TipoDeLicenca, PrazoDeLicenca, Software, ChaveDeAtivacao, Status FROM tb_Chaves WHERE Deleted = 0");
        }

        protected void GridSoftware_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridSoftware.DataSource = Framework.GetDataTable("select tb_Software.ID_Software, tb_Software.NomeSoftware, tb_Chaves.ID_ChaveAtivacao, tb_Chaves.DataDeCompra, tb_Chaves.TipoDeLicenca, tb_Chaves.PrazoDeLicenca, tb_Chaves.ChaveDeAtivacao, tb_ChaveDeAtivacao.Status from tb_Software Inner Join tb_Chaves on tb_Software.ID_Software = tb_Chaves.ID_ChaveAtivacao");
        }

        protected void GridRelacionar_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridRelacionar.DataSource = Framework.GetDataTable("SELECT ID_Relacionar, UsuarioRelacionar, MaquinaRelacionar, ChaveAtivacaoRelacionar FROM tb_Relacionar WHERE Deleted = 0");
            GridRelacionar.DataBind();
        }

        #endregion

        #region OnItemCommand

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

        protected void GridMaquinas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID_Maquina"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        LimpaCampos();
                        PopulaCamposCadastroMaquina(_cdID);
                        PnlCadastroMaquina.Visible = true;
                        break;

                    case "opExcluir":
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_Maquinas Maquina = new tb_Maquinas();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objMaquina in ctx.tb_Maquinas where objMaquina.ID_Maquina == ID select objMaquina).FirstOrDefault();

                            Query.Deleted = 1;
                            ctx.SaveChanges();
                            AtualizaGridMaquinas();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Response.Write("Erro, " + ex.Message);
            }
        }

        protected void GridChaves_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID_ChaveAtivacao"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        LimpaCampos();
                        PopulaCamposCadastroChave(_cdID);
                        PnlCadastroChaveAtivacao.Visible = true;
                        break;

                    case "opExcluir":
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_Chaves Chave = new tb_Chaves();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objChave in ctx.tb_Chaves where objChave.ID_ChaveAtivacao == ID select objChave).FirstOrDefault();

                            Query.Deleted = 1;
                            ctx.SaveChanges();
                            AtualizaGridChaves();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Response.Write("Erro, " + ex.Message);
            }
        }

        protected void GridSoftware_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID_Software"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        LimpaCampos();
                        PopulaCamposCadastroSoftware(_cdID);
                        PnlConsultarSoftware.Visible = true;
                        break;

                    case "opExcluir":
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_Software Software = new tb_Software();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objSoftware in ctx.tb_Software where objSoftware.ID_Software == ID select objSoftware).FirstOrDefault();

                            Query.Deleted = 1;
                            ctx.SaveChanges();
                            AtualizaGridSoftware();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Response.Write("Erro, " + ex.Message);
            }
        }

        protected void GridRelacionar_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID_Relacionar"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        LimpaCampos();
                        PopulaCamposRelacionar(_cdID);
                        PnlRelacionar.Visible = true;
                        break;

                    case "opExcluir":
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_Relacionar Relacionar = new tb_Relacionar();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objRelacionar in ctx.tb_Relacionar where objRelacionar.ID_Relacionar == ID select objRelacionar).FirstOrDefault();

                            Query.Deleted = 1;
                            ctx.SaveChanges();
                            AtualizaGridRelacionar();
                        }
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_Chaves Chave = new tb_Chaves();
                            try
                            {
                                if (!string.IsNullOrEmpty(HdfID.Value))
                                {
                                    int _id = Convert.ToInt32(HdfID.Value);

                                    var Query = (from objChave in ctx.tb_Chaves select objChave);

                                    Chave = Query.FirstOrDefault();
                                }
                                else
                                {
                                    Chave.Status = 0;

                                    if (string.IsNullOrEmpty(HdfID.Value))
                                    {
                                        ctx.tb_Chaves.Add(Chave);
                                    }
                                    ctx.SaveChanges();
                                    AtualizaGridRelacionar();
                                }
                            }
                            catch (Exception ex)
                            {
                                Response.Write("Erro, " + ex.Message);
                            }
                        }
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_Maquinas Maquina = new tb_Maquinas();
                            try
                            {
                                if (!string.IsNullOrEmpty(HdfID.Value))
                                {
                                    int _id = Convert.ToInt32(HdfID.Value);

                                    var Query = (from objMaquina in ctx.tb_Maquinas select objMaquina);

                                    Maquina = Query.FirstOrDefault();
                                }
                                else
                                {
                                    Maquina.Status = "1";

                                    if (string.IsNullOrEmpty(HdfID.Value))
                                    {
                                        ctx.tb_Maquinas.Add(Maquina);
                                    }
                                    ctx.SaveChanges();
                                    AtualizaGridRelacionar();
                                }
                            }
                            catch (Exception ex)
                            {
                                Response.Write("Erro, " + ex.Message);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Response.Write("Erro, " + ex.Message);
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulaDdlSetorUsuario();
                PopulaDdlSetorMaquina();
                PopulaDdlTipoDeLicenca();
                PopulaDdlSoftware();
            }
        }
        #endregion


    }
}