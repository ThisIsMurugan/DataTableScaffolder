[Authorize]
public class LedgerMastersController : Controller
{
private UnitOfWork _uow;
public LedgerMastersController()
{
this._uow = new UnitOfWork();
}
public LedgerMastersController(UnitOfWork unitOfWork)
{
this._uow = unitOfWork;
}
public ActionResult Index()
{
return View();
}
public JsonResult GetAll(string sidx = null, string sort = null, int page = 0, int rows = 0)
{
sort = (sort == null) ? "" : sort;
int pageIndex = Convert.ToInt32(page) - 1;
int pageSize = rows;
var List = _uow.LedgerMasterRepository.Get();
var data = List.Select(
 t => new
{
ID=t.ID,
LedgerName=t.LedgerName,
LedgerGroupMasterID=t.LedgerGroupMasterID,
FirmMasterID=t.FirmMasterID,
NamePrintable=t.NamePrintable,
IsCheckStockAccount=t.IsCheckStockAccount,
Street=t.Street,
Area=t.Area,
City=t.City,
Pin=t.Pin,
State=t.State,
Country=t.Country,
Transport=t.Transport,
Phone=t.Phone,
Fax=t.Fax,
CellNo=t.CellNo,
Email=t.Email,
TinNo=t.TinNo,
GSTNo=t.GSTNo,
Details=t.Details,
Supplier=t.Supplier,
Roller=t.Roller,
JobWorker=t.JobWorker,
Customer=t.Customer,
Dyer=t.Dyer,
Winder=t.Winder,
Weaver=t.Weaver,
Employee=t.Employee,
Agent=t.Agent,
Warper=t.Warper,
Processor=t.Processor,
AgentName=t.AgentName,
Image=t.Image,
CommisionPercentage=t.CommisionPercentage,
IsActive=t.IsActive,
ShortCode=t.ShortCode,
Line=t.Line,
TransportType=t.TransportType,
DeliveryType=t.DeliveryType,
LCParty=t.LCParty,
DueDate=t.DueDate,
RegularDiscount=t.RegularDiscount,
Wages=t.Wages,
TDS=t.TDS,
ContactPerson=t.ContactPerson,
RetailAgent=t.RetailAgent,
Broker=t.Broker,
LedgerPartyTypeMasterID=t.LedgerPartyTypeMasterID,
BranchMasterID=t.BranchMasterID,
}
);
int totalRecords = List.Count();
var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
if (sort.ToUpper() == "DESC")
{
List = List.OrderByDescending(t => t.ColorName);
List = List.Skip(pageIndex * pageSize).Take(pageSize);
}
else
{
List = List.OrderBy(t => t.ColorName);
List = List.Skip(pageIndex * pageSize).Take(pageSize);
}
var jsonData = new
{
total = totalPages,
page,
records = totalRecords,
rows = data
};
return Json(jsonData, JsonRequestBehavior.AllowGet);
}
public ActionResult Details(int? id)
{
if (id == null)
{
return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
}
LedgerMaster getMaster = _uow.LedgerMasterRepository.GetByID(id);
if (getMaster == null)
{
return HttpNotFound();
}
return View(getMaster);
}
public string Create([Bind(Include ="ID,LedgerName,LedgerGroupMasterID,FirmMasterID,NamePrintable,IsCheckStockAccount,Street,Area,City,Pin,State,Country,Transport,Phone,Fax,CellNo,Email,TinNo,GSTNo,Details,Supplier,Roller,JobWorker,Customer,Dyer,Winder,Weaver,Employee,Agent,Warper,Processor,AgentName,Image,CommisionPercentage,IsActive,ShortCode,Line,TransportType,DeliveryType,LCParty,DueDate,RegularDiscount,Wages,TDS,ContactPerson,RetailAgent,Broker,LedgerPartyTypeMasterID,BranchMasterID" , Exclude = "ID")] LedgerMaster objMaster)
{
string msg = "";
try
{
if (ModelState.IsValid)
{
_uow.LedgerMasterRepository.Insert(objMaster);
_uow.Save();
msg = "Saved Successully";
}
else
{
msg = "Validation Error";
}
}
catch (Exception ex)
{
msg = "Error while Processing the Reqeust " + ex.Message;
}
return msg;
}
public string Edit([Bind(Include = "ID,LedgerName,LedgerGroupMasterID,FirmMasterID,NamePrintable,IsCheckStockAccount,Street,Area,City,Pin,State,Country,Transport,Phone,Fax,CellNo,Email,TinNo,GSTNo,Details,Supplier,Roller,JobWorker,Customer,Dyer,Winder,Weaver,Employee,Agent,Warper,Processor,AgentName,Image,CommisionPercentage,IsActive,ShortCode,Line,TransportType,DeliveryType,LCParty,DueDate,RegularDiscount,Wages,TDS,ContactPerson,RetailAgent,Broker,LedgerPartyTypeMasterID,BranchMasterID")] LedgerMaster objMaster)
{
string msg = "";
try
{
if (ModelState.IsValid)
{
_uow.LedgerMasterRepository.Update(objMaster);
_uow.Save();
msg = "Updated Successully";
}
else
{
msg = "Update Validation Error";
}
}
catch (Exception ex)
{
msg = "Error while Processing the Request " + ex.Message;
}
return msg;
}
public string Delete(int id)
{
string msg = "";
try
{
LedgerMaster objMaster = _uow.LedgerMasterRepository.GetByID(id);
_uow.LedgerMasterRepository.Delete(id);
_uow.Save();
msg = "Deleted Successfully";
}
catch (Exception ex)
{
msg = "Error While Processing the Request " + ex.Message;
}
return msg;
}
protected override void Dispose(bool disposing)
{
if (disposing)
{
_uow.Dispose();
}
base.Dispose(disposing);
}
}
