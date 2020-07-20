using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomScaffolder
{
    public class LedgerMaster
    {
        public int ID { get; set; }
        public string LedgerName { get; set; }
        public int LedgerGroupMasterID { get; set; }
        public int FirmMasterID { get; set; }
        public string NamePrintable { get; set; }
        public bool IsCheckStockAccount { get; set; }
        public string Street { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Transport { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string CellNo { get; set; }
        public string Email { get; set; }
        public string TinNo { get; set; }
        public string GSTNo { get; set; }
        public string Details { get; set; }
        public bool Supplier { get; set; }
        public bool Roller { get; set; }
        public bool JobWorker { get; set; }
        public bool Customer { get; set; }
        public bool Dyer { get; set; }
        public bool Winder { get; set; }
        public bool Weaver { get; set; }
        public bool Employee { get; set; }
        public bool Agent { get; set; }
        public bool Warper { get; set; }
        public bool Processor { get; set; }
        public string AgentName { get; set; }
        public string Image { get; set; }
        public decimal CommisionPercentage { get; set; }
        public bool IsActive { get; set; }
        public string ShortCode { get; set; }
        public string Line { get; set; }
        public string TransportType { get; set; }
        public string DeliveryType { get; set; }
        public string LCParty { get; set; }
        public int DueDate { get; set; }
        public decimal RegularDiscount { get; set; }
        public decimal Wages { get; set; }
        public string TDS { get; set; }
        public string ContactPerson { get; set; }
        public string RetailAgent { get; set; }
        public string Broker { get; set; }
        public int LedgerPartyTypeMasterID { get; set; }
        public int BranchMasterID { get; set; }

    }
}
