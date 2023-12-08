using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectGCA4._0.Útil;

namespace Project_GCA_4._0.WebForms
{
    public partial class Relacionar : System.Web.UI.Page
    {
        protected void EscondePaineis()
        {
            PnlRelacionar.Visible =
            PnlConsultarRelacionar.Visible = false;
        }

        protected void AtualizaGridRelacionar()
        {
            GridRelacionar.DataSource = Framework.GetDataTable("SELECT RE.id_relacionar, US.id_usuario, US.nomeUsuario, MAQ.id_maquina, MAQ.nomeMaquina, SO.id_software, SO.nomeSoftware, CH.id_chave, CH.chave FROM tb_Relacionar RE INNER JOIN tb_usuarios US ON RE.id_usuario = US.id_usuario INNER JOIN tb_maquinas MAQ ON RE.id_maquina = MAQ.id_maquina INNER JOIN tb_software SO ON RE.id_software = SO.id_software INNER JOIN tb_chaves CH ON RE.id_chave = CH.id_chave WHERE RE.deleted = 0 ORDER BY RE.id_relacionar");
            GridRelacionar.DataBind();
        }

        protected void PopulaCamposRelacionar(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_relacionar Chave = new tb_relacionar();

                var Query = (from objRelacionar in ctx.tb_relacionar where objRelacionar.id_relacionar == ID select objRelacionar).FirstOrDefault();


                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    DdlRelacionarUsuario.SelectedValue = Query.id_usuario.ToString();
                    DdlRelacionarMaquina.SelectedValue = Query.id_maquina.ToString();
                    DdlRelacionarSoftware.SelectedValue = Query.id_software.ToString();
                    DdlRelacionarChaveAtivacao.SelectedValue = Query.id_chave.ToString();
                }
            }
        }

        protected void PopulaDdlRelacionarUsuario()
        {
            DdlRelacionarUsuario.DataSource = Framework.GetDataTable("SELECT id_usuario, nomeUsuario FROM tb_usuarios WHERE deleted = 0 ORDER BY nomeUsuario");
            DdlRelacionarUsuario.DataBind();
            DdlRelacionarUsuario.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopulaDdlRelacionarMaquina()
        {
            DdlRelacionarMaquina.DataSource = Framework.GetDataTable("SELECT id_maquina, nomeMaquina FROM tb_maquinas WHERE deleted = 0 ORDER BY nomeMaquina");
            DdlRelacionarMaquina.DataBind();
            DdlRelacionarMaquina.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopulaDdlRelacionarSoftware()
        {
            DdlRelacionarSoftware.DataSource = Framework.GetDataTable("SELECT id_software, nomeSoftware FROM tb_software WHERE deleted = 0 ORDER BY nomeSoftware");
            DdlRelacionarSoftware.DataBind();
            DdlRelacionarSoftware.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopulaDdlRelacionarChaveAtivacao()
        {
            DdlRelacionarChaveAtivacao.DataSource = Framework.GetDataTable("SELECT id_chave, chave FROM tb_chaves WHERE deleted = 0 ORDER BY chave");
            DdlRelacionarChaveAtivacao.DataBind();
            DdlRelacionarChaveAtivacao.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void SalvarRelacionar_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_relacionar Relacao = new tb_relacionar();
                tb_relacionar Relacao2 = new tb_relacionar();
                try
                {
                    int _usuariorelacionarID = Convert.ToInt32(DdlRelacionarUsuario.SelectedValue);
                    int _maquinarelacionarID = Convert.ToInt32(DdlRelacionarMaquina.SelectedValue);
                    int _softwarerelacionarID = Convert.ToInt32(DdlRelacionarSoftware.SelectedValue);
                    int _chavedeativacaoID = Convert.ToInt32(DdlRelacionarChaveAtivacao.SelectedValue);


                    var strsql = (from objRelacao in ctx.tb_relacionar
                                  where
                                  objRelacao.id_usuario == _usuariorelacionarID &&
                                  objRelacao.id_maquina == _maquinarelacionarID &&
                                  objRelacao.id_software == _softwarerelacionarID &&
                                  objRelacao.id_chave == _chavedeativacaoID &&
                                  objRelacao.deleted == 0
                                  select objRelacao);

                    Relacao2 = strsql.FirstOrDefault();

                    //if (strsql.Count() > 0)
                    if (strsql.Any())
                    {
                        // ja existe software cadastrado
                        Response.Write("Essa Máquina já foi registrada");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(HdfID.Value))
                        {
                            int _id = Convert.ToInt32(HdfID.Value);

                            var Query = (from objRelacao in ctx.tb_relacionar select objRelacao);

                            Relacao = Query.FirstOrDefault();
                        }
                        Relacao.id_usuario = Convert.ToInt32(DdlRelacionarUsuario.SelectedValue);
                        Relacao.id_maquina = Convert.ToInt32(DdlRelacionarMaquina.SelectedValue);
                        Relacao.id_chave = Convert.ToInt32(DdlRelacionarChaveAtivacao.SelectedValue);
                        Relacao.id_software = Convert.ToInt32(DdlRelacionarSoftware.SelectedValue);
                        Relacao.deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_relacionar.Add(Relacao);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
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
                tb_maquinas Maquina = new tb_maquinas();
                try
                {
                    if (!string.IsNullOrEmpty(HdfID.Value))
                    {
                        int _id = Convert.ToInt32(HdfID.Value);
                        var Query = (from objMaquina in ctx.tb_maquinas select objMaquina);
                        Maquina = Query.FirstOrDefault();

                    }
                    Maquina.status = 1;

                    if (string.IsNullOrEmpty(HdfID.Value))
                    {
                        ctx.tb_maquinas.Add(Maquina);
                    }
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }

            using (GCAEntities ctx = new GCAEntities())
            {
                tb_chaves Chave = new tb_chaves();
                try
                {
                    if (!string.IsNullOrEmpty(HdfID.Value))
                    {
                        int _id = Convert.ToInt32(HdfID.Value);
                        var Query2 = (from objChave in ctx.tb_chaves select objChave);
                        Chave = Query2.FirstOrDefault();
                    }
                    var Query = (from objChaveAtivacao in ctx.tb_chaves select objChaveAtivacao).FirstOrDefault();

                    Chave.status = 1;

                    if (string.IsNullOrEmpty(HdfID.Value))
                    {
                        ctx.tb_chaves.Add(Chave);
                    }
                    ctx.SaveChanges();
                }
                catch (Exception ex)
            {
                Response.Write("Erro, " + ex.Message);
            }
        }
    }

    protected void CancelarRelacionar_Click(object sender, EventArgs e)
    {
        EscondePaineis();
        PnlConsultarRelacionar.Visible = true;
    }

    protected void GridRelacionar_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        GridRelacionar.DataSource = Framework.GetDataTable("SELECT RE.id_relacionar, US.id_usuario, US.nomeUsuario, MAQ.id_maquina, MAQ.nomeMaquina, SO.id_software, SO.nomeSoftware, CH.id_chave, CH.chave " +
                                                           "FROM tb_Relacionar RE " +
                                                           "INNER JOIN tb_usuarios US " +
                                                           "ON RE.id_usuario = US.id_usuario " +
                                                           "INNER JOIN tb_maquinas MAQ " +
                                                           "ON RE.id_maquina = MAQ.id_maquina " +
                                                           "INNER JOIN tb_software SO " +
                                                           "ON RE.id_software = SO.id_software " +
                                                           "INNER JOIN tb_chaves CH " +
                                                           "ON RE.id_chave = CH.id_chave " +
                                                           "WHERE RE.deleted = 0 " +
                                                           "ORDER BY RE.id_relacionar");
    }

    protected void GridRelacionar_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["id_relacionar"]);

            switch (e.CommandName)
            {
                case "opSelecionar":
                    break;

                case "opEditar":
                    EscondePaineis();
                    PopulaCamposRelacionar(_cdID);
                    PnlRelacionar.Visible = true;
                    break;

                case "opExcluir":
                    using (GCAEntities ctx = new GCAEntities())
                    {
                        tb_relacionar Relacionar = new tb_relacionar();

                        int ID = _cdID;
                        HdfID.Value = _cdID.ToString();

                        var Query = (from objRelacionar in ctx.tb_relacionar where objRelacionar.id_relacionar == ID select objRelacionar).FirstOrDefault();

                        Query.deleted = 1;
                        ctx.SaveChanges();
                        AtualizaGridRelacionar();
                    }

                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_maquinas Maquina = new tb_maquinas();
                            try
                            {
                                if (!string.IsNullOrEmpty(HdfID.Value))
                                {
                                    int _id = Convert.ToInt32(HdfID.Value);
                                    var Query = (from objMaquina in ctx.tb_maquinas select objMaquina);
                                    Maquina = Query.FirstOrDefault();

                                }
                                Maquina.status = 0;

                                if (string.IsNullOrEmpty(HdfID.Value))
                                {
                                    ctx.tb_maquinas.Add(Maquina);
                                }
                                ctx.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Response.Write("Erro, " + ex.Message);
                            }
                        }

                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_chaves Chave = new tb_chaves();
                            try
                            {
                                if (!string.IsNullOrEmpty(HdfID.Value))
                                {
                                    int _id = Convert.ToInt32(HdfID.Value);
                                    var Query2 = (from objChave in ctx.tb_chaves select objChave);
                                    Chave = Query2.FirstOrDefault();
                                }
                                var Query = (from objChaveAtivacao in ctx.tb_chaves select objChaveAtivacao).FirstOrDefault();

                                Chave.status = 0;

                                if (string.IsNullOrEmpty(HdfID.Value))
                                {
                                    ctx.tb_chaves.Add(Chave);
                                }
                                ctx.SaveChanges();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulaDdlRelacionarUsuario();
            PopulaDdlRelacionarMaquina();
            PopulaDdlRelacionarSoftware();
            PopulaDdlRelacionarChaveAtivacao();
        }
    }

    protected void btCadastroRelacionar_Click(object sender, EventArgs e)
    {
        EscondePaineis();
        PnlRelacionar.Visible = true;
    }
}
}