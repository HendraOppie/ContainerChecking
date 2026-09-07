Imports AIT.SM

Public Class RequestResetPassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbAppsName.Text = APPS_NAME
    End Sub

    Private Sub btSubmit_Click(sender As Object, e As EventArgs) Handles btSubmit.Click
        Dim dataUser As UserManager.DataUser = p_getUserDataByEmail()
        If dataUser.UserId = String.Empty Then
            PopupAlertMessage(Me.Page, "Email not found")
        ElseIf dataUser.Password = String.Empty Then
            PopupAlertMessage(Me.Page, "Your account is not allowed to change password")
        Else
            Dim dataEmail As New EmailNotification.DataSendEmail
            dataEmail.To = tbEmail.Text
            dataEmail.Subject = APPS_NAME & " :: Your Request to Change Password"
            dataEmail.Body = "Hi,<br />You are requesting to Change Password.<br />If this is true then follow the link " & URL_PREFIX_USER_PROFILE & p_setEncryptedParameters(dataUser) & " ."
            dataEmail.Body += "<br />But if this is not true or you want to cancel your request, just ignore this email.<br />The link is valid for 24 hours only."
            dataEmail.Body += "<br /><br />Regards,<br />IT Dept."

            If p_sendEmail(dataEmail) Then
                Session(SESSION_INFO) = "A link to Change Password has been sent to " & tbEmail.Text & ". Please check email and follow the link."
                GotoPage(Me.Page, PAGE_INFO)
            End If
        End If
    End Sub

    Private Function p_setEncryptedParameters(dataUser As UserManager.DataUser) As String
        Dim stringToEncrypt As String = String.Format("{0},{1}", Now.AddHours(24).ToString, dataUser.UserId)
        Dim sEncrypted As String = New SystemManager().Encrypt(stringToEncrypt)
        'Dim sDecrypted As String = New SystemManager().Decrypt(sEncrypted) 'testing purpose only

        Return sEncrypted
    End Function

    Private Function p_sendEmail(data As EmailNotification.DataSendEmail) As Boolean
        Try
            Dim oEmail As New EmailNotification
            oEmail.SendEmail(data, Session(FILE_CONNECTION_STRING))
            Return True
        Catch ex As Exception
            Throw
        End Try

        Return False
    End Function

    Private Function p_getUserDataByEmail() As UserManager.DataUser
        Try
            Return New UserManager().GetDataByEmail(tbEmail.Text, Session(FILE_CONNECTION_STRING))
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class