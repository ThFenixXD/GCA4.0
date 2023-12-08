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
            HdfID.Value =
            string.Empty;
        }

        protected void AtualizaGridSoftware()
        {
            GridSoftware.DataSource = Framework.GetDataTable("SELECT SO.id_software, SO.nomeSoftware, SO.versao, SO.ano, SO.fabricante, IDI.id_idioma, IDI.idioma, TEC.id_tecnologia, TEC.tecnologia, CSO.id_compatibilidadeSO, CSO.compatibilidadeSO FROM tb_software SO INNER JOIN tb_idiomas IDI ON SO.id_idioma = IDI.id_idioma INNER JOIN tb_tecnologia TEC ON SO.id_tecnologia = TEC.id_tecnologia INNER JOIN tb_compatibilidadeSO CSO ON SO.id_compatibilidadeSO = CSO.id_compatibilidadeSO WHERE SO.deleted = 0 ORDER BY SO.nomeSoftware");
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
                    txtVersão.Text = Query.versao;
                    txtAno.Text = Query.ano;
                    ddlFabricante.SelectedValue = Query.id_fabricante.ToString();
                    ddlIdioma.SelectedValue = Query.id_idioma.ToString();
                    ddlTecnologia.SelectedValue = Query.id_tecnologia.ToString();
                    ddlCompatibilidade.SelectedValue = Query.id_compatibilidadeSO.ToString();
                }
            }
        }

        protected void PopuladdlFabricante()
        {
            ddlFabricante.DataSource = Framework.GetDataTable("SELECT id_fabricante, nomeFabricante FROM tb_fabricante WHERE deleted = 0");
            ddlFabricante.DataBind();
            ddlFabricante.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopuladdlIdioma()
        {
            ddlIdioma.DataSource = Framework.GetDataTable("SELECT id_idioma, idioma FROM tb_idiomas WHERE deleted = 0");
            ddlIdioma.DataBind();
            ddlIdioma.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopuladdlTecnologia()
        {
            ddlTecnologia.DataSource = Framework.GetDataTable("SELECT id_tecnologia, tecnologia FROM tb_tecnologia WHERE deleted = 0");
            ddlTecnologia.DataBind();
            ddlTecnologia.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopuladdlCompatibilidade()
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
                    int _fabricante = Convert.ToInt32(ddlFabricante.SelectedValue);
                    string _versao = txtVersão.Text.Trim();
                    string _ano = txtAno.Text.Trim();
                    int _idioma = Convert.ToInt32(ddlIdioma.SelectedValue);
                    int _tecnologia = Convert.ToInt32(ddlTecnologia.SelectedValue);
                    int _compatibilidade = Convert.ToInt32(ddlCompatibilidade.SelectedValue);

                    var strsql = (from objSoftware in ctx.tb_software
                                  where
                                  objSoftware.nomeSoftware == _nomesoftware &&
                                  objSoftware.id_fabricante == _fabricante &&
                                  objSoftware.versao == _versao &&
                                  objSoftware.ano == _ano &&
                                  objSoftware.id_idioma == _idioma &&
                                  objSoftware.id_tecnologia == _tecnologia &&
                                  objSoftware.id_compatibilidadeSO == _compatibilidade &&
                                  objSoftware.deleted == 0
                                  select objSoftware);

                    Software2 = strsql.FirstOrDefault();

                    //if (strsql.Count() > 0)
                    if (strsql.Any())
                    {
                        // ja existe software cadastrado
                        //Response.Write("Esse Software já foi registrado");
                        Framework.Alerta(this, "Registro já consta no Sistema!");
                    }
                    else
                    {
                        //Não existe software cadastrado ..... aqui 

                        //if (!string.IsNullOrEmpty(HdfID.Value))
                        //{
                        //    int _id = Convert.ToInt32(HdfID.Value);
                        //    var Query2 = (from objSoftware in ctx.tb_software select objSoftware);
                        //    Software = Query2.FirstOrDefault();
                        //}

                        if (!string.IsNullOrEmpty(HdfID.Value))
                        {
                            int _id = Convert.ToInt32(HdfID.Value);
                            Software = ctx.tb_software.FirstOrDefault(objSoftware => objSoftware.id_software == _id);
                        }

                        Software.nomeSoftware = txtNomeSoftware.Text;
                        Software.versao = txtVersão.Text;
                        Software.ano = txtAno.Text;
                        Software.id_fabricante = Convert.ToInt32(ddlFabricante.SelectedValue);
                        Software.id_idioma = Convert.ToInt32(ddlIdioma.SelectedValue);
                        Software.id_tecnologia = Convert.ToInt32(ddlTecnologia.SelectedValue);
                        Software.id_compatibilidadeSO = Convert.ToInt32(ddlCompatibilidade.SelectedValue);
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

        protected void BtCancelarSoftware_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarSoftware.Visible = true;
            LimpaCampos();
        }

        protected void GridSoftware_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridSoftware.DataSource = Framework.GetDataTable("SELECT SO.id_software, SO.nomeSoftware, SO.versao, SO.ano, SO.fabricante, IDI.id_idioma, IDI.idioma, TEC.id_tecnologia, TEC.tecnologia, CSO.id_compatibilidadeSO, CSO.compatibilidadeSO FROM tb_software SO INNER JOIN tb_idiomas IDI ON SO.id_idioma = IDI.id_idioma INNER JOIN tb_tecnologia TEC ON SO.id_tecnologia = TEC.id_tecnologia INNER JOIN tb_compatibilidadeSO CSO ON SO.id_compatibilidadeSO = CSO.id_compatibilidadeSO WHERE SO.deleted = 0 ORDER BY SO.nomeSoftware");
        }

        protected void GridSoftware_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["id_software"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        LimpaCampos();
                        PopulaCamposCadastroSoftware(_cdID);
                        PnlCadastroSoftware.Visible = true;
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
            PopuladdlTecnologia();
            PopuladdlCompatibilidade();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopuladdlIdioma();
                PopuladdlTecnologia();
                PopuladdlCompatibilidade();
            }
        }
    }
}