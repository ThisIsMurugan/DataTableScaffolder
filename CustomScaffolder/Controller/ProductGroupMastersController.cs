[Authorize]
public class ProductGroupMastersController : Controller
{
private UnitOfWork _uow;
public ProductGroupMastersController()
{
this._uow = new UnitOfWork();
}
public ProductGroupMastersController(UnitOfWork unitOfWork)
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
var List = _uow.ProductGroupMasterRepository.Get();
var data = List.Select(
 t => new
{
ID=t.ID,
ProductGroupName=t.ProductGroupName,
PurchaseRate=t.PurchaseRate,
UnitMasterID=t.UnitMasterID,
IsActive=t.IsActive,
IntroductionDate=t.IntroductionDate,
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
ProductGroupMaster getMaster = _uow.ProductGroupMasterRepository.GetByID(id);
if (getMaster == null)
{
return HttpNotFound();
}
return View(getMaster);
}
public string Create([Bind(Include ="ID,ProductGroupName,PurchaseRate,UnitMasterID,IsActive,IntroductionDate" , Exclude = "ID")] ProductGroupMaster objMaster)
{
string msg = "";
try
{
if (ModelState.IsValid)
{
_uow.ProductGroupMasterRepository.Insert(objMaster);
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
public string Edit([Bind(Include = "ID,ProductGroupName,PurchaseRate,UnitMasterID,IsActive,IntroductionDate")] ProductGroupMaster objMaster)
{
string msg = "";
try
{
if (ModelState.IsValid)
{
_uow.ProductGroupMasterRepository.Update(objMaster);
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
ProductGroupMaster objMaster = _uow.ProductGroupMasterRepository.GetByID(id);
_uow.ProductGroupMasterRepository.Delete(id);
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
