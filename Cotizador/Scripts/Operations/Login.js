

function Login() {
    $(document).ready(function () {

        $("#btnlogin").on("click", function () {

            var form = $("#Login");

            if (VerifyData(form)) {

                $("#btnlogin").text("Espere..").attr("disabled", true);

                var Usuario = $("#usuario").val();
                var clave = $("#clave").val();
                var remeb = $("#remenber").val();

                var data = { usuario: Usuario, clave: clave, remenber: remeb };
                $.post("/Users/Login/", data, function (backdata, status) {

                    if (backdata.Error) {
                        $("#btnlogin").text("Iniciar Sesión").attr("disabled", false);
                        $(".text-danger").text("<i class='fas fa-exclamation-circle'></i>");
                        $("#ErrorText").text("Usuario O Contrseña Incorrectos");
                        
                    }

                });
            }
        })

    });

}

function VerifyData(form) {

}