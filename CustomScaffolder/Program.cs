using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomScaffolder
{
    class Program
    {
        static void Main(string[] args)
        {
            GetMyProperties(new LedgerRemarkMaster());
            Console.ReadLine();
        }

        private static void GetMyProperties(object obj)
        {
            //Get the Class Name
            //Console.WriteLine(obj.GetType().Name);
            string strClassName = obj.GetType().Name;
            string strPropertyName = "";
            foreach (PropertyInfo pinfo in obj.GetType().GetProperties())
            {
                //Get the Property datatype name
                //Console.WriteLine(pinfo.PropertyType.Name);

                //Get the Property Name
                //Console.WriteLine(pinfo.Name);
                strPropertyName += pinfo.Name + ",";
            }
            strPropertyName = strPropertyName.Remove(strPropertyName.Length - 1);
            //Step:1=> Generate the UOW Classes
            CreateUOWClass(strClassName);

            //Step:2=> Generate the Controller Classes
            CreateControllerClass(strClassName, strPropertyName, obj);

            //Step:3=> Generate the Identity File
            CreateIdentityFile(strClassName);

            //Step:4=> Generate the Migration file
            CreateMigrationFile(strClassName);

            //Step:5=> Generate the View File
            CreateViewFile(strClassName);

            //Step:6=> Generate the Scripts Files
            CreateJQGridScriptFile(strClassName, obj);
        }

        private static void CreateJQGridScriptFile(string strClassName, object obj)
        {
            string fileName = @"C:\Users\Living Room\source\repos\CustomScaffolder\CustomScaffolder\Script\" + strClassName + "Grid.js";
            try
            {
                //Check If the file Exists
                if (File.Exists(fileName))
                {
                    //Delete the File
                    File.Delete(fileName);
                }

                // Create a new file 
                using (FileStream fs = File.Create(fileName))
                {
                    StringBuilder struow = new StringBuilder();
                    struow.AppendLine("$(function () {");
                    struow.AppendLine("var urlTemp=\"/Master/" + strClassName + "s\";");
                    struow.AppendLine("$(\"#jqGrid\").jqGrid({");
                    struow.AppendLine("url: urlTemp+\"/GetAll\",");
                    struow.AppendLine("datatype: 'json',");
                    struow.AppendLine("mtype: 'Get',");

                    struow.AppendLine("colNames: [");
                    string strColNames = "";
                    foreach (PropertyInfo pinfo in obj.GetType().GetProperties())
                    {
                        strColNames += "'" + pinfo.Name + "',";

                    }
                    strColNames = strColNames.Remove(strColNames.Length - 1);
                    struow.AppendLine(strColNames);

                    struow.AppendLine("],");

                    struow.AppendLine("colModel: [");

                    string strColModels = "";
                    StringBuilder temp = new StringBuilder();
                    foreach (PropertyInfo pinfo in obj.GetType().GetProperties())
                    {
                        if (pinfo.Name.ToUpper() == "ID")
                        {
                            temp.AppendLine("{ key: true, hidden: true, name: 'ID', index: 'ID', editable: true },");
                        }
                        //else if (pinfo.Name.ToUpper() == "ISACTIVE")
                        //{
                        //    strColModels += "{" +
                        //    "key: false, name: 'IsActive', index: 'IsActive', editable: true, edittype: 'checkbox', editoptions: { value: \"True: False\" }," +
                        //     "formatter: \"checkbox\", formatoptions: { disabled: false }" +
                        // "},";
                        //}
                        else if (pinfo.PropertyType.Name.ToUpper() == "BOOLEAN")
                        {
                            temp.AppendLine("{" +
                           "key: false, name: '" + pinfo.Name + "', index: '" + pinfo.Name + "', editable: true, edittype: 'checkbox', editoptions: { value: \"True: False\" }," +
                            "formatter: \"checkbox\", formatoptions: { disabled: false }" +
                        "},");
                        }
                        else if (pinfo.PropertyType.Name.ToUpper() == "DATETIME")
                        {
                            temp.AppendLine(" {" +
                            "key: false, name: '" + pinfo.Name + "', index: '" + pinfo.Name + "', editable: true, formatter: 'date'," +
                            "formatoptions: { srcformat: 'm/d/Y', newformat: 'ShortDate' }," +
                            "editoptions: { dataInit: function(el) { setTimeout(function() { $(el).datepicker(); }, 200); } }" +
                            "},");
                        }
                        else if (pinfo.Name.ToUpper().Contains("MASTERID"))
                        {
                            temp.AppendLine("{" +
                            "key: false, name: '" + pinfo.Name + "', index: '" + pinfo.Name + "', editable: true," +
                            "edittype: 'select'," +
                            "editoptions:" +
                            "{" +

                                "dataUrl: \"/Master/UnitMasters/GetUnits\"," +
                                    "buildSelect: function(response) {" +

                                    "var data = typeof response === \"string\" ? $.parseJSON(response) : response;" +
                                    "var s = \"<select>\";" +
                                    "s += '<option value=\"0\">Please Select</option>';" +
                                    "for (var i = 0; i < data['rows'].length; i++)" +
                                    "{" +
                                        "s += '<option value=\"' + data['rows'][i].ID + '\">' + data['rows'][i].UnitName + '</option>';" +
                                    "}" +
                                    "return s + '</select>';" +
                                    "}" +
                                "}," +
                            "},");
                        }
                        else
                        {
                            temp.AppendLine("{ key: false, name: " + "'" + pinfo.Name + "'" + ", index: " + "'" + pinfo.Name + "'" + ", editable: true },");
                        }

                    }
                    struow.AppendLine(temp.ToString());
                    struow.AppendLine("],");

                    struow.AppendLine("pager: jQuery('#jqControls'),");
                    struow.AppendLine("rowNum: 10,");

                    struow.AppendLine("rowList: [10, 20, 30, 40, 50],");
                    struow.AppendLine("height: '100%',");

                    struow.AppendLine("viewrecords: true,");
                    struow.AppendLine("caption: '" + strClassName + " Records',");

                    struow.AppendLine("emptyrecords: 'No Records are Available to Display',");

                    struow.AppendLine("jsonReader:");
                    struow.AppendLine("{");
                    struow.AppendLine("root: \"rows\",");
                    struow.AppendLine("page: \"page\",");
                    struow.AppendLine("total: \"total\",");
                    struow.AppendLine("records: \"records\",");
                    struow.AppendLine("repeatitems: false,");
                    struow.AppendLine("Id: \"0\"");
                    struow.AppendLine("},");
                    struow.AppendLine("autowidth: true,");
                    struow.AppendLine("multiselect: false");

                    struow.AppendLine("}).navGrid('#jqControls', { edit: true, add: true, del: true, search: false, refresh: true },");
                    struow.AppendLine("{");

                    struow.AppendLine("zIndex: 100,");

                    struow.AppendLine("url: urlTemp + '/Edit',");

                    struow.AppendLine("closeOnEscape: true,");

                    struow.AppendLine("closeAfterEdit: true,");

                    struow.AppendLine("recreateForm: true,");

                    struow.AppendLine("afterComplete: function(response) {");
                    struow.AppendLine("if (response.responseText)");
                    struow.AppendLine("{");

                    struow.AppendLine("alert(response.responseText);");
                    struow.AppendLine("}");
                    struow.AppendLine("}");
                    struow.AppendLine("},");

                    struow.AppendLine("{");
                    struow.AppendLine("zIndex: 100,");

                    struow.AppendLine("url: urlTemp + \"/Create\",");
                    struow.AppendLine("closeOnEscape: true,");

                    struow.AppendLine("closeAfterAdd: true,");
                    struow.AppendLine("afterComplete: function(response) {");
                    struow.AppendLine("if (response.responseText)");
                    struow.AppendLine("{");

                    struow.AppendLine("alert(response.responseText);");
                    struow.AppendLine("}");
                    struow.AppendLine("}");
                    struow.AppendLine("},");

                    struow.AppendLine("{");
                    struow.AppendLine("zIndex: 100,");

                    struow.AppendLine("url: urlTemp + \"/Delete\",");
                    struow.AppendLine("closeOnEscape: true,");

                    struow.AppendLine("closeAfterDelete: true,");
                    struow.AppendLine("recreateForm: true,");
                    struow.AppendLine("msg: \"Are you sure you want to delete.. ? \",");
                    struow.AppendLine("afterComplete: function(response) {");
                    struow.AppendLine("if (response.responseText)");
                    struow.AppendLine("{");

                    struow.AppendLine("alert(response.responseText);");
                    struow.AppendLine("}");
                    struow.AppendLine("}");
                    struow.AppendLine("});");
                    struow.AppendLine("});");


                    Byte[] title = new UTF8Encoding(true).GetBytes(struow.ToString());
                    fs.Write(title, 0, title.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to Create the Javascript " + ex.Message);
            }
            Console.WriteLine("Javascript Created successfully with the file name " + fileName);
        }

        private static void CreateViewFile(string strClassName)
        {
            string fileName = @"C:\Users\Living Room\source\repos\CustomScaffolder\CustomScaffolder\View\" + strClassName + "_view.cshtml";
            try
            {
                //Check If the file Exists
                if (File.Exists(fileName))
                {
                    //Delete the File
                    File.Delete(fileName);
                }

                // Create a new file 
                using (FileStream fs = File.Create(fileName))
                {
                    StringBuilder struow = new StringBuilder();
                    struow.AppendLine("@{");

                    struow.AppendLine("ViewBag.Title = \"Index\";");
                    struow.AppendLine("}");

                    struow.AppendLine("<h3>" + strClassName + "</h3>");
                    struow.AppendLine("<div>");
                    struow.AppendLine("<table id=\"jqGrid\"></table>");

                    struow.AppendLine("<div id=\"jqControls\"></div>");
                    struow.AppendLine("</div>");

                    struow.AppendLine("@section scripts{");
                    struow.AppendLine("<script src=\"~/Scripts/JqGrids/" + strClassName + "Grid.js\"></script>");
                    struow.AppendLine("}");

                    Byte[] title = new UTF8Encoding(true).GetBytes(struow.ToString());
                    fs.Write(title, 0, title.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to Create the Migration Class " + ex.Message);
            }
            Console.WriteLine("Migration Class Created successfully with the file name " + fileName);
        }

        private static void CreateMigrationFile(string strClassName)
        {
            string fileName = @"C:\Users\Living Room\source\repos\CustomScaffolder\CustomScaffolder\Migration\" + strClassName + "_Migration.cs";
            try
            {
                //Check If the file Exists
                if (File.Exists(fileName))
                {
                    //Delete the File
                    File.Delete(fileName);
                }

                // Create a new file 
                using (FileStream fs = File.Create(fileName))
                {
                    StringBuilder struow = new StringBuilder();
                    struow.AppendLine("Add-Migration " + strClassName + "_Added");
                    struow.AppendLine("Update-Database");
                    Byte[] title = new UTF8Encoding(true).GetBytes(struow.ToString());
                    fs.Write(title, 0, title.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to Create the Migration Class " + ex.Message);
            }
            Console.WriteLine("Migration Class Created successfully with the file name " + fileName);
        }

        private static void CreateIdentityFile(string strClassName)
        {
            string fileName = @"C:\Users\Living Room\source\repos\CustomScaffolder\CustomScaffolder\Identity\" + strClassName + "_Identity.cs";
            try
            {
                //Check If the file Exists
                if (File.Exists(fileName))
                {
                    //Delete the File
                    File.Delete(fileName);
                }

                // Create a new file 
                using (FileStream fs = File.Create(fileName))
                {
                    StringBuilder struow = new StringBuilder();
                    struow.AppendLine("public DbSet<" + strClassName + "> " + strClassName + "s {get;set;}");
                    Byte[] title = new UTF8Encoding(true).GetBytes(struow.ToString());
                    fs.Write(title, 0, title.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to Create the Identity Class " + ex.Message);
            }
            Console.WriteLine("Identity Class Created successfully with the file name " + fileName);
        }


        private static void CreateControllerClass(string strClassName, string PropertyName, object obj)
        {
            string fileName = @"C:\Users\Living Room\source\repos\CustomScaffolder\CustomScaffolder\Controller\" + strClassName + "sController.cs";
            string RepositoryName = strClassName + "Repository";
            try
            {
                //Check If the file Exists
                if (File.Exists(fileName))
                {
                    //Delete the File
                    File.Delete(fileName);
                }

                // Create a new file 
                using (FileStream fs = File.Create(fileName))
                {
                    string _value = "_" + strClassName.First().ToString().ToLower() + strClassName.Substring(1) + "Repository";

                    StringBuilder struow = new StringBuilder();
                    struow.AppendLine("[Authorize]");
                    struow.AppendLine("public class " + strClassName + "sController : Controller");
                    struow.AppendLine("{");
                    struow.AppendLine("private UnitOfWork _uow;");

                    //Constructor
                    struow.AppendLine("public " + strClassName + "sController()");
                    struow.AppendLine("{");
                    struow.AppendLine("this._uow = new UnitOfWork();");
                    struow.AppendLine("}");

                    //Constructor Injection
                    struow.AppendLine("public " + strClassName + "sController(UnitOfWork unitOfWork)");
                    struow.AppendLine("{");
                    struow.AppendLine("this._uow = unitOfWork;");
                    struow.AppendLine("}");

                    //Index Method
                    struow.AppendLine("public ActionResult Index()");
                    struow.AppendLine("{");
                    struow.AppendLine("return View();");
                    struow.AppendLine("}");

                    //Get All Items
                    struow.AppendLine("public JsonResult GetAll(string sidx = null, string sort = null, int page = 0, int rows = 0)");
                    struow.AppendLine("{");
                    struow.AppendLine("sort = (sort == null) ? \"\" : sort;");
                    struow.AppendLine("int pageIndex = Convert.ToInt32(page) - 1;");
                    struow.AppendLine("int pageSize = rows;");
                    struow.AppendLine("var List = _uow." + RepositoryName + ".Get();");
                    struow.AppendLine("var data = List.Select(");
                    struow.AppendLine(" t => new");
                    struow.AppendLine("{");

                    foreach (PropertyInfo pinfo in obj.GetType().GetProperties())
                    {
                        struow.AppendLine(pinfo.Name + "=t." + pinfo.Name + ",");
                    }

                    struow.AppendLine("}");
                    struow.AppendLine(");");

                    struow.AppendLine("int totalRecords = List.Count();");
                    struow.AppendLine("var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);");
                    struow.AppendLine("if (sort.ToUpper() == \"DESC\")");
                    struow.AppendLine("{");

                    struow.AppendLine("List = List.OrderByDescending(t => t.ColorName);");
                    struow.AppendLine("List = List.Skip(pageIndex * pageSize).Take(pageSize);");
                    struow.AppendLine("}");
                    struow.AppendLine("else");
                    struow.AppendLine("{");

                    struow.AppendLine("List = List.OrderBy(t => t.ColorName);");
                    struow.AppendLine("List = List.Skip(pageIndex * pageSize).Take(pageSize);");
                    struow.AppendLine("}");

                    struow.AppendLine("var jsonData = new");
                    struow.AppendLine("{");

                    struow.AppendLine("total = totalPages,");

                    struow.AppendLine("page,");

                    struow.AppendLine("records = totalRecords,");

                    struow.AppendLine("rows = data");
                    struow.AppendLine("};");
                    struow.AppendLine("return Json(jsonData, JsonRequestBehavior.AllowGet);");
                    struow.AppendLine("}");

                    //Details Method
                    struow.AppendLine("public ActionResult Details(int? id)");
                    struow.AppendLine("{");
                    struow.AppendLine("if (id == null)");
                    struow.AppendLine("{");
                    struow.AppendLine("return new HttpStatusCodeResult(HttpStatusCode.BadRequest);");
                    struow.AppendLine("}");

                    struow.AppendLine(strClassName + " getMaster = _uow." + RepositoryName + ".GetByID(id);");
                    struow.AppendLine("if (getMaster == null)");
                    struow.AppendLine("{");
                    struow.AppendLine("return HttpNotFound();");
                    struow.AppendLine("}");
                    struow.AppendLine("return View(getMaster);");
                    struow.AppendLine("}");

                    //Create Method
                    struow.AppendLine("public string Create([Bind(Include =\"" + PropertyName + "\" , Exclude = \"ID\")] " + strClassName + " objMaster)");
                    struow.AppendLine("{");
                    struow.AppendLine("string msg = \"\";");
                    struow.AppendLine("try");
                    struow.AppendLine("{");
                    struow.AppendLine("if (ModelState.IsValid)");
                    struow.AppendLine("{");

                    struow.AppendLine("_uow." + RepositoryName + ".Insert(objMaster);");

                    struow.AppendLine("_uow.Save();");

                    struow.AppendLine("msg = \"Saved Successully\";");
                    struow.AppendLine("}");
                    struow.AppendLine("else");
                    struow.AppendLine("{");

                    struow.AppendLine("msg = \"Validation Error\";");
                    struow.AppendLine("}");
                    struow.AppendLine("}");
                    struow.AppendLine("catch (Exception ex)");
                    struow.AppendLine("{");

                    struow.AppendLine("msg = \"Error while Processing the Reqeust \" + ex.Message;");
                    struow.AppendLine("}");

                    struow.AppendLine("return msg;");
                    struow.AppendLine("}");

                    //Edit Method
                    struow.AppendLine("public string Edit([Bind(Include = \"" + PropertyName + "\")] " + strClassName + " objMaster)");
                    struow.AppendLine("{");
                    struow.AppendLine("string msg = \"\";");
                    struow.AppendLine("try");
                    struow.AppendLine("{");
                    struow.AppendLine("if (ModelState.IsValid)");
                    struow.AppendLine("{");

                    struow.AppendLine("_uow." + RepositoryName + ".Update(objMaster);");

                    struow.AppendLine("_uow.Save();");

                    struow.AppendLine("msg = \"Updated Successully\";");
                    struow.AppendLine("}");
                    struow.AppendLine("else");
                    struow.AppendLine("{");

                    struow.AppendLine("msg = \"Update Validation Error\";");
                    struow.AppendLine("}");
                    struow.AppendLine("}");
                    struow.AppendLine("catch (Exception ex)");
                    struow.AppendLine("{");

                    struow.AppendLine("msg = \"Error while Processing the Request \" + ex.Message;");
                    struow.AppendLine("}");
                    struow.AppendLine("return msg;");

                    struow.AppendLine("}");

                    //Delete Method

                    struow.AppendLine("public string Delete(int id)");
                    struow.AppendLine("{");
                    struow.AppendLine("string msg = \"\";");
                    struow.AppendLine("try");
                    struow.AppendLine("{");

                    struow.AppendLine(strClassName + " objMaster = _uow." + RepositoryName + ".GetByID(id);");

                    struow.AppendLine("_uow." + RepositoryName + ".Delete(id);");

                    struow.AppendLine("_uow.Save();");

                    struow.AppendLine("msg = \"Deleted Successfully\";");
                    struow.AppendLine("}");
                    struow.AppendLine("catch (Exception ex)");
                    struow.AppendLine("{");

                    struow.AppendLine("msg = \"Error While Processing the Request \" + ex.Message;");
                    struow.AppendLine("}");
                    struow.AppendLine("return msg;");
                    struow.AppendLine("}");

                    //Dispose Method
                    struow.AppendLine("protected override void Dispose(bool disposing)");
                    struow.AppendLine("{");
                    struow.AppendLine("if (disposing)");
                    struow.AppendLine("{");

                    struow.AppendLine("_uow.Dispose();");
                    struow.AppendLine("}");

                    struow.AppendLine("base.Dispose(disposing);");
                    struow.AppendLine("}");

                    //Close the Class
                    struow.AppendLine("}");

                    Byte[] title = new UTF8Encoding(true).GetBytes(struow.ToString());
                    fs.Write(title, 0, title.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to Create the Controller Class " + ex.Message);
            }
            Console.WriteLine("Controller Class Created successfully with the file name " + fileName);

        }

        private static void CreateUOWClass(string strClassName)
        {
            string uowfileName = @"C:\Users\Living Room\source\repos\CustomScaffolder\CustomScaffolder\UOW\" + strClassName + "_UOW.cs";
            try
            {
                //Check If the file Exists
                if (File.Exists(uowfileName))
                {
                    //Delete the File
                    File.Delete(uowfileName);
                }

                // Create a new file 
                using (FileStream fs = File.Create(uowfileName))
                {
                    string _value = "_" + strClassName.First().ToString().ToLower() + strClassName.Substring(1) + "Repository";

                    StringBuilder struow = new StringBuilder();
                    struow.AppendLine("public partial class UnitOfWork ");
                    struow.AppendLine("{");
                    struow.AppendLine("private GenericRepository<" + strClassName + ">" + _value + ";");
                    struow.AppendLine("public GenericRepository<" + strClassName + "> " + strClassName + "Repository {");
                    struow.AppendLine(" get");
                    struow.AppendLine("{");
                    struow.AppendLine("if (this." + _value + " == null)");
                    struow.AppendLine("{");
                    struow.AppendLine("this." + _value + " = new GenericRepository<" + strClassName + ">(context);");
                    struow.AppendLine("}");
                    struow.AppendLine("return " + _value + ";");
                    struow.AppendLine("}");
                    struow.AppendLine("}");
                    struow.AppendLine("}");
                    Byte[] title = new UTF8Encoding(true).GetBytes(struow.ToString());
                    fs.Write(title, 0, title.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to Create the UOW Class " + ex.Message);
            }
            Console.WriteLine("UOW Class Created successfully with the file name " + uowfileName);

        }
    }

    public class ProductGroupMaster
    {
        public int ID { get; set; }
        public string ProductGroupName { get; set; }
        public decimal PurchaseRate { get; set; }
        public string UnitMasterID { get; set; }
        public bool IsActive { get; set; }
        public DateTime IntroductionDate { get; set; }
    }
}
