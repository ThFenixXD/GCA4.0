﻿using System;
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
            GridMaquinas.DataSource = Framework.GetDataTable("SELECT MAQ.id_maquina, MAQ.nomeMaquina, SE.id_setor , SE.nomeSetor, MAQ.status FROM tb_Maquinas MAQ INNER JOIN tb_setores SE ON MAQ.id_setor = SE.id_setor  WHERE MAQ.deleted = 0  ORDER BY MAQ.NomeMaquina");
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
            DdlSetorMaquina.DataSource = Framework.GetDataTable("SELECT ID_Setor, NomeSetor FROM tb_Setores WHERE Deleted = 0 ORDER BY nomeSetor");
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
                                  objMaquina.deleted == 0
                                  select objMaquina);

                    Maquina2 = strsql.FirstOrDefault();

                    //if (strsql.Count() > 0)
                    if (strsql.Any())
                    {
                        // ja existe software cadastrado
                        //Response.Write("Essa Máquina já foi registrada");
                        Framework.Alerta(this, "Registro já consta no Sistema!");
                    }
                    else
                    {
                        //if (!string.IsNullOrEmpty(HdfID.Value))
                        //{
                        //    int _id = Convert.ToInt32(HdfID.Value);
                        //    var Query = (from objMaquina in ctx.tb_maquinas select objMaquina);
                        //    Maquina = Query.FirstOrDefault();
                        //}
                        if (!string.IsNullOrEmpty(HdfID.Value))
                        {
                            int _id = Convert.ToInt32(HdfID.Value);
                            Maquina = ctx.tb_maquinas.FirstOrDefault(objMaquina => objMaquina.id_maquina == _id);
                        }
                        Maquina.nomeMaquina = txtNomeMaquina.Text;
                        Maquina.id_setor = Convert.ToInt32(DdlSetorMaquina.SelectedValue);
                        Maquina.status = 0;
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

        protected void BtCancelarMaquina_Click(object sender, EventArgs e)
        {
            EscondePaineis();
            PnlConsultarMaquinas.Visible = true;
            LimpaCampos();
        }

        protected void GridMaquinas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridMaquinas.DataSource = Framework.GetDataTable("SELECT MAQ.id_maquina, MAQ.nomeMaquina, SE.id_setor , SE.nomeSetor, MAQ.status FROM tb_Maquinas MAQ INNER JOIN tb_setores SE ON MAQ.id_setor = SE.id_setor  WHERE MAQ.deleted = 0  ORDER BY MAQ.NomeMaquina");
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