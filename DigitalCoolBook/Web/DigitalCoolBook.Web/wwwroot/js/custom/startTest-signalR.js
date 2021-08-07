/*"use strict";*/

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/testhub")
    .build();

connection.on("OnConnected", function (studentName) {
    var li = document.createElement("li");
    li.textContent = studentName;
    document.getElementById("activeStudents").appendChild(li);
});

connection.on("OnDisconnected", function (studentName) {
    var li = document.createElement("li");
    li.textContent = studentName;
    document.getElementById("finishedStudents").appendChild(li);

    $(`#activeStudents:contains('${studentName}')`).remove();
});

connection.on("SubmitAll",
    function () {
        submitForm();
    });

connection.start().catch(function (err) {
    return console.error(err.toString());
});
