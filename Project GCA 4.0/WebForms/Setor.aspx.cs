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
            GridSetores.DataSource = Framework.GetDataTable("SELECT id_setor, nomeSetor FROM tb_setores WHERE deleted = 0 ORDER BY nomeSetor");
            GridSetores.DataBind();
        }

        protected void PopulaCamposCadastroSetor(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_setores setor = new tb_setores();

                var Query = (from objsetor in ctx.tb_setores where objsetor.id_setor == ID select objsetor).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtNomeSetor.Text = Query.nomeSetor;
                }
            }
        }

        protected void BtSalvarSetor_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_setores Setor = new tb_setores();
                tb_setores Setor2 = new tb_setores();
                try
                {
                    string _nomesetor = txtNomeSetor.Text.Trim();

                    var strsql = (from objSetor in ctx.tb_setores
                                  where objSetor.nomeSetor == _nomesetor && objSetor.deleted == 0
                                  select objSetor);

                    Setor2 = strsql.FirstOrDefault();

                    //if (strsql.Count() > 0)
                    if (strsql.Any())
                    {
                        // ja existe software cadastrado
                        Response.Write("Esse Setor já foi registrado");
                    }
                    else
                    {
                        //if (!string.IsNullOrEmpty(HdfID.Value))
                        //{
                        //    int _id = Convert.ToInt32(HdfID.Value);
                        //    var Query = (from objSetor in ctx.tb_setores select objSetor);
                        //    Setor = Query.FirstOrDefault();
                        //}
                        if (!string.IsNullOrEmpty(HdfID.Value))
                        {
                            int _id = Convert.ToInt32(HdfID.Value);
                            Setor = ctx.tb_setores.FirstOrDefault(objSetor => objSetor.id_setor == _id);
                        }
                        Setor.nomeSetor = txtNomeSetor.Text;
                        Setor.deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_setores.Add(Setor);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlConsultarSetores.Visible = true;
                        AtualizaGridSetores();
                        Framework.AlertaSucesso(this);
                    }
                }
                catch (Exception ex)
                {
                    //Response.Write("Erro, " + ex.Message);
                    Framework.AlertaErro(this, ex);
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
            GridSetores.DataSource = Framework.GetDataTable("SELECT id_setor, nomeSetor FROM tb_setores WHERE deleted = 0 ORDER BY nomeSetor");
        }

        protected void btCadastrarSetor_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroSetor.Visible = true;
        }

        protected void GridSetores_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["id_setor"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        PopulaCamposCadastroSetor(_cdID);
                        PnlCadastroSetor.Visible = true;
                        break;

                    case "opExcluir":
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_setores setor = new tb_setores();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objsetor in ctx.tb_setores where objsetor.id_setor == ID select objsetor).FirstOrDefault();

                            Query.deleted = 1;
                            ctx.SaveChanges();
                            AtualizaGridSetores();
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