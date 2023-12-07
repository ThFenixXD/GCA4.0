<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.Master" AutoEventWireup="true" CodeBehind="Idioma.aspx.cs" Inherits="Project_GCA_4._0.WebForms.Idioma" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="PnlCadastroIdioma" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="false">
        <section class="row">
            <div class="col-12 col-md-12 col-sm-12 mb-5 text-uppercase">
                <asp:Label ID="lbIdiomaTitulo" CssClass="LbTitulo" runat="server" Text="Idioma"></asp:Label>
            </div>
            <div class="DivTextBlock row d-flex m-auto gap-2">
                <div class="row">
                    <asp:Label ID="lbIdioma" CssClass="lbTextBlock col-4 col-md-4 col-sm-4" runat="server" Text="Idioma"></asp:Label>
                    <asp:TextBox ID="txtIdioma" CssClass="col-8 col-md-8 col-sm-8" runat="server"></asp:TextBox>
                </div>
                <div class="row">
                    <asp:Label ID="lbSigla" CssClass="lbTextBlock col-4 col-md-4 col-sm-4" runat="server" Text="Sigla"></asp:Label>
                    <asp:TextBox ID="txtSigla" CssClass="col-8 col-md-8 col-sm-8" runat="server"></asp:TextBox>
                </div>
                <div class="row">
                    <asp:Label ID="lbPaisOrigem" CssClass="lbTextBlock col-4 col-md-4 col-sm-4" runat="server" Text="Pais Origem"></asp:Label>
                    <asp:TextBox ID="txtPaisOrigem" CssClass="col-8 col-md-8 col-sm-8" runat="server"></asp:TextBox>
                </div>
                <div class="row p-0 m-0 gap-3 justify-content-end">
                    <div class="col-7 col-md-7 col-sm-7"></div>
                    <asp:Button ID="BtSalvarIdioma" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Salvar" OnClick="BtSalvarIdioma_Click" />
                    <asp:Button ID="BtCancelarIdioma" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Cancelar" OnClick="BtCancelarIdioma_Click" />
                </div>
            </div>
        </section>
    </asp:Panel>

    <asp:Panel ID="PnlConsultarIdiomas" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="true">
        <div class="row">
            <div class="col-12 col-md-12 col-sm-12 my-4 text-uppercase">
                <asp:Label runat="server" Text="Idiomas" CssClass="LbTitulo"></asp:Label>
            </div>
            <div class="col-12 col-md-12 col-sm-12">
                <telerik:RadGrid ID="GridIdiomas" runat="server" AutoGenerateColumns="false" OnNeedDataSource="GridIdiomas_NeedDataSource" OnItemCommand="GridIdiomas_ItemCommand">
                    <GroupingSettings CollapseAllTooltip="collaps all columns" />
                    <MasterTableView DataKeyNames="id_idioma">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="OP" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:Button ID="btEditar" runat="server" Text="Editar" CommandName="opEditar" />
                                    <asp:Button ID="btexcluir" runat="server" Text="Excluir" CommandName="opExcluir" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="col_CodIdioma" DataField="id_idioma" HeaderText="COD" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_Idioma" DataField="idioma" HeaderText="IDIOMA" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_Sigla" DataField="sigla" HeaderText="SIGLA" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_PaisOrigem" DataField="pais" HeaderText="PAÍS ORIGEM" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="col-12 col-md-12 col-sm-12 text-end my-3">
                <asp:Button ID="btCadastrarIdioma" runat="server" Text="Cadastrar" OnClick="btCadastrarIdioma_Click" />
            </div>
        </div>
    </asp:Panel>

    <asp:HiddenField ID="HdfID" runat="server" />
</asp:Content>
