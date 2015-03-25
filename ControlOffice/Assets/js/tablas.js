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


function CargarElectronicos() {
    var config = {
        ordenPorDefecto: 'Id_electronico',
        colNames: ['Id', 'Tipo', 'Cantidad', 'Marca', 'Número de serie'],
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
        ]
    };
    jqGridStart('tabla1', 'paginador-tabla1', 'electronicos/listaElectronicos', config, 'Id_electronico');
}

function CargarSolicitudesElectronicos() {
    var config = {
        ordenPorDefecto: 'Id_solicitud',
        colNames: ['Id', 'Folio de la solicitud', 'Destino','Descripción',  'Fecha de envío'],
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
        ]
    };
    jqGridStart('tabla1', 'paginador-tabla1', 'electronicos/listaSolicitudElectronicos', config, 'Id_solicitud');
}

function CargarMobiliarios() {
    var config = {
        ordenPorDefecto: 'Id_mobiliario',
        colNames: ['Id', 'Tipo', 'Cantidad', 'Marca', 'Número de serie'],
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
        ]
    };
    jqGridStart('tabla1', 'paginador-tabla1', 'mobiliario/listaMobiliarios', config, 'Id_mobiliario');
}

function CargarSolicitudesMobiliarios() {
    var config = {
        ordenPorDefecto: 'Id_solicitud',
        colNames: ['Id', 'Folio de la solicitud',  'Destino','Descripción', 'Fecha de envío'],
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
        ]
    };
    jqGridStart('tabla1', 'paginador-tabla1', 'mobiliario/listaSolicitudMobiliarios', config, 'Id_solicitud');
}

function CargarConsumibles() {
    var config = {
        ordenPorDefecto: 'Id_consumible',
        colNames: ['Id', 'Tipo', 'Cantidad', 'Clave', 'Fecha de recepción','Entregado', 'Recibido'],
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
        ]
    };
    jqGridStart('tabla1', 'paginador-tabla1', 'consumibles/listaConsumibles', config, 'Id_consumible');
}

function CargarSolicitudesConsumibles() {
    var config = {
        ordenPorDefecto: 'Id_solicitud',
        colNames: ['Id', 'Folio de la solicitud', 'Destino', 'Descripción', 'Fecha de envío'],
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
        ]
    };
    jqGridStart('tabla1', 'paginador-tabla1', 'consumibles/listaSolicitudConsumibles', config, 'Id_solicitud');
}