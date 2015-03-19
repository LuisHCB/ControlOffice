$(
    function ()
    {
        
        $("body").on("click", 'button', function ()
        {           
            //data-ajax="true"
            if ($(this).data('ajax') == undefined) return; // si no tiene le atributo usar-ajax no hace nada

            if ($(this).data('ajax') != true) return; // si no tiene le atributo usar-ajax no hace nada

            var form = $(this).closest("form"); //parentNode();//     
            var buttons = $("button", form);
            var button = $(this);
            var url = form.attr('action');
           
            //Pregunta si debe lanzar una confirmacion
            if (button.data('confirm') != undefined)
            {
                if (button.data('confirm') == '') {
                    if (!confirm("¿Esta seguro de realizar esta acción?")) return false;//regresa
                }
                else {
                    if (!confirm(button.data('confirm'))) return false;//regresa, alerta con el mensaje definido en confirm
                }
            }

            //verifica que el formulario sea valido
            if (!form.valid()) {
                return false;
            }


            // aqui crear un div que bloquee todo el formulario mientras se guarda la informacion

            //en caso de que exista un alerta

            $(".alert", form).remove();//se eleimina

            //parte de jquery form
            form.ajaxSubmit({
                dataType: 'JSON',
                type: 'POST',
                url: url,
                success: function (respuesta) {
                    //aqui remover el div de espera  block.remove();

                    //Aqui deberia resetear el fomrulario
                    /*
                    if (respuesta.response) {
                        if (button.data("reset") != undefined) {
                            if (button.data("reset")) form.reset();  //resetea formulario
                        }
                    }*/
                    

                    //Muestra mensaje
                    if (respuesta.mensaje != null) {
                        if (respuesta.mensaje.length > 0) {
                            var css = "";
                            if (respuesta.response) css = "alert-success";
                            else css = "alert-danger";

                            var msj = '<div class="alert  ' + css + ' alert-dismissable " > <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times; </button> ' + respuesta.mensaje + ' </div>';

                            form.prepend(msj);
                        }
                    }

                    //muestra los mensaje flotantes
                    if (respuesta.alerta != null) {
                        if (respuesta.alerta.length > 0) {
                            //muestra mensaje de alerta
                            $.notify(respuesta.alerta, "success");                           
                        }
                    }
                    
                    //ejecutar funciones, solo si la accion fue correcta
                    if (respuesta.response) {
                        if (respuesta.funcion != null) {
                            setTimeout(respuesta.funcion, 0);//ejecuta la funcion en 0 segundos
                        }
                    }

                    //redirecciona
                    if (respuesta.href != null) {
                        if (respuesta.href == 'self') window.location.reload(true);//reelee
                        else redirect(respuesta.href);//redirecciona, esta funcion esta definida en cada pagina
                    }
                }//funcion success
            });//ajaxSubmit

            return false;



        });// cualquier click en el body

        //------------------------  Funcion para tablas -------------------------------
        CargarElectronicos();
    }// funcion anonima para document.ready
 );


function CargarElectronicos() {
    var config = {
        ordenPorDefecto:'Id_electronico',
        colNames: ['Id', 'Tipo','Cantidad','Marca'],
        colModel: [
            { name: 'Id_electronico', index: 'Id_electronico', hidden: false /*true*/ },
            {name: 'Id_tipo_electronico', index: 'Id_tipo_electronico', formatter: function (cellvalue, options, rowObject) {
                    return '<a>' + ('product/ver/' + rowObject.id, cellvalue) + '</a>';
                }
            },
            { name:'Cantidad', index: 'Cantidad'
            },
            { name:'Id_marca', index: 'Id_marca'
            }

        ]
    };

    jqGridStart('tabla1', 'paginador-tabla1', 'electronicos/listaElectronicos', config,'Id_electronico');
}

function jqGridStart(id, pager, url, conf) {
    var grid = $("#" + id);

    var start = {
        url: baseUrl(url),
        datatype: 'json',
        mtype: 'POST',
        rowNum: 2,
        rowList: [2, 3, 10],
        pager: '#' + pager,
        sortname: (conf.sortname == undefined ?conf.ordenPorDefecto : null),
        sortorder: (conf.sortorder == undefined ? 'asc' : null),
        viewrecords: true,
        autowidth: true,
        height: 'auto',
        filterToolbar: true
    };

    for (key in conf) {
        start[key] = conf[key];
    }

    grid.jqGrid(start);

    return grid;
}