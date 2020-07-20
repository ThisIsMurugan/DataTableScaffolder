public partial class UnitOfWork 
{
private GenericRepository<ProductGroupMaster>_productGroupMasterRepository;
public GenericRepository<ProductGroupMaster> ProductGroupMasterRepository {
 get
{
if (this._productGroupMasterRepository == null)
{
this._productGroupMasterRepository = new GenericRepository<ProductGroupMaster>(context);
}
return _productGroupMasterRepository;
}
}
}
