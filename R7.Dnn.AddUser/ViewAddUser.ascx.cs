using System;
using DotNetNuke.Entities.Icons;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Services.Exceptions;
using R7.Dnn.AddUser.Models;
using R7.Dnn.Extensions.Modules;

namespace R7.Dnn.AddUser
{
    public class ViewAddUser: PortalModuleBase<AddUserSettings>, IActionable
    {
        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);

            try {
                ContainerControl.Visible = ModulePermissionController.CanEditModuleContent (ModuleConfiguration);
            }
            catch (Exception ex) {
                Exceptions.ProcessModuleLoadException (this, ex);
            }
        }

        #region IActionable implementation

        public ModuleActionCollection ModuleActions {
            get {
                var actions = new ModuleActionCollection ();
                actions.Add (
                    GetNextActionID (),
                    LocalizeString ("AddUser.Action"),
                    ModuleActionType.AddContent,
                    "",
                    IconController.IconURL ("Add"),
                    EditUrl ("AddUser"),
                    false,
                    SecurityAccessLevel.Edit,
                    true,
                    false
                );

                return actions;
            }
        }

        #endregion
    }
}
