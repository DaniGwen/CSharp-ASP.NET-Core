
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

//Modal for VAN details
$(function (e) {
    $('.c-card').on('click', function (event) {

        $('.c-main__van-popup').addClass('active');

        var elements = $(this);

        var brand = elements.find('.c-card__van-brand').text();
        var model = elements.find('.c-card__van-model').text();

        $('.van-details').text(model);

        $(".c-main__van-popup").dialog({
            resizable: true,
            height: 650,
            width: 500,
            modal: true,
            buttons: {
                "Close": function () {
                    $(this).dialog("close");
                }
            }
        });
    })

})