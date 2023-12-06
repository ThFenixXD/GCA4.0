using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectGCA4._0.Útil;

namespace Project_GCA_4._0.WebForms
{
    public partial class TipoLicenca : System.Web.UI.Page
    {
        protected void EscondePaineis()
        {
            PnlCadastroTipoLicenca.Visible = false;
        }

        protected void LimpaCampos()
        {
            txtTipoLicenca.Text =
            HdfID.Value =
            string.Empty;
        }

        protected void AtualizaGridLicenca()
        {
            GridLicencas.DataSource = Framework.GetDataTable("select ID_TipoDeLicenca, TipoDeLicenca from tb_TipoLicenca where deleted = 0 order by tipodelicenca");
            GridLicencas.DataBind();
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
                        PnlCadastroTipoLicenca.Visible = true;
                        AtualizaGridLicenca();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
        }

        protected void BtCancelarTipoLicenca_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroTipoLicenca.Visible = true;
            LimpaCampos();
        }

        protected void GridLicencas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridLicencas.DataSource = Framework.GetDataTable("select ID_TipoDeLicenca, TipoDeLicenca from tb_TipoLicenca where deleted = 0 order by tipodelicenca");
        }

        protected void GridLicencas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID_TipoDeLicenca"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        LimpaCampos();
                        PnlConsultarLicencas.Visible = true;
                        break;

                    case "opExcluir":
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_TipoLicenca TipoLicenca = new tb_TipoLicenca();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objTipoLicenca in ctx.tb_TipoLicenca where objTipoLicenca.ID_TipoDeLicenca == ID select objTipoLicenca).FirstOrDefault();

                            Query.Deleted = 1;
                            ctx.SaveChanges();
                            AtualizaGridLicenca();
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

        }

        protected void btCadastrarLicença_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroTipoLicenca.Visible = true;
        }
    }
}