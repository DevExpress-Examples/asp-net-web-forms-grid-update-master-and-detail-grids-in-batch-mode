<!-- default file list -->
*Files to look at*:

* **[Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))**
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
* [UpdateLogic.js](./CS/WebSite/UpdateLogic.js) (VB: [UpdateLogic.js](./VB/WebSite/UpdateLogic.js))
<!-- default file list end -->
# How to update master and detail grids simultaneously in Batch Edit mode
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t146190/)**
<!-- run online end -->


<p>It's necessary to perform the following steps to accomplish this task:</p>
<p><br />1) Hide built-in command buttons using the <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_CommandButtonInitializetopic">CommandButtonInitialize</a>  event and create custom ones;<br />2) Handle the client-side<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_DetailRowCollapsingtopic"> DetailRowCollapsing</a> , <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_DetailRowExpandingtopic">DetailRowExpanding</a> events to track what detail grids are currently expanded; <br />3) Use the master grid's custom callback (the client-side <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_PerformCallbacktopic">PerformCallback</a>  method) to update all grids simultaneously on the server side;<br />4) Use the <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditConfirmShowingtopic">BatchEditConfirmShowing</a>  event to avoid losing changes in detail grids on an external callback;<br />5) Handle the server-side <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_CustomCallbacktopic">CustomCallback</a>  event to get all detail grids using the <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_FindDetailRowTemplateControltopic">FindDetailRowTemplateControl</a>  method and update them using the <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_UpdateEdittopic">UpdateEdit</a>  method.<br /><br />The attached example illustrates how to implement all these steps. </p>

<br/>


