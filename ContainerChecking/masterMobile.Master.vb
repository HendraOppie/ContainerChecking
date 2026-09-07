Imports AIT.SM

Public Class masterMobile
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session(SESSION_FILE_CONNECTION_STRING) Is Nothing Then GotoPage(Me.Page, PAGE_LOGIN)
        lbUsername.Text = SessionDataUser().UserName
    End Sub

    Private Sub p_isUserMustChangePassword()
        If Session(SESSION_USER_MUST_CHANGE_PASSWORD) IsNot Nothing Then
            ibtHome.Enabled = False
            ibtUser.Enabled = False
        End If
    End Sub

    Private Sub ibtHome_Click(sender As Object, e As ImageClickEventArgs) Handles ibtHome.Click
        GotoPage(Me.Page, PAGE_MENU)
    End Sub

    Private Sub ibtUser_Click(sender As Object, e As ImageClickEventArgs) Handles ibtUser.Click
        GotoPage(Me.Page, PAGE_USER_PROFILE_MOBILE)
    End Sub
End Class