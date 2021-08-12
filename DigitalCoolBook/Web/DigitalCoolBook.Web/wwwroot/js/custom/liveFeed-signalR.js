"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/liveFeedHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveLiveMessages",
    function (messages) {
        for (var i = 0; i < messages.length; i++) {
            var li = document.createElement("li");
            var span = document.createElement("span");
            span.classList.add("live-feed-username");
            span.textContent = `${messages[i].userName}`;

            var messagesList = document.getElementById("messagesList");
            messagesList.appendChild(span);
            messagesList.appendChild(li);
            li.textContent = `${messages[i].message}`;
        }
    });

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    var span = document.createElement("span");
    span.classList.add("live-feed-username");
    span.textContent = `${user}`;

    var messagesList = document.getElementById("messagesList");
    messagesList.appendChild(span);
    messagesList.appendChild(li);
    li.textContent = `${message}`;
});

connection.start().then(function () {
    connection.invoke("GetLiveMessages");
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});


document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").innerHTML;
    var message = document.getElementById("messageInput").value;

    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});