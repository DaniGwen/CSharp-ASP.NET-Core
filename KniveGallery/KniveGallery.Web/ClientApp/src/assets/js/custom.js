function openModal(e) {
  var img = $(e).attr('src');
  $('.knive-details__image-enlarge').addClass('active').effect("slide");
  $('.knive-details__image-enlarge__image').attr('src', img);
  $('.knive-details__image-enlarge__close').on('click', function () {
    $('.knive-details__image-enlarge').removeClass('active').effect("drop", { direction: "down" }, "slow");
    $('.knive-details__image-enlarge').clearQueue();
  });
};

document.ready = function () {
  var element = document.getElementById('homepage-video');
  element.muted = "muted";
}
