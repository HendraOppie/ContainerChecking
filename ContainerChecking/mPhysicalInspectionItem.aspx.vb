Imports AIT.SM
Imports AIT.ContainerCheck
Imports System.IO
Imports System.Drawing

Public Class PhysicalCheckItem
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lbItem.Text = Session(SESSION_PHYSICAL_INSPECTION_SELECTED_ITEM)
            Session.Remove(SESSION_PHYSICAL_INSPECTION_SELECTED_ITEM)

            p_populateExistingData()

            If Session(SESSION_PHYSICAL_INSPECTION_ITEM_TYPE) = PhysicalCheckItemManager.PhysicalCheckItemType.BODY.ToString Then
                tblSeal.Visible = False
            ElseIf Session(SESSION_PHYSICAL_INSPECTION_ITEM_TYPE) = PhysicalCheckItemManager.PhysicalCheckItemType.PALLET.ToString Then
                tblSeal.Visible = False
            Else
                panBody.Visible = False
            End If
            Session.Remove(SESSION_PHYSICAL_INSPECTION_ITEM_TYPE)
        End If
    End Sub

    Private Sub p_populateExistingData()
        Dim data As PhysicalConditionManager.DataPhysicalCondition = Session(SESSION_DATA_PHYSICAL_CONDITION)
        cbIsSealed.Checked = data.isCheckedSealed
        cbBroken.Checked = data.isBroken
        tbRemark.Text = data.Remark
        tbSealNumber.Text = data.Remark
        p_showThumbnail(data)
    End Sub

    Private Sub p_showThumbnail(data As PhysicalConditionManager.DataPhysicalCondition)
        If data.Thumbnail.Length > 0 Then
            imgThumbnail.Visible = True
            imgThumbnail.ImageUrl = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(data.Thumbnail))
            Dim dWidth As Double = imgThumbnail.Width.Value
            imgThumbnail.Width = 250
            imgThumbnail.Height = 150
        End If
    End Sub

    Private Function p_getCheckItemId() As String
        Dim alCheckItems As ArrayList = Session(SESSION_DATA_LIST_CHECK_ITEMS)
        For Each dt As PhysicalCheckItemManager.DataPhysicalCheckItem In alCheckItems
            If dt.Name = lbItem.Text Then
                Return dt.ID
            End If
        Next

        Return String.Empty
    End Function

    Protected Sub btSubmit_Click(sender As Object, e As EventArgs) Handles btSubmit.Click
        If Not SessionDataRoleItem(Me.Page.GetType().Name).bCreate Then
            PopupAlertMessage(Me.Page, MESSAGE_ALERT_ACCESS_DENIED)
            Exit Sub
        End If
        Try
            If p_isInputValid() Then
                Dim oPhysicalCondition As New PhysicalConditionManager
                oPhysicalCondition.Save(p_setData, Session(SESSION_FILE_CONNECTION_STRING))

                GotoPage(Me.Page, PAGE_PHYSICAL_INSPECTION_MOBILE)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Function p_isInputValid() As Boolean
        Dim data As PhysicalConditionManager.DataPhysicalCondition = Session(SESSION_DATA_PHYSICAL_CONDITION)
        Dim bPictureMandatory As Boolean = True
        If tblSeal.Visible And Not cbIsSealed.Checked Then bPictureMandatory = False
        If bPictureMandatory And Not fuPicture.HasFile And data.Thumbnail.Length = 0 Then
            PopupAlertMessage(Me.Page, "Please provide picture")
            Return False
        End If

        Return True
    End Function

    Private Function p_setData() As PhysicalConditionManager.DataPhysicalCondition
        Dim data As New PhysicalConditionManager.DataPhysicalCondition
        data = Session(SESSION_DATA_PHYSICAL_CONDITION)
        data.ItemID = p_getCheckItemId()

        If tblSeal.Visible Then
            data.isCheckedSealed = cbIsSealed.Checked
            data.isBroken = cbBrokenSeal.Checked
            data.Remark = tbSealNumber.Text
        Else
            data.isCheckedSealed = True
            data.isBroken = cbBroken.Checked
            data.Remark = tbRemark.Text
        End If

        If fuPicture.HasFile Then
            Dim imageBytes As Byte() = p_convertUploadFileIntoBinary()
            data.Picture = New ImageManager().ResizeHighQualityAndBytesToBytes(imageBytes)
            data.Thumbnail = New ImageManager().GetThumbnailAndBytesToBytes(imageBytes)
        End If

        Return data
    End Function

    Private Function p_convertUploadFileIntoBinary() As Byte()
        Dim bytes As Byte()
        Using fs As Stream = fuPicture.PostedFile.InputStream
            Using br As BinaryReader = New BinaryReader(fs)
                bytes = br.ReadBytes(fs.Length)
            End Using
        End Using

        Return bytes
    End Function

    Private Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Session.Remove(SESSION_DATA_PHYSICAL_CONDITION)
        GotoPage(Me.Page, PAGE_PHYSICAL_INSPECTION_MOBILE)
    End Sub

    Private Sub cbIsSealed_CheckedChanged(sender As Object, e As EventArgs) Handles cbIsSealed.CheckedChanged
        If Not cbIsSealed.Checked Then
            cbBrokenSeal.Checked = False
            tbSealNumber.Text = String.Empty
        End If
        cbBrokenSeal.Enabled = cbIsSealed.Checked
        tbSealNumber.Enabled = cbIsSealed.Checked
    End Sub
End Class