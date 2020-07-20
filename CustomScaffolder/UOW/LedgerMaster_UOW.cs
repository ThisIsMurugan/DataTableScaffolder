public partial class UnitOfWork 
{
private GenericRepository<LedgerMaster>_ledgerMasterRepository;
public GenericRepository<LedgerMaster> LedgerMasterRepository {
 get
{
if (this._ledgerMasterRepository == null)
{
this._ledgerMasterRepository = new GenericRepository<LedgerMaster>(context);
}
return _ledgerMasterRepository;
}
}
}
