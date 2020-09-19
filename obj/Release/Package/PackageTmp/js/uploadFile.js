$(document).ready(function(){
    var file = $("#filer_input2")[0].files[countfile]; 
    countfile++;

    if (file) {
        var upload_file = file;
        var upload_filename = upload_file.name;
        var upload_maxsize = 10485760;
        var form_data = new FormData();
        form_data.append(upload_filename, upload_file);
        $.ajax({
            url: "../UploadFile.ashx",
            type: "POST",
            data: form_data,
            contentType: false,
            processData: false,
            success: function (result) {
                console.log("R " + result);
                var newvalue="";
                var valueoffiles = $("#tempValue").text();
                if (valueoffiles == "") {
                    newvalue = result;
                }
                else {
                    newvalue = valueoffiles + ',' + result;
                }
                console.log("New "+ newvalue);
                $("#tempValue").text(newvalue);
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
})


