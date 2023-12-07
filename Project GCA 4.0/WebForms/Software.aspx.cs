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
            GridSoftware.DataSource = Framework.GetDataTable("SELECT ID_Software, NomeSoftware, VersaoAno, Fabricante, Idioma, Tecnologia, Compatibilidade FROM tb_Software WHERE Deleted = 0 Order By NomeSoftware");
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

        protected void PopuladdlTecnologiaSoftware()
        {
            ddlTecnologia.DataSource = Framework.GetDataTable("SELECT ID_Tecnologia, Tecnologia FROM tb_Tecnologia WHERE Deleted = 0");
            ddlTecnologia.DataBind();
            ddlTecnologia.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void PopuladdlCompatibilidadeSoftware()
        {
            ddlCompatibilidade.DataSource = Framework.GetDataTable("SELECT ID_Compatibilidade, Compatibilidade FROM tb_Compatibilidade WHERE Deleted = 0");
            ddlCompatibilidade.DataBind();
            ddlCompatibilidade.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void BtSalvarSoftware_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_Software Software = new tb_Software();
                tb_Software Software2 = new tb_Software();
                try
                {
                    string _nomesoftware = txtNomeSoftware.Text.Trim();
                    string _fabricante = txtFabricante.Text.Trim();
                    string _versao = txtVersão.Text.Trim();
                    string _tecnologia = ddlTecnologia.SelectedValue;
                    string _compatibilidade = ddlCompatibilidade.SelectedValue;

                    var strsql = (from objSoftware in ctx.tb_Software
                                  where objSoftware.Fabricante == _fabricante && objSoftware.NomeSoftware == _nomesoftware && objSoftware.VersaoAno == _versao && objSoftware.Tecnologia == _tecnologia && objSoftware.Compatibilidade == _compatibilidade && objSoftware.Deleted == 0
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

                            var Query = (from objSoftware in ctx.tb_Software select objSoftware);

                            Software = Query.FirstOrDefault();
                        }
                        else
                        {
                            var Query = (from objSoftware in ctx.tb_Software select objSoftware);

                            Software.NomeSoftware = txtNomeSoftware.Text;
                            Software.VersaoAno = txtVersão.Text;
                            Software.Fabricante = txtFabricante.Text;
                            Software.Idioma = txtIdioma.Text;
                            Software.Tecnologia = ddlTecnologia.SelectedItem.ToString();
                            Software.ID_Tecnologia = Convert.ToInt32(ddlTecnologia.SelectedValue);
                            Software.Compatibilidade = ddlCompatibilidade.SelectedItem.ToString();
                            Software.ID_Compatibilidade = Convert.ToInt32(ddlCompatibilidade.SelectedValue);
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
            GridSoftware.DataSource = Framework.GetDataTable("SELECT ID_Software, NomeSoftware, VersaoAno, Fabricante, Idioma, Tecnologia, Compatibilidade FROM tb_Software WHERE Deleted = 0 Order By NomeSoftware");
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