<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Ride2Md.Master" CodeBehind="RequestDetail.aspx.cs" Inherits="R2MD.RequestDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="Server">

<link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<style>
	.sectionheader{
		background-color: aliceblue;
		line-height:30px;
		padding-left: 10px;
	}
	.save{
		float: right;
		height:30px;
		width:50px;
		margin-right: 20px;
        display:none;
	}
	.insection{
		margin: 10px 40px;
	}
	.halfrow{
		width:45%;
		display: inline-table;
		vertical-align: top;
        
        margin-right:20px;
	}
    .halfitem{
        width:49%;
        display:inline-block;
    }
	.sectionitem{
		min-height: 20px;
		display: table;
		line-height: 20px;
		margin-bottom: 10px;
	}
	.sectionitem .left{
		width: 80px;
		display: inline-block;
	}
    .insideleft{
        width: 49%;
        display: inline-block;
    }
    .insideright{
        width: 49%;
        display: inline-block;
        float:right;
        margin-top:-3px
    }
	input{
		width:98%;
        height:25px;
	}
    select{
         width:98%;
         height:25px;
     }
    textarea{
        width:98%
    }
	.subsectionleft{
		width:100px;
		display: table-cell;
    	//vertical-align: middle;
	}
	.subsectionright{
		display: table-cell;
    	vertical-align: middle;
        width:100%;
	}
	.subsectioninput{
		margin-bottom: 10px;
	}
	
	
	
</style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="Server">
<h1>Service Request Details</h1>
<div class="main">
	<div class="section sec-patientinfo">
		<div class="sectionheader">PATIENT INFO <button class="save">Save</button></div>
		<div class="insection">
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Patient Name</div>
                    </div>
                    <div class="subsectionright">
                        <div class="halfitem">
                            <asp:TextBox ID="requestfname" runat="server" CssClass="" ></asp:TextBox>
                        </div>
					    <div class="halfitem">
					        <asp:TextBox ID="requestlname" runat="server" CssClass="" ></asp:TextBox>
                        </div>
                    </div>
				</div>
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Gender</div>
                    </div>
                    <div class="subsectionright">
                        <div class="halfitem">
					        <asp:DropDownList runat="server" ID="requestgender">
						        <asp:ListItem>male</asp:ListItem>
						        <asp:ListItem>Female</asp:ListItem>
					        </asp:DropDownList>
                        </div>
                        <div class="halfitem">
					            <div class="insideleft">Weight</div>
                            <div class="insideright">
					            <asp:TextBox ID="requestweight" runat="server" CssClass="" ></asp:TextBox>
                            </div>	
                        </div>
                    </div>
				</div>
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">DOB</div>
                    </div>
                    <div class="subsectionright">
                        <div class="halfitem">
					        <asp:TextBox ID="requestdob" runat="server" CssClass="datepicker" ></asp:TextBox>
                        </div>
                        <div class="halfitem">
					            <div class="insideleft">Height</div>
                            <div class="insideright">
					            <asp:TextBox ID="requestheight" runat="server" CssClass="" ></asp:TextBox>
                            </div>	
                        </div>


					    		
                    </div>
				</div>		
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">SSN</div>
                    </div>
                    <div class="subsectionright">
                        <div class="halfitem">
					        <asp:TextBox ID="requestssn" runat="server" CssClass="" ></asp:TextBox>	
                        </div>
                    </div>
				</div>																			
			</div>
			<div class="halfrow">
					
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">MRN</div>
                    </div>
                    <div class="subsectionright">
					    <asp:TextBox ID="requestmrn" runat="server" CssClass="" ></asp:TextBox>
                    </div>
				</div>
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Patient ID</div>
                    </div>
                    <div class="subsectionright">
					    <asp:TextBox ID="requestpatientid" runat="server" CssClass="" ></asp:TextBox>
                    </div>
				</div>
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Patient Info</div>	
                    </div>
                    <div class="subsectionright">
					    <asp:TextBox id="requestpatientinfo" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                    </div>
				</div>				
			</div>
		</div>
	</div>
	
	<div class="section sec-transportation">
		<div class="sectionheader">TRANSPORTATION</div>
		<div class="insection">
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Date</div>
                    </div>
                    <div class="subsectionright">
                        <div class="halfitem">
					        <asp:TextBox ID="requesttransdate" runat="server" CssClass="datepicker" ></asp:TextBox>
                        </div>
                        <div class="halfitem">
					            <div class="insideleft">Companions</div>
                            <div class="insideright">
					            <asp:TextBox ID="requesttranscompaions" runat="server" CssClass="" ></asp:TextBox>
                            </div>	
                        </div>   		
                    </div>
				</div>
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Time</div>
                    </div>
                    <div class="subsectionright">
                        <div class="halfitem">
					        <asp:TextBox ID="requesttranstime" runat="server" CssClass="" ></asp:TextBox>
                        </div>
                        <div class="halfitem">
					            <div class="insideleft">Round Trip</div>
                            <div class="insideright">
					            <asp:DropDownList runat="server" ID="requesttransround">
						            <asp:ListItem>Y</asp:ListItem>
						            <asp:ListItem>N</asp:ListItem>
					            </asp:DropDownList>	
                            </div>	
                        </div>   		
                    </div>
				</div>
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Level of Service</div>
                    </div>
					<div class="subsectionright">
						<asp:DropDownList runat="server" ID="requestlevel">
							<asp:ListItem>Ambulatory</asp:ListItem>
						</asp:DropDownList>							
					</div>	
				</div>				
				<div class="sectionitem">
					<div class="subsectionleft">
						<div class="left">Pickup Address</div>
					</div>
					<div class="subsectionright">
						<div class="subsectioninput"><asp:TextBox ID="requestpickupadd1" runat="server" CssClass="" placeholder="Add1" ></asp:TextBox></div>
						<div class="subsectioninput"><asp:TextBox ID="requestpickupadd2" runat="server" CssClass="" placeholder="Add2"></asp:TextBox></div>
						<div class="subsectioninput"><asp:TextBox ID="requestpickupcity" runat="server" CssClass="" placeholder="City"></asp:TextBox></div>
						<div class="halfitem">
					        <asp:TextBox ID="requestpickupstate" runat="server" CssClass="" placeholder="State"></asp:TextBox>
                        </div>
                        <div class="halfitem">
					        <asp:TextBox ID="requestpickuszip" runat="server" CssClass="" placeholder="Zipcode"></asp:TextBox>
                        </div>

					</div>													
				</div>	
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Phone</div>
                    </div>
                    <div class="subsectionright">
                        <div class="halfitem">
					        <asp:TextBox ID="requesttransphone" runat="server" CssClass="" ></asp:TextBox>
                        </div>
                    </div>
				</div>		
																									
			</div>
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Provider Assigned</div>
                    </div>
                    <div class="subsectionright"><asp:TextBox ID="requestproviderassigned" runat="server" CssClass="" ></asp:TextBox></div>
                   
				</div>		
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Driver Assigned</div>
                    </div>
					<div class="subsectionright"><asp:TextBox ID="requestdriverassigned" runat="server" CssClass="" ></asp:TextBox></div>
				</div>
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Mileage</div>
                    </div>
					<div class="subsectionright"><asp:TextBox ID="requestmileage" runat="server" CssClass="" ></asp:TextBox></div>	
				</div>
				<div class="sectionitem">
					<div class="subsectionleft">
						<div class="left">Dropoff Address</div>
					</div>
					<div class="subsectionright">
						<div class="subsectioninput"><asp:TextBox ID="requestdropoffadd1" runat="server" CssClass=""  placeholder="Add1"></asp:TextBox></div>
						<div class="subsectioninput"><asp:TextBox ID="requestdropoffadd2" runat="server" CssClass=""  placeholder="Add2"></asp:TextBox></div>
						<div class="subsectioninput"><asp:TextBox ID="requestdropoffcity" runat="server" CssClass=""  placeholder="City"></asp:TextBox></div>
						<div class="halfitem">
					        <asp:TextBox ID="requestdropoffstate" runat="server" CssClass=""  placeholder="State"></asp:TextBox>
                        </div>
                        <div class="halfitem">
					        <asp:TextBox ID="requestdropoffzip" runat="server" CssClass=""   placeholder="Zipcode"></asp:TextBox>
                        </div>
					</div>													
				</div>		
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Phone</div>
                    </div>
                    <div class="subsectionright">
					    <div class="halfitem"><asp:TextBox ID="requestdropoffphone" runat="server" CssClass="" ></asp:TextBox></div>
                    </div>
				</div>					
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Additional Infomation</div>
                    </div>
                    <div class="subsectionright">
					    <asp:TextBox id="requesttransadditional" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                    </div>
				</div>				
			</div>
		</div>
	</div>
	
	
	<div class="section sec-onsite">
		<div class="sectionheader">ON SITE INTERPRETING</div>
		<div class="insection">
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Language</div>
                    </div>
                    <div class="subsectionright">
					    <asp:DropDownList runat="server" ID="requestlanguage2">
						    <asp:ListItem></asp:ListItem>
					    </asp:DropDownList>	
                    </div>
				</div>
				
																									
			</div>
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Needed Location</div>
                    </div>
                    <div class="subsectionright">
					    <asp:DropDownList runat="server" ID="requestneedlocation">
						    <asp:ListItem></asp:ListItem>
					    </asp:DropDownList>	
                    </div>
				</div>		
						
			</div>
		</div>
	</div>
	
	
	<div class="section sec-medical">
		<div class="sectionheader">MEDICAL EQUIPMENT</div>
		<div class="insection">
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Equipment Type</div>
                    </div>
                    <div class="subsectionright">
					    <asp:TextBox ID="requestequipment" runat="server" CssClass="" ></asp:TextBox>
                    </div>
				</div>
				
																									
			</div>
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Additional Infomation</div>
                    </div>
                    <div class="subsectionright">
					    <asp:TextBox id="requestonsiteadditional" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                    </div>
				</div>		
						
			</div>
		</div>
	</div>	
	
	<div class="section sec-home">
		<div class="sectionheader">HOME HEALTH</div>
		<div class="insection">
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Service Type</div>
                    </div>
                    <div class="subsectionright">
					    <asp:DropDownList runat="server" ID="requesthomeservicetype">
						    <asp:ListItem></asp:ListItem>
					    </asp:DropDownList>
                    </div>
				</div>
				
																									
			</div>
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Additional Infomation</div>
                    </div>
                    <div class="subsectionright">
                        <asp:TextBox id="requesthomeadditional" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                    </div>
				</div>		
						
			</div>
		</div>
	</div>		
	
	<div class="section sec-diagnostic">
		<div class="sectionheader">DIAGNOSTIC SERVICE</div>
		<div class="insection">
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Diagnostic Services Needed</div>
                    </div>
                    <div class="subsectionright">
					    <asp:TextBox ID="requestdiagnostic" runat="server" CssClass="" ></asp:TextBox>
                    </div>
				</div>
				
																									
			</div>
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Additional Infomation</div>
                    </div>
                    <div class="subsectionright">
					    <asp:TextBox id="requestdiagnosticadditional" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                    </div>
				</div>		
						
			</div>
		</div>
	</div>		
	
	
	
	<div class="section sec-requestor">
		<div class="sectionheader">REQUESTOR INFORMATION</div>
		<div class="insection">
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Name</div>
                    </div>
                    <div class="subsectionright">
					    <div class="halfitem">
                            <asp:TextBox ID="requstorfname" runat="server" CssClass="" ></asp:TextBox>
                        </div>
					    <div class="halfitem">
					        <asp:TextBox ID="requstorlname" runat="server" CssClass="" ></asp:TextBox>
                        </div>
                    </div>
				</div>
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Request Date</div>
                    </div>
                    <div class="subsectionright">
					     <div class="halfitem"><asp:TextBox ID="requestdate" runat="server" CssClass="datepicker" ></asp:TextBox></div>
                    </div>
				</div>
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Company</div>
                    </div>
                    <div class="subsectionright">
					    <asp:TextBox ID="requestcompany" runat="server" CssClass="" ></asp:TextBox>
                    </div>
						
				</div>
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Relationship to patient</div>
                    </div>
                    <div class="subsectionright">

						<asp:DropDownList runat="server" ID="requestorrelationship">
							<asp:ListItem>Parent</asp:ListItem>
						</asp:DropDownList>	
						
					</div>	
				</div>				
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Phone</div>
                    </div>
                    <div class="subsectionright">
					    <asp:TextBox ID="requestorphone" runat="server" CssClass="" ></asp:TextBox>
                    </div>
				</div>		
																									
			</div>
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Attachment</div>
                    </div>
                    <div class="subsectionright">

					</div>
				</div>		
				
			</div>
		</div>
	</div>	
	
</div>

<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<script>
$( ".datepicker" ).datepicker();
var param1var = getQueryVariable("type");
$( ".sec-transportation" ).hide();
$( ".sec-onsite" ).hide();
$( ".sec-home" ).hide();
$( ".sec-diagnostic" ).hide();
$( ".sec-medical" ).hide();
if(param1var == "Transportation"){
	$( ".sec-transportation" ).show();
}
if(param1var == "Interpreting"){
	$( ".sec-onsite" ).show();
}
if(param1var == "Home Health"){
	$( ".sec-home" ).show();
}
if(param1var == "Diagnostic Services"){
	$( ".sec-diagnostic" ).show();
}
if(param1var == "Medical Equipment"){
	$( ".sec-medical" ).show();
}

function getQueryVariable(variable) {
  var query = window.location.search.substring(1);
  var vars = query.split("&");
  for (var i=0;i<vars.length;i++) {
    var pair = vars[i].split("=");
    if (pair[0] == variable) {
      return pair[1];
    }
  } 
  alert('Query Variable ' + variable + ' not found');
}
</script>

</asp:Content>

