Public Class MenuMobile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub ibtHandover_Click(sender As Object, e As ImageClickEventArgs) Handles ibtHandover.Click
        GotoPage(Me.Page, PAGE_DELIVERY_NOTE_MOBILE, True)
    End Sub

    Private Sub ibtInspection_Click(sender As Object, e As ImageClickEventArgs) Handles ibtInspection.Click
        GotoPage(Me.Page, PAGE_PHYSICAL_INSPECTION_MOBILE, True)
    End Sub

    Private Sub ibtLogout_Click(sender As Object, e As ImageClickEventArgs) Handles ibtLogout.Click
        GotoPage(Me.Page, PAGE_LOGIN)
    End Sub

    Private Sub ibtTemp_Click(sender As Object, e As ImageClickEventArgs) Handles ibtTemp.Click
        GotoPage(Me.Page, PAGE_TEMPERATURE_LOG_MOBILE, True)
    End Sub

    Private Sub lbtHandover_Click(sender As Object, e As EventArgs) Handles lbtHandover.Click
        GotoPage(Me.Page, PAGE_DELIVERY_NOTE_MOBILE, True)
    End Sub

    Private Sub lbtInspection_Click(sender As Object, e As EventArgs) Handles lbtInspection.Click
        GotoPage(Me.Page, PAGE_PHYSICAL_INSPECTION_MOBILE, True)
    End Sub

    Private Sub lbtLogout_Click(sender As Object, e As EventArgs) Handles lbtLogout.Click
        GotoPage(Me.Page, PAGE_LOGIN)
    End Sub

    Private Sub lbtTemp_Click(sender As Object, e As EventArgs) Handles lbtTemp.Click
        GotoPage(Me.Page, PAGE_TEMPERATURE_LOG_MOBILE, True)
    End Sub
End Class