//Onload. 
$(function () {
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
            clearCustomerForm();
            appendTable(result);
        })

        .fail(function (xhr, status, error) {

            alert("Fail");
            console.log("Error", xhr, status, error);

        })
});
//Get customer to edit.
$(document).on('click', '.editCustomer', function () {
    
    $.ajax({
        url: '/api/Customers/GetCustomer',
        method: 'GET',
        data: {
            "Id": $(this).attr("id"),
        }
    })
        .done(function (result) {
            editCustomer(result);
        })

        .fail(function (xhr, status, error) {

            alert("Fail");
            console.log("Error", xhr, status, error);

        })

});

$(document).on('click', '.updateCustomer', function () {
   
    $.ajax({
        url: '/api/customers',
        method: 'PUT',
        data: {
            "Id": $("#customerForm [name=Id]").val(),
            "FirstName": $("#customerForm [name=FirstName]").val(),
            "LastName": $("#customerForm [name=LastName]").val(),
            "Email": $("#customerForm [name=Email]").val(),
            "Gender": $("#customerForm [name=Gender]").val(),
            "Age": $("#customerForm [name=Age]").val(),
          
        }
    })
        .done(function (result) {
            clearCustomerForm();
            $("#updateCustomer")
                .attr('disabled','disabled'); 
            appendTable(result);
        })
        .fail(function (xhr, status, error) {

            alert("Fail");
            console.log("Error", xhr, status, error);

        })

});

$(document).on('click', '.removeCustomer', function () {
    $.ajax({
        url: '/api/Customers',
        method: 'DELETE',
        data: {
            "Id": $(this).attr("id"),
        }
    })
        .done(function (result) {
            $("#customersTable").html('');
            appendTable(result);
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
    });
}
function editCustomer(customer) {
    console.log(customer);
    $("#customerForm [name=FirstName]").val(customer.firstName);
    $("#customerForm [name=LastName]").val(customer.lastName);
    $("#customerForm [name=Email]").val(customer.email);
    $("#customerForm [name=Gender]").val(customer.gender);
    $("#customerForm [name=Age]").val(customer.age);
    $("#customerForm [name=Id]").val(customer.id);
    
    $("#updateCustomer")
        .removeAttr('disabled');   
}

function clearCustomerForm() {
    $("#customerForm [name=FirstName]").val('');
    $("#customerForm [name=LastName]").val('');
    $("#customerForm [name=Email]").val('');
    $("#customerForm [name=Gender]").val('Male');
    $("#customerForm [name=Age]").val('');
    $("#customerForm [name=Id]").val('');
}