
function UserLogin() {
    $(document).ready(function () {

        var form = $("#Login");

            if (VerifyData(form)) {

                var button = $("#btnlogin");
                button.text("Iniciando..");
                button.attr("disabled", true);

                var values = form.serializeArray();
                console.log(values);

                $.ajax({
                    method: "POST",
                    url: "/Users/Login",
                    data: values,
                    success: function (callback, status) {

                        if (callback.Error) {
                            $("#btnlogin").text("iniciar Sesión").attr("disabled", false);
                            $("#btnlogin").after("<label class='text-danger'><strong>!El Usuario O Contraseña Son Incorrectos</strong></label>");
                            $("#login-card").effect("shake");
                        }
                        else {
                            window.location.href = "/home/Index";
                        }
                    }

                });
            }
    });

}

function VerifyData(form) {

    var data = form.serializeArray();

    $(data).each(function (x) {


    });

    return true;
}