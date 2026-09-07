Imports AIT.SM

Public Class MainMst
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim dtUser As New UserManager.DataUser
        'dtUser = Session("dtUser")
        'lbUsername.Text = dtUser.UserName

    End Sub

    Private Sub ibtMenu_Click(sender As Object, e As ImageClickEventArgs) Handles ibtMenu.Click
        If divSideNav.Style.Item("width") = "30px" Then
            divSideNav.Style.Item("width") = "200px"
            divPlaceHolder.Style.Item("left") = "220px"
        Else
            divSideNav.Style.Item("width") = "30px"
            divPlaceHolder.Style.Item("left") = "50px"
        End If

    End Sub

    Private Sub ibtLogoAgility_Click(sender As Object, e As ImageClickEventArgs) Handles ibtLogoAgility.Click
        Dim modified_URL As String = "window.open('https://connections.agility.com/', '_blank');"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OPEN_WINDOW", modified_URL, True)
    End Sub
End Class