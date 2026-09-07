Public Class ThankYou1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session(SESSION_INFO) Is Nothing Then
                Response.Redirect(PAGE_LOGIN)
            End If

            lbHeaderInfo.Text = Session(SESSION_INFO_Header)
            lbInfo.Text = Session(SESSION_INFO)
            Session.Clear()
        End If

    End Sub

End Class