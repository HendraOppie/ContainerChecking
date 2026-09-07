Imports AIT.SM

Module GeneralModule

#Region "Public Const"
    Public Const APPS_NAME As String = "Container Check"
    Public Const FILE_CONNECTION_STRING As String = "cs_ContainerCheck.xml"
    Public Const URL_REPORTALWEB As String = "http://agility.id/ReportalWeb/"
    Public Const URL_PARAMETER As String = "urlParameter"
    Public Const URL_PREFIX As String = "http://agility.id/ContainerChecking/"
    Public Const URL_PREFIX_POD As String = URL_PREFIX & "mPOD.aspx?id="
    Public Const URL_PREFIX_USER_PROFILE As String = URL_PREFIX & "mUserProfile.aspx?id="
    Public Const FOLDER_TEMP_FILE As String = "~/Tempe/"
    Public Const FILEPATH_PDF_USERGUIDE As String = "Files/ContainerChecking_UserGuide.pdf"
    Public Const CODE_LOGIN_TO_MOBILE As String = "_mbl"

    Public Const URL_REPORT_SERVER As String = "~/Report/"
    Public Const FOLDER_REPORT_FILE As String = "~/Report/"

    Public Const OPTIONAL_SEARCH_PICTURE_SHOW_THUMBNAIL As String = "Thumbnail"
    Public Const OPTIONAL_SEARCH_PICTURE_SHOW_PICTURE As String = "Picture"
    Public Const OPTIONAL_SEARCH_NO As String = "NO"
    Public Const OPTIONAL_SEARCH_YES As String = "YES"
    Public Const OPTIONAL_SEARCH_FALSE As String = "FALSE"
    Public Const OPTIONAL_SEARCH_TRUE As String = "TRUE"
    Public Const OPTIONAL_SEARCH_DISABLED = "DISABLED"
    Public Const OPTIONAL_SEARCH_ENABLED = "ENABLED"

    Public Const SESSION_FILE_CONNECTION_STRING As String = "fileConnectionString"

    Public Const SESSION_INFO_Header As String = "headerInfo"
    Public Const SESSION_INFO As String = "Info"
    Public Const SESSION_INFO_REDIRECT As String = "InfoRedirect"
    Public Const SESSION_USER_ID As String = "UserId"
    Public Const SESSION_USER_MUST_CHANGE_PASSWORD As String = "UserMustChangePassword"

    Public Const SESSION_DATA_CONTAINERS As String = "DataContainers"
    Public Const SESSION_DATA_CUSTOMERS As String = "DataCustomers"
    Public Const SESSION_DATA_PHYSICAL_CONDITION As String = "DataPhysicalCondition"
    Public Const SESSION_DATA_USER As String = "DataUser"
    Public Const SESSION_DATA_ROLE As String = "DataRole"
    Public Const SESSION_DATA_LATITUDE As String = "ccLatitude"
    Public Const SESSION_DATA_LONGITUDE As String = "ccLongitude"

    Public Const SESSION_DATA_LIST_CHECK_ITEMS As String = "ListCheckItems"
    Public Const SESSION_DATA_LIST_LOCATIONS As String = "ListLocations"
    Public Const SESSION_DATA_LIST_CONTAINERS As String = "ListContainers"
    Public Const SESSION_DATA_LIST_CONTAINERS_SEARCH_RESULT As String = "ListContainerSearchResult"
    Public Const SESSION_DATA_LIST_CONTAINERS_SELECTED As String = "ListContainersSelected"
    Public Const SESSION_DATA_LIST_PHYSICAL_CONDITIONS As String = "ListPhysicalCondition"

    Public Const SESSION_SEARCH_RESULT_USERS As String = "ListUserSearchResult"
    Public Const SESSION_SEARCH_RESULT_PHYSICAL_CONDITIONS As String = "ListPhysicalConditionSearchResult"
    Public Const SESSION_SEARCH_RESULT_TEMPERATURE_LOG As String = "ListPhysicalTemperatureLogSearchResult"
    Public Const SESSION_SEARCH_RESULT_POD As String = "ListPODSearchResult"

    Public Const SESSION_PHYSICAL_INSPECTION_ITEM_SIDE As String = "PhysicalInspectionItemSide"
    Public Const SESSION_PHYSICAL_INSPECTION_ITEM_TYPE As String = "PhysicalInspectionItemType"
    Public Const SESSION_PHYSICAL_INSPECTION_SELECTED_ITEM As String = "PhysicalInspectionSelectedItem"

    Public Const PAGE_LOGIN As String = "Login"
    Public Const PAGE_SIGNUP As String = "SignUp"
    Public Const PAGE_REQUEST_RESET_PASSWORD As String = "RequestResetPassword"
    Public Const PAGE_USER_PROFILE As String = "UserProfile"
    Public Const PAGE_USER_PROFILE_MOBILE As String = "mUserProfile"
    Public Const PAGE_USER As String = "User"
    Public Const PAGE_ROLE As String = "Role"
    Public Const PAGE_MENU As String = "mMenu"
    Public Const PAGE_HOME As String = "Home"
    Public Const PAGE_CONTAINERS As String = "Containers"
    Public Const PAGE_PHYSICAL_INSPECTION As String = "PhysicalInspection"
    Public Const PAGE_PHYSICAL_INSPECTION_MOBILE As String = "mPhysicalInspection"
    Public Const PAGE_TEMPERATURE_LOG As String = "TemperatureLog"
    Public Const PAGE_TEMPERATURE_LOG_MOBILE As String = "mTemperatureLog"
    Public Const PAGE_PHYSICAL_INSPECTION_ITEM_MOBILE As String = "mPhysicalInspectionItem"
    Public Const PAGE_DELIVERY_NOTE_MOBILE As String = "mDeliveryNote"
    Public Const PAGE_POD As String = "ProofOfDelivery"
    Public Const PAGE_POD_MOBILE As String = "mPOD"
    Public Const PAGE_LOCATION As String = "Location"
    Public Const PAGE_CUSTOMER As String = "Customer"
    Public Const PAGE_INFO As String = "Info"
    Public Const PAGE_INFO_MOBILE As String = "mInfo"
    Public Const PAGE_THANK_YOU_MOBILE As String = "mThankYou"
    Public Const PAGE_REPORT_VIEWER As String = "ReportViewer"

    Public Const ROLE_ITEM_ROLE = "/Role"

    Public Const MESSAGE_ALERT_ACCESS_DENIED As String = "Access Denied. Check your Role or contact Administrator"

#End Region

#Region "Public"

    Public Function CollectingSelectedRowsID(oTable As Table) As ArrayList
        Return p_collectingSelectedRowsID(oTable)
    End Function

    Public Sub ClearTableRows(oTable As Table)
        p_clearTableRows(oTable)
    End Sub

    Public Function CreateHyperlinkCell(sText As String, sUrl As String) As TableCell
        Return p_createHyperlinkCell(sText, sUrl)
    End Function

    Public Function CreateLabelCell(sValue As String, Optional iWidth As Int16 = 0) As TableCell
        Return p_createLabelCell(sValue, iWidth)
    End Function

    Public Function CreateImageCell(valueBase64String As String, Optional iHeight As Int16 = 150, Optional iWidth As Int16 = 250) As TableCell
        Return p_createImageCell(valueBase64String, iHeight, iWidth)
    End Function

    Public Function CreateCheckBoxCell(sID As String, Optional bSelect As Boolean = False) As TableCell
        Return p_createCheckBoxCell(sID, bSelect)
    End Function

    Public Sub GotoPage(oPageCurrent As Page, sPageDestination As String, Optional bCheckAccess As Boolean = False)
        If bCheckAccess Then
            If p_sessionDataRoleItem(sPageDestination).bRead Then
                HttpContext.Current.Response.Redirect(p_pageFullname(sPageDestination), True)
            Else
                p_popupAlertMessage(oPageCurrent, MESSAGE_ALERT_ACCESS_DENIED)
            End If
        Else
            HttpContext.Current.Response.Redirect(p_pageFullname(sPageDestination), True)
        End If

    End Sub

    Public Sub TransferPage(sPageDestination As String)
        HttpContext.Current.Server.Transfer(p_pageFullname(sPageDestination))
    End Sub

    Public Function PageFullname(sPageName As String) As String
        Return p_pageFullname(sPageName)
    End Function

    Public Sub PopupConfirmMessage(oPage As Page, sMessage As String)
        p_popupConfirmMessage(oPage, sMessage)
    End Sub

    Public Sub PopulateInfoInPage(oLabelInfo As Label, oPage As Page, sMessage As String, Optional iType As Int16 = 0)
        p_populateInfoInPage(oLabelInfo, oPage, sMessage, iType)
    End Sub

    Public Sub PopupAlertMessageAndRedirect(oPage As Page, sMessage As String, sRedirectPage As String)
        p_popupAlertMessageAndRedirect(oPage, sMessage, sRedirectPage)
    End Sub

    Public Sub PopupAlertMessage(oPage As Page, sMessage As String)
        p_popupAlertMessage(oPage, sMessage)
    End Sub

    Public Function SessionDataUser() As UserManager.DataUser
        If Not System.Web.HttpContext.Current.Session(SESSION_DATA_USER) Is Nothing Then
            Return System.Web.HttpContext.Current.Session(SESSION_DATA_USER)
        End If
        Return New UserManager.DataUser
    End Function

    Public Function SessionDataRole() As RoleManager.DataRole
        If Not System.Web.HttpContext.Current.Session(SESSION_DATA_ROLE) Is Nothing Then
            Return System.Web.HttpContext.Current.Session(SESSION_DATA_ROLE)
        End If
        Return New RoleManager.DataRole
    End Function

    Public Function SessionDataRoleItem(sFormId As String) As RoleManager.DataRoleItem
        Return p_sessionDataRoleItem(sFormId)
    End Function

    Public Sub DownloadFile(originFilename As String, Optional downloadedFilename As String = "")
        p_downloadFile(originFilename, downloadedFilename)
    End Sub

    Public Function SetExcelTitle(sValue As String, Optional vCellFormat As OpenXmlManager.CellFormatCollection = OpenXmlManager.CellFormatCollection.Bold14) As OpenXmlManager.DataHeader
        Return p_setExcelDataTitle(sValue, vCellFormat)
    End Function

    Public Function SetExcelSubtitle(sSubtitle As String, sValue As String, Optional vCellFormat As OpenXmlManager.CellFormatCollection = OpenXmlManager.CellFormatCollection.Bold) As OpenXmlManager.DataHeader
        Return p_setExcelSubtitle(sSubtitle, sValue, vCellFormat)
    End Function

    Public Function SetExcelColumnHeader(sValue As String, Optional vCellFormat As OpenXmlManager.CellFormatCollection = OpenXmlManager.CellFormatCollection.BoldBorder) As OpenXmlManager.DataHeader
        Return p_setExcelColumnHeader(sValue, vCellFormat)
    End Function

    Public Function SetExcelDetailContent(sValue As String, Optional vCellFormat As OpenXmlManager.CellFormatCollection = OpenXmlManager.CellFormatCollection.Border, Optional vType As OpenXmlManager.ContentDataType = OpenXmlManager.ContentDataType.String) As OpenXmlManager.DataDetail
        Return p_setExcelDetailContent(sValue, vCellFormat, vType)
    End Function

    Public Function SetExcelFooter(sTitle As String, sValue As String, Optional vCellFormat As OpenXmlManager.CellFormatCollection = OpenXmlManager.CellFormatCollection.Bold) As OpenXmlManager.DataFooter
        Return p_setExcelFooter(sTitle, sValue, vCellFormat)
    End Function

#End Region

#Region "Private"

    Private Function p_collectingSelectedRowsID(oTable As Table) As ArrayList
        Dim alSelectedId As New ArrayList

        For Each row As TableRow In oTable.Rows
            For Each cell As TableCell In row.Cells
                For Each cbRow As CheckBox In cell.Controls
                    If cbRow.Checked Then alSelectedId.Add(cbRow.ID)
                    GoTo labelGotoNextRow
                Next
            Next
labelGotoNextRow:
        Next

        Return alSelectedId
    End Function

    Private Sub p_clearTableRows(oTable As Table)
        Dim iRowIdx As Int16 = 0
        Dim alRowToRemove As New ArrayList

        For Each oRow As TableRow In oTable.Rows
            If iRowIdx > 1 Then alRowToRemove.Add(oRow)
            iRowIdx += 1
        Next

        For Each oRow As TableRow In alRowToRemove
            oTable.Rows.Remove(oRow)
        Next
    End Sub

    Private Function p_createHyperlinkCell(sText As String, sUrl As String) As TableCell
        Dim oCell As New TableCell
        Dim oHyperlink As New HyperLink
        oHyperlink.Text = sText
        oHyperlink.NavigateUrl = sUrl
        oHyperlink.Target = "_blank"
        oCell.Controls.Add(oHyperlink)
        Return oCell
    End Function

    Private Function p_createLabelCell(sValue As String, iWidth As Int16) As TableCell
        Dim oCell As New TableCell
        Dim oLabel As New Label
        If iWidth > 0 Then oLabel.Width = iWidth
        oLabel.Text = sValue
        oCell.Controls.Add(oLabel)
        Return oCell
    End Function

    Private Function p_createImageCell(valueBase64String As String, iHeight As Int16, iWidth As Int16) As TableCell
        If valueBase64String.Length > 0 Then
            Dim oCell As New TableCell
            Dim oImage As New Image
            If iHeight > 0 Then oImage.Height = iHeight
            If iWidth > 0 Then oImage.Width = iWidth
            oImage.ImageUrl = String.Format("data:image/png;base64,{0}", valueBase64String)
            oCell.Controls.Add(oImage)
            Return oCell
        Else
            Return p_createLabelCell(String.Empty, 0)
        End If
    End Function

    Private Function p_createCheckBoxCell(sID As String, bSelect As Boolean) As TableCell
        Dim oCell As New TableCell
        Dim oCheckBox As New CheckBox
        oCheckBox.AutoPostBack = True
        oCheckBox.ID = sID
        oCheckBox.Checked = bSelect
        'AddHandler oCheckBox.CheckedChanged, AddressOf p_CheckBoxDetailOnCheckedChanged
        oCell.Controls.Add(oCheckBox)
        Return oCell
    End Function

    Private Sub p_popupConfirmMessage(oPage As Page, sMessage As String)
        ScriptManager.RegisterStartupScript(oPage, oPage.GetType, "CC_Msg_Confirm", "confirm('" & sMessage & "');", True)
    End Sub

    Private Sub p_popupAlertMessageAndRedirect(oPage As Page, sMessage As String, sRedirectPage As String)
        Dim script As String = "{ alert('" & sMessage & "');window.location ='" & p_pageFullname(sRedirectPage).Replace("~/", "") & "'; }"
        ScriptManager.RegisterStartupScript(oPage, oPage.GetType(), "CC_Msg_alert", Script, True)
    End Sub

    Private Sub p_populateInfoInPage(oLabelInfo As Label, oPage As Page, sMessage As String, Optional iType As Int16 = 0)
        If iType = 1 Then
            oLabelInfo.ForeColor = System.Drawing.Color.Green '= info success
        ElseIf iType = 2 Then
            oLabelInfo.ForeColor = System.Drawing.Color.Red '= alert
            p_popupAlertMessage(oPage, sMessage)
        Else
            oLabelInfo.ForeColor = System.Drawing.Color.Black 'black = normal info
        End If

        oLabelInfo.Text = sMessage
    End Sub

    Private Sub p_popupAlertMessage(oPage As Page, sMessage As String)
        ScriptManager.RegisterStartupScript(oPage, oPage.GetType, "CC_Msg_Alert", "alert('" & sMessage & "');", True)
    End Sub

    Private Function p_pageFullname(sPageName As String) As String
        'Return "~/" & sPageName & ".aspx"
        Return "~/" & sPageName
    End Function

    Private Function p_sessionDataRoleItem(sFormId As String) As RoleManager.DataRoleItem
        sFormId = sFormId.Replace("_aspx", "")
        If System.Web.HttpContext.Current.Session(SESSION_DATA_ROLE) IsNot Nothing Then
            Dim data As New RoleManager.DataRole
            data = System.Web.HttpContext.Current.Session(SESSION_DATA_ROLE)

            For Each dataRoleItem As RoleManager.DataRoleItem In data.alItem
                If dataRoleItem.FormId.ToLower = sFormId.ToLower Then
                    Return dataRoleItem
                End If
            Next
        End If
        Return New RoleManager.DataRoleItem
    End Function

    Private Sub p_downloadFile(originFilename As String, Optional downloadedFilename As String = "")
        If downloadedFilename.Length = 0 Then downloadedFilename = originFilename
        Dim sPath As String = HttpContext.Current.Server.MapPath(FOLDER_TEMP_FILE & originFilename)
        Dim File As New System.IO.FileInfo(sPath)

        If File.Exists Then
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ClearContent()
            HttpContext.Current.Response.ClearHeaders()
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + downloadedFilename)
            HttpContext.Current.Response.AddHeader("Content-Length", File.Length.ToString())
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            HttpContext.Current.Response.WriteFile(File.FullName)
            HttpContext.Current.Response.Flush()
            HttpContext.Current.Response.Close()
        End If

    End Sub

    Private Function p_setExcelDataTitle(sValue As String, Optional vCellFormat As OpenXmlManager.CellFormatCollection = OpenXmlManager.CellFormatCollection.Bold14) As OpenXmlManager.DataHeader
        Dim data As New OpenXmlManager.DataHeader
        data.Type = OpenXmlManager.HeaderType.Title
        data.Value = sValue
        data.FormatCollection = vCellFormat
        Return data
    End Function

    Private Function p_setExcelSubtitle(sSubtitle As String, sValue As String, Optional vCellFormat As OpenXmlManager.CellFormatCollection = OpenXmlManager.CellFormatCollection.Bold) As OpenXmlManager.DataHeader
        Dim data As New OpenXmlManager.DataHeader
        data.Type = OpenXmlManager.HeaderType.Subtitle
        data.Subtitle = sSubtitle
        data.Value = sValue
        data.FormatCollection = vCellFormat
        Return data
    End Function

    Private Function p_setExcelColumnHeader(sValue As String, Optional vCellFormat As OpenXmlManager.CellFormatCollection = OpenXmlManager.CellFormatCollection.BoldBorder) As OpenXmlManager.DataHeader
        Dim data As New OpenXmlManager.DataHeader
        data.Type = OpenXmlManager.HeaderType.Colomn
        data.Value = sValue
        data.FormatCollection = vCellFormat
        Return data
    End Function

    Private Function p_setExcelDetailContent(sValue As String, Optional vCellFormat As OpenXmlManager.CellFormatCollection = OpenXmlManager.CellFormatCollection.Border, Optional vType As OpenXmlManager.ContentDataType = OpenXmlManager.ContentDataType.String) As OpenXmlManager.DataDetail
        Dim data As New OpenXmlManager.DataDetail
        data.Value = sValue
        data.DataType = vType
        data.FormatCollection = vCellFormat
        Return data
    End Function

    Private Function p_setExcelFooter(sTitle As String, sValue As String, Optional vCellFormat As OpenXmlManager.CellFormatCollection = OpenXmlManager.CellFormatCollection.Bold) As OpenXmlManager.DataFooter
        Dim data As New OpenXmlManager.DataFooter
        data.Title = sTitle
        data.Value = sValue
        data.FormatCollection = vCellFormat
        Return data
    End Function

#End Region


End Module
