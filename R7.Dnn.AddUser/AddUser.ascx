<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="AddUser.ascx.cs" Inherits="R7.Dnn.AddUser.AddUser" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelcontrol.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnCssInclude runat="server" FilePath="~/DesktopModules/R7.Dnn.AddUser/R7.Dnn.AddUser/admin.css" />
<div class="dnnForm dnnClear">
    <fieldset>  
        <div class="dnnFormItem">
        </div>
    </fieldset>
    <ul class="dnnActions dnnClear">
        <li><asp:LinkButton id="buttonAddUser" runat="server" CssClass="dnnPrimaryAction" ResourceKey="buttonAddUser.Text" OnClick="buttonAddUser_Click" CausesValidation="true" /></li>
        <li><asp:HyperLink id="linkCancel" runat="server" CssClass="dnnSecondaryAction" ResourceKey="cmdCancel" /></li>
    </ul>
</div>
