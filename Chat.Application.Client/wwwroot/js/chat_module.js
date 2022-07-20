

"use strict";

window.onload = function () {
    var url = document.getElementById("chatNotificationHub").value;
    var connection = new signalR.HubConnectionBuilder().withUrl(url + "/chatNotificationHub").build();

    //Disable send button until connection is established
    document.getElementById("sendButton").disabled = true;

    connection.on("ReceiveChatMessage",
        function (model) {
            var element = document.getElementById("messages");
            if (element.childNodes.length === 50) {
                $("#messagesList li:nth-child(1)").remove();
            }

            var li = document.createElement("li");
            element.appendChild(li);
            li.innerHTML = "<div><strong style='margin-right:20px;'>" +
                model.userName +
                ":</strong>" +
                model.message +
                " <small style='margin-left:30px;'>" +
                model.dateTime +
                "</small></div>";
        });

    connection.on("ReceiveChatHistory",
        function (model) {
            var element = document.getElementById("messages");
            if (element.childNodes.length === 0) {

                for (var i = 0; i < model.length; i++) {
                    var li = document.createElement("li");
                    element.appendChild(li);
                    li.innerHTML = "<div><strong style='margin-right:20px;'>" +
                        model[i].userName +
                        ":</strong>" +
                        model[i].message +
                        " <small style='margin-left:30px;'>" +
                        model[i].dateTime +
                        "</small></div>";
                }
            }
        });

    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("sendButton").addEventListener("click",
        function (event) {

            var message = document.getElementById("messageInput").value;
            var user = document.getElementById("userName").value;
            var d = new Date();
            var date = d.getHours() +
                ":" +
                d.getMinutes() +
                ", " +
                (d.getMonth() + 1) +
                "/" +
                d.getDate() +
                "/" +
                d.getFullYear();
            var username = user.split("@")[0];
            var model = { UserName: username, Message: message, DateTime: date };

            connection.invoke("SendMessageAsync", model).catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
        });
};