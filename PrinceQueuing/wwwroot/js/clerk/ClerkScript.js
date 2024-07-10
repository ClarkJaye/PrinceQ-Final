$(document).ready(function () {
    GetClerkNumber();
    //Display Current Serve
    DisplayCurrentServe();

    //Load all Filling Queue
    GetAllFillingQueue();
    //Load all releasing Queue
    GetAllRleasingQueue();

    //LOAD all Reserve Queue
    GetAllReserveQueue();
    //LOAD All Queuenumbers Count on each Category
    GetAllQueueCountNumbers();

    // Bind click event to the Next 
    $('#WaitingCountNumber').on("click", ".nextBtn", getNextQueueNumber);
    // Bind click event to the Done 
    $('#callBtn').on("click", CallQueue);
    // Bind click event to the Reserve
    $('#reserveBtn').on("click", ReserveQueue);
    // Bind click event to the Cancel
    $('#cancelBtn').on("click", CancelQueue);
    // Bind click event to the Serve Queue in  Reserve Table
    $('#resQueues').on("click", ".serveReserveQueueBtn", ServeQueueInTable);
    // Bind click event to the Cancel Queue in  Reserve Table
    $('#resQueues').on("click", ".cancelReserveQueueBtn", CancelQueueFromTable);
    // Bind click event to Transfer the Queue from filling up table to releasing table
    $('#fillingQueues').on("click", ".toReleasingBtn", ToReleasingQueue);
    // Bind click event to the Cancel Queue in  Filling Table
    $('#fillingQueues').on("click", ".cancelFillingBtn", CancelQueueFromTable);
    // Bind click event to the Serve Queue in  Release Table
    $('#releasingQueues').on("click", ".serveReleasingBtn", ServeQueueInTable);
    // Bind click event to the Cancel Queue in  Release Table
    $('#releasingQueues').on("click", ".cancelReleaseBtn", CancelQueueFromTable);
});

//Date and Time
const displayDateTime = () => {
    var currentTime = new Date();
    var options = { hour: '2-digit', minute: '2-digit', second: '2-digit', hour12: true };
    var formattedTime = currentTime.toLocaleString('en-US', options);
    var optionsDate = { year: 'numeric', month: 'long', day: 'numeric' };
    var formattedDate = currentTime.toLocaleString('en-US', optionsDate);

    $('#time').text(formattedTime);
    $('#date').text(formattedDate);
};
// Initial display
displayDateTime();
setInterval(displayDateTime, 1000);
//Display Current Serve
function DisplayCurrentServe() {
    var servingDisplay = document.getElementById("servingDisplay");
    $.ajax({
        type: 'GET',
        url: "/Clerk/GetServings",
        dataType: "json",
        success: function (response) {
            if (response != null) {
                if (response.categoryId === 1) {
                    servingDisplay.innerText = "A - " + response.queueNumber;
                } else if (response.categoryId === 2) {
                    servingDisplay.innerText = "B - " + response.queueNumber;
                } else if (response.categoryId === 3) {
                    servingDisplay.innerText = "C - " + response.queueNumber;
                } else if (response.categoryId === 4) {
                    servingDisplay.innerText = "D - " + response.queueNumber;
                } else {
                    servingDisplay.innerText = "----";
                }       
            }
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });
}

//Display Designated Clerk
function GetClerkNumber() {
    $.ajax({
        url: "/Clerk/DesignatedDeviceId",
        type: 'GET',
        success: function (response) {
            console.log(response)
            var clerk = response.device;
            var user = response.user;
            $('#ClerkNum').text(clerk.clerkNumber);

            // Trim the clerk number
            var clerkNumber = clerk.clerkNumber.replace("Clerk ", "").trim();
            var storageKey = "Clerk" + clerkNumber;

            // Remove all existing user IDs stored in localStorage
            for (let i = 0; i < localStorage.length; i++) {
                let key = localStorage.key(i);
                if (key.startsWith("Clerk")) {
                    // Check if the key matches the current clerk number
                    if (key !== storageKey) {
                        localStorage.removeItem(key);
                    }
                }
            }

            // Store the new user ID
            localStorage.setItem(storageKey, user.id)
        },
        error: function (error) {
            console.log(error);
        }
    });
}

// Function to get the next queue number
function getNextQueueNumber() {
    var button = $(this);
    var callBtn = $('#callBtn');
    var categoryId = button.attr('id');
/*    var prevQueue = JSON.parse(localStorage.getItem('queueItem'));*/

    $.ajax({
        type: 'GET',
        url: "/Clerk/NextQueue",
        dataType: "json",
        //data: { id: categoryId, prevQueue: JSON.stringify(prevQueue) },
        data: { id: categoryId },
        success: function (response) {
            var queue = response.obj;
            if (response && response.isSuccess == false) {
                //alert(response.message);
                button.prop('disabled', true);
                setTimeout(function () {
                    button.prop('disabled', false);
                }, 1000)
            }
            else {
                // Store the Queue Object into local storage
/*                localStorage.setItem('queueItem', JSON.stringify(queue));*/

                // Play background music
                playBackgroundMusic();

                button.prop('disabled', true);
                callBtn.prop('disabled', true);

                setTimeout(function () {
                    button.prop('disabled', false);
                    callBtn.prop('disabled', false);
                }, 2000);

                // Enable button after 1 second
                setTimeout(function () {
                    // Speak the category and queue number
                    var getQ = getCategoryLetter(queue.categoryId);
                    var queueNumber = getQ +"-- " + queue.queueNumber;
                   
                    var clerk = document.getElementById("ClerkNum").textContent;
                    var speechText = queueNumber + " Please Proceed to " + clerk;
                    speak(speechText);
                }, 1000);
            }
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });
}

// Function to Call the queue number
function CallQueue() {
    var button = $(this);
    $.ajax({
        type: 'GET',
        url: "/Clerk/GetServings",
        dataType: "json",
        success: function (response) {
            if (response && response.isSuccess == true) {
                var servingDisplay = document.getElementById("servingDisplay");
                // Play background music
                playBackgroundMusic();

                servingDisplay.classList.add("blink-red");
                setTimeout(function () {
                    servingDisplay.classList.remove("blink-red");
                }, 3000);
                button.prop('disabled', true);
                setTimeout(function () {
                    button.prop('disabled', false);
                    var getQ = getCategoryLetter(response.categoryId);
                    var queue = getQ + "-- " + response.queueNumber;

                    var clerk = document.getElementById("ClerkNum").textContent;
                    var speechText = queue + " Please Proceed to " + clerk
                    speak(speechText);
                }, 1000);


            } else {
                //alert("There is no Queue Number.");
                button.prop('disabled', true);
                setTimeout(function () {
                    button.prop('disabled', false);
                }, 1000);
            }
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });
};

//CANCEL QUEUE
function CancelQueue() {
    var button = $(this);
    var servingDisplay = document.getElementById("servingDisplay");
    $.ajax({
        type: 'GET',
        url: "/Clerk/CancelQueue",
        dataType: "json",
        success: function (response) {
            if (response && response.isSuccess == true) {
                servingDisplay.innerText = "----";

                ////Clear the localstorage
                //localStorage.removeItem('queueItem');

                button.prop('disabled', true);
                setTimeout(function () {
                    button.prop('disabled', false);
                }, 1000)
            }
            else {
                //alert(response.message)
                button.prop('disabled', true);
                setTimeout(function () {
                    button.prop('disabled', false);
                }, 1000)
            }
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });
};

//RESERVE QUEUE
function ReserveQueue() {
    var button = $(this);
    var servingDisplay = document.getElementById("servingDisplay");

    $.ajax({
        type: 'GET',
        url: "/Clerk/ReserveQueue",
        dataType: "json",
        success: function (response) {
            if (response) {
                servingDisplay.innerText = "----";
                ////Clear the localstorage
                //localStorage.removeItem('queueItem');
                button.prop('disabled', true);
                setTimeout(function () {
                    button.prop('disabled', false);
                }, 1000)
            } else {
                alert(response.message);
            }
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });
};
function formatDate(date) {
    var options = { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit', second: '2-digit' };
    return date.toLocaleString('en-US', options).toUpperCase();
}

//Load Reserve Table
function GetAllReserveQueue() {
    $.ajax({
        type: 'GET',
        url: "/Clerk/GetReservedQueues",
        dataType: "json",
        success: function (response) {
            var reserveQ = response.obj;
            reserveQ.sort(function (a, b) {
                return new Date(a.reserv_At) - new Date(b.reserv_At);
            });
            var tbody = $("#resQueues");
            tbody.empty();

            if (reserveQ.length > 0) {
                var tableRows = reserveQ.map(function (queue) {
                    return createTableReserve(queue);
                });
                tbody.append(tableRows);
            } else {
                var noReserveRow = $("<tr>").append($("<td colspan='4'>").text("No reservations yet"));
                tbody.append(noReserveRow);
            }
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });

}
//Function Create Table
function createTableReserve(queue) {
    var queueNumber = $("<th>").addClass("h2 align-middle").text(queue.category + "-" + queue.qNumber);

    var reserve_At = new Date(queue.reserv_At);
    var formattedDateTime = formatDate(reserve_At);

    var reserveAtCell = $("<td>")
        .addClass("text-center align-middle")
        .text(formattedDateTime);

    var stageClass = queue.stageId === 1 ? 'text-info' : queue.stageId === 2 ? 'text-danger' : '';

    var stageCell = $("<td>").addClass("h5 align-middle " + stageClass).text(
        queue.stageId === 1 ? "For Filling Up" : queue.stageId === 2 ? "For Releasing" : ""
    );

    var actionCell = $("<td>");

    //Button
    var serveButton = $("<button>")
        .addClass("btn serveReserveQueueBtn queueBtn-" + queue.category)
        .attr("data-genDate", queue.generateDate)
        .attr("data-category", queue.categId)
        .attr("data-qnumber", queue.qNumber)
        .text("Serve");
    var cancelButton = $("<button>")
        .addClass("btn btn-secondary cancelReserveQueueBtn")
        .attr("data-genDate", queue.generateDate)
        .attr("data-category", queue.categId)
        .attr("data-qnumber", queue.qNumber)
        .text("Cancel");
        

    actionCell.addClass("tableAction d-flex justify-content-center gap-3");
    actionCell.append(serveButton, cancelButton);

    var tableRow = $("<tr>").append(queueNumber, reserveAtCell, stageCell, actionCell);
    return tableRow;
}

//COUNT All on Each Category and Load All Buttons
function GetAllQueueCountNumbers(id) {
    $.ajax({
        type: 'GET',
        url: "/Clerk/GetCategories",
        dataType: "json",
        success: function (response) {
            var container = $('#WaitingCountNumber');
            container.empty();
            if (response && response.isSuccess == true && response.obj.length > 0) {
                response.obj.forEach(function (item) {
                    var categoryId = item.categoryId;
                    var categoryCount = item.queueCount;
                    var category = getCategoryLetter(categoryId);

                    var button = $('<button>')
                        .addClass('btn nextBtn')
                        .addClass('category-' + category)
                        .attr('id', categoryId)
                        .attr('disabled', id === categoryId)
                        .append(
                            $('<h1>')
                                .addClass('categoryBtn m-0')
                                .attr('id', 'countQ' + category)
                                .text(category + ' - ' + categoryCount)
                        );

                    // Disable the button
                    if (id === categoryId) {
                        button.prop('disabled', true);
                        setTimeout(() => {
                            button.prop('disabled', false);
                        }, 1000);
                    }

                    var div = $('<div>')
                        .append(
                            $('<h5>')
                                .addClass('categoryName text-center')
                                .text('Category ' + category)
                        )
                        .append(button);

                    container.append(div);
                });
            } else {
                container.append('<p>There are no category assign yet.</p>');
            }
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });
}

//Load Filling Up Table
function GetAllFillingQueue() {
    $.ajax({
        type: 'GET',
        url: "/Clerk/GetAllFillingUpQueues",
        dataType: "json",
        success: function (response) {
            var fillingQ = response.obj;
            fillingQ.sort(function (a, b) {
                return new Date(a.filling_At) - new Date(b.filling_At);
            });
            var tbody = $("#fillingQueues");
            tbody.empty();

            if (fillingQ.length > 0) {
                var tableRows = fillingQ.map(function (queue) {
                    return createTableFilling(queue);
                });
                tbody.append(tableRows);
            } else {
                var noFillingRow = $("<tr>").append($("<td colspan='2'>").text("No filling up queues yet."));
                tbody.append(noFillingRow);
            }
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });
}
//Function Create Table for Filling Up
function createTableFilling(queue) {
    var queueNumber = $("<th>").addClass("h4 align-middle w-50").text(queue.category + "-" + queue.qNumber);
    var actionCell = $("<td>");

    //Button
    var toReleaseButton = $("<button>")
        .addClass("btn btn-primary toReleasingBtn")
        .attr("data-genDate", queue.generateDate)
        .attr("data-category", queue.categId)
        .attr("data-qnumber", queue.qNumber)
        .text("Releasing");
    var cancelButton = $("<button>")
        .addClass("btn btn-sm btn-secondary cancelFillingBtn")
        .attr("data-genDate", queue.generateDate)
        .attr("data-category", queue.categId)
        .attr("data-qnumber", queue.qNumber)
        .text("Cancel");


    actionCell.addClass("tableAction d-flex justify-content-center gap-3");
    actionCell.append(toReleaseButton, cancelButton);

    var tableRow = $("<tr>").append(queueNumber, actionCell);
    return tableRow;
}

//Load Filling Up Table
function GetAllRleasingQueue() {
    $.ajax({
        type: 'GET',
        url: "/Clerk/GetAllReleasingQueues",
        dataType: "json",
        success: function (response) {
            var releasingQ = response.obj;
            releasingQ.sort(function (a, b) {
                return new Date(a.releasing_At) - new Date(b.releasing_At);
            });
            var tbody = $("#releasingQueues");
            tbody.empty();

            if (releasingQ.length > 0) {
                // First row
                var firstRow = createTableReleasing(releasingQ[0], 0);
                tbody.append(firstRow);

                // Subsequent rows
                var otherRows = releasingQ.slice(1).map((q, index) => createTableReleasing(q, index + 1));
                tbody.append(otherRows);
            } else {
                var noReleasingRow = $("<tr>").append($("<td colspan='2'>").text("No releasing queues yet."));
                tbody.append(noReleasingRow);
            }
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });
}

//Function Create Table for Filling Up
function createTableReleasing(queue, rowIndex) {
    var queueNumber = $("<th>").addClass("h4 align-middle w-50").text(queue.category + "-" + queue.qNumber);
    var actionCell = $("<td>");

    if (rowIndex === 0) {
        //Button
        var serveReleaseButton = $("<button>")
            .addClass("btn btn-success serveReleasingBtn")
            .attr("data-genDate", queue.generateDate)
            .attr("data-category", queue.categId)
            .attr("data-qnumber", queue.qNumber)
            .text("Serve");

        serveReleaseButton.prop('disabled', true);
        setTimeout(() => {
            serveReleaseButton.prop('disabled', false);
        }, 500);

        actionCell.addClass("tableAction d-flex justify-content-center gap-3");
        actionCell.prepend(serveReleaseButton);
    } else {
        actionCell.addClass("tableAction");
    }

    var tableRow = $("<tr>").append(queueNumber, actionCell);
    return tableRow;
}

//Transfer the Queue From Filling Up Table Table to Releasing Table
function ToReleasingQueue() {
    var generateDate = $(this).data("gendate");
    var categoryId = $(this).data("category");
    var qNumber = $(this).data("qnumber");
    $.ajax({
        type: 'GET',
        url: "/Clerk/FillingToReleaseQueue",
        data: {
            generateDate: generateDate,
            categoryId: categoryId,
            qNumber: qNumber,
        },
        dataType: "json",
        success: function (response) {
            if (response && response.isSuccess == true) {
                //
            } else {
                alert(response.message)
            }

        }, error: function (error) {

        }
    });

}


//Speak
function speak(text) {
    const synth = window.speechSynthesis;
    const utterance = new SpeechSynthesisUtterance(text);

    synth.speak(utterance);
}

// Play background music
function playBackgroundMusic() {
    const audio = new Audio('/src/audio/ascend.mp3');
    audio.play();
}

///////////////////////////////

//SERVE =>  RESERVE QUEUE
function ServeQueueInTable() {
    var generateDate = $(this).data("gendate");
    var categoryId = $(this).data("category");
    var qNumber = $(this).data("qnumber");
    var callBtn = $('#callBtn');

    //var prevQueue = JSON.parse(localStorage.getItem('queueItem'));

    $.ajax({
        type: 'GET',
        url: "/Clerk/ServeQueueFromTable",
        data: {
            generateDate: generateDate,
            categoryId: categoryId,
            qNumber: qNumber,
/*            prevQueue: JSON.stringify(prevQueue),*/
        },
        dataType: "json",
        success: function (response) {
            var queueItem = response.obj;
            if (response && response.isSuccess == true) {
                //// Store the Queue Object into local storage
                //localStorage.setItem('queueItem', JSON.stringify(queueItem));

                // Play background music
                playBackgroundMusic();

                //Display the Current Serve
                DisplayCurrentServe();
                callBtn.prop('disabled', true);
                // Execute after 1 second
                setTimeout(function () {
                    callBtn.prop('disabled', false);

                    // Speak the category and queue number
                    var getQ = getCategoryLetter(queueItem.categoryId);
                    var queue = getQ + "-- " + queueItem.queueNumber;

                    const clerkNumber = document.getElementById("ClerkNum").textContent;
                    var speechText = queue + " Please Proceed to " + clerkNumber
                    speak(speechText);
                }, 1000);
            }
            else {
                alert(response.message);
            }
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });

};

//CANCEL QUEUE
function CancelQueueFromTable() {
    var id = $(this).attr('id');
    var generateDate = $(this).data("gendate");
    var categoryId = $(this).data("category");
    var qNumber = $(this).data("qnumber");

    $.ajax({
        type: 'GET',
        url: "/Clerk/CancelQueueNumber",
        data: {
            generateDate: generateDate,
            categoryId: categoryId,
            qNumber: qNumber,
        },
        dataType: "json",
        success: function (response) {
            if (response) {
                // 
            } else {
               //
            }
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });
};

//Helper
function getCategoryLetter(categoryId) {
    switch (categoryId) {
        case 1:
            return 'A';
        case 2:
            return 'B';
        case 3:
            return 'C';
        case 4:
            return 'D';
        default:
            return '';
    }
}


//Display Designated Clerk
function GetClerkNumber() {
    $.ajax({
        url: "/Clerk/DesignatedDeviceId",
        type: 'GET',
        success: function (response) {
            //console.log(response)
            var clerk = response.device;
            $('#ClerkNum').text(clerk.clerkNumber);    
        },
        error: function (error) {
            console.log(error);
        }
    });
}

