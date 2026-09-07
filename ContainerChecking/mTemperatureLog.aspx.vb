Imports System.IO
Imports AIT.SM
Imports AIT.ContainerCheck

Public Class ContainerLog
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            p_setListLocation()
            p_setListContainer()
            p_setListSetTemperature()
            p_setListTemperature()
            p_setListBattery()
        End If
    End Sub

    Private Sub p_setListLocation()
        ddLocation.Items.Clear()
        Dim alLocations As New ArrayList
        Try
            alLocations = New LocationsManager().GetListActive(Session(SESSION_FILE_CONNECTION_STRING))
            If alLocations.Count > 0 Then
                For Each dt As LocationsManager.DataLocations In alLocations
                    ddLocation.Items.Add(dt.Name)
                Next
                ddLocation.Text = ddLocation.Items(0).Text
            End If
            Session(SESSION_DATA_LIST_LOCATIONS) = alLocations
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub p_setListContainer()
        ddContainerNumber.Items.Clear()
        Dim alContainers As New ArrayList
        Try
            alContainers = New ContainersManager().GetListReceived(Session(SESSION_FILE_CONNECTION_STRING))
            If alContainers.Count > 0 Then
                For Each dt As ContainersManager.DataContainers In alContainers
                    ddContainerNumber.Items.Add(dt.ContainerNumber)
                Next
                ddContainerNumber.Text = ddContainerNumber.Items(0).Text
            End If
            Session(SESSION_DATA_LIST_CONTAINERS) = alContainers
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub p_setListSetTemperature()
        ddSetTemperature.Items.Clear()
        For i As Decimal = 3 To 6 Step 0.1
            ddSetTemperature.Items.Add(i.ToString)
        Next
        ddSetTemperature.Text = ddSetTemperature.Items(20).Text
    End Sub

    Private Sub p_setListTemperature()
        ddTemperature.Items.Clear()
        For i As Decimal = 3 To 6 Step 0.1
            ddTemperature.Items.Add(i.ToString)
        Next
        ddTemperature.Text = ddTemperature.Items(20).Text
    End Sub

    Private Sub p_setListBattery()
        ddBattery.Items.Clear()
        For i As Decimal = 100 To 60 Step -0.1
            ddBattery.Items.Add(i.ToString)
        Next
        ddBattery.Text = ddBattery.Items(100).Text
    End Sub

    Protected Sub btSubmit_Click(sender As Object, e As EventArgs) Handles btSubmit.Click
        If Not SessionDataRoleItem(Me.Page.GetType().Name).bCreate Then
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            Exit Sub
        End If
        Try
            If p_isInputValid() Then
                Dim oOtherCondition As New OtherConditionsManager
                oOtherCondition.Save(p_setData(), Session(SESSION_FILE_CONNECTION_STRING))

                p_populateInfo("Submission successfully completed.<br />&Click Continue to submit another container")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub p_populateInfo(message As String)
        Session(SESSION_INFO) = message
        Session(SESSION_INFO_REDIRECT) = PAGE_TEMPERATURE_LOG_MOBILE
        TransferPage(PAGE_INFO_MOBILE)
    End Sub

    Private Function p_isInputValid() As Boolean
        If ddContainerNumber.Text = String.Empty Then
            Response.Write("<script>alert('Please provide Container Number')</script>")
            Return False
        End If
        If Not fuContainer.HasFile Then
            Response.Write("<script>alert('Please provide picture')</script>")
            Return False
        End If
        If Not fuTemperature.HasFile Then
            Response.Write("<script>alert('Please provide picture')</script>")
            Return False
        End If

        Return True
    End Function

    Private Function p_convertUploadFileIntoBinary(oFileUpload As FileUpload) As Byte()
        Dim bytes As Byte()
        Using fs As Stream = oFileUpload.PostedFile.InputStream
            Using br As BinaryReader = New BinaryReader(fs)
                bytes = br.ReadBytes(fs.Length)
            End Using
        End Using

        Return bytes
    End Function

    Private Function p_setData() As OtherConditionsManager.DataOtherConditions
        Dim data As New OtherConditionsManager.DataOtherConditions
        data.Hawb = p_getHawb()
        data.ContainerNumber = ddContainerNumber.Text
        data.LocationID = p_getLocationId()
        data.SetTemperature = ddSetTemperature.Text
        data.Temperature = ddTemperature.Text
        data.Battery = ddBattery.Text
        data.Remark = tbRemark.Text

        Dim imageBytes As Byte() = p_convertUploadFileIntoBinary(fuContainer)
        data.imgContainer = New ImageManager().ResizeHighQualityAndBytesToBytes(imageBytes)
        data.thumbContainer = New ImageManager().GetThumbnailAndBytesToBytes(imageBytes)

        imageBytes = p_convertUploadFileIntoBinary(fuTemperature)
        data.imgTemperature = New ImageManager().ResizeHighQualityAndBytesToBytes(imageBytes)
        data.thumbTemperature = New ImageManager().GetThumbnailAndBytesToBytes(imageBytes)

        data.UserId = SessionDataUser().UserId

        Return data
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

    Private Function p_getHawb() As String
        Dim alContainers As ArrayList = Session(SESSION_DATA_LIST_CONTAINERS)
        For Each dt As ContainersManager.DataContainers In alContainers
            If dt.ContainerNumber = ddContainerNumber.Text Then
                Return dt.Hawb
            End If
        Next

        Return String.Empty
    End Function

End Class