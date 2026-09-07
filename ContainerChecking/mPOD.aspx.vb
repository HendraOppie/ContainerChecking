Imports System.IO
Imports AIT.SM
Imports AIT.ContainerCheck

Public Class POD
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        p_populateContainerList()
        p_getLocation()
    End Sub

    Private Function p_populateContainerListFromUrlParameters() As ArrayList
        Dim sExpiredDate As String = p_getUrlParameterItem(0)
        Dim dateExpiry As Date = Convert.ToDateTime(sExpiredDate)
        If DateDiff(DateInterval.Minute, dateExpiry, Now) >= 0 Then Throw New HttpException(404, "Page Expired")

        Dim sContainerNumbers() As String = p_getUrlParameterItem(2).Split("|")
        Return p_getListContainerReceivedMatchSelected(sContainerNumbers)
    End Function

    Private Sub p_getLocation()
        ClientScript.RegisterStartupScript(Me.[GetType](), "Javascript", "javascript:getLocation(); ", True)
        Session(SESSION_DATA_LATITUDE) = String.Empty
        If hidLat.Value.Length > 0 Then Session(SESSION_DATA_LATITUDE) = hidLat.Value
        Session(SESSION_DATA_LONGITUDE) = String.Empty
        If hidLon.Value.Length > 0 Then Session(SESSION_DATA_LONGITUDE) = hidLon.Value
    End Sub


    Private Function p_getUrlParameterItem(iIndex As Int16) As String '0= string expiry date; 1= userid; 2= list container with | delimiter
        Dim sUrlParameter As String = Request.QueryString("id").Replace(" ", "+")
        If sUrlParameter = String.Empty Then
            Throw New HttpException(404, "Page Not found")
        Else
            Dim sDecryptedUrlParameter As String = New SystemManager().Decrypt(sUrlParameter)
            Dim lUrlParameter() As String = sDecryptedUrlParameter.Split(",")

            Return lUrlParameter(iIndex)
        End If

        Return String.Empty
    End Function

    Private Function p_getListContainerReceivedMatchSelected(sContainerNumbers() As String) As ArrayList
        Dim alContainerReceived As ArrayList = New ContainersManager().GetListReceived(Server.MapPath("~/Bin/") & FILE_CONNECTION_STRING)

        Dim alContainerSelected As New ArrayList
        For Each data As ContainersManager.DataContainers In alContainerReceived
            For i As Int16 = 0 To sContainerNumbers.Count - 1
                If data.ContainerNumber = sContainerNumbers(i) Then
                    alContainerSelected.Add(data)
                    Exit For
                End If
            Next
        Next

        Return alContainerSelected
    End Function

    Private Sub p_populateContainerList()
        Dim alContainerList As New ArrayList

        If Session(SESSION_DATA_LIST_CONTAINERS_SELECTED) Is Nothing Then
            alContainerList = p_populateContainerListFromUrlParameters()
        Else
            alContainerList = Session(SESSION_DATA_LIST_CONTAINERS_SELECTED)
        End If

        Dim iSeq As Int16 = 0
        For Each data As ContainersManager.DataContainers In alContainerList
            iSeq += 1
            Dim lbContainerNumber As New Label
            lbContainerNumber.Text = iSeq.ToString & ". " & data.ContainerNumber

            Dim panelContainerNumber As New Panel
            panelContainerNumber.Height = 30
            panelContainerNumber.Controls.Add(lbContainerNumber)
            divContainerList.Controls.Add(panelContainerNumber)
        Next

        Session(SESSION_DATA_LIST_CONTAINERS_SELECTED) = alContainerList
    End Sub

    Private Function p_isInputValid() As Boolean
        If tbNama.Text.Trim = String.Empty Then
            PopupAlertMessage(Me.Page, "Please provide Name")
            Return False
        End If
        If tbDepartment.Text.Trim = String.Empty Then
            PopupAlertMessage(Me.Page, "Please provide Department")
            Return False
        End If
        If inpByteSignature.Value = String.Empty Then
            PopupAlertMessage(Me.Page, "Please provide signature")
            Return False
        End If

        If p_isContainersAlreadyPOD() Then Return False
        Return True
    End Function

    Private Function p_isContainersAlreadyPOD() As Boolean
        Dim alContainerList As ArrayList = Session(SESSION_DATA_LIST_CONTAINERS_SELECTED)
        For Each dataContainer As ContainersManager.DataContainers In alContainerList
            Dim dataItem As New ProofOfDeliveryManager.DataPODItem
            dataItem.Hawb = dataContainer.Hawb
            dataItem.ContainerNumber = dataContainer.ContainerNumber

            Dim dataChecked As ProofOfDeliveryManager.DataPODItem = New ProofOfDeliveryManager().GetDataItem_HawbContainer(dataItem, Server.MapPath("~/Bin/") & FILE_CONNECTION_STRING)
            If Not dataChecked.ContainerNumber = String.Empty Then
                PopupAlertMessage(Me.Page, "Container#" & dataChecked.ContainerNumber & " already POD. Cannot Proceed. Please Regenerate POD.")
                Return True
            End If
        Next

        Return False
    End Function

    Private Function p_setData() As ProofOfDeliveryManager.DataPOD
        Dim data As New ProofOfDeliveryManager.DataPOD
        If Session(SESSION_DATA_USER) Is Nothing Then
            data.HandoverType = ProofOfDeliveryManager.HandoverType.CUSTOMER.ToString
            data.UserId = p_getUrlParameterItem(1)
        Else
            data.HandoverType = ProofOfDeliveryManager.HandoverType.USER.ToString
            data.UserId = SessionDataUser.UserId
        End If

        data.ReceivedWho = tbNama.Text
        data.Department = tbDepartment.Text
        data.Remark = tbRemark.Text
        data.imgSignature = Convert.FromBase64String(inpByteSignature.Value.Replace("data:image/png;base64,", ""))

        data.Longitude = Session(SESSION_DATA_LONGITUDE)
        data.Latitude = Session(SESSION_DATA_LATITUDE)
        data.DeviceInfo = p_getDeviceInfo()

        Dim alContainerList As ArrayList = Session(SESSION_DATA_LIST_CONTAINERS_SELECTED)
        For Each dataContainer As ContainersManager.DataContainers In alContainerList
            Dim dataItem As New ProofOfDeliveryManager.DataPODItem
            dataItem.Hawb = dataContainer.Hawb
            dataItem.ContainerNumber = dataContainer.ContainerNumber

            data.alItem.Add(dataItem)
        Next

        Return data
    End Function

    Private Function p_getDeviceInfo() As String
        Return Request.UserAgent
    End Function

    Private Sub ibtSubmit_Click(sender As Object, e As EventArgs) Handles ibtSubmit.Click
        'Response.Write("<script>alert('Longitude: " & Convert.ToString(hidLon.Value) & "  Latitude: " & Convert.ToString(hidLat.Value) & "')</script>")
        Try
            If p_isInputValid() Then
                Dim oPodManager As New ProofOfDeliveryManager
                oPodManager.Save(p_setData(), Server.MapPath("~/Bin/") & FILE_CONNECTION_STRING)
                If Session(SESSION_DATA_USER) Is Nothing Then
                    Session(SESSION_INFO_Header) = "Tanda Terima Sukses Dikirim dan Disimpan"
                    Session(SESSION_INFO) = "Terima kasih atas kerjasamanya."
                    TransferPage(PAGE_THANK_YOU_MOBILE)

                Else
                    Session.Remove(SESSION_DATA_LIST_CONTAINERS)
                    Session(SESSION_INFO) = "POD has successfully submitted"
                    Session(SESSION_INFO_REDIRECT) = PAGE_DELIVERY_NOTE_MOBILE
                    TransferPage(PAGE_INFO_MOBILE)
                End If

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

End Class