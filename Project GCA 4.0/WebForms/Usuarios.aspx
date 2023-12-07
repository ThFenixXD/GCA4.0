<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Project_GCA_4._0.WebForms.Usuarios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Panel ID="PnlCadastroUsuario" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="false">
        <section class="row">
            <div class="col-12 col-md-12 col-sm-12 mb-5 text-uppercase">
                <asp:Label ID="lbUsuarioTitulo" CssClass="LbTitulo" runat="server" Text="Usuário"></asp:Label>
            </div>
            <div class="DivTextBlock row d-flex m-auto gap-2">
                <div class="row">
                    <asp:Label ID="lbNomeUsuario" CssClass="lbTextBlock col-4 col-md-4 col-sm-4 p-0 m-0" runat="server" Text="Nome do Usuário"></asp:Label>
                    <asp:TextBox ID="txtNomeUsuario" CssClass="txtTextBlock col-8 col-md-8 col-sm-8" runat="server"></asp:TextBox>
                </div>
                <div class="row">
                    <asp:Label ID="lbFuncao" CssClass="lbTextBlock col-4 col-md-4 col-sm-4" runat="server" Text="Função"></asp:Label>
                    <asp:TextBox ID="txtFuncaoUsuario" CssClass="col-8 col-md-8 col-sm-8" runat="server"></asp:TextBox>
                </div>
                <div class="row">
                    <asp:Label ID="lbSetorUsuario" CssClass="lbTextBlock col-4 col-md-4 col-sm-4" runat="server" Text="Setor"></asp:Label>
                    <asp:DropDownList ID="ddlSetorUsuario" CssClass="col-8 col-md-8 col-sm-8 text-center" runat="server" DataTextField="NomeSetor" DataValueField="ID_Setor"></asp:DropDownList>
                </div>
                <div class="row p-0 m-0 gap-3">
                    <div class="col-7 col-md-7 col-sm-7"></div>
                    <asp:Button ID="BtSalvarUsuario" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Salvar" OnClick="BtSalvarUsuario_Click" />
                    <asp:Button ID="BtCancelarUsuario" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Cancelar" OnClick="BtCancelarUsuario_Click" />
                </div>
            </div>
        </section>
    </asp:Panel>

    <asp:Panel ID="PnlConsultarUsuarios" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="true">
        <div class="row">
            <div class="col-12 col-md-12 col-sm-12 my-4 text-uppercase">
                <asp:Label runat="server" Text="Usuários" CssClass="LbTitulo"></asp:Label>
            </div>
            <div class="col-12 col-md-12 col-sm-12">
                <telerik:RadGrid ID="GridUsuarios" runat="server" AutoGenerateColumns="false" OnNeedDataSource="GridUsuarios_NeedDataSource" OnItemCommand="GridUsuarios_ItemCommand">
                    <GroupingSettings CollapseAllTooltip="collaps all columns" />
                    <MasterTableView DataKeyNames="id_usuario, id_setor ">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="OP" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:Button ID="btEditar" runat="server" Text="Editar" CommandName="opEditar" />
                                    <asp:Button ID="btexcluir" runat="server" Text="Excluir" CommandName="opExcluir" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="col_CodUsuario" DataField="id_usuario" HeaderText="COD USUARIO" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_NomeUsuario" DataField="nomeUsuario" HeaderText="NOME USUÁRIO" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_Funcao" DataField="funcaoUsuario" HeaderText="FUNÇÃO" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_CodSetor" DataField="id_setor" HeaderText="COD SETOR" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_NomeSetor" DataField="nomeSetor" HeaderText="NOME SETOR" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="col-12 col-md-12 col-sm-12 text-end my-3">
                <asp:Button ID="btCadastroUsuarios" runat="server" Text="Cadastrar" OnClick="btCadastroUsuarios_Click1" />
            </div>
        </div>

    </asp:Panel>

    <asp:HiddenField ID="HdfID" runat="server" />
</asp:Content>
