using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R2MD
{
    public class NormalObj
    {
        public string code { get; set; }
        public string message { get; set; }
        public string print { get; set; }
    }
    public class ServiceObj {
        public string sEcho { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public ServiceObj() {
            this.aaData = new List<ServiceData>();
        }
        public List<ServiceData> aaData { get; set; }
        
    }
    public class ServiceData {
        public string ServiceId { get; set; }
        public string Name { get; set; }
        public string MRN { get; set; }
        public string PatientId { get; set; }
        public string RequestType { get; set; }
        public string LevelofService { get; set; }
        public string ScheduleTime { get; set; }
        public string Status { get; set; }
        public string typeID { get; set; }
        public string RequestorId { get; set; }        
        public string Instruction { get; set; }
        public string PassengerNumber { get; set; }
        public string InterpretLanguage { get; set; }
        public string EquipmentType { get; set; }
        public string DiagnosticServices { get; set; }
        public string Payor { get; set; }
        public string PayorNumber { get; set; }
        public string HomeServiceType { get; set; }
        public string TransportType { get; set; }
        public string InterpretAtPickup { get; set; }
        public string InterpretAtDropoff { get; set; }
        public string CreateDateTime { get; set; }
        public string InsurancePlanId { get; set; }
        public string mileage { get; set; }
        public string driverassigned { get; set; }
        public string roundtrip { get; set; }
        public string compaions { get; set; }
        public string PayorName { get; set; }
        public string pickName { get; set; }
        public string pickAddress1 { get; set; }
        public string pickAddress2 { get; set; }
        public string pickCity { get; set; }
        public string pickState { get; set; }
        public string pickZip { get; set; }
        public string pickPhone { get; set; }
        public string dropName { get; set; }
        public string dropAddress1 { get; set; }
        public string dropAddress2 { get; set; }
        public string dropCity { get; set; }
        public string dropState { get; set; }
        public string dropZip { get; set; }
        public string dropPhone { get; set; }
        public string Provider { get; set; }
        public string PatientAddress1 { get; set; }
        public string PatientAddress2 { get; set; }
        public string PatientCity { get; set; }
        public string PatientState{ get; set; }
        public string empty { get; set; }
    }
    public class providerObj {
        public string providerID { get; set; }
        public string provider { get; set; }
    }
    public class payerObj {
        public string payerID { get; set; }
        public string payerName { get; set; }
    }
    public class PatientObj
    {
        public PatientObj()
        {
            this.data = new List<PatientData>();
        }
        public List<PatientData> data { get; set; }
    }
    public class PatientData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string SSN { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string MRN { get; set; }
        public string PatientID { get; set; }
        public string Info { get; set; }
        public string Payer { get; set; }
        public string PayerID { get; set; }
        public string ID { get; set; }
        public string data { get; set; }
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string zip { get; set; }
        public string phone { get; set; }
        public string BAdd1 { get; set; }
        public string BAdd2 { get; set; }
        public string BCity { get; set; }
        public string BState { get; set; }
        public string Bzip { get; set; }
        public string effectiveDate { get; set; }
    }

    public class InvoiceObj {
        public InvoiceObj()
        {
            this.data = new List<InvoiceData>();
        }
        public List<InvoiceData> data { get; set; }
    }
    public class InvoiceData {
        public string invoiceId { get; set; }
        public string ClientComapny { get; set; }
        public string Amount { get; set; }
        public string TripDate { get; set; }
        public string InvoiceDate { get; set; }
        public string PaidDate { get; set; }
        public string Status { get; set; }
        public string tripID { get; set; }
    }

    public class TripObj
    {
        public TripObj()
        {
            this.data = new List<TripData>();
        }
        public List<TripData> data { get; set; }
    }
    public class TripData
    {
        public string tripID { get; set; }
        public string PatientName { get; set; }
        public string MRN { get; set; }
        public string Service { get; set; }
        public string Level { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }
    }
}



 
