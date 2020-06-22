$(document).ready(function () {

    function Dialog(arg1, arg2) {
        $('#dialog').dialog({
            buttons: {

                "отказ": function () {
                    $('#dialog').empty();
                    $(this).dialog("close");
                },

                "към теста": function () {
                    $(this).dialog("close");
                    window.location = "/Test/StartTest/" + arg2;
                }
            }
        })
            .append("<label>" + arg1 + "</label>");
    }

    function DialogEmpty(arg1) {
        $('#dialog').dialog({
            buttons: {
                "затвори": function () {
                    $('#dialog').empty();
                    $(this).dialog('close');
                }
            }
        })
            .append("<label>" + arg1 + "</label>");
    }

    $("#activeTests").on('click', function () {
        $.get("/Test/IsStudentInTest", function (response) {
            if (response.success) {
                Dialog(response.testName, response.testId);
            }
            else {
                DialogEmpty(response.message)
            }
        });
    })
})
