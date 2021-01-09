﻿$(document).ready(function () {
    $('#savedRepliesButton').click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        $('#savedRepliesSpan').toggleClass("show");
        $('#themeSpan').removeClass("show");
        $('#popupEmoji').removeClass("show");
        $('#chatStickerSpan').removeClass("show");
    });

    $('#savedRepliesSpan').click(function (e) {
        e.stopPropagation();
    });

    $('body').click(function () {
        $('#savedRepliesSpan').removeClass("show");
    });
});

function pasteQuickReply(element) {
    document.getElementById("messageInput").innerHTML += element.innerHTML;
}

function addQuickReply() {
    let quickReplyText = document.getElementById('messageInput').innerHTML;
    alert(quickReplyText);
}