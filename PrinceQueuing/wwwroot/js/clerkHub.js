//Create Connection
//var connectionQueueHub = new signalR.HubConnectionBuilder().withUrl("/hubs/queueHub").build();
var connectionQueueHub = new signalR.HubConnectionBuilder().withUrl("/PrinceQ.DataAccess/hubs/queueHub").build();
/*GetAllQueueCountNumbers();*/

//From Generate a Number By Register Personnel
connectionQueueHub.on('UpdateQueueFromPersonnel', () => {
    GetAllQueueCountNumbers();
});

//Next Queuenumber
connectionQueueHub.on('UpdateQueue', (id) => {
    GetAllQueueCountNumbers(id);
});

connectionQueueHub.on("fillingQueue", function () {
    GetAllFillingQueue();
});
connectionQueueHub.on("releasingQueue", function () {
    GetAllRleasingQueue();
});

// Put Queuenumber In Reserved
connectionQueueHub.on("reservedQueue", function () {
    GetAllReserveQueue();
});

//Serve the Queue in Reserve
connectionQueueHub.on("servedInReservedQueue", () => {
    GetAllReserveQueue();
});
//Cancel the Queue in Reserve
connectionQueueHub.on("cancelreservedqueue", () => {
    GetAllReserveQueue();
});

//Serving Display For Clerk
connectionQueueHub.on("ServeInReservedQueue", () => {
     GetAllReserveQueue();
})
//End

//CancelQueue of that User
connectionQueueHub.on("cancelQueueInMenu", () => {
    var servingDisplay = document.getElementById("servingDisplay");
    servingDisplay.innerText = "----";
});

//For that User
connectionQueueHub.on("DisplayQueue", function () {
    DisplayCurrentServe();
});



//Display In TV to Remove
connectionQueueHub.on("QueueDisplayInTvRemove", (value) => {
    var clerkname = value.toLowerCase();

    if (clerkname === 'clerk 1') {
        var display = document.getElementById('TVClerk1');
        display.innerText = "----"
    } else if (clerkname === 'clerk 2') {
        var display = document.getElementById('TVClerk2');
        display.innerText = "----"
    } else if (clerkname === 'clerk 3') {
        var display = document.getElementById('TVClerk3');
        display.innerText = "----"
    }



})

// Display In TV
connectionQueueHub.on("QueueNumberDisplayInTV", (value, clerkname) => {
    if (value) {
        var display;
        var categories = {
            1: "A",
            2: "B",
            3: "C",
            4: "D",
            5: "E",
            6: "F"
        };

        clerkname = clerkname.toLowerCase();

        if (clerkname === 'clerk 1') {
            display = document.getElementById('TVClerk1');
        } else if (clerkname === 'clerk 2') {
            display = document.getElementById('TVClerk2');
        } else if (clerkname === 'clerk 3') {
            display = document.getElementById('TVClerk3');
        }

        if (display) {
            var category = categories[value.categoryId] || "----";
            display.innerText = category + " - " + value.queueNumberServe;
        }
    }
});

function fulfilled() {
    console.log("Connection Successful");
}

function rejected() {
    console.log("Connection failed");
}

// Start connection
connectionQueueHub.start().then(fulfilled, rejected);