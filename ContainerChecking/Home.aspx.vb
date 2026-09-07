Public Class HomeMain
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session(SESSION_FILE_CONNECTION_STRING) Is Nothing Then Response.Redirect("~/Login.aspx")
    End Sub

End Class