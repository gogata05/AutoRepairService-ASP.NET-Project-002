"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} : ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;    
    connection.invoke("SendMessage", "Support", "Please wait for the support to join the chat.").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    event.preventDefault();
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput");
    if (!message.value) {
        alert("Please enter a message.");
        return false;
    }
    connection.invoke("SendMessage", user, message.value).catch(function (err) {
        return console.error(err.toString());
    });  
    message.value = '';
});

document.getElementById("leaveGroup").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    connection.invoke("SendMessage", user, "Left the chat").catch(function (err) {
        return console.error(err.toString());
    });
    connection.invoke("RemoveUserFromGroup").catch(function (err) {
        return console.error(err.toString());
    });
    connection.stop();
});
