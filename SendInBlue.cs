using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mailinblue;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace SendInBlue
{
    public class EmailData
    {
        public string message_id { get; set; }
    }

    public class EmailReturn
    {
        public string code { get; set; }
        public string message { get; set; }
        public EmailData data { get; set; }
    }

    public class Reference
    {
        public string id { get; set; }
    }

    public class SMSData
    {
        public string status { get; set; }
        public int number_sent { get; set; }
        public string to { get; set; }
        public int sms_count { get; set; }
        public double credits_used { get; set; }
        public double remaining_credit { get; set; }
        public Reference reference { get; set; }
    }

    public class SMSReturn
    {
        public string code { get; set; }
        public string message { get; set; }
        public SMSData data { get; set; }
    }

    public class MeetingCategoryReturn
    {
        public string MeetingCategoryId { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public string ColorHex { get; set; }
    }

    public class MeetingCategoriesResponseData
    {
        public string code { get; set; }
        public string message { get; set; }
        public List<MeetingCategoryReturn> MeetingCategories { get; set; }

    }

    public class BucketBListResponseData
    {
        public string totalUnits { get; set; }
        public string totalParticipation { get; set; }
        public string totalLogevity { get; set; }
        public string lastAmountDistributed { get; set; }
        public int staffId { get; set; }
        public string code { get; set; }
        public string message { get; set; }


    }

    public class ResponseData
    {
        public string code { get; set; }
        public string message { get; set; }
        public List<SMSReturn> SMS { get; set; }
        public List<EmailReturn> Email { get; set; }
    }

    public class SendInBlue
    {
        class SMSReturnError
        {
            public string code { get; set; }
            public string message { get; set; }
            public List<SMSData> data { get; set; }
        }
        class EmailReturnError
        {
            public string code { get; set; }
            public string message { get; set; }
            public List<EmailData> data { get; set; }
        }

        public EmailReturn SendEmail(string sFrom, string sTo, string sSubject, string sBody, string attach="" )
        {
            API sendinBlue = new API(ConfigurationManager.AppSettings["SendInBlueAccessKey"]);
            List<string> from_name = new List<string>();
            Dictionary<string, Object> EmailData = new Dictionary<string, Object>();
            Dictionary<string, String> to_name = new Dictionary<string, string>();
            List<string> attachment = new List<string>();
            to_name.Add(sTo, "");
            EmailData.Add("to", to_name);
            from_name.Add(sFrom);
            EmailData.Add("from", from_name);
            EmailData.Add("subject", sSubject);
            EmailData.Add("text", sBody);
            if (!string.IsNullOrEmpty(attach)) {
                if (attach.Contains(',')) {                    
                    foreach (var i in attach.Split(',')) {
                        attachment.Add(i);
                    }
                }
                EmailData.Add("attachment", attachment);
            }            
            Object sendEmail = new Object();
            EmailReturn result = new EmailReturn();
            try
            {
                sendEmail = sendinBlue.send_email(EmailData);
                result = JsonConvert.DeserializeObject<EmailReturn>(sendEmail.ToString());
            }
            catch (Exception e)
            {
                try
                {
                    EmailReturnError errorResult;
                    errorResult = JsonConvert.DeserializeObject<EmailReturnError>(sendEmail.ToString());
                    result.code = errorResult.code;
                    result.message = errorResult.message;
                }
                catch (Exception ex)
                {
                    result.code = "Failure";
                    result.message = ex.Message + " in SendInBlue.SendEmail. [" + sendEmail.ToString() + "]";
                }
            }
            return result;
        }
        public SMSReturn SendSMS(string sFrom, string sTo, string sText, string sMo = "")
        {
            Dictionary<string, string> SMSData = new Dictionary<string, string>();
            SMSData.Add("to", sTo);
            SMSData.Add("from", sFrom);
            SMSData.Add("text", sText);
            SMSData.Add("type", "marketing");
            SMSData.Add("tag", "Tag1");
            SMSData.Add("web_url", "http://example.com");

            API sendinBlue = new mailinblue.API(ConfigurationManager.AppSettings["SendInBlueAccessKey"]);
            Object sendSms = new Object();
            SMSReturn result = new SMSReturn();
            try
            {
                sendSms = sendinBlue.send_sms(SMSData);
                result = JsonConvert.DeserializeObject<SMSReturn>(sendSms.ToString());
            }
            catch
            {
                try
                {
                    SMSReturnError errorResult;
                    errorResult = JsonConvert.DeserializeObject<SMSReturnError>(sendSms.ToString());
                    result.code = errorResult.code;
                    result.message = errorResult.message;
                }
                catch (Exception ex)
                {
                    result.code = "Failure";
                    result.message = ex.Message + " in SendInBlue.SendSMS. [" + sendSms.ToString() + "]";
                }
            }
            return result;
        }
    }
}
