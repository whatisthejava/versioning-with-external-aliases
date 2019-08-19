function writeX(url, version, addHeader, delegatetogetbody) {
    $.ajax({
        type: "POST",
        url: url,
        beforeSend: function (request) {
            if (addHeader === true) {
                request.setRequestHeader("api-version", version);
            }
        },
        data: delegatetogetbody(),
        contentType: "application/json",
        dataType: "json",
        success: function () {

            var results = $("#result");

            results.empty();
            results.append("Done");

        },
        error: function (response) {
            var results = $("#result");

            if (response.status !== 200) {
                results.empty();
                results.append("No matching record found");

            } else {
                results.empty();
                results.append("Done");
            }
        },
        failure: function (response) {
            alert(response);
        }
    })
}
function get(url, version, addHeader, idOfElement) {
    $.ajax({
        type: "GET",
        url: url + "/" + $(idOfElement).val(),
        beforeSend: function (request) {
            if (addHeader === true) {
                request.setRequestHeader("api-version", version);
            }
        },
        contentType: "application/json",
        dataType: "json",
        success: function (response) {
            var results = $("#result");

            results.empty();
            var jsonString = JSON.stringify(response, null, 2);
            results.append(jsonString);

        },
        error: function (response) {
            var results = $("#result");
            results.empty();
            results.append("No matching record found");
        },
        failure: function (response) {
            alert(response);
        }
    })
}

function getAll(url, version, addHeader) {
    $.ajax({
        type: "GET",
        url: url,
        beforeSend: function (request) {
            if (addHeader === true) {
                request.setRequestHeader("api-version", version);
            }
        },
        contentType: "application/json",
        dataType: "json",
        success: function (response) {
            var results = $("#result");
            results.empty();
            $.each(response, function (i, item) {
                var jsonString = JSON.stringify(item, null, 2);
                results.append(jsonString);
                results.append($('<br>'));
            });
        },
        error: function (response) {
            var results = $("#result");
            results.empty();
            results.append("No matching record found");
        },
        failure: function (response) {
            alert(response);
        }
    })
}