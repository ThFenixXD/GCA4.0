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
            GridLicencas.DataSource = Framework.GetDataTable("SELECT id_tipoLicenca, tipoLicenca, prazoLicenca FROM tb_tipoLicenca WHERE deleted = 0 ORDER BY tipoLicenca");
            GridLicencas.DataBind();
        }

        protected void PopulaCamposCadastroTipoLicenca(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_tipoLicenca tipoLicenca = new tb_tipoLicenca();

                var Query = (from objTipoLicenca in ctx.tb_tipoLicenca where objTipoLicenca.id_tipoLicenca == ID select objTipoLicenca).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtTipoLicenca.Text = Query.tipoLicenca;
                    txtPrazoLicenca.Text = Query.prazoLicenca;
                }
            }
        }

        protected void BtSalvarTipoLicenca_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_tipoLicenca TipoLicenca = new tb_tipoLicenca();
                tb_tipoLicenca TipoLicenca2 = new tb_tipoLicenca();
                try
                {
                    string _tipoLicenca = txtTipoLicenca.Text.Trim();
                    string _prazoLicenca = txtPrazoLicenca.Text.Trim();

                    var strsql = (from objtipoLicenca in ctx.tb_tipoLicenca
                                  where 
                                  objtipoLicenca.tipoLicenca == _tipoLicenca && 
                                  objtipoLicenca.prazoLicenca == _prazoLicenca &&
                                  objtipoLicenca.deleted == 0
                                  select objtipoLicenca);

                    TipoLicenca2 = strsql.FirstOrDefault();

                    //if (strsql.Count() > 0)
                    if (strsql.Any())
                    {
                        // ja existe software cadastrado
                        Response.Write("Esse Usuário já foi registrado");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(HdfID.Value))
                        {
                            int _id = Convert.ToInt32(HdfID.Value);

                            var Query = (from objTipoLicenca in ctx.tb_tipoLicenca select objTipoLicenca);

                            TipoLicenca = Query.FirstOrDefault();
                        }
                        TipoLicenca.tipoLicenca = txtTipoLicenca.Text;
                        TipoLicenca.prazoLicenca = txtPrazoLicenca.Text;
                        TipoLicenca.deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_tipoLicenca.Add(TipoLicenca);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlConsultarLicencas.Visible = true;
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
            PnlConsultarLicencas.Visible = true;
            LimpaCampos();
        }

        protected void GridLicencas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridLicencas.DataSource = Framework.GetDataTable("SELECT id_tipoLicenca, tipoLicenca, prazoLicenca FROM tb_tipoLicenca WHERE deleted = 0 ORDER BY tipoLicenca");
        }

        protected void GridLicencas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["id_tipoLicenca"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        PopulaCamposCadastroTipoLicenca(_cdID);
                        PnlCadastroTipoLicenca.Visible = true;
                        break;

                    case "opExcluir":
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_tipoLicenca TipoLicenca = new tb_tipoLicenca();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objTipoLicenca in ctx.tb_tipoLicenca where objTipoLicenca.id_tipoLicenca == ID select objTipoLicenca).FirstOrDefault();

                            Query.deleted = 1;
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