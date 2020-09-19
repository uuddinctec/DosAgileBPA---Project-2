
$("#pickup-time").hide();
$(".pickup-validate").hide();

$('#roundtrip').click(function () {
    $("#pickup-time").toggle(this.checked);
});

$("#appt-table").on('click', '.appt-remove', function () {
    $(this).closest('tr').remove();
});


$('#appt-save').on('click', function () {

    var apptDate = $("#appt-date").val();
    var apptTime1 = $("#transport-time1").val();
    var apptTime2 = $("#transport-time2 option:selected").text();
    var apptPass = $("#passenger").val();
    var apptRT = $("#roundtrip").val();
    var apptPickup1 = $("#pickup-time1").val();
    var apptPickup2 = $("#pickup-time2 option:selected").text();

    if (apptDate == "" || apptTime1 == "") {
        $(".pickup-validate").show();
    } else {
        $('#myModal').modal('toggle');
        $(".pickup-validate").hide();


        if ($("#roundtrip").is(':checked')) {
            apptRT = "Yes";
        } else {
            apptRT = "No";
            apptPickup1 = "";
            apptPickup2 = "";
        }


        $("#appt-table").find('tbody')
        .append('<tr><td>' + apptDate + '</td><td>' + apptTime1 + apptTime2 + '</td><td>' + apptPass + '</td><td>' + apptRT + '</td><td>' + apptPickup1 + apptPickup2 + '</td><td><a class="appt-remove" id="appt-remove">x</a></td></tr>');

        $("#appt-date").val("");
        $('#datepicker').val("").datepicker('update');
        $("#transport-time1").val("");
        $("#transport-time2").find('option:first').attr('selected', 'selected');
        $("#passenger").val("");
        $("#roundtrip").removeAttr('checked');
        $("#pickup-time").hide();
        $("#pickup-time1").val("");
        $("#pickup-time2").find('option:first').attr('selected', 'selected');
    };

});



$('#requestor-phone').keyup(function () {
    this.value = this.value.replace(/(\d{3})\-?(\d{3})\-?(\d{4})/, '$1-$2-$3');
});

$('#rider-phone').keyup(function () {
    this.value = this.value.replace(/(\d{3})\-?(\d{3})\-?(\d{4})/, '$1-$2-$3');
});

$('#transport-time1').keyup(function () {
    this.value = this.value.replace(/(\d{2})\:?(\d{2})/, '$1:$2');
});

$('#pickup-time1').keyup(function () {
    this.value = this.value.replace(/(\d{2})\:?(\d{2})/, '$1:$2');
});




$('#btn-continue').on('click', function () {
    $('#review').parent().addClass('active');
    $('.tab-pane').addClass('active in');
    $('[data-toggle="tab"]').parent().removeClass('active');
    $('.pager').hide();
    $(this).hide();
    $('#submit').removeClass('hide');


});

$('a[href="#documents"]').on('shown.bs.tab', function (e) {
    $('#btn-continue').removeClass('hide');
    $('#review').on('click', function () {
        $('#review').parent().addClass('active');
        $('.tab-pane').addClass('active in');
        $('[data-toggle="tab"]').parent().removeClass('active');
        $('.pager').hide();
        $('#submit').removeClass('hide');
    });

});

$('a[href="#services"]').on('shown.bs.tab', function (e) {
    $('#btn-continue').addClass('hide');
    $add1 = $("#rider-add1").val();
    $add2 = $("#rider-add2").val();
    $city = $("#rider-city").val();
    $state = $("#rider-state").val();
    $zip = $("#rider-zip").val();
    $country = $("#rider-country").val();
    $('#pickup-add1').val($add1);
    $('#pickup-add2').val($add2);
    $('#pickup-city').val($city);
    $('#pickup-state').val($state);
    $('#pickup-zip').val($zip);
    $('#pickup-country').val($country);
});


$(document).ready(function () {

    $('#transport-date').blur(function () {
        $('#recur-start-date').val($(this).val());
    });


    $('#myself').change(function () {
        if (this.checked) {

            //get the values of the filled fields
            $fname = $("#fname").val();
            $lname = $("#lname").val();
            $phone = $("#requestor-phone").val();

            $('#rider-fname').val($fname);
            $('#rider-lname').val($lname);
            $('#rider-phone').val($phone);
            $('#pickup-phone').val($phone);


            // then form will be automatically filled .. 

        }
        else {
            $('#rider-fname').val('');
            $('#rider-lname').val('');
            $('#rider-phone').val('');
            $('#pickup-phone').val('');


        }
    });


    $('#box-transport').on('change', function () {


        if ($(this).is(':checked')) {
            $('#transport-date').addClass('required');
            $('#transport-time1').addClass('required');
            $('#pickup-name').addClass('required');
            $('#pickup-add1').addClass('required');
            $('#pickup-city').addClass('required');
            $('#pickup-state').addClass('required');
            $('#pickup-zip').addClass('required');
            $('#pickup-phone').addClass('required');
            $('#dropoff-add1').addClass('required');
            $('#dropoff-city').addClass('required');
            $('#dropoff-state').addClass('required');
            $('#dropoff-zip').addClass('required');
        } else {
            $('#transport-date').removeClass('required');
            $('#transport-time1').removeClass('required');
            $('#pickup-name').removeClass('required');
            $('#pickup-add1').removeClass('required');
            $('#pickup-city').removeClass('required');
            $('#pickup-state').removeClass('required');
            $('#pickup-zip').removeClass('required');
            $('#pickup-phone').removeClass('required');
            $('#dropoff-add1').removeClass('required');
            $('#dropoff-city').removeClass('required');
            $('#dropoff-state').removeClass('required');
            $('#dropoff-zip').removeClass('required');
        }
    });


});



