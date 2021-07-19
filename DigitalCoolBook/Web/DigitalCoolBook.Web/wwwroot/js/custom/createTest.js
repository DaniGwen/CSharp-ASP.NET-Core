
// Adding question fields on number input
$(function () {
    $('#numberOfQuestions').change(function () {
        // remove fields before create new ones
        $('#questions').empty();
        $('#validateInput').empty();

        var numberOfQuestions = $('#numberOfQuestions').val();

        // Input field for each question with its label and alert span
        for (var i = 0; i < numberOfQuestions; i++) {
            $('#questions').append(
                $('<label >Question ' + (i + 1) + '<label/>'),
                $('<input class="mt-2 form-control" id="question' + i + '" name="[' + i + '].Question" placeholder="Enter question.."/>'),
                $('<span class="text-danger" id="alertQuestion' + i + '"></span>'),
                $('<br/>'),
                $('<label class="form-group">Number of answers<label/>'),
                // Input number for answers and call AppendFields()
                $('<input class="form-control light-green" onchange="AppendFields(this,' + i + ')" type="number" max="7" min="0" value="0" id="numberOfAnswers" name="answersCount' + i + '"/>'),
                $('<span class="text-danger" id="validateNumberOfAnswers">'),
                $('<div class="form-group" id="answerInput' + i + '"></div>'),
                $('<hr class="hr-orchid w-100"/>')
            );
        }
    });
});

// Append answers fields on answersCount change
function AppendFields(answerCountValue, indexQuestion) {

    var currentQuestionInput = $('#answerInput' + indexQuestion + '');

    var index = $(answerCountValue).val();

    var answersLetters = ['A', 'B', 'C', 'D', 'E', 'F', 'G'];

    $(currentQuestionInput).empty();

    // Print answer input fields
    for (var i = 0; i < index; i++) {
        $(currentQuestionInput).append(
            $('<label>Answer ' + answersLetters[i] + '<label/>'),
            $('<input class="form-control" id="answer' + i + '" name="[' + indexQuestion + '].Answers" placeholder="Enter answer.."/>'),
            $('<span class="text-danger" id="alertAnswer' + i + '"></span>')
        );
    }
}

// Input validation for questions and answers and Submit form
$(function () {
    $('#save').click(function (event) {
        // Alert variables
        var alertNumberOfQuestions = $('#validateInput');
        var alertNumberOfAnswers = $('#validateNumberOfAnswers');
        var alertPlace = $('#alertPlace');
        var alertLesson = $('#alertLesson');


        //empty alert messages
        alertNumberOfQuestions.empty();
        alertNumberOfAnswers.empty();
        alertPlace.empty();
        alertLesson.empty();

        // Error if questions or answers less than zero
        var numberOfQuestions = $('#numberOfQuestions').val();
        var numberOfAnswers = $('#numberOfAnswers').val();

        var errorCount = 0;

        if ($("#inputPlace").val() === '') {
            alertPlace.html('Please enter location').fadeToggle(2000);
            errorCount += 1;
        }

        if ($("#LessonId").val() === "Choose topic") {
            alertLesson.html('Please select topic').fadeToggle(2000);
            errorCount += 1;
        }

        if (numberOfQuestions <= 0) {
            alertNumberOfQuestions.html("Please add questions").fadeToggle(2000);
            errorCount += 1;
        }

        if (numberOfAnswers <= 0) {
            alertNumberOfAnswers.html("Please add answers").fadeToggle(2000);
            errorCount += 1;
        }

        for (var i = 0; i < numberOfQuestions; i++) {
            if ($('#question' + i).val() === '') {
                $('#alertQuestion' + i).html('Fill the question').fadeToggle(2000);
                errorCount += 1;
            } else {
                $('#alertQuestion' + i).empty();
            }

            for (var j = 0; j < numberOfAnswers; j++) {
                if ($('#answer' + j + '').val() === '') {
                    $('#alertAnswer' + j + '').html('Fill the answer').fadeToggle(2000);
                    errorCount += 1;
                } else {
                    $('#alertAnswer' + i).empty();
                }
            }
        }

        // Submit if no errors
        if (errorCount > 0) {
            event.preventDefault();
        }
    });
});