using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectGCA4._0.Útil;

namespace Project_GCA_4._0.WebForms
{
    public partial class Maquinas : System.Web.UI.Page
    {
        protected void EscondePaineis()
        {
            PnlCadastroMaquina.Visible =
            PnlConsultarMaquinas.Visible = false;
        }

        protected void LimpaCampos()
        {
            txtNomeMaquina.Text =
            HdfID.Value =
            string.Empty;
        }

        protected void AtualizaGridMaquinas()
        {
            GridMaquinas.DataSource = Framework.GetDataTable("SELECT tb_maquinas.id_maquina, tb_maquinas.nomeMaquina, tb_setores.id_setor , tb_setores.nomeSetor FROM tb_Maquinas INNER JOIN tb_setores ON tb_maquinas.id_setor = tb_setores.id_setor WHERE tb_maquinas.deleted = 0 Order By NomeMaquina");
            GridMaquinas.DataBind();
        }

        protected void PopulaCamposCadastroMaquina(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_maquinas maquina = new tb_maquinas();

                var Query = (from objMaquina in ctx.tb_maquinas where objMaquina.id_maquina == ID select objMaquina).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtNomeMaquina.Text = Query.nomeMaquina;
                    DdlSetorMaquina.SelectedValue = Query.id_setor.ToString();
                }
            }
        }

        protected void PopulaDdlSetorMaquina()
        {
            DdlSetorMaquina.DataSource = Framework.GetDataTable("SELECT ID_Setor, NomeSetor FROM tb_Setores WHERE Deleted = 0");
            DdlSetorMaquina.DataBind();
            DdlSetorMaquina.Items.Insert(0, new ListItem("Selecionar"));
        }

        protected void BtSalvarMaquina_Click(object sender, EventArgs e)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                tb_maquinas Maquina = new tb_maquinas();
                tb_maquinas Maquina2 = new tb_maquinas();
                try
                {
                    string _nomemaquina = txtNomeMaquina.Text.Trim();
                    int _setor = Convert.ToInt32(DdlSetorMaquina.SelectedValue);

                    var strsql = (from objMaquina in ctx.tb_maquinas
                                  where
                                  objMaquina.nomeMaquina == _nomemaquina && 
                                  objMaquina.id_setor == _setor 
                                  && objMaquina.deleted == 0
                                  select objMaquina);

                    Maquina2 = strsql.FirstOrDefault();

                    if (strsql.Count() > 0)
                    {
                        // ja existe software cadastrado
                        Response.Write("Essa Máquina já foi registrada");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(HdfID.Value))
                        {
                            int _id = Convert.ToInt32(HdfID.Value);

                            var Query = (from objMaquina in ctx.tb_maquinas select objMaquina);

                            Maquina = Query.FirstOrDefault();
                        }
                        Maquina.nomeMaquina = txtNomeMaquina.Text;
                        Maquina.id_setor = Convert.ToInt32(DdlSetorMaquina.SelectedValue);
                        Maquina.deleted = 0;

                        if (string.IsNullOrEmpty(HdfID.Value))
                        {
                            ctx.tb_maquinas.Add(Maquina);
                        }
                        ctx.SaveChanges();
                        EscondePaineis();
                        LimpaCampos();
                        PnlConsultarMaquinas.Visible = true;
                        AtualizaGridMaquinas();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Erro, " + ex.Message);
                }
            }
        }

        protected void BtCancelarMaquina_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarMaquinas.Visible = true;
            LimpaCampos();
        }

        protected void GridMaquinas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridMaquinas.DataSource = Framework.GetDataTable("SELECT tb_maquinas.id_maquina, tb_maquinas.nomeMaquina, tb_setores.id_setor , tb_setores.nomeSetor FROM tb_Maquinas INNER JOIN tb_setores ON tb_maquinas.id_setor = tb_setores.id_setor WHERE tb_maquinas.deleted = 0 Order By NomeMaquina");
        }

        protected void GridMaquinas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["id_maquina"]);

                switch (e.CommandName)
                {
                    case "opSelecionar":
                        break;

                    case "opEditar":
                        EscondePaineis();
                        LimpaCampos();
                        PopulaCamposCadastroMaquina(_cdID);
                        PnlCadastroMaquina.Visible = true;
                        break;

                    case "opExcluir":
                        using (GCAEntities ctx = new GCAEntities())
                        {
                            tb_maquinas Maquina = new tb_maquinas();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objMaquina in ctx.tb_maquinas where objMaquina.id_maquina == ID select objMaquina).FirstOrDefault();

                            Query.deleted = 1;
                            ctx.SaveChanges();
                            AtualizaGridMaquinas();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Response.Write("Erro, " + ex.Message);
            }
        }

        protected void btCadastroMaquina_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlCadastroMaquina.Visible = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulaDdlSetorMaquina();
            }
        }


    }
}