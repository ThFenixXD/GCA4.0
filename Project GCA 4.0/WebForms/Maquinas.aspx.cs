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
            GridMaquinas.DataSource = Framework.GetDataTable("SELECT ID_Maquina, ID_Maquina, NomeMaquina, ID_Setor, SetorMaquina, Status FROM tb_Maquinas WHERE Deleted = 0 Order By NomeMaquina");
            GridMaquinas.DataBind();
        }

        protected void PopulaCamposCadastroMaquina(int _cdID)
        {
            using (GCAEntities ctx = new GCAEntities())
            {
                int ID = _cdID;
                HdfID.Value = _cdID.ToString();

                tb_Maquinas Maquina = new tb_Maquinas();

                var Query = (from objMaquina in ctx.tb_Maquinas where objMaquina.ID_Maquina == ID select objMaquina).FirstOrDefault();

                if (!string.IsNullOrEmpty(Query.ToString()))
                {
                    txtNomeMaquina.Text = Query.NomeMaquina;
                    DdlSetorMaquina.SelectedValue = Query.ID_Setor.ToString();
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
                tb_Maquinas Maquina = new tb_Maquinas();
                tb_Maquinas Maquina2 = new tb_Maquinas();
                try
                {
                    string _nomemaquina = txtNomeMaquina.Text.Trim();
                    int _setor = Convert.ToInt32(DdlSetorMaquina.SelectedValue);

                    var strsql = (from objMaquina in ctx.tb_Maquinas
                                  where objMaquina.NomeMaquina == _nomemaquina && objMaquina.ID_Setor == _setor && objMaquina.ID_Setor == _setor && objMaquina.Deleted == 0
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

                            var Query = (from objMaquina in ctx.tb_Maquinas select objMaquina);

                            Maquina = Query.FirstOrDefault();
                        }
                        else
                        {
                            Maquina.NomeMaquina = txtNomeMaquina.Text;
                            Maquina.ID_Setor = Convert.ToInt32(DdlSetorMaquina.SelectedValue);
                            Maquina.SetorMaquina = DdlSetorMaquina.SelectedItem.ToString();
                            Maquina.Status = "INATIVA";
                            Maquina.Deleted = 0;

                            if (string.IsNullOrEmpty(HdfID.Value))
                            {
                                ctx.tb_Maquinas.Add(Maquina);
                            }
                            ctx.SaveChanges();
                            EscondePaineis();
                            LimpaCampos();
                            PnlConsultarMaquinas.Visible = true;
                            AtualizaGridMaquinas();
                        }
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
            GridMaquinas.DataSource = Framework.GetDataTable("SELECT ID_Maquina, ID_Maquina, NomeMaquina, ID_Setor, SetorMaquina, Status FROM tb_Maquinas WHERE Deleted = 0");
        }

        protected void GridMaquinas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                int _cdID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ID_Maquina"]);

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
                            tb_Maquinas Maquina = new tb_Maquinas();

                            int ID = _cdID;
                            HdfID.Value = _cdID.ToString();

                            var Query = (from objMaquina in ctx.tb_Maquinas where objMaquina.ID_Maquina == ID select objMaquina).FirstOrDefault();

                            Query.Deleted = 1;
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