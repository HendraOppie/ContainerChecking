Public Class SignUp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbAppsName.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().FullName
    End Sub

End Class