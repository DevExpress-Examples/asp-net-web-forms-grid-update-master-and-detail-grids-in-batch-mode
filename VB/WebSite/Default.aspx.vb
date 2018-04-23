Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Linq
Imports DevExpress.Web.Data
Imports DevExpress.Web.ASPxGridView
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Grid_BatchUpdate(ByVal sender As Object, ByVal e As ASPxDataBatchUpdateEventArgs)
        For Each args In e.InsertValues
            InsertNewItem(args.NewValues, True,DirectCast(sender, ASPxGridView))
        Next args
        For Each args In e.UpdateValues
            UpdateItem(args.Keys, args.NewValues, True)
        Next args
        For Each args In e.DeleteValues
            DeleteItem(args.Keys, args.Values, True)
        Next args
        e.Handled = True
    End Sub
    Protected Sub InsertNewItem(ByVal newValues As OrderedDictionary, ByVal isParent As Boolean, ByVal currentGrid As ASPxGridView)
        'comment the bellow line to check data modifications
        Throw New NotImplementedException("Data modifications aren't allowed in online example")
        If isParent Then
            Insert(newValues,nwd1)
        Else
            nwd2.InsertParameters("CategoryID").DefaultValue = currentGrid.GetMasterRowKeyValue().ToString()
            Insert(newValues, nwd2)
        End If

    End Sub
    Private Sub Insert(ByVal newValues As OrderedDictionary, ByVal datasource As AccessDataSource)
        For Each item In newValues.Keys
            datasource.InsertParameters(CStr(item)).DefaultValue = Convert.ToString(newValues(item))
        Next item
        datasource.Insert()
    End Sub
    Protected Sub UpdateItem(ByVal keys As OrderedDictionary, ByVal newValues As OrderedDictionary, ByVal isParent As Boolean)
        'comment the bellow line to check data modifications
        Throw New NotImplementedException("Data modifications aren't allowed in online example")
        If isParent Then
            Update(keys, newValues, nwd1)
        Else
            Update(keys, newValues, nwd2)
        End If
    End Sub

    Private Sub Update(ByVal keys As OrderedDictionary, ByVal newValues As OrderedDictionary, ByVal datasource As AccessDataSource)
        For Each item In newValues.Keys
            datasource.UpdateParameters(CStr(item)).DefaultValue = Convert.ToString(newValues(item))
        Next item
        datasource.UpdateParameters(nwd2.UpdateParameters.Count - 1).DefaultValue = Convert.ToString(keys(0))
        datasource.Update()
    End Sub
    Protected Sub DeleteItem(ByVal keys As OrderedDictionary, ByVal values As OrderedDictionary, ByVal isParent As Boolean)
        'comment the bellow line to check data modifications
        Throw New NotImplementedException("Data modifications aren't allowed in online example")
        If isParent Then
            nwd1.DeleteParameters(0).DefaultValue = Convert.ToString(keys("CategoryID"))
            nwd1.Delete()
        Else
            nwd2.DeleteParameters(0).DefaultValue = Convert.ToString(keys("ProductID"))
            nwd2.Delete()
        End If

    End Sub

    Protected Sub grid2_BeforePerformDataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Dim child As ASPxGridView = TryCast(sender, ASPxGridView)
        Dim container As GridViewDetailRowTemplateContainer = TryCast(child.NamingContainer, GridViewDetailRowTemplateContainer)
        child.ClientInstanceName = "detailGrid" & container.KeyValue
        Session("Category") = child.GetMasterRowKeyValue()

    End Sub
    Protected Sub Grid_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        Dim parentGrid As ASPxGridView = TryCast(sender, ASPxGridView)
        parentGrid.UpdateEdit()
        parentGrid.DataBind()
        If String.IsNullOrEmpty(e.Parameters) Then
            Return
        End If
        Dim [paramArray]() As String = e.Parameters.Split(","c)
        For i As Integer = 0 To [paramArray].Length - 1
            If String.IsNullOrWhiteSpace([paramArray](i)) Then
                Continue For
            End If
            Dim child As ASPxGridView = TryCast(parentGrid.FindDetailRowTemplateControl(Convert.ToInt32([paramArray](i)), "grid2"), ASPxGridView)
            If child IsNot Nothing Then
                child.UpdateEdit()
                child.DataBind()
            End If
        Next i
    End Sub
    Protected Sub grid_BatchUpdate(ByVal sender As Object, ByVal e As ASPxDataBatchUpdateEventArgs)
        For Each args In e.InsertValues
            InsertNewItem(args.NewValues, False, DirectCast(sender, ASPxGridView))
        Next args
        For Each args In e.UpdateValues
            UpdateItem(args.Keys, args.NewValues, False)
        Next args
        For Each args In e.DeleteValues
            DeleteItem(args.Keys, args.Values, False)
        Next args
        e.Handled = True
    End Sub
    Protected Sub Grid_CommandButtonInitialize(ByVal sender As Object, ByVal e As ASPxGridViewCommandButtonEventArgs)
        If e.ButtonType = ColumnCommandButtonType.Update OrElse e.ButtonType = ColumnCommandButtonType.Cancel Then
            e.Visible = False
        End If
    End Sub
End Class