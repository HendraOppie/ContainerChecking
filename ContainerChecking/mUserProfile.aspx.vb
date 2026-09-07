Imports AIT.SM

Public Class MobileUserProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        p_validateUrlParameter()

        If Not IsPostBack Then
            p_populateUserInfo()
        End If

        If Session(SESSION_USER_MUST_CHANGE_PASSWORD) IsNot Nothing Then
            tbUserName.Enabled = False
            tbEmail.Enabled = False
            btUpdate.Enabled = False
            btUpdate.BackColor = System.Drawing.Color.Gray
        End If
    End Sub

    Private Sub p_validateUrlParameter()
        Dim sExpiredDate As String = p_getUrlParameterItem(0)
        If Not sExpiredDate = String.Empty Then
            Dim dateExpiry As Date = Convert.ToDateTime(sExpiredDate)
            If DateDiff(DateInterval.Minute, dateExpiry, Now) >= 0 Then Throw New HttpException(404, "Page Expired")

            p_setSessionForForgotPasswordMode(p_getUrlParameterItem(1))
        End If
    End Sub

    Private Function p_getUrlParameterItem(iIndex As Int16) As String '0= string expiry date; 1= userid
        Dim sUrlParameter As String = Request.QueryString("id").Replace(" ", "+")
        If Not sUrlParameter = String.Empty Then
            Dim sDecryptedUrlParameter As String = New SystemManager().Decrypt(sUrlParameter)
            Dim lUrlParameter() As String = sDecryptedUrlParameter.Split(",")

            Return lUrlParameter(iIndex)
        End If

        Return String.Empty
    End Function

    Private Sub p_setSessionForForgotPasswordMode(userID As String)
        Dim sPath As String = Server.MapPath("~/Bin/")
        Session(SESSION_FILE_CONNECTION_STRING) = sPath & FILE_CONNECTION_STRING

        Session(SESSION_USER_MUST_CHANGE_PASSWORD) = True
        tbPass.Enabled = False

        Dim dtUser As UserManager.DataUser = getDataUser(userID)
        Session(SESSION_DATA_USER) = dtUser

        Dim dtRole As RoleManager.DataRole = getDataRole(dtUser.RoleId)
        Session(SESSION_DATA_ROLE) = dtRole
    End Sub

    Private Function getDataUser(userID As String) As UserManager.DataUser
        Try
            Return New UserManager().GetData(userID, FILE_CONNECTION_STRING)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function getDataRole(sRoleId) As RoleManager.DataRole
        Try
            Return New RoleManager().GetData(sRoleId, FILE_CONNECTION_STRING)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub p_populateUserInfo()
        lbUserId.Text = SessionDataUser().UserId
        tbUserName.Text = SessionDataUser().UserName
        tbEmail.Text = SessionDataUser().Email
        lbRole.Text = SessionDataRole().Name
    End Sub

    Private Sub btSubmit_Click(sender As Object, e As EventArgs) Handles btSubmit.Click
        If p_isInputValidChangePassword() Then
            Try
                Dim oUM As New UserManager
                oUM.UpdatePassword(p_setDataChangePassword, Session(SESSION_FILE_CONNECTION_STRING))
                PopupAlertMessageAndRedirect(Me.Page, "Change Password successfully completed. Please login using New Password", PAGE_LOGIN)
            Catch ex As Exception
                Throw
            End Try
        End If
    End Sub

    Private Sub btUpdate_Click(sender As Object, e As EventArgs) Handles btUpdate.Click
        If p_isInputValid() Then
            Try
                Dim oUM As New UserManager
                oUM.UpdateWithoutPassword(p_setData, Session(SESSION_FILE_CONNECTION_STRING))
                PopupAlertMessageAndRedirect(Me.Page, "Update Profile successfully completed. Please re-login", PAGE_LOGIN)
            Catch ex As Exception
                Throw
            End Try
        End If
    End Sub

    Private Function p_setData() As UserManager.DataUser
        Dim data As UserManager.DataUser = SessionDataUser()
        data.UserName = tbUserName.Text
        data.Email = tbEmail.Text
        Return data
    End Function

    Private Function p_setDataChangePassword() As UserManager.DataUser
        Dim data As UserManager.DataUser = SessionDataUser()
        data.Password = tbPassNewConfirm.Text
        Return data
    End Function

    Private Function p_isInputValid() As Boolean
        If tbUserName.Text.Length < 5 Then
            PopupAlertMessage(Me.Page, "Username is too short. Please provide fullname")
            Return False
        End If

        Return True
    End Function

    Private Function p_isInputValidChangePassword() As Boolean
        If Not tbPass.Text = SessionDataUser().Password Then
            PopupAlertMessage(Me.Page, "Please provide valid Current Password")
            Return False
        End If
        If tbPassNew.Text.Trim = String.Empty Then
            PopupAlertMessage(Me.Page, "Please provide New Password")
            Return False
        End If
        If tbPass.Enabled = True And tbPass.Text.Trim = tbPassNew.Text.Trim Then
            PopupAlertMessage(Me.Page, "Please provide different Password than existing")
            Return False
        End If
        If tbPassNewConfirm.Text.Trim = String.Empty Then
            PopupAlertMessage(Me.Page, "Please provide New Password Confirmation")
            Return False
        End If
        If tbPassNewConfirm.Text.Length < 6 Then
            PopupAlertMessage(Me.Page, "Password is too short. Min required is 6 chars")
            Return False
        End If
        If tbPassNew.Text <> tbPassNewConfirm.Text Then
            PopupAlertMessage(Me.Page, "Please make sure New Password Confirmation is match")
            Return False
        End If

        Return True
    End Function
End Class