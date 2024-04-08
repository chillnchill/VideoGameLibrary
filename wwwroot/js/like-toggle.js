//$(document).ready(function () {
//    $(".like-button").click(function (event) {
//        event.preventDefault(); // Prevent default form submission

//        // Toggle heart icon
//        var icon = $(this).find("i");
//        icon.toggleClass("far fas"); // Toggle between empty and full heart icons

//        // Retrieve game ID from the button's data attribute
//        var gameId = $(this).data("game-id");

//        // Check if the game ID is valid
//        if (!gameId) {
//            console.error("Missing game ID");
//            return;
//        }

//        // Send Ajax request to like the game
//        $.post("/Game/Like", { gameId: gameId })
//            .done(function () {
//                // Handle success
//                console.log("Game liked successfully");
//                // You can display a success message here if needed
//            })
//            .fail(function () {
//                // Handle failure
//                console.error("Failed to like the game");
//                // You can display an error message here if needed
//            });
//    });
//});

$(document).ready(function () {
    $('.like-button').click(function (event) {
        var gameId = $(this).data('gameId');
        var likeIcon = $('#like-icon-' + gameId);

        event.preventDefault(); // Not strictly necessary here

        $.ajax({
            url: '/Game/Like',
            type: 'POST',
            data: { gameId: gameId },
            beforeSend: function () {
                likeIcon.removeClass('far fa-heart').addClass('fas fa-spin fa-heart'); // Add spinner while liking
            },
            success: function (data) {
                // Handle successful like action (optional)
                likeIcon.removeClass('fas fa-spin fa-heart').addClass('fas fa-heart'); // Update to full heart
            },
            error: function (jqXHR, textStatus, errorThrown) {
                // Handle errors (optional)
                likeIcon.removeClass('fas fa-spin fa-heart').addClass('far fa-heart'); // Revert to empty heart
            }
        });
    });
});