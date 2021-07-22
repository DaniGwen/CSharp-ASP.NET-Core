$(document).ready(function () {
    function dialog(success, testId, testName) {
        $('#dialog').dialog({
            buttons: {

                "Cancel": function () {
                    $('#dialog').empty();
                    $(this).dialog("close");
                },

                "To test": function () {
                    $(this).dialog("close");
                    window.location = "/Test/StartTest/" + testId;
                }
            }
        })
            .append("<label>" + testName + "</label>");
    }

    function dialogEmpty(message) {
        $('#dialog').dialog({
            buttons: {
                "Close": function () {
                    $('#dialog').empty();
                    $(this).dialog('close');
                }
            }
        })
            .append("<label>" + message + "</label>");
    }

    $("#activeTests").on('click',
        function () {
            $.get("/Test/IsStudentInTest",
                function (response) {
                    if (response.success) {
                        dialog(response.success, response.testId, response.testName);
                    } else {
                        dialogEmpty(response.message);
                    }
                });
        });
})
