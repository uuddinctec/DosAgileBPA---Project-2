<%@ Page Title="" Language="C#" MasterPageFile="~/Ride2Md.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="R2MD.Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="Server">
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.2.4/css/buttons.dataTables.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    
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

        .dt-buttons {
            float: right !important;
            margin-top: 10px;
        }   
        .createnew{
            font-size:15px;
            float:right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="Server">    
    <div class="main">
	        <h1>Service Requests<input type="button" value="Create New" class="createnew"/></h1>
	        <div class="filters">
		        <div class="filter-box">
			        <p>Filters</p>
			        <div class="filter-line">
				        <div class="filter-item">
					        <span>Client</span>
					        <input type="text" class="data-client">
				        </div>
                        <div class="filter-item">
					        <span>Invoice #</span>
					        <input type="text" class="data-invoice">
				        </div>
                        <div class="filter-item">
					        <span>Status</span>
					        <select class="data-status" style="width:50%;height:26px">
                                <option value="-1">All</option>	 
                                <option value="1" selected="selected">Unprocessed</option>                           
	                            <option value="2">Billed</option>
	                            <option value="3">Paid</option>
	                            <option value="4">Denied</option>
                            </select>

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
					        <span>Pt. Name/ID</span>
					        <input type="text" class="data-name">
				        </div>


			        </div>		
			        <button type="button" onclick="filterdata()">Filter</button>
                    <button type="button" onclick="cleardata()">Clear</button>
		        </div>
	        </div>
	        <table id="infotable" class="display" width="100%">
		        <thead>
				        <tr>
					        <th>tripID</th>
                            <th>invoiceId</th>
					        <th>Status</th>
					        <th>TripDate</th>
					        <th>PaidDate</th>
					        <th>InvoiceDate</th>
					        <th>ClientComapny</th>


				        </tr>
			        </thead>	

	        </table>
        	<table id="triptable" class="display" width="100%">
		        <thead>
				        <tr>
					        <th>tripID</th>
                            <th>PatientName</th>
					        <th>MRN</th>
					        <th>Service</th>
					        <th>Level</th>
					        <th>Date</th>
					        <th>Status</th>


				        </tr>
			        </thead>	

	        </table>



        </div>

	
</div>
    
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.2.4/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.24/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.print.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.24/build/pdfmake.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.flash.min.js"></script>
    
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script>
	
	$(document).ready(function() {
	    
	    $("#triptable").hide();
		$( ".datepicker" ).datepicker();
		var table = $('#infotable').DataTable( {
		    "ajax": "ServiceRequest.svc/GetInvoice",
			bFilter: false,
			lengthChange: false,
			select: false,
			dom: 'Bfrtip',
			buttons: [
                {
                    extend: 'excelHtml5',
                    text: 'Export',
                    title: 'Service_Requests_'+ new Date($.now())
                },
                {
                    text: 'Edit',
                    action: function (e, dt, node, config) {
                        var dd = dt.row('.selected').data();
                        window.location.replace("RequestDetail.aspx?tripid=" + dd["tripID"]);
                    }
                }
			],
			"columns": [
				{ "data": "tripID" },
                { "data": "invoiceId" },
				{ "data": "Status" },
				{ "data": "TripDate" },
				{ "data": "PaidDate" },
				{ "data": "InvoiceDate" },
				{ "data": "ClientComapny" },
			],
			"columnDefs": [
				{
					"targets": [ 0],
					"visible": false
				}
			],
			"pageLength": 25
		} );
		$('#infotable tbody').on('click', 'tr', function () {
		    if ($(this).hasClass('selected')) {
		        $(this).removeClass('selected');
		    }
		    else {
		        table.$('tr.selected').removeClass('selected');
		        $(this).addClass('selected');
		    }
		    var dataobj = table.row(this).data();
		    var ajax = "/ServiceRequest.svc/GetTrip?tripID=" + dataobj["tripID"];
		    $("#triptable").show();
		    if (!$.fn.DataTable.isDataTable('#triptable')) {
		        var triptable = $('#triptable').DataTable({
		            "ajax": ajax,
		            bFilter: false,
		            lengthChange: false,
		            bPaginate: false,
	
		            "columns": [
                        { "data": "tripID" },
                        { "data": "PatientName" },
                        { "data": "MRN" },
                        { "data": "Service" },
                        { "data": "Level" },
                        { "data": "Date" },
                        { "data": "Status" },
		            ],
		            "columnDefs": [
                        {
                            "targets": [0],
                            "visible": false
                        }
		            ],
		            "pageLength": 25
		        });
		    }
		    else {
		        $('#triptable').DataTable().ajax.url(ajax).load();
		    }
		    
		});
		
		
	});
	    function filterdata(){
		    var name = $( ".data-name" ).val();
		    var client = $(".data-client").val();
		    var datefrom = $( ".data-from" ).val();
		    var dateto = $(".data-to").val();
		    var status = $(".data-status").val();
		    var invoice = $(".data-invoice").val();
		    //alert(name+service+status+datefrom+dateto);
	        //get new data
		    var newajax = "ServiceRequest.svc/GetInvoice?Name=" + name + "&dateFrom=" + datefrom + "&dateTo=" + dateto + "&Status=" + status + "&Client=" + client + "&Invoice=" + invoice;
		    //alert(newajax);
		    $('#infotable').DataTable().ajax.url(newajax).load();
		    //refresh table
	    }
	    function cleardata() {
	        $(".data-name").val('');
	        $(".data-client").val('');
	        $(".data-from").val('');
	        $(".data-to").val('');
	        $(".data-status").val('');
	        $(".data-invoice").val('');
	        filterdata();
	    }
    </script>

</asp:Content>
