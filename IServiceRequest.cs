using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace R2MD
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceRequest" in both code and config file together.
    [ServiceContract]
    public interface IServiceRequest
    {
        
        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        PatientObj GetPatientInfo(string FirstName = null, string LastName = null);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        InvoiceObj GetInvoice(string Name = null, string Client = null, string dateFrom = null, string Invoice = null, string Status = null,string id = null);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        TripObj GetTrip(string tripID);


    }
}
