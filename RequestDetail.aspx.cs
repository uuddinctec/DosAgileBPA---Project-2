using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;
using System.Web.Services;

namespace R2MD
{
    public partial class RequestDetail : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) {
                if (Request.QueryString["serviceid"] != null && Request.QueryString["type"] != null)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["serviceid"].ToString()) && !string.IsNullOrEmpty(Request.QueryString["type"].ToString()))
                    {
                        Session["attachmentDatatable"] = "";
                        var serviceID = Request.QueryString["serviceid"].ToString();
                        var typeID = Request.QueryString["type"].ToString();
                        if (IsDigitsOnly(typeID))
                        {
                            #region dropdownlist bind
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
                            {
                                SqlCommand cmd = new SqlCommand("SELECT [ServiceRequestStatusId],[ServiceRequestStatus] FROM [dbo].[ServiceRequestStatus]", conn);
                                conn.Open();

                                //get status table
                                SqlDataReader dr = cmd.ExecuteReader();
                                DataTable dtable = new DataTable();
                                dtable.Load(dr);
                                dr.Close();
                                if (dtable != null && dtable.Rows.Count > 0)
                                {
                                    statusDDl.DataSource = dtable;
                                    statusDDl.DataTextField = "ServiceRequestStatus";
                                    statusDDl.DataValueField = "ServiceRequestStatusId";
                                    statusDDl.DataBind();
                                }

                                //get level list
                                cmd.CommandText = "SELECT [ServiceRequestLevelId],[ServiceRequestLevel] FROM [dbo].[ServiceRequestLevels]";
                                SqlDataReader dreader = cmd.ExecuteReader();
                                DataTable datat = new DataTable();
                                datat.Load(dreader);
                                dreader.Close();
                                if (datat != null && datat.Rows.Count > 0)
                                {
                                    requestlevel.DataSource = datat;
                                    requestlevel.DataValueField = "ServiceRequestLevelId";
                                    requestlevel.DataTextField = "ServiceRequestLevel";
                                    requestlevel.DataBind();
                                    
                                }

                                cmd.CommandText = "SELECT [GuarantorRelationshipId],[Code],[Description] FROM [dbo].[GuarantorRelationship]";
                                SqlDataReader dder = cmd.ExecuteReader();
                                DataTable dta = new DataTable();
                                dta.Load(dder);
                                dder.Close();
                                if (dta != null && dta.Rows.Count > 0)
                                {
                                    requestorrelationship.DataSource = dta;
                                    requestorrelationship.DataValueField = "Code";
                                    requestorrelationship.DataTextField = "Description";
                                    requestorrelationship.DataBind();
                                    requestorrelationship.SelectedValue = "9";
                                }

                                conn.Close();
                            }
                            #endregion
                            //get service data
                            if (serviceID != "-1")
                            {
                                #region getData
                                DataTable dt = new DataTable();
                                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
                                {
                                    #region SQL Querry
                                    try
                                    {
                                        SqlCommand cmd = new SqlCommand(@"GetDetailInfo", conn);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@id", serviceID);
                                        conn.Open();
                                        SqlDataReader dr = cmd.ExecuteReader();
                                        dt.Load(dr);
                                        dr.Close();
                                    }
                                    catch (Exception ex) { }
                                    finally
                                    {
                                        if (conn != null) conn.Close();
                                    }
                                    #endregion

                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        #region Patient
                                        requestfname.Text = dt.Rows[0]["FirstName"] != DBNull.Value ? dt.Rows[0]["FirstName"].ToString() : "";
                                        requestlname.Text = dt.Rows[0]["LastName"] != DBNull.Value ? dt.Rows[0]["LastName"].ToString() : "";
                                        if (dt.Rows[0]["PatientGender"] != DBNull.Value)
                                        {
                                            if (dt.Rows[0]["PatientGender"].ToString().ToLower() == "male")
                                            {
                                                requestgender.SelectedValue = "0";
                                            }
                                            else
                                            {
                                                requestgender.SelectedValue = "1";
                                            }
                                        }
                                        //requestgender.SelectedItem.Text = dt.Rows[0]["PatientGender"] != DBNull.Value ? dt.Rows[0]["PatientGender"].ToString() : "";
                                        requestweight.Text = dt.Rows[0]["PatientWeight"] != DBNull.Value ? dt.Rows[0]["PatientWeight"].ToString() : "";

                                    if (dt.Rows[0]["PatientBirthDate"] != DBNull.Value)
                                    {
                                        if (!string.IsNullOrEmpty(dt.Rows[0]["PatientBirthDate"].ToString()))
                                        {
                                            try
                                            {
                                                requestdob.Text = Convert.ToDateTime(dt.Rows[0]["PatientBirthDate"].ToString()).ToString("yyyy-MM-dd");

                                            }
                                            catch
                                            {
                                                requestdob.Text = "";
                                            }
                                        }
                                        else
                                        {
                                            requestdob.Text = "";
                                        }
                                    }
                                    //status,
                                    var firstSelection = statusDDl.Items[0].Value;
                                    statusDDl.SelectedValue = dt.Rows[0]["ServiceRequestStatusId"] != DBNull.Value ? dt.Rows[0]["ServiceRequestStatusId"].ToString() : firstSelection;
                                    requestheight.Text = dt.Rows[0]["Height"] != DBNull.Value ? dt.Rows[0]["Height"].ToString() : "";
                                    requestssn.Text = dt.Rows[0]["PatientSocialSecurity"] != DBNull.Value ? dt.Rows[0]["PatientSocialSecurity"].ToString() : "";
                                    requestmrn.Text = dt.Rows[0]["MRN"] != DBNull.Value ? dt.Rows[0]["MRN"].ToString() : "";
                                    requestpatientid.Text = dt.Rows[0]["patientId"] != DBNull.Value ? dt.Rows[0]["patientId"].ToString() : "";
                                    requestpatientinfo.Text = dt.Rows[0]["OtherInformation"] != DBNull.Value ? dt.Rows[0]["OtherInformation"].ToString() : "";

                                    #region payorInsurance Info
                                    //payer --if insuranceID is null, use payor, else use insuranceName

                                    if (dt.Rows[0]["PatientInsuranceId"] != DBNull.Value) {
                                        if (dt.Rows[0]["PatientInsurancePlanId"] != DBNull.Value)
                                        {
                                            var payorname = dt.Rows[0]["PatientInsurancePayorName"] != DBNull.Value ? dt.Rows[0]["PatientInsurancePayorName"].ToString() : "";
                                            var add1 = dt.Rows[0]["AddressLine1"] != DBNull.Value ? dt.Rows[0]["AddressLine1"].ToString() : "";
                                            var add2 = dt.Rows[0]["AddressLine2"] != DBNull.Value ? dt.Rows[0]["AddressLine2"].ToString() : "";
                                            var city = dt.Rows[0]["City"] != DBNull.Value ? dt.Rows[0]["City"].ToString() : "";
                                            var state = dt.Rows[0]["State"] != DBNull.Value ? dt.Rows[0]["State"].ToString() : "";
                                            var ZipCode = dt.Rows[0]["ZipCode"] != DBNull.Value ? dt.Rows[0]["ZipCode"].ToString() : "";
                                            var result = payorname + " " + add1 + " " + add2 + " " + city + " " + state + " " + ZipCode;
                                            Payer.Text = result;//dt.Rows[0]["PatientInsurancePayorName"] != DBNull.Value ? dt.Rows[0]["PatientInsurancePayorName"].ToString() : "";
                                            payerID.Text = dt.Rows[0]["PatientInsurancePlanId"].ToString();
                                        }
                                        else
                                        {
                                            Payer.Text = dt.Rows[0]["PatientPayorName"] != DBNull.Value ? dt.Rows[0]["PatientPayorName"].ToString() : "";
                                        }
                                        PayID.Text = dt.Rows[0]["PatientPayorNumber"] != DBNull.Value ? dt.Rows[0]["PatientPayorNumber"].ToString() : "";
                                            try
                                            {
                                                EffectiveDate.Text = dt.Rows[0]["EffectiveDate"] != DBNull.Value ? Convert.ToDateTime(dt.Rows[0]["EffectiveDate"].ToString()).ToString("yyyy-MM-dd") : "";
                                            }
                                            catch {
                                                EffectiveDate.Text = "";
                                            }
                                        PatientInsuranceId.Text = dt.Rows[0]["PatientInsuranceId"].ToString();
                                    }


                                        
                                        //else //old payor data
                                        //{
                                        //    if (dt.Rows[0]["InsurancePlanId"] != DBNull.Value)
                                        //    {
                                        //        Payer.Text = dt.Rows[0]["PayorName"] != DBNull.Value ? dt.Rows[0]["PayorName"].ToString() : "";
                                        //        payerID.Text = dt.Rows[0]["InsurancePlanId"].ToString();
                                        //    }
                                        //    else
                                        //    {
                                        //        Payer.Text = dt.Rows[0]["Payor"] != DBNull.Value ? dt.Rows[0]["Payor"].ToString() : "";
                                        //    }
                                        //    PayID.Text = dt.Rows[0]["PayorNumber"] != DBNull.Value ? dt.Rows[0]["PayorNumber"].ToString() : "";
                                        //}

                                        #endregion

                                        PatientAdd1.Text = dt.Rows[0]["PatientAddressLine1"] != DBNull.Value ? dt.Rows[0]["PatientAddressLine1"].ToString() : "";
                                        PatientAdd2.Text = dt.Rows[0]["PatientAddressLine2"] != DBNull.Value ? dt.Rows[0]["PatientAddressLine2"].ToString() : "";
                                        PatientCity.Text = dt.Rows[0]["PatientCity"] != DBNull.Value ? dt.Rows[0]["PatientCity"].ToString() : "";
                                        PatientState.Text = dt.Rows[0]["PatientState"] != DBNull.Value ? dt.Rows[0]["PatientState"].ToString() : "";
                                        PatientZip.Text = dt.Rows[0]["PatientZip"] != DBNull.Value ? dt.Rows[0]["PatientZip"].ToString() : "";
                                        BillingAdd1.Text = dt.Rows[0]["BillingAddress1"] != DBNull.Value ? dt.Rows[0]["BillingAddress1"].ToString() : "";
                                        BillingAdd2.Text = dt.Rows[0]["BillingAddress2"] != DBNull.Value ? dt.Rows[0]["BillingAddress2"].ToString() : "";
                                        BillingCity.Text = dt.Rows[0]["BillingCity"] != DBNull.Value ? dt.Rows[0]["BillingCity"].ToString() : "";
                                        BillingState.Text = dt.Rows[0]["BillingState"] != DBNull.Value ? dt.Rows[0]["BillingState"].ToString() : "";
                                        BillingZip.Text = dt.Rows[0]["BillingZip"] != DBNull.Value ? dt.Rows[0]["BillingZip"].ToString() : "";                                                                                                                                              
                                        #endregion
                                        #region requestor
                                        if (dt.Rows[0]["CreateDateTime"] != DBNull.Value)
                                        {
                                            try
                                            {
                                                DateTime temp = Convert.ToDateTime(dt.Rows[0]["CreateDateTime"].ToString());
                                                requestdate.Text = temp.ToString("yyyy-MM-dd");
                                            }
                                            catch {
                                                requestdate.Text = "";
                                            }
                                        }
                                        else {
                                            requestdate.Text = "";
                                        }
                                        //requestdate.Text = dt.Rows[0]["CreateDateTime"] != DBNull.Value ? dt.Rows[0]["CreateDateTime"].ToString("yyyy-MM-dd") : "";
                                        if (dt.Rows[0]["RequestorId"] != DBNull.Value)
                                        {
                                            requstorfname.Text = dt.Rows[0]["GFN"] != DBNull.Value ? dt.Rows[0]["GFN"].ToString() : "";
                                            requstorlname.Text = dt.Rows[0]["GLN"] != DBNull.Value ? dt.Rows[0]["GLN"].ToString() : "";
                                            requestcompany.Text = dt.Rows[0]["EmployerName"] != DBNull.Value ? dt.Rows[0]["EmployerName"].ToString() : "";
                                            requestorrelationship.SelectedValue = dt.Rows[0]["RelationshipToPatient"] != DBNull.Value ? dt.Rows[0]["RelationshipToPatient"].ToString() : "1";
                                            requestorphone.Text = dt.Rows[0]["GP"] != DBNull.Value ? dt.Rows[0]["GP"].ToString() : "";
                                            requestorID.Text = dt.Rows[0]["RequestorId"] != DBNull.Value ? dt.Rows[0]["RequestorId"].ToString() : "";
                                        }
                                        else
                                        {
                                            requstorfname.Text = dt.Rows[0]["FirstName"] != DBNull.Value ? dt.Rows[0]["FirstName"].ToString() : "";
                                            requstorfname.Enabled = false;
                                            requstorlname.Text = dt.Rows[0]["LastName"] != DBNull.Value ? dt.Rows[0]["LastName"].ToString() : "";
                                            requstorlname.Enabled = false;
                                            requestcompany.Text = dt.Rows[0]["PatientCompany"] != DBNull.Value ? dt.Rows[0]["PatientCompany"].ToString() : "";
                                            requestorrelationship.SelectedValue = "1";
                                            requestorphone.Text = dt.Rows[0]["phone"] != DBNull.Value ? dt.Rows[0]["phone"].ToString() : "";
                                        }
                                        #region attachment
                                        //Attachment
                                        DataTable attachmentDatatable = new DataTable();
                                        attachmentDatatable.Columns.Add("PDF", typeof(string));
                                        attachmentDatatable.Columns.Add("PDFName", typeof(string));
                                        attachmentDatatable.Columns.Add("id", typeof(string));
                                        foreach (DataRow drow in dt.Rows)
                                        {
                                            var fileUrl = "";
                                            var filename = drow["FileName"] != DBNull.Value ? drow["FileName"].ToString() : "";
                                            var fileid = drow["PatientAttachmentId"] != DBNull.Value ? drow["PatientAttachmentId"].ToString() : "";
                                            string currentpath = Context.Request.Url.Scheme + "://" + Context.Request.Url.Authority;
                                            if (!string.IsNullOrEmpty(filename)) fileUrl = currentpath + "/ReadAttachment.aspx?ID=" + fileid;

                                                DataRow dr = attachmentDatatable.NewRow();
                                                dr["PDFName"] = filename;
                                                dr["PDF"] = fileUrl;
                                                dr["id"] = fileid;
                                                if (!string.IsNullOrEmpty(filename) && !string.IsNullOrEmpty(fileUrl) && !string.IsNullOrEmpty(fileid))
                                                {
                                                    attachmentDatatable.Rows.Add(dr);
                                                }

                                            }
                                            if (attachmentDatatable != null && attachmentDatatable.Rows.Count > 0)
                                            {
                                                rptPDF.DataSource = attachmentDatatable;
                                                rptPDF.DataBind();
                                                rptPDFUP.Update();
                                                rptPDF.Visible = true;
                                                Session["attachmentDatatable"] = attachmentDatatable;
                                            }
                                            else
                                            {
                                                rptPDF.Visible = false;
                                            }
                                            #endregion
                                            #endregion
                                        #region transportation
                                        if (Request.QueryString["type"].ToString() == "1")
                                        {
                                            var date = "";
                                            var time = "";
                                            if (dt.Rows[0]["ScheduleTime"] != DBNull.Value)
                                            {
                                                try
                                                {
                                                    DateTime datetime = Convert.ToDateTime(dt.Rows[0]["ScheduleTime"].ToString());
                                                    date = datetime.Date.ToString("yyyy-MM-dd");
                                                    time = datetime.TimeOfDay.ToString();
                                                }
                                                catch
                                                {
                                                    date = "";
                                                    time = "";
                                                }
                                            }
                                            requesttransdate.Text = date;
                                            requesttranstime.Text = time;
                                            //leve,
                                            var levelFirstSelection = requestlevel.Items[0].Value;
                                            requestlevel.SelectedValue = dt.Rows[0]["ServiceRequestLevelId"] != DBNull.Value ? dt.Rows[0]["ServiceRequestLevelId"].ToString() : levelFirstSelection;
                                            //requestlevel.SelectedItem.Text = dt.Rows[0]["TransportType"] != DBNull.Value ? dt.Rows[0]["TransportType"].ToString() : "";

                                            pickupID.Text = dt.Rows[0]["PickupId"] != DBNull.Value ? dt.Rows[0]["PickupId"].ToString() : "";
                                            dropoffID.Text = dt.Rows[0]["DropOffId"] != DBNull.Value ? dt.Rows[0]["DropOffId"].ToString() : "";
                                            requestpickupadd1.Text = dt.Rows[0]["PAdd1"] != DBNull.Value ? dt.Rows[0]["PAdd1"].ToString() : "";
                                            requestpickupadd2.Text = dt.Rows[0]["PAdd2"] != DBNull.Value ? dt.Rows[0]["PAdd2"].ToString() : "";
                                            requestpickupcity.Text = dt.Rows[0]["pCity"] != DBNull.Value ? dt.Rows[0]["pCity"].ToString() : "";
                                            requestpickupstate.Text = dt.Rows[0]["pState"] != DBNull.Value ? dt.Rows[0]["pState"].ToString() : "";
                                            requestpickuszip.Text = dt.Rows[0]["Pzip"] != DBNull.Value ? dt.Rows[0]["Pzip"].ToString() : "";
                                            requesttransphone.Text = dt.Rows[0]["pPhone"] != DBNull.Value ? dt.Rows[0]["pPhone"].ToString() : "";
                                            requestdropoffadd1.Text = dt.Rows[0]["dAdd1"] != DBNull.Value ? dt.Rows[0]["dAdd1"].ToString() : "";
                                            requestdropoffadd2.Text = dt.Rows[0]["dAdd2"] != DBNull.Value ? dt.Rows[0]["dAdd2"].ToString() : "";
                                            requestdropoffcity.Text = dt.Rows[0]["dCity"] != DBNull.Value ? dt.Rows[0]["dCity"].ToString() : "";
                                            requestdropoffstate.Text = dt.Rows[0]["dState"] != DBNull.Value ? dt.Rows[0]["dState"].ToString() : "";
                                            requestdropoffzip.Text = dt.Rows[0]["dzip"] != DBNull.Value ? dt.Rows[0]["dzip"].ToString() : "";
                                            requestdropoffphone.Text = dt.Rows[0]["dPhone"] != DBNull.Value ? dt.Rows[0]["dPhone"].ToString() : "";
                                            requestdriverassigned.Text = dt.Rows[0]["driverassigned"] != DBNull.Value ? dt.Rows[0]["driverassigned"].ToString() : "";
                                            requesttranscompaions.Text = dt.Rows[0]["compaions"] != DBNull.Value ? dt.Rows[0]["compaions"].ToString() : "";

                                            if (dt.Rows[0]["roundtrip"] != DBNull.Value)
                                            {
                                                if (Convert.ToBoolean(dt.Rows[0]["roundtrip"].ToString())) requesttransround.SelectedValue = "1";
                                                else requesttransround.SelectedValue = "0";
                                            }
                                            requestmileage.Text = dt.Rows[0]["mileage"] != DBNull.Value ? dt.Rows[0]["mileage"].ToString() : "";
                                            requesttransadditional.Text = dt.Rows[0]["Instruction"] != DBNull.Value ? dt.Rows[0]["Instruction"].ToString() : "";
                                            requestproviderassigned.Text = dt.Rows[0]["CompanyName"] != DBNull.Value ? dt.Rows[0]["CompanyName"].ToString() : "";
                                        }
                                        #endregion
                                        #region Interpreting
                                        else if (Request.QueryString["type"].ToString() == "2")
                                        {
                                            if (dt.Rows[0]["InterpretLanguage"] != DBNull.Value)
                                            {
                                                if (dt.Rows[0]["InterpretLanguage"].ToString() == "Spanish") requestlanguage2.SelectedValue = "0";
                                                else if (dt.Rows[0]["InterpretLanguage"].ToString() == "Creole") requestlanguage2.SelectedValue = "1";
                                                else if (dt.Rows[0]["InterpretLanguage"].ToString() == "Portuguese") requestlanguage2.SelectedValue = "2";
                                                else requestlanguage2.SelectedValue = "3";
                                            }
                                            else
                                            {
                                                requestlanguage2.SelectedValue = "3";
                                            }

                                            if (dt.Rows[0]["PickupId"] != DBNull.Value)
                                            {
                                                requestneedlocation.SelectedValue = "0";
                                                AddressID.Text = dt.Rows[0]["PickupId"] != DBNull.Value ? dt.Rows[0]["PickupId"].ToString() : "";
                                                AAdd1.Text = dt.Rows[0]["PAdd1"] != DBNull.Value ? dt.Rows[0]["PAdd1"].ToString() : "";
                                                AAdd2.Text = dt.Rows[0]["PAdd2"] != DBNull.Value ? dt.Rows[0]["PAdd2"].ToString() : "";
                                                ACity.Text = dt.Rows[0]["pCity"] != DBNull.Value ? dt.Rows[0]["pCity"].ToString() : "";
                                                AState.Text = dt.Rows[0]["pState"] != DBNull.Value ? dt.Rows[0]["pState"].ToString() : "";
                                                Azip.Text = dt.Rows[0]["Pzip"] != DBNull.Value ? dt.Rows[0]["Pzip"].ToString() : "";
                                                Aphone.Text = dt.Rows[0]["pPhone"] != DBNull.Value ? dt.Rows[0]["pPhone"].ToString() : "";

                                            }
                                            else
                                            {
                                                requestneedlocation.SelectedValue = "1";
                                                AddressID.Text = dt.Rows[0]["DropOffId"] != DBNull.Value ? dt.Rows[0]["DropOffId"].ToString() : "";
                                                AAdd1.Text = dt.Rows[0]["dAdd1"] != DBNull.Value ? dt.Rows[0]["dAdd1"].ToString() : "";
                                                AAdd2.Text = dt.Rows[0]["dAdd2"] != DBNull.Value ? dt.Rows[0]["dAdd2"].ToString() : "";
                                                ACity.Text = dt.Rows[0]["dCity"] != DBNull.Value ? dt.Rows[0]["dCity"].ToString() : "";
                                                AState.Text = dt.Rows[0]["dState"] != DBNull.Value ? dt.Rows[0]["dState"].ToString() : "";
                                                Azip.Text = dt.Rows[0]["dzip"] != DBNull.Value ? dt.Rows[0]["dzip"].ToString() : "";
                                                Aphone.Text = dt.Rows[0]["dPhone"] != DBNull.Value ? dt.Rows[0]["dPhone"].ToString() : "";

                                            }
                                        }
                                        #endregion
                                        #region other request
                                        else if (Request.QueryString["type"].ToString() == "3")
                                        {
                                            requestequipment.Text = dt.Rows[0]["EquipmentType"] != DBNull.Value ? dt.Rows[0]["EquipmentType"].ToString() : "";
                                            requestonsiteadditional.Text = dt.Rows[0]["Instruction"] != DBNull.Value ? dt.Rows[0]["Instruction"].ToString() : "";
                                        }
                                        else if (Request.QueryString["type"].ToString() == "4")
                                        {
                                            requesthomeservicetype.SelectedValue = dt.Rows[0]["HomeServiceType"] != DBNull.Value ? dt.Rows[0]["HomeServiceType"].ToString() : "Other";
                                            requesthomeadditional.Text = dt.Rows[0]["Instruction"] != DBNull.Value ? dt.Rows[0]["Instruction"].ToString() : "";
                                        }
                                        else if (Request.QueryString["type"].ToString() == "5")
                                        {
                                            requestdiagnostic.Text = dt.Rows[0]["DiagnosticServices"] != DBNull.Value ? dt.Rows[0]["DiagnosticServices"].ToString() : "";
                                            requestdiagnosticadditional.Text = dt.Rows[0]["Instruction"] != DBNull.Value ? dt.Rows[0]["Instruction"].ToString() : "";
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                            }
                            else {
                                DateTime temp = DateTime.Now;
                                requestdate.Text = temp.ToString("yyyy-MM-dd");
                            }
                        }
                        else {
                            Response.Redirect("ServiceViewer.aspx");
                        }
                    }
                    else {
                        Response.Redirect("ServiceViewer.aspx");
                    }
                }                
            }
            else
            {
                #region postBackFunction
                if (Request["__EVENTARGUMENT"].Split('_')[0] == "uploadPDF")
                {
                    bindPDF(Request["__EVENTARGUMENT"].Split('_')[1], Request["__EVENTARGUMENT"].Split('_')[2]);
                }
                else if (Request["__EVENTARGUMENT"].Split('_')[0] == "deletePDF")
                {
                    bindPDF("", "", Request["__EVENTARGUMENT"].Split('_')[1]);
                }
                else {
                    save();
                }
                #endregion
            }
        }
        private void save() {
            SaveData sd = new SaveData();
            var status = "";
            var payorInsuranceID = "";

            if (Request.QueryString["type"] != null)
            {
                //update service
                if (Request.QueryString["serviceid"] != null)
                {
                    status = statusDDl.SelectedItem.Value.ToString();

                    if (!string.IsNullOrEmpty(Request.QueryString["serviceid"].ToString()) && Request.QueryString["serviceid"].ToString() != "-1")
                    {
                        try
                        {
                            #region updateService
                            var serviceID = Request.QueryString["serviceid"].ToString();
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
                            {
                                conn.Open();
                                SqlCommand cmd = new SqlCommand();
                                cmd.Connection = conn;

                                int sID = Convert.ToInt32(Request.QueryString["serviceid"].ToString());
                                int type = Convert.ToInt32(Request.QueryString["type"].ToString());

                                var providerassigned = requestproviderassigned.Text;

                                //Save Patient Info
                                #region Patient
                                var PatientID = "";
                                //update Patient Info(exp: payer payerid status)
                                if (!string.IsNullOrEmpty(requestpatientid.Text))
                                {
                                    PatientID = requestpatientid.Text;
                                    sd.UpdatePatientInfo(requestfname.Text, requestlname.Text, requestgender.SelectedItem.Text, requestweight.Text,
                                        requestheight.Text, requestdob.Text, requestssn.Text, requestmrn.Text, PatientID, requestpatientinfo.Text, cmd,
                                        PatientAdd1.Text,PatientAdd2.Text,PatientCity.Text,PatientState.Text,PatientZip.Text,BillingAdd1.Text,BillingAdd2.Text,
                                        BillingCity.Text,BillingState.Text,BillingZip.Text);

                                }
                                //createNewPatient
                                else
                                {
                                    PatientID = sd.savePatientInfo(requestfname.Text, requestlname.Text,null, null, null, PatientAdd1.Text, PatientAdd2.Text,PatientCity.Text,
                                            PatientState.Text, null, PatientZip.Text, requestgender.SelectedItem.Text, requestweight.Text, requestheight.Text, requestdob.Text,
                                            null, requestpatientinfo.Text, requestssn.Text, requestmrn.Text, cmd,BillingAdd1.Text,BillingAdd2.Text,BillingCity.Text,
                                            BillingState.Text,BillingZip.Text);                                               
                                }
                                #endregion
                                //save requestor(exp requestdate)
                                #region requestor
                                if (requestorrelationship.SelectedItem.Value == "1")//self
                                {
                                    sd.UpdateRequestorInfo(0, requestfname.Text, requestlname.Text, "-1", sID, requestcompany.Text, "1", requestorphone.Text, cmd, PatientID);
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(requestorID.Text))
                                    { //update requestor
                                        sd.UpdateRequestorInfo(1, requstorfname.Text, requstorlname.Text, requestorID.Text, sID, requestcompany.Text, requestorrelationship.SelectedItem.Value, requestorphone.Text, cmd, PatientID);
                                    }
                                    else
                                    { //create new requestor
                                        sd.UpdateRequestorInfo(2, requstorfname.Text, requstorlname.Text, "-1", sID, requestcompany.Text, requestorrelationship.SelectedItem.Value, requestorphone.Text, cmd, PatientID);
                                    }
                                }
                                #endregion
                                //save pdate file
                                #region update file
                                DataTable dt = Session["attachmentDatatable"] as DataTable;

                                sd.deleteAttachment(PatientID, cmd);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        sd.updateUFData(PatientID, dr["id"].ToString(), cmd);
                                    }
                                }
                                #endregion

                                // save service
                                //get payorInsuranceID
                                #region Get insuranceID
                                if (!string.IsNullOrEmpty(payerID.Text))
                                {
                                    DataTable name = sd.CheckPayorName(payerID.Text, cmd);

                                    var payorname = name.Rows[0]["PayorName"] != DBNull.Value ? name.Rows[0]["PayorName"].ToString() : "";
                                    var add1 = name.Rows[0]["AddressLine1"] != DBNull.Value ? name.Rows[0]["AddressLine1"].ToString() : "";
                                    var add2 = name.Rows[0]["AddressLine2"] != DBNull.Value ? name.Rows[0]["AddressLine2"].ToString() : "";
                                    var city = name.Rows[0]["City"] != DBNull.Value ? name.Rows[0]["City"].ToString() : "";
                                    var state = name.Rows[0]["State"] != DBNull.Value ? name.Rows[0]["State"].ToString() : "";
                                    var ZipCode = name.Rows[0]["ZipCode"] != DBNull.Value ? name.Rows[0]["ZipCode"].ToString() : "";
                                    var result = payorname + " " + add1 + " " + add2 + " " + city + " " + state + " " + ZipCode;

                                    if (result.Trim() == Payer.Text.Trim())
                                    {
                                        payorInsuranceID = payerID.Text;
                                    }
                                }
                                #endregion
                                #region service
                                if (type == 1)
                                { //transportation
                                    DateTime date = new DateTime();
                                    var pID = pickupID.Text;
                                    var dID = dropoffID.Text;
                                    //update pickup && drop off location info
                                    if (!string.IsNullOrEmpty(pickupID.Text) && !string.IsNullOrEmpty(dropoffID.Text))
                                    {
                                        
                                        sd.updateAddress(Convert.ToInt32(pickupID.Text), Convert.ToInt32(dropoffID.Text), null, requestpickupadd1.Text, requestpickupadd2.Text, requestpickupcity.Text, requestpickupstate.Text,
                                            requestpickuszip.Text, null, requesttransphone.Text, null, requestdropoffadd1.Text, requestdropoffadd2.Text,
                                            requestdropoffcity.Text, requestdropoffstate.Text, requestdropoffzip.Text, null, requestdropoffphone.Text, cmd);
                                        pID = pickupID.Text;
                                        dID = dropoffID.Text;
                                    }
                                    else {
                                        
                                        if (string.IsNullOrEmpty(pickupID.Text)) {
                                            DataTable dtTable = sd.GetPDAddressID(null, requestpickupadd1.Text, requestpickupadd2.Text, requestpickupcity.Text, requestpickupstate.Text,
                                            requestpickuszip.Text, null, requesttransphone.Text, null, null, null, null, null, null, null, null,cmd, "1");
                                            if (dtTable != null && dtTable.Rows.Count > 0)
                                            {
                                                DataRow dr = dtTable.Rows[0];
                                                pID = dr["pickupID"].ToString();
                                                dID = dropoffID.Text;
                                            }
                                            if (!string.IsNullOrEmpty(dropoffID.Text)) {
                                                sd.updateAddress(-1, Convert.ToInt32(dID), null, null, null, null, null, null, null, null, null, requestdropoffadd1.Text, requestdropoffadd2.Text,
                                                requestdropoffcity.Text, requestdropoffstate.Text, requestdropoffzip.Text, null, requestdropoffphone.Text, cmd);
                                            }
                                        }
                                        if (string.IsNullOrEmpty(dropoffID.Text))
                                        {
                                            DataTable dtTable = sd.GetPDAddressID(null, null, null, null, null, null, null, null, null, requestdropoffadd1.Text, requestdropoffadd2.Text,
                                            requestdropoffcity.Text, requestdropoffstate.Text, requestdropoffzip.Text, null, requestdropoffphone.Text, cmd, "2");
                                            if (dtTable != null && dtTable.Rows.Count > 0)
                                            {
                                                DataRow dr = dtTable.Rows[0];
                                                dID = dr["dropoffID"].ToString();
                                            }
                                            if (!string.IsNullOrEmpty(pickupID.Text))
                                            {
                                                sd.updateAddress(Convert.ToInt32(pID), -1, null, requestpickupadd1.Text, requestpickupadd2.Text, requestpickupcity.Text, requestpickupstate.Text,
                                                requestpickuszip.Text, null, requesttransphone.Text, null, null, null, null, null, null, null, null, cmd);
                                            }


                                        }
                                    }
                                    if (!string.IsNullOrEmpty(requesttransdate.Text))
                                    {
                                        try
                                        {
                                            date = Convert.ToDateTime(requesttransdate.Text + " " + requesttranstime.Text);
                                        }
                                        catch
                                        {
                                            date = DateTime.MinValue;
                                        }
                                    }
                                    else
                                    {
                                        date = DateTime.MinValue;
                                    }
                                    sd.updateServiceTrans(PatientID, sID, requestmileage.Text, requestdriverassigned.Text, providerassigned, date, requesttranscompaions.Text,
                                    requesttransround.SelectedItem.Text, requestlevel.SelectedItem.Value, requesttransadditional.Text, Convert.ToInt32(status), Payer.Text, payorInsuranceID, PayID.Text,
                                    requestdate.Text, cmd, EffectiveDate.Text, PatientInsuranceId.Text,pID,dID);
                                }
                                else if (type == 2)
                                {//interpreting
                                    var langurage = requestlanguage2.SelectedItem.Text;
                                    var mode = requestneedlocation.SelectedItem.Value;
                                    var id = AddressID.Text;
                                    //update address(pickup or drop off)
                                    if (mode == "0") //pickup
                                    {
                                        sd.updateAddress(Convert.ToInt32(id), -1, null, AAdd1.Text, AAdd2.Text, ACity.Text, AState.Text,
                                            Azip.Text, null, Aphone.Text, null, null, null, null, null, null, null, null, cmd);
                                        sd.updateInterpreting(0, PatientID, Payer.Text, PayID.Text, Convert.ToInt32(status), requestdate.Text, payorInsuranceID, Convert.ToInt32(id), -1, true, false, sID, langurage, cmd, EffectiveDate.Text, PatientInsuranceId.Text);

                                    }
                                    else
                                    {
                                        sd.updateAddress(-1, Convert.ToInt32(id), null, null, null, null, null, null, null, null, null, AAdd1.Text, AAdd2.Text, ACity.Text, AState.Text,
                                            Azip.Text, null, Aphone.Text, cmd);
                                        sd.updateInterpreting(0, PatientID, Payer.Text, PayID.Text, Convert.ToInt32(status), requestdate.Text, payorInsuranceID, -1, Convert.ToInt32(id), false, true, sID, langurage, cmd, EffectiveDate.Text, PatientInsuranceId.Text);
                                    }
                                }
                                else if (type == 3)
                                {// medical                                                           
                                    sd.saveDetailServiceData(3, PatientID, Convert.ToInt32(status), sID, cmd, requestequipment.Text, requestonsiteadditional.Text, requestdate.Text, Payer.Text, payorInsuranceID, PayID.Text, EffectiveDate.Text, PatientInsuranceId.Text);
                                }
                                else if (type == 4)
                                {//home
                                    sd.saveDetailServiceData(4, PatientID, Convert.ToInt32(status), sID, cmd, requesthomeservicetype.SelectedItem.Text, requesthomeadditional.Text, requestdate.Text, Payer.Text, payorInsuranceID, PayID.Text, EffectiveDate.Text, PatientInsuranceId.Text);
                                }
                                else if (type == 5)//diagnostic
                                {
                                    sd.saveDetailServiceData(5, PatientID, Convert.ToInt32(status), sID, cmd, requestdiagnostic.Text, requestdiagnosticadditional.Text, requestdate.Text, Payer.Text, payorInsuranceID, PayID.Text, EffectiveDate.Text, PatientInsuranceId.Text);
                                }
                                //sd.saveDetailServiceData('2',Convert.ToInt32(status), sID, cmd, providerassigned,);
                                #endregion

                                if (conn != null) conn.Close();
                            }
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('Save successfully.');", true);


                            #endregion
                        }
                        catch (Exception ex)
                        {
                            string cs = ex.Message.ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('" + cs + " No changes committed.');", true);
                        }
                    }
                    //create New Service
                    else
                    {
                        CreateNewService(status, payorInsuranceID);
                    }
                }
                //create New Service
                else
                {
                    CreateNewService(status, payorInsuranceID);
                }
            }
            else {
                Response.Redirect("ServiceViewer.aspx");
            }
        }
        private void bindPDF(string input = "", string name = "", string i="")
        {
            string filePath = input;
            string fileName = name;
            if (!string.IsNullOrEmpty(filePath))
            {
                DataTable dt = Session["attachmentDatatable"] as DataTable;

                if (dt != null && dt.Rows.Count > 0)
                {
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add("PDF", typeof(string));
                    dt.Columns.Add("PDFName", typeof(string));
                    dt.Columns.Add("id", typeof(string));
                }                             
                
                    if (filePath != "")
                    {
                        DataRow dr = dt.NewRow();
                        dr["PDF"] = Context.Request.Url.Scheme + "://" + Context.Request.Url.Authority + "/ReadAttachment.aspx?ID=" + filePath;
                        dr["PDFName"] = fileName;
                        dr["id"] = filePath;
                        dt.Rows.Add(dr);
                    }

                
                rptPDF.DataSource = dt;
                rptPDF.DataBind();
                rptPDFUP.Update();
                rptPDF.Visible = true;
                Session["attachmentDatatable"] = dt;
            }
            else
            {
                if (!string.IsNullOrEmpty(i))
                {
                    DataTable dt = Session["attachmentDatatable"] as DataTable;
                    dt.Rows[Convert.ToInt32(i)].Delete();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        rptPDF.DataSource = dt;
                        rptPDF.DataBind();
                        rptPDFUP.Update();
                        rptPDF.Visible = true;
                        Session["attachmentDatatable"] = dt;
                    }
                    else {
                        rptPDF.DataSource = "";
                        rptPDF.DataBind();
                        rptPDFUP.Update();
                        rptPDF.Visible = false;
                        Session["attachmentDatatable"] = "";
                    }
                }
                else {
                    rptPDF.DataSource = "";
                    rptPDF.DataBind();
                    rptPDFUP.Update();
                    rptPDF.Visible = false;
                    Session["attachmentDatatable"] = "";
                }
                
            }
        }

        protected void CreateNewService(string status, string insID) {
            try { 
                int type = Convert.ToInt32(Request.QueryString["type"].ToString());
                var providerassigned = requestproviderassigned.Text;
                DateTime datetime = DateTime.MinValue;
                if (!string.IsNullOrEmpty(requesttransdate.Text))
                {
                    try { 
                        datetime = Convert.ToDateTime(requesttransdate.Text + " " + requesttranstime.Text);
                    }
                    catch
                    {
                        datetime = DateTime.MinValue;
                    }
                }
                else {
                    datetime = DateTime.MinValue;
                }
                SaveData sd = new SaveData();
                var GID = "";
                var PatientID = "";
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    //createNewPatient

                    //get payorInsuranceID
                    if (!string.IsNullOrEmpty(payerID.Text))
                    {
                        DataTable name = sd.CheckPayorName(payerID.Text, cmd);

                        var payorname = name.Rows[0]["PayorName"] != DBNull.Value ? name.Rows[0]["PayorName"].ToString() : "";
                        var add1 = name.Rows[0]["AddressLine1"] != DBNull.Value ? name.Rows[0]["AddressLine1"].ToString() : "";
                        var add2 = name.Rows[0]["AddressLine2"] != DBNull.Value ? name.Rows[0]["AddressLine2"].ToString() : "";
                        var city = name.Rows[0]["City"] != DBNull.Value ? name.Rows[0]["City"].ToString() : "";
                        var state = name.Rows[0]["State"] != DBNull.Value ? name.Rows[0]["State"].ToString() : "";
                        var ZipCode = name.Rows[0]["ZipCode"] != DBNull.Value ? name.Rows[0]["ZipCode"].ToString() : "";
                        var result = payorname + " " + add1 + " " + add2 + " " + city + " " + state + " " + ZipCode;

                        if (result.Trim() == Payer.Text.Trim())
                        {
                            insID = payerID.Text;
                        }
                    }
                    else insID = null;

                    if (requestorrelationship.SelectedItem.Value == "1")//self
                    {
                        if (string.IsNullOrEmpty(requestpatientid.Text))
                        {
                            PatientID = sd.savePatientInfo(requestfname.Text, requestlname.Text, null, requestorphone.Text, requestcompany.Text, PatientAdd1.Text, PatientAdd2.Text, PatientCity.Text,
                                            PatientState.Text, null, PatientZip.Text,requestgender.SelectedItem.Text, requestweight.Text, requestheight.Text, requestdob.Text,
                                            null, requestpatientinfo.Text, requestssn.Text, requestmrn.Text, cmd,BillingAdd1.Text, BillingAdd2.Text, BillingCity.Text,
                                            BillingState.Text, BillingZip.Text);
                        }
                        else {
                            PatientID = requestpatientid.Text;
                        }
                    }
                    else
                    {
                        DataTable gDT = new DataTable();
                        if (!string.IsNullOrEmpty(requestpatientid.Text)) //if patientID exist
                        {
                            //Update PatientInfo
                            PatientID = requestpatientid.Text;
                            sd.UpdatePatientInfo(requestfname.Text, requestlname.Text, requestgender.SelectedItem.Text, requestweight.Text,
                                requestheight.Text, requestdob.Text, requestssn.Text, requestmrn.Text, PatientID, requestpatientinfo.Text, cmd,
                                PatientAdd1.Text, PatientAdd2.Text, PatientCity.Text, PatientState.Text, PatientZip.Text, BillingAdd1.Text, BillingAdd2.Text,
                                BillingCity.Text, BillingState.Text, BillingZip.Text);
                            //Save New GuarantorInfo
                            gDT = sd.SaveGuarantorInformation(requstorfname.Text, requstorlname.Text, null, requestorphone.Text,
                            requestcompany.Text, requestfname.Text, requestlname.Text, null, PatientAdd1.Text, PatientAdd2.Text, PatientCity.Text, PatientState.Text, null, PatientZip.Text,
                            requestgender.SelectedItem.Text, requestweight.Text, requestheight.Text, requestdob.Text,
                            null, requestpatientinfo.Text, cmd, requestorrelationship.SelectedItem.Value, requestssn.Text, requestmrn.Text
                            , BillingAdd1.Text, BillingAdd2.Text, BillingCity.Text, BillingState.Text, BillingZip.Text, PatientID);
                        }
                        else {
                            gDT = sd.SaveGuarantorInformation(requstorfname.Text, requstorlname.Text, null, requestorphone.Text,
                            requestcompany.Text, requestfname.Text, requestlname.Text, null, PatientAdd1.Text, PatientAdd2.Text, PatientCity.Text, PatientState.Text, null, PatientZip.Text,
                            requestgender.SelectedItem.Text, requestweight.Text, requestheight.Text, requestdob.Text,
                            null, requestpatientinfo.Text, cmd, requestorrelationship.SelectedItem.Value, requestssn.Text, requestmrn.Text
                            , BillingAdd1.Text, BillingAdd2.Text, BillingCity.Text, BillingState.Text, BillingZip.Text);
                        }    
                        if (gDT != null && gDT.Rows.Count > 0)
                        {
                            DataRow dr = gDT.Rows[0];
                            PatientID = dr["PatientID"].ToString();
                            GID = dr["GuarantorId"].ToString();
                        }
                        else {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('Unknown Error! No changes committed. Please report to administrator.');", true);
                            return;
                        }
                    }

                    if (!string.IsNullOrEmpty(PatientID))
                    {
                        //save pdate file
                        #region update file
                        DataTable dt = Session["attachmentDatatable"] as DataTable;

                        sd.deleteAttachment(PatientID, cmd);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                sd.updateUFData(PatientID, dr["id"].ToString(), cmd);
                            }
                        }
                        #endregion
                        // save service
                        #region service
                        if (type == 1)
                        { //transportation                                             
                            if (requesttransround.SelectedItem.Text == "Y")
                            {
                                //save trip1 dplocation
                                DataTable dtTable = sd.GetPDAddressID(null, requestpickupadd1.Text, requestpickupadd2.Text, requestpickupcity.Text, requestpickupstate.Text,
                                    requestpickuszip.Text, null, requesttransphone.Text, null, requestdropoffadd1.Text, requestdropoffadd2.Text,
                                    requestdropoffcity.Text, requestdropoffstate.Text, requestdropoffzip.Text, null, requestdropoffphone.Text, cmd);

                                var pID = "";
                                var dID = "";
                                if (dtTable != null && dtTable.Rows.Count > 0)
                                {                                    
                                    DataRow dr = dtTable.Rows[0];
                                    pID = dr["pickupID"].ToString();
                                    dID = dr["dropoffID"].ToString();
                                    //if (IsDigitsOnly(requestmileage.Text))
                                        sd.saveService(PatientID, "1", GID, null, requesttransadditional.Text, datetime, requesttranscompaions.Text, null, null, null, Payer.Text, PayID.Text, pID, dID, null, requestlevel.SelectedItem.Text, null, null, cmd, insID, "true", status, requestmileage.Text, requestdriverassigned.Text, requesttranscompaions.Text, providerassigned, EffectiveDate.Text);
                                    //else
                                    //{
                                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('Mileage format is incorrect! No changes committed. Please report to administrator.');", true);
                                    //    return;
                                    //}
                                }
                                else {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('Unknown Error! No changes committed. Please report to administrator.');", true);
                                    return;
                                }
                                // save trip2 location
                                dtTable = sd.GetPDAddressID(null, requestdropoffadd1.Text, requestdropoffadd2.Text,
                                    requestdropoffcity.Text, requestdropoffstate.Text, requestdropoffzip.Text, null, requestdropoffphone.Text, null, requestpickupadd1.Text, requestpickupadd2.Text, requestpickupcity.Text, requestpickupstate.Text,
                                    requestpickuszip.Text, null, requesttransphone.Text, cmd);

                                if (dtTable != null && dtTable.Rows.Count > 0)
                                {
                                    datetime = Convert.ToDateTime(datetime.Date.ToString("yyyy-MM-dd") + " " + "23:59:00");
                                    DataRow dr = dtTable.Rows[0];
                                    pID = dr["pickupID"].ToString();
                                    dID = dr["dropoffID"].ToString();
                                    //if (IsDigitsOnly(requestmileage.Text))                                    
                                        sd.saveService(PatientID, "1", GID, null, requesttransadditional.Text, datetime, requesttranscompaions.Text, null, null, null, Payer.Text, PayID.Text, pID, dID, null, requestlevel.SelectedItem.Text, null, null, cmd, insID, "true", status, requestmileage.Text, requestdriverassigned.Text, requesttranscompaions.Text, providerassigned, EffectiveDate.Text);
                                    //else
                                    //{
                                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('Mileage format is incorrect! No changes committed. Please report to administrator.');", true);
                                    //    return;
                                    //}
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('Unknown Error! No changes committed. Please report to administrator.');", true);
                                    return;
                                }
                            }
                            else
                            {
                                //save trip1 dplocation
                                DataTable dtTable = sd.GetPDAddressID(null, requestpickupadd1.Text, requestpickupadd2.Text, requestpickupcity.Text, requestpickupstate.Text,
                                    requestpickuszip.Text, null, requesttransphone.Text, null, requestdropoffadd1.Text, requestdropoffadd2.Text,
                                    requestdropoffcity.Text, requestdropoffstate.Text, requestdropoffzip.Text, null, requestdropoffphone.Text, cmd);

                                var pID = "";
                                var dID = "";
                                if (dtTable != null && dtTable.Rows.Count > 0)
                                {
                                    DataRow dr = dtTable.Rows[0];
                                    pID = dr["pickupID"].ToString();
                                    dID = dr["dropoffID"].ToString();
                                    
                                    //if (IsDigitsOnly(requestmileage.Text))
                                        sd.saveService(PatientID, "1", GID, null, requesttransadditional.Text, datetime, requesttranscompaions.Text, null, null, null, Payer.Text, PayID.Text, pID, dID, null, requestlevel.SelectedItem.Text, null, null, cmd, insID, "false", status, requestmileage.Text, requestdriverassigned.Text, requesttranscompaions.Text, providerassigned, EffectiveDate.Text);
                                    //else
                                    //{
                                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('Mileage format is incorrect! No changes committed. Please report to administrator.');", true);
                                    //    return;
                                    //}
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('Unknown Error! No changes committed. Please report to administrator.');", true);
                                    return;
                                }
                            }
                        }
                        else if (type == 2)
                        {//interpreting
                            var langurage = requestlanguage2.SelectedItem.Text;
                            var mode = requestneedlocation.SelectedItem.Value;

                            if (mode == "0") //pickup
                            {
                                DataTable dtTable = sd.GetPDAddressID(null, AAdd1.Text, AAdd2.Text, ACity.Text, AState.Text, Azip.Text, null, Aphone.Text, null, null, null, null, null, null, null, null, cmd, "1");
                                if (dtTable != null && dtTable.Rows.Count > 0)
                                {
                                    DataRow dr = dtTable.Rows[0];
                                    var pID = dr["pickupID"].ToString();
                                    sd.updateInterpreting(1, PatientID, Payer.Text, PayID.Text, Convert.ToInt32(status), requestdate.Text, insID, Convert.ToInt32(pID), -1, true, false, -1, langurage, cmd,EffectiveDate.Text);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('Unknown Error! No changes committed. Please report to administrator.');", true);
                                    return;
                                }
                            }
                            else
                            {
                                DataTable dtTable = sd.GetPDAddressID(null, null, null, null, null, null, null, null, null, AAdd1.Text, AAdd2.Text, ACity.Text, AState.Text, Azip.Text, null, Aphone.Text, cmd, "2");
                                if (dtTable != null && dtTable.Rows.Count > 0)
                                {
                                    DataRow dr = dtTable.Rows[0];
                                    var dID = dr["dropoffID"].ToString();
                                    sd.updateInterpreting(1, PatientID, Payer.Text, PayID.Text, Convert.ToInt32(status), requestdate.Text, insID, -1, Convert.ToInt32(dID), false, true, -1, langurage, cmd, EffectiveDate.Text);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('Unknown Error! No changes committed. Please report to administrator.');", true);
                                    return;
                                }
                            }
                        }
                        else if (type == 3)
                        {// medical                                                           
                            sd.saveService(PatientID, "3", GID, null, requestonsiteadditional.Text, DateTime.MinValue, null, null, requestequipment.Text, null, Payer.Text, PayID.Text, null, null, null, null, null, null, cmd, insID, null, status,null,null,null,null, EffectiveDate.Text);
                        }
                        else if (type == 4)
                        {//home
                            sd.saveService(PatientID, "4", GID, null, requesthomeadditional.Text, DateTime.MinValue, null, null, null, null, Payer.Text, PayID.Text, null, null, requesthomeservicetype.SelectedItem.Text, null, null, null, cmd, insID, null, status,null, null, null, null, EffectiveDate.Text);
                        }
                        else if (type == 5)//diagnostic
                        {
                            sd.saveService(PatientID, "5", GID, null, requestdiagnosticadditional.Text, DateTime.MinValue, null, null, null, requestdiagnostic.Text, Payer.Text, PayID.Text, null, null, null, null, null, null, cmd, insID, null, status, null, null, null, null, EffectiveDate.Text);
                        }
                        //sd.saveDetailServiceData('2',Convert.ToInt32(status), sID, cmd, providerassigned,);
                        #endregion
                    }
                    else {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('Unknown Error! No changes committed. Please report to administrator.');", true);
                        return;
                    }
                    if (conn != null) conn.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('Save successfully.');", true);

                }

            }
            catch (Exception ex)
            {
                string cs = ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "error('"+cs+" No changes committed.');", true);
            }
            //Response.Redirect("ServiceViewer.aspx");
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<providerObj> GetTransCompany(string pre)
        {
            List<providerObj> result = new List<providerObj>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT [TransportationProviderId],[CompanyName] FROM [dbo].[TransportationCompanies]", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (drow["CompanyName"].ToString().ToLower().Contains(pre.ToLower()))
                        {
                            providerObj po = new providerObj();
                            po.providerID = drow["TransportationProviderId"] != DBNull.Value ? drow["TransportationProviderId"].ToString() : "";
                            po.provider = drow["CompanyName"] != DBNull.Value ? drow["CompanyName"].ToString() : "";
                            result.Add(po);
                        }
                    }
                }
                conn.Close();
            }
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<payerObj> GetPayerInfo(string pre) {
            List<payerObj> result = new List<payerObj>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(@"SELECT [InsurancePlanId],[PlanCode],[PayorName],[AddressLine1],
                        [AddressLine2],[City],[State],[ZipCode] FROM [dbo].[InsurancePlan]", conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (drow["PayorName"].ToString().ToLower().Contains(pre.ToLower()))
                        {
                            payerObj po = new payerObj();
                            po.payerID = drow["InsurancePlanId"] != DBNull.Value ? drow["InsurancePlanId"].ToString() : "";
                            var name = drow["PayorName"] != DBNull.Value ? drow["PayorName"].ToString() : "";
                            var add1 = drow["AddressLine1"] != DBNull.Value ? drow["AddressLine1"].ToString() : "";
                            var add2 = drow["AddressLine2"] != DBNull.Value ? drow["AddressLine2"].ToString() : "";
                            var city = drow["City"] != DBNull.Value ? drow["City"].ToString() : "";
                            var state = drow["State"] != DBNull.Value ? drow["State"].ToString() : "";
                            var ZipCode = drow["ZipCode"] != DBNull.Value ? drow["ZipCode"].ToString() : "";
                            po.payerName = name + " " + add1 + " " + add2 + " " + city + " " + state + " " + ZipCode;
                            result.Add(po);
                        }
                    }
                }
                conn.Close();
            }
            return result;
        }
    }    
}