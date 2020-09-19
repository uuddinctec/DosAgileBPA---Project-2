using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Newtonsoft.Json;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;




namespace R2MD
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    // To allow this Web Service to be called from script, using ASP+NET AJAX, uncomment the following line+ 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public string FromEmail = "";
        public string staffEmail = "";
        [WebMethod]
        public void GetEmailInfo(Information obj)
        {

            try
            {
                FromEmail = ConfigurationManager.AppSettings["FromEmail"];
                staffEmail = ConfigurationManager.AppSettings["staffEmail"];
                SaveData sd = new SaveData();            
                var email_content = " ";
                var language = ""; 
                var PatientID = "";
                var GID = "";
                var pickupID = "";
                var dropoffID = "";
                var interLan = "";
                var ipickup = "";
                var idropoff = "";
                string email_content_attachment = "";
                DataTable dt = new DataTable();

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString)) {
                    IsolationLevel level = IsolationLevel.ReadUncommitted;
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction(level, "ReadDirtyMessageAmount"))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Transaction = tran;
                        cmd.Connection = conn;
                                    
                        //Page1
                        email_content = "<table style=\"background-color: #262f67; width: 100%; color: #6d6e71;\"><tr><td>&nbsp;</td></tr><tr><td style=\"width: 10%;\">&nbsp;</td><td style=\"width: 80%; background: #ffffff; padding: 20px;\">";
                        email_content += "<p style=\"font-size: 16px; color: #262f67; text-transform: uppercase; margin-bottom: 0;\">Requestor Info</p><p><b>Name:</b> " + obj.requestfname + " " + obj.requestlname + "</p>";
                        email_content += "<p><b>Email:</b> " + obj.email + "</p>";
                        email_content += "<p><b>Phone:</b> " + obj.requestorphone + "</p>";
                        email_content += "<p><b>Company:</b> " + obj.company + "</p> <hr style=\"border-top: 1px solid #e2e2e3;\">";

                        if (obj.languageenglish == "on") language +="English,";         
                        else if (obj.languagespanish == "on") language += "Spanish,";        
                        else language += obj.languageother2;

                        if (obj.pickupperson == "myself")
                        {
                            var height = obj.heightft + " ft " + obj.heightin + " in";
                            email_content += "<p style=\"font-size: 16px; color: #262f67; text-transform: uppercase; margin-bottom: 0;\">Rider Info</p><table style=\"text-align: left; width: 100%;\"><tr><td style=\"width: 50%;\"><p><b>Requesting pickup for:</b> Myself</p>";
                            //save myself patientInfo
                            PatientID=sd.savePatientInfo(obj.riderfname, obj.riderlname, obj.email, obj.riderphone, obj.company, obj.rideradd1,obj.rideradd2,obj.ridercity,obj.riderstate,obj.ridercountry,obj.riderzip,obj.gender,obj.weightlbs, height,obj.birthdate, language, obj.extrainfo,null,null,cmd);

                        }
                        else
                        {
                            var height = obj.heightft + " ft " + obj.heightin + " in";
                            email_content += "<table style=\"text-align: left; width: 100%;\"><tr><td style=\"width: 50%; font-weight: normal;\"><p style=\"font-size: 16px; color: #262f67; text-transform: uppercase;\">Rider Info</p><p><b>Requesting pickup for:</b> " + obj.riderfname + " " + obj.riderlname + "</p>";
                            //save guarantor Info
                            dt=sd.SaveGuarantorInformation(obj.requestfname, obj.requestlname, obj.email, obj.requestorphone, obj.company, obj.riderfname,obj.riderlname,obj.riderphone,obj.rideradd1,obj.rideradd2,obj.ridercity,obj.riderstate,obj.ridercountry,obj.riderzip,obj.gender,obj.weightlbs, height,obj.birthdate, language, obj.extrainfo, cmd);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                PatientID = dr["PatientID"].ToString();
                                GID = dr["GuarantorId"].ToString();
                            }

                        }
                    
                    
                    
                    
                        //Page2
                        email_content += "<p><b>Pickup Address:</b> " + obj.rideradd1 + ", " + obj.rideradd2 + ", " + obj.ridercity + ", " + obj.riderstate + ", " + obj.riderzip + ", " + obj.ridercountry + "</p>";
                        email_content += "<p><b>Phone:</b> " + obj.riderphone + "</p>";
                        email_content += "<p><b>Gender:</b> " + obj.gender + "</p>";
                        email_content += "<p><b>Height:</b> " + obj.heightft + " ft " + obj.heightin + " in</p>";
                        email_content += "<p><b>Weight:</b> " + obj.weightlbs + " lbs</p>";
                        email_content += "<p><b>Date of Birth:</b> " + obj.birthdate + "</p></td>";


                        if (obj.languageenglish == "on")
                        {
                            email_content += "<td style=\"width: 50%;\"><p><b>Language:</b> English </p>";
                        }
                        else if (obj.languagespanish == "on")
                        {
                            email_content += "<td style=\"width: 50%;\"><p><b>Language:</b> Spanish </p>";
                        }
                        else
                        {
                            email_content += "<td style=\"width: 50%;\"><p><b>Language:</b> " + obj.languageother2 + "</p>";
                        }


                        email_content += "<p><b>Pertinent Information:</b> " + obj.extrainfo + "</p>";
                        email_content += "<p><b>Payor Type:</b> " + obj.insurancetype + "</p>";
                        email_content += "<p><b>ID Number:</b> " + obj.insurancenumber + "</p></td></tr></table>";


                    


                        //service Page
                        if (obj.servicetransport == "on")
                        {
                            //save pick up and drop off address data
                            dt=sd.GetPDAddressID(obj.pickupname, obj.pickupadd1, obj.pickupadd2, obj.pickupcity, obj.pickupstate, obj.pickupzip, null, obj.pickupphone,
                                obj.dropoffname, obj.dropoffadd1, obj.dropoffadd2, obj.dropoffcity, obj.dropoffstate, obj.dropoffzip, null, obj.dropoffphone, cmd);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                pickupID = dr["pickupID"].ToString();
                                dropoffID = dr["dropoffID"].ToString();
                            }

                            email_content += "<hr style=\"border-top: 1px solid #e2e2e3;\"><p style=\"font-size: 16px; color: #262f67; text-transform: uppercase;\">TRANSPORTATION Service</p>";
                            email_content += "<table style=\"width: 100%; line-height: 24px; text-align: center;\"><tr style=\"background-color: #f6f6f6\"><td><b>Date</b></td><td><b>Time</b></td><td><b>Passengers</b></td><td><b>RT</b></td><td><b>Pickup</b></td></tr>";


                            if (obj.serviceinterpret == "on")
                            {
                            
                                if (obj.interpretspanish == "on")
                                {
                                    interLan += "Spanish, ";
                                }
                                if (obj.interpretcreole == "on")
                                {
                                    interLan += "Creole, ";
                                }
                                if (obj.interpretother == "on")
                                {
                                    interLan += obj.otherlanguage;
                                }
                            
                                if (obj.interpretpickup == "on")
                                {
                                    ipickup = "true";
                                }

                                if (obj.interpretdropoff == "on")
                                {
                                    idropoff = "true";
                                }

                            }


                            obj.piecesappointmenttable = obj.appointmenttabledata.Split(',');
                            obj.piecesappointmenttablecount = obj.piecesappointmenttable.Length / 6;                                                                  
                            for (var i = 0; i < Convert.ToInt32(obj.piecesappointmenttablecount); i++)
                            {
                                email_content += "<tr>";
                                var startnumber = 6 * i;
                                email_content += "<td>" + obj.piecesappointmenttable[startnumber] + "</td>";
                                email_content += "<td>" + obj.piecesappointmenttable[startnumber + 1] + "</td>";
                                email_content += "<td>" + obj.piecesappointmenttable[startnumber + 2] + "</td>";
                                email_content += "<td>" + obj.piecesappointmenttable[startnumber + 3] + "</td>";
                                email_content += "<td>" + obj.piecesappointmenttable[startnumber + 4] + "</td>";
                                email_content += "</tr>";

                                if (obj.piecesappointmenttable[startnumber].Split(' ').Length > 0) { }

                            
                                //save service
                                var rt = "false";
                                if (obj.piecesappointmenttable[startnumber + 3] == "Yes")
                                {
                                    rt = "true";
                                }

                                for (var n = 0; n < obj.piecesappointmenttable[startnumber].Split(' ').Length; n++) {
                                    if (!string.IsNullOrEmpty(PatientID) && !string.IsNullOrEmpty(pickupID) && !string.IsNullOrEmpty(dropoffID))
                                    {
                                        if (obj.serviceinterpret == "on")
                                        {
                                            DateTime pickupDateTime = Convert.ToDateTime(obj.piecesappointmenttable[startnumber].Split(' ')[n] + " " + obj.piecesappointmenttable[startnumber + 1]);
                                            //pickupService
                                            sd.saveService(PatientID, "1", GID, null, obj.transportinstructions, pickupDateTime, obj.piecesappointmenttable[startnumber + 2], interLan, null, null, obj.insurancetype, obj.insurancenumber, pickupID, dropoffID, null, obj.transporttype, ipickup, idropoff, cmd, null, rt);

                                            if (obj.piecesappointmenttable[startnumber + 3] == "Yes")
                                            { //round trip
                                                if (string.IsNullOrEmpty(obj.piecesappointmenttable[startnumber + 4]) || obj.piecesappointmenttable[startnumber + 4] == "AM" || obj.piecesappointmenttable[startnumber + 4] == "PM") { 
                                                    obj.piecesappointmenttable[startnumber + 4] = "23:59";
                                                }
                                                DateTime DropoffDateTime = Convert.ToDateTime(obj.piecesappointmenttable[startnumber].Split(' ')[n] + " " + obj.piecesappointmenttable[startnumber + 4]);
                                                //DropoffService

                                                dt = sd.GetPDAddressID(obj.dropoffname, obj.dropoffadd1, obj.dropoffadd2, obj.dropoffcity, obj.dropoffstate, obj.dropoffzip, null, obj.dropoffphone, 
                                                            obj.pickupname, obj.pickupadd1, obj.pickupadd2, obj.pickupcity, obj.pickupstate, obj.pickupzip, null, obj.pickupphone, cmd);
                                                if (dt != null && dt.Rows.Count > 0)
                                                {
                                                    DataRow dr = dt.Rows[0];
                                                    pickupID = dr["pickupID"].ToString();
                                                    dropoffID = dr["dropoffID"].ToString();
                                                }

                                                sd.saveService(PatientID, "1", GID, null, obj.transportinstructions, DropoffDateTime, obj.piecesappointmenttable[startnumber + 2], interLan, null, null, obj.insurancetype, obj.insurancenumber, pickupID, dropoffID, null, obj.transporttype, ipickup, idropoff, cmd, null, rt);
                                            }
                                        }
                                        else
                                        {
                                            DateTime pickupDateTime = Convert.ToDateTime(obj.piecesappointmenttable[startnumber].Split(' ')[n] + " " + obj.piecesappointmenttable[startnumber + 1]);
                                            //pickupService
                                            sd.saveService(PatientID, "1", GID, null, obj.transportinstructions, pickupDateTime, obj.piecesappointmenttable[startnumber + 2], null, null, null, obj.insurancetype, obj.insurancenumber, pickupID, dropoffID, null, obj.transporttype, null, null, cmd, null, rt);

                                            if (obj.piecesappointmenttable[startnumber + 3] == "Yes")
                                            { //round trip
                                                if (string.IsNullOrEmpty(obj.piecesappointmenttable[startnumber + 4]) || obj.piecesappointmenttable[startnumber + 4]=="AM" || obj.piecesappointmenttable[startnumber + 4]=="PM")
                                                {
                                                    obj.piecesappointmenttable[startnumber + 4] = "23:59";
                                                }
                                                DateTime DropoffDateTime = Convert.ToDateTime(obj.piecesappointmenttable[startnumber].Split(' ')[n] + " " + obj.piecesappointmenttable[startnumber + 4]);
                                                //DropoffService
                                                dt = sd.GetPDAddressID(obj.dropoffname, obj.dropoffadd1, obj.dropoffadd2, obj.dropoffcity, obj.dropoffstate, obj.dropoffzip, null, obj.dropoffphone,
                                                            obj.pickupname, obj.pickupadd1, obj.pickupadd2, obj.pickupcity, obj.pickupstate, obj.pickupzip, null, obj.pickupphone, cmd);
                                                if (dt != null && dt.Rows.Count > 0)
                                                {
                                                    DataRow dr = dt.Rows[0];
                                                    pickupID = dr["pickupID"].ToString();
                                                    dropoffID = dr["dropoffID"].ToString();
                                                }

                                                sd.saveService(PatientID, "1", GID, null, obj.transportinstructions, DropoffDateTime, obj.piecesappointmenttable[startnumber + 2], null, null, null, obj.insurancetype, obj.insurancenumber, pickupID,dropoffID,  null, obj.transporttype, null, null, cmd, null, rt);
                                            }
                                        }

                                    }
                                }                         
                            
                            
                            }                                                   
                            email_content += "</table><br>";
                            email_content += "<table style=\"text-align: left; width: 100%;\"><tr><td style=\"width: 50%;\"><p><b>Transport Type:</b> " + obj.transporttype + "</p>";
                            email_content += "<p><b>Pick Up Location Name:</b> " + obj.pickupname + "</p>";
                            email_content += "<p><b>Pick Up Address:</b> " + obj.pickupadd1 + ", " + obj.pickupadd2 + ", " + obj.pickupcity + ", " + obj.pickupstate + ", " + obj.pickupzip + ", " + obj.pickupcountry + "</p>";
                            email_content += "<p><b>Pick Up Phone:</b> " + obj.pickupphone + "</p></td>";
                            email_content += "<td style=\"width: 50%;\"><p><b>Drop Off Location Name:</b> " + obj.dropoffname + "</p>";
                            email_content += "<p><b>Drop Off Address:</b> " + obj.dropoffadd1 + ", " + obj.dropoffadd2 + ", " + obj.dropoffcity + ", " + obj.dropoffstate + ", " + obj.dropoffzip + ", " + obj.dropoffcountry + "</p>";
                            email_content += "<p><b>Drop Off Phone:</b> " + obj.dropoffphone + "</p>";
                            email_content += "<p><b>Additional Instructions:</b> " + obj.transportinstructions + "</p></td></tr></table>";



                        }
                        if (obj.serviceinterpret == "on")
                        {
                            email_content += "<hr style=\"border-top: 1px solid #e2e2e3;\"><p style=\"font-size: 16px; color: #262f67; text-transform: uppercase;\">ON SITE INTERPRETING Service</p>";
                            email_content += "<p><b>Language:</b> ";
                            if (obj.interpretspanish == "on")
                            {
                                email_content += "Spanish ";
                            }
                            if (obj.interpretcreole == "on")
                            {
                                email_content += "Creole ";
                            }
                            if (obj.interpretother == "on")
                            {
                                email_content += obj.otherlanguage;
                            }
                            email_content += "</p>";
                            if (obj.interpretpickup == "on")
                            {
                                email_content += "<p><b>Interpreting is needed at Pick Up Location</p>";
                            }

                            if (obj.interpretdropoff == "on")
                            {
                                email_content += "<p><b>Interpreting is needed at Drop Off Location</p>";
                            }

                        }
                        if (obj.serviceequipment == "on")
                        {
                            email_content += "<hr style=\"border-top: 1px solid #e2e2e3;\"><p style=\"font-size: 16px; color: #262f67; text-transform: uppercase;\">MEDICAL EQUIPMENT Service</p>";
                            email_content += "<p><b>Equipment Type:</b> " + obj.equiptype + "</p>";
                            email_content += "<p><b>Additional Information:</b> " + obj.equipinfo + "</p>";

                            if (!string.IsNullOrEmpty(PatientID)){
                                sd.saveService(PatientID, "3", GID, null, obj.equipinfo, DateTime.MinValue, null, null, obj.equiptype, null, obj.insurancetype, obj.insurancenumber, null, null,null,null, null, null, cmd);
                            }

                        }
                        if (obj.servicehome == "on")
                        {
                            email_content += "<hr style=\"border-top: 1px solid #e2e2e3;\"><p style=\"font-size: 16px; color: #262f67; text-transform: uppercase;\">HOME HEALTH / HOME INFUSION</p><ul style=\"padding-left: 0\">";
                            var serviceType = "";
                            if (obj.homenurse == "on")
                            {
                                email_content += "<li>Certified Nurse</li>";
                                serviceType += "Certified Nurse,";
                            }
                            if (obj.homeaid == "on")
                            {
                                email_content += "<li>Aid: YES</li>";
                                serviceType += "Home Health Aide,";
                            }
                            if (obj.homernlpn == "on")
                            {
                                email_content += "<li>RN/LPN: YES</li>";
                                serviceType += "RN/LPN,";
                            }
                            if (obj.homecompanion == "on")
                            {
                                email_content += "<li>Companion Services: YES</li>";
                                serviceType += "Companion Services";
                            }

                            email_content += "</ul><p><b>Additional Information:</b> " + obj.homeinfo + "</p>";

                            if (!string.IsNullOrEmpty(PatientID))
                            {
                                sd.saveService(PatientID, "4", GID, null, obj.homeinfo, DateTime.MinValue, null, null, null, null, obj.insurancetype, obj.insurancenumber,null,null,serviceType,null, null, null, cmd);
                            }
                        }
                        if (obj.servicediagnostic == "on")
                        {
                            email_content += "<hr style=\"border-top: 1px solid #e2e2e3;\"><p style=\"font-size: 16px; color: #262f67; text-transform: uppercase;\">DIAGNOSTIC Service</p>";

                            email_content += "<p><b>Diagnostic Service Type:</b> " + obj.diagnostictype + "</p>";
                            email_content += "<p><b>Diagnostic Service Information:</b> " + obj.diagnosticinfo + "</p>";
                            if (!string.IsNullOrEmpty(PatientID))
                            {
                                sd.saveService(PatientID, "5", GID, null, obj.diagnosticinfo, DateTime.MinValue, null, null, null, obj.diagnostictype, obj.insurancetype, obj.insurancenumber, null, null, null, null, null, null, cmd);
                            }

                        }
                        var uF = obj.UploadFile;
                        email_content_attachment = email_content;
                        string currentpath = "";
                        //currentpath = Context.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped);
                        //currentpath = Context.Request.Url.GetLeftPart(UriPartial.Authority);
                        currentpath = Context.Request.Url.Scheme + "://" + Context.Request.Url.Authority;
                        if (!string.IsNullOrEmpty(PatientID) && !string.IsNullOrEmpty(uF))
                        {
                            email_content_attachment += "<hr style=\"border-top: 1px solid #e2e2e3;\"><p style=\"font-size: 16px; color: #262f67; text-transform: uppercase;\"Attachments</p>";

                            email_content_attachment += "<p><b>Attached Files:</b> ";
                            if (uF.Contains(','))
                            {
                                string[] ufA = uF.Split(',');
                                foreach (var i in ufA)
                                {
                                    sd.updateUFData(PatientID, i, cmd);
                                    email_content_attachment += "<a href=\""+currentpath + "/ReadAttachment.aspx?ID=" + i + "\">File "+i+"</a>,";
                                }
                                email_content_attachment = email_content_attachment.TrimEnd(',');
                            }
                            else
                            {
                                sd.updateUFData(PatientID, uF, cmd);
                                email_content_attachment = "<a href=\"" + currentpath + "/ReadAttachment.aspx?ID=" + uF + "\">File " + uF + "</a>";
                            }
                            email_content_attachment += "</p>";
                        }
                        email_content += "</td><td style=\"width: 10%;\">&nbsp;</td></tr><tr><td>&nbsp;</td></tr></table>";
                        email_content_attachment += "</td><td style=\"width: 10%;\">&nbsp;</td></tr><tr><td>&nbsp;</td></tr></table>";

                        //update upload file, add patientID

                        tran.Commit();
                    }
                    if (conn != null) conn.Close();
                }
                //using lambda expression
                Diagnostics.ExceptionHandler.ExManager.Process(()=>sendPatientEmail(email_content, obj.email), "LogAndThrewException");
                sendStaffEmail(email_content_attachment);


            }
            catch (Exception ex)
            {
                Exception exceptionToThrow;
                if (Diagnostics.ExceptionHandler.ExManager.HandleException(ex, "LogAndWrap",
                    out exceptionToThrow))
                {
                    if (exceptionToThrow == null)
                        throw;
                    else
                        throw exceptionToThrow;
                }
            }
        }
        protected void sendStaffEmail(string emailContaent)
        {
            try
            {
                String response = String.Empty;
                SendInBlue.EmailReturn emailReturn = new SendInBlue.EmailReturn();
                SendInBlue.SendInBlue sendInBlue = new SendInBlue.SendInBlue();
                string sFrom = FromEmail;
                string sTo = staffEmail;
                string sSubject = "A new service has been submitted.";
                string sBody = "<table style=\"width: 100%; color: #6d6e71;\"><tr><td>&nbsp;</td></tr><tr><td style=\"width: 10%;\">&nbsp;</td><td style=\"width: 80%; background: #ffffff; padding: 20px;\"><p style=\"font-size: 16px; color: #262f67;\"> Hello, </p><p style=\"font-size: 16px; color: #262f67;\">"+
                    "A new service has been submitted. Please review the information below, and see attachments for more detail. </p></td>"+
                    "<td>&nbsp;</td></tr></table>" + emailContaent;
                emailReturn = sendInBlue.SendEmail(sFrom, sTo, sSubject, sBody);
                response = emailReturn.message;
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
        }
        protected void sendPatientEmail(string emailContaent, string email)
        {
            String response = String.Empty;
            SendInBlue.EmailReturn emailReturn = new SendInBlue.EmailReturn();
            SendInBlue.SendInBlue sendInBlue = new SendInBlue.SendInBlue();
            string sFrom = FromEmail;
            string sTo = email;
            string sSubject = "Thanks from Ride2MD";
            string sBody = "<table style=\"background-color: #262f67; width: 100%; color: #6d6e71;\"><tr><td>&nbsp;</td></tr><tr><td style=\"width: 10%;\">&nbsp;</td><td style=\"width: 80%; background: #ffffff; padding: 20px;\"><a href=\"http://www.ride2md.com\"><img src=\"http://ride2md.com/images/logo_poster_.png\"></a><p style=\"font-family: arial; font-size: 16px; color: #262f67; text-transform: uppercase;\">Thank you for filling out the Ride Request form</p><p style=\"font-family: arial\">We have received your information. Someone from Ride2MD will be contacting you shortly.</p><p style=\"font-family: arial\">If you have requested a pickup service, remember to call in at (855) 631-7433.</p></td><td>&nbsp;</td></tr></table>" + emailContaent;
            emailReturn = sendInBlue.SendEmail(sFrom, sTo, sSubject, sBody);
            response = emailReturn.message;
        }
                       
    }
}
