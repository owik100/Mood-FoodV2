"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/queqHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});

connection.on("ReceiveOrder", function (output) {

    var obj = JSON.parse(output);

    var table = document.getElementById("tableQ").getElementsByTagName('tbody')[0];
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    var cell1 = row.insertCell(0);
    var cell2 = row.insertCell(1);
    var cell3 = row.insertCell(2);
    var cell4 = row.insertCell(3);
    var cell5 = row.insertCell(4);
    var cell6 = row.insertCell(5);
    var cell7 = row.insertCell(6);
    cell1.innerHTML = obj.OrderDate;
    cell2.innerHTML = obj.OrderValue;
    cell3.innerHTML = obj.OptionalDescription;
    cell4.innerHTML = obj.OrderItemCount;
    cell5.innerHTML = '<a class="btn btn bg-success" href="/Manage/OrderComplete/' + obj.OrderId + ' ">Zatwierdź</a>'
    cell6.innerHTML = '<a class="btn btn bg-primary" href="/Manage/OrderDetails/' + obj.OrderId + ' ">Szczegóły</a>'
    if (obj.UserID != null) {
        cell7.innerHTML = '<a class="btn btn bg-danger" href="/Manage/UserOrders/' + obj.UserID + ' ">Użytkownik</a>'
    }
});