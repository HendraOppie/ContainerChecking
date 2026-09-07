Imports System.Web.SessionState
Imports System.Web.Routing

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        RegisterRoutes(RouteTable.Routes)
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

    Private Shared Sub RegisterRoutes(routes As RouteCollection)
        routes.MapPageRoute(PAGE_LOGIN, PAGE_LOGIN, "~/" & PAGE_LOGIN & ".aspx")
        routes.MapPageRoute(PAGE_SIGNUP, PAGE_SIGNUP, "~/" & PAGE_SIGNUP & ".aspx")
        routes.MapPageRoute(PAGE_REQUEST_RESET_PASSWORD, PAGE_REQUEST_RESET_PASSWORD, "~/" & PAGE_REQUEST_RESET_PASSWORD & ".aspx")
        routes.MapPageRoute(PAGE_USER_PROFILE, PAGE_USER_PROFILE, "~/" & PAGE_USER_PROFILE & ".aspx")
        routes.MapPageRoute(PAGE_USER_PROFILE_MOBILE, PAGE_USER_PROFILE_MOBILE, "~/" & PAGE_USER_PROFILE_MOBILE & ".aspx")
        routes.MapPageRoute(PAGE_USER, PAGE_USER, "~/" & PAGE_USER & ".aspx")
        routes.MapPageRoute(PAGE_ROLE, PAGE_ROLE, "~/" & PAGE_ROLE & ".aspx")
        routes.MapPageRoute(PAGE_MENU, PAGE_MENU, "~/" & PAGE_MENU & ".aspx")
        routes.MapPageRoute(PAGE_HOME, PAGE_HOME, "~/" & PAGE_HOME & ".aspx")
        routes.MapPageRoute(PAGE_CONTAINERS, PAGE_CONTAINERS, "~/" & PAGE_CONTAINERS & ".aspx")
        routes.MapPageRoute(PAGE_PHYSICAL_INSPECTION, PAGE_PHYSICAL_INSPECTION, "~/" & PAGE_PHYSICAL_INSPECTION & ".aspx")
        routes.MapPageRoute(PAGE_PHYSICAL_INSPECTION_MOBILE, PAGE_PHYSICAL_INSPECTION_MOBILE, "~/" & PAGE_PHYSICAL_INSPECTION_MOBILE & ".aspx")
        routes.MapPageRoute(PAGE_TEMPERATURE_LOG, PAGE_TEMPERATURE_LOG, "~/" & PAGE_TEMPERATURE_LOG & ".aspx")
        routes.MapPageRoute(PAGE_TEMPERATURE_LOG_MOBILE, PAGE_TEMPERATURE_LOG_MOBILE, "~/" & PAGE_TEMPERATURE_LOG_MOBILE & ".aspx")
        routes.MapPageRoute(PAGE_PHYSICAL_INSPECTION_ITEM_MOBILE, PAGE_PHYSICAL_INSPECTION_ITEM_MOBILE, "~/" & PAGE_PHYSICAL_INSPECTION_ITEM_MOBILE & ".aspx")
        routes.MapPageRoute(PAGE_DELIVERY_NOTE_MOBILE, PAGE_DELIVERY_NOTE_MOBILE, "~/" & PAGE_DELIVERY_NOTE_MOBILE & ".aspx")
        routes.MapPageRoute(PAGE_POD, PAGE_POD, "~/" & PAGE_POD & ".aspx")
        routes.MapPageRoute(PAGE_POD_MOBILE, PAGE_POD_MOBILE, "~/" & PAGE_POD_MOBILE & ".aspx")
        'routes.MapPageRoute(PAGE_POD_MOBILE, PAGE_POD_MOBILE & "/{id}", "~/" & PAGE_POD_MOBILE & ".aspx")
        routes.MapPageRoute(PAGE_LOCATION, PAGE_LOCATION, "~/" & PAGE_LOCATION & ".aspx")
        routes.MapPageRoute(PAGE_CUSTOMER, PAGE_CUSTOMER, "~/" & PAGE_CUSTOMER & ".aspx")
        routes.MapPageRoute(PAGE_INFO, PAGE_INFO, "~/" & PAGE_INFO & ".aspx")
        routes.MapPageRoute(PAGE_INFO_MOBILE, PAGE_INFO_MOBILE, "~/" & PAGE_INFO_MOBILE & ".aspx")
        routes.MapPageRoute(PAGE_THANK_YOU_MOBILE, PAGE_THANK_YOU_MOBILE, "~/" & PAGE_THANK_YOU_MOBILE & ".aspx")
        routes.MapPageRoute(PAGE_REPORT_VIEWER, PAGE_REPORT_VIEWER, "~/" & PAGE_REPORT_VIEWER & ".aspx")

    End Sub

End Class