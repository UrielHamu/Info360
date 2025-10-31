// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function filtrarEmpresas() {
    var input = document.getElementById("buscador").value.toLowerCase();
    var empresas = document.getElementsByClassName("empresa-item");
    for (let i = 0; i < empresas.length; i++) {
        let nombre = empresas[i].textContent.toLowerCase();
        empresas[i].style.display = nombre.includes(input) ? "" : "none";
    }
}