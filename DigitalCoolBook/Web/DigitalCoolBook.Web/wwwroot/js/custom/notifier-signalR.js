var connection = new signalR.HubConnectionBuilder()
    .withUrl("/notifierhub")
    .build();

connection.on("ReceiveNotification", function (message) {
    var element = document.getElementsByClassName("notifier-message")[0];
    element.InnerHTML = message;
    console.log(message);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});