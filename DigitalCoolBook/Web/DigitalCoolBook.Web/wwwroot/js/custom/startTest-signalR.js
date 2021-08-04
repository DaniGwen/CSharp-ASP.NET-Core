//"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/testhub")
    .build();

connection.on("OnConnected", function (user) {
    var li = document.createElement("li");
    li.textContent = user;
    document.getElementById("activeStudents").appendChild(li);
});

connection.on("OnDisconnected", function (message, studentName) {
    var li = document.createElement("li");
    li.textContent = message;
    document.getElementById("finishedStudents").appendChild(li);

    $("#added-students:contains(" + studentName + ")").css("text-decoration", "line-through");
});

connection.on("SubmitAll",
    function () {
        submitForm();
    });

connection.start().catch(function (err) {
    return console.error(err.toString());
});
