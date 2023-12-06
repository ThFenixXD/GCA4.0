using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectGCA4._0.Útil;

namespace Project_GCA_4._0.WebForms
{
    public partial class Setor : System.Web.UI.Page
    {
        protected void EscondePaineis()
        {
            PnlCadastroSetor.Visible =
            PnlConsultarSetores.Visible = false;
        }

        protected void LimpaCampos()
        {
            txtNomeSetor.Text =
            HdfID.Value =
            string.Empty;
        }

        protected void AtualizaGridSetores()
        {
            GridSetores.DataSource = Framework.GetDataTable("select ID_Setor, NomeSetor From tb_Setores Where Deleted = 0 Order By NomeSetor");
            GridSetores.DataBind();
        }

        protected void BtSalvarSetor_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_Setores Setor = new tb_Setores();
                try
                {
                    if (!string.IsNullOrEmpty(HdfID.Value))
                    {
                        int _id = Convert.ToInt32(HdfID.Value);

                        var Query = (from objSetor in ctx.tb_Setores select objSetor);

                        Setor = Query.FirstOrDefault();
                    }
                    else
                    {
                        Setor.NomeSetor = txtNomeSetor.Text;
                        Setor.Deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_Setores.Add(Setor);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlConsultarSetores.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
        }

        protected void BtCancelarSetor_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarSetores.Visible = true;
            LimpaCampos();
        }

        protected void GridSetores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridSetores.DataSource = Framework.GetDataTable("select ID_Setor, NomeSetor From tb_Setores Where Deleted = 0 Order By NomeSetor");
        }

        protected void GridSetores_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID_Setor"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        LimpaCampos();
                        PnlCadastroSetor.Visible = true;
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

        protected void btCadastrarSetor_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroSetor.Visible = true;
        }
    }
}