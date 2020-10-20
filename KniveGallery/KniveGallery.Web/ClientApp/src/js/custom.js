$(function openModal(e) {
  debugger;
  var img = $(e).attr('src');
  $('.knive-details__image-enlarge').addClass('active');
  $('.knive-details__image-enlarge__image').attr('src', img);
  $('.knive-details__image-enlarge__close').on('click', function () {
    $('.knive-details__image-enlarge').removeClass('active');
  })
});
