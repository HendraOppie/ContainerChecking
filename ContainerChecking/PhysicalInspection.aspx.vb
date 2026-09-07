Imports System.IO
Imports AIT.ContainerCheck
Imports AIT.SM

Public Class PhysicalInspection
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            p_setOptionEnabledSearchList()
            p_setListLocation()
            p_setListItem()
        End If

        p_populateSearchResult()
    End Sub

    Private Sub p_setListItem()
        ddItemSearch.Items.Clear()
        ddItemSearch.Items.Add(String.Empty)
        Dim alItems As New ArrayList
        If Session(SESSION_DATA_LIST_CHECK_ITEMS) Is Nothing Then
            Try
                alItems = New PhysicalCheckItemManager().GetList(Session(SESSION_FILE_CONNECTION_STRING))
                Session(SESSION_DATA_LIST_CHECK_ITEMS) = alItems
            Catch ex As Exception
                Throw
            End Try
        Else
            alItems = Session(SESSION_DATA_LIST_CHECK_ITEMS)
        End If

        If alItems.Count > 0 Then
            For Each dt As PhysicalCheckItemManager.DataPhysicalCheckItem In alItems
                ddItemSearch.Items.Add(dt.Name)
            Next
            'ddItemSearch.Text = ddLocation.Items(0).Text
        End If
    End Sub

    Private Sub p_setListLocation()
        ddLocationSearch.Items.Clear()
        ddLocationSearch.Items.Add(String.Empty)
        Dim alLocations As New ArrayList
        If Session(SESSION_DATA_LIST_LOCATIONS) Is Nothing Then
            Try
                alLocations = New LocationsManager().GetListActive(Session(SESSION_FILE_CONNECTION_STRING))
                Session(SESSION_DATA_LIST_LOCATIONS) = alLocations
            Catch ex As Exception
                Throw
            End Try
        Else
            alLocations = Session(SESSION_DATA_LIST_LOCATIONS)
        End If

        If alLocations.Count > 0 Then
            For Each dt As LocationsManager.DataLocations In alLocations
                ddLocationSearch.Items.Add(dt.Name)
            Next
            'ddLocation.Text = ddLocation.Items(0).Text
        End If
    End Sub

    Private Sub p_setOptionEnabledSearchList()
        ddCheckedSearch.Items.Clear()
        ddCheckedSearch.Items.Add(String.Empty)
        ddCheckedSearch.Items.Add(OPTIONAL_SEARCH_NO)
        ddCheckedSearch.Items.Add(OPTIONAL_SEARCH_YES)

        ddBrokenSearch.Items.Clear()
        ddBrokenSearch.Items.Add(String.Empty)
        ddBrokenSearch.Items.Add(OPTIONAL_SEARCH_NO)
        ddBrokenSearch.Items.Add(OPTIONAL_SEARCH_YES)

        ddPictureSearch.Items.Clear()
        ddPictureSearch.Items.Add(OPTIONAL_SEARCH_PICTURE_SHOW_THUMBNAIL)
        ddPictureSearch.Items.Add(OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE)
    End Sub

    Private Sub ibtClearSearch_Click(sender As Object, e As ImageClickEventArgs) Handles ibtClearSearch.Click
        tbHawbSearch.Text = String.Empty
        tbContainerSearch.Text = String.Empty
        ddLocationSearch.Text = String.Empty
        ddItemSearch.Text = String.Empty
        ddCheckedSearch.Text = String.Empty
        ddBrokenSearch.Text = String.Empty
        tbRemarkSearch.Text = String.Empty
        tbUserSearch.Text = String.Empty
        tbLastUpdatedSearch.Text = String.Empty
        ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW_THUMBNAIL
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
        If tbContainerSearch.Text.Trim = String.Empty _
            And tbHawbSearch.Text.Trim = String.Empty _
            And ddLocationSearch.Text = String.Empty _
            And ddItemSearch.Text = String.Empty _
            And ddCheckedSearch.Text = String.Empty _
            And ddBrokenSearch.Text = String.Empty _
            And tbRemarkSearch.Text.Trim = String.Empty _
            And tbUserSearch.Text.Trim = String.Empty _
            And tbLastUpdatedSearch.Text.Trim = String.Empty _
            Then
            tbLastUpdatedSearch.Text = DateTime.Now.ToString("dd MMM yyyy")
        End If

        If ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE _
            And (tbContainerSearch.Text.Replace("*", "").Replace("%", "").Trim = String.Empty _
            Or ddLocationSearch.Text.Trim = String.Empty) Then
            PopupAlertMessage(Me.Page, "Please narrow your search by minumum specify Container Number and Location")
            Return False
        End If

        Return True
    End Function

    Private Sub p_searchAndPopulate()
        Try
            Dim dataSearchCriteria As New PhysicalConditionManager.DataPhysicalConditionSearchCriteria
            If Not ddCheckedSearch.Text = String.Empty Then dataSearchCriteria.isCheckedSealedInclude = True
            If Not ddBrokenSearch.Text = String.Empty Then dataSearchCriteria.isBrokenInclude = True
            If ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE Then dataSearchCriteria.isPictureInclude = True

            Dim alSearchRessult As ArrayList = New PhysicalConditionManager().GetSearch(p_collectSearchingCriteria, dataSearchCriteria, Session(SESSION_FILE_CONNECTION_STRING))
            Session(SESSION_SEARCH_RESULT_PHYSICAL_CONDITIONS) = alSearchRessult
            p_populateSearchResult()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Function p_collectSearchingCriteria() As PhysicalConditionManager.DataPhysicalCondition
        Dim data As New PhysicalConditionManager.DataPhysicalCondition
        data.Hawb = tbHawbSearch.Text.Replace("*", "%")
        data.ContainerNumber = tbContainerSearch.Text.Replace("*", "%")
        data.LocationName = ddLocationSearch.Text
        data.ItemName = ddItemSearch.Text
        data.Remark = tbRemarkSearch.Text.Replace("*", "%")

        If ddCheckedSearch.Text = "YES" Then
            data.isCheckedSealed = True
        ElseIf ddCheckedSearch.Text = "NO" Then
            data.isCheckedSealed = False
        End If

        If ddBrokenSearch.Text = "YES" Then
            data.isBroken = True
        ElseIf ddBrokenSearch.Text = "NO" Then
            data.isBroken = False
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
        If Session(SESSION_SEARCH_RESULT_PHYSICAL_CONDITIONS) Is Nothing Then Exit Sub
        Dim alSearchResult As ArrayList = Session(SESSION_SEARCH_RESULT_PHYSICAL_CONDITIONS)
        ClearTableRows(tblDetail)
        For Each data As PhysicalConditionManager.DataPhysicalCondition In alSearchResult
            p_populateTableRows(data, bSelectAll)
        Next
    End Sub

    Private Sub p_populateTableRows(data As PhysicalConditionManager.DataPhysicalCondition, bSelectAll As Boolean)
        Dim oRow As New TableRow
        oRow.VerticalAlign = VerticalAlign.Top

        oRow.Cells.Add(CreateCheckBoxCell(String.Format("{0}_{1}_{2}_{3}", data.Hawb, data.ContainerNumber, data.LocationID, data.ItemID), bSelectAll))
        oRow.Cells.Add(CreateLabelCell(data.Hawb))
        oRow.Cells.Add(CreateLabelCell(data.ContainerNumber))
        oRow.Cells.Add(CreateLabelCell(data.LocationName))
        oRow.Cells.Add(CreateLabelCell(data.ItemName))

        Dim sValue As String = OPTIONAL_SEARCH_NO
        If data.isCheckedSealed Then sValue = OPTIONAL_SEARCH_YES
        oRow.Cells.Add(CreateLabelCell(sValue))

        sValue = OPTIONAL_SEARCH_NO
        If data.isBroken Then sValue = OPTIONAL_SEARCH_YES
        oRow.Cells.Add(CreateLabelCell(sValue))

        oRow.Cells.Add(CreateLabelCell(data.Remark, 250))
        oRow.Cells.Add(CreateLabelCell(data.UserName))
        oRow.Cells.Add(CreateLabelCell(data.LastUpdated.ToString("dd MMM yyyy")))

        If ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE Then
            oRow.Cells.Add(CreateImageCell(Convert.ToBase64String(data.Picture), 0, 0))
        Else
            oRow.Cells.Add(CreateImageCell(Convert.ToBase64String(data.Thumbnail), 0, 0))
        End If

        tblDetail.Rows.Add(oRow)
    End Sub

    Private Sub ibtDeleteList_Click(sender As Object, e As ImageClickEventArgs) Handles ibtDeleteList.Click
        If Session(SESSION_SEARCH_RESULT_PHYSICAL_CONDITIONS) Is Nothing Then Exit Sub
        If Not SessionDataRoleItem(Me.Page.GetType().Name).bDelete Then
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            Exit Sub
        End If

        Dim alSelectedData As ArrayList = p_collectingSelectedData()

        If alSelectedData.Count = 0 Then
            PopupAlertMessage(Me.Page, "No Selected Data")
        Else
            For Each data As PhysicalConditionManager.DataPhysicalCondition In alSelectedData
                Dim dataContainer As New ContainersManager.DataContainers
                dataContainer.Hawb = data.Hawb
                dataContainer.ContainerNumber = data.ContainerNumber

                If p_getDataContainer(dataContainer).Status = ContainersManager.ContainerStatus.DELIVERED.ToString Then
                    PopupAlertMessage(Me.Page, String.Format("Cannot Delete Data {0}. Status already Delivered", data.ContainerNumber))
                    Exit For
                Else
                    Try
                        Dim oManager As New PhysicalConditionManager
                        oManager.Delete(data, Session(SESSION_FILE_CONNECTION_STRING))
                    Catch ex As Exception
                        Throw
                    End Try
                End If
            Next
        End If

        cbDetail.Checked = False
        p_searchAndPopulate()
    End Sub

    Private Function p_getDataContainer(xData As ContainersManager.DataContainers) As ContainersManager.DataContainers
        Try
            Return New ContainersManager().GetData(xData, Session(SESSION_FILE_CONNECTION_STRING))
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function p_setDataTitle(sValue As String) As OpenXmlManager.DataHeader
        Dim oCell As New OpenXmlManager.DataHeader
        oCell.Type = OpenXmlManager.HeaderType.Title
        oCell.Value = sValue
        oCell.FormatCollection = OpenXmlManager.CellFormatCollection.Bold14
        Return oCell
    End Function

    Private Function p_setSubtitle(sSubtitle As String, sValue As String) As OpenXmlManager.DataHeader
        Dim oCell As New OpenXmlManager.DataHeader
        oCell.Type = OpenXmlManager.HeaderType.Subtitle
        oCell.Subtitle = sSubtitle
        oCell.Value = sValue
        oCell.FormatCollection = OpenXmlManager.CellFormatCollection.Bold
        Return oCell
    End Function

    Private Function p_setColumnHeader(sValue As String) As OpenXmlManager.DataHeader
        Dim oCell As New OpenXmlManager.DataHeader
        oCell.Type = OpenXmlManager.HeaderType.Colomn
        oCell.Value = sValue
        oCell.FormatCollection = OpenXmlManager.CellFormatCollection.BoldBorder
        Return oCell
    End Function

    Private Function p_setDetailContent(sValue As String, Optional vCellFormat As OpenXmlManager.CellFormatCollection = OpenXmlManager.CellFormatCollection.Border, Optional vType As OpenXmlManager.ContentDataType = OpenXmlManager.ContentDataType.String) As OpenXmlManager.DataDetail
        Dim oCell As New OpenXmlManager.DataDetail
        oCell.Value = sValue
        oCell.DataType = vType
        oCell.FormatCollection = vCellFormat
        Return oCell
    End Function

    Private Function p_setFooter(sTitle As String, sValue As String) As OpenXmlManager.DataFooter
        Dim oCell As New OpenXmlManager.DataFooter
        oCell.Title = sTitle
        oCell.Value = sValue
        oCell.FormatCollection = OpenXmlManager.CellFormatCollection.Bold
        Return oCell
    End Function

    Private Function p_collectingSelectedData() As ArrayList
        Dim alSelectedId As ArrayList = CollectingSelectedRowsID(tblDetail)

        Dim alDataFromList As New ArrayList
        For Each sID As String In alSelectedId
            Dim aID() As String = sID.Split("_")
            Dim sContainerNumber As String = String.Empty
            Dim sHawb As String = String.Empty
            Dim sItem As String = String.Empty

            Dim data As New PhysicalConditionManager.DataPhysicalCondition
            If aID.Count = 4 Then
                data.Hawb = aID(0)
                data.ContainerNumber = aID(1)
                data.LocationID = aID(2)
                data.ItemID = aID(3)
            End If

            Dim dataFromList As PhysicalConditionManager.DataPhysicalCondition = p_getDataFromList(data)
            If Not dataFromList.ContainerNumber = String.Empty Then
                alDataFromList.Add(dataFromList)
            End If

        Next
        Return alDataFromList
    End Function

    Private Function p_getDataFromList(xData As PhysicalConditionManager.DataPhysicalCondition) As PhysicalConditionManager.DataPhysicalCondition
        Dim dataFromList As New PhysicalConditionManager.DataPhysicalCondition

        Dim alSearchResult As ArrayList = Session(SESSION_SEARCH_RESULT_PHYSICAL_CONDITIONS)
        For Each data As PhysicalConditionManager.DataPhysicalCondition In alSearchResult
            If data.ContainerNumber = xData.ContainerNumber And data.Hawb = xData.Hawb And data.LocationID = xData.LocationID And data.ItemID = xData.ItemID Then
                dataFromList = data
                Return dataFromList
            End If
        Next

        Return dataFromList
    End Function

    Private Sub ibtExport_Click(sender As Object, e As ImageClickEventArgs) Handles ibtExport.Click
        If Session(SESSION_SEARCH_RESULT_PHYSICAL_CONDITIONS) Is Nothing Then Exit Sub
        If Not SessionDataRoleItem(Me.Page.GetType().Name).bExport Then
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            Exit Sub
        End If

        Dim alSelectedData As ArrayList = p_collectingSelectedData()
        If alSelectedData.Count = 0 Then
            PopupAlertMessage(Me.Page, "No Selected Data")
        Else
            Dim sFilename As String = String.Format("ContainerPhysicalInspection_{0}.xlsx", New SystemManager().SuffixDateNow())
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

        sheetExcel.alHeader.Add(p_setDataTitle("CONTAINER PHYSICAL INSPECTION"))
        sheetExcel.alHeader.Add(p_setSubtitle(String.Empty, String.Empty)) 'add empty row for separator

        sheetExcel.alHeader.Add(p_setSubtitle("Search Criteria :", String.Empty))
        If Not tbHawbSearch.Text.Trim = String.Empty Then sheetExcel.alHeader.Add(p_setSubtitle("HAWB :", tbHawbSearch.Text))
        If Not tbContainerSearch.Text.Trim = String.Empty Then sheetExcel.alHeader.Add(p_setSubtitle("Container :", tbContainerSearch.Text))
        If Not ddLocationSearch.Text = String.Empty Then sheetExcel.alHeader.Add(p_setSubtitle("Location :", ddLocationSearch.Text))
        If Not ddItemSearch.Text = String.Empty Then sheetExcel.alHeader.Add(p_setSubtitle("Item :", ddItemSearch.Text))
        If Not ddCheckedSearch.Text = String.Empty Then sheetExcel.alHeader.Add(p_setSubtitle("Is Checked :", ddCheckedSearch.Text))
        If Not ddBrokenSearch.Text = String.Empty Then sheetExcel.alHeader.Add(p_setSubtitle("Is Broken :", ddBrokenSearch.Text))
        If Not tbRemarkSearch.Text.Trim = String.Empty Then sheetExcel.alHeader.Add(p_setSubtitle("Remark :", tbRemarkSearch.Text))
        If Not tbUserSearch.Text.Trim = String.Empty Then sheetExcel.alHeader.Add(p_setSubtitle("User :", tbUserSearch.Text))
        If Not tbLastUpdatedSearch.Text = String.Empty Then sheetExcel.alHeader.Add(p_setSubtitle("Last Updated :", tbLastUpdatedSearch.Text))
        sheetExcel.alHeader.Add(p_setSubtitle(String.Empty, String.Empty)) 'add empty row for separator

        sheetExcel.alHeader.Add(p_setColumnHeader("HAWB"))
        sheetExcel.alHeader.Add(p_setColumnHeader("Container Number"))
        sheetExcel.alHeader.Add(p_setColumnHeader("Location"))
        sheetExcel.alHeader.Add(p_setColumnHeader("Item"))
        sheetExcel.alHeader.Add(p_setColumnHeader("Is Checked"))
        sheetExcel.alHeader.Add(p_setColumnHeader("Is Broken"))
        sheetExcel.alHeader.Add(p_setColumnHeader("Remark"))
        sheetExcel.alHeader.Add(p_setColumnHeader("User"))
        sheetExcel.alHeader.Add(p_setColumnHeader("Last Updated"))
        If ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE Then sheetExcel.alHeader.Add(p_setColumnHeader("Photo"))

        For Each data As PhysicalConditionManager.DataPhysicalCondition In alSelectedData
            Dim alRow As New ArrayList
            alRow.Add(p_setDetailContent(data.Hawb))
            alRow.Add(p_setDetailContent(data.ContainerNumber))
            alRow.Add(p_setDetailContent(data.LocationName))
            alRow.Add(p_setDetailContent(data.ItemName))

            If data.isCheckedSealed Then
                alRow.Add(p_setDetailContent(OPTIONAL_SEARCH_YES))
            Else
                alRow.Add(p_setDetailContent(OPTIONAL_SEARCH_NO))
            End If

            If data.isBroken Then
                alRow.Add(p_setDetailContent(OPTIONAL_SEARCH_YES))
            Else
                alRow.Add(p_setDetailContent(OPTIONAL_SEARCH_NO))
            End If

            alRow.Add(p_setDetailContent(data.Remark))
            alRow.Add(p_setDetailContent(data.UserName))
            'error : alRow.Add(p_setDetailContent(data.LastUpdated.ToString("dd MMM yyyy hh:mm:ss"), OpenXmlManager.CellFormatCollection.DateAndTime))
            alRow.Add(p_setDetailContent(data.LastUpdated.ToString("dd MMM yyyy hh:mm:ss")))
            'n/a :If ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW Then alRow.Add(p_setDetailContent(data.Picture))
            sheetExcel.alListDetail.Add(alRow)
        Next

        sheetExcel.alFooter.Add(p_setFooter(String.Empty, String.Empty)) 'add empty row for separator
        sheetExcel.alFooter.Add(p_setFooter("EOF", String.Empty))

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