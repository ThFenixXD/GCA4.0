using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectGCA4._0.Útil;

namespace Project_GCA_4._0.WebForms
{
    public partial class Software : System.Web.UI.Page
    {
        protected void EscondePaineis()
        {
            PnlCadastroSoftware.Visible =
            PnlConsultarSoftware.Visible = false;
        }

        protected void LimpaCampos()
        {
            txtNomeSoftware.Text =
            txtFabricante.Text =
            HdfID.Value =
            string.Empty;
        }

        protected void AtualizaGridSoftware()
        {
            GridSoftware.DataSource = Framework.GetDataTable("SELECT ID_Software, NomeSoftware, Fabricante FROM tb_Software WHERE Deleted = 0 Order By NomeSoftware");
            GridSoftware.DataBind();
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

        protected void BtCancelarSoftware_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarSoftware.Visible = true;
            LimpaCampos();
        }

        protected void GridSoftware_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridSoftware.DataSource = Framework.GetDataTable("select tb_Software.ID_Software, tb_Software.NomeSoftware, tb_Chaves.ID_ChaveAtivacao, tb_Chaves.DataDeCompra, tb_Chaves.TipoDeLicenca, tb_Chaves.PrazoDeLicenca, tb_Chaves.ChaveDeAtivacao, tb_ChaveDeAtivacao.Status from tb_Software Inner Join tb_Chaves on tb_Software.ID_Software = tb_Chaves.ID_ChaveAtivacao");
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


        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}