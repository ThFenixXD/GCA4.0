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
            GridSoftware.DataSource = Framework.GetDataTable("SELECT SO.id_software, SO.nomeSoftware, SO.versao, SO.ano, SO.fabricante, IDI.id_idioma, TEC.id_tecnologia, CSO.id_compatibilidadeSO FROM tb_software SO INNER JOIN tb_idiomas IDI ON SO.id_idioma = IDI.id_idioma INNER JOIN tb_tecnologia TEC ON SO.id_tecnologia = TEC.deleted INNER JOIN tb_compatibilidadeSO CSO ON SO.id_compatibilidade = CSO.id_compatibilidadeSO ORDER BY SO.nomeSoftware");
            GridSoftware.DataBind();
        }

        protected void PopulaCamposCadastroSoftware(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_software Software = new tb_software();

                var Query = (from objSoftware in ctx.tb_software where objSoftware.id_software == ID select objSoftware).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtNomeSoftware.Text = Query.nomeSoftware;
                    txtFabricante.Text = Query.fabricante;
                }
            }
        }

        protected void PopuladdlTecnologiaSoftware()
        {
            ddlTecnologia.DataSource = Framework.GetDataTable("SELECT ID_Tecnologia, Tecnologia FROM tb_Tecnologia WHERE Deleted = 0");
            ddlTecnologia.DataBind();
            ddlTecnologia.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopuladdlCompatibilidadeSoftware()
        {
            ddlCompatibilidade.DataSource = Framework.GetDataTable("SELECT id_compatibilidadeSO, compatibilidadeSO FROM tb_compatibilidadeSO WHERE deleted = 0");
            ddlCompatibilidade.DataBind();
            ddlCompatibilidade.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void BtSalvarSoftware_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_software Software = new tb_software();
                tb_software Software2 = new tb_software();
                try
                {
                    string _nomesoftware = txtNomeSoftware.Text.Trim();
                    string _fabricante = txtFabricante.Text.Trim();
                    string _versao = txtVersão.Text.Trim();
                    string _ano = txtAno.Text.Trim();
                    int _idioma = Convert.ToInt32(ddlIdioma.SelectedValue);
                    int _tecnologia = Convert.ToInt32(ddlTecnologia.SelectedValue);
                    int _compatibilidade = Convert.ToInt32(ddlCompatibilidade.SelectedValue);

                    var strsql = (from objSoftware in ctx.tb_software
                                  where
                                  objSoftware.nomeSoftware == _nomesoftware &&
                                  objSoftware.fabricante == _fabricante &&
                                  objSoftware.versao == _versao &&
                                  objSoftware.ano == _ano &&
                                  objSoftware.id_idioma == _idioma &&
                                  objSoftware.id_tecnologia == _tecnologia &&
                                  objSoftware.id_compatibilidade == _compatibilidade &&
                                  objSoftware.deleted == 0
                                  select objSoftware);

                    Software2 = strsql.FirstOrDefault();

                    if (strsql.Count() > 0)
                    {
                        // ja existe software cadastrado
                        Response.Write("Esse Software já foi registrado");
                    }
                    else
                    {
                        //Não existe software cadastrado ..... aqui 

                        if (!string.IsNullOrEmpty(HdfID.Value))
                        {
                            int _id = Convert.ToInt32(HdfID.Value);

                            var Query2 = (from objSoftware in ctx.tb_software select objSoftware);

                            Software = Query2.FirstOrDefault();
                        }
                        var Query = (from objSoftware in ctx.tb_software select objSoftware);

                        Software.nomeSoftware = txtNomeSoftware.Text;
                        Software.versao = txtVersão.Text;
                        Software.ano = txtAno.Text;
                        Software.fabricante = txtFabricante.Text;
                        Software.id_idioma = Convert.ToInt32(ddlIdioma.SelectedValue);
                        Software.id_tecnologia = Convert.ToInt32(ddlTecnologia.SelectedValue);
                        Software.id_compatibilidade = Convert.ToInt32(ddlCompatibilidade.SelectedValue);
                        Software.deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_software.Add(Software);
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
            GridSoftware.DataSource = Framework.GetDataTable("SELECT SO.id_software, SO.nomeSoftware, SO.versao, SO.ano, SO.fabricante, IDI.id_idioma, TEC.id_tecnologia, CSO.id_compatibilidadeSO FROM tb_software SO INNER JOIN tb_idiomas IDI ON SO.id_idioma = IDI.id_idioma INNER JOIN tb_tecnologia TEC ON SO.id_tecnologia = TEC.deleted INNER JOIN tb_compatibilidadeSO CSO ON SO.id_compatibilidade = CSO.id_compatibilidadeSO ORDER BY SO.nomeSoftware");
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
                            tb_software Software = new tb_software();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objSoftware in ctx.tb_software where objSoftware.id_software == ID select objSoftware).FirstOrDefault();

                            Query.deleted = 1;
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

        protected void btCadastrarSoftware_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroSoftware.Visible = true;
            PopuladdlTecnologiaSoftware();
            PopuladdlCompatibilidadeSoftware();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopuladdlTecnologiaSoftware();
                PopuladdlCompatibilidadeSoftware();
            }
        }
    }
}