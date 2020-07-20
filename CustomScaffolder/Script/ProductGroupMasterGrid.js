$(function () {
var urlTemp="/Master/ProductGroupMasters";
$("#jqGrid").jqGrid({
url: urlTemp+"/GetAll",
datatype: 'json',
mtype: 'Get',
colNames: [
'ID','ProductGroupName','PurchaseRate','UnitMasterID','IsActive','IntroductionDate'
],
colModel: [
    { key: true, hidden: true, name: 'ID', index: 'ID', editable: true },
    { key: true, name: 'ProductGroupName', index: 'ProductGroupName', editable: true },
    { key: true, name: 'PurchaseRate', index: 'PurchaseRate', editable: true },
    { key: true, name: 'UnitMasterID', index: 'UnitMasterID', editable: true, edittype: 'select', editoptions: { dataUrl: "/Master/UnitMasters/GetUnits", buildSelect: function (response) { var data = typeof response === "string" ? $.parseJSON(response) : response; var s = "<select>"; s += '<option value="0">Please Select</option>'; for (var i = 0; i < data['rows'].length; i++){ s += '<option value="' + data['rows'][i].ID + '">' + data['rows'][i].UnitName + '</option>'; } return s + '</select>'; } }, },
    { key: false, name: 'IsActive', index: 'IsActive', editable: true, edittype: 'checkbox', editoptions: { value: "True: False" }, formatter: "checkbox", formatoptions: { disabled: false } },
    { key: true, name: 'IntroductionDate', index: 'IntroductionDate', editable: true, formatter: 'date', formatoptions: { srcformat: 'm/d/Y', newformat: 'ShortDate' }, editoptions: { dataInit: function (el) { setTimeout(function () { $(el).datepicker(); }, 200); } } },
],
pager: jQuery('#jqControls'),
rowNum: 10,
rowList: [10, 20, 30, 40, 50],
height: '100%',
viewrecords: true,
caption: 'ProductGroupMaster Records',
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
