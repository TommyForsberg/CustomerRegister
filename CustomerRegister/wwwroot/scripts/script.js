//Onload. 
$(function () {
    $("#updateCustomer").removeAttr('disabled');
    $.ajax({
        url: '/api/Customers',
        method: 'GET'

    })
        .done(function (result) {
            appendTable(result);
        })

        .fail(function (xhr, status, error) {

            alert(`Fail!`)
            console.log("Error", xhr, status, error);

        })
});

//For posting customer to database
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
             $("#customerForm [name=FirstName]").val('');
            $("#customerForm [name=LastName]").val('');
            $("#customerForm [name=Email]").val('');
             $("#customerForm [name=Gender]").val('');
             $("#customerForm [name=Age]").val('');
        
            console.log(result);
            //appendTable(result);

        })

        .fail(function (xhr, status, error) {

            alert("Fail");
            console.log("Error", xhr, status, error);

        })
});

function appendTable(result) {
    $("#customersTable").html('<tr><th>ID</th><th>Förnamn</th><th>Efternamn</th><th>Email</th><th>Kön</th><th>Ålder</th><th></th></tr>');
    $.each(result, function (i, item) {

        $("#customersTable").append(

            '<tr><td>' + item.id + '</td><td>' + item.firstName + '</td>' +
            '<td>' + item.lastName + '</td>' +
            '<td>' + item.email + '</td>' +
            '<td>' + item.gender + '</td>' +
            '<td>' + item.age + '</td>' +
            '<td><button class="removeCustomer btn btn-danger" id=' + item.id + '>Ta bort</button></td>' +
            '<td><button class="editCustomer btn btn-primary" id=' + item.id + '>Redigera</button></td></tr>'
        );
        console.log("Success!", item)
    });
}