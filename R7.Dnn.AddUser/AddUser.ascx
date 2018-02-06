<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AddUser.ascx.cs" Inherits="R7.Dnn.AddUser.AddUser" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/R7.Dnn.AddUser/R7.Dnn.AddUser/admin.css" />
<asp:Panel id="panelUserInfo" runat="server" CssClass="dnnForm dnnClear">
    <fieldset>  
        <div class="dnnFormItem dnnFormRequired">
            <dnn:Label id="labelLastName" runat="server" ControlName="textLastName" />
            <asp:TextBox id="textLastName" runat="server" />
        </div>
		<div class="dnnFormItem dnnFormRequired">
			<dnn:Label id="labelFirstName" runat="server" ControlName="textFirstName" />
			<asp:TextBox id="textFirstName" runat="server" />
        </div>
		<div class="dnnFormItem">
            <dnn:Label id="labelOtherName" runat="server" ControlName="textOtherName" />
            <asp:TextBox id="textOtherName" runat="server" />
        </div>
		<div class="dnnFormItem dnnFormRequired">
            <dnn:Label id="labelEmail" runat="server" ControlName="textEmail" />
            <asp:TextBox id="textEmail" runat="server" />
        </div>
    </fieldset>
    <ul class="dnnActions dnnClear">
        <li><asp:LinkButton id="buttonAddUser" runat="server" CssClass="dnnPrimaryAction" ResourceKey="buttonAddUser.Text" OnClick="buttonAddUser_Click" CausesValidation="true" /></li>
        <li><asp:HyperLink id="linkCancel" runat="server" CssClass="dnnSecondaryAction" ResourceKey="cmdCancel" /></li>
    </ul>
</asp:Panel>
<asp:Panel id="panelUserAdded" runat="server" CssClass="dnnForm dnnClear" Visible="false">
    <fieldset>
		<div class="dnnFormItem">
            <dnn:Label id="labelLogin" runat="server" ControlName="textLogin" />
            <asp:TextBox id="textLogin" runat="server" ReadOnly="true" />
        </div>
        <div class="dnnFormItem">
            <dnn:Label id="labelPassword" runat="server" ControlName="textPassword" />
            <asp:TextBox id="textPassword" runat="server" ReadOnly="true" />
        </div>
    </fieldset>
    <ul class="dnnActions dnnClear">
        <li><asp:HyperLink id="linkDone" runat="server" CssClass="dnnPrimaryAction" ResourceKey="linkDone.Text" /></li>
    </ul>
</asp:Panel>

