document.getElementById("menu-toggle").addEventListener("click", function () {
    var sidebar = document.getElementById("sidebar");
    var content = document.getElementById("content");

    sidebar.classList.toggle("hidden");
    content.classList.toggle("full-width");
});
