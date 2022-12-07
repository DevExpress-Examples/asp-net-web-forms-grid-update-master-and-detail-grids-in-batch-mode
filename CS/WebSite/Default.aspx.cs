using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DevExpress.Web.Data;
using DevExpress.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Grid_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
    {        
        foreach (var args in e.InsertValues)
            InsertNewItem(args.NewValues, true,(ASPxGridView)sender);
        foreach (var args in e.UpdateValues)
            UpdateItem(args.Keys, args.NewValues, true);
        foreach (var args in e.DeleteValues)
            DeleteItem(args.Keys, args.Values, true);
        e.Handled = true;
    }
    protected void InsertNewItem(OrderedDictionary newValues, bool isParent, ASPxGridView currentGrid)
    {
        //comment the bellow line to check data modifications
        throw new NotImplementedException("Data modifications aren't allowed in online example");
        if (isParent)
            Insert(newValues,nwd1);
        else{
            nwd2.InsertParameters["CategoryID"].DefaultValue = currentGrid.GetMasterRowKeyValue().ToString();
            Insert(newValues, nwd2);           
        }

    }
    private void Insert(OrderedDictionary newValues, AccessDataSource datasource)
    {
        foreach (var item in newValues.Keys)
        {
            datasource.InsertParameters[(string)item].DefaultValue = Convert.ToString(newValues[item]);
        }
        datasource.Insert();
    }
    protected void UpdateItem(OrderedDictionary keys, OrderedDictionary newValues, bool isParent)
    {
        //comment the bellow line to check data modifications
        throw new NotImplementedException("Data modifications aren't allowed in online example");
        if (isParent)
            Update(keys, newValues, nwd1);
        else
            Update(keys, newValues, nwd2);
    }

    private void Update(OrderedDictionary keys, OrderedDictionary newValues, AccessDataSource datasource)
    {
        foreach (var item in newValues.Keys)
        {
            datasource.UpdateParameters[(string)item].DefaultValue = Convert.ToString(newValues[item]);
        }
        datasource.UpdateParameters[nwd2.UpdateParameters.Count - 1].DefaultValue = Convert.ToString(keys[0]);
        datasource.Update();
    }
    protected void DeleteItem(OrderedDictionary keys, OrderedDictionary values, bool isParent)
    {
        //comment the bellow line to check data modifications
        throw new NotImplementedException("Data modifications aren't allowed in online example");
        if (isParent)
        {
            nwd1.DeleteParameters[0].DefaultValue = Convert.ToString(keys["CategoryID"]);
            nwd1.Delete();
        }
        else
        {
            nwd2.DeleteParameters[0].DefaultValue = Convert.ToString(keys["ProductID"]);
            nwd2.Delete();
        }

    }

    protected void grid2_BeforePerformDataSelect(object sender, EventArgs e)
    {
        ASPxGridView child = sender as ASPxGridView;
        GridViewDetailRowTemplateContainer container = child.NamingContainer as GridViewDetailRowTemplateContainer;
        child.ClientInstanceName = "detailGrid" + container.KeyValue;      
        Session["Category"] = child.GetMasterRowKeyValue();

    }
    protected void Grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        ASPxGridView parentGrid = sender as ASPxGridView;
        parentGrid.UpdateEdit();
        parentGrid.DataBind();
        if (String.IsNullOrEmpty(e.Parameters))
            return;
        string[] paramArray = e.Parameters.Split(',');
        for (int i = 0; i < paramArray.Length; i++)
        {
            if (String.IsNullOrWhiteSpace(paramArray[i]))
                continue;
            ASPxGridView child = parentGrid.FindDetailRowTemplateControl(Convert.ToInt32(paramArray[i]), "grid2") as ASPxGridView;
            if (child != null)
            {
                child.UpdateEdit();
                child.DataBind();
            }
        }
    }
    protected void grid_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
    {
        foreach (var args in e.InsertValues)
            InsertNewItem(args.NewValues, false, (ASPxGridView)sender);
        foreach (var args in e.UpdateValues)
            UpdateItem(args.Keys, args.NewValues, false);
        foreach (var args in e.DeleteValues)
            DeleteItem(args.Keys, args.Values, false);
        e.Handled = true;
    }
    protected void Grid_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
    {
        if (e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.Cancel)
        {
            e.Visible = false;
        }
    }
}