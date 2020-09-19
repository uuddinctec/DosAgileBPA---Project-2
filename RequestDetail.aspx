<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Ride2Md.Master" CodeBehind="RequestDetail.aspx.cs" Inherits="R2MD.RequestDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" type="text/css" href="css/jquery.timepicker.css" />
    <style>
	    .sectionheader{
		    background-color: aliceblue;
		    line-height:30px;
		    padding-left: 10px;
            width: 100%;
            height: 30px;
	    }
        .btn-info {
            color: #fff;
            background-color: #5bc0de;
            border-color: #46b8da;
            padding: 3px 10px;
        }
	    .save{
		    float: right;
		    height:30px;
		    width:50px;
		    margin-right: 20px;
                line-height: 20px;
            /*display:none;*/
	    }
	    .release{
		    float: right;
		    height:30px;
		    width:90px;
		    margin-right: 20px;
                line-height: 20px;
            /*display:none;*/
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
        .threeitem{
            width:32%;
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
    	    /*//vertical-align: middle;*/
	    }
	    .subsectionright{
		    display: table-cell;
    	    vertical-align: middle;
            width:100%;
	    }
	    .subsectioninput{
		    margin-bottom: 10px;
	    }
        .white {
            background-color:white !important
        }
	    th, td {
            border: 1px solid silver;
        }
	    select[disabled]{
          background-color: #eee;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody" runat="Server">
    <h1>Service Request Details</h1>
    <div class="main">
	    <div class="section sec-patientinfo">
		    <div class="sectionheader">
               <div style="float:left">PATIENT INFO</div>
                <input type="submit" class="save" value="Save"/>
                <input class="release" value="Edit Patient" type="button" onclick="patientrelease()" />
                <input class="save" type="button" value="Back" onclick="back()" />      
		    </div>
		    <div class="insection">
			    <div class="halfrow">
				    <div class="sectionitem">
                        <div class="subsectionleft">
					        <div class="left">Patient Name</div>
                        </div>
                        <div class="subsectionright">
                            <div class="threeitem">
                                <asp:TextBox ID="requestfname" runat="server" CssClass="" required="required"></asp:TextBox>
                            </div>
					        <div class="threeitem">
					            <asp:TextBox ID="requestlname" runat="server" CssClass=""  required="required"></asp:TextBox>
                            </div>
					        <div class="threeitem">
                                <input type="button" value="Search" data-toggle="modal" data-target="#myModal"/>
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
						            <asp:ListItem Value="0">Male</asp:ListItem>
						            <asp:ListItem Value="1">Female</asp:ListItem>
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
					            <asp:TextBox ID="requestdob" runat="server" CssClass="datepicker" type="date"></asp:TextBox>
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
                <div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Payer</div>
                    </div>
                    <div class="subsectionright">
                        <div class="halfitem">
					        <asp:TextBox ID="Payer" runat="server" CssClass="payerinfocomplete"  required="required"></asp:TextBox>
                            <div style="display:none"><asp:TextBox ID="payerID" runat="server" CssClass="" ></asp:TextBox></div>
                        </div>
                        <div class="halfitem">
					            <div class="insideleft">ID</div>
                            <div class="insideright">
					            <asp:TextBox ID="PayID" runat="server" CssClass="" ></asp:TextBox>
                            </div>	
                        </div>   		
                    </div>
				</div>	
                <div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Effective Date</div>	
                    </div>
                    <div class="subsectionright">					    
                        <div class="halfitem">
					        <asp:TextBox id="EffectiveDate" CssClass="datepicker" type="date" runat="server" />
                        </div>
                    </div>
				</div>
                <div class="sectionitem">
					<div class="subsectionleft">
						<div class="left">Address</div>
					</div>
					<div class="subsectionright">
						<div class="subsectioninput"><asp:TextBox ID="PatientAdd1" runat="server" CssClass=""  placeholder="Add1"  required="required"></asp:TextBox></div>
						<div class="subsectioninput"><asp:TextBox ID="PatientAdd2" runat="server" CssClass=""  placeholder="Add2" ></asp:TextBox></div>
						<div class="subsectioninput"><asp:TextBox ID="PatientCity" runat="server" CssClass=""  placeholder="City"  required="required"></asp:TextBox></div>
						<div class="halfitem">
					        <asp:TextBox ID="PatientState" runat="server" CssClass=""  placeholder="State"  required="required"></asp:TextBox>
                        </div>
                        <div class="halfitem">
					        <asp:TextBox ID="PatientZip" runat="server" CssClass=""   placeholder="Zipcode"  required="required"></asp:TextBox>
                        </div>
					</div>													
				</div>																		
			</div>
			<div class="halfrow">
				<div class="sectionitem">
                    <div class="subsectionleft">
					    <div class="left">Status</div>
                    </div>
                    <div class="subsectionright">
					    <asp:DropDownList runat="server" ID="statusDDl" CssClass="white"></asp:DropDownList>
                    </div>
				</div>	
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
					    <div class="left">Internal ID</div>
                    </div>
                    <div class="subsectionright">
					    <asp:TextBox ID="requestpatientid" runat="server" CssClass="" disabled="disabled"></asp:TextBox>
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
                
                <div class="sectionitem">
					<div class="subsectionleft">
						<div class="left">Billing Address</div>
					</div>
					<div class="subsectionright">
						<div class="subsectioninput"><asp:TextBox ID="BillingAdd1" runat="server" CssClass=""  placeholder="Add1"  ></asp:TextBox></div>
						<div class="subsectioninput"><asp:TextBox ID="BillingAdd2" runat="server" CssClass=""  placeholder="Add2" ></asp:TextBox></div>
						<div class="subsectioninput"><asp:TextBox ID="BillingCity" runat="server" CssClass=""  placeholder="City"  ></asp:TextBox></div>
						<div class="halfitem">
					        <asp:TextBox ID="BillingState" runat="server" CssClass=""  placeholder="State"  ></asp:TextBox>
                        </div>
                        <div class="halfitem">
					        <asp:TextBox ID="BillingZip" runat="server" CssClass=""   placeholder="Zipcode" ></asp:TextBox>
                        </div>
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
					        <asp:TextBox ID="requesttransdate" runat="server" CssClass="datepicker"  type="date" required="required" ></asp:TextBox>
                        </div>
                        <div class="halfitem">
					            <div class="insideleft">Companions</div>
                            <div class="insideright">
					            <asp:TextBox ID="requesttranscompaions" runat="server" CssClass="" type="number" ></asp:TextBox>
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
					        <asp:TextBox ID="requesttranstime" runat="server" CssClass="timepicker" ></asp:TextBox>
                        </div>
                        <div class="halfitem">
					            <div class="insideleft">Round Trip</div>
                                <div class="insideright">
					                <span> <%--style="float:left;width:49%">--%>
                                        <asp:DropDownList runat="server" ID="requesttransround">
						                    <asp:ListItem Value="1">Y</asp:ListItem>
						                    <asp:ListItem Value="0">N</asp:ListItem>
					                    </asp:DropDownList>	
					                </span>
                                   <%-- <span style="float:right;width:45%"><input type="button" value="..." ></input></span>--%>
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
						    </asp:DropDownList>							
					    </div>	
				    </div>
                    <%--<div class="sectionitem">
                        <div class="subsectionleft">
					        <div class="left">Pickup Location Name</div>
                        </div>
                        <div class="subsectionright">                        
					        <asp:TextBox ID="PickupLocation" runat="server" CssClass=""  required="required" placeholder="Location Name"></asp:TextBox>                        
                        </div>
				    </div>	--%>					
				    <div class="sectionitem">
					    <div class="subsectionleft">
						    <div class="left">Address</div>
					    </div>
					    <div class="subsectionright">
                            <div style="display:none"><asp:TextBox runat="server" ID="pickupID"></asp:TextBox></div>
						    <div class="subsectioninput"><asp:TextBox ID="requestpickupadd1" runat="server" CssClass="" placeholder="Add1"  required="required"></asp:TextBox></div>
						    <div class="subsectioninput"><asp:TextBox ID="requestpickupadd2" runat="server" CssClass="" placeholder="Add2"></asp:TextBox></div>
						    <div class="subsectioninput"><asp:TextBox ID="requestpickupcity" runat="server" CssClass="" placeholder="City"  required="required"></asp:TextBox></div>
						    <div class="halfitem">
					            <asp:TextBox ID="requestpickupstate" runat="server" CssClass="" placeholder="State"  required="required"></asp:TextBox>
                            </div>
                            <div class="halfitem">
					            <asp:TextBox ID="requestpickuszip" runat="server" CssClass="" placeholder="Zipcode"  required="required"></asp:TextBox>
                            </div>

					    </div>													
				    </div>	
				    <div class="sectionitem">
                        <div class="subsectionleft">
					        <div class="left">Phone</div>
                        </div>
                        <div class="subsectionright">
                            <div class="halfitem">
					            <asp:TextBox ID="requesttransphone" runat="server" CssClass=""  required="required"></asp:TextBox>
                            </div>
                        </div>
				    </div>		
																									
			    </div>
			    <div class="halfrow">
				    <div class="sectionitem">
                        <div class="subsectionleft">
					        <div class="left">Provider Assigned</div>
                        </div>
                        <div class="subsectionright">
                            <asp:TextBox ID="requestproviderassigned" runat="server" CssClass="Providerassigned" ></asp:TextBox>
                            <asp:TextBox ID="providerID" runat="server" ></asp:TextBox>
                        </div>
                   
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
                    <%--<div class="sectionitem">
                        <div class="subsectionleft">
					        <div class="left">Cost</div>
                        </div>
                        <div class="subsectionright">                        
					        <asp:TextBox ID="Cost" runat="server" CssClass=""  required="required" placeholder=" "></asp:TextBox>                        
                        </div>
				    </div>	--%>
                    <%--<div class="sectionitem">
                        <div class="subsectionleft">
					        <div class="left">Drop Off Location Name</div>
                        </div>
                        <div class="subsectionright">                        
					        <asp:TextBox ID="DropOffLocation" runat="server" CssClass=""  required="required" placeholder="Location Name"></asp:TextBox>                        
                        </div>
				    </div>	--%>
				    <div class="sectionitem">
					    <div class="subsectionleft">
						    <div class="left">Address</div>
					    </div>
					    <div class="subsectionright">
                            <div style="display:none"><asp:TextBox runat="server" ID="dropoffID"></asp:TextBox></div>
						    <div class="subsectioninput"><asp:TextBox ID="requestdropoffadd1" runat="server" CssClass=""  placeholder="Add1"  required="required"></asp:TextBox></div>
						    <div class="subsectioninput"><asp:TextBox ID="requestdropoffadd2" runat="server" CssClass=""  placeholder="Add2" ></asp:TextBox></div>
						    <div class="subsectioninput"><asp:TextBox ID="requestdropoffcity" runat="server" CssClass=""  placeholder="City"  required="required"></asp:TextBox></div>
						    <div class="halfitem">
					            <asp:TextBox ID="requestdropoffstate" runat="server" CssClass=""  placeholder="State"  required="required"></asp:TextBox>
                            </div>
                            <div class="halfitem">
					            <asp:TextBox ID="requestdropoffzip" runat="server" CssClass=""   placeholder="Zipcode"  required="required"></asp:TextBox>
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
					        <div class="left">Needed Location</div>
                        </div>
                        <div class="subsectionright">
					        <asp:DropDownList runat="server" ID="requestneedlocation">
						        <asp:ListItem Value="0">PICK UP</asp:ListItem>
                                <asp:ListItem Value="1">DROP OFF</asp:ListItem>
					        </asp:DropDownList>	
                        </div>

				    </div>	
				    <div class="sectionitem">
					    <div class="subsectionleft">
						    <div class="left">Address</div>
					    </div>
					    <div class="subsectionright">
                            <div style="display:none"><asp:TextBox runat="server" ID="AddressID"></asp:TextBox></div>
						    <div class="subsectioninput"><asp:TextBox ID="AAdd1" runat="server" CssClass="" placeholder="Add1"  required="required"></asp:TextBox></div>
						    <div class="subsectioninput"><asp:TextBox ID="AAdd2" runat="server" CssClass="" placeholder="Add2"></asp:TextBox></div>
						    <div class="subsectioninput"><asp:TextBox ID="ACity" runat="server" CssClass="" placeholder="City"  required="required"></asp:TextBox></div>
						    <div class="halfitem">
					            <asp:TextBox ID="AState" runat="server" CssClass="" placeholder="State"  required="required"></asp:TextBox>
                            </div>
                            <div class="halfitem">
					            <asp:TextBox ID="Azip" runat="server" CssClass="" placeholder="Zipcode"  required="required"></asp:TextBox>
                            </div>

					    </div>
                    													
				    </div>	
				    <div class="sectionitem">
                        <div class="subsectionleft">
					        <div class="left">Phone</div>
                        </div>
                        <div class="subsectionright">
					        <div class="halfitem"><asp:TextBox ID="Aphone" runat="server" CssClass=""  required="required"></asp:TextBox></div>
                        </div>
				    </div>																							
			    </div>

			    <div class="halfrow">
					
				    <div class="sectionitem">
                        <div class="subsectionleft">
					        <div class="left">Language</div>
                        </div>
                        <div class="subsectionright">
					        <asp:DropDownList runat="server" ID="requestlanguage2">	
                                <asp:ListItem Value="0" >Spanish</asp:ListItem>
                                <asp:ListItem Value="1" >Creole</asp:ListItem>
                                <asp:ListItem Value="2" >Portuguese</asp:ListItem>
                                <asp:ListItem Value="3" >Other</asp:ListItem>					    
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
						        <asp:ListItem Value="Certified Nurse">Certified Nurse</asp:ListItem>
                                <asp:ListItem Value="Home Health Aide">Home Health Aide</asp:ListItem>
                                <asp:ListItem Value="RN/LPN">RN/LPN</asp:ListItem>                            
                                <asp:ListItem Value="Other">Other</asp:ListItem>
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
                                <asp:TextBox ID="requstorfname" runat="server" CssClass=""  required="required"></asp:TextBox>
                                <div style="display:none"><asp:TextBox ID="requestorID" runat="server"  ></asp:TextBox></div>
                            </div>
					        <div class="halfitem">
					            <asp:TextBox ID="requstorlname" runat="server" CssClass="" required="required" ></asp:TextBox>
                            </div>
                        </div>
				    </div>
				    <div class="sectionitem">
                        <div class="subsectionleft">
					        <div class="left">Request Date</div>
                        </div>
                        <div class="subsectionright">
					         <div class="halfitem"><asp:TextBox ID="requestdate" runat="server" CssClass="datepicker" type="date"></asp:TextBox></div>
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
						    </asp:DropDownList>	
						
					    </div>	
				    </div>				
				    <div class="sectionitem">
                        <div class="subsectionleft">
					        <div class="left">Phone</div>
                        </div>
                        <div class="subsectionright">
					        <asp:TextBox ID="requestorphone" runat="server" CssClass=""  required="required"></asp:TextBox>
                        </div>
				    </div>		
																									
			    </div>
			    <div class="halfrow">
				    <div class="sectionitem">
                        <div class="subsectionleft">
					        <div class="left">Attachment</div>
                        </div>
                        <div class="subsectionright">                          
                            <div class="pdfUpdateSection">
                                <span style="float:left;margin-bottom:10px">
                                    <asp:UpdatePanel runat="server" ID="UploadUP"  UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:FileUpload ID="FileUpload" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>                       
                                </span>
                                <div class="divButton btn btn-info" style="cursor:pointer;    float: right; " onclick="uploadPDF();">Upload</div>
                            </div>  
                            <div>
                                <asp:UpdatePanel runat="server" ID="hidePDF"  UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="display:none"><asp:Label ID="uploadFID" runat="server"></asp:Label></div>
                                        <div style="display:none"><asp:Label ID="uploadName" runat="server"></asp:Label></div>
                                        <div style="display:none"><asp:Label runat="server" ID="deleteFID"></asp:Label></div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>     
                            </div>                                                
                            <div style="width: 100%;float: left;margin-bottom:15px">
                                <asp:UpdatePanel runat="server" ID="rptPDFUP" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Repeater runat="server" ID="rptPDF">
                                            <HeaderTemplate>
                                                <table>
                                                    <tr style="    height: 39px;"><th style="padding: 5px;width: 85%;cursor:pointer">PDF Name</th><th style="min-width: 20px;"></th></tr>                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="padding: 5px;word-break: break-word;"><a href="<%# Eval("PDF") %>"><%# Eval("PDFName") %></a></td>
                                                    <td>   
                                                        <div class="btn btn-info" style="padding: 3px 5px; margin: 5px 6px;cursor:pointer" id="<%#Container.ItemIndex.ToString() %>" onclick="DeleteFunction(this);">Delete</div>   
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                    
                            </div>
					    </div>
				    </div>		
				
			</div>
		</div>
	</div>	

    <div style="display:none"><asp:TextBox runat="server" ID="PatientInsuranceId"></asp:TextBox></div>






      <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Patient List</h4>
        </div>
        <div class="modal-body">
          <div class="row">
                <div class="responsive-table">
                    <table class="table table-bordered" id="patientTable">
                        <thead>
                            <tr>
                                <th style="display:none">Frist Name</th>
                                <th style="display:none">Last Name</th>
                                <th style="display:none">DOB</th>
                                <th style="display:none">Gender</th>
                                <th style="display:none">Height</th>
                                <th style="display:none">Weight</th>
                                <th style="display:none">SSN</th>
                                <th style="display:none">Payer</th>
                                <th style="display:none">PayerID</th>
                                <th style="display:none">ID</th>
                                <th style="display:none">MRN</th>
                                <th style="display:none">PatientID</th>
                                <th style="display:none">Info</th>
                                <th style="display:none">Add1</th>
                                <th style="display:none">Add2</th>
                                <th style="display:none">city</th>
                                <th style="display:none">state</th>
                                <th style="display:none">zip</th>
                                <th style="display:none">phone</th>
                                <th >User</th>
                                <th style="display:none">BAdd1</th>
                                <th style="display:none">BAdd2</th>
                                <th style="display:none">Bcity</th>
                                <th style="display:none">Bstate</th>
                                <th style="display:none">Bzip</th>
                                <th style="display:none">effectiveDate</th>
                                
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
      </div>
      
    </div>
  </div>
	
</div>

    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript" src="js/jquery.timepicker.js"></script>
    <script src="https://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script>

    $("#cphBody_providerID").hide();
    $(".release").hide();
    var table = $('#patientTable').DataTable();
    if ($("#requestpatientid").val() != "") {
        $("#cphBody_requestgender").prop('disabled', true);
        $("#cphBody_requestweight").prop('disabled', true);
        $("#cphBody_requestheight").prop('disabled', true);
        $("#cphBody_requestdob").prop('disabled', true);
        $("#cphBody_requestssn").prop('disabled', true);
        $("#cphBody_Payer").prop('disabled', true);
        $("#cphBody_payerID").prop('disabled', true);
        $("#cphBody_PayID").prop('disabled', true);
        $("#cphBody_requestmrn").prop('disabled', true);
        $("#cphBody_requestpatientid").prop('disabled', true);
        $("#cphBody_requestpatientinfo").prop('disabled', true);
        $("#cphBody_PatientAdd1").prop('disabled', true);
        $("#cphBody_PatientAdd2").prop('disabled', true);
        $("#cphBody_PatientCity").prop('disabled', true);
        $("#cphBody_PatientState").prop('disabled', true);
        $("#cphBody_PatientZip").prop('disabled', true);
        $("#cphBody_BillingAdd1").prop('disabled', true);
        $("#cphBody_BillingAdd2").prop('disabled', true);
        $("#cphBody_BillingCity").prop('disabled', true);
        $("#cphBody_BillingState").prop('disabled', true);
        $("#cphBody_BillingZip").prop('disabled', true);
        $("#cphBody_EffectiveDate").prop('disabled', true);
        $(".release").show();
    }
    $("#cphBody_requestorrelationship").change(function () {
        if ($("#cphBody_requestorrelationship").val() == "1") {
            $("#cphBody_requstorfname").val($("#cphBody_requestfname").val());
            $("#cphBody_requstorlname").val($("#cphBody_requestlname").val());
            $("#cphBody_requstorfname").prop('disabled', true);
            $("#cphBody_requstorlname").prop('disabled', true);
            
        }
        else {
            $("#cphBody_requstorfname").prop('disabled', false);
            $("#cphBody_requstorlname").prop('disabled', false);
        }
    });


    $('#form1').submit(function ()
    {
        $("#cphBody_requstorfname").prop('disabled', false);
        $("#cphBody_requstorlname").prop('disabled', false);
        $("#cphBody_requestgender").prop('disabled', false);
        $("#cphBody_requestweight").prop('disabled', false);
        $("#cphBody_requestheight").prop('disabled', false);
        $("#cphBody_requestdob").prop('disabled', false);
        $("#cphBody_requestssn").prop('disabled', false);
        $("#cphBody_Payer").prop('disabled', false);
        $("#cphBody_payerID").prop('disabled', false);
        $("#cphBody_PayID").prop('disabled', false);
        $("#cphBody_requestmrn").prop('disabled', false);
        $("#cphBody_requestpatientinfo").prop('disabled', false);
        $("#cphBody_requestpatientid").prop('disabled', false);
        $("#cphBody_PatientAdd1").prop('disabled', false);
        $("#cphBody_PatientAdd2").prop('disabled', false);
        $("#cphBody_PatientCity").prop('disabled', false);
        $("#cphBody_PatientState").prop('disabled', false);
        $("#cphBody_PatientZip").prop('disabled', false);
        $("#cphBody_BillingAdd1").prop('disabled', false);
        $("#cphBody_BillingAdd2").prop('disabled', false);
        $("#cphBody_BillingCity").prop('disabled', false);
        $("#cphBody_BillingState").prop('disabled', false);
        $("#cphBody_BillingZip").prop('disabled', false);
        $("#cphBody_EffectiveDate").prop('disabled', false);
        
        __doPostBack('<%=rptPDFUP.UniqueID%>', 'save_1');

        return false;

    // ... continue work
    });

    $('#myModal').on('shown.bs.modal', function () {
        var firstname = $('#cphBody_requestfname').val();
        var lastname = $('#cphBody_requestlname').val();
        var ajaxcall = "ServiceRequest.svc/GetPatientInfo?FirstName="+firstname+"&&LastName="+lastname;
        var table = $('#patientTable').DataTable({
            "ajax": ajaxcall,
            bFilter: false,
            lengthChange: false,
            dom: 'Bfrtip',
            "bDestroy": true,
            buttons: [
                {
                    extend: 'excelHtml5',
                    title: 'Service_Requests_' + new Date($.now())
                }
            ],
            "columns": [
				{ "data": "FirstName" },
                { "data": "LastName" },
				{ "data": "DOB" },
                { "data": "Gender" },
                { "data": "Height" },
                { "data": "Weight" },
                { "data": "SSN" },
                { "data": "Payer" },
                { "data": "PayerID" },
                { "data": "ID" },
                { "data": "MRN" },
                { "data": "PatientID" },
                { "data": "Info" },
                { "data": "Add1" },
                { "data": "Add2" },
                { "data": "City" },
                { "data": "State" },
                { "data": "zip" },
                { "data": "phone" },
                { "data": "data" },
                { "data": "BAdd1" },
                { "data": "BAdd2" },
                { "data": "BCity" },
                { "data": "BState" },
                { "data": "Bzip" },                
                { "data": "effectiveDate" }
            ],
            "columnDefs": [
				{
				    "targets": [0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,20,21,22,23,24,25],
				    "visible": false
				}
            ],
        });
        $('#patientTable tbody').off('click');
        $('#patientTable tbody').on('click', 'tr', function () {
            var dataobj = table.row(this).data();
            $('#cphBody_requestgender').val(dataobj["Gender"]);
            $("#cphBody_requestgender").prop('disabled', true);
            $('#cphBody_requestweight').val(dataobj["Weight"]);
            $("#cphBody_requestweight").prop('disabled', true);
            $('#cphBody_requestheight').val(dataobj["Height"]);
            $("#cphBody_requestheight").prop('disabled', true);
            $('#cphBody_requestdob').val(dataobj["DOB"]);
            $("#cphBody_requestdob").prop('disabled', true);
            $('#cphBody_requestssn').val(dataobj["SSN"]);
            $("#cphBody_requestssn").prop('disabled', true);
            $('#cphBody_Payer').val(dataobj["Payer"]);
            $("#cphBody_Payer").prop('disabled', true);
            $('#cphBody_payerID').val(dataobj["PayerID"]);
            $("#cphBody_payerID").prop('disabled', true);
            $('#cphBody_PayID').val(dataobj["ID"]);
            $("#cphBody_PayID").prop('disabled', true);
            $('#cphBody_requestmrn').val(dataobj["MRN"]);
            $("#cphBody_requestmrn").prop('disabled', true);
            $('#cphBody_requestpatientid').val(dataobj["PatientID"]);
            $("#cphBody_requestpatientid").prop('disabled', true);
            $('#cphBody_requestpatientinfo').val(dataobj["Info"]);
            $("#cphBody_requestpatientinfo").prop('disabled', true);
            $('#cphBody_PatientAdd1').val(dataobj["Add1"]);
            $("#cphBody_PatientAdd1").prop('disabled', true);
            $('#cphBody_PatientAdd2').val(dataobj["Add2"]);
            $("#cphBody_PatientAdd2").prop('disabled', true);
            $('#cphBody_PatientCity').val(dataobj["City"]);
            $("#cphBody_PatientCity").prop('disabled', true);
            $('#cphBody_PatientState').val(dataobj["State"]);
            $("#cphBody_PatientState").prop('disabled', true);
            $('#cphBody_PatientZip').val(dataobj["zip"]);
            $("#cphBody_PatientZip").prop('disabled', true);
            $('#cphBody_BillingAdd1').val(dataobj["BAdd1"]);
            $("#cphBody_BillingAdd1").prop('disabled', true);
            $('#cphBody_BillingAdd2').val(dataobj["BAdd2"]);
            $("#cphBody_BillingAdd2").prop('disabled', true);
            $('#cphBody_BillingCity').val(dataobj["BCity"]);
            $("#cphBody_BillingCity").prop('disabled', true);
            $('#cphBody_BillingState').val(dataobj["BState"]);
            $("#cphBody_BillingState").prop('disabled', true);
            $('#cphBody_BillingZip').val(dataobj["Bzip"]);
            $("#cphBody_BillingZip").prop('disabled', true);
            $('#cphBody_EffectiveDate').val(dataobj["effectiveDate"]);
            $("#cphBody_EffectiveDate").prop('disabled', true);
            
            if ($("#cphBody_requestorrelationship").val() == "1") {
                $("#cphBody_requstorfname").val($("#cphBody_requestfname").val());
                $("#cphBody_requstorlname").val($("#cphBody_requestlname").val());
                $("#cphBody_requstorfname").prop('disabled', true);
                $("#cphBody_requstorlname").prop('disabled', true);

                }
                else {
                    $("#cphBody_requstorfname").prop('disabled', false);
                    $("#cphBody_requstorlname").prop('disabled', false);
                }

                var param1var = getQueryVariable("type");
                if (param1var == "1" ) {
                    $('#cphBody_requestpickupadd1').val(dataobj["Add1"]);
                    $('#cphBody_requestpickupadd2').val(dataobj["Add2"]);
                    $('#cphBody_requestpickupcity').val(dataobj["City"]);
                    $('#cphBody_requestpickupstate').val(dataobj["State"]);
                    $('#cphBody_requestpickuszip').val(dataobj["zip"]);
                    $('#cphBody_requesttransphone').val(dataobj["phone"]);
                }
                else if(param1var == "2"){
                    $('#cphBody_AAdd1').val(dataobj["Add1"]);
                    $('#cphBody_AAdd2').val(dataobj["Add2"]);
                    $('#cphBody_ACity').val(dataobj["City"]);
                    $('#cphBody_AState').val(dataobj["State"]);
                    $('#cphBody_Azip').val(dataobj["State"]);
                    $('#cphBody_Aphone').val(dataobj["State"]);
                }
                $('.modal').modal('hide').data('bs.modal', null);
                $("#myModal .close").click();
                $('.modal-backdrop').remove();
                $(".release").show();

            });
        });
        $(function () {
            var payerid;
            $(".payerinfocomplete").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "RequestDetail.aspx/GetPayerInfo",
                        data: "{ 'pre':'" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item.payerName,
                                    des: item.payerID
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            console.log(textStatus);
                        }
                    });
                },
                minLength: 2,
                select: function (event, ui) {
                    $("#cphBody_payerID").val(ui.item.des);
                }
            });
        });
        $(function () {        
            $(".Providerassigned").autocomplete({
                source: function (request, response) {
                    $.ajax({ 
                        url: "RequestDetail.aspx/GetTransCompany",
                        data: "{ 'pre':'" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item.provider
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            console.log(textStatus);
                        }
                    });
                },
                minLength: 2,
                select: function (event, ui) {
                    $("#cphBody_statusDDl").val('2');
                }
            });
            $("#cphBody_requestproviderassigned").change(function () {
                if ($("#cphBody_requestproviderassigned").val() != "") {
                    $("#cphBody_statusDDl").val('2');
                }
                else {
                    $("#cphBody_statusDDl").val('1');
                }
            });        
        });

    function patientrelease() {
        $("#cphBody_requestgender").prop('disabled', false);
        $("#cphBody_requestweight").prop('disabled', false);
        $("#cphBody_requestheight").prop('disabled', false);
        $("#cphBody_requestdob").prop('disabled', false);
        $("#cphBody_requestssn").prop('disabled', false);
        $("#cphBody_Payer").prop('disabled', false);
        $("#cphBody_payerID").prop('disabled', false);
        $("#cphBody_PayID").prop('disabled', false);
        $("#cphBody_requestmrn").prop('disabled', false);
        $("#cphBody_requestpatientinfo").prop('disabled', false);
        $("#cphBody_PatientAdd1").prop('disabled', false);
        $("#cphBody_PatientAdd2").prop('disabled', false);
        $("#cphBody_PatientCity").prop('disabled', false);
        $("#cphBody_PatientState").prop('disabled', false);
        $("#cphBody_PatientZip").prop('disabled', false);
        $("#cphBody_BillingAdd1").prop('disabled', false);
        $("#cphBody_BillingAdd2").prop('disabled', false);
        $("#cphBody_BillingCity").prop('disabled', false);
        $("#cphBody_BillingState").prop('disabled', false);
        $("#cphBody_BillingZip").prop('disabled', false);
        $("#cphBody_EffectiveDate").prop('disabled', false);
        $(".release").hide();
    }


        $(".datepicker").datepicker({ dateFormat: 'yy-mm-dd' });
        $('.timepicker').timepicker({ show2400: true, timeFormat :'H:i',});
        var param1var = getQueryVariable("type");
        $( ".sec-transportation" ).hide();
        $( ".sec-onsite" ).hide();
        $( ".sec-home" ).hide();
        $( ".sec-diagnostic" ).hide();
        $( ".sec-medical" ).hide();
        if(param1var == "1"){
            $(".sec-transportation").show();
            $(".sec-onsite").remove();
            $(".sec-home").remove();
            $(".sec-diagnostic").remove();
            $(".sec-medical").remove();
        }
        if(param1var == "2"){
            $(".sec-onsite").show();
            $(".sec-transportation").remove();
            $(".sec-home").remove();
            $(".sec-diagnostic").remove();
            $(".sec-medical").remove();
        }
        if(param1var == "4"){
            $(".sec-home").show();
            $(".sec-transportation").remove();
            $(".sec-onsite").remove();
            $(".sec-diagnostic").remove();
            $(".sec-medical").remove();
        }
        if(param1var == "5"){
            $(".sec-diagnostic").show();
            $(".sec-transportation").remove();
            $(".sec-onsite").remove();
            $(".sec-home").remove();
            $(".sec-medical").remove();
        }
        if(param1var == "3"){
            $(".sec-medical").show();
            $(".sec-transportation").remove();
            $(".sec-onsite").remove();
            $(".sec-home").remove();
            $(".sec-diagnostic").remove();
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
          //alert('Query Variable ' + variable + ' not found');
        }


        function uploadPDF() {
            var file = $("#<%=FileUpload.ClientID%>")[0].files[0]
            if (file) {
                var upload_file = file;
                var upload_filename = upload_file.name;
                var form_data = new FormData();
                form_data.append(upload_filename, upload_file);
                $.ajax({
                    url: "UploadFile.ashx",
                    type: "POST",
                    data: form_data,
                    contentType: false,
                    processData: false,
                    success: function (result) {                    
                        var temp = $("#<%=uploadFID.ClientID%>").text();
                        var name = $("#<%=uploadName.ClientID%>").text();
                        var newvalue = "", newname="";
                        var valueoffiles = $("#tempValue").text();
                        if (temp == "") {
                            newvalue = result;
                            newname = upload_file.name;
                        }
                        else {
                            newvalue = temp + ',' + result;
                            newname = name + ',' + upload_file.name;
                        }
                        console.log("New " + newvalue);
                        $("#<%=uploadFID.ClientID%>").text(newvalue);
                        $("#<%=uploadName.ClientID%>").text(newname);
                        __doPostBack('<%=rptPDFUP.UniqueID%>', 'uploadPDF_' + result + '_' + upload_file.name);
                    },
                    error: function (err) {
                        console.log(err);
                    }
                });
            }
        }
        function DeleteFunction (obj){            
            var i = obj.id;
            __doPostBack('<%=rptPDFUP.UniqueID%>', 'deletePDF_' + i);
        }
        function error(x) {
            alert(x);
            $("#cphBody_requestpatientid").prop('disabled', true);
            if (x == "Save successfully.") {
                var name = getQueryVariable("Name") == undefined ? "" : getQueryVariable("Name");
                var datefrom = getQueryVariable("dateFrom") == undefined ? "" : getQueryVariable("dateFrom");
                var dateto = getQueryVariable("dateTo") == undefined ? "" : getQueryVariable("dateTo");
                var status = getQueryVariable("Status") == undefined ? "-1" : getQueryVariable("Status");
                var serviceID = getQueryVariable("serviceid") == undefined ? "" : getQueryVariable("serviceid");
                var service = getQueryVariable("Service") == undefined ? "" : getQueryVariable("Service");
                var provider = getQueryVariable("Provider") == undefined ? "" : getQueryVariable("Provider");
                window.location.href = "ServiceViewer.aspx?Name=" + name + "&dateFrom=" + datefrom + "&dateTo=" + dateto + "&Status=" + status + "&ServiceId=" + serviceID + "&Service=" + service + "&Provider=" + provider;
            }

        }    
        function back() {
            var name = getQueryVariable("Name") == undefined ? "" : getQueryVariable("Name");
            var datefrom = getQueryVariable("dateFrom") == undefined ? "" : getQueryVariable("dateFrom");
            var dateto = getQueryVariable("dateTo") == undefined ? "" : getQueryVariable("dateTo");
            var status = getQueryVariable("Status") == undefined ? "1" : getQueryVariable("Status");
            var serviceID = getQueryVariable("serviceid") == undefined ? "" : getQueryVariable("serviceid");
            var service = getQueryVariable("Service") == undefined ? "" : getQueryVariable("Service");
            var provider = getQueryVariable("Provider") == undefined ? "" : getQueryVariable("Provider");
            window.location.href = "ServiceViewer.aspx?Name=" + name + "&dateFrom=" + datefrom + "&dateTo=" + dateto + "&Status=" + status + "&ServiceId=" + serviceID + "&Service=" + service + "&Provider=" + provider;

        }
    </script>
</asp:Content>

