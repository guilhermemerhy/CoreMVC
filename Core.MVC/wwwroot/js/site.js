var employee = {

    init: () => {
        $("#Birth").mask("99/99/9999");
        employee.fClone();
    },

    fSave: () => {

        var dependent = [];

        $("input[name='Dependent']").each(function () {
            if ($(this).val() != "") {
                dependent.push({
                    Name: $(this).val(),
                    Id: $(this).attr("id")
                });
            }
        });

        let obj = {
            Id: $("#Id").val(),
            Name: $("#Name").val(),
            Email: $("#Email").val(),
            Birth: $("#Birth").val(),
            Role: $("#Role").val(),
            Genre: $("input:radio[name='genre']:checked").val() == 1 ? true : false,
            Dependent: dependent
        };


        if (!employee.fValidate(obj))
            return;

        $.ajax({
            url: window.location.origin + "/Employee/CreateOrUpdate",
            type: 'POST',
            contentType: "application/json",
            data: JSON.stringify(obj) 
        }).done(function (resp) {
            resp = JSON.parse(resp);

            if (resp.sucess)
                window.location.href = window.location.origin + "/Employee/Index";
            else {
                var msg = "";
                $(resp.erros).each(function () {
                    msg += this + "<br/>";
                });
                employee.fErrorMensage(msg);
            }

        }).fail(function (err) {
            employee.fErrorMensage("Ocorreu um erro ao processar a solicitação");
        });
    },

    fClone: () => {
        $(".btn-adicionar-campo").unbind("click").click(function () {

            let divClone = $(".btn-campo-multiplo.adicionar-linha .row:last").clone();
            $(".btn-campo-multiplo.adicionar-linha").append(divClone);
            $(".btn-campo-multiplo.adicionar-linha .row:last input").val('');

            if ($(".btn-campo-multiplo.adicionar-linha .row").length > 1) {
                $(".btn-campo-multiplo.adicionar-linha .btn-adicionar-campo").removeClass("btn-adicionar-campo").addClass("btn-remover-campo");
            }
            $(".btn-campo-multiplo.adicionar-linha .row:first .btn-remover-campo").removeClass("btn-remover-campo").addClass("btn-adicionar-campo");
            $(".btn-campo-multiplo.adicionar-linha .row:last input:first").focus();
            employee.fClone();
        });


        $(".btn-campo-multiplo.adicionar-linha .row .btn-remover-campo").click(function () {
            $(this).parent().parent().remove();
        });
    },

    fValidate: (obj) => {

        var valide = true;
        var msg = "";
        if (!employee.isValidDate(obj.Birth)) {
            msg += "- O campo Data de Nascimento está inválido<br/>";
            valide = false;
        }
        if (obj.Name == "") {
            msg += "- O campo nome é obrigatório <br/>";
            valide = false;
        }

        if (!employee.isValidEmail(obj.Email)) {
            msg += "- O campo Email está em um formato inválido <br/>";
            valide = false;
        }

        if (!valide) {
            employee.fErrorMensage(msg);
        }

        return valide;

    },

    isValidDate: (date) => {
        if (date == "")
            return true;

        let ardt = new Array;
        let ExpReg = new RegExp("(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}");
        ardt = date.split("/");
        erro = false;
        if (date.search(ExpReg) == -1) {
            erro = true;
        }
        else if (((ardt[1] == 4) || (ardt[1] == 6) || (ardt[1] == 9) || (ardt[1] == 11)) && (ardt[0] > 30))
            erro = true;
        else if (ardt[1] == 2) {
            if ((ardt[0] > 28) && ((ardt[2] % 4) != 0))
                erro = true;
            if ((ardt[0] > 29) && ((ardt[2] % 4) == 0))
                erro = true;
        }
        if (erro) {
            return false;
        }
        return true;

    },

    isValidEmail: (email) => {

        if (email == "")
            return true;

        let re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email.toLowerCase());
    },

    fErrorMensage: (html) => {
        $("#error").html(html).show();       
    }

}
