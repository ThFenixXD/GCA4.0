using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectGCA4._0.Útil;

namespace Project_GCA_4._0.WebForms
{
    public partial class Idioma : System.Web.UI.Page
    {
        protected void EscondePaineis()
        {
            PnlCadastroIdioma.Visible =
            PnlConsultarIdiomas.Visible = false;
        }

        protected void LimpaCampos()
        {
            txtIdioma.Text =
            HdfID.Value =
            string.Empty;
        }

        protected void AtualizaGridIdioma()
        {
            GridIdiomas.DataSource = Framework.GetDataTable("SELECT id_idioma, idioma, sigla, pais FROM tb_idiomas WHERE deleted = 0 ORDER BY idioma");
            GridIdiomas.DataBind();
        }

        protected void PopulaCamposCadastroUsuario(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_idiomas Usuario = new tb_idiomas();

                var Query = (from objIdioma in ctx.tb_idiomas where objIdioma.id_idioma == ID select objIdioma).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtIdioma.Text = Query.idioma;
                    txtSigla.Text = Query.sigla;
                    txtPaisOrigem.Text = Query.pais;
                }
            }
        }

        protected void BtSalvarIdioma_Click(object sender, EventArgs e) /*REVISAR COMPARAÇÃO*/
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_idiomas Idioma = new tb_idiomas();
                tb_idiomas Idioma2 = new tb_idiomas();
                try
                {
                    string _idioma = txtIdioma.Text.Trim();
                    string _sigla = txtSigla.Text.Trim();
                    string _pais = txtPaisOrigem.Text.Trim();

                    var strsql = (from objIdioma in ctx.tb_idiomas
                                  where objIdioma.idioma == _idioma &&
                                  objIdioma.sigla == _sigla &&
                                  objIdioma.pais == _pais &&
                                  objIdioma.deleted == 0
                                  select objIdioma);

                    Idioma2 = strsql.FirstOrDefault();

                    //if (strsql.Count() > 0)
                    if (strsql.Any())
                    {
                        Response.Write("Esse Idioma já foi registrado");
                    }
                    else
                    {
                        //if (!string.IsNullOrEmpty(HdfID.Value))
                        //{
                        //    int _id = Convert.ToInt32(HdfID.Value);
                        //    var Query = (from objidioma in ctx.tb_idiomas select objidioma);
                        //    idioma = Query.FirstOrDefault();
                        //}
                        if (!string.IsNullOrEmpty(HdfID.Value))
                        {
                            int _id = Convert.ToInt32(HdfID.Value);
                            Idioma = ctx.tb_idiomas.FirstOrDefault(objIdioma => objIdioma.id_idioma == _id);
                        }

                        Idioma.idioma = txtIdioma.Text;
                        Idioma.sigla = txtSigla.Text;
                        Idioma.pais = txtPaisOrigem.Text;
                        Idioma.deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_idiomas.Add(Idioma);
                        }

                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlConsultarIdiomas.Visible = true;
                        AtualizaGridIdioma();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
        }

        protected void BtCancelarIdioma_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarIdiomas.Visible = true;
            LimpaCampos();
        }

        protected void btCadastrarIdioma_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroIdioma.Visible = true;
        }

        protected void GridIdiomas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridIdiomas.DataSource = Framework.GetDataTable("SELECT id_idioma, idioma, sigla, pais FROM tb_idiomas WHERE deleted = 0 ORDER BY idioma");
        }

        protected void GridIdiomas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["id_idioma"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        PopulaCamposCadastroUsuario(_cdID);
                        PnlCadastroIdioma.Visible = true;
                        break;

                    case "opExcluir":
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_idiomas idioma = new tb_idiomas();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objIdioma in ctx.tb_idiomas where objIdioma.id_idioma == ID select objIdioma).FirstOrDefault();

                            Query.deleted = 1;
                            ctx.SaveChanges();
                            AtualizaGridIdioma();
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