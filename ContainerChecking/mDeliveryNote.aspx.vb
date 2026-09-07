Imports System.IO
Imports AIT.SM
Imports AIT.ContainerCheck
Imports QRCoder
Imports System.Drawing

Public Class Handover
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            divPOD.Visible = False
        End If

        p_getAndPopulateContainerList()
    End Sub

    Private Sub p_getAndPopulateContainerList()
        Dim alContainerList As New ArrayList
        If Session(SESSION_DATA_LIST_CONTAINERS) Is Nothing Then
            alContainerList = New ContainersManager().GetListReceived(Session(SESSION_FILE_CONNECTION_STRING))
            Session(SESSION_DATA_LIST_CONTAINERS) = alContainerList
        Else
            alContainerList = Session(SESSION_DATA_LIST_CONTAINERS)
        End If

        For Each data As ContainersManager.DataContainers In alContainerList
            Dim cbContainerNumber As New CheckBox
            cbContainerNumber.AutoPostBack = True
            cbContainerNumber.Text = data.ContainerNumber

            Dim panelContainerNumber As New Panel
            panelContainerNumber.Height = 30
            panelContainerNumber.Controls.Add(cbContainerNumber)
            divContainerList.Controls.Add(panelContainerNumber)
        Next
    End Sub

    Private Sub p_generatePODLink(alSelectedContainerNumbers As ArrayList)
        Dim sContentQr As String = URL_PREFIX_POD & p_setEncryptedParameters(alSelectedContainerNumbers)
        'Dim sContentQr As String = "https://localhost:44307/mPOD.aspx?id=" & p_setEncryptedParameters(alSelectedContainerNumbers) 'testing purpose only

        Dim qrCodeDt As QRCodeData = New QRCodeGenerator().CreateQrCode(sContentQr, QRCodeGenerator.ECCLevel.H)
        Dim qrC As QRCode = New QRCode(qrCodeDt)
        Dim qrCodeImage As Bitmap = qrC.GetGraphic(20)
        Dim stream As MemoryStream = New MemoryStream()
        qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png)
        imgQR.ImageUrl = "data:image/png;base64," & Convert.ToBase64String(stream.ToArray())

        tbLink.Text = sContentQr

        'Response.Redirect(sContentQr) 'testing purpose only
    End Sub

    Private Function p_setEncryptedParameters(alSelectedContainerNumbers As ArrayList) As String
        Dim sDelimiter As String = ","
        Dim sSelectedContainerNumbers As String = String.Empty
        For Each sContainerNumber As String In alSelectedContainerNumbers
            sSelectedContainerNumbers += sContainerNumber & "|"
        Next
        sSelectedContainerNumbers = sSelectedContainerNumbers.Substring(0, sSelectedContainerNumbers.Length - 1)

        Dim stringToEncrypt As String = String.Format("{0},{1},{2}", Now.AddMinutes(20).ToString, SessionDataUser().UserId, sSelectedContainerNumbers)
        Dim sEncrypted As String = New SystemManager().Encrypt(stringToEncrypt)
        'Dim sDecrypted As String = New SystemManager().Decrypt(sEncrypted) 'testing purpose only

        Return sEncrypted
    End Function

    Private Sub btCopyLink_Click(sender As Object, e As EventArgs) Handles btCopyLink.Click

    End Sub

    Private Function p_getListSelectedContainerNumbers() As ArrayList
        Dim alSelectedContainerNumbers As New ArrayList
        For Each oDivControl As Control In divContainerList.Controls
            If oDivControl.GetType = GetType(Panel) Then
                For Each cb As CheckBox In oDivControl.Controls
                    If cb.Checked Then alSelectedContainerNumbers.Add(cb.Text)
                Next
            End If
        Next

        Return alSelectedContainerNumbers
    End Function

    Private Sub btGeneratePOD_Click(sender As Object, e As EventArgs) Handles btGeneratePOD.Click
        If SessionDataRoleItem(PAGE_POD_MOBILE).bCreate Then
            If btGeneratePOD.Text = "Generate POD" Then
                Dim alSelectedContainerNumbers As ArrayList = p_getListSelectedContainerNumbers()
                If alSelectedContainerNumbers.Count = 0 Then
                    PopupAlertMessage(Me.Page, "Please choose Container Number")
                    Exit Sub
                End If
                p_generatePODLink(alSelectedContainerNumbers)
                p_setSessionSelectedContainerNumbers(alSelectedContainerNumbers)

                btGeneratePOD.Text = "Regenerate POD"
                divContainerList.Visible = False
                divPOD.Visible = True
            Else
                btGeneratePOD.Text = "Generate POD"
                divPOD.Visible = False
                divContainerList.Visible = True
            End If
        Else
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
        End If
        Session.Remove(SESSION_DATA_LIST_CONTAINERS)
    End Sub

    Private Sub p_setSessionSelectedContainerNumbers(alSelectedContainerNumbers As ArrayList)
        Dim alDataSelectedContainerNumber As New ArrayList

        Dim alDataContainerNumber As ArrayList = Session(SESSION_DATA_LIST_CONTAINERS)
        For Each data As ContainersManager.DataContainers In alDataContainerNumber
            For Each sContainerNumber As String In alSelectedContainerNumbers
                If data.ContainerNumber = sContainerNumber Then
                    alDataSelectedContainerNumber.Add(data)
                    Exit For
                End If
            Next
        Next

        Session(SESSION_DATA_LIST_CONTAINERS_SELECTED) = alDataSelectedContainerNumber
    End Sub

    Private Sub cbSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles cbSelectAll.CheckedChanged
        For Each oDivControl As Control In divContainerList.Controls
            If oDivControl.GetType = GetType(Panel) Then
                For Each cb As CheckBox In oDivControl.Controls
                    cb.Checked = cbSelectAll.Checked
                Next
            End If
        Next
    End Sub

    Private Sub btPOD_Click(sender As Object, e As EventArgs) Handles btPOD.Click
        GotoPage(Me.Page, PAGE_POD_MOBILE, True)
    End Sub
End Class