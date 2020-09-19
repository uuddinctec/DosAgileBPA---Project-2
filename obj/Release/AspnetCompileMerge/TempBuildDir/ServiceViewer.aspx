<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Ride2Md.Master" CodeBehind="ServiceViewer.aspx.cs" Inherits="R2MD.ServiceViewer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="Server">
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <style>
	    body{
		    margin:0px;
	    }
	    .main{
		    margin:10px;
	    }
	    .header{
		    background-color: aliceblue;
		    height: 40px;
	    }
	    .logo{
		    padding: 10px;
		    display: inline-block;
	    }
	    .leftitem{
		    display: inline-block;
		    margin-left: 30px;
	    }
	    .leftitem span{
		    margin-right: 15px;
	    }
	    .rightitem{
		    float: right;
		    padding: 10px;
	    }
	    .filters{
		    background-color:aliceblue;
	    }	
	    .filter-box{
		    padding: 5px 20px;
	    }
	    .filter-item{
		    display: inline-block;
		    margin-right: 30px;
		    width: 30%;
		    margin-bottom: 10px;
	    }
	    .filter-item span{
		    margin-right: 20px;
	    }	
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="Server">    
    <div class="main">
	        <h1>Service Requests</h1>
	        <div class="filters">
		        <div class="filter-box">
			        <p>Filters</p>
			        <div class="filter-line">
				        <div class="filter-item">
					        <span>Name</span>
					        <input type="text" class="data-name">
				        </div>
				        	
			        </div>
			        <div class="filter-line">
				        <div class="filter-item">
					        <span>Date Range From</span>
					        <input type="text" class="data-from datepicker">
				        </div>
				        <div class="filter-item">
					        <span>To</span>
					        <input type="text" class="data-to datepicker" >
				        </div>	
				        <div class="filter-item">
					        
				        </div>


			        </div>		
			        <button type="button" onclick="filterdata()">Filter</button>
		        </div>
	        </div>
	        <table id="infotable" class="display" width="100%">
		        <thead>
				        <tr>
					        <th>ID</th>
					        <th>Name</th>
					        <th>MRN</th>
					        <th>Patient ID</th>
					        <th>Service</th>
					        <th>Level of Service</th>
					        <th>Appt Date</th>
					        <th>Status</th>
				        </tr>
			        </thead>	

	        </table>
        </div>
    
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
	
	$(document).ready(function() {
		$( ".datepicker" ).datepicker();
		var table = $('#infotable').DataTable( {
		    "ajax": "ServiceRequest.svc/GetServiceList",
			bFilter: false,
			lengthChange : false,
			"columns": [
				{ "data": "ServiceId" },
				{ "data": "Name" },
				{ "data": "MRN" },
				{ "data": "PatientId" },
				{ "data": "RequestType" },
				{ "data": "LevelofService" },
				{ "data": "ScheduleTime" },
				{ "data": "Status" }
			],
			"columnDefs": [
				{
					"targets": [ 0 ],
					"visible": false
				}
			]
		} );
		$('#infotable tbody').on( 'click', 'tr', function () {
			var dataobj = table.row( this ).data();
			window.location.replace("RequestDetail.aspx?serviceid="+dataobj["ServiceId"]+"&type="+dataobj["RequestType"]);
		} );
		
		
		
		
	    } );
	    function filterdata(){
		    var name = $( ".data-name" ).val();
		    var service = $( ".data-service" ).val();
		     var datefrom = $( ".data-from" ).val();
		    var dateto = $( ".data-to" ).val();
		    //alert(name+service+status+datefrom+dateto);
	        //get new data
		    var newajax = "ServiceRequest.svc/GetServiceList?Name="+name+"&dateFrom="+datefrom+"&dateTo="+dateto;
		    $('#infotable').DataTable().ajax.url(newajax).load();
		    //refresh table
	    }
    </script>

</asp:Content>
