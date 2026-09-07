Public Class Info
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session(SESSION_INFO) Is Nothing Then
                GotoPage(Me.Page, PAGE_LOGIN)
            End If

            lbInfo.Text = Session(SESSION_INFO)
            Session.Remove(SESSION_INFO)
        End If
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        GotoPage(Me.Page, Session(SESSION_INFO_REDIRECT))
    End Sub
End Class