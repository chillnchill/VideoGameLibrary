$(document).ready(function () {

    $('#collectionsDropdown').hover(function () {
        $(this).addClass('show');
        $(this).find('.dropdown-menu').addClass('show');
    }, function () {
        $(this).removeClass('show');
        $(this).find('.dropdown-menu').removeClass('show');
    });

    $('.dropdown-menu').mouseleave(function () {
        $(this).removeClass('show');
        $(this).parent().removeClass('show');
    });
});