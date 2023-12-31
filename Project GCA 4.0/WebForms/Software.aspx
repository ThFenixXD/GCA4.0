﻿<%@ Page Title="" Language="C#" MasterPageFile="~/WebForms/MasterPage.Master" AutoEventWireup="true" CodeBehind="Software.aspx.cs" Inherits="Project_GCA_4._0.WebForms.Software" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="PnlCadastroSoftware" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="false" ValidationGroup="GrupoCadastroSoftware">

        <section class="row">
            <div class="col-12 col-md-12 col-sm-12 mb-5 text-uppercase">
                <asp:Label ID="lbSoftwareTitulo" CssClass="LbTitulo" runat="server" Text="Software"></asp:Label>
            </div>
            <div class="DivTextBlock row d-flex m-auto gap-2">
                <div class="row">
                    <asp:Label ID="lbNomeSoftware" CssClass="lbTextBlock col-4 col-md-4 col-sm-4 p-0" runat="server" Text="Nome do Software"></asp:Label>
                    <asp:TextBox ID="txtNomeSoftware" CssClass="col-8 col-md-8 col-sm-8" runat="server" PlaceHolder="Nome do Programa"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNomeSoftware" CssClass="RequiredField" ControlToValidate="txtNomeSoftware" runat="server" ErrorMessage="Campo Invalido!" AutoPostBack="true"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <asp:Label ID="lbVersao" CssClass="lbTextBlock col-4 col-md-4 col-sm-4 p-0" runat="server" Text="Versão"></asp:Label>
                    <asp:TextBox ID="txtVersão" CssClass="col-8 col-md-8 col-sm-8" runat="server" PlaceHolder="Versão 1.0"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvVersao" CssClass="RequiredField" ControlToValidate="txtVersão" runat="server" ErrorMessage="Campo Invalido!" AutoPostBack="true"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <asp:Label ID="lbAno" CssClass="lbTextBlock col-4 col-md-4 col-sm-4 p-0" runat="server" Text="Ano"></asp:Label>
                    <asp:TextBox ID="txtAno" CssClass="col-8 col-md-8 col-sm-8" runat="server" PlaceHolder="XXXX"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revAno" CssClass="req" ControlToValidate="txtAno" runat="server" ErrorMessage="Campo Invalido!" ValidationExpression="\d{4}" AutoPostBack="true"></asp:RegularExpressionValidator>
                </div>
                <div class="row">
                    <asp:Label ID="lbFabricante" CssClass="lbTextBlock col-4 col-md-4 col-sm-4" runat="server" Text="Fabricante" PlaceHolder="Empresa X"></asp:Label>
                    <asp:DropDownList ID="ddlFabricante" CssClass="col-8 col-md-8 col-sm-8 text-center" runat="server" DataTextField="nomeFabricante" DataValueField="id_fabricante"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvFabricante" CssClass="RequiredField" ControlToValidate="ddlFabricante" runat="server" ErrorMessage="Campo Invalido!" AutoPostBack="true"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <asp:Label ID="lbIdioma" CssClass="lbTextBlock col-4 col-md-4 col-sm-4" runat="server" Text="Idioma"></asp:Label>
                    <asp:DropDownList ID="ddlIdioma" CssClass="col-8 col-md-8 col-sm-8 text-center" runat="server" DataTextField="idioma" DataValueField="id_idioma"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvIdioma" CssClass="RequiredField" ControlToValidate="ddlIdioma" runat="server" ErrorMessage="Campo Invalido!" AutoPostBack="true"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <asp:Label ID="lbTecnologia" CssClass="lbTextBlock col-4 col-md-4 col-sm-4 p-0" runat="server" Text="Tecnologia"></asp:Label>
                    <asp:DropDownList ID="ddlTecnologia" CssClass="col-8 col-md-8 col-sm-8 text-center" runat="server" DataTextField="Tecnologia" DataValueField="ID_Tecnologia"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvTecnologia" CssClass="RequiredField" ControlToValidate="ddlTecnologia" runat="server" ErrorMessage="Campo Invalido!" AutoPostBack="true"></asp:RequiredFieldValidator>
                </div>
                <div class="row">
                    <asp:Label ID="lbCompatibilidade" CssClass="lbTextBlock col-4 col-md-4 col-sm-4" runat="server" Text="Compatibilidade"></asp:Label>
                    <asp:DropDownList ID="ddlCompatibilidade" CssClass="col-8 col-md-8 col-sm-8 text-center" runat="server" DataTextField="compatibilidadeSO" DataValueField="id_compatibilidadeSO"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvCompatibilidade" CssClass="RequiredField" ControlToValidate="ddlCompatibilidade" runat="server" ErrorMessage="Campo Invalido!" AutoPostBack="true"></asp:RequiredFieldValidator>
                </div>
                <div class="row p-0 m-0 gap-3 justify-content-end">
                    <div class="col-7 col-md-7 col-sm-7"></div>
                    <asp:Button ID="BtSalvarSoftware" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Salvar" OnClick="BtSalvarSoftware_Click" />
                    <asp:Button ID="BtCancelarSoftware" CssClass="col-2 col-md-2 col-sm-2" runat="server" Text="Cancelar" OnClick="BtCancelarSoftware_Click" />
                </div>
            </div>
        </section>
    </asp:Panel>

    <asp:Panel ID="PnlConsultarSoftware" CssClass="Pnl col-9 col-md-9 col-sm-9 d-flex align-items-center justify-content-center text-center" runat="server" Visible="true">
        <div class="row">
            <div class="col-12 col-md-12 col-sm-12 my-4 text-uppercase">
                <asp:Label runat="server" Text="Software" CssClass="LbTitulo"></asp:Label>
            </div>
            <div class="col-12 col-md-12 col-sm-12">
                <telerik:RadGrid ID="GridSoftware" runat="server" AutoGenerateColumns="false" OnNeedDataSource="GridSoftware_NeedDataSource" OnItemCommand="GridSoftware_ItemCommand">
                    <GroupingSettings CollapseAllTooltip="collaps all columns" />
                    <MasterTableView DataKeyNames="id_software">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="OP" AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:Button ID="btEditar" runat="server" Text="Editar" CommandName="opEditar" />
                                    <asp:Button ID="btexcluir" runat="server" Text="Excluir" CommandName="opExcluir" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="col_CodSoftware" DataField="id_software" HeaderText="COD" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_Software" DataField="nomeSoftware" HeaderText="SOFTWARE" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_Versao" DataField="versao" HeaderText="VERSÃO" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_Ano" DataField="ano" HeaderText="ANO" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_Fabricante" DataField="nomeFabricante" HeaderText="FABRICANTE" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_Idioma" DataField="idioma" HeaderText="IDIOMA" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_Tecnologia" DataField="Tecnologia" HeaderText="TECNOLOGIA" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="col_Compatibilidade" DataField="CompatibilidadeSO" HeaderText="COMPATIBILIDADE" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="col-12 col-md-12 col-sm-12 text-end my-3">
                <asp:Button ID="btCadastrarSoftware" runat="server" Text="Cadastrar" OnClick="btCadastrarSoftware_Click" />
            </div>
        </div>
    </asp:Panel>

    <asp:HiddenField ID="HdfID" runat="server" />
</asp:Content>
