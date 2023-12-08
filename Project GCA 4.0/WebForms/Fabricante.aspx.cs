using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectGCA4._0.Útil;

namespace Project_GCA_4._0.WebForms
{
    public partial class Fabricante1 : System.Web.UI.Page
    {
        protected void EscondePaineis()
        {
            PnlCadastroFabricante.Visible =
            PnlConsultaFabricante.Visible = false;
        }

        protected void LimpaCampos()
        {
            txtFabricante.Text =
            HdfID.Value =
            string.Empty;
        }

        protected void AtualizaGridFabricante()
        {
            GridFabricante.DataSource = Framework.GetDataTable("SELECT id_fabricante, nomeFabricante FROM tb_fabricante WHERE deleted = 0");
            GridFabricante.DataBind();
        }

        protected void PopulaCamposCadastroChave(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_fabricante fabricante = new tb_fabricante();

                var Query = (from objFabricante in ctx.tb_fabricante where objFabricante.id_fabricante == ID select objFabricante).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtFabricante.Text = Query.nomeFabricante;
                }
            }
        }

        protected void btCadastrarFabricante_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroFabricante.Visible = true;
        }

        protected void BtSalvarFabricante_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_fabricante Fabricante = new tb_fabricante();
                tb_fabricante Fabricante2 = new tb_fabricante();
                try
                {
                    string _fabricante = txtFabricante.Text.Trim();


                    var strsql = (from objFabricante in ctx.tb_fabricante
                                  where objFabricante.nomeFabricante == _fabricante
                                  && objFabricante.deleted == 0
                                  select objFabricante);

                    Fabricante2 = strsql.FirstOrDefault();

                    //if (strsql.Count() > 0)
                    if (strsql.Any())
                    {
                        // ja existe software cadastrado
                        //Response.Write("Essa Chave de Ativação já foi registrada");
                        Framework.Alerta(this, "Registro já consta no Sistema!");
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

                            Fabricante = ctx.tb_fabricante.FirstOrDefault(objFabricante => objFabricante.id_fabricante == _id);
                        }

                        Fabricante.nomeFabricante = txtFabricante.Text;
                        Fabricante.deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_fabricante.Add(Fabricante);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlConsultaFabricante.Visible = true;
                        AtualizaGridFabricante();
                        Framework.Alerta(this, "Salvo com Sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    Framework.AlertaErro(this, ex);
                }
            }
        }

        protected void BtCancelarFabricante_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultaFabricante.Visible = true;
            LimpaCampos();
        }

        protected void GridFabricante_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridFabricante.DataSource = Framework.GetDataTable("SELECT id_fabricante, nomeFabricante FROM tb_fabricante WHERE deleted = 0");
        }

        protected void GridFabricante_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["id_fabricante"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        LimpaCampos();
                        PopulaCamposCadastroChave(_cdID);
                        PnlCadastroFabricante.Visible = true;
                        break;

                    case "opExcluir":
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_fabricante Fabricante = new tb_fabricante();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objFabricante in ctx.tb_fabricante where objFabricante.id_fabricante == ID select objFabricante).FirstOrDefault();

                            Query.deleted = 1;
                            ctx.SaveChanges();
                            AtualizaGridFabricante();
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