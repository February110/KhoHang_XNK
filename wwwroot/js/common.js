document.addEventListener("DOMContentLoaded", function () {
    var searchInput = document.getElementById("searchInput");
    if (searchInput) {
        searchInput.addEventListener("input", function () {
            var input = this.value.toLowerCase();
            var rows = document.querySelectorAll("table tbody tr");
            rows.forEach(function (row) {
                var match = Array.from(row.cells).some(function (cell) {
                    return cell.textContent.toLowerCase().includes(input);
                });
                row.style.display = match ? "" : "none";
            });
        });
    }
});