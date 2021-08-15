
var toggleBtn = document.getElementsByClassName("live-feed-toggle")[0];
var liveFeed = document.getElementsByClassName("live-feed-wrapper")[0];
var toggleBtnIcon = document.getElementById("live-feed-toggle-icon");

toggleBtn.addEventListener("click",
    function () {
        if (liveFeed.classList.contains("live-feed-hide")) {
            liveFeed.classList.remove("live-feed-hide");
            liveFeed.classList.add("live-feed-show");
            toggleBtnIcon.className = "fas fa-chevron-circle-left live-feed-toggle";
        }
        else {
            liveFeed.classList.remove("live-feed-show");
            liveFeed.classList.add("live-feed-hide");
            toggleBtnIcon.className = "fas fa-chevron-circle-right live-feed-toggle";
        }
    });
