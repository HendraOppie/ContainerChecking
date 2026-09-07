Imports AIT.SM

Public Class Login
    Inherits System.Web.UI.Page

    Dim sPath As String = Server.MapPath("~/Bin/")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Clear()
    End Sub

    Protected Sub btLogin_Click(sender As Object, e As EventArgs) Handles btLogin.Click
        If tbUser.Text.Trim = String.Empty Then Exit Sub

        Dim bRequestToMobileVersion As Boolean = False
        If tbPass.Text.Substring(tbPass.Text.Length - Len(CODE_LOGIN_TO_MOBILE), Len(CODE_LOGIN_TO_MOBILE)) = CODE_LOGIN_TO_MOBILE Then bRequestToMobileVersion = True

        Dim dtUser As UserManager.DataUser = getDataUser()

        If dtUser.UserId <> String.Empty Then
            If dtUser.bValid Then
                If dtUser.bEnabled Then
                    Session(SESSION_DATA_USER) = dtUser

                    Dim dtRole As RoleManager.DataRole = getDataRole(dtUser.RoleId)
                    Session(SESSION_DATA_ROLE) = dtRole
                    Session(SESSION_FILE_CONNECTION_STRING) = sPath & FILE_CONNECTION_STRING

                    If dtUser.Password = String.Empty Then
                        If (Request.Browser.IsMobileDevice) Then
                            GotoPage(Me.Page, PAGE_MENU)
                        Else
                            If bRequestToMobileVersion Then
                                GotoPage(Me.Page, PAGE_MENU)
                            Else
                                GotoPage(Me.Page, PAGE_HOME)
                            End If
                        End If
                    Else
                        If dtUser.bChangePassword Then
                            Session(SESSION_USER_MUST_CHANGE_PASSWORD) = True
                            If (Request.Browser.IsMobileDevice) Then
                                GotoPage(Me.Page, PAGE_USER_PROFILE_MOBILE)
                            Else
                                If bRequestToMobileVersion Then
                                    GotoPage(Me.Page, PAGE_USER_PROFILE_MOBILE)
                                Else
                                    GotoPage(Me.Page, PAGE_USER_PROFILE)
                                End If

                            End If
                        Else
                            If (Request.Browser.IsMobileDevice) Then
                                GotoPage(Me.Page, PAGE_MENU)
                            Else
                                If bRequestToMobileVersion Then
                                    GotoPage(Me.Page, PAGE_MENU)
                                Else
                                    GotoPage(Me.Page, PAGE_HOME)
                                End If
                            End If
                        End If
                    End If
                Else
                    PopupAlertMessage(Me.Page, "User has been disabled")
                End If
            Else
                PopupAlertMessage(Me.Page, "Invalid Username/Password")
            End If
        Else
            PopupAlertMessage(Me.Page, "Invalid Username/Password")
            tbPass.Text = String.Empty
        End If

    End Sub

    Private Function getDataUser() As UserManager.DataUser
        Try
            Return New UserManager().GetValidDataWithLdapExam(tbUser.Text, tbPass.Text.Replace(CODE_LOGIN_TO_MOBILE, ""), Environment.UserDomainName, sPath & FILE_CONNECTION_STRING)
        Catch ex As Exception
            If ex.Message.Contains("The user name or password is incorrect") Then
                Return New UserManager.DataUser
            Else
                Throw
            End If
        End Try
    End Function

    Private Function getDataRole(sRoleId) As RoleManager.DataRole
        Try
            Return New RoleManager().GetData(sRoleId, sPath & FILE_CONNECTION_STRING)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub Login_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        If (Request.Browser.IsMobileDevice) Then
            'do something
        End If

    End Sub

    Private Sub btSignUp_Click(sender As Object, e As EventArgs) Handles btSignUp.Click
        GotoPage(Me.Page, PAGE_SIGNUP)
    End Sub

End Class