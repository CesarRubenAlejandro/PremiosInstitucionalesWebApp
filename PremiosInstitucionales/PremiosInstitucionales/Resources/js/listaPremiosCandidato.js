$(document).ready(function () {
    $(".descPremio").html(function () {
        var yourString = $(this).html(); //replace with your string.
        var maxLength = 155; // maximum number of characters to extract

        //trim the string to the maximum length
        var trimmedString = yourString.substr(0, maxLength);

        //re-trim if we are in the middle of a word
        return trimmedString.substr(0, Math.min(trimmedString.length, trimmedString.lastIndexOf(" ")))+"...";
    });
});