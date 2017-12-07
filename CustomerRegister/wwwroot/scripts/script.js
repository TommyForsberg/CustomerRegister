$("#addCustomer").click(function () {

    $.ajax({
        url: '/api/Customers',
        method: 'POST',
        data: {
            "FirstName": $("#customerForm [name=FirstName]").val(),
            "LastName": $("#customerForm [name=LastName]").val(),
            "Email": $("#customerForm [name=Email]").val(),
            "Gender": $("#customerForm [name=Gender]").val(),
            "Age": $("#customerForm [name=Age]").val(),
        }

    })
        .done(function (result) {
            console.log(result);
            //appendTable(result);

        })

        .fail(function (xhr, status, error) {

            alert("Fail");
            console.log("Error", xhr, status, error);

        })
});