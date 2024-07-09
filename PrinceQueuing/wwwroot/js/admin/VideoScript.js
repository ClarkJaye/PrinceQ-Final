$(document).ready(function () {
    GetAllVideos();
});

function GetAllVideos() {
    $.ajax({
        type: 'GET',
        url: '/Admin/AllVideos',
        dataType: 'json',
        success: function (response) {
            console.log(response);
            if (response.length > 0) {
                let videoContainer = '';
                response.forEach(function (videoFile, index) {
                    let fileName = videoFile.split('/').pop();
                    videoContainer += `
                        <div class="video-player videoInList mb-2" data-video-src="${videoFile}">
                            <video id="main-video-${index}" width="940" style="width:100%; height:auto" src="${videoFile}" data-video-id="${fileName}" muted></video>
                        </div>
                    `;
                });
                $('#listVidsContainer').html(videoContainer);

                // Add click event handler to each video element
                $('.videoInList').click(function () {
                    let videoSrc = $(this).data('video-src');
                    $('#main-video').attr('src', videoSrc);
                });
            } else {
                $('#listVidsContainer').html('<h4>No Video</h4>');
            }
        },
        error: function (error) {
            console.log("Unable to get the data - " + error);
        }
    });
}