using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace R2MD
{
    public class SaveData
    {
        public string saveUploadFileData(string name, string ContentType, int size, byte[] fileData) {
            string result = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString)) {
                try
                {
                    SqlCommand cmd = new SqlCommand("[dbo].[SaveUploadFile]", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fileName", name);
                    cmd.Parameters.AddWithValue("@fileType", ContentType);
                    cmd.Parameters.AddWithValue("@fileData", fileData);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        result = dr["PatientAttID"].ToString();
                    }
                    dr.Close();
                }
                catch(Exception ex) {
                    Exception exceptionToThrow;
                    if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                        out exceptionToThrow))
                    {
                        if (exceptionToThrow == null)
                            throw ex;
                        else
                            throw exceptionToThrow;
                    }
                    else throw ex;
                }
                finally
                {
                    if (conn != null) {
                        conn.Close();
                    }
                }
            }
            return result;
        }

        public string savePatientInfo(string requestfname, string requestlname, string email, string requestorphone, string company,
            string Add1, string Add2, string City, string state, string country,string zip, string gender,string weight, string height,string birthdate,
            string lan, string oInfo, string ssn, string mrn, SqlCommand cmd,
            string badd1=null,string badd2 = null, string bcity = null, string bstate = null, string bzip = null) {
            
            string result = null;
            try
            {
                cmd.Parameters.Clear();
                SqlDataReader dr = null;
                cmd.CommandText = "[dbo].[SavePatientsInfo]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@requestfname", requestfname);
                cmd.Parameters.AddWithValue("@requestlname", requestlname);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@requestorphone", requestorphone);
                cmd.Parameters.AddWithValue("@company", company);
                cmd.Parameters.AddWithValue("@Add1", Add1);
                cmd.Parameters.AddWithValue("@Add2", Add2);
                cmd.Parameters.AddWithValue("@City", City);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@zip", zip);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@weight", weight);
                cmd.Parameters.AddWithValue("@height", height);
                cmd.Parameters.AddWithValue("@birthdate", birthdate);
                cmd.Parameters.AddWithValue("@language", lan);
                cmd.Parameters.AddWithValue("@oInfo", oInfo);
                cmd.Parameters.AddWithValue("@mrn", mrn);
                cmd.Parameters.AddWithValue("@ssn", ssn);
                cmd.Parameters.AddWithValue("@badd1", badd1);
                cmd.Parameters.AddWithValue("@badd2", badd2);
                cmd.Parameters.AddWithValue("@bcity", bcity);
                cmd.Parameters.AddWithValue("@bstate", bstate);
                cmd.Parameters.AddWithValue("@bzip", bzip);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result = dr["PatientID"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw ex;
                    else
                        throw exceptionToThrow;
                }
                else throw ex;
            }

            return result;
        }

        public DataTable SaveGuarantorInformation(string requestfname, string requestlname, string email, string requestorphone, string company,
            string riderfName,string riderlName, string riderPhone, string Add1, string Add2, string City,
            string state, string country,string zip, string gender, string weight, string height, string birthdate,string lan, 
            string oInfo,SqlCommand cmd, string relationship=null, string ssn=null, string mrn=null,
            string badd1 = null, string badd2 = null, string bcity = null, string bstate = null, string bzip = null, string PatientID = null)
            {
                DataTable result = new DataTable();
                SqlDataReader dr = null;
                try
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "[dbo].[SaveGuarantorInfo]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@requestfname", requestfname); 
                    cmd.Parameters.AddWithValue("@requestlname", requestlname);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@requestorphone", requestorphone);
                    cmd.Parameters.AddWithValue("@company", company);
                    cmd.Parameters.AddWithValue("@riderfName", riderfName);
                    cmd.Parameters.AddWithValue("@riderlName", riderlName);
                    cmd.Parameters.AddWithValue("@riderphone", riderPhone);
                    cmd.Parameters.AddWithValue("@Add1", Add1);
                    cmd.Parameters.AddWithValue("@Add2", Add2);
                    cmd.Parameters.AddWithValue("@City", City);
                    cmd.Parameters.AddWithValue("@state", state);
                    cmd.Parameters.AddWithValue("@country", country);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@weight", weight);
                    cmd.Parameters.AddWithValue("@height", height);
                    cmd.Parameters.AddWithValue("@birthdate", birthdate);
                    cmd.Parameters.AddWithValue("@Language", lan);
                    cmd.Parameters.AddWithValue("@Info", oInfo);
                    cmd.Parameters.AddWithValue("@ssn", ssn);
                    cmd.Parameters.AddWithValue("@mrn", mrn);
                    cmd.Parameters.AddWithValue("@zip", zip);
                    cmd.Parameters.AddWithValue("@badd1", badd1);
                    cmd.Parameters.AddWithValue("@badd2", badd2);
                    cmd.Parameters.AddWithValue("@bcity", bcity);
                    cmd.Parameters.AddWithValue("@bstate", bstate);
                    cmd.Parameters.AddWithValue("@bzip", bzip);
                    if(!string.IsNullOrEmpty(PatientID))cmd.Parameters.AddWithValue("@PatientID", Convert.ToInt32(PatientID));
                
                if (!string.IsNullOrEmpty(relationship))
                    {
                        cmd.Parameters.AddWithValue("@relationship", relationship);
                    }
                    else {
                        cmd.Parameters.AddWithValue("@relationship", "9");
                    }
                    dr = cmd.ExecuteReader();
                    result.Load(dr);
                    dr.Close();
                }
                catch (Exception ex)
                {
                    Exception exceptionToThrow;
                    if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                        out exceptionToThrow))
                    {
                        if (exceptionToThrow == null)
                            throw ex;
                        else
                            throw exceptionToThrow;
                    }
                    else throw ex;
                }
            return result;
        }

        //mode=0 servicetransport, mode=1 serviceinterpret, 
        public void saveService(string PatientID, string RequestType, string RequestorID, string Lan, string instruction, DateTime ScheduleTime,
            string PassengerNumber, string InterpretLan, string EquipmentType, string DiagnosticSe, string Payor, string PNumber,
            string pid, string did, string homeSType, string TransportType, string iatPickup,string iatDropoff,SqlCommand cmd, string insID=null,string rt=null, string sid=null
            ,string mileage=null, string driver=null, string compaions=null, string provider=null, string effectiveDate=null) {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[SaveService]";
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("@PatientID", Convert.ToInt32(PatientID));
                cmd.Parameters.AddWithValue("@RequestType", RequestType);
                if (!string.IsNullOrEmpty(RequestorID)) cmd.Parameters.AddWithValue("@RequestorID", Convert.ToInt32(RequestorID));
                cmd.Parameters.AddWithValue("@Lan", Lan);
                cmd.Parameters.AddWithValue("@instruction", instruction);
                if (ScheduleTime!=DateTime.MinValue) cmd.Parameters.AddWithValue("@ScheduleTime", ScheduleTime);
                if(!string.IsNullOrEmpty(PassengerNumber)) cmd.Parameters.AddWithValue("@PassengerNumber", Convert.ToInt32(PassengerNumber));
                cmd.Parameters.AddWithValue("@InterpretLan", InterpretLan);
                cmd.Parameters.AddWithValue("@EquipmentType", EquipmentType);
                cmd.Parameters.AddWithValue("@DiagnosticSe", DiagnosticSe);
                
                cmd.Parameters.AddWithValue("@PNumber", PNumber);
                if (!string.IsNullOrEmpty(pid)) cmd.Parameters.AddWithValue("@pickID", Convert.ToInt32(pid));
                if (!string.IsNullOrEmpty(did)) cmd.Parameters.AddWithValue("@dropID", Convert.ToInt32(did));
                cmd.Parameters.AddWithValue("@HomeServiceType", homeSType);
                cmd.Parameters.AddWithValue("@TransportType", TransportType);
                if (!string.IsNullOrEmpty(iatPickup)) cmd.Parameters.AddWithValue("@InterpretAtPickup", Convert.ToBoolean(iatPickup));
                if (!string.IsNullOrEmpty(iatDropoff)) cmd.Parameters.AddWithValue("@InterpretAtDropoff", Convert.ToBoolean(iatDropoff));
                if (!string.IsNullOrEmpty(insID)) cmd.Parameters.AddWithValue("@InsID", Convert.ToInt32(insID));
                else { cmd.Parameters.AddWithValue("@Payor", Payor); }
                if (!string.IsNullOrEmpty(rt)) cmd.Parameters.AddWithValue("@rt", Convert.ToBoolean(rt));
                if (!string.IsNullOrEmpty(sid)) cmd.Parameters.AddWithValue("@status", Convert.ToInt32(sid));
                //if(!string.IsNullOrEmpty(mileage)) cmd.Parameters.AddWithValue("@mileage", Convert.ToInt32(mileage));
                cmd.Parameters.AddWithValue("@mileage", mileage);
                cmd.Parameters.AddWithValue("@driver", driver);
                if (!string.IsNullOrEmpty(compaions)) cmd.Parameters.AddWithValue("@compaions", Convert.ToInt32(compaions));
                if (!string.IsNullOrEmpty(effectiveDate)) cmd.Parameters.AddWithValue("@effectiveDate", Convert.ToDateTime(effectiveDate));                
                cmd.Parameters.AddWithValue("@provider", provider);
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw ex;
                    else
                        throw exceptionToThrow;
                }
                else throw ex;
            }


        }

        public DataTable GetPDAddressID(string pickupName, string pAdd1, string pAdd2, string pCity, string pState, string pZip, string pCountry, string pPhone,
            string dropName, string dAdd1, string dAdd2, string dCity, string dState, string dZip, string dCountry, string dPhone, SqlCommand cmd, string mode=null) {
            DataTable result = new DataTable();
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[saveGetPDAddress]";
                cmd.CommandType = CommandType.StoredProcedure;
                if(!string.IsNullOrEmpty(mode)) cmd.Parameters.AddWithValue("@mode", Convert.ToInt32(mode));
                cmd.Parameters.AddWithValue("@pickupName", pickupName);
                cmd.Parameters.AddWithValue("@pAdd1", pAdd1);
                cmd.Parameters.AddWithValue("@pAdd2", pAdd2);
                cmd.Parameters.AddWithValue("@pCity", pCity);
                cmd.Parameters.AddWithValue("@pstate", pState);
                cmd.Parameters.AddWithValue("@pZip", pZip);
                cmd.Parameters.AddWithValue("@pcountry", pCountry);
                cmd.Parameters.AddWithValue("@pPhone", pPhone);
                cmd.Parameters.AddWithValue("@dropName", dropName);
                cmd.Parameters.AddWithValue("@dAdd1", dAdd1);
                cmd.Parameters.AddWithValue("@dAdd2", dAdd2);
                cmd.Parameters.AddWithValue("@dCity", dCity);
                cmd.Parameters.AddWithValue("@dstate", dState);
                cmd.Parameters.AddWithValue("@dZip", dZip);
                cmd.Parameters.AddWithValue("@dcountry", dCountry);
                cmd.Parameters.AddWithValue("@dPhone", dPhone);
                SqlDataReader dr = cmd.ExecuteReader();
                result.Load(dr);
                dr.Close();
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw ex;
                    else
                        throw exceptionToThrow;
                }
                else throw ex;
            }
            return result;
        }

        public void updateUFData(string patientID, string id,SqlCommand cmd) {
            try {
                cmd.CommandText = "update [PI].[PatientAttachment] Set [PatientId]=@patientID Where [PatientAttachmentId]=@id";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@patientID", patientID);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw ex;
                    else
                        throw exceptionToThrow;
                }
                else throw ex;
            }
        }
        public void deleteAttachment(string patientID, SqlCommand cmd)
        {
            try
            {
                cmd.CommandText = "update [PI].[PatientAttachment] Set [PatientId]=null Where [PatientId]=@patientID";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@patientID", patientID);                
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw ex;
                    else
                        throw exceptionToThrow;
                }
                else throw ex;
            }
        }        
        public void saveDetailData(int status, int serviceID, SqlCommand cmd, string provider = null) {
            try
            {
                cmd.CommandText = "saveDetailData";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                //cmd.Parameters.AddWithValue("@mode", mode);
                if(!string.IsNullOrEmpty(provider)) cmd.Parameters.AddWithValue("@providerName", provider);
                cmd.Parameters.AddWithValue("@statusId", status);
                cmd.Parameters.AddWithValue("@serviceRID", serviceID);
                //if (!string.IsNullOrEmpty(providerID)) cmd.Parameters.AddWithValue("@providerID", providerID);
                cmd.ExecuteNonQuery();  
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw ex;
                    else
                        throw exceptionToThrow;
                }
                else throw ex;
            }
        }
        public void UpdatePatientInfo(string requestfname, string requestlname, string requestgender, string requestweight, string requestheight,
            string dob,string requestssn, string requestmrn, string requestpatientid, string requestpatientinfo, SqlCommand cmd,
            string add1=null, string add2=null, string city=null, string state=null, string zip=null,
            string badd1 = null, string badd2 = null, string bcity = null, string bstate = null, string bzip = null)
        {
            try {
                cmd.CommandText = "[dbo].[updatePatientsInfo]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@patientID", requestpatientid);
                cmd.Parameters.AddWithValue("@requestfname", requestfname);
                cmd.Parameters.AddWithValue("@requestlname", requestlname);
                cmd.Parameters.AddWithValue("@dob", dob);
                cmd.Parameters.AddWithValue("@requestgender", requestgender);
                cmd.Parameters.AddWithValue("@requestweight", requestweight);
                cmd.Parameters.AddWithValue("@requestheight", requestheight);
                cmd.Parameters.AddWithValue("@requestssn", requestssn);
                cmd.Parameters.AddWithValue("@requestmrn", requestmrn);
                cmd.Parameters.AddWithValue("@add1", add1);
                cmd.Parameters.AddWithValue("@add2", add2);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@zip", zip);
                cmd.Parameters.AddWithValue("@badd1", badd1);
                cmd.Parameters.AddWithValue("@badd2", badd2);
                cmd.Parameters.AddWithValue("@bcity", bcity);
                cmd.Parameters.AddWithValue("@bstate", bstate);
                cmd.Parameters.AddWithValue("@bzip", bzip);
                cmd.Parameters.AddWithValue("@requestpatientinfo", requestpatientinfo);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw ex;
                    else
                        throw exceptionToThrow;
                }
                else throw ex;
            }
        }
        public void UpdateRequestorInfo(int mode, string requstorfname, string requstorlname, string requestpatientid, 
            int serviceID, string requestcompany, string relationship, string requestorphone, SqlCommand cmd, string PatientID) {
            try
            {
                cmd.CommandText = "[dbo].[updateRequestorInfo]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@mode", mode);
                cmd.Parameters.AddWithValue("@requstorfname", requstorfname);
                cmd.Parameters.AddWithValue("@requstorlname", requstorlname);
                if (serviceID != -1) cmd.Parameters.AddWithValue("@serviceID", serviceID);
                if (requestpatientid != "-1") cmd.Parameters.AddWithValue("@GuarantorId", Convert.ToInt32(requestpatientid));
                if (!string.IsNullOrEmpty(PatientID)) cmd.Parameters.AddWithValue("@patientID", Convert.ToInt32(PatientID));
                cmd.Parameters.AddWithValue("@requestcompany", requestcompany);
                cmd.Parameters.AddWithValue("@relationship", relationship);
                cmd.Parameters.AddWithValue("@requestorphone", requestorphone);               
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw ex;
                    else
                        throw exceptionToThrow;
                }
                else throw ex;
            }
        }
        public void saveDetailServiceData(int mode, string patientID, int status, int serviceID, 
            SqlCommand cmd, string name, string info, string date, string payer, string insID, string pid, 
            string effectiveDate = null,string patientInsId=null) {
            try{
                cmd.CommandText = "[dbo].[updateServiceOther]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@mode", mode);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@info", info);
                cmd.Parameters.AddWithValue("@serviceID", serviceID);
                cmd.Parameters.AddWithValue("@status", status);                                
                cmd.Parameters.AddWithValue("@payerid", pid);
                cmd.Parameters.AddWithValue("@patientInsId", patientInsId);
                if (!string.IsNullOrEmpty(patientID)) cmd.Parameters.AddWithValue("@patientID", Convert.ToInt32(patientID));
                if (!string.IsNullOrEmpty(insID))
                {
                    cmd.Parameters.AddWithValue("@InsID", Convert.ToInt32(insID));
                }
                else {
                    cmd.Parameters.AddWithValue("@payer", payer);
                }    
                if (!string.IsNullOrEmpty(date)) cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(date));
                if (!string.IsNullOrEmpty(effectiveDate)) cmd.Parameters.AddWithValue("@effectiveDate", Convert.ToDateTime(effectiveDate));
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw ex;
                    else
                        throw exceptionToThrow;
                }
                else throw ex;
            }
        }

        public void updateInterpreting(int mode, string PatientID, string Payer, string PayID, int status, 
            string requestdate, string payorInsuranceID, int did, int pid, Boolean dpit, Boolean dbit, int sID, 
            string langurage, SqlCommand cmd, string effectiveDate=null, string patientInsId = null) {
            try { 
                cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[updateInterService]";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", mode);
                if (!string.IsNullOrEmpty(PatientID)) cmd.Parameters.AddWithValue("@patientID", Convert.ToInt32(PatientID));
                
                cmd.Parameters.AddWithValue("@payerid", PayID);
                if (status != -1) cmd.Parameters.AddWithValue("@status", status);
                if (!string.IsNullOrEmpty(requestdate)) cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(requestdate));
                if (!string.IsNullOrEmpty(payorInsuranceID))
                { cmd.Parameters.AddWithValue("@InsID", Convert.ToInt32(payorInsuranceID)); }
                else { cmd.Parameters.AddWithValue("@payer", Payer); }
                if (did != -1)  cmd.Parameters.AddWithValue("@did", did);
                if (pid != -1)  cmd.Parameters.AddWithValue("@pid", pid);
                cmd.Parameters.AddWithValue("@dpit", dpit);
                cmd.Parameters.AddWithValue("@dbit", dbit);
                cmd.Parameters.AddWithValue("@patientInsId", patientInsId);
                if (sID != -1) cmd.Parameters.AddWithValue("@serviceID", sID);
                if (!string.IsNullOrEmpty(effectiveDate)) cmd.Parameters.AddWithValue("@effectiveDate", Convert.ToDateTime(effectiveDate));
                cmd.Parameters.AddWithValue("@lan", langurage);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw ex;
                    else
                        throw exceptionToThrow;
                }
                else throw ex;
            }
        }

        public void updateAddress(int pickupID, int dropoffID,string pickupName, string pAdd1, string pAdd2, string pCity, string pState, string pZip, string pCountry, string pPhone,
            string dropName, string dAdd1, string dAdd2, string dCity, string dState, string dZip, string dCountry, string dPhone, SqlCommand cmd)
        {
            try { 
            cmd.Parameters.Clear();
            cmd.CommandText = "[dbo].[updatePDAddress]";
            cmd.CommandType = CommandType.StoredProcedure;
            if (pickupID != -1) cmd.Parameters.AddWithValue("@pickupID", pickupID);
            if (dropoffID != -1) cmd.Parameters.AddWithValue("@dropoffID", dropoffID);
            cmd.Parameters.AddWithValue("@pickupName", pickupName);
            cmd.Parameters.AddWithValue("@pAdd1", pAdd1);
            cmd.Parameters.AddWithValue("@pAdd2", pAdd2);
            cmd.Parameters.AddWithValue("@pCity", pCity);
            cmd.Parameters.AddWithValue("@pstate", pState);
            cmd.Parameters.AddWithValue("@pZip", pZip);
            cmd.Parameters.AddWithValue("@pcountry", pCountry);
            cmd.Parameters.AddWithValue("@pPhone", pPhone);
            cmd.Parameters.AddWithValue("@dropName", dropName);
            cmd.Parameters.AddWithValue("@dAdd1", dAdd1);
            cmd.Parameters.AddWithValue("@dAdd2", dAdd2);
            cmd.Parameters.AddWithValue("@dCity", dCity);
            cmd.Parameters.AddWithValue("@dstate", dState);
            cmd.Parameters.AddWithValue("@dZip", dZip);
            cmd.Parameters.AddWithValue("@dcountry", dCountry);
            cmd.Parameters.AddWithValue("@dPhone", dPhone);
            cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw ex;
                    else
                        throw exceptionToThrow;
                }
                else throw ex;
            }
        }

        public void updateServiceTrans(string patientID,int serviceID, string requestmileage, string requestdriverassigned, string providerassigned, DateTime date, string requesttranscompaions,
                    string requesttransround, string requestlevel, string requesttransadditional, int status, 
                    string Payer,string InsID, string PayID, string createDate, SqlCommand cmd, string effectiveDate = null,
                    string patientInsId=null, string pid=null, string did=null)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = "[dbo].[updateServiceTrans]";
                cmd.CommandType = CommandType.StoredProcedure;
                //if(!string.IsNullOrEmpty(requestmileage)) cmd.Parameters.AddWithValue("@requestmileage", Convert.ToInt32(requestmileage));
                cmd.Parameters.AddWithValue("@requestmileage", requestmileage);
                cmd.Parameters.AddWithValue("@requestdriverassigned", requestdriverassigned);
                cmd.Parameters.AddWithValue("@providerassigned", providerassigned);
                cmd.Parameters.AddWithValue("@serviceID", serviceID);
                cmd.Parameters.AddWithValue("@patientInsId", patientInsId);
                if (date != DateTime.MinValue) cmd.Parameters.AddWithValue("@date", date);
                if (!string.IsNullOrEmpty(requesttranscompaions)) cmd.Parameters.AddWithValue("@requesttranscompaions", Convert.ToInt32(requesttranscompaions));
                if (!string.IsNullOrEmpty(requesttransround))
                {
                    if (requesttransround == "Y")
                    {
                        cmd.Parameters.AddWithValue("@requesttransround", true);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@requesttransround", false);
                    }
                }
                //cmd.Parameters.AddWithValue("@requesttransround", requesttransround);
                if (!string.IsNullOrEmpty(requestlevel)) cmd.Parameters.AddWithValue("@requestlevel", Convert.ToInt32(requestlevel));
                cmd.Parameters.AddWithValue("@requesttransadditional", requesttransadditional);
                cmd.Parameters.AddWithValue("@status", status);
                
                cmd.Parameters.AddWithValue("@PayID", PayID);
                if (!string.IsNullOrEmpty(createDate)) cmd.Parameters.AddWithValue("@createDate", Convert.ToDateTime(createDate));
                if (!string.IsNullOrEmpty(InsID)) { cmd.Parameters.AddWithValue("@InsID", Convert.ToInt32(InsID)); }
                else { cmd.Parameters.AddWithValue("@Payer", Payer); }
                if (!string.IsNullOrEmpty(patientID)) cmd.Parameters.AddWithValue("@patientID", Convert.ToInt32(patientID));
                if (!string.IsNullOrEmpty(effectiveDate)) cmd.Parameters.AddWithValue("@effectiveDate", Convert.ToDateTime(effectiveDate));
                if (!string.IsNullOrEmpty(pid)) cmd.Parameters.AddWithValue("@pickupId", Convert.ToInt32(pid));
                if (!string.IsNullOrEmpty(did)) cmd.Parameters.AddWithValue("@dropoffId", Convert.ToInt32(did));
                
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw ex;
                    else
                        throw exceptionToThrow;
                }
                else throw ex;
            }
        }

        public DataTable CheckPayorName(string id, SqlCommand cmd) {
            SqlDataReader dr = null;
            DataTable result = new DataTable();
            try {                 
                cmd.Parameters.Clear();
                cmd.CommandText = "SELECT [PlanCode],[PayorName],[AddressLine1],[AddressLine2],[City],[State],[ZipCode] FROM [dbo].[InsurancePlan] Where [InsurancePlanId]=@id";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                dr = cmd.ExecuteReader();
                result.Load(dr);
                dr.Close();               
            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndThrewException",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw ex;
                    else
                        throw exceptionToThrow;
                }
                else throw ex;
            }
            return result;
        }

    }
}