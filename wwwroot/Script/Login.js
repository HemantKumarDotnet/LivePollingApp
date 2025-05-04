$(document).ready(function () {
    $('#btnSignin').click(function (e) {
        UserLogin(e);
    });
});

toastr.options = {
    "closeButton": true,
    "progressBar": true,
    "positionClass": "toast-top-right",
    //"positionClass": "toast-top-full-width",
    "timeOut": "5000"
};


function UserLogin(event) {
    try {
        var formData = {
            RoleId: parseInt($('#ddlRole').val()) || 0,
            Username: $('#txtUserName').val(),
            Password: $('#txtPassword').val(),
        };
        $.ajax({
            type: "POST",
            url: "/Home/UserLogin",
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.success) {
                    toastr.success(response.message || "Logged in successfully!", "Success");
                    setTimeout(function () {
                        /*window.location.href = "/Home/Index";*/
                        window.location.href = "/Polling/Index";
                    }, 5000);
                }
                else
                    toastr.error(response.message || "Invalid Credential.!!!", "Error");
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
        console.error("Error in UserLogin :", ex);
    }
}