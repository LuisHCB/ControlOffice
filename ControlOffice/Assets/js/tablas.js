function jqGridStart(id, pager, url, conf) {
    var grid = $("#" + id);

    var start = {
        url: baseUrl(url),
        datatype: 'json',
        mtype: 'POST',
        rowNum: 20,
        rowList: [20, 50, 100],
        pager: '#' + pager,
        sortname: (conf.sortname == undefined ? conf.ordenPorDefecto : null),
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

function eliminar(ruta)
{   
    if (confirm("¿Estas seguro de eliminar este elemento?")) {
        $.ajax(ruta, {
            type: 'POST',
            async: true,
            beforeSend: function () { },
            success: function (respuesta) {
                if (respuesta.response)
                {                    
                    $('#tabla1').trigger('reloadGrid');   //actualiza la tabla              
                }
                else
                {
                    alert("No se puede eliminar el elemento. Error: "+respuesta.alerta);
                }
            },
            error: function (a, b, c) {
                alert("Error inesperado: No se logro eliminar el elemento seleccionado. " + a + ", " + b + ", " + c );
            }
        });
    }    
}

function darDeBaja(ruta)
{
    if (confirm("¿Estas seguro de dar de baja este usuario?")) {
        $.ajax(ruta, {
            type: 'POST',
            async: true,
            beforeSend: function () { },
            success: function (respuesta) {
                if (respuesta.response) {
                    $.notify(respuesta.alerta,"success");
                    $('#tabla1').trigger('reloadGrid');   //actualiza la tabla              
                }
                else {
                    alert("No se puede dara de baja este usuario. Error: " + respuesta.mensaje);
                }
            },
            error: function (a, b, c) {
                alert("Error inesperado: No se logro dar de baja a este usuario. " + a + ", " + b + ", " + c);
            }
        });
    }
}

function darDeAlta(ruta) {
    if (confirm("¿Estas seguro de dar de alta este usuario?")) {
        $.ajax(ruta, {
            type: 'POST',
            async: true,
            beforeSend: function () { },
            success: function (respuesta) {
                if (respuesta.response) {
                    $.notify(respuesta.alerta, "success");
                    $('#tabla1').trigger('reloadGrid');   //actualiza la tabla              
                }
                else {
                    alert("No se puede dara de alta este usuario. Error: " + respuesta.mensaje);
                }
            },
            error: function (a, b, c) {
                alert("Error inesperado: No se logro dar de alta a este usuario. " + a + ", " + b + ", " + c);
            }
        });
    }
}

function CargarElectronicos() {
    var config = {       
        ordenPorDefecto: 'Id_electronico',
        colNames: ['Id', 'Tipo', 'Cantidad', 'Marca', 'Número de serie','Acciones'],
        colModel: [
            { name: 'Id_electronico', index: 'Id_electronico', hidden: false /*true*/ },
            {
                name: 'Tipo_electronico.Nombre', index: 'Id_tipo_electronico', formatter: function (cellvalue, options, rowObject) {
                    return '<a>' + ('product/ver/' + rowObject.id, cellvalue) + '</a>';
                }
            },
            {
                name: 'Cantidad', index: 'Cantidad'
            },
            {
                name: 'Marca_electronicos.Marca', index: 'Id_marca'
            },
            {
                name: 'NoSerie', index: 'NoSerie'
            },
            {
                name: 'Id_electronico', index: 'Id_electronico',
                formatter: function (cellvalue, options, rowObject) {
                    return '<a title="Eliminar" class="ui-pg-button ui-corner-all ui-state-disabled" onclick="'+"eliminar('electronicos/eliminar?id="+cellvalue +"');"+'" >  <span class="eliminar"> </span></a>';
                }
            },
        ]
    };
    jqGridStart('tabla1', 'paginador-tabla1', 'electronicos/listaElectronicos', config, 'Id_electronico');
}

function CargarSolicitudesElectronicos() {
    var config = {
        ordenPorDefecto: 'Id_solicitud',
        colNames: ['Id', 'Folio de la solicitud', 'Destino','Descripción',  'Fecha de envío','Acciones'],
        colModel: [
            { name: 'Id_solicitud', index: 'Id_solicitud', hidden: true },
            {
                name: 'Folio', index: 'Folio'
            },            
            {
                name: 'Destino', index: 'Destino'
            },
            {
                name: 'Descripcion', index: 'Descripcion'
            },
            {
                name: 'Fecha_envio_texto', index: 'Fecha_envio', formatter: function (cellvalue, options, rowObject) {
                    // return  (new Date(cellvalue)).toDateString();
                    return cellvalue;
                }
            },
            {
                name: 'Id_solicitud', index: 'Id_solicitud',
                formatter: function (cellvalue, options, rowObject) {
                    return '<a title="Eliminar" class="ui-pg-button ui-corner-all ui-state-disabled" onclick="' + "eliminar('electronicos/eliminarSolicitud?id=" + cellvalue + "');" + '" >  <span class="eliminar"> </span></a>';
                }
            },
        ]
    };
    jqGridStart('tabla1', 'paginador-tabla1', 'electronicos/listaSolicitudElectronicos', config, 'Id_solicitud');
}

function CargarMobiliarios() {
    var config = {
        ordenPorDefecto: 'Id_mobiliario',
        colNames: ['Id', 'Tipo', 'Cantidad', 'Marca', 'Número de serie','Acciones'],
        colModel: [
            { name: 'Id_mobiliario', index: 'Id_mobiliario', hidden: false /*true*/ },
            {
                name: 'Tipo_mobiliario.Nombre', index: 'Id_tipo_mobiliario', formatter: function (cellvalue, options, rowObject) {
                    return '<a>' + ('product/ver/' + rowObject.id, cellvalue) + '</a>';
                }
            },
            {
                name: 'Cantidad', index: 'Cantidad'
            },
            {
                name: 'Marca_mobiliario.Marca', index: 'Id_marca'
            },
            {
                name: 'NoSerie', index: 'NoSerie'
            },
            {
                name: 'Id_mobiliario', index: 'Id_mobiliario',
                formatter: function (cellvalue, options, rowObject) {
                    return '<a title="Eliminar" class="ui-pg-button ui-corner-all ui-state-disabled" onclick="' + "eliminar('mobiliario/eliminar?id=" + cellvalue + "');" + '" >  <span class="eliminar"> </span></a>';
                }
            },
        ]
    };
    jqGridStart('tabla1', 'paginador-tabla1', 'mobiliario/listaMobiliarios', config, 'Id_mobiliario');
}

function CargarSolicitudesMobiliarios() {
    var config = {
        ordenPorDefecto: 'Id_solicitud',
        colNames: ['Id', 'Folio de la solicitud',  'Destino','Descripción', 'Fecha de envío','Acciones'],
        colModel: [
            { name: 'Id_solicitud', index: 'Id_solicitud', hidden: true },
            {
                name: 'Folio', index: 'Folio'
            },
            {
                name: 'Destino', index: 'Destino'
            },
            {
                name: 'Descripcion', index: 'Descripcion'
            },
            
            {
                name: 'Fecha_envio_texto', index: 'Fecha_envio', formatter: function (cellvalue, options, rowObject) {
                    // return  (new Date(cellvalue)).toDateString();
                    return cellvalue;
                }
            },
            {
                name: 'Id_solicitud', index: 'Id_solicitud',
                formatter: function (cellvalue, options, rowObject) {
                    return '<a title="Eliminar" class="ui-pg-button ui-corner-all ui-state-disabled" onclick="' + "eliminar('mobiliario/eliminarSolicitud?id=" + cellvalue + "');" + '" >  <span class="eliminar"> </span></a>';
                }
            },
        ]
    };
    jqGridStart('tabla1', 'paginador-tabla1', 'mobiliario/listaSolicitudMobiliarios', config, 'Id_solicitud');
}

function CargarConsumibles() {
    var config = {
        ordenPorDefecto: 'Id_consumible',
        colNames: ['Id', 'Tipo', 'Cantidad', 'Clave', 'Fecha de recepción','Entregado', 'Recibido','Acciones'],
        colModel: [
            { name: 'Id_consumible', index: 'Id_consumible', hidden: false /*true*/ },
            {
                name: 'Tipo_consumible.Nombre', index: 'Id_tipo_consumible', /*formatter: function (cellvalue, options, rowObject) {
                    return '<a>' + ('product/ver/' + rowObject.id, cellvalue) + '</a>';
                }*/
            },
            {
                name: 'Cantidad', index: 'Cantidad'
            },
            {
                name: 'Clave', index: 'Clave'
            },
            {
                name: 'Fecha_recepcion_texto', index: 'Fecha_recepcion'
            },
            {
                name: 'Entrega', index: 'Entrega'
            },
            {
                name: 'Usuario_recibe', index: 'Usuario_recibe'
            },
            {
                name: 'Id_consumible', index: 'Id_consumible',
                formatter: function (cellvalue, options, rowObject) {
                    return '<a title="Eliminar" class="ui-pg-button ui-corner-all ui-state-disabled" onclick="' + "eliminar('consumibles/eliminar?id=" + cellvalue + "');" + '" >  <span class="eliminar"> </span></a>';
                }
            },
        ]
    };
    jqGridStart('tabla1', 'paginador-tabla1', 'consumibles/listaConsumibles', config, 'Id_consumible');
}

function CargarSolicitudesConsumibles() {
    var config = {
        ordenPorDefecto: 'Id_solicitud',
        colNames: ['Id', 'Folio de la solicitud', 'Destino', 'Descripción', 'Fecha de envío','Acciones'],
        colModel: [
            { name: 'Id_solicitud', index: 'Id_solicitud', hidden: true },
            {
                name: 'Folio', index: 'Folio'
            },
            {
                name: 'Destino', index: 'Destino'
            },
            {
                name: 'Descripcion', index: 'Descripcion'
            },

            {
                name: 'Fecha_envio_texto', index: 'Fecha_envio', formatter: function (cellvalue, options, rowObject) {
                    // return  (new Date(cellvalue)).toDateString();
                    return cellvalue;
                }
            },
             {
                 name: 'Id_solicitud', index: 'Id_solicitud',
                 formatter: function (cellvalue, options, rowObject) {
                     return '<a title="Eliminar" class="ui-pg-button ui-corner-all ui-state-disabled" onclick="' + "eliminar('consumibles/eliminarSolicitud?id=" + cellvalue + "');" + '" >  <span class="eliminar"> </span></a>';
                 }
             },
        ]
    };
    jqGridStart('tabla1', 'paginador-tabla1', 'consumibles/listaSolicitudConsumibles', config, 'Id_solicitud');
}

function CargarDocumentosEnviados() {
    var config = {
        ordenPorDefecto: 'Id_documento',
        colNames: ['Id','Tipo de documento','Folio', 'Destino', 'Asunto','Acciones'/*, 'Fecha de envío'*/],
        colModel: [
            { name: 'Id_documento', index: 'Id_documento', hidden: true },
            {
                name: 'Tipos_documento.Tipo_documento', index: 'Id_tipo_documento'
            },
            {
                name: 'Folio', index: 'Folio'
            },
            {
                name: 'Destino', index: 'Destino'
            },
            {
                name: 'Asunto', index: 'Asunto'
            },
            {
                name: 'Id_documento', index: 'Id_documento',
                formatter: function (cellvalue, options, rowObject) {
                    return '<a title="Eliminar" class="ui-pg-button ui-corner-all ui-state-disabled" onclick="' + "eliminar('documentos/eliminarDocEnviado?id=" + cellvalue + "');" + '" >  <span class="eliminar"> </span></a>';
                }
            },

            /*{
                name: 'Fecha_envio_texto', index: 'Fecha_envio', formatter: function (cellvalue, options, rowObject) {
                    // return  (new Date(cellvalue)).toDateString();
                    return cellvalue;
                }
            },*/
        ]
    };
    jqGridStart('tabla1', 'paginador-tabla1', 'documentos/listaDocumentosEnviados', config, 'Id_documento');
}

function CargarDocumentosRecibidos() {
    var config = {
        ordenPorDefecto: 'Id_documento',
        colNames: ['Id', 'Folio', 'Destino', 'Asunto', 'Requiere respuesta', 'Respuesta','Acciones'/*, 'Fecha de envío'*/],
        colModel: [
            { name: 'Id_documento', index: 'Id_documento', hidden: true },
            {
                name: 'Folio', index: 'Folio'
            },
            {
                name: 'Destino', index: 'Destino'
            },
            {
                name: 'Asunto', index: 'Asunto'
            },
            {
                name: 'Requiere_respuesta', index: 'Requiere_respuesta', formatter: function (cellvalue, options, rowObject) {
                    return   cellvalue?'Si':'No';                    
                }
            },
            {
                name: 'Respuesta', index: 'Respuesta'
            },
            {
                name: 'Id_documento', index: 'Id_documento',
                formatter: function (cellvalue, options, rowObject) {
                    return '<a title="Eliminar" class="ui-pg-button ui-corner-all ui-state-disabled" onclick="' + "eliminar('documentos/eliminarDocRecibido?id=" + cellvalue + "');" + '" >  <span class="eliminar"> </span></a>';
                }
            },

            /*{
                name: 'Fecha_envio_texto', index: 'Fecha_envio', formatter: function (cellvalue, options, rowObject) {
                    // return  (new Date(cellvalue)).toDateString();
                    return cellvalue;
                }
            },*/
        ]
    };

    jqGridStart('tabla1', 'paginador-tabla1', 'documentos/listaDocumentosRecibidos', config, 'Id_documento');
}

function CargarDocumentosEstandar() {
    
    var config = {
        ordenPorDefecto: 'Id_documento',
        colNames: ['Id','Folio',  'Tipo de documento','Descripcion','Acciones'],
        colModel: [
            { name: 'Id_documento', index: 'Id_documento', hidden: true },
            {
                name: 'Folio', index: 'Folio'
            },
            {
                name: 'Tipo_documento.Tipo_documento', index: 'Id_tipo_documento'
            },
            {
                name: 'Descripcion', index: 'Descripcion'
            },
            {
                name: 'Id_documento', index: 'Id_documento',
                formatter: function (cellvalue, options, rowObject) {
                    return '<a title="Eliminar" class="ui-pg-button ui-corner-all ui-state-disabled" onclick="' + "eliminar('documentos/eliminarDocInterno?id=" + cellvalue + "');" + '" >  <span class="eliminar"> </span></a>';
                }
            },

            /*{
                name: 'Fecha_envio_texto', index: 'Fecha_envio', formatter: function (cellvalue, options, rowObject) {
                    // return  (new Date(cellvalue)).toDateString();
                    return cellvalue;
                }
            },*/
        ]
    };

    jqGridStart('tabla1', 'paginador-tabla1', 'documentos/listaDocumentosInternos', config, 'Id_documento');
}

function CargarUsuarios() {
    var config = {
        ordenPorDefecto: 'Usuario',
        colNames: ['Usuario', 'Nombre del usuario', 'Activo','Administrador','Acciones'/*, 'Fecha de envío'*/],
        colModel: [
            { name: 'Usuario', index: 'Usuario', hidden: false },
            {
                name: 'Nombre', index: 'Nombre'
            },
            {
                name: 'Activo', index: 'Activo',
                formatter: function (cellvalue, options, rowObject) {
                    return cellvalue ? 'Si' : 'No';
                }
            },
            {
                name: 'Administrador', index: 'Administrador',
                formatter: function (cellvalue, options, rowObject) {
                    return cellvalue ? 'Si' : 'No';
                }
            },
            {
                name: 'Id_documento', index: 'Id_documento',
                formatter: function (cellvalue, options, rowObject) {
                    if(rowObject.Activo)
                    {
                        return '<a title="Dar de baja" class="ui-pg-button ui-corner-all ui-state-disabled" onclick="' + "darDeBaja('usuarios/darDeBaja?id=" + rowObject.Usuario + "');" + '" >  <span class="darDeBaja"> </span></a>' +
                               ' <a data-target="#form-usuario-info" data-toggle="modal" title="Modificar contraseña" class="ui-pg-button ui-corner-all ui-state-disabled" onclick="' + "$('#modal-form-pass').load('usuarios/modificarPass?id=" + rowObject.Usuario + "');" + '" >  <span class="contrasena"> </span></a>';;

                    }
                    else
                    {
                        return '<a title="Dar de alta" class="ui-pg-button ui-corner-all ui-state-disabled" onclick="' + "darDeAlta('usuarios/darDeAlta?id=" + rowObject.Usuario + "');" + '" >  <span class="darDeAlta"> </span></a>';
                    }
                }
            },

            /*{
                name: 'Fecha_envio_texto', index: 'Fecha_envio', formatter: function (cellvalue, options, rowObject) {
                    // return  (new Date(cellvalue)).toDateString();
                    return cellvalue;
                }
            },*/
        ]
    };

    jqGridStart('tabla1', 'paginador-tabla1', 'usuarios/listaUsuarios', config, 'Usuario');
}