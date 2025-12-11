var dataTable;

$(document).ready(function () {
    cargarDatatable();
});


function cargarDatatable() {
    dataTable = $("#tblCategorias").DataTable({
        "ajax": {
            "url": "/admin/categorias/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "nombre", "width": "30%", "className": "text-center" },
            { "data": "orden", "width": "30%", "className": "text-center" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Categorias/Edit/${data}" class="btn btn-secondary" style="cursor:pointer; width:140px; font-size: 15px; outline: none; box-shadow: 0 0 0 0.1rem black;" onFocus="this.style.boxShadow = '0 0 0 0.2rem black)'">
                                Editar <i class="far fa-edit"></i> 
                                </a>
                                &nbsp;
                                <a onclick=Delete("/Admin/Categorias/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:140px; font-size: 15px; outline: none; box-shadow: 0 0 0 0.1rem black;" onFocus="this.style.boxShadow = '0 0 0 0.2rem black)'">
                                Borrar <i class="far fa-trash-alt"></i>
                                </a>
                          </div>
                         `;
                }, "width": "40%"
            }
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

function Delete(url) {
    Swal.fire({
        icon: "warning",
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        showCancelButton: true,
        cancelButtonText: `Cancelar <i class="fa fa-thumbs-down"></i>`,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: `Si, eliminar! <i class="fa fa-thumbs-up"></i>`,
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        Swal.fire({
                            title: "Eliminado!",
                            text: data.message,
                            icon: "success"
                        });
                        dataTable.ajax.reload();
                    }
                    else {
                        Swal.fire({
                            title: "Error!",
                            text: data.message,
                            icon: "error"
                        });
                    }
                }
            });
        }
    });
}