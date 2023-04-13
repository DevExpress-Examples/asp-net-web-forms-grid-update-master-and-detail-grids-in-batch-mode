var visibleIndicies = [];
var errorMessage = "";
var buttonFlag = false;
function OnClick(s, e) {
    if (visibleIndicies.length == 0)
        grid.UpdateEdit();
    else {
        buttonFlag = true;
        grid.PerformCallback(visibleIndicies);
    }
}
function OnCancel(s, e) {
    for (var i = 0; i < visibleIndicies.length; i++) {
        var currentKey = grid.GetRowKey(visibleIndicies[i]);
        var childgrid = ASPxClientControl.GetControlCollection().GetByName("detailGrid" + currentKey);
        if (childgrid != undefined && childgrid != null) {
            childgrid.CancelEdit();
        }
    }
    grid.CancelEdit();
}
function OnConfirm(s, e) {
    if (e.requestTriggerID == 'Grid' && buttonFlag) {
        e.cancel = true;
    }
}
function AddCurrentDetailGrid(visibleIndex) {
    if (visibleIndicies.indexOf(visibleIndex) == -1)
        visibleIndicies.push(visibleIndex);
}
function RemoveCurrentDetailGrid(visibleIndex) {
    var arrayIndex = visibleIndicies.indexOf(visibleIndex);
    if (arrayIndex > -1)
        visibleIndicies.splice(arrayIndex, 1);
}
function OnExpanding(s, e) {
    AddCurrentDetailGrid(e.visibleIndex);
}
function OnCollapsing(s, e) {
    RemoveCurrentDetailGrid(e.visibleIndex);
}
function OnEndCallback(s, e) {
    if (buttonFlag)
        buttonFlag = false;
    if (errorMessage != "") {
        lbl.SetText(errorMessage);
        errorMessage = "";
    }
    else
        lbl.SetText("");
}
function OnCallbackError(s, e) {
    errorMessage = e.message;
    e.handled = true;
}