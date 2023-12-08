<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.Master" AutoEventWireup="true" CodeBehind="Relacionar.aspx.cs" Inherits="Project_GCA_4._0.WebForms.Relacionar" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="PnlRelacionar" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="false">
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
                    <asp:DropDownList ID="DdlRelacionarUsuario" CssClass="col-4 col-md-4 col-sm-4 text-center" runat="server" DataTextField="nomeUsuario" DataValueField="id_usuario"></asp:DropDownList>
                    <asp:DropDownList ID="DdlRelacionarMaquina" CssClass="col-4 col-md-4 col-sm-4 text-center" runat="server" DataTextField="nomeMaquina" DataValueField="id_maquina"></asp:DropDownList>
                </div>
                <div class="row p-0 m-0 gap-4 justify-content-evenly">
                    <asp:Label CssClass="lbRelacionar col-4 col-md-4 col-sm-4 text-center" runat="server" Text="Software"></asp:Label>
                    <asp:Label CssClass="lbRelacionar col-4 col-md-4 col-sm-4 text-center" runat="server" Text="Chave de Ativação"></asp:Label>
                </div>
                <div class="row p-0 m-0 gap-4 justify-content-evenly">
                    <asp:DropDownList ID="DdlRelacionarSoftware" CssClass="col-4 col-md-4 col-sm-4 text-center" runat="server" DataTextField="nomeSoftware" DataValueField="id_software" AutoPostBack="true"></asp:DropDownList>
                    <asp:DropDownList ID="DdlRelacionarChaveAtivacao" CssClass="col-4 col-md-4 col-sm-4 text-center" runat="server" DataTextField="chave" DataValueField="id_chave"></asp:DropDownList>
                </div>
                <div class="row gap-4 mt-5 justify-content-center">
                    <asp:Button ID="SalvarRelacionar" CssClass="col-3 col-md-3 col-sm-3 text-center" runat="server" Text="Salvar" OnClick="SalvarRelacionar_Click" />
                    <asp:Button ID="CancelarRelacionar" CssClass="col-3 col-md-3 col-sm-3  text-center" runat="server" Text="Cancelar" OnClick="CancelarRelacionar_Click" />
                </div>
            </div>
        </section>
    </asp:Panel>

    <asp:Panel ID="PnlConsultarRelacionar" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="true">
        <div class="row">
            <div class="col-12 col-md-12 col-sm-12 my-4 text-uppercase">
                <asp:Label runat="server" Text="Relacionar" CssClass="LbTitulo"></asp:Label>
            </div>
            <div class="col-12 col-md-12 col-sm-12">
                <telerik:RadGrid ID="GridRelacionar" runat="server" AutoGenerateColumns="false" OnNeedDataSource="GridRelacionar_NeedDataSource" OnItemCommand="GridRelacionar_ItemCommand">
                    <GroupingSettings CollapseAllTooltip="collaps all columns" />
                    <MasterTableView DataKeyNames="id_relacionar, id_usuario, id_maquina, id_software, id_chave">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="OP" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:Button ID="btEditar" runat="server" Text="Editar" CommandName="opEditar" />
                                    <asp:Button ID="btexcluir" runat="server" Text="Excluir" CommandName="opExcluir" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="col_CodRelacao" DataField="id_relacionar" HeaderText="COD" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_NomeUsuario" DataField="nomeUsuario" HeaderText="USUARIO" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_NomeMaquina" DataField="nomeMaquina" HeaderText="MAQUINA" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_NomeSoftware" DataField="nomeSoftware" HeaderText="SOFTWARE" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_ChaveAtivacao" DataField="chave" HeaderText="CHAVE DE ATIVAÇÃO" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="col-12 col-md-12 col-sm-12 text-end my-3">
                <asp:Button ID="btCadastroRelacionar" runat="server" Text="Cadastrar" OnClick="btCadastroRelacionar_Click" />
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="HdfID" runat="server" />
</asp:Content>
