<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.Master" AutoEventWireup="true" CodeBehind="Chaves.aspx.cs" Inherits="Project_GCA_4._0.WebForms.Chaves" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PnlCadastroChaveAtivacao" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="false">
        <section class="row">
            <div class="col-12 col-md-12 col-sm-12 mb-5 text-uppercase">
                <asp:Label ID="lbChaveAtivacaoTitulo" CssClass="LbTitulo" runat="server" Text="Chave de Ativação"></asp:Label>
            </div>
            <div class="DivTextBlock row d-flex m-auto gap-2">
                <div class="row">
                    <asp:Label ID="lbSoftware" CssClass="lbTextBlock col-4 col-md-4 col-sm-4 p-0" runat="server" Text="Software"></asp:Label>
                    <asp:DropDownList ID="DdlSoftware" CssClass="col-8 col-md-8 col-sm-8" runat="server" DataTextField="nomeSoftware" DataValueField="id_software"></asp:DropDownList>
                </div>
                <div class="row">
                    <asp:Label ID="lbDataDeCompra" CssClass="lbTextBlock col-4 col-md-4 col-sm-4 p-0" runat="server" Text="Data de Compra"></asp:Label>
                    <asp:TextBox ID="txtDataDeCompra" CssClass="col-8 col-md-8 col-sm-8 text-center" runat="server"></asp:TextBox>
                </div>
                <div class="row">
                    <asp:Label ID="lbTipoDeLicenca" CssClass="lbTextBlock col-4 col-md-4 col-sm-4 p-0" runat="server" Text="Tipo de Licença"></asp:Label>
                    <asp:DropDownList ID="DdlTipoLicenca" CssClass="col-8 col-md-8 col-sm-8" runat="server" DataTextField="tipoLicenca" DataValueField="id_tipoLicenca"></asp:DropDownList>
                </div>
                <div class="row">
                    <asp:Label ID="lbPrazoDeLicenca" CssClass="lbTextBlock col-4 col-md-4 col-sm-4 p-0" runat="server" Text="Prazo de Licença"></asp:Label>
                    <asp:TextBox ID="txtPrazoLicenca" CssClass="col-8 col-md-8 col-sm-8" runat="server" DataTextField="prazoLicenca" DataValueField="id_tipoLicenca"></asp:TextBox>
                </div>
                <div class="row">
                    <asp:Label ID="lbChaveAtivacao" CssClass="lbTextBlock col-4 col-md-4 col-sm-4 p-0" runat="server" Text="Chave de Ativação"></asp:Label>
                    <asp:TextBox ID="txtChaveAtivacao" CssClass="col-8 col-md-8 col-sm-8" runat="server"></asp:TextBox>
                </div>
                <div class="row p-0 m-0 gap-3 justify-content-end">
                    <div class="col-7 col-md-7 col-sm-7"></div>
                    <asp:Button ID="BtSalvarChaveAtivacao" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Salvar" OnClick="BtSalvarChaveAtivacao_Click" />
                    <asp:Button ID="BtCancelarChaveAtivacao" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Cancelar" OnClick="BtCancelarChaveAtivacao_Click" />
                </div>
            </div>
        </section>
    </asp:Panel>

    <asp:Panel ID="PnlConsultarChaves" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="true">
        <div class="row">
            <div class="col-12 col-md-12 col-sm-12 my-4 text-uppercase">
                <asp:Label runat="server" Text="Chaves de Ativação" CssClass="LbTitulo"></asp:Label>
            </div>
            <div class="col-12 col-md-12 col-sm-12">
                <telerik:RadGrid ID="GridChaves" runat="server" AutoGenerateColumns="false" OnNeedDataSource="GridChaves_NeedDataSource" OnItemCommand="GridChaves_ItemCommand">
                    <GroupingSettings CollapseAllTooltip="collaps all columns" />
                    <MasterTableView DataKeyNames="id_chave">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="OP" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:Button ID="btEditar" runat="server" Text="Editar" CommandName="opEditar" />
                                    <asp:Button ID="btexcluir" runat="server" Text="Excluir" CommandName="opExcluir" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="col_CodSoftware" DataField="id_chave" HeaderText="COD" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_Software" DataField="nomeSoftware" HeaderText="SOFTWARE" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_ChaveAtivacao" DataField="chave" HeaderText="CHAVE DE ATIVAÇÃO" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_DataDeCompra" DataField="dataDeCompra" HeaderText="DATA DE COMPRA" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_TipoDeLicenca" DataField="tipoLicenca" HeaderText="TIPO DE LICENCA" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_PrazoDeLicenca" DataField="prazoLicenca" HeaderText="PRAZO DE LICENCA" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_Status" DataField="status" HeaderText="STATUS" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="col-12 col-md-12 col-sm-12 text-end my-3">
                <asp:Button ID="btCadastrarChave" runat="server" Text="Cadastrar" OnClick="btCadastrarChave_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="HdfID" runat="server" />
</asp:Content>
