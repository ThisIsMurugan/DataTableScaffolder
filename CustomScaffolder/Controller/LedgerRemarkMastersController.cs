[Authorize]
public class LedgerRemarkMastersController : Controller
{
private UnitOfWork _uow;
public LedgerRemarkMastersController()
{
this._uow = new UnitOfWork();
}
public LedgerRemarkMastersController(UnitOfWork unitOfWork)
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
var List = _uow.LedgerRemarkMasterRepository.Get();
var data = List.Select(
 t => new
{
ID=t.ID,
Name=t.Name,
Description=t.Description,
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
LedgerRemarkMaster getMaster = _uow.LedgerRemarkMasterRepository.GetByID(id);
if (getMaster == null)
{
return HttpNotFound();
}
return View(getMaster);
}
public string Create([Bind(Include ="ID,Name,Description" , Exclude = "ID")] LedgerRemarkMaster objMaster)
{
string msg = "";
try
{
if (ModelState.IsValid)
{
_uow.LedgerRemarkMasterRepository.Insert(objMaster);
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
public string Edit([Bind(Include = "ID,Name,Description")] LedgerRemarkMaster objMaster)
{
string msg = "";
try
{
if (ModelState.IsValid)
{
_uow.LedgerRemarkMasterRepository.Update(objMaster);
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
LedgerRemarkMaster objMaster = _uow.LedgerRemarkMasterRepository.GetByID(id);
_uow.LedgerRemarkMasterRepository.Delete(id);
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
