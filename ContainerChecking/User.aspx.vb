Imports AIT.SM
Imports AIT.ContainerCheck

Public Class User
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            p_setRoleList()
            p_setOptionEnabledSearchList()
            p_populateData()
        End If

        p_populateSearchResult()
        p_implementUserAccess()
    End Sub

    Private Sub p_setOptionEnabledSearchList()
        ddEnabledSearch.Items.Clear()
        ddEnabledSearch.Items.Add(String.Empty)
        ddEnabledSearch.Items.Add(OPTIONAL_SEARCH_DISABLED)
        ddEnabledSearch.Items.Add(OPTIONAL_SEARCH_ENABLED)
    End Sub

    Private Sub p_populateData()
        tbUserId.Text = SessionDataUser().UserId
        tbUsername.Text = SessionDataUser().UserName
        tbEmail.Text = SessionDataUser().Email
        ddRole.Text = p_getRoleName(SessionDataUser().RoleId)
        cbEnabled.Checked = SessionDataUser().bEnabled
        cbChangePassword.Checked = SessionDataUser().bChangePassword
    End Sub

    Private Sub p_setRoleList()
        Try
            Dim alList As ArrayList = New RoleManager().GetList(Session(SESSION_FILE_CONNECTION_STRING))

            ddRole.Items.Clear()
            ddRole.Items.Add(String.Empty)

            For Each data As RoleManager.DataRole In alList
                ddRole.Items.Add(data.Name)
            Next

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub p_implementUserAccess()
        ibtNew.Enabled = SessionDataRoleItem(Me.Page.GetType().Name).bCreate
        tbUserId.Enabled = SessionDataRoleItem(Me.Page.GetType().Name).bCreate
        cbEnabled.Enabled = SessionDataRoleItem(Me.Page.GetType().Name).bCreate
        cbChangePassword.Enabled = SessionDataRoleItem(Me.Page.GetType().Name).bCreate
        ddRole.Enabled = SessionDataRoleItem(Me.Page.GetType().Name & ROLE_ITEM_ROLE).bCreate
        ibtSave.Enabled = SessionDataRoleItem(Me.Page.GetType().Name).bCreate

        ibtSave.Enabled = SessionDataRoleItem(Me.Page.GetType().Name).bUpdate
        ibtDelete.Enabled = SessionDataRoleItem(Me.Page.GetType().Name).bDelete
        ibtSearch.Enabled = SessionDataRoleItem(Me.Page.GetType().Name).bSearch
        ibtDeleteList.Enabled = SessionDataRoleItem(Me.Page.GetType().Name).bDelete
    End Sub

    Private Function p_isDeleteValid() As Boolean
        If tbUserId.Text = String.Empty Then Return False
        If tbUserId.Text = SessionDataUser().UserId Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Cannot Delete your own user", 2)
            Return False
        End If

        Try
            If New ContainersManager().IsUpdatedByUserExists(tbUserId.Text, Session(SESSION_FILE_CONNECTION_STRING)) Then
                PopulateInfoInPage(lbSysInfo, Me.Page, "Cannot Delete. UserId found in Containers Data", 2)
                Return False
            End If

            If New LocationsManager().IsUpdatedByUserExists(tbUserId.Text, Session(SESSION_FILE_CONNECTION_STRING)) Then
                PopulateInfoInPage(lbSysInfo, Me.Page, "Cannot Delete. UserId found in Locations Data", 2)
                Return False
            End If

            If New CustomersManager().IsUpdatedByUserExists(tbUserId.Text, Session(SESSION_FILE_CONNECTION_STRING)) Then
                PopulateInfoInPage(lbSysInfo, Me.Page, "Cannot Delete. UserId found in Customers Data", 2)
                Return False
            End If

            If New OtherConditionsManager().IsUpdatedByUserExists(tbUserId.Text, Session(SESSION_FILE_CONNECTION_STRING)) Then
                PopulateInfoInPage(lbSysInfo, Me.Page, "Cannot Delete. UserId found in Temperature Log Data", 2)
                Return False
            End If

            If New PhysicalConditionManager().IsUpdatedByUserExists(tbUserId.Text, Session(SESSION_FILE_CONNECTION_STRING)) Then
                PopulateInfoInPage(lbSysInfo, Me.Page, "Cannot Delete. UserId found in Physical Inspection Data", 2)
                Return False
            End If

            If New ProofOfDeliveryManager().IsUpdatedByUserExists(tbUserId.Text, Session(SESSION_FILE_CONNECTION_STRING)) Then
                PopulateInfoInPage(lbSysInfo, Me.Page, "Cannot Delete. UserId found in POD Data", 2)
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try

        Return True
    End Function

    Private Function p_isInputValid() As Boolean
        If tbUserId.Text.Length < 6 Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "User ID is too short. Min required is 6 chars", 2)
            Return False
        End If
        If tbUsername.Text.Length < 5 Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Username is too short. Please provide fullname", 2)
            Return False
        End If
        If ddRole.Text.TrimEnd = String.Empty Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Please provide Role", 2)
            Return False
        End If
        If tbUserId.Enabled And tbUserId.Text = SessionDataUser().UserId And cbEnabled.Checked = False Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Cannot Disabled your own user", 2)
            Return False
        End If

        Return True
    End Function

    Private Function p_setData() As UserManager.DataUser
        Dim data As New UserManager.DataUser
        data.UserId = tbUserId.Text
        data.UserName = tbUsername.Text
        data.Email = tbEmail.Text
        data.bEnabled = cbEnabled.Checked
        data.bChangePassword = cbChangePassword.Checked
        data.RoleId = p_getRoleId(ddRole.Text)
        Return data
    End Function

    Private Function p_getRoleId(sName As String)
        Dim alList As New ArrayList

        Try
            alList = New RoleManager().GetList(Session(SESSION_FILE_CONNECTION_STRING))
        Catch ex As Exception
            Throw
        End Try

        For Each data As RoleManager.DataRole In alList
            If data.Name = sName Then Return data.ID
        Next

        Return String.Empty
    End Function

    Private Function p_getRoleName(sID As String) As String
        Try
            Dim data As RoleManager.DataRole = New RoleManager().GetData(sID, Session(SESSION_FILE_CONNECTION_STRING))
            Return data.Name
        Catch ex As Exception
            Throw
        End Try

        Return String.Empty
    End Function

    Private Sub p_populateSearchResult(Optional bSelectAll As Boolean = False)
        If Session(SESSION_SEARCH_RESULT_USERS) Is Nothing Then Exit Sub
        Dim alSearchResult As ArrayList = Session(SESSION_SEARCH_RESULT_USERS)
        ClearTableRows(tblDetail)
        For Each data As UserManager.DataUser In alSearchResult
            p_populateTableRows(data, bSelectAll)
        Next
    End Sub

    Private Sub p_populateTableRows(data As UserManager.DataUser, bSelectAll As Boolean)
        Dim oRow As New TableRow
        oRow.VerticalAlign = VerticalAlign.Top

        Dim oCellCheckBox As New TableCell
        Dim oCheckBox As New CheckBox
        oCheckBox.AutoPostBack = True
        oCheckBox.ID = data.UserId
        oCheckBox.Checked = bSelectAll
        AddHandler oCheckBox.CheckedChanged, AddressOf p_CheckBoxDetailOnCheckedChanged
        oCellCheckBox.Controls.Add(oCheckBox)
        oRow.Cells.Add(oCellCheckBox)

        oRow.Cells.Add(CreateLabelCell(data.UserId))
        oRow.Cells.Add(CreateLabelCell(data.UserName))
        oRow.Cells.Add(CreateLabelCell(data.Email))

        Dim sValue As String = OPTIONAL_SEARCH_ENABLED
        If Not data.bEnabled Then sValue = OPTIONAL_SEARCH_DISABLED
        oRow.Cells.Add(CreateLabelCell(sValue))

        oRow.Cells.Add(CreateLabelCell(data.RoleName))

        tblDetail.Rows.Add(oRow)
    End Sub

    Protected Sub p_CheckBoxDetailOnCheckedChanged(sender As Object, e As EventArgs)
        Dim oCb As CheckBox = sender
        If Not oCb.Checked Then
            cbDetail.Checked = False
            Exit Sub
        End If

        lbSysInfo.Text = String.Empty
        Dim alSearchResult As ArrayList = Session(SESSION_SEARCH_RESULT_USERS)
        For Each data As UserManager.DataUser In alSearchResult
            If data.UserId = sender.id Then
                p_populateDataSearch(data)
                Exit For
            End If
        Next
    End Sub

    Private Sub p_populateDataSearch(data As UserManager.DataUser)
        tbUserId.Text = data.UserId
        tbUsername.Text = data.UserName
        tbEmail.Text = data.Email
        ddRole.Text = data.RoleName
        cbEnabled.Checked = data.bEnabled
        cbChangePassword.Checked = data.bChangePassword
    End Sub

    Private Sub ibtSearch_Click(sender As Object, e As ImageClickEventArgs) Handles ibtSearch.Click
        If Not SessionDataRoleItem(Me.Page.GetType().Name).bSearch Then
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            Exit Sub
        End If

        Try
            p_searchAndPopulate()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub p_searchAndPopulate()
        Try
            Dim alSearchRessult As ArrayList = New UserManager().GetSearch(p_collectSearchingCriteria, Session(SESSION_FILE_CONNECTION_STRING))
            Session(SESSION_SEARCH_RESULT_USERS) = alSearchRessult
            p_populateSearchResult()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Function p_collectSearchingCriteria() As UserManager.DataUser
        Dim data As New UserManager.DataUser
        data.UserId = tbUserIdSearch.Text.Replace("*", "%")
        data.UserName = tbUsernameSearch.Text.Replace("*", "%")
        data.Email = tbEmailSearch.Text.Replace("*", "%")
        data.RoleId = tbRoleSearch.Text.Replace("*", "%")

        If Not ddEnabledSearch.Text = String.Empty Then data.bValid = True
        If ddEnabledSearch.Text = OPTIONAL_SEARCH_ENABLED Then data.bEnabled = 1

        Return data
    End Function

    Private Sub ibtNew_Click(sender As Object, e As ImageClickEventArgs) Handles ibtNew.Click
        tbUserId.Text = String.Empty
        tbUsername.Text = String.Empty
        tbEmail.Text = String.Empty
        ddRole.Text = String.Empty
        cbEnabled.Checked = True
        cbChangePassword.Checked = True
    End Sub

    Private Sub ibtSave_Click(sender As Object, e As ImageClickEventArgs) Handles ibtSave.Click
        Try
            Dim data As UserManager.DataUser = p_setData()
            Dim oUM As New UserManager
            Dim xData As UserManager.DataUser = oUM.GetData(data.UserId, Session(SESSION_FILE_CONNECTION_STRING))
            Dim bExists As Boolean = True
            If xData.UserId = String.Empty Then bExists = False

            If p_isInputValid() Then
                If bExists Then
                    oUM.UpdateWithoutPassword(data, Session(SESSION_FILE_CONNECTION_STRING))
                    If data.UserId = SessionDataUser().UserId Then
                        PopupAlertMessageAndRedirect(Me.Page, "Update Profile successfully completed. Please re-login", PAGE_LOGIN)
                    Else
                        PopulateInfoInPage(lbSysInfo, Me.Page, "Update Profile successfully completed", 1)
                    End If
                Else
                    data.Password = String.Empty
                    data.bChangePassword = 1
                    oUM.Insert(data, Session(SESSION_FILE_CONNECTION_STRING))
                    PopulateInfoInPage(lbSysInfo, Me.Page, "Create user successfully completed", 1)
                End If
            End If

            If Not Session(SESSION_SEARCH_RESULT_USERS) Is Nothing Then p_searchAndPopulate()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ibtClearSearch_Click(sender As Object, e As ImageClickEventArgs) Handles ibtClearSearch.Click
        tbUserIdSearch.Text = String.Empty
        tbUsernameSearch.Text = String.Empty
        tbEmailSearch.Text = String.Empty
        ddEnabledSearch.Text = String.Empty
        tbRoleSearch.Text = String.Empty
    End Sub

    Private Sub ibtDelete_Click(sender As Object, e As ImageClickEventArgs) Handles ibtDelete.Click
        Try
            If p_isDeleteValid() Then
                Dim oUM As New UserManager
                oUM.Delete(tbUserId.Text, Session(SESSION_FILE_CONNECTION_STRING))
                PopulateInfoInPage(lbSysInfo, Me.Page, "Delete user successfully completed", 1)
                p_populateData()

                If Not Session(SESSION_SEARCH_RESULT_USERS) Is Nothing Then p_searchAndPopulate()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ibtDeleteList_Click(sender As Object, e As ImageClickEventArgs) Handles ibtDeleteList.Click
        If Session(SESSION_SEARCH_RESULT_USERS) Is Nothing Then Exit Sub
        If Not SessionDataRoleItem(Me.Page.GetType().Name).bDelete Then
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            Exit Sub
        End If

        Dim alSelectedData As ArrayList = p_collectingSelectedData()

        If alSelectedData.Count = 0 Then
            PopupAlertMessage(Me.Page, "No Selected Data")
        Else
            For Each data As UserManager.DataUser In alSelectedData
                p_populateDataSearch(data)
                If Not p_isDeleteValid() Then
                    Exit For
                Else
                    Try
                        Dim oUM As New UserManager
                        oUM.Delete(tbUserId.Text, Session(SESSION_FILE_CONNECTION_STRING))
                    Catch ex As Exception
                        Throw
                    End Try
                End If
            Next
        End If

        p_searchAndPopulate()
        PopulateInfoInPage(lbSysInfo, Me.Page, "Delete users successfully completed", 1)
    End Sub

    Private Function p_collectingSelectedData() As ArrayList
        Dim alSelectedId As ArrayList = CollectingSelectedRowsID(tblDetail)

        Dim alDataFromList As New ArrayList
        For Each sID As String In alSelectedId
            Dim dataFromList As UserManager.DataUser = p_getDataFromList(sID)
            If Not dataFromList.UserId = String.Empty Then
                alDataFromList.Add(dataFromList)
            End If

        Next
        Return alDataFromList
    End Function

    Private Function p_getDataFromList(sUserId As String) As UserManager.DataUser
        Dim dataFromList As New UserManager.DataUser

        Dim alSearchResult As ArrayList = Session(SESSION_SEARCH_RESULT_USERS)
        For Each data As UserManager.DataUser In alSearchResult
            If data.UserId = sUserId Then
                dataFromList = data
                Return dataFromList
            End If
        Next

        Return dataFromList
    End Function

    Private Sub cbDetail_CheckedChanged(sender As Object, e As EventArgs) Handles cbDetail.CheckedChanged
        p_populateSearchResult(cbDetail.Checked)
    End Sub

End Class