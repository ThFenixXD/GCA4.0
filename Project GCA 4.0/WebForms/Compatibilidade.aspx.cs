using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectGCA4._0.Útil;


namespace Project_GCA_4._0.WebForms
{
    public partial class Compatibilidade : System.Web.UI.Page
    {
        protected void EscondePaineis()
        {
            PnlCadastroCompatibilidade.Visible =
            PnlConsultarCompatibilidade.Visible = false;
        }

        protected void LimpaCampos()
        {
            txtCompatibilidade.Text =
            HdfID.Value =
            string.Empty;
        }

        protected void AtualizaGridCompatibilidade()
        {
            GridCompatibilidade.DataSource = Framework.GetDataTable("SELECT id_compatibilidadeSO, compatibilidadeSO FROM tb_compatibilidadeSO WHERE deleted = 0 ORDER BY compatibilidadeSO");
            GridCompatibilidade.DataBind();
            
        }

        protected void PopulaCamposCadastroCompatibilidade(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_compatibilidadeSO Compatibilidade = new tb_compatibilidadeSO();

                var Query = (from objCompatibilidade in ctx.tb_compatibilidadeSO where objCompatibilidade.id_compatibilidadeSO == ID select objCompatibilidade).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtCompatibilidade.Text = Query.compatibilidadeSO;
                }
            }
        }

        protected void GridCompatibilidade_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridCompatibilidade.DataSource = Framework.GetDataTable("SELECT id_compatibilidadeSO, compatibilidadeSO FROM tb_compatibilidadeSO WHERE deleted = 0 ORDER BY compatibilidadeSO");
        }

        protected void GridCompatibilidade_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["id_compatibilidadeSO"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        PopulaCamposCadastroCompatibilidade(_cdID);
                        PnlCadastroCompatibilidade.Visible = true;
                        break;

                    case "opExcluir":
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_compatibilidadeSO compatibilidade = new tb_compatibilidadeSO();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objCompatibilidade in ctx.tb_compatibilidadeSO where objCompatibilidade.id_compatibilidadeSO == ID select objCompatibilidade).FirstOrDefault();

                            Query.deleted = 1;
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

        protected void btCadastrarCompatibilidade_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            LimpaCampos();
            PnlCadastroCompatibilidade.Visible = true;
        }

        protected void BtSalvarCompatibilidade_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_compatibilidadeSO Compatibilidade = new tb_compatibilidadeSO();
                tb_compatibilidadeSO Compatibilidade2 = new tb_compatibilidadeSO();
                try
                {
                    string _compatibilidade = txtCompatibilidade.Text.Trim();

                    var strsql = (from objCompatibilidade in ctx.tb_compatibilidadeSO
                                  where 
                                  objCompatibilidade.compatibilidadeSO == _compatibilidade &&
                                  objCompatibilidade.deleted == 0
                                  select objCompatibilidade);

                    Compatibilidade2 = strsql.FirstOrDefault();

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
                        //    var Query = (from objCompatibilidade in ctx.tb_compatibilidadeSO select objCompatibilidade);
                        //    Compatibilidade = Query.FirstOrDefault();
                        //}
                        if (!string.IsNullOrEmpty(HdfID.Value))
                        {
                            int _id = Convert.ToInt32(HdfID.Value);

                            Compatibilidade = ctx.tb_compatibilidadeSO.FirstOrDefault(objCompatibilidade => objCompatibilidade.id_compatibilidadeSO == _id);
                        }
                        Compatibilidade.compatibilidadeSO = txtCompatibilidade.Text;
                        Compatibilidade.deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_compatibilidadeSO.Add(Compatibilidade);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlConsultarCompatibilidade.Visible = true;
                        AtualizaGridCompatibilidade();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
        }

        protected void BtCancelarCompatibilidade_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            LimpaCampos();
            PnlConsultarCompatibilidade.Visible = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


    }
}