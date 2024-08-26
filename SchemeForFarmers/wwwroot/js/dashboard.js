"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Bidder/dashboardHub").build();

$(function () {
    connection.start().then(function () {
        InvokeBids();

    }).catch(function (err) {
        return console.error(err.toString());
    });
});

// Product
function InvokeBids() {
    connection.invoke("SendBids").catch(function (err) {
        return console.error(err.toString());
    });
}

connection.on("ReceivedBids", function (Bids) {
    BindProductsToGrid(Bids);
});

function BindProductsToGrid(Bids) {
    $('#table').empty();
    var x = 0;
    //console.log(Bids.count());
    $.each(Bids, function (index, bid) {
        x++;
    });

    var tr;

    if (x > 0) {
        $.each(Bids, function (index, bid) {
            //console.log(x);
            tr = $('<tr/>');
            tr.append(`<td>${bid.cropType}</td>`);
            tr.append(`<td>${bid.cropName}</td>`);
            tr.append(`<td>${bid.quantity}</td>`);
            tr.append(`<td>${bid.basePrice}</td>`);
            tr.append(`<td>${bid.bid}</td>`);
            tr.append('<td><a class="btn btn-outline-primary" href="/Bidder/BidOnCrop/' + bid.cropId + '">Bid on the Crop</a></td>')
            $('#table').append(tr);

        });
    }
    else {
        tr = $('<tr/>');
        tr.append('<p>No Active Auctions right now!</p>')
        $('#tb').html("");
        $('#tb').append(tr);
    }

}


var backgroundColors = [
    'rgba(255, 99, 132, 0.2)',
    'rgba(54, 162, 235, 0.2)',
    'rgba(255, 206, 86, 0.2)',
    'rgba(75, 192, 192, 0.2)',
    'rgba(153, 102, 255, 0.2)',
    'rgba(255, 159, 64, 0.2)'
];
var borderColors = [
    'rgba(255, 99, 132, 1)',
    'rgba(54, 162, 235, 1)',
    'rgba(255, 206, 86, 1)',
    'rgba(75, 192, 192, 1)',
    'rgba(153, 102, 255, 1)',
    'rgba(255, 159, 64, 1)'
];