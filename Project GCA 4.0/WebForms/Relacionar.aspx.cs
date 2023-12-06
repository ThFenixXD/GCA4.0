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
            GridRelacionar.DataSource = Framework.GetDataTable("SELECT ID_Relacionar, UsuarioRelacionar, MaquinaRelacionar, ChaveAtivacaoRelacionar FROM tb_Relacionar WHERE Deleted = 0");
            GridRelacionar.DataBind();
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

        protected void PopulaDdlRelacionarUsuario()
        {
            DdlRelacionarUsuario.DataSource = Framework.GetDataTable("SELECT ID_Usuario, NomeUsuario FROM tb_Usuarios  WHERE Deleted = 0 Order By NomeUsuario");
            DdlRelacionarUsuario.DataBind();
            DdlRelacionarUsuario.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopulaDdlRelacionarMaquina()
        {
            DdlRelacionarMaquina.DataSource = Framework.GetDataTable("SELECT ID_Maquina, NomeMaquina FROM tb_Maquinas WHERE Deleted = 0 Order By NomeMaquina");
            DdlRelacionarMaquina.DataBind();
            DdlRelacionarMaquina.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopulaDdlRelacionarSoftware()
        {
            DdlRelacionarSoftware.DataSource = Framework.GetDataTable("SELECT ID_Software, NomeSoftware FROM tb_Software WHERE Deleted = 0 Order by NomeSoftware");
            DdlRelacionarSoftware.DataBind();
            DdlRelacionarSoftware.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopulaDdlRelacionarChaveAtivacao()
        {
            DdlRelacionarChaveAtivacao.DataSource = Framework.GetDataTable("SELECT ID_ChaveAtivacao, ChaveDeAtivacao FROM tb_Chaves WHERE Deleted = 0 Order By ChaveDeAtivacao");
            DdlRelacionarChaveAtivacao.DataBind();
            DdlRelacionarChaveAtivacao.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void SalvarRelacionar_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_Relacionar Relacao = new tb_Relacionar();
                try
                {
                    if (!string.IsNullOrEmpty(HdfID.Value))
                    {
                        int _id = Convert.ToInt32(HdfID.Value);

                        var Query = (from objRelacao in ctx.tb_Relacionar select objRelacao);

                        Relacao = Query.FirstOrDefault();
                    }
                    else
                    {
                        Relacao.UsuarioRelacionar = DdlRelacionarUsuario.SelectedItem.ToString();
                        Relacao.ID_Usuario = Convert.ToInt32(DdlRelacionarUsuario.SelectedValue);
                        Relacao.MaquinaRelacionar = DdlRelacionarMaquina.SelectedItem.ToString();
                        Relacao.ID_Maquina = Convert.ToInt32(DdlRelacionarMaquina.SelectedValue);
                        Relacao.SoftwareRelacionar = DdlRelacionarSoftware.SelectedItem.ToString();
                        Relacao.ChaveAtivacaoRelacionar = DdlRelacionarChaveAtivacao.SelectedItem.ToString();
                        Relacao.ID_ChaveAtivacao = Convert.ToInt32(DdlRelacionarChaveAtivacao.SelectedValue);
                        Relacao.Deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_Relacionar.Add(Relacao);
                        }
                        ctx.SaveChanges();
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

        protected void CancelarRelacionar_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarRelacionar.Visible = true;
        }

        protected void GridRelacionar_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridRelacionar.DataSource = Framework.GetDataTable("SELECT ID_Relacionar, UsuarioRelacionar, MaquinaRelacionar, ChaveAtivacaoRelacionar FROM tb_Relacionar WHERE Deleted = 0");
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