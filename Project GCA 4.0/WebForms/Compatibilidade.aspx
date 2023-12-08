<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.Master" AutoEventWireup="true" CodeBehind="Compatibilidade.aspx.cs" Inherits="Project_GCA_4._0.WebForms.Compatibilidade" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <asp:Panel ID="PnlCadastroCompatibilidade" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="false">
        <section class="row">
            <div class="col-12 col-md-12 col-sm-12 mb-5 text-uppercase">
                <asp:Label ID="lbCompatibilidadeTitulo" CssClass="LbTitulo" runat="server" Text="Compatibilidade"></asp:Label>
            </div>
            <div class="DivTextBlock row d-flex m-auto gap-2">
                <div class="row">
                    <asp:Label ID="lbCompatibilidade" CssClass="lbTextBlock col-4 col-md-4 col-sm-4 p-0" runat="server" Text="Compatibilidade"></asp:Label>
                    <asp:TextBox ID="txtCompatibilidade" CssClass="col-8 col-md-8 col-sm-8" runat="server" DataTextField="compatibilidadeSO" DataValueField="id_compatibilidadeSO"></asp:TextBox>
                </div>
          
                <div class="row p-0 m-0 gap-3 justify-content-end">
                    <div class="col-7 col-md-7 col-sm-7"></div>
                    <asp:Button ID="BtSalvarCompatibilidade" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Salvar" OnClick="BtSalvarCompatibilidade_Click" />
                    <asp:Button ID="BtCancelarCompatibilidade" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Cancelar" OnClick="BtCancelarCompatibilidade_Click" />
                </div>
            </div>
        </section>
    </asp:Panel>

    <asp:Panel ID="PnlConsultarCompatibilidade" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="true">
        <div class="row">
            <div class="col-12 col-md-12 col-sm-12 my-4 text-uppercase">
                <asp:Label runat="server" Text="Compatibilidade" CssClass="LbTitulo"></asp:Label>
            </div>
            <div class="col-12 col-md-12 col-sm-12">
                <telerik:RadGrid ID="GridCompatibilidade" runat="server" AutoGenerateColumns="false" OnNeedDataSource="GridCompatibilidade_NeedDataSource" OnItemCommand="GridCompatibilidade_ItemCommand">
                    <GroupingSettings CollapseAllTooltip="collaps all columns" />
                    <MasterTableView DataKeyNames="id_compatibilidadeSO">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="OP" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:Button ID="btEditar" runat="server" Text="Editar" CommandName="opEditar" />
                                    <asp:Button ID="btexcluir" runat="server" Text="Excluir" CommandName="opExcluir" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="col_id_Compatibilidade" DataField="id_compatibilidadeSO" HeaderText="COD" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_Compatibilidade" DataField="CompatibilidadeSO" HeaderText="COMPATIBILIDADE" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="col-12 col-md-12 col-sm-12 text-end my-3">
                <asp:Button ID="btCadastrarCompatibilidade" runat="server" Text="Cadastrar" OnClick="btCadastrarCompatibilidade_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="HdfID" runat="server" />
</asp:Content>
