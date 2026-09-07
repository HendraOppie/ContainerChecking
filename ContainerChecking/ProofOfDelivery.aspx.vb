Imports System.IO
Imports AIT.ContainerCheck
Imports AIT.SM

Public Class ProofOfDelivery
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            p_setOptionSearchList()
        End If

        p_populateSearchResult()
    End Sub

    Private Sub p_setOptionSearchList()
        ddPictureSearch.Items.Clear()
        ddPictureSearch.Items.Add(OPTIONAL_SEARCH_PICTURE_SHOW_THUMBNAIL)
        ddPictureSearch.Items.Add(OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE)

        ddHandoverTypeSearch.Items.Clear()
        ddHandoverTypeSearch.Items.Add(String.Empty)
        ddHandoverTypeSearch.Items.Add(ProofOfDeliveryManager.HandoverType.CUSTOMER.ToString())
        ddHandoverTypeSearch.Items.Add(ProofOfDeliveryManager.HandoverType.USER.ToString())
    End Sub

    Private Sub ibtClearSearch_Click(sender As Object, e As ImageClickEventArgs) Handles ibtClearSearch.Click
        tbIdSearch.Text = String.Empty
        ddHandoverTypeSearch.Text = String.Empty
        tbReceivedSearch.Text = String.Empty
        tbDepartmentSearch.Text = String.Empty
        tbRemarkSearch.Text = String.Empty
        tbHawbSearch.Text = String.Empty
        tbContainerNumberSearch.Text = String.Empty
        tbUserSearch.Text = String.Empty
        tbLastUpdatedSearch.Text = String.Empty
        ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW_THUMBNAIL
        ddHandoverTypeSearch.Text = String.Empty
    End Sub

    Private Sub ibtSearch_Click(sender As Object, e As ImageClickEventArgs) Handles ibtSearch.Click
        If Not SessionDataRoleItem(Me.Page.GetType().Name).bSearch Then
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            Exit Sub
        End If
        If p_isSearchingCriteriaValid() Then
            Try
                p_searchAndPopulate()
            Catch ex As Exception
                Throw
            End Try

        End If
    End Sub

    Private Function p_isSearchingCriteriaValid() As Boolean
        If tbIdSearch.Text.Trim = String.Empty _
            And ddHandoverTypeSearch.Text.Trim = String.Empty _
            And tbReceivedSearch.Text = String.Empty _
            And tbDepartmentSearch.Text = String.Empty _
            And tbRemarkSearch.Text = String.Empty _
            And tbHawbSearch.Text = String.Empty _
            And tbContainerNumberSearch.Text = String.Empty _
            And tbUserSearch.Text.Trim = String.Empty _
            And tbLastUpdatedSearch.Text.Trim = String.Empty _
            Then
            tbLastUpdatedSearch.Text = DateTime.Now.ToString("dd MMM yyyy")
        End If

        If ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE Then
            If tbIdSearch.Text.Replace("*", "").Replace("%", "").Trim = String.Empty _
                And tbHawbSearch.Text.Replace("*", "").Replace("%", "").Trim = String.Empty _
                And tbContainerNumberSearch.Text.Replace("*", "").Replace("%", "").Trim = String.Empty Then

                PopupAlertMessage(Me.Page, "Please narrow your search by minumum specify ID or Item")
                Return False

            End If
        End If

        Return True
    End Function

    Private Sub p_searchAndPopulate()
        Try
            Dim dataSearchCriteria As New ProofOfDeliveryManager.DataPODSearchCriteria
            If ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE Then dataSearchCriteria.isPictureInclude = True

            Dim data As ProofOfDeliveryManager.DataPOD = p_collectSearchingCriteria(dataSearchCriteria)
            Dim alSearchRessult As ArrayList = New ProofOfDeliveryManager().GetSearch(data, dataSearchCriteria, Session(SESSION_FILE_CONNECTION_STRING))
            Session(SESSION_SEARCH_RESULT_POD) = alSearchRessult
            p_populateSearchResult()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Function p_collectSearchingCriteria(ByRef dataSearchCriteria As ProofOfDeliveryManager.DataPODSearchCriteria) As ProofOfDeliveryManager.DataPOD
        Dim data As New ProofOfDeliveryManager.DataPOD
        data.ID = tbIdSearch.Text.Replace("*", "%")
        data.HandoverType = ddHandoverTypeSearch.Text
        data.ReceivedWho = tbReceivedSearch.Text.Replace("*", "%")
        data.Department = tbDepartmentSearch.Text.Replace("*", "%")
        data.Remark = tbRemarkSearch.Text.Replace("*", "%")

        Dim dataItem As New ProofOfDeliveryManager.DataPODItem
        If tbHawbSearch.Text.Length > 0 Then dataItem.Hawb = tbHawbSearch.Text.Replace("*", "%")
        If tbContainerNumberSearch.Text.Length > 0 Then dataItem.ContainerNumber = tbContainerNumberSearch.Text.Replace("*", "%")
        If tbHawbSearch.Text.Length + tbContainerNumberSearch.Text.Length > 0 Then
            Dim alItem As New ArrayList()
            alItem.Add(dataItem)
            data.alItem = alItem
        End If

        data.UserName = tbUserSearch.Text.Replace("*", "%")
        If tbLastUpdatedSearch.Text = String.Empty Then
            data.LastUpdated = "28 Jan 1900"
        Else
            data.LastUpdated = tbLastUpdatedSearch.Text
        End If

        Return data
    End Function

    Private Sub p_populateSearchResult(Optional bSelectAll As Boolean = False)
        If Session(SESSION_SEARCH_RESULT_POD) Is Nothing Then Exit Sub
        Dim alSearchResult As ArrayList = Session(SESSION_SEARCH_RESULT_POD)
        ClearTableRows(tblDetail)
        For Each data As ProofOfDeliveryManager.DataPOD In alSearchResult
            p_populateTableRows(data, bSelectAll)
        Next
    End Sub

    Private Function p_compileItemList(alItem As ArrayList, sColumnName As String) As String
        Dim sItem As String = String.Empty

        For Each data As ProofOfDeliveryManager.DataPODItem In alItem
            If sColumnName = NameOf(ProofOfDeliveryManager.DataPODItem.Hawb) Then
                sItem += data.Hawb & "; "
            ElseIf sColumnName = NameOf(ProofOfDeliveryManager.DataPODItem.ContainerNumber) Then
                sItem += data.ContainerNumber & "; "
            End If
        Next

        Return sItem
    End Function

    Private Sub p_populateTableRows(data As ProofOfDeliveryManager.DataPOD, bSelectAll As Boolean)
        Dim oRow As New TableRow
        oRow.VerticalAlign = VerticalAlign.Top

        oRow.Cells.Add(CreateCheckBoxCell(data.ID, bSelectAll))
        oRow.Cells.Add(CreateLabelCell(data.ID))
        oRow.Cells.Add(CreateLabelCell(data.HandoverType))
        oRow.Cells.Add(CreateLabelCell(data.ReceivedWho))
        oRow.Cells.Add(CreateLabelCell(data.Department))
        oRow.Cells.Add(CreateLabelCell(data.Remark, 250))
        oRow.Cells.Add(CreateLabelCell(p_compileItemList(data.alItem, NameOf(ProofOfDeliveryManager.DataPODItem.Hawb))))
        oRow.Cells.Add(CreateLabelCell(p_compileItemList(data.alItem, NameOf(ProofOfDeliveryManager.DataPODItem.ContainerNumber))))

        If data.Latitude.Length > 0 And data.Longitude.Length > 0 Then
            oRow.Cells.Add(CreateHyperlinkCell("See in Google Maps", String.Format("https://www.google.com/maps/place/{0},{1}", data.Latitude, data.Longitude)))
        Else
            oRow.Cells.Add(CreateLabelCell("N/A"))
        End If

        '<asp:TableHeaderCell Text="Device Info" />
        '<asp:TableHeaderCell>&nbsp</asp:TableHeaderCell>
        'oRow.Cells.Add(CreateLabelCell(data.DeviceInfo, 150))

        oRow.Cells.Add(CreateLabelCell(data.UserName))
        oRow.Cells.Add(CreateLabelCell(data.LastUpdated.ToString("dd MMM yyyy")))

        If data.imgSignature.Length > 0 Then oRow.Cells.Add(CreateImageCell(Convert.ToBase64String(data.imgSignature), 0, 0))
        'If ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE Then oRow.Cells.Add(CreateImageCell(Convert.ToBase64String(data.imgPicture)))

        tblDetail.Rows.Add(oRow)
    End Sub

    Private Sub ibtDeleteList_Click(sender As Object, e As ImageClickEventArgs) Handles ibtDeleteList.Click
        If Session(SESSION_SEARCH_RESULT_POD) Is Nothing Then Exit Sub
        If Not SessionDataRoleItem(Me.Page.GetType().Name).bDelete Then
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            Exit Sub
        End If

        Dim alSelectedData As ArrayList = p_collectingSelectedData()

        If alSelectedData.Count = 0 Then
            PopupAlertMessage(Me.Page, "No Selected Data")
        Else
            For Each data As ProofOfDeliveryManager.DataPOD In alSelectedData
                Try
                    Dim oManager As New ProofOfDeliveryManager
                    oManager.Delete(data, Session(SESSION_FILE_CONNECTION_STRING))
                Catch ex As Exception
                    Throw
                End Try
            Next
        End If

        p_searchAndPopulate()
    End Sub

    Private Function p_getDataContainer(xData As ContainersManager.DataContainers) As ContainersManager.DataContainers
        Try
            Return New ContainersManager().GetData(xData, Session(SESSION_FILE_CONNECTION_STRING))
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function p_collectingSelectedData() As ArrayList
        Dim alSelectedId As ArrayList = CollectingSelectedRowsID(tblDetail)

        Dim alDataFromList As New ArrayList
        For Each sID As String In alSelectedId
            Dim dataFromList As ProofOfDeliveryManager.DataPOD = p_getDataFromList(sID)
            If Not dataFromList.ID = String.Empty Then
                alDataFromList.Add(dataFromList)
            End If

        Next
        Return alDataFromList
    End Function

    Private Function p_getDataFromList(sId As String) As ProofOfDeliveryManager.DataPOD
        Dim dataFromList As New ProofOfDeliveryManager.DataPOD

        Dim alSearchResult As ArrayList = Session(SESSION_SEARCH_RESULT_POD)
        For Each data As ProofOfDeliveryManager.DataPOD In alSearchResult
            If data.ID = sId Then
                dataFromList = data
                Return dataFromList
            End If
        Next

        Return dataFromList
    End Function

    Private Sub ibtExport_Click(sender As Object, e As ImageClickEventArgs) Handles ibtExport.Click
        If Session(SESSION_SEARCH_RESULT_POD) Is Nothing Then Exit Sub
        If Not SessionDataRoleItem(Me.Page.GetType().Name).bExport Then
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            Exit Sub
        End If

        Dim alSelectedData As ArrayList = p_collectingSelectedData()
        If alSelectedData.Count = 0 Then
            PopupAlertMessage(Me.Page, "No Selected Data")
        Else
            Dim sFilename As String = String.Format("ContainerProofOfDelivery_{0}.xlsx", New SystemManager().SuffixDateNow())
            Dim sPath As String = HttpContext.Current.Server.MapPath(FOLDER_TEMP_FILE & sFilename)

            Try
                p_createExcelFile(sPath, alSelectedData)
                p_downloadAndDeleteSource(sFilename, sPath)
            Catch ex As Exception
                Throw
            End Try
        End If
    End Sub

    Private Sub p_createExcelFile(sPath As String, alSelectedData As ArrayList)
        Dim sheetExcel As New OpenXmlManager.DataSheet
        sheetExcel.SheetName = "Data_" & Me.Page.GetType().Name.Replace("_aspx", "")

        sheetExcel.alHeader.Add(SetExcelTitle("CONTAINER PROOF OF DELIVERY"))
        sheetExcel.alHeader.Add(SetExcelSubtitle(String.Empty, String.Empty)) 'add empty row for separator

        sheetExcel.alHeader.Add(SetExcelSubtitle("Search Criteria :", String.Empty))
        If Not tbIdSearch.Text.Trim = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("ID :", tbIdSearch.Text))
        If Not ddHandoverTypeSearch.Text.Trim = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Handover Type :", ddHandoverTypeSearch.Text))
        If Not tbReceivedSearch.Text = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Received By :", tbReceivedSearch.Text))
        If Not tbDepartmentSearch.Text = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Department :", tbDepartmentSearch.Text))
        If Not tbRemarkSearch.Text.Trim = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Remark :", tbRemarkSearch.Text))
        If Not tbHawbSearch.Text = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Item :", tbHawbSearch.Text))
        If Not tbContainerNumberSearch.Text = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Item :", tbContainerNumberSearch.Text))
        If Not tbUserSearch.Text.Trim = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("User :", tbUserSearch.Text))
        If Not tbLastUpdatedSearch.Text = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Last Updated :", tbLastUpdatedSearch.Text))
        sheetExcel.alHeader.Add(SetExcelSubtitle(String.Empty, String.Empty)) 'add empty row for separator

        sheetExcel.alHeader.Add(SetExcelColumnHeader("ID"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("Handover Type"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("Received By"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("Department"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("Remark"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("List HAWB"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("List Container"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("User"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("Last Updated"))
        If ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE Then
            sheetExcel.alHeader.Add(SetExcelColumnHeader("Signature"))
            'sheetExcel.alHeader.Add(SetExcelColumnHeader("Picture"))
        End If

        For Each data As ProofOfDeliveryManager.DataPOD In alSelectedData
            Dim alRow As New ArrayList
            alRow.Add(SetExcelDetailContent(data.ID))
            alRow.Add(SetExcelDetailContent(data.HandoverType))
            alRow.Add(SetExcelDetailContent(data.ReceivedWho))
            alRow.Add(SetExcelDetailContent(data.Department))
            alRow.Add(SetExcelDetailContent(data.Remark))
            alRow.Add(SetExcelDetailContent(p_compileItemList(data.alItem, NameOf(ProofOfDeliveryManager.DataPODItem.Hawb))))
            alRow.Add(SetExcelDetailContent(p_compileItemList(data.alItem, NameOf(ProofOfDeliveryManager.DataPODItem.ContainerNumber))))
            alRow.Add(SetExcelDetailContent(data.UserName))
            'error : alRow.Add(p_setDetailContent(data.LastUpdated.ToString("dd MMM yyyy hh:mm:ss"), OpenXmlManager.CellFormatCollection.DateAndTime))
            alRow.Add(SetExcelDetailContent(data.LastUpdated.ToString("dd MMM yyyy hh:mm:ss")))
            'n/a :If ddSignatureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW Then alRow.Add(p_setDetailContent(data.Picture))
            sheetExcel.alListDetail.Add(alRow)
        Next

        sheetExcel.alFooter.Add(SetExcelFooter(String.Empty, String.Empty)) 'add empty row for separator
        sheetExcel.alFooter.Add(SetExcelFooter("EOF", String.Empty))

        Dim alSheets As New ArrayList
        alSheets.Add(sheetExcel)

        Dim o As New OpenXmlManager
        o.Create(sPath, alSheets)
    End Sub

    Private Sub p_downloadAndDeleteSource(sFilename As String, sPath As String)
        DownloadFile(sFilename)
        If File.Exists(sPath) Then File.Delete(sPath)
    End Sub

    Private Sub cbDetail_CheckedChanged(sender As Object, e As EventArgs) Handles cbDetail.CheckedChanged
        p_populateSearchResult(cbDetail.Checked)
    End Sub
End Class