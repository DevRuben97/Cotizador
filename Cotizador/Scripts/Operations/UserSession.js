
function UserLogin() {
    $(document).ready(function () {

        var form = $("#Login");

            if (VerifyData(form)) {

                var button = $("#btnlogin");
                button.text("Verificando..");
                button.attr("disabled", true);

                var values = form.serializeArray();

                $.ajax({
                    method: "POST",
                    url: "/Users/Login",
                    data: values,
                    success: function (callback) {

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
    var validate = false;
    if (data[0]["value"] == "") {
        $("span[name='" + data[0]["name"] + "']").text("Este Campo es Requrido");
        validate = false;
    }
    else if (data[1]["value"] == "") {
        $("span[name='" + data[1]["name"] + "']").text("Este Campo es Requrido");
        validate = false;
    }
    else {
        validate = true;
    }
    
    return validate;
}
//Eliminar las etequitas de error:

$(document).ready(function () {

    $("#Usuario").focus(function () {

        $("span[name='Usuario']").text("");
    });
    $("#Clave").focus(function () {
        $("span[name='Clave']").text("");
    }).keypress(function (key) {
        if (key.keyCode == 13) {
            UserLogin();
        }
    })
})