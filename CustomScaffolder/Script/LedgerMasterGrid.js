$(function () {
var urlTemp="/Master/LedgerMasters";
$("#jqGrid").jqGrid({
url: urlTemp+"/GetAll",
datatype: 'json',
mtype: 'Get',
colNames: [
'ID','LedgerName','LedgerGroupMasterID','FirmMasterID','NamePrintable','IsCheckStockAccount','Street','Area','City','Pin','State','Country','Transport','Phone','Fax','CellNo','Email','TinNo','GSTNo','Details','Supplier','Roller','JobWorker','Customer','Dyer','Winder','Weaver','Employee','Agent','Warper','Processor','AgentName','Image','CommisionPercentage','IsActive','ShortCode','Line','TransportType','DeliveryType','LCParty','DueDate','RegularDiscount','Wages','TDS','ContactPerson','RetailAgent','Broker','LedgerPartyTypeMasterID','BranchMasterID'
],
colModel: [
{ key: true, hidden: true, name: 'ID', index: 'ID', editable: true },
{ key: true, name: 'LedgerName', index: 'LedgerName', editable: true },
{key: true, name: 'LedgerGroupMasterID', index: 'LedgerGroupMasterID', editable: true,edittype: 'select',editoptions:{dataUrl: "/Master/UnitMasters/GetUnits",buildSelect: function(response) {var data = typeof response === "string" ? $.parseJSON(response) : response;var s = "<select>";s += '<option value="0">Please Select</option>';for (var i = 0; i < data['rows'].length; i++){s += '<option value="' + data['rows'][i].ID + '">' + data['rows'][i].UnitName + '</option>';}return s + '</select>';}},},
{key: true, name: 'FirmMasterID', index: 'FirmMasterID', editable: true,edittype: 'select',editoptions:{dataUrl: "/Master/UnitMasters/GetUnits",buildSelect: function(response) {var data = typeof response === "string" ? $.parseJSON(response) : response;var s = "<select>";s += '<option value="0">Please Select</option>';for (var i = 0; i < data['rows'].length; i++){s += '<option value="' + data['rows'][i].ID + '">' + data['rows'][i].UnitName + '</option>';}return s + '</select>';}},},
{ key: true, name: 'NamePrintable', index: 'NamePrintable', editable: true },
{key: false, name: 'IsCheckStockAccount', index: 'IsCheckStockAccount', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" },formatter: "checkbox", formatoptions: { disabled: false }},
{ key: true, name: 'Street', index: 'Street', editable: true },
{ key: true, name: 'Area', index: 'Area', editable: true },
{ key: true, name: 'City', index: 'City', editable: true },
{ key: true, name: 'Pin', index: 'Pin', editable: true },
{ key: true, name: 'State', index: 'State', editable: true },
{ key: true, name: 'Country', index: 'Country', editable: true },
{ key: true, name: 'Transport', index: 'Transport', editable: true },
{ key: true, name: 'Phone', index: 'Phone', editable: true },
{ key: true, name: 'Fax', index: 'Fax', editable: true },
{ key: true, name: 'CellNo', index: 'CellNo', editable: true },
{ key: true, name: 'Email', index: 'Email', editable: true },
{ key: true, name: 'TinNo', index: 'TinNo', editable: true },
{ key: true, name: 'GSTNo', index: 'GSTNo', editable: true },
{ key: true, name: 'Details', index: 'Details', editable: true },
{key: false, name: 'Supplier', index: 'Supplier', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" },formatter: "checkbox", formatoptions: { disabled: false }},
{key: false, name: 'Roller', index: 'Roller', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" },formatter: "checkbox", formatoptions: { disabled: false }},
{key: false, name: 'JobWorker', index: 'JobWorker', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" },formatter: "checkbox", formatoptions: { disabled: false }},
{key: false, name: 'Customer', index: 'Customer', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" },formatter: "checkbox", formatoptions: { disabled: false }},
{key: false, name: 'Dyer', index: 'Dyer', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" },formatter: "checkbox", formatoptions: { disabled: false }},
{key: false, name: 'Winder', index: 'Winder', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" },formatter: "checkbox", formatoptions: { disabled: false }},
{key: false, name: 'Weaver', index: 'Weaver', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" },formatter: "checkbox", formatoptions: { disabled: false }},
{key: false, name: 'Employee', index: 'Employee', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" },formatter: "checkbox", formatoptions: { disabled: false }},
{key: false, name: 'Agent', index: 'Agent', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" },formatter: "checkbox", formatoptions: { disabled: false }},
{key: false, name: 'Warper', index: 'Warper', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" },formatter: "checkbox", formatoptions: { disabled: false }},
{key: false, name: 'Processor', index: 'Processor', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" },formatter: "checkbox", formatoptions: { disabled: false }},
{ key: true, name: 'AgentName', index: 'AgentName', editable: true },
{ key: true, name: 'Image', index: 'Image', editable: true },
{ key: true, name: 'CommisionPercentage', index: 'CommisionPercentage', editable: true },
{key: false, name: 'IsActive', index: 'IsActive', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" },formatter: "checkbox", formatoptions: { disabled: false }},
{ key: true, name: 'ShortCode', index: 'ShortCode', editable: true },
{ key: true, name: 'Line', index: 'Line', editable: true },
{ key: true, name: 'TransportType', index: 'TransportType', editable: true },
{ key: true, name: 'DeliveryType', index: 'DeliveryType', editable: true },
{ key: true, name: 'LCParty', index: 'LCParty', editable: true },
{ key: true, name: 'DueDate', index: 'DueDate', editable: true },
{ key: true, name: 'RegularDiscount', index: 'RegularDiscount', editable: true },
{ key: true, name: 'Wages', index: 'Wages', editable: true },
{ key: true, name: 'TDS', index: 'TDS', editable: true },
{ key: true, name: 'ContactPerson', index: 'ContactPerson', editable: true },
{ key: true, name: 'RetailAgent', index: 'RetailAgent', editable: true },
{ key: true, name: 'Broker', index: 'Broker', editable: true },
{key: true, name: 'LedgerPartyTypeMasterID', index: 'LedgerPartyTypeMasterID', editable: true,edittype: 'select',editoptions:{dataUrl: "/Master/UnitMasters/GetUnits",buildSelect: function(response) {var data = typeof response === "string" ? $.parseJSON(response) : response;var s = "<select>";s += '<option value="0">Please Select</option>';for (var i = 0; i < data['rows'].length; i++){s += '<option value="' + data['rows'][i].ID + '">' + data['rows'][i].UnitName + '</option>';}return s + '</select>';}},},
{key: true, name: 'BranchMasterID', index: 'BranchMasterID', editable: true,edittype: 'select',editoptions:{dataUrl: "/Master/UnitMasters/GetUnits",buildSelect: function(response) {var data = typeof response === "string" ? $.parseJSON(response) : response;var s = "<select>";s += '<option value="0">Please Select</option>';for (var i = 0; i < data['rows'].length; i++){s += '<option value="' + data['rows'][i].ID + '">' + data['rows'][i].UnitName + '</option>';}return s + '</select>';}},},

],
pager: jQuery('#jqControls'),
rowNum: 10,
rowList: [10, 20, 30, 40, 50],
height: '100%',
viewrecords: true,
caption: 'LedgerMaster Records',
emptyrecords: 'No Records are Available to Display',
jsonReader:
{
root: "rows",
page: "page",
total: "total",
records: "records",
repeatitems: false,
Id: "0"
},
autowidth: true,
multiselect: false
}).navGrid('#jqControls', { edit: true, add: true, del: true, search: false, refresh: true },
{
zIndex: 100,
url: urlTemp + '/Edit',
closeOnEscape: true,
closeAfterEdit: true,
recreateForm: true,
afterComplete: function(response) {
if (response.responseText)
{
alert(response.responseText);
}
}
},
{
zIndex: 100,
url: urlTemp + "/Create",
closeOnEscape: true,
closeAfterAdd: true,
afterComplete: function(response) {
if (response.responseText)
{
alert(response.responseText);
}
}
},
{
zIndex: 100,
url: urlTemp + "/Delete",
closeOnEscape: true,
closeAfterDelete: true,
recreateForm: true,
msg: "Are you sure you want to delete.. ? ",
afterComplete: function(response) {
if (response.responseText)
{
alert(response.responseText);
}
}
});
});
