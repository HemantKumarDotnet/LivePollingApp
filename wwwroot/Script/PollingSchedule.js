$(document).ready(function () {
    $('#btnSubmit').click(function (e) {
        SubmitPollingDate(e);
    });
});
toastr.options = {
    "closeButton": true,
    "progressBar": true,
    "positionClass": "toast-top-center",
    "timeOut": "5000"
};

function SubmitPollingDate(event) {
    try {
        var formData = {
            PollingDate: $('#txtPollingDate').val() || "",
            IsActive: $('#chkActive').is(':checked')
        };
        $.ajax({
            type: "POST",
            url: "/Polling/SubmitPollingDate",
            data: JSON.stringify(formData),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.success) {
                    toastr.success(response.message || "Polling Date saved successfully!", "Success");
                    Reset();
                    
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
        console.error("Error in SubmitPollingDate :", ex);
    }
}

function Reset() {
    $("#txtPollingDate").val("");
    $('#chkActive').attr("checked", true);
}