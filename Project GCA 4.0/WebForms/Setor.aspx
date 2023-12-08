<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.Master" AutoEventWireup="true" CodeBehind="Setor.aspx.cs" Inherits="Project_GCA_4._0.WebForms.Setor" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="PnlCadastroSetor" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="false">
        <section class="row">
            <div class="col-12 col-md-12 col-sm-12 mb-5 text-uppercase">
                <asp:Label ID="lbSetorTitulo" CssClass="LbTitulo" runat="server" Text="Setor"></asp:Label>
            </div>
            <div class="DivTextBlock row d-flex m-auto gap-2">
                <div class="row">
                    <asp:Label ID="lbSetor" CssClass="lbTextBlock col-4 col-md-4 col-sm-4" runat="server" Text="Nome do Setor"></asp:Label>
                    <asp:TextBox ID="txtNomeSetor" CssClass="col-8 col-md-8 col-sm-8" runat="server"></asp:TextBox>
                </div>
                <div class="row p-0 m-0 gap-3 justify-content-end">
                    <div class="col-7 col-md-7 col-sm-7"></div>
                    <asp:Button ID="BtSalvarSetor" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Salvar" OnClick="BtSalvarSetor_Click" />
                    <asp:Button ID="BtCancelarSetor" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Cancelar" OnClick="BtCancelarSetor_Click" />
                </div>
            </div>
        </section>
    </asp:Panel>

    <asp:Panel ID="PnlConsultarSetores" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="true">
        <div class="row">
            <div class="col-12 col-md-12 col-sm-12 my-4 text-uppercase">
                <asp:Label runat="server" Text="Setores" CssClass="LbTitulo"></asp:Label>
            </div>
            <div class="col-12 col-md-12 col-sm-12">
                <telerik:RadGrid ID="GridSetores" runat="server" AutoGenerateColumns="false" OnNeedDataSource="GridSetores_NeedDataSource" OnItemCommand="GridSetores_ItemCommand">
                    <GroupingSettings CollapseAllTooltip="collaps all columns" />
                    <MasterTableView DataKeyNames="id_setor">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="OP" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:Button ID="btEditar" runat="server" Text="Editar" CommandName="opEditar" />
                                    <asp:Button ID="btexcluir" runat="server" Text="Excluir" CommandName="opExcluir" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="col_CodSetor" DataField="id_setor" HeaderText="COD" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_NomeSetor" DataField="nomeSetor" HeaderText="SETOR" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="col-12 col-md-12 col-sm-12 text-end my-3">
                <asp:Button ID="btCadastrarSetor" runat="server" Text="Cadastrar" OnClick="btCadastrarSetor_Click" />
            </div>
        </div>
    </asp:Panel>

    <asp:HiddenField ID="HdfID" runat="server" />
</asp:Content>
