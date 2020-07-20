$(function () {
var urlTemp="/Master/LedgerRemarkMasters";
$("#jqGrid").jqGrid({
url: urlTemp+"/GetAll",
datatype: 'json',
mtype: 'Get',
colNames: [
'ID','Name','Description'
],
colModel: [
{ key: true, hidden: true, name: 'ID', index: 'ID', editable: true },
{ key: false, name: 'Name', index: 'Name', editable: true },
{ key: false, name: 'Description', index: 'Description', editable: true },

],
pager: jQuery('#jqControls'),
rowNum: 10,
rowList: [10, 20, 30, 40, 50],
height: '100%',
viewrecords: true,
caption: 'LedgerRemarkMaster Records',
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
