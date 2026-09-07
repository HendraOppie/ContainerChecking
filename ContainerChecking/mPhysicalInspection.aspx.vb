Imports System.IO
Imports AIT.SM
Imports AIT.ContainerCheck

Public Class PhysicalCheck
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            p_setListLocation()
            p_setListContainer()
            p_setListCheckItem()

            p_getAndPopulateData()
        End If
    End Sub

    Private Sub p_getAndPopulateData()
        Dim data As New PhysicalConditionManager.DataPhysicalCondition
        If Session(SESSION_DATA_PHYSICAL_CONDITION) Is Nothing Then
            data.Hawb = p_getHawb()
            data.ContainerNumber = ddContainerNumber.Text
            data.LocationID = p_getLocationId()
        Else
            data = Session(SESSION_DATA_PHYSICAL_CONDITION)
            Session.Remove(SESSION_DATA_PHYSICAL_CONDITION)

            ddContainerNumber.Text = data.ContainerNumber
            ddLocation.Text = p_getLocationName(data.LocationID)
        End If

        Dim alData As ArrayList = p_getListData(data)
        For Each dt As PhysicalConditionManager.DataPhysicalCondition In alData
            p_populateData(dt)
        Next
        Session(SESSION_DATA_LIST_PHYSICAL_CONDITIONS) = alData

    End Sub

    Private Function p_getListData(data As PhysicalConditionManager.DataPhysicalCondition) As ArrayList
        Try
            Return New PhysicalConditionManager().GetList(data, Session(SESSION_FILE_CONNECTION_STRING))
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub p_populateData(data As PhysicalConditionManager.DataPhysicalCondition)
        Dim bSameRow As Boolean = False
        Dim iRowIdx As Int16 = 0
        Dim iCellIdx As Int16 = 0

        For Each oRow As TableRow In tblDetail.Rows
            If iRowIdx > 0 Then
                iCellIdx = 0
                For Each oCell As TableCell In oRow.Cells
                    If iCellIdx = 0 Then
                        For Each oControl As Control In oCell.Controls
                            If oControl.GetType = GetType(LinkButton) Then
                                Dim oli As LinkButton = oControl
                                If oli.Text = p_getCheckItem(data.ItemID).Name Then bSameRow = True
                            End If
                        Next
                    ElseIf iCellIdx = 1 Then
                        If bSameRow Then
                            For Each ctrl As Control In oCell.Controls
                                If ctrl.GetType = GetType(Label) Then
                                    DirectCast(ctrl, Label).Text = p_generateSummary(data)
                                End If
                            Next

                            If data.Thumbnail.Length > 0 Then
                                Dim oImg As New Image
                                oImg.ImageUrl = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(data.Thumbnail))
                                oCell.Controls.Add(oImg)
                            End If
                        End If
                    End If

                    iCellIdx += 1
                Next
            End If

            If bSameRow Then Exit For
            iRowIdx += 1
        Next

    End Sub

    Private Function p_generateSummary(data As PhysicalConditionManager.DataPhysicalCondition) As String
        Dim sBr As String = "<br />"
        Dim sInspected As String = "Inspected. "
        Dim sBroken As String = String.Empty
        Dim sRemark As String = "Remark: "

        If data.isBroken Then
            sBroken = "Broken Found."
        Else
            sBroken = "No Broken Found."
        End If
        If data.Remark.Length > 0 Then sRemark = data.Remark

        If p_getCheckItem(data.ItemID).ItemType = PhysicalCheckItemManager.PhysicalCheckItemType.SEAL.ToString Then
            If Not data.isCheckedSealed Then Return sInspected & "No Sealed. " & sBr & sRemark

            Return sInspected & "Sealed. " & sBroken & sBr & sRemark
        Else
            Return sInspected & sBroken & sBr & sRemark
        End If
    End Function

    Private Sub p_setListLocation()
        ddLocation.Items.Clear()
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
                ddLocation.Items.Add(dt.Name)
            Next
            'ddLocation.Text = ddLocation.Items(0).Text
        End If
    End Sub

    Private Sub p_setListCheckItem()
        Try
            If Session(SESSION_DATA_LIST_CHECK_ITEMS) Is Nothing Then Session(SESSION_DATA_LIST_CHECK_ITEMS) = New PhysicalCheckItemManager().GetList(Session(SESSION_FILE_CONNECTION_STRING))
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub p_setListContainer()
        ddContainerNumber.Items.Clear()
        Dim alContainers As New ArrayList
        Try
            alContainers = New ContainersManager().GetListNotDelivered(Session(SESSION_FILE_CONNECTION_STRING))
            If alContainers.Count > 0 Then
                For Each dt As ContainersManager.DataContainers In alContainers
                    ddContainerNumber.Items.Add(dt.ContainerNumber)
                Next
                'ddContainerNumber.Text = ddContainerNumber.Items(0).Text
            End If
            Session(SESSION_DATA_LIST_CONTAINERS) = alContainers
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Function p_getCheckItem(sId As String) As PhysicalCheckItemManager.DataPhysicalCheckItem
        Dim alCheckItems As ArrayList = Session(SESSION_DATA_LIST_CHECK_ITEMS)

        For Each dt As PhysicalCheckItemManager.DataPhysicalCheckItem In alCheckItems
            If dt.ID = sId Then
                Return dt
            End If
        Next

        Return New PhysicalCheckItemManager.DataPhysicalCheckItem
    End Function

    Private Function p_getCheckItemType(sItemName As String) As String
        Dim alCheckItems As ArrayList = Session(SESSION_DATA_LIST_CHECK_ITEMS)
        For Each dt As PhysicalCheckItemManager.DataPhysicalCheckItem In alCheckItems
            If dt.Name = sItemName Then
                Return dt.ItemType
            End If
        Next

        Return String.Empty
    End Function

    Private Function p_getCheckItemID(sItemName As String) As String
        Dim alCheckItems As ArrayList = Session(SESSION_DATA_LIST_CHECK_ITEMS)
        For Each dt As PhysicalCheckItemManager.DataPhysicalCheckItem In alCheckItems
            If dt.Name = sItemName Then
                Return dt.ID
            End If
        Next

        Return String.Empty
    End Function

    Private Function p_getLocationId() As String
        Dim alLocations As ArrayList = Session(SESSION_DATA_LIST_LOCATIONS)
        For Each dt As LocationsManager.DataLocations In alLocations
            If dt.Name = ddLocation.Text Then
                Return dt.ID
            End If
        Next

        Return String.Empty
    End Function

    Private Function p_getLocationName(sId As String) As String
        Dim alLocations As ArrayList = Session(SESSION_DATA_LIST_LOCATIONS)
        For Each dt As LocationsManager.DataLocations In alLocations
            If dt.ID = sId Then
                Return dt.Name
            End If
        Next

        Return String.Empty
    End Function

    Private Function p_getHawb() As String
        Dim alContainers As ArrayList = Session(SESSION_DATA_LIST_CONTAINERS)
        For Each dt As ContainersManager.DataContainers In alContainers
            If dt.ContainerNumber = ddContainerNumber.Text Then
                Return dt.Hawb
            End If
        Next

        Return String.Empty
    End Function

    Private Sub p_populateItemForm(selectedItem As String)
        p_setDefaultData(selectedItem)
        If p_isUserAccessGranted() Then
            Session(SESSION_PHYSICAL_INSPECTION_SELECTED_ITEM) = selectedItem
            Session(SESSION_PHYSICAL_INSPECTION_ITEM_TYPE) = p_getCheckItemType(selectedItem)
            TransferPage(PAGE_PHYSICAL_INSPECTION_ITEM_MOBILE)
        Else
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
        End If
    End Sub

    Private Function p_isUserAccessGranted() As Boolean
        Dim data As PhysicalConditionManager.DataPhysicalCondition = Session(SESSION_DATA_PHYSICAL_CONDITION)
        If data.UserId = String.Empty Then
            data.UserId = SessionDataUser().UserId
            Return SessionDataRoleItem(PAGE_PHYSICAL_INSPECTION_ITEM_MOBILE).bCreate
        Else
            Return SessionDataRoleItem(PAGE_PHYSICAL_INSPECTION_ITEM_MOBILE).bUpdate
        End If

        Return True
    End Function

    Private Sub p_setDefaultData(selectedItem As String)
        Dim data As New PhysicalConditionManager.DataPhysicalCondition
        If Not Session(SESSION_DATA_PHYSICAL_CONDITION) Is Nothing Then data = Session(SESSION_DATA_PHYSICAL_CONDITION)

        data.LocationID = p_getLocationId()
        data.ContainerNumber = ddContainerNumber.Text
        data.Hawb = p_getHawb()

        p_populateExistingData(selectedItem, data)
        Session(SESSION_DATA_PHYSICAL_CONDITION) = data
    End Sub

    Private Sub p_populateExistingData(selectedItem As String, ByRef data As PhysicalConditionManager.DataPhysicalCondition)
        Dim alData As ArrayList = Session(SESSION_DATA_LIST_PHYSICAL_CONDITIONS)
        For Each dt As PhysicalConditionManager.DataPhysicalCondition In alData
            If dt.ItemID = p_getCheckItemID(selectedItem) Then
                data = dt
                Exit For
            End If
        Next
    End Sub

    Private Sub ddContainerNumber_TextChanged(sender As Object, e As EventArgs) Handles ddContainerNumber.TextChanged
        Session.Remove(SESSION_DATA_PHYSICAL_CONDITION)
        p_resetLabelSummary()
        p_getAndPopulateData()
    End Sub

    Private Sub ddLocation_TextChanged(sender As Object, e As EventArgs) Handles ddLocation.TextChanged
        Session.Remove(SESSION_DATA_PHYSICAL_CONDITION)
        p_resetLabelSummary()
        p_getAndPopulateData()
    End Sub

    Private Sub linkButtonDetailOnClick(sender As Object, e As EventArgs)
        If ddContainerNumber.Text = String.Empty Then
            PopupAlertMessage(Me.Page, "Please choose Container Number")
        Else
            If SessionDataRoleItem(PAGE_PHYSICAL_INSPECTION_ITEM_MOBILE).bRead Then
                p_populateItemForm(sender.Text)
            Else
                PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            End If
        End If
    End Sub

    Private Sub p_resetLabelSummary()
        Dim iRowIdx = 0
        Dim iCellIdx As Int16 = 0

        For Each oRow As TableRow In tblDetail.Rows
            If iRowIdx > 0 Then
                iCellIdx = 0
                For Each oCell As TableCell In oRow.Cells
                    If iCellIdx = 1 Then
                        For Each ctrl As Control In oCell.Controls
                            If ctrl.GetType = GetType(Label) Then
                                DirectCast(ctrl, Label).Text = "Not Inspected<br />Remark:"
                            ElseIf ctrl.GetType = GetType(Image) Then
                                oCell.Controls.Remove(ctrl)
                            End If
                        Next

                    End If

                    iCellIdx += 1
                Next
            End If
            iRowIdx += 1
        Next

    End Sub

    Private Sub p_populateDefaultDetails()
        Dim alCheckItems As ArrayList = Session(SESSION_DATA_LIST_CHECK_ITEMS)
        For Each dt As PhysicalCheckItemManager.DataPhysicalCheckItem In alCheckItems
            Dim oLinkButtonDetail As New LinkButton
            oLinkButtonDetail.Width = 80
            oLinkButtonDetail.CssClass = "linkbuttonAsLabel"
            oLinkButtonDetail.Font.Bold = True
            AddHandler oLinkButtonDetail.Click, AddressOf linkButtonDetailOnClick
            oLinkButtonDetail.Text = dt.Name

            Dim oLabel As New Label
            oLabel.Width = 200
            oLabel.Text = "Not Inspected<br />Remark:"

            Dim oCell1 As New TableCell
            oCell1.Controls.Add(oLinkButtonDetail)

            Dim oCell2 As New TableCell
            oCell2.Controls.Add(oLabel)

            Dim oRow As New TableRow
            oRow.VerticalAlign = VerticalAlign.Top
            oRow.Cells.Add(oCell1)
            oRow.Cells.Add(oCell2)
            tblDetail.Rows.Add(oRow)

        Next
    End Sub

    Private Sub PhysicalCheck_Init(sender As Object, e As EventArgs) Handles Me.Init
        p_setListCheckItem()
        p_populateDefaultDetails()
    End Sub

    Private Sub ibtRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles ibtRefresh.Click
        p_getAndPopulateData()
    End Sub
End Class