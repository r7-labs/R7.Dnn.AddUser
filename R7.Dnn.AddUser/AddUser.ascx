<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AddUser.ascx.cs" Inherits="R7.Dnn.AddUser.AddUser" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/R7.Dnn.AddUser/R7.Dnn.AddUser/admin.css" />
<asp:Panel id="panelUserInfo" runat="server" CssClass="dnnForm dnnClear r7-add-user">
    <fieldset>  
        <div class="dnnFormItem dnnFormRequired">
            <dnn:Label id="labelLastName" runat="server" ControlName="textLastName" />
            <asp:TextBox id="textLastName" runat="server" MaxLength="50" />
			<asp:RequiredFieldValidator runat="server" resourcekey="LastName_Required.Text"
				ControlToValidate="textLastName"
				ValidationGroup="AddUser"
				Display="Dynamic"
			    CssClass="dnnFormMessage dnnFormError" />
        </div>
		<div class="dnnFormItem dnnFormRequired">
			<dnn:Label id="labelFirstName" runat="server" ControlName="textFirstName" />
			<asp:TextBox id="textFirstName" runat="server" MaxLength="50" />
            <asp:RequiredFieldValidator runat="server" resourcekey="FirstName_Required.Text"
                ControlToValidate="textFirstName"
                ValidationGroup="AddUser"
                Display="Dynamic"
                CssClass="dnnFormMessage dnnFormError" />
        </div>
		<div class="dnnFormItem">
            <dnn:Label id="labelOtherName" runat="server" ControlName="textOtherName" />
            <asp:TextBox id="textOtherName" runat="server" MaxLength="50" />
        </div>
		<div class="dnnFormItem dnnFormRequired">
            <dnn:Label id="labelEmail" runat="server" ControlName="textEmail" />
            <asp:TextBox id="textEmail" runat="server" MaxLength="128" />
			<asp:RequiredFieldValidator runat="server" resourcekey="Email_Required.Text"
                ControlToValidate="textEmail"
                ValidationGroup="AddUser"
                Display="Dynamic"
				CssClass="dnnFormMessage dnnFormError" />
            <asp:RegularExpressionValidator runat="server" resourcekey="Email_Invalid.Text"
                ControlToValidate="textEmail"
				ValidationGroup="AddUser"
                Display="Dynamic"
				CssClass="dnnFormMessage dnnFormError" 
                ValidationExpression="^\s*[a-zA-Z0-9_%+#&amp;'*/=^`{|}~-](?:\.?[a-zA-Z0-9_%+#&amp;'*/=^`{|}~-])*@(?:[a-zA-Z0-9_](?:(?:\.?|-*)[a-zA-Z0-9_])*\.[a-zA-Z]{2,9}|\[(?:2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?:2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?:2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?:2[0-4]\d|25[0-5]|[01]?\d\d?)])\s*$">
            </asp:RegularExpressionValidator>
	    </div>
    </fieldset>
    <ul class="dnnActions dnnClear">
        <li><asp:LinkButton id="buttonAddUser" runat="server" CssClass="dnnPrimaryAction" ResourceKey="buttonAddUser.Text" OnClick="buttonAddUser_Click" CausesValidation="true" ValidationGroup="AddUser" /></li>
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

