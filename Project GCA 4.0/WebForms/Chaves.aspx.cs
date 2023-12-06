using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectGCA4._0.Útil;

namespace Project_GCA_4._0.WebForms
{
    public partial class Chaves : System.Web.UI.Page
    {
        protected void EscondePaineis()
        {
            PnlCadastroChaveAtivacao.Visible =
            PnlConsultarChaves.Visible = false;
        }

        protected void LimpaCampos()
        {
            txtPrazoLicenca.Text =
            HdfID.Value =
            string.Empty;
        }

        protected void AtualizaGridChaves()
        {
            GridChaves.DataSource = Framework.GetDataTable("select tb_Software.ID_Software, tb_Software.NomeSoftware, tb_Chaves.ID_ChaveAtivacao, tb_Chaves.DataDeCompra, tb_Chaves.TipoDeLicenca, tb_Chaves.PrazoDeLicenca, tb_Chaves.ChaveDeAtivacao, tb_Chaves.Status from tb_Software Inner Join tb_Chaves on tb_Software.ID_Software = tb_Chaves.ID_ChaveAtivacao where tb_chaves.deleted = 0 Order By = ChaveDeAtivacao");
            GridChaves.DataBind();
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

        protected void BtCancelarChaveAtivacao_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarChaves.Visible = true;
            LimpaCampos();
        }

        protected void GridChaves_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridChaves.DataSource = Framework.GetDataTable("SELECT ID_ChaveAtivacao, DataDeCompra, TipoDeLicenca, PrazoDeLicenca, Software, ChaveDeAtivacao, Status FROM tb_Chaves WHERE Deleted = 0");
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


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulaDdlTipoDeLicenca();
                PopulaDdlSoftware();
            }
        }
    }
}