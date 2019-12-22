let currentUsername = null;

function renderMessages(data) {
    $('#messages').empty();

    for (let message of data) {
        $('#messages').append(
            '<div class="message d-flex justify-content-start"><strong>'
            + message.user
            + '</strong>: '
            + message.contend
            + '!</div>'
        );
    }
}

function loadMessages() {
    $.get({
        url: appUrl + "messages/all",
        success: function success(data) {
            renderMessages(data);
        },
        error: function error(error) {
            console.log(error);
        }
    });
}

function createMessage() {
    let username = currentUsername;
    let message = $('#message').val();

    if (username == null) {
        alert('Choose username first!');
        return;
    };

    if (message.length == 0) {
        alert('Please enter message.');
        return;
    };

    $.post({
        url: appUrl + 'messages/create',
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({
            content: message, username: username
        }),
        success: function success(data) {
            loadMessages();
        },
        error: function error(error) {
            console.log(error);
        }
    });
}


$('#reset-data').hide();
loadMessages();
