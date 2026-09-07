Imports AIT.SM
Imports AIT.ContainerCheck

Public Class UserProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            p_populateData()
        End If

        p_implementUserAccess()
    End Sub

    Private Sub p_populateData()
        tbUserId.Text = SessionDataUser().UserId
        tbUsername.Text = SessionDataUser().UserName
        tbEmail.Text = SessionDataUser().Email
        tbRoleId.Text = SessionDataUser().RoleId
        tbRoleName.Text = SessionDataRole().Name
        cbEnabled.Checked = SessionDataUser().bEnabled
        cbChangePassword.Checked = SessionDataUser().bChangePassword

        If SessionDataUser().Password = String.Empty Then divChangePassword.Visible = False
    End Sub

    Private Sub p_implementUserAccess()
        ibtNew.ToolTip = "not available"
        ibtSave.Enabled = SessionDataRoleItem(Me.Page.GetType().Name).bUpdate
        ibtDelete.ToolTip = "not available"
    End Sub

    Private Sub btChangePassword_Click(sender As Object, e As EventArgs) Handles btChangePassword.Click
        If p_isInputValidForChangePassword() Then
            Try
                Dim oUM As New UserManager
                oUM.UpdatePassword(p_setDataChangePassword, Session(SESSION_FILE_CONNECTION_STRING))
                PopupAlertMessageAndRedirect(Me.Page, "Change Password successfully completed. Please login using New Password", PAGE_LOGIN)
            Catch ex As Exception
                Throw
            End Try
        End If
    End Sub

    Private Function p_isInputValid() As Boolean
        If tbUsername.Text.Length < 5 Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Username is too short. Please provide fullname", 2)
            Return False
        End If

        Return True
    End Function

    Private Function p_isInputValidForChangePassword() As Boolean
        If Not tbPassword.Text.Trim = SessionDataUser().Password Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Please provide valid Current Password", 2)
            Return False
        End If
        If tbPasswordNew.Text.Trim = String.Empty Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Please provide New Password", 2)
            Return False
        End If
        If tbPassword.Text.Trim = tbPasswordNew.Text.Trim Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Please provide different password than existing", 2)
            Return False
        End If
        If tbNewPasswordConfirmation.Text.Trim = String.Empty Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Please provide New Password Confirmation", 2)
            Return False
        End If
        If tbNewPasswordConfirmation.Text.Length < 6 Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Password is too short. Min required is 6 chars", 2)
            Return False
        End If
        If Not tbPasswordNew.Text = tbNewPasswordConfirmation.Text Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Please make sure New Password Confirmation is match", 2)
            Return False
        End If
        Return True
    End Function

    Private Function p_setData() As UserManager.DataUser
        Dim data As UserManager.DataUser = SessionDataUser()
        data.UserId = tbUserId.Text
        data.UserName = tbUsername.Text
        data.Email = tbEmail.Text
        Return data
    End Function

    Private Function p_setDataChangePassword() As UserManager.DataUser
        Dim data As UserManager.DataUser = SessionDataUser()
        data.Password = tbNewPasswordConfirmation.Text
        data.bChangePassword = False
        Return data
    End Function

    Private Sub ibtSave_Click(sender As Object, e As ImageClickEventArgs) Handles ibtSave.Click
        Try
            If p_isInputValid() Then
                Dim oUM As New UserManager
                oUM.UpdateWithoutPassword(p_setData(), Session(SESSION_FILE_CONNECTION_STRING))
                PopupAlertMessageAndRedirect(Me.Page, "Update Profile successfully completed. Please re-login", PAGE_LOGIN)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class