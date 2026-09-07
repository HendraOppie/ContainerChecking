Imports System.IO
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Net
Imports AIT.SM

Public Class masterMain
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session(SESSION_FILE_CONNECTION_STRING) Is Nothing Then GotoPage(Me.Page, PAGE_LOGIN)

        If Not IsPostBack Then
            tblSubmenuMaster.Visible = False
            tblSubmenuSetting.Visible = False
            hideSideNav()
        End If

        lbUsernameTop.Text = SessionDataRole.Name & " | " & SessionDataUser().UserName
        lbUsernameLeft.Text = SessionDataUser().UserName

        p_isUserMustChangePassword()
    End Sub

    Private Sub p_isUserMustChangePassword()
        If Session(SESSION_USER_MUST_CHANGE_PASSWORD) IsNot Nothing Then
            ibtHome.Enabled = False
            ibtUserTop.Enabled = False

            ibtUserLeft.Enabled = False
            lbUsernameLeft.Enabled = False
            ibtMaster.Enabled = False
            lbMaster.Enabled = False
            ibtContainerList.Enabled = False
            lbContainer.Enabled = False
            ibtInspection.Enabled = False
            lbInspection.Enabled = False
            ibtTemperature.Enabled = False
            lbTemperature.Enabled = False
            ibtPod.Enabled = False
            lbPod.Enabled = False
            ibtSetting.Enabled = False
            lbSetting.Enabled = False
        End If
    End Sub

    Private Sub ibtLogoAgility_Click(sender As Object, e As ImageClickEventArgs) Handles ibtLogoAgility.Click
        Dim modified_URL As String = "window.open('https://connections.agility.com/', '_blank');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", modified_URL, True)
    End Sub

    Private Sub ibtMenu_Click(sender As Object, e As ImageClickEventArgs) Handles ibtMenu.Click
        showHideSideNav()
    End Sub

    Private Sub ibtSetting_Click(sender As Object, e As ImageClickEventArgs) Handles ibtSetting.Click
        showHideSideNav()
    End Sub

    Private Sub lbSetting_Click(sender As Object, e As EventArgs) Handles lbSetting.Click
        tblSubmenuSetting.Visible = Not tblSubmenuSetting.Visible
        lbUser.Visible = True
        lbRole.Visible = True
    End Sub

    Private Sub ibtHome_Click(sender As Object, e As ImageClickEventArgs) Handles ibtHome.Click
        GotoPage(Me.Page, PAGE_HOME)
    End Sub

    Private Sub ibtContainerList_Click(sender As Object, e As ImageClickEventArgs) Handles ibtContainerList.Click
        GotoPage(Me.Page, PAGE_CONTAINERS, True)
    End Sub

    Private Sub showHideSideNav()
        If Not divSideNav.Style("Width") = "50px" Then
            hideSideNav()
        Else
            showSideNav()
        End If
    End Sub

    Private Sub showSideNav()
        If Not divSideNav.Style("Width") = "230px" Then
            divSideNav.Style("Width") = "230px"
            divPlaceHolder.Style("Left") = "250px"

            lbUsernameLeft.Visible = True
            lbMaster.Visible = True
            lbContainer.Visible = True
            lbInspection.Visible = True
            lbTemperature.Visible = True
            lbPod.Visible = True
            lbSetting.Visible = True
            lbReport.Visible = True
            lbHelp.Visible = True
            lbLogout.Visible = True
        End If
    End Sub

    Private Sub hideSideNav()
        divSideNav.Style("Width") = "50px"
        divPlaceHolder.Style("Left") = "70px"
        divPlaceHolder.Style("Width") = "100%"

        lbUsernameLeft.Visible = False
        lbMaster.Visible = False
        lbContainer.Visible = False
        lbInspection.Visible = False
        lbTemperature.Visible = False
        lbPod.Visible = False
        lbSetting.Visible = False
        lbLocation.Visible = False
        lbCustomer.Visible = False
        lbReport.Visible = False
        lbHelp.Visible = False
        lbLogout.Visible = False
        tblSubmenuMaster.Visible = False
        tblSubmenuSetting.Visible = False

    End Sub

    Private Function getDataRoleItem(sFormId As String) As RoleManager.DataRoleItem
        Dim dataRole As New RoleManager.DataRole
        dataRole = Session(SESSION_DATA_ROLE)

        For Each dataRoleItem As RoleManager.DataRoleItem In dataRole.alItem
            If dataRoleItem.FormId = sFormId Then
                Return dataRoleItem
            End If
        Next

        Return New RoleManager.DataRoleItem
    End Function

    Private Sub lbContainer_Click(sender As Object, e As EventArgs) Handles lbContainer.Click
        GotoPage(Me.Page, PAGE_CONTAINERS, True)
    End Sub

    Private Sub ibtLogout_Click(sender As Object, e As ImageClickEventArgs) Handles ibtLogout.Click
        GotoPage(Me.Page, PAGE_LOGIN)
    End Sub

    Private Sub ibtUserTop_Click(sender As Object, e As ImageClickEventArgs) Handles ibtUserTop.Click
        GotoPage(Me.Page, PAGE_USER_PROFILE)
    End Sub

    Private Sub ibtUserLeft_Click(sender As Object, e As ImageClickEventArgs) Handles ibtUserLeft.Click
        GotoPage(Me.Page, PAGE_USER_PROFILE)
    End Sub

    Private Sub lbUsernameLeft_Click(sender As Object, e As EventArgs) Handles lbUsernameLeft.Click
        GotoPage(Me.Page, PAGE_USER_PROFILE)
    End Sub

    Private Sub ibtInspection_Click(sender As Object, e As ImageClickEventArgs) Handles ibtInspection.Click
        GotoPage(Me.Page, PAGE_PHYSICAL_INSPECTION, True)
    End Sub

    Private Sub lbInspection_Click(sender As Object, e As EventArgs) Handles lbInspection.Click
        GotoPage(Me.Page, PAGE_PHYSICAL_INSPECTION, True)
    End Sub

    Private Sub ibtTemperature_Click(sender As Object, e As ImageClickEventArgs) Handles ibtTemperature.Click
        GotoPage(Me.Page, PAGE_TEMPERATURE_LOG, True)
    End Sub

    Private Sub lbTemperature_Click(sender As Object, e As EventArgs) Handles lbTemperature.Click
        GotoPage(Me.Page, PAGE_TEMPERATURE_LOG, True)
    End Sub

    Private Sub ibtPod_Click(sender As Object, e As ImageClickEventArgs) Handles ibtPod.Click
        GotoPage(Me.Page, PAGE_POD, True)
    End Sub

    Private Sub lbPod_Click(sender As Object, e As EventArgs) Handles lbPod.Click
        GotoPage(Me.Page, PAGE_POD, True)
    End Sub

    Private Sub lbLocation_Click(sender As Object, e As EventArgs) Handles lbLocation.Click
        GotoPage(Me.Page, PAGE_LOCATION, True)
    End Sub

    Private Sub p_openUserGuide()
        Dim FilePath As String = Server.MapPath(FILEPATH_PDF_USERGUIDE)
        Response.ContentType = "application/pdf"
        HttpContext.Current.Response.AddHeader("Content-Disposition", "inline; filename=" + FilePath)
        Dim FileBuffer As Byte() = New WebClient().DownloadData(FilePath)
        Response.BinaryWrite(FileBuffer)
    End Sub

    Private Sub ibtHelp_Click(sender As Object, e As ImageClickEventArgs) Handles ibtHelp.Click
        p_openUserGuide()
    End Sub

    Private Sub lbHelp_Click(sender As Object, e As EventArgs) Handles lbHelp.Click
        p_openUserGuide()
    End Sub

    Private Sub lbUser_Click(sender As Object, e As EventArgs) Handles lbUser.Click
        GotoPage(Me.Page, PAGE_USER, True)
    End Sub

    Private Sub lbRole_Click(sender As Object, e As EventArgs) Handles lbRole.Click
        GotoPage(Me.Page, PAGE_ROLE, True)
    End Sub

    Private Sub ibtMaster_Click(sender As Object, e As ImageClickEventArgs) Handles ibtMaster.Click
        showHideSideNav()
    End Sub

    Private Sub lbMaster_Click(sender As Object, e As EventArgs) Handles lbMaster.Click
        tblSubmenuMaster.Visible = Not tblSubmenuMaster.Visible
        lbLocation.Visible = True
        lbCustomer.Visible = True
    End Sub

    Private Sub lbCustomer_Click(sender As Object, e As EventArgs) Handles lbCustomer.Click
        GotoPage(Me.Page, PAGE_CUSTOMER, True)
    End Sub

    Private Sub lbReport_Click(sender As Object, e As EventArgs) Handles lbReport.Click
        'Response.Redirect(URL_REPORTALWEB)
        GotoPage(Me.Page, PAGE_REPORT_VIEWER)
    End Sub

    Private Sub ibtReport_Click(sender As Object, e As ImageClickEventArgs) Handles ibtReport.Click
        'Response.Redirect(URL_REPORTALWEB)
        GotoPage(Me.Page, PAGE_REPORT_VIEWER)
    End Sub

    Private Sub lbLogout_Click(sender As Object, e As EventArgs) Handles lbLogout.Click
        GotoPage(Me.Page, PAGE_LOGIN)
    End Sub

End Class