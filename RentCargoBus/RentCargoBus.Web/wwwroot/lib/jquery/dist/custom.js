
//Delete VAN dialog confirmation
function deleteVan(element) {
    var vanId = $(element).attr('id');
    $("#dialog").html("Delete van?");

    $("#dialog").dialog({
        resizable: true,
        height: 200,
        width: 300,
        modal: true,
        buttons: {

            "Cancel": function () {
                $(this).dialog("close");
            },

            "Delete": function () {
                $.ajax({
                    url: "/Admin/DeleteVan/" + vanId,
                    type: "POST",

                    success: function (message) {
                        $("#dialog").html(message);
                        $("#dialog").dialog({
                            resizable: false,
                            height: 200,
                            width: 300,
                            modal: true,
                            buttons: {

                                "Ok": function () {
                                    $(element).parentsUntil(".c-main__vans").remove();
                                    $(this).dialog('close');
                                }
                            }
                        });
                    },
                    // error dialog
                    error: function (message) {
                        $("#dialog").html(message);
                        $("#dialog").dialog({
                            maxHeight: 200,
                            width: 300,
                            modal: true,
                            buttons: {
                                "Close": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                });
                $(this).dialog("close");
            }
        }
    });
};


//Delete CAR dialog confirmation
function deleteCar(element) {
    var carId = $(element).attr('id');
    $("#dialog").html("Delete car?");

    $("#dialog").dialog({
        resizable: true,
        height: 200,
        width: 300,
        modal: true,
        buttons: {

            "Cancel": function () {
                $(this).dialog("close");
            },

            "Delete": function () {
                $.ajax({
                    url: "/Admin/DeleteCar/" + carId,
                    type: "POST",
                    success: function (message) {
                        $("#dialog").html(message);
                        $("#dialog").dialog({
                            resizable: false,
                            height: 200,
                            width: 300,
                            modal: true,
                            buttons: {
                                "Ok": function () {
                                    $(element).parentsUntil(".c-main__vans").remove();
                                    $(this).dialog('close');
                                }
                            }
                        });
                    },
                    // error dialog
                    error: function (message) {
                        $("#dialog").html(message);
                        $("#dialog").dialog({
                            maxHeight: 200,
                            width: 300,
                            modal: true,
                            buttons: {
                                "Close": function () {
                                    $(this).dialog("close");
                                }
                            }
                        });
                    }
                });
                $(this).dialog("close");
            }
        }
    });
};

//Image enlarge in VanDetails and CarDetails
function openModal(e) {
    var img = $(e).attr('src');
    $('.vehicle-details__image-enlarge').addClass('active');
    $('.vehicle-details__image-enlarge__image').attr('src', img);
    $('.vehicle-details__image-enlarge__close').on('click', function () {
        $('.vehicle-details__image-enlarge').removeClass('active');
    })
};

//Send Email
$(function () {
    $('.vehicle-details__contact-us__send-email').on('click', function () {
        $('.send-email-modal').dialog("open");
    });

    $(function () {
        $('.send-email-modal').dialog({
            autoOpen: false,
            show: {
                effect: "clip"
            },
            height: 400,
            width: 350,
            modal: true,
            buttons: {
                "Send request": function () {
                    post();
                    $(this).dialog("close").fadeOut("slow");
                },
                Cancel: function () {
                    $(this).fadeOut("slow", function () {
                        $(this).dialog('close');
                    });
                }
            }
        });
    })

    $(function () {
        $('.send-email-modal__result').dialog({
            autoOpen: false,
            show: {
                effect: "clip"
            },
            height: 250,
            width: 300,
            modal: true,
            buttons: {
                "Close": function () {
                    $(this).hide("fast", function () {
                        $(this).dialog('close');
                    });
                }
            }
        });
    })

    function post() {
        var brand = $('.vehicle-details__brand').text();
        var model = $('.vehicle-details__model').text();
        var plate = $('.plate-number').text();
        var sender = $("#name").val();
        var senderEmail = $("#email").val();

        $.post({
            url: '/Home/SendEmail',
            data: { 'brand': brand, 'model': model, 'plate': plate, 'sender': sender, 'senderEmail': senderEmail },
            success: function () {
                $('.send-email-modal__result').prop('title', 'Success');
                $('.send-email-modal__result').html("<h4>Request send!</h4><br/><h5>Thank you.</h5>").css({
                    textAlign: 'center',
                });
                $('.send-email-modal__result').dialog("open");
            },
            error: function () {
                $('.send-email-modal__result').prop('title', 'Error');
                $('.send-email-modal__result').html("<strong>Could not send request...</strong><br/><p>Please use the provided phone number.</p><br/><p>Thank you.</p>");
                $('.send-email-modal__result').dialog("open");
            }
        })
    }
})
