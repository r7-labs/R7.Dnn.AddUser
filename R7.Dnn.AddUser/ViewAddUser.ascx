<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewAddUser.ascx.cs" Inherits="R7.Dnn.AddUser.ViewAddUser" %>
<div class="r7-dnn-add-user">
    <a href='<%: EditUrl ("AddUser") %>' class="btn btn-primary btn-block" role="button">
	    <span class="fa fa-user-plus"></span> <%: LocalizeString ("AddUser.Action") %>
	</a>
</div>