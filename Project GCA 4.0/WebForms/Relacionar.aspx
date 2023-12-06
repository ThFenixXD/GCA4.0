<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.Master" AutoEventWireup="true" CodeBehind="Relacionar.aspx.cs" Inherits="Project_GCA_4._0.WebForms.Relacionar" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="PnlRelacionar" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server">
        <section class="row">
            <div class="col-12 col-md-12 col-sm-12 mx-auto mb-5 text-uppercase">
                <asp:Label ID="Label2" CssClass="LbTitulo" runat="server" Text="Relacionar"></asp:Label>
            </div>
            <div class="DivTextBlock row d-flex m-auto p-0 gap-4">
                <div class="row p-0 m-0 gap-2 justify-content-evenly">
                    <asp:Label CssClass="lbRelacionar col-4 col-md-4 col-sm-4 text-center" runat="server" Text="Usuario"></asp:Label>
                    <asp:Label CssClass="lbRelacionar col-4 col-md-4 col-sm-4 text-center" runat="server" Text="Máquina"></asp:Label>
                </div>
                <div class="row p-0 m-0 gap-4 justify-content-evenly">
                    <asp:DropDownList ID="DdlRelacionarUsuario" CssClass="col-4 col-md-4 col-sm-4 text-center" runat="server" DataTextField="NomeUsuario" DataValueField="ID_Usuario"></asp:DropDownList>
                    <asp:DropDownList ID="DdlRelacionarMaquina" CssClass="col-4 col-md-4 col-sm-4 text-center" runat="server" DataTextField="NomeMaquina" DataValueField="ID_Maquina"></asp:DropDownList>
                </div>
                <div class="row p-0 m-0 gap-4 justify-content-evenly">
                    <asp:Label CssClass="lbRelacionar col-4 col-md-4 col-sm-4 text-center" runat="server" Text="Software"></asp:Label>
                    <asp:Label CssClass="lbRelacionar col-4 col-md-4 col-sm-4 text-center" runat="server" Text="Chave de Ativação"></asp:Label>
                </div>
                <div class="row p-0 m-0 gap-4 justify-content-evenly">
                    <asp:DropDownList ID="DdlRelacionarSoftware" CssClass="col-4 col-md-4 col-sm-4 text-center" runat="server" DataTextField="NomeSoftware" DataValueField="ID_Software" AutoPostBack="true"></asp:DropDownList>
                    <asp:DropDownList ID="DdlRelacionarChaveAtivacao" CssClass="col-4 col-md-4 col-sm-4 text-center" runat="server" DataTextField="ChaveDeAtivacao" DataValueField="ID_ChaveAtivacao"></asp:DropDownList>
                </div>
                <div class="row gap-4 mt-5 justify-content-center">
                    <asp:Button ID="SalvarRelacionar" CssClass="col-3 col-md-3 col-sm-3 text-center" runat="server" Text="Salvar" OnClick="SalvarRelacionar_Click" />
                    <asp:Button ID="CancelarRelacionar" CssClass="col-3 col-md-3 col-sm-3  text-center" runat="server" Text="Cancelar" OnClick="CancelarRelacionar_Click" />
                </div>
            </div>
        </section>
    </asp:Panel>

    <asp:Panel ID="PnlConsultarRelacionar" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="true">
        <telerik:radgrid id="GridRelacionar" runat="server" autogeneratecolumns="false" onneeddatasource="GridRelacionar_NeedDataSource" onitemcommand="GridRelacionar_ItemCommand">
            <GroupingSettings CollapseAllTooltip="collaps all columns" />
            <MasterTableView DataKeyNames="ID_Relacionar">
                <Columns>
                    <telerik:GridTemplateColumn UniqueName="OP" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btEditar" runat="server" Text="Editar" CommandName="opEditar" />
                            <asp:Button ID="btexcluir" runat="server" Text="Excluir" CommandName="opExcluir" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn UniqueName="col_NomeUsuario" DataField="UsuarioRelacionar" HeaderText="USUARIO" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="col_NomeMaquina" DataField="MaquinaRelacionar" HeaderText="MAQUINA" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="col_NomeSoftware" DataField="SoftwareRelacionar" HeaderText="SOFTWARE" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="col_ChaveAtivacao" DataField="ChaveAtivacaoRelacionar" HeaderText="CHAVE DE ATIVAÇÃO" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:radgrid>
    </asp:Panel>
    <asp:HiddenField ID="HdfID" runat="server" />
</asp:Content>
