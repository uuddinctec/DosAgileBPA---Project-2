using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace R2MD
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceRequest" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceRequest.svc or ServiceRequest.svc.cs at the Solution Explorer and start debugging.
    public class ServiceRequest : IServiceRequest
    {        
        public PatientObj GetPatientInfo(string FirstName = null, string LastName = null)
        {
            return _GetPatientInfo(FirstName, LastName);
        }
        private PatientObj _GetPatientInfo(string FirstName = null, string LastName = null)
        {
            PatientObj result = new PatientObj();
            try
            {
                Service se = new Service();
                DataTable dt = se.getPInfo(FirstName.ToLower(), LastName.ToLower());
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PatientData sd = new PatientData();
                        sd.FirstName = dr["FirstName"] != DBNull.Value ? dr["FirstName"].ToString() : "";
                        sd.LastName = dr["LastName"] != DBNull.Value ? dr["LastName"].ToString() : "";
                        sd.MRN = dr["MRN"] != DBNull.Value ? dr["MRN"].ToString() : "";
                        if (dr["PatientGender"] != DBNull.Value)
                        {
                            if (dr["PatientGender"].ToString().ToLower() == "male")
                            {
                                sd.Gender = "0";
                            }
                            else
                            {
                                sd.Gender = "1";
                            }
                        }
                        //sd.Gender = dr["PatientGender"] != DBNull.Value ? dr["PatientGender"].ToString() : "";
                        sd.Weight = dr["PatientWeight"] != DBNull.Value ? dr["PatientWeight"].ToString() : "";
                        sd.Height = dr["Height"] != DBNull.Value ? dr["Height"].ToString() : "";
                        sd.DOB = dr["PatientBirthDate"] != DBNull.Value ? dr["PatientBirthDate"].ToString() : "";
                        sd.SSN = dr["PatientSocialSecurity"] != DBNull.Value ? dr["PatientSocialSecurity"].ToString() : "";
                        sd.PatientID = dr["PatientId"] != DBNull.Value ? dr["PatientId"].ToString() : "";
                        sd.Info = dr["OtherInformation"] != DBNull.Value ? dr["OtherInformation"].ToString() : "";


                        if (dr["PatientInsuranceId"] != DBNull.Value)
                        {
                            if (dr["PatientInsurancePlanId"] != DBNull.Value)
                            {
                                sd.Payer = dr["PatientInsurancePayorName"] != DBNull.Value ? dr["PatientInsurancePayorName"].ToString() : "";
                                sd.PayerID = dr["PatientInsurancePlanId"].ToString();
                            }
                            else
                            {
                                sd.Payer = dr["PatientPayorName"] != DBNull.Value ? dr["PatientPayorName"].ToString() : "";
                                sd.PayerID = "";
                            }
                            sd.ID = dr["PatientPayorNumber"] != DBNull.Value ? dr["PatientPayorNumber"].ToString() : "";
                            sd.effectiveDate = dr["EffectiveDate"] != DBNull.Value ? Convert.ToDateTime(dr["EffectiveDate"].ToString()).ToString("yyyy-MM-dd")  : "";
                        }
                        //else //old payor data
                        //{
                        //    if (dr["InsurancePlanId"] != DBNull.Value)
                        //    {
                        //        sd.Payer = dr["PayorName"] != DBNull.Value ? dr["PayorName"].ToString() : "";
                        //        sd.PayerID = dr["InsurancePlanId"].ToString();
                        //    }
                        //    else
                        //    {
                        //        sd.Payer = dr["Payor"] != DBNull.Value ? dr["Payor"].ToString() : "";
                        //        sd.PayerID = "";
                        //    }
                        //    sd.ID = dr["PayorNumber"] != DBNull.Value ? dr["PayorNumber"].ToString() : "";
                        //    sd.effectiveDate = "";
                        //}

                        
                        //sd.Payer = dr["Payor"] != DBNull.Value ? dr["Payor"].ToString() : "";
                        
                        sd.Add1 = dr["Add1"] != DBNull.Value ? dr["Add1"].ToString() : "";
                        sd.Add2 = dr["Add2"] != DBNull.Value ? dr["Add2"].ToString() : "";
                        sd.City = dr["City"] != DBNull.Value ? dr["City"].ToString() : "";
                        sd.State = dr["State"] != DBNull.Value ? dr["State"].ToString() : "";
                        sd.zip = dr["zip"] != DBNull.Value ? dr["zip"].ToString() : "";
                        sd.phone=dr["Phone"] != DBNull.Value ? dr["Phone"].ToString() : "";
                        sd.data = sd.FirstName + " " + sd.LastName + " , " + sd.DOB + " , " + dr["PatientGender"].ToString() + " , " + sd.MRN + " , " + sd.SSN + " , " + sd.Weight + " , " + sd.Height + " , " + sd.Payer + " , " + sd.ID + " , " + sd.Add1 + " , " + sd.Add2 + " , " + sd.City + " , " + sd.State + " , " + sd.zip + " , " + sd.phone;
                        sd.BAdd1 = dr["BillingAddress1"] != DBNull.Value ? dr["BillingAddress1"].ToString() : "";
                        sd.BAdd2 = dr["BillingAddress2"] != DBNull.Value ? dr["BillingAddress2"].ToString() : "";
                        sd.BCity = dr["BillingCity"] != DBNull.Value ? dr["BillingCity"].ToString() : "";
                        sd.BState = dr["BillingState"] != DBNull.Value ? dr["BillingState"].ToString() : "";
                        sd.Bzip = dr["BillingZip"] != DBNull.Value ? dr["BillingZip"].ToString() : "";





                        result.data.Add(sd);
                    }
                }


            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw;
                    else
                        throw exceptionToThrow;
                }
            }
            return result;
        }


        public InvoiceObj GetInvoice(string Name = null, string Client = null, string dateFrom = null, string Invoice = null, string Status = null, string id = null) {
            return _GetInvoice(Name, Client, dateFrom, Invoice, Status,id);
        }
        private InvoiceObj _GetInvoice(string Name = null, string Client = null, string dateFrom = null, string Invoice = null, string Status = null, string id = null) {
            InvoiceObj result = new InvoiceObj();
            try
            {
                Service se = new Service();
                DataTable dt = se.Invoice(Name, Client, dateFrom, Invoice, Status, id);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        InvoiceData sd = new InvoiceData();
                        sd.invoiceId = dr["InvoiceId"] != DBNull.Value ? dr["InvoiceId"].ToString() : "";
                        sd.ClientComapny = dr["Client"] != DBNull.Value ? dr["Client"].ToString() : "";
                        sd.Amount = dr["Amount"] != DBNull.Value ? dr["Amount"].ToString() : "";
                        sd.PaidDate = dr["PayDate"] != DBNull.Value ? dr["PayDate"].ToString() : "";
                        sd.Status = dr["InvoiceStatus"] != DBNull.Value ? dr["InvoiceStatus"].ToString() : "";
                        sd.TripDate = dr["tripdate"] != DBNull.Value ? dr["tripdate"].ToString() : "";
                        sd.InvoiceDate = dr["BillDate"] != DBNull.Value ? dr["BillDate"].ToString() : "";
                        sd.tripID= dr["serviceID"] != DBNull.Value ? dr["serviceID"].ToString() : "";
                        result.data.Add(sd);
                    }
                }
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw;
                    else
                        throw exceptionToThrow;
                }
            }
            return result;
        }


        public TripObj GetTrip(string tripID) {
            return _GetTrip(tripID);
        }
        private TripObj _GetTrip(string id) {
            TripObj result = new TripObj();
            try
            {
                Service se = new Service();
                DataTable dt = se.GetTripInfo(id);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TripData sd = new TripData();
                        sd.tripID = dr["ID"] != DBNull.Value ? dr["ID"].ToString() : "";
                        sd.PatientName = dr["FullName"] != DBNull.Value ? dr["FullName"].ToString() : "";
                        sd.MRN = dr["MRN"] != DBNull.Value ? dr["MRN"].ToString() : "";
                        sd.Service = dr["RequestType"] != DBNull.Value ? dr["RequestType"].ToString() : "";
                        sd.Level = dr["LevelofService"] != DBNull.Value ? dr["LevelofService"].ToString() : "";
                        sd.Date = dr["ScheduleTime"] != DBNull.Value ? dr["ScheduleTime"].ToString() : "";
                        sd.Status = dr["Status"] != DBNull.Value ? dr["Status"].ToString() : "";
                        result.data.Add(sd);
                    }
                }
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw;
                    else
                        throw exceptionToThrow;
                }
            }
            return result;
        }

    }
}
