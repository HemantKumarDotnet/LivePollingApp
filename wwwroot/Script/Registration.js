$(document).ready(function () {
    $('#SignUp').click(function (e) {
        Registration(e);
    });
});

toastr.options = {
    "closeButton": true,
    "progressBar": true,
    "positionClass": "toast-top-center",
    "timeOut": "5000"
};

function Registration(event) {
    try {
        var formData = {
            RoleId: parseInt($('#ddlRole').val())||0,
            Role: $('#ddlRole option:selected').text(),
            Username: $('#txtUserName').val(),
            Email: $('#txtEmail').val(),
            Password: $('#txtPassword').val(),
            ConfirmPassword: $('#txtConfirmPassword').val(),
        };
        $.ajax({
            type: "POST",
            url: "/Home/UserRegistration",
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.success) {
                    toastr.success(response.message || "Registered successfully!", "Success");
                    Reset();
                    setTimeout(function () {
                        window.location.href = "/Home/Login";
                    }, 5000);
                }
                else
                    toastr.error(response.message || "Something went wrong!", "Error");
            },
            error: function (xhr) {
                let errorMsg = "";
                try {
                    var response = JSON.parse(xhr.responseText);
                    errorMsg += response.message;
                } catch (e) {
                    errorMsg += xhr.statusText;
                }
                toastr.error(errorMsg, "Error");
            }
        });
    } catch (ex) {
        console.error("Error in Registration :", ex);
    }
}

function Reset() {
    $("#ddlRole").val("0");
    $("#txtUserName").val("");
    $("#txtEmail").val("");
    $("#txtPassword").val("");
    $("#txtConfirmPassword").val("");
}