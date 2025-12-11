var dataTable;

$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    $.ajax({
        url: "/admin/pedidos/GetAll",
        type: "GET",
        datatype: "json",
        success: function (data) {
            var cardContainer = $("#cardContainer");
            cardContainer.empty(); // Limpiar el contenedor antes de agregar nuevas tarjetas

            if (data.data.length === 0) {
                var noOrdersHtml = `
                    <div class="col-12 text-center">
                        <h4>No hay pedidos disponibles.</h4>
                    </div>
                `;
                cardContainer.append(noOrdersHtml);
            } else {
                data.data.forEach(function (item) {
                    var cardHtml = `
                        <div class="col-md-3 mb-4">
                            <div class="card card-producto" style="background-color: #ffe6dd;">
                            <h4 class="card-header text-center">${item.nombre}</h4>
                                <div class="card-body">
                                    <p class="card-text text-center">Producto: ${item.producto.nombre}</p>
                                    <p class="card-text text-center">Cantidad: ${item.cantidad}</p>
                                    <p class="card-text text-center">Precio por unidad: $${item.producto.precio}</p>
                                    <p class="card-text text-center">Total: $${item.montoTotal}</p>
                                    <div class="text-center">
                                        <a href="/Admin/Ventas/Create/${item.id}" class="btn form-control" style="background-color: #c96442; color: white; font-size: 16px; outline: none; box-shadow: 0 0 0 0.1rem black;" onFocus="this.style.boxShadow = '0 0 0 0.2rem black)'">
                                            Finalizar venta <i class="mr-4 fas fa-cash-register"></i> 
                                        </a>
                                        <a href="/Admin/Pedidos/Edit/${item.id}" class="btn form-control btn-secondary mt-2 " style="cursor:pointer; font-size: 15px; outline: none; box-shadow: 0 0 0 0.1rem black;" onFocus="this.style.boxShadow = '0 0 0 0.2rem black)'">
                                            Editar <i class="far fa-edit"></i> 
                                        </a>
                                        <a onclick=Delete("/Admin/Pedidos/Delete/${item.id}") class="btn form-control btn-danger mt-2   text-white" style="cursor:pointer; font-size: 15px; outline: none; box-shadow: 0 0 0 0.1rem black;" onFocus="this.style.boxShadow = '0 0 0 0.2rem black)'">
                                            Borrar <i class="far fa-trash-alt"></i>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    `;
                    cardContainer.append(cardHtml);
                });
            }
        },
        error: function () {
            alert("Error al cargar los pedidos.");
        }
    });
}

function Delete(url) {
    Swal.fire({
        icon: "warning",
        title: "¿Está seguro de borrar?",
        text: "¡Este contenido no se puede recuperar!",
        showCancelButton: true,
        cancelButtonText: `Cancelar <i class="fa fa-thumbs-down"></i>`,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: `Sí, eliminar! <i class="fa fa-thumbs-up"></i>`,
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
                        cargarDatatable(); // Recargar las tarjetas después de eliminar
                    } else {
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