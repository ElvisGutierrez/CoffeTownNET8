var dataTable;

$(document).ready(function () {
    cargarDatatable();
});


function cargarDatatable() {
    dataTable = $("#tblVentas").DataTable({
        "ajax": {
            "url": "/admin/ventas/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "fechaVenta", "width": "20%", "className": "text-center" },
            { "data": "nombre", "width": "20%", "className": "text-center" },
            { "data": "producto.nombre", "width": "20%", "className": "text-center" },
            { "data": "cantidad", "width": "20%", "className": "text-center" },
            { "data": "montoTotal", "width": "20%", "className": "text-center" },
            
        ],
        "language": {
            "decimal": "",
            "emptyTable": "No hay registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "width": "100%"
    });
}