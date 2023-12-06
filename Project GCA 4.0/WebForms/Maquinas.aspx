<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.Master" AutoEventWireup="true" CodeBehind="Maquinas.aspx.cs" Inherits="Project_GCA_4._0.WebForms.Maquinas" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="PnlCadastroMaquina" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="false">
        <section class="row">
            <div class="col-12 col-md-12 col-sm-12 mb-5 text-uppercase">
                <asp:Label ID="lbMaquinaTitulo" CssClass="LbTitulo" runat="server" Text="Máquina"></asp:Label>
            </div>
            <div class="DivTextBlock row d-flex m-auto gap-2">
                <div class="row">
                    <asp:Label ID="lbNomeMaquina" CssClass="lbTextBlock col-4 col-md-4 col-sm-4 p-0" runat="server" Text="Nome da Máquina"></asp:Label>
                    <asp:TextBox ID="txtNomeMaquina" CssClass="col-8 col-md-8 col-sm-8 p-0" runat="server"></asp:TextBox>
                </div>
                <div class="row">
                    <asp:Label ID="lbSetorMaquina" CssClass="lbTextBlock col-4 col-md-4 col-sm-4" runat="server" Text="Setor"></asp:Label>
                    <asp:DropDownList ID="DdlSetorMaquina" CssClass="col-8 col-md-8 col-sm-8 text-center" runat="server" DataTextField="NomeSetor" DataValueField="ID_Setor"></asp:DropDownList>
                </div>
                <div class="row p-0 m-0 gap-3 justify-content-end">
                    <div class="col-7 col-md-7 col-sm-7"></div>
                    <asp:Button ID="BtSalvarMaquina" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Salvar" OnClick="BtSalvarMaquina_Click" />
                    <asp:Button ID="BtCancelarMaquina" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Cancelar" OnClick="BtCancelarMaquina_Click" />
                </div>
            </div>
        </section>
    </asp:Panel>

    <asp:Panel ID="PnlConsultarMaquinas" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="true">
        <div class="row">
            <div class="col-12 col-md-12 col-sm-12 my-4 text-uppercase">
                <asp:Label runat="server" Text="Máquinas" CssClass="LbTitulo"></asp:Label>
            </div>
            <telerik:RadGrid ID="GridMaquinas" runat="server" AutoGenerateColumns="false" OnNeedDataSource="GridMaquinas_NeedDataSource" OnItemCommand="GridMaquinas_ItemCommand">
                <GroupingSettings CollapseAllTooltip="collaps all columns" />
                <MasterTableView DataKeyNames="ID_Maquina">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="OP" AllowFiltering="false">
                            <ItemTemplate>
                                <asp:Button ID="btEditar" runat="server" Text="Editar" CommandName="opEditar" />
                                <asp:Button ID="btexcluir" runat="server" Text="Excluir" CommandName="opExcluir" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="col_CodMaquina" DataField="ID_Maquina" HeaderText="COD" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="col_NomeMaquina" DataField="NomeMaquina" HeaderText="NOME DA MÁQUINA" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="col_CodSetor" DataField="ID_Setor" HeaderText="COD SETOR" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="col_Setor" DataField="SetorMaquina" HeaderText="SETOR" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="col_Status" DataField="Status" HeaderText="STATUS" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <div class="col-12 col-md-12 col-sm-12 text-end my-3">
                <asp:Button ID="btCadastroMaquina" runat="server" Text="Cadastrar" OnClick="btCadastroMaquina_Click" />
            </div>
        </div>
    </asp:Panel>

    <asp:HiddenField ID="HdfID" runat="server" />
</asp:Content>
