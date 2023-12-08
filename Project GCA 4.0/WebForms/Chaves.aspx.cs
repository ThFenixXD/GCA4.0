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
            GridChaves.DataSource = Framework.GetDataTable("SELECT CH.id_chave , CH.chave, CH.dataDeCompra, TPL.id_tipoLicenca, TPL.tipoLicenca, TPL.prazoLicenca, SO.id_software, SO.nomeSoftware, CH.status FROM tb_chaves CH INNER JOIN tb_tipoLicenca TPL ON CH.id_tipoLicenca = TPL.id_tipoLicenca INNER JOIN tb_software SO ON CH.id_software = SO.id_software WHERE CH.deleted = 0 ORDER BY CH.chave");
            GridChaves.DataBind();
        }

        protected void PopulaCamposCadastroChave(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_chaves Chave = new tb_chaves();

                var Query = (from objChave in ctx.tb_chaves where objChave.id_chave == ID select objChave).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtDataDeCompra.Text = Query.dataDeCompra;
                }
            }
        }

        protected void PopulaDdlTipoDeLicenca()
        {
            DdlTipoDeLicenca.DataSource = Framework.GetDataTable("SELECT id_tipoLicenca, tipoLicenca FROM tb_tipoLicenca WHERE deleted = 0");
            DdlTipoDeLicenca.DataBind();
            DdlTipoDeLicenca.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopulaDdlSoftware()
        {
            DdlSoftware.DataSource = Framework.GetDataTable("SELECT id_software, nomeSoftware FROM tb_Software WHERE deleted = 0");
            DdlSoftware.DataBind();
            DdlSoftware.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void BtSalvarChaveAtivacao_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_chaves Chave = new tb_chaves();
                tb_chaves Chave2 = new tb_chaves();
                try
                {
                    string _chavedeativacao = txtChaveAtivacao.Text.Trim();
                    string _datadecompra = txtDataDeCompra.Text.Trim();
                    int _tipodelicenca = Convert.ToInt32(DdlTipoDeLicenca.SelectedValue);
                    int _software = Convert.ToInt32(DdlSoftware.SelectedValue);


                    var strsql = (from objChave in ctx.tb_chaves
                                  where objChave.chave == _chavedeativacao && objChave.dataDeCompra == _datadecompra && objChave.id_tipoLicenca == _tipodelicenca && objChave.deleted == 0
                                  select objChave);

                    Chave2 = strsql.FirstOrDefault();

                    //if (strsql.Count() > 0)
                    if (strsql.Any())
                    {
                        // ja existe software cadastrado
                        Response.Write("Essa Chave de Ativação já foi registrada");
                    }
                    else
                    {
                        //if (!string.IsNullOrEmpty(HdfID.Value))
                        //{
                        //    int _id = Convert.ToInt32(HdfID.Value);
                        //    var Query2 = (from objChave in ctx.tb_chaves select objChave);
                        //    Chave = Query2.FirstOrDefault();
                        //}
                        if (!string.IsNullOrEmpty(HdfID.Value))
                        {
                            int _id = Convert.ToInt32(HdfID.Value);

                            Chave = ctx.tb_chaves.FirstOrDefault(objChave => objChave.id_chave == _id);
                        }
                        var Query = (from objChaveAtivacao in ctx.tb_chaves select objChaveAtivacao).FirstOrDefault();

                        Chave.dataDeCompra = txtDataDeCompra.Text;
                        Chave.id_tipoLicenca = Convert.ToInt32(DdlTipoDeLicenca.SelectedValue);
                        Chave.prazoLicenca = txtPrazoLicenca.Text;
                        Chave.id_software = Convert.ToInt32(DdlSoftware.SelectedValue);
                        Chave.chave = txtChaveAtivacao.Text;
                        Chave.status = 0;
                        Chave.deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_chaves.Add(Chave);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlConsultarChaves.Visible = true;
                        AtualizaGridChaves();
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
            GridChaves.DataSource = Framework.GetDataTable("SELECT CH.id_chave , CH.chave, CH.dataDeCompra, TPL.id_tipoLicenca, TPL.tipoLicenca, TPL.prazoLicenca, SO.id_software, SO.nomeSoftware, CH.status FROM tb_chaves CH INNER JOIN tb_tipoLicenca TPL ON CH.id_tipoLicenca = TPL.id_tipoLicenca INNER JOIN tb_software SO ON CH.id_software = SO.id_software WHERE CH.deleted = 0 ORDER BY CH.chave");
            //GridChaves.DataSource = Framework.GetDataTable("select * from tb_chaves");
        }

        protected void GridChaves_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["id_chave"]);

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
                            tb_chaves Chave = new tb_chaves();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objChave in ctx.tb_chaves where objChave.id_chave == ID select objChave).FirstOrDefault();

                            Query.deleted = 1;
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

        protected void btCadastrarChave_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroChaveAtivacao.Visible = true;
        }
    }
}