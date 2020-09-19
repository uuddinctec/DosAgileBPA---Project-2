<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Ride2Md.Master" CodeBehind="ServiceViewer.aspx.cs" Inherits="R2MD.ServiceViewer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="Server">
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.2.4/css/buttons.dataTables.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />    
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
        .hidden{
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="Server">    
    <div class="main">
	        <h1>Service Requests<input type="button" value="Create New"  class="createnew" onclick="createnew"  data-toggle="modal" data-target="#myModal" /></h1>
	        <div class="filters">
		        <div class="filter-box">
			        <p>Filters</p>
			        <div class="filter-line">
				        <div class="filter-item">
					        <span>Name</span>
					        <input type="text" class="data-name" />
				        </div>
                        <div class="filter-item">
					        <span>Status</span>
					        <select class="data-status" style="width:50%;height:26px">
                                <option value="-1" selected="selected">All</option>	 
                                <option value="1">Unassigned</option>                           
	                            <option value="2">Assigned</option>
	                            <option value="3">Completed</option>
	                            <option value="4">Cancelled</option>
                            </select>

				        </div>				        	
			        </div>
			        <div class="filter-line">
				        <div class="filter-item">
					        <span>Date Range From</span>
					        <input type="text" class="data-from datepicker" />
				        </div>
				        <div class="filter-item">
					        <span>To</span>
					        <input type="text" class="data-to datepicker" />
				        </div>	
				        <div class="filter-item">					        
				        </div>
			        </div>		
			        <button type="button" onclick="filterdata()">Filter</button>
                    <button type="button" onclick="cleardata()">Clear</button>
                    <asp:Button runat="server" ID="Excel" OnClick="Excel_Click" Text="Export" />                                  
		        </div>
	        </div>
        <asp:HiddenField runat="server" ID="HideName"></asp:HiddenField>
        <asp:HiddenField runat="server" ID="HideFrom"></asp:HiddenField>
        <asp:HiddenField runat="server" ID="HideTo"></asp:HiddenField>
        <asp:HiddenField runat="server" ID="HideStatus"></asp:HiddenField>
	        <table id="infotable" class="display" width="100%">
		        <thead>
				    <tr>
					    <th style="display:none">ID</th>
                        <th style="display:none">Typeid</th>
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



    <!-- Modal -->
    <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Please select a type</h4>
        </div>
        <div class="modal-body">
            <select class="form-control" id="modal-option">
              <option value="1">Transportation</option>
              <option value="2">Interpreting</option>
              <option value="3">Medical Equipment</option>
              <option value="4">Home Health</option>
              <option value="5">Diagnostic Services</option>
            </select>
        </div>
      
      <div class="modal-footer">
          <input type="button" value="Create" onclick="createnew()"/>
      </div>
          </div>
    </div>
  </div>
	    
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.2.4/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/2.5.0/jszip.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.2.4/js/buttons.flash.min.js"></script>    
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script>	
	    $(document).ready(function() {
	        var selected = [];
	        var name = getQueryVariable("Name") == undefined ? "" : getQueryVariable("Name");
	        var datefrom = getQueryVariable("dateFrom") == undefined ? "" : getQueryVariable("dateFrom");
	        var dateto = getQueryVariable("dateTo") == undefined ? "" : getQueryVariable("dateTo");
	        var status = getQueryVariable("Status") == undefined ? "-1" : getQueryVariable("Status");
	        var serviceId = getQueryVariable("ServiceId") == undefined ? "" : getQueryVariable("ServiceId");
	        $(".data-name").val(decodeURIComponent(name));
	        $(".data-service").val();
	        $(".data-from").val(datefrom);
	        $(".data-to").val(dateto);
	        $(".data-status").val(status);

		    $( ".datepicker" ).datepicker();
		    var table = $('#infotable').DataTable({		        
			    bFilter: false,
			    lengthChange: false,
			    dom: 'Bfrtip',
			    buttons: [
			    ],
			    "columns": [
				    { "data": "ServiceId" },
                    { "data": "typeID" },
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
					    "targets": [ 0,1 ],
					    "visible": false
				    }
			    ],
			    "order": [[7, "asc"]],
			    "pageLength": 25,
			    rowId:'ServiceId',
		        bServerSide: true,
		        sAjaxSource: "DatatableHandler.ashx?Name=" + name + "&dateFrom=" + datefrom + "&dateTo=" + dateto + "&Status=" + status,
		        sServerMethod: 'POST',
		        bStateSave: true,
		        "rowCallback": function (row, data) {
		            if ($.inArray(data.DT_RowId, selected) !== -1) {
		                $(row).addClass('selected');
		            }
		        },
		        "initComplete": function (settings, json) {
		            var indexes = table.rows().eq(0).filter(function (rowIdx) {
		                return table.cell(rowIdx, 0).data() === serviceId ? true : false;
		            });
		            table.rows(indexes)
                        .nodes()
                        .to$()
                        .addClass('selected');
		        }
		    } );
				   
		    $('#infotable tbody').on('click', 'tr', function () {

		        var id = this.id;
		        var index = $.inArray(id, selected);		    
		        if (index === -1) {
		            selected.push(id);
		        } else {
		            selected.splice(index, 1);
		        }		    
		        $(this).toggleClass('selected');

		        var dataobj = table.row(this).data();
		        var name = $(".data-name").val();
		        var service = $(".data-service").val();
		        var datefrom = $(".data-from").val();
		        var dateto = $(".data-to").val();
		        var status = $(".data-status").val();
		        window.location.href="RequestDetail.aspx?serviceid=" + dataobj["ServiceId"] + "&type=" + dataobj["typeID"] + "&Name=" + name + "&dateFrom=" + datefrom + "&dateTo=" + dateto + "&Status=" + status;
		    } );								
	    });
	    function createnew() {
	        window.location.href = "RequestDetail.aspx?serviceid=-1&type=" + $("#modal-option").val();
	    }
	    function filterdata(){
		    var name = $( ".data-name" ).val();
		    var service = $( ".data-service" ).val();
		    var datefrom = $( ".data-from" ).val();
		    var dateto = $(".data-to").val();
		    var status = $( ".data-status" ).val();
	        //get new data
		    var newajax = "DatatableHandler.ashx?Name=" + name + "&dateFrom=" + datefrom + "&dateTo=" + dateto + "&Status=" + status;
		    $('#infotable').DataTable().ajax.url(newajax).load();
		    $('#<%=HideName.ClientID%>').val(name);
	        $('#<%=HideFrom.ClientID%>').val(datefrom);
	        $('#<%=HideTo.ClientID%>').val(dateto);
	        $('#<%=HideStatus.ClientID%>').val(status);		 
	    }
	    function cleardata() {
	        $(".data-name").val("");
	        $(".data-from").val("");
	        $(".data-to").val("");
	        $(".data-status").val("-1");
	        filterdata();
	        $('#<%=HideName.ClientID%>').val("");
	        $('#<%=HideFrom.ClientID%>').val("");
	        $('#<%=HideTo.ClientID%>').val("");
	        $('#<%=HideStatus.ClientID%>').val("-1");
	    }
	    function getQueryVariable(variable) {
	        var query = window.location.search.substring(1);
	        var vars = query.split("&");
	        for (var i = 0; i < vars.length; i++) {
	            var pair = vars[i].split("=");
	            if (pair[0] == variable) {
	                return pair[1];
	            }
	        }
	    }
	    
    </script>

</asp:Content>
