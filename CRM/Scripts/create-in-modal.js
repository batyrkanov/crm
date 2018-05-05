$(document).ready(function () {
    $("#btnSubmit").click(function () {
        var myformdata = $("#addCompany").serialize();
        $.ajax({
            type: "POST",
            url: "/Companies/Create",
            data: myformdata,
            success: function () {
                $("#myModalCompany").modal("hide");
                location.reload();
            }
        });
    });

    $("#btnSubmitCategory").click(function () {
        var myformdata = $("#addCategory").serialize();
        $.ajax({
            type: "POST",
            url: "/Categories/Create",
            data: myformdata,
            success: function () {
                $("#myModalCategory").modal("hide");
                location.reload();
            }
        });
    });

    $("#btnSubmitStatus").click(function () {
        var myformdata = $("#addStatus").serialize();
        $.ajax({
            type: "POST",
            url: "/TaskStatus/Create",
            data: myformdata,
            success: function () {
                $("#myModalStatus").modal("hide");
                location.reload();
            }
        });
    });

    $.validator.addMethod("maxDate", function (value, element) {
        var maximum = "2100-12-31T23:59:59";
        var inputDate = new DateTime(value);
        if (inputDate < maximum)
            return true;
        return false;
    }, "Год не может превышать 2100-12-31T23:59:59!"); 

    $("#datetimepicker2").validate({
        rules: {
            reportDate: {
                required: true,
                datetime: true,
                maxDate: true
            }
        }
    });
});