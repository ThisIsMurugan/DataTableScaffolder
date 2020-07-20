public partial class UnitOfWork 
{
private GenericRepository<LedgerRemarkMaster>_ledgerRemarkMasterRepository;
public GenericRepository<LedgerRemarkMaster> LedgerRemarkMasterRepository {
 get
{
if (this._ledgerRemarkMasterRepository == null)
{
this._ledgerRemarkMasterRepository = new GenericRepository<LedgerRemarkMaster>(context);
}
return _ledgerRemarkMasterRepository;
}
}
}
