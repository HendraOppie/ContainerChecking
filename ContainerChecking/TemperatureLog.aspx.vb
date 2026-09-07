Imports System.IO
Imports AIT.ContainerCheck
Imports AIT.SM

Public Class TemperatureLog
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            p_setOptionEnabledSearchList()
            p_setListLocation()
        End If

        p_populateSearchResult()
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
        ddPictureSearch.Items.Clear()
        ddPictureSearch.Items.Add(OPTIONAL_SEARCH_PICTURE_SHOW_THUMBNAIL)
        ddPictureSearch.Items.Add(OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE)
    End Sub

    Private Sub ibtClearSearch_Click(sender As Object, e As ImageClickEventArgs) Handles ibtClearSearch.Click
        tbHawbSearch.Text = String.Empty
        tbContainerSearch.Text = String.Empty
        ddLocationSearch.Text = String.Empty
        tbSetTemperature.Text = String.Empty
        tbTemperature.Text = String.Empty
        tbBattery.Text = String.Empty
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
            And tbSetTemperature.Text = String.Empty _
            And tbTemperature.Text = String.Empty _
            And tbBattery.Text = String.Empty _
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
            Dim dataSearchCriteria As New OtherConditionsManager.DataOtherConditionSearchCriteria
            If ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE Then dataSearchCriteria.isPictureInclude = True

            Dim data As OtherConditionsManager.DataOtherConditions = p_collectSearchingCriteria(dataSearchCriteria)
            Dim alSearchRessult As ArrayList = New OtherConditionsManager().GetSearch(data, dataSearchCriteria, Session(SESSION_FILE_CONNECTION_STRING))
            Session(SESSION_SEARCH_RESULT_TEMPERATURE_LOG) = alSearchRessult
            p_populateSearchResult()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Function p_collectSearchingCriteria(ByRef dataSearchCriteria As OtherConditionsManager.DataOtherConditionSearchCriteria) As OtherConditionsManager.DataOtherConditions
        Dim data As New OtherConditionsManager.DataOtherConditions
        data.Hawb = tbHawbSearch.Text.Replace("*", "%")
        data.ContainerNumber = tbContainerSearch.Text.Replace("*", "%")
        data.LocationName = ddLocationSearch.Text

        Dim sOperator As String = String.Empty, sValue As String = String.Empty

        If Not tbSetTemperature.Text.Trim = String.Empty Then
            p_getOperator(tbSetTemperature.Text.Trim, sOperator, sValue)
            dataSearchCriteria.OperatorSetTemperature = sOperator
            data.SetTemperature = sValue
        End If

        If Not tbTemperature.Text.Trim = String.Empty Then
            p_getOperator(tbTemperature.Text.Trim, sOperator, sValue)
            dataSearchCriteria.OperatorTemperature = sOperator
            data.Temperature = sValue
        End If

        If Not tbBattery.Text.Trim = String.Empty Then
            p_getOperator(tbBattery.Text.Trim, sOperator, sValue)
            dataSearchCriteria.OperatorBattery = sOperator
            data.Battery = sValue
        End If

        data.Remark = tbRemarkSearch.Text.Replace("*", "%")
        data.UserName = tbUserSearch.Text.Replace("*", "%")

        If tbLastUpdatedSearch.Text = String.Empty Then
            data.LastUpdated = "28 Jan 1900"
        Else
            data.LastUpdated = tbLastUpdatedSearch.Text
        End If

        Return data
    End Function

    Private Sub p_getOperator(sContent As String, ByRef sOperator As String, ByRef sValue As String)
        sContent = sContent.Trim
        If sContent.Contains("=") Then
            sOperator = "="
            sValue = sContent.Replace("=", "").Trim
        ElseIf sContent.Contains(">") Then
            sOperator = ">"
            sValue = sContent.Replace(">", "").Trim
        ElseIf sContent.Contains("<") Then
            sOperator = "<"
            sValue = sContent.Replace("<", "").Trim
        ElseIf sContent.Contains(">=") Then
            sOperator = ">="
            sValue = sContent.Replace(">=", "").Trim
        ElseIf sContent.Contains("<=") Then
            sOperator = "<="
            sValue = sContent.Replace("<=", "").Trim
        Else
            sOperator = "="
            sValue = sContent
        End If
    End Sub

    Private Sub p_populateSearchResult(Optional bSelectAll As Boolean = False)
        If Session(SESSION_SEARCH_RESULT_TEMPERATURE_LOG) Is Nothing Then Exit Sub
        Dim alSearchResult As ArrayList = Session(SESSION_SEARCH_RESULT_TEMPERATURE_LOG)
        ClearTableRows(tblDetail)
        For Each data As OtherConditionsManager.DataOtherConditions In alSearchResult
            p_populateTableRows(data, bSelectAll)
        Next
    End Sub

    Private Sub p_populateTableRows(data As OtherConditionsManager.DataOtherConditions, bSelectAll As Boolean)
        Dim oRow As New TableRow
        oRow.VerticalAlign = VerticalAlign.Top

        oRow.Cells.Add(CreateCheckBoxCell(data.ID, bSelectAll))
        oRow.Cells.Add(CreateLabelCell(data.Hawb))
        oRow.Cells.Add(CreateLabelCell(data.ContainerNumber))
        oRow.Cells.Add(CreateLabelCell(data.LocationName))
        oRow.Cells.Add(CreateLabelCell(data.SetTemperature))
        oRow.Cells.Add(CreateLabelCell(data.Temperature))
        oRow.Cells.Add(CreateLabelCell(data.Battery))
        oRow.Cells.Add(CreateLabelCell(data.Remark, 250))
        oRow.Cells.Add(CreateLabelCell(data.UserName))
        oRow.Cells.Add(CreateLabelCell(data.LastUpdated.ToString("dd MMM yyyy")))

        If ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE Then
            oRow.Cells.Add(CreateImageCell(Convert.ToBase64String(data.imgContainer), 0, 0))
            oRow.Cells.Add(CreateImageCell(Convert.ToBase64String(data.imgTemperature), 0, 0))
        Else
            oRow.Cells.Add(CreateImageCell(Convert.ToBase64String(data.thumbContainer), 0, 0))
            oRow.Cells.Add(CreateImageCell(Convert.ToBase64String(data.thumbTemperature), 0, 0))
        End If

        tblDetail.Rows.Add(oRow)
    End Sub

    Private Sub ibtDeleteList_Click(sender As Object, e As ImageClickEventArgs) Handles ibtDeleteList.Click
        If Session(SESSION_SEARCH_RESULT_TEMPERATURE_LOG) Is Nothing Then Exit Sub
        If Not SessionDataRoleItem(Me.Page.GetType().Name).bDelete Then
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            Exit Sub
        End If

        Dim alSelectedData As ArrayList = p_collectingSelectedData()

        If alSelectedData.Count = 0 Then
            PopupAlertMessage(Me.Page, "No Selected Data")
        Else
            For Each data As OtherConditionsManager.DataOtherConditions In alSelectedData
                Dim dataContainer As New ContainersManager.DataContainers
                dataContainer.Hawb = data.Hawb
                dataContainer.ContainerNumber = data.ContainerNumber

                If p_getDataContainer(dataContainer).Status = ContainersManager.ContainerStatus.DELIVERED.ToString Then
                    PopupAlertMessage(Me.Page, String.Format("Cannot Delete Data {0}. Status already Delivered", data.ContainerNumber))
                    Exit For
                Else
                    Try
                        Dim oManager As New OtherConditionsManager
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

    Private Function p_collectingSelectedData() As ArrayList
        Dim alSelectedId As ArrayList = CollectingSelectedRowsID(tblDetail)

        Dim alDataFromList As New ArrayList
        For Each sID As String In alSelectedId
            Dim dataFromList As OtherConditionsManager.DataOtherConditions = p_getDataFromList(sID)
            If Not dataFromList.ID = String.Empty Then
                alDataFromList.Add(dataFromList)
            End If

        Next
        Return alDataFromList
    End Function

    Private Function p_getDataFromList(sId As String) As OtherConditionsManager.DataOtherConditions
        Dim dataFromList As New OtherConditionsManager.DataOtherConditions

        Dim alSearchResult As ArrayList = Session(SESSION_SEARCH_RESULT_TEMPERATURE_LOG)
        For Each data As OtherConditionsManager.DataOtherConditions In alSearchResult
            If data.ID = sId Then
                dataFromList = data
                Return dataFromList
            End If
        Next

        Return dataFromList
    End Function

    Private Sub ibtExport_Click(sender As Object, e As ImageClickEventArgs) Handles ibtExport.Click
        If Session(SESSION_SEARCH_RESULT_TEMPERATURE_LOG) Is Nothing Then Exit Sub
        If Not SessionDataRoleItem(Me.Page.GetType().Name).bExport Then
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            Exit Sub
        End If

        Dim alSelectedData As ArrayList = p_collectingSelectedData()
        If alSelectedData.Count = 0 Then
            PopupAlertMessage(Me.Page, "No Selected Data")
        Else
            Dim sFilename As String = String.Format("ContainerTemperatureLog_{0}.xlsx", New SystemManager().SuffixDateNow())
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

        sheetExcel.alHeader.Add(SetExcelTitle("CONTAINER TEMPERATURE AND BATTERY LOG"))
        sheetExcel.alHeader.Add(SetExcelSubtitle(String.Empty, String.Empty)) 'add empty row for separator

        sheetExcel.alHeader.Add(SetExcelSubtitle("Search Criteria :", String.Empty))
        If Not tbHawbSearch.Text.Trim = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("HAWB :", tbHawbSearch.Text))
        If Not tbContainerSearch.Text.Trim = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Container :", tbContainerSearch.Text))
        If Not ddLocationSearch.Text = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Location :", ddLocationSearch.Text))
        If Not tbSetTemperature.Text = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Set Temperature :", tbSetTemperature.Text))
        If Not tbTemperature.Text = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Temperature :", tbTemperature.Text))
        If Not tbBattery.Text = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Battery :", tbBattery.Text))
        If Not tbRemarkSearch.Text.Trim = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Remark :", tbRemarkSearch.Text))
        If Not tbUserSearch.Text.Trim = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("User :", tbUserSearch.Text))
        If Not tbLastUpdatedSearch.Text = String.Empty Then sheetExcel.alHeader.Add(SetExcelSubtitle("Last Updated :", tbLastUpdatedSearch.Text))
        sheetExcel.alHeader.Add(SetExcelSubtitle(String.Empty, String.Empty)) 'add empty row for separator

        sheetExcel.alHeader.Add(SetExcelColumnHeader("HAWB"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("Container Number"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("Location"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("Set Temperature"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("Temperature"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("Battery"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("Remark"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("User"))
        sheetExcel.alHeader.Add(SetExcelColumnHeader("Last Updated"))
        If ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE Then
            sheetExcel.alHeader.Add(SetExcelColumnHeader("Photo Container"))
            sheetExcel.alHeader.Add(SetExcelColumnHeader("Photo Temperature"))
        End If

        For Each data As OtherConditionsManager.DataOtherConditions In alSelectedData
            Dim alRow As New ArrayList
            alRow.Add(SetExcelDetailContent(data.Hawb))
            alRow.Add(SetExcelDetailContent(data.ContainerNumber))
            alRow.Add(SetExcelDetailContent(data.LocationName))
            alRow.Add(SetExcelDetailContent(data.SetTemperature, OpenXmlManager.CellFormatCollection.Dec2Border, OpenXmlManager.ContentDataType.Number))
            alRow.Add(SetExcelDetailContent(data.Temperature, OpenXmlManager.CellFormatCollection.Dec2Border, OpenXmlManager.ContentDataType.Number))
            alRow.Add(SetExcelDetailContent(String.Format("{0}%", data.Battery.ToString())))
            alRow.Add(SetExcelDetailContent(data.Remark))
            alRow.Add(SetExcelDetailContent(data.UserName))
            'error : alRow.Add(p_setDetailContent(data.LastUpdated.ToString("dd MMM yyyy hh:mm:ss"), OpenXmlManager.CellFormatCollection.DateAndTime))
            alRow.Add(SetExcelDetailContent(data.LastUpdated.ToString("dd MMM yyyy hh:mm:ss")))
            'n/a :If ddPictureSearch.Text = OPTIONAL_SEARCH_PICTURE_SHOW Then alRow.Add(p_setDetailContent(data.Picture))
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