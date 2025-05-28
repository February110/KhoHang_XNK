document.addEventListener('DOMContentLoaded', function () {
    // Hàm khởi tạo với cấu hình tùy chỉnh
    function initializeList(config) {
        const {
            searchInputId = 'searchInput',
            exportBtnId = 'exportExcelBtn',
            clearSearchBtnId = 'clearSearch',
            noResultsId = 'noResults',
            tableBodyId = 'tableBody',
            rowSelector = '.data-row',
            searchFields = ['ma', 'ten', 'mota', 'loai'],
            exportUrl
        } = config;

        const searchInput = document.getElementById(searchInputId);
        const exportBtn = document.getElementById(exportBtnId);
        const clearSearchBtn = document.getElementById(clearSearchBtnId);
        const noResults = document.getElementById(noResultsId);
        const tableBody = document.getElementById(tableBodyId);
        const allRows = document.querySelectorAll(rowSelector);

        if (!searchInput || !exportBtn) {
            console.error('Required elements not found');
            return;
        }

        let currentSearchTerm = '';
        let searchTimeout;

        // Tìm kiếm
        searchInput.addEventListener('input', function () {
            clearTimeout(searchTimeout);
            searchTimeout = setTimeout(() => {
                performSearch(this.value.trim());
            }, 300);
        });

        // Xóa tìm kiếm
        if (clearSearchBtn) {
            clearSearchBtn.addEventListener('click', function () {
                searchInput.value = '';
                performSearch('');
            });
        }

        function performSearch(searchTerm) {
            currentSearchTerm = searchTerm.toLowerCase();
            let visibleCount = 0;

            allRows.forEach(row => {
                const shouldShow = searchTerm === '' ||
                    searchFields.some(field => row.dataset[field]?.toLowerCase().includes(currentSearchTerm));

                if (shouldShow) {
                    row.style.display = '';
                    visibleCount++;
                } else {
                    row.style.display = 'none';
                }
            });

            // Cập nhật UI
            if (searchTerm === '') {
                if (clearSearchBtn) clearSearchBtn.style.display = 'none';
                if (noResults) noResults.classList.add('d-none');
                if (tableBody) tableBody.style.display = '';
            } else {
                if (clearSearchBtn) clearSearchBtn.style.display = 'block';
                if (visibleCount === 0) {
                    if (noResults) noResults.classList.remove('d-none');
                    if (tableBody) tableBody.style.display = 'none';
                } else {
                    if (noResults) noResults.classList.add('d-none');
                    if (tableBody) tableBody.style.display = '';
                }
            }
        }

        // Xuất Excel
        if (exportBtn && exportUrl) {
            exportBtn.addEventListener('click', function () {
                console.log('Export button clicked');
                exportToExcel();
            });

            // Trong initializeList
            async function exportToExcel() {
                try {
                    exportBtn.disabled = true;

                    const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
                    const token = tokenInput ? tokenInput.value : null;

                    const form = new FormData();
                    form.append('searchTerm', currentSearchTerm || '');
                    form.append('fromDate', fromDate ? fromDate.value : ''); // Đảm bảo gửi giá trị từ input
                    form.append('toDate', toDate ? toDate.value : '');       // Đảm bảo gửi giá trị từ input
                    if (token) form.append('__RequestVerificationToken', token);

                    console.log('Sending POST request to:', exportUrl);
                    console.log('Form data:', [...form.entries()]);
                    console.log('Anti-Forgery Token:', token);

                    const response = await fetch(exportUrl, {
                        method: 'POST',
                        body: form
                    });

                    console.log('Response status:', response.status);
                    console.log('Response headers:', [...response.headers.entries()]);

                    if (!response.ok) {
                        let errorMessage = `HTTP error! status: ${response.status}`;
                        const errorText = await response.text();
                        console.log('Error response:', errorText);
                        if (errorText) errorMessage += ` - ${errorText}`;
                        throw new Error(errorMessage);
                    }

                    const contentDisposition = response.headers.get('Content-Disposition');
                    let filename = `Export_${new Date().toISOString().slice(0, 19).replace(/[-:]/g, '')}.xlsx`;
                    if (contentDisposition) {
                        const filenameMatch = contentDisposition.match(/filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/);
                        if (filenameMatch && filenameMatch[1]) filename = filenameMatch[1].replace(/['"]/g, '');
                    }

                    const blob = await response.blob();
                    const url = window.URL.createObjectURL(blob);
                    const a = document.createElement('a');
                    a.href = url;
                    a.download = filename;
                    document.body.appendChild(a);
                    a.click();
                    document.body.removeChild(a);
                    window.URL.revokeObjectURL(url);
                } catch (error) {
                    console.error('Export error:', error);
                    alert(`Lỗi xuất Excel: ${error.message}`);
                } finally {
                    exportBtn.disabled = false;
                }
            }
        }
    }

    // Xuất hàm để sử dụng
    window.initializeList = initializeList;
});