Imports System.IO
Imports AIT.SM
Imports AIT.ContainerCheck

Public Class Containers
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lbSysInfo.Text = String.Empty

            p_getAndPopulateCustomer("BDOBIO001") 'temporary hardcoded 
            p_setStatusList()
        End If

        p_populateSearchResult()
    End Sub

    Private Sub p_setStatusList()
        ddStatusSearch.Items.Clear()
        ddStatusSearch.Items.Add(String.Empty)
        ddStatusSearch.Items.Add(ContainersManager.ContainerStatus.INCOMING.ToString)
        ddStatusSearch.Items.Add(ContainersManager.ContainerStatus.RECEIVED.ToString)
        ddStatusSearch.Items.Add(ContainersManager.ContainerStatus.DELIVERED.ToString)
    End Sub

    Private Sub ibtDelete_Click(sender As Object, e As EventArgs) Handles ibtDelete.Click
        If Not p_isInputValid() Then Exit Sub

        Try
            Dim data As ContainersManager.DataContainers = p_setData()

            Dim exists As Boolean = False
            If p_isDataExistsValid(data, exists) Then
                If Not SessionDataRoleItem(Me.Page.GetType().Name).bDelete Then
                    PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
                    Exit Sub
                End If

                Dim oManager As New ContainersManager
                oManager.Delete(data, Session(SESSION_FILE_CONNECTION_STRING))

                PopulateInfoInPage(lbSysInfo, Me.Page, "Data Successfully Deleted", 1)
                Session.Remove(SESSION_DATA_CONTAINERS)
                tbHawb.Text = String.Empty
                tbMawb.Text = String.Empty
                tbContainer.Text = String.Empty
                'tbCustomerID.Text = String.Empty
                p_research()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub p_getAndPopulateCustomer(Id As String)
        Dim data As New CustomersManager.DataCustomers
        If Session(SESSION_DATA_CUSTOMERS) Is Nothing Then
            data = New CustomersManager().GetData(Id, Session(SESSION_FILE_CONNECTION_STRING))
        Else
            data = Session(SESSION_DATA_CUSTOMERS)
        End If

        tbCustomerID.Text = data.ID
        lbCustomerInfo.Text = data.Name
        If Not data.Address1 = String.Empty Then lbCustomerInfo.Text += "<br/>" & data.Address1
        If Not data.Address2 = String.Empty Then lbCustomerInfo.Text += "<br/>" & data.Address2
        If Not data.Address3 = String.Empty Then lbCustomerInfo.Text += "<br/>" & data.Address3
        If Not data.City = String.Empty Then
            lbCustomerInfo.Text += "<br/>" & data.City
            If Not data.State = String.Empty Then lbCustomerInfo.Text += ", " & data.State
        Else
            If Not data.State = String.Empty Then lbCustomerInfo.Text += "<br/>" & data.State
        End If
    End Sub

    Private Sub ibtNew_Click(sender As Object, e As ImageClickEventArgs) Handles ibtNew.Click
        tbContainer.Text = String.Empty
        lbSysInfo.Text = String.Empty
        Session.Remove(SESSION_DATA_CONTAINERS)
    End Sub

    Private Sub ibtSave_Click(sender As Object, e As ImageClickEventArgs) Handles ibtSave.Click
        If Not p_isInputValid() Then Exit Sub

        Try
            Dim data As ContainersManager.DataContainers = p_setData()

            Dim exists As Boolean = False
            If p_isDataExistsValid(data, exists) Then
                Dim oManager As New ContainersManager
                If exists Then
                    If SessionDataRoleItem(Me.Page.GetType().Name).bUpdate Then
                        oManager.Update(data, Session(SESSION_FILE_CONNECTION_STRING))
                    Else
                        PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
                        Exit Sub
                    End If
                Else
                    If SessionDataRoleItem(Me.Page.GetType().Name).bCreate Then
                        oManager.Insert(data, Session(SESSION_FILE_CONNECTION_STRING))
                    Else
                        PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
                        Exit Sub
                    End If

                End If
            End If
        Catch ex As Exception
            Throw
        End Try

        PopulateInfoInPage(lbSysInfo, Me.Page, "Data Successfully Saved", 1)
        p_research()
    End Sub

    Private Function p_isDataExistsValid(ByRef data As ContainersManager.DataContainers, ByRef exists As Boolean)
        If Session(SESSION_DATA_CONTAINERS) Is Nothing Then
            Dim xData As ContainersManager.DataContainers = New ContainersManager().GetData(data, Session(SESSION_FILE_CONNECTION_STRING))
            If xData.ContainerNumber = String.Empty Then
                exists = False
            Else
                exists = True
                data.Status = xData.Status
                data.ReceivedDate = xData.ReceivedDate
                data.DeliveredDate = xData.DeliveredDate

                If Not data.Status = ContainersManager.ContainerStatus.INCOMING.ToString Then
                    PopulateInfoInPage(lbSysInfo, Me.Page, "Update/Delete only allowed when Status " & ContainersManager.ContainerStatus.INCOMING.ToString, 2)
                    Return False
                End If
            End If

        End If

        Return True
    End Function

    Private Function p_isInputValid(Optional bDelete As Boolean = False) As Boolean
        If tbHawb.Text.Trim = String.Empty Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Mandatory field (red highlighted) cannot be empty", 2)
            Return False
        End If
        If tbMawb.Text.Trim = String.Empty Then tbMawb.Text = tbHawb.Text

        If tbContainer.Text.Trim = String.Empty Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Mandatory field (red highlighted) cannot be empty", 2)
            Return False
        End If

        If tbCustomerID.Text.Trim = String.Empty Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "Mandatory field (red highlighted) cannot be empty", 2)
            Return False
        End If

        If Session(SESSION_DATA_CONTAINERS) IsNot Nothing Then
            Dim xData As ContainersManager.DataContainers = Session(SESSION_DATA_CONTAINERS)
            If Not xData.Status = ContainersManager.ContainerStatus.INCOMING.ToString Then
                PopulateInfoInPage(lbSysInfo, Me.Page, "Update/Delete only allowed when Status=" & ContainersManager.ContainerStatus.INCOMING.ToString, 2)
                Return False
            End If
        End If

        Return True
    End Function

    Private Function p_setData() As ContainersManager.DataContainers
        Dim data As New ContainersManager.DataContainers
        data.ContainerNumber = tbContainer.Text
        data.Mawb = tbMawb.Text
        data.Hawb = tbHawb.Text
        data.CustomerId = tbCustomerID.Text
        data.UserId = SessionDataUser().UserId

        If Session(SESSION_DATA_CONTAINERS) IsNot Nothing Then
            Dim xData As ContainersManager.DataContainers = Session(SESSION_DATA_CONTAINERS)
            data.Status = xData.Status
            data.ReceivedDate = xData.ReceivedDate
            data.DeliveredDate = xData.DeliveredDate
        End If

        Return data
    End Function

    Private Sub ibtSearch_Click(sender As Object, e As ImageClickEventArgs) Handles ibtSearch.Click
        If Not SessionDataRoleItem(Me.Page.GetType().Name).bSearch Then
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            Exit Sub
        End If

        p_search()
    End Sub

    Private Sub p_research()
        If Session(SESSION_DATA_LIST_CONTAINERS_SEARCH_RESULT) Is Nothing Then Exit Sub
        p_search()
    End Sub

    Private Sub p_search()
        p_validateSearchingCriteria()
        Try
            Dim alSearchRessult As ArrayList = New ContainersManager().GetList(p_collectSearchingCriteria, Session(SESSION_FILE_CONNECTION_STRING))
            Session(SESSION_DATA_LIST_CONTAINERS_SEARCH_RESULT) = alSearchRessult
        Catch ex As Exception
            Throw
        End Try
        p_populateSearchResult()
    End Sub

    Private Sub p_populateSearchResult(Optional bSelectAll As Boolean = False)
        If Session(SESSION_DATA_LIST_CONTAINERS_SEARCH_RESULT) Is Nothing Then Exit Sub
        Dim alSearchResult As ArrayList = Session(SESSION_DATA_LIST_CONTAINERS_SEARCH_RESULT)
        ClearTableRows(tblDetail)
        For Each data As ContainersManager.DataContainers In alSearchResult
            p_populateTableRows(data, bSelectAll)
        Next
    End Sub

    Private Sub p_populateTableRows(data As ContainersManager.DataContainers, bSelectAll As Boolean)
        Dim oRow As New TableRow
        oRow.VerticalAlign = VerticalAlign.Top

        Dim oCellCheckBox As New TableCell
        Dim oCheckBox As New CheckBox
        oCheckBox.AutoPostBack = True
        oCheckBox.ID = String.Format("{0}_{1}", data.ContainerNumber, data.Hawb)
        oCheckBox.Checked = bSelectAll
        AddHandler oCheckBox.CheckedChanged, AddressOf p_CheckBoxDetailOnCheckedChanged
        oCellCheckBox.Controls.Add(oCheckBox)
        oRow.Cells.Add(oCellCheckBox)

        oRow.Cells.Add(CreateLabelCell(data.ContainerNumber))
        oRow.Cells.Add(CreateLabelCell(data.Hawb))
        oRow.Cells.Add(CreateLabelCell(data.Mawb))
        oRow.Cells.Add(CreateLabelCell(data.CustomerName))
        oRow.Cells.Add(CreateLabelCell(data.Status))
        oRow.Cells.Add(CreateLabelCell(data.ReceivedDate.ToString("dd MMM yyyy")))
        oRow.Cells.Add(CreateLabelCell(data.DeliveredDate.ToString("dd MMM yyyy")))
        oRow.Cells.Add(CreateLabelCell(data.UserName))
        oRow.Cells.Add(CreateLabelCell(data.LastUpdated.ToString("dd MMM yyyy")))

        tblDetail.Rows.Add(oRow)
    End Sub

    Private Sub p_validateSearchingCriteria()
        If tbContainerSearch.Text.Trim = String.Empty _
            And tbHawbSearch.Text.Trim = String.Empty _
            And tbMawbSearch.Text.Trim = String.Empty _
            And tbCustomerSearch.Text.Trim = String.Empty _
            And ddStatusSearch.Text.Trim = String.Empty _
            And tbReceivedDateSearch.Text.Trim = String.Empty _
            And tbDeliveredDateSearch.Text.Trim = String.Empty _
            And tbUserSearch.Text.Trim = String.Empty _
            And tbLastUpdatedSearch.Text.Trim = String.Empty _
            Then
            tbLastUpdatedSearch.Text = DateTime.Now.ToString("dd MMM yyyy")
        End If
    End Sub

    Private Function p_collectSearchingCriteria() As ContainersManager.DataContainers
        Dim data As New ContainersManager.DataContainers
        data.ContainerNumber = tbContainerSearch.Text.Replace("*", "%")
        data.Mawb = tbMawbSearch.Text.Replace("*", "%")
        data.Hawb = tbHawbSearch.Text.Replace("*", "%")
        data.CustomerName = tbCustomerSearch.Text.Replace("*", "%")
        data.Status = ddStatusSearch.Text

        If tbReceivedDateSearch.Text = String.Empty Then
            data.ReceivedDate = "28 Jan 1900"
        Else
            data.ReceivedDate = tbReceivedDateSearch.Text
        End If

        If tbDeliveredDateSearch.Text = String.Empty Then
            data.DeliveredDate = "28 Jan 1900"
        Else
            data.DeliveredDate = tbDeliveredDateSearch.Text
        End If


        data.UserName = tbUserSearch.Text.Replace("*", "%")

        If tbLastUpdatedSearch.Text = String.Empty Then
            data.LastUpdated = "28 Jan 1900"
        Else
            data.LastUpdated = tbLastUpdatedSearch.Text
        End If

        Return data
    End Function

    Private Sub cbDetail_CheckedChanged(sender As Object, e As EventArgs) Handles cbDetail.CheckedChanged
        p_populateSearchResult(cbDetail.Checked)
    End Sub

    Protected Sub p_CheckBoxDetailOnCheckedChanged(sender As Object, e As EventArgs)
        Dim oCb As CheckBox = sender
        If Not oCb.Checked Then
            cbDetail.Checked = False
            Exit Sub
        End If

        Dim sID As String = sender.id
        Dim aID() As String = sID.Split("_")
        Dim sContainerNumber As String = String.Empty
        Dim sHawb As String = String.Empty
        If aID.Count = 2 Then
            sContainerNumber = aID(0)
            sHawb = aID(1)
        End If

        Dim alSearchResult As ArrayList = Session(SESSION_DATA_LIST_CONTAINERS_SEARCH_RESULT)
        For Each data As ContainersManager.DataContainers In alSearchResult
            If data.ContainerNumber = sContainerNumber And data.Hawb = sHawb Then
                p_populateData(data)
                Exit For
            End If
        Next
    End Sub

    Private Sub p_populateData(data As ContainersManager.DataContainers)
        tbContainer.Text = data.ContainerNumber
        tbHawb.Text = data.Hawb
        tbMawb.Text = data.Mawb

        'lbCustomerInfo.Text = data.CustomerId

        lbStatus.Text = data.Status
        lbReceivedDate.Text = data.ReceivedDate.ToString("dd MMM yyyy hh:mm:ss")
        lbDeliveredDate.Text = data.DeliveredDate.ToString("dd MMM yyyy hh:mm:ss")
        lbUserName.Text = data.UserName
        lbLastUpdated.Text = data.LastUpdated.ToString("dd MMM yyyy hh:mm:ss")
    End Sub

    Private Sub ibtClearSearch_Click(sender As Object, e As ImageClickEventArgs) Handles ibtClearSearch.Click
        tbContainerSearch.Text = String.Empty
        tbHawbSearch.Text = String.Empty
        tbMawbSearch.Text = String.Empty
        tbCustomerSearch.Text = String.Empty
        ddStatusSearch.Text = String.Empty
        tbReceivedDateSearch.Text = String.Empty
        tbDeliveredDateSearch.Text = String.Empty
        tbUserSearch.Text = String.Empty
        tbLastUpdatedSearch.Text = String.Empty
    End Sub

    Private Sub ibtDeleteList_Click(sender As Object, e As ImageClickEventArgs) Handles ibtDeleteList.Click
        If Not SessionDataRoleItem(Me.Page.GetType().Name).bDelete Then
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            Exit Sub
        End If

        Dim alDataToDelete As ArrayList = p_collectingSelectedData()

        If alDataToDelete.Count = 0 Then
            PopulateInfoInPage(lbSysInfo, Me.Page, "No Selected Data to Delete", 2)
        Else
            For Each data As ContainersManager.DataContainers In alDataToDelete
                p_getDataFromList(data)

                If Not data.Status = ContainersManager.ContainerStatus.INCOMING.ToString Then
                    PopulateInfoInPage(lbSysInfo, Me.Page, "Cannot Delete Data " & data.ContainerNumber & " due to it's Status", 2)
                    Exit Sub
                Else
                    Try
                        Dim oManager As New ContainersManager
                        oManager.Delete(data, Session(SESSION_FILE_CONNECTION_STRING))
                        PopulateInfoInPage(lbSysInfo, Me.Page, "Data " & data.ContainerNumber & " Successfully Deleted", 1)
                    Catch ex As Exception
                        Throw
                    End Try
                End If
            Next
        End If

        p_research()
    End Sub

    Private Function p_collectingSelectedData() As ArrayList
        Dim alSelectedId As ArrayList = CollectingSelectedRowsID(tblDetail)

        Dim alDataFromList As New ArrayList
        For Each sID As String In alSelectedId
            Dim aID() As String = sID.Split("_")
            Dim sContainerNumber As String = String.Empty
            Dim sHawb As String = String.Empty
            If aID.Count = 2 Then
                sContainerNumber = aID(0)
                sHawb = aID(1)
            End If

            Dim data As New ContainersManager.DataContainers
            data.ContainerNumber = sContainerNumber
            data.Hawb = sHawb

            Dim dataFromList As ContainersManager.DataContainers = p_getDataFromList(data)
            If Not dataFromList.ContainerNumber = String.Empty Then
                alDataFromList.Add(dataFromList)
            End If

        Next

        Return alDataFromList
    End Function

    Private Function p_getDataFromList(xData As ContainersManager.DataContainers) As ContainersManager.DataContainers
        Dim dataFromList As New ContainersManager.DataContainers

        Dim alSearchResult As ArrayList = Session(SESSION_DATA_LIST_CONTAINERS_SEARCH_RESULT)
        For Each data As ContainersManager.DataContainers In alSearchResult
            If data.ContainerNumber = xData.ContainerNumber And data.Hawb = xData.Hawb Then
                dataFromList = data
                Return dataFromList
            End If
        Next

        Return dataFromList
    End Function

End Class