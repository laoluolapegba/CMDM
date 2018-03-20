<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="CMdm.UI.Web.Views.Shared.Menu" %>
<%@ Import Namespace="Microsoft.AspNet.Identity" %>
<%@ Import Namespace="CMdm.Data" %>
<%@ Import Namespace="CMdm.Data.Rbac" %>
<%@ Import Namespace="CMdm.Entities.ViewModels" %>
<%@ Import Namespace="CMdm.UI.Web.Helpers.CrossCutting.Security" %>

 <%
     List<MenuViewModel> menuList = null;
     var _db = new AppDbContext();
     if (HttpContext.Current.Request.IsAuthenticated)
     {
         var identity = ((CustomPrincipal)HttpContext.Current.User).CustomIdentity;
         var menus = (from p in _db.CM_PERMISSIONS
                      join x in _db.CM_ROLE_PERM_XREF on p.PERMISSION_ID equals x.PERMISSION_ID
                      where x.ROLE_ID == identity.UserRoleId
                      where p.ISACTIVE == true
                      select new
                      {
                          PermissionId = p.PERMISSION_ID,
                          RoleId = x.ROLE_ID,
                          PermissionDesc = p.PERMISSIONDESCRIPTION,
                          ActionName = p.ACTION_NAME,
                          ControllerName = p.CONTROLLER_NAME,
                          ParentPermission = p.PARENT_PERMISSION,
                          IconClass = p.ICON_CLASS,
                          IsopenClass = p.ISOPEN_CLASS,
                          ToggleIconClass = p.TOGGLE_ICON,
                          Url = p.FORM_URL

                      }).AsEnumerable().OrderBy(a=>a.PermissionId);


         var menuData = menus.Select(o => new MenuViewModel
         {
             PermissionId = o.PermissionId,
             RoleId = o.RoleId,
             PermissionDesc = o.PermissionDesc,
             ActionName = o.ActionName,
             ControllerName = o.ControllerName,
             ParentPermission = o.ParentPermission,
             IconClass = o.IconClass,
             IsopenClass = o.IsopenClass,
             ToggleIconClass = o.ToggleIconClass,
             Url = o.Url
         }).ToList();


         //var menuList = menuData as IEnumerable<MenuViewModel>;
         menuList = menuData;
     }
        %>

    <ul class="sidebar-menu">
        <li class="header">MAIN NAVIGATION</li>
        <%
            if (HttpContext.Current.Request.IsAuthenticated)
            { 
            for (int i = 0; i < menuList.Count; i++)
            {
                if (menuList[i].ParentPermission == 0)
                {%>
                        <li class="treeview">
                            <a href="<%=ResolveUrl(menuList[i].Url)%>">
                                <i class="fa <%:menuList[i].IconClass%>"></i> <span><%:menuList[i].PermissionDesc%></span>
                                <i class="toggle-icon fa fa-angle-left pull-right"></i>
                            </a>
                            <ul class="treeview-menu">

                                <%for (int j = 0; j < menuList.Count; j++)
                                    {
                                        if (menuList[j].ParentPermission == menuList[i].PermissionId)
                                        {%>
                                        <li>
                                            <a href="<%=ResolveUrl(menuList[j].Url)%>" >
                                                <i class="fa fa-circle-o"></i> <span><%:menuList[j].PermissionDesc%></span>
                                                <%--<i class="fa fa-angle-left pull-right"></i>--%>
                                            </a>
                                            <%--<ul class="treeview-menu">
                                                <%for (int k = 0; k < menuList.Count; k++)
                                                    {
                                                        if (menuList[k].ParentPermission == menuList[j].PermissionId)
                                                        {%>
                                                        <li>
                                                            <a href="<%=ResolveUrl(menuList[k].Url)%>">
                                                                <i class="fa fa-circle-o"></i>
                                                                <%:menuList[k].PermissionDesc%>
                                                            </a>
                                                        </li>
                                                   <% }
                                                       }
                                                %>
                                            </ul>--%>
                                        </li>
                                  <%  }
                                      }
                                %>

                            </ul>
                        </li>
              <%      }
                      }
                  }
            %>

        </ul>
