$(document).ready(function () {
    const tbody = document.querySelector('#myTable tbody');
    let page = 1;
    const pageSize = 10;
    let loading = false;
    let noMoreData = false;

    function appendRows(items) {
        items.forEach(item => {
            const editUrl = `${getByIdRoomBaseUrl}?id=${item.roomId}`;
            const tr = `
                <tr>
                    <td>
                        <a href="${editUrl}" class="btn btn-sm btn-link text-primary">
                            <i class="fas fa-edit"></i>
                        </a>
                    </td>
                    <td>
                        <a href="${editUrl}" class="btn-delete btn btn-sm btn-link text-danger">
                            <i class="fa fa-trash"></i>
                        </a>
                    </td>
                    <td>${item.roomId}</td>
                    <td>${item.roomType}</td>
                    <td>${item.floor}</td>
                    <td>${item.roomNumber}</td>
                    <td>${item.bedType}</td>
                    <td>${item.price}</td>
                </tr>`;
            tbody.insertAdjacentHTML('beforeend', tr);
        });
    }

    function loadMore() {
        if (loading || noMoreData) return;
        loading = true;

        $.ajax({
            url: '/api/Root/SearchByRoom',
            type: 'GET',
            data: { page: page, pageSize: pageSize },
            success: function (response) {
                const items = response.items || response.Items || [];
                if (items.length === 0) {
                    noMoreData = true;
                    tbody.insertAdjacentHTML('beforeend', `
                        <tr>
                            <td colspan="8" class="text-center text-muted">沒有更多資料了</td>
                        </tr>
                    `);
                } else {
                    appendRows(items);
                    page++;
                }
                loading = false;
            },
            error: function (xhr, status, error) {
                console.error('Ajax Error:', status, error, xhr.responseText);
                alert('載入失敗：' + (xhr.responseJSON?.message || error));
                loading = false;
            }
        });
    }

    // 初始載入
    loadMore();

    // 無限滾動（加入 debounce）
    let debounceTimer;
    $('#table-container').on('scroll', function () {
        clearTimeout(debounceTimer);
        debounceTimer = setTimeout(() => {
            if (this.scrollTop + this.clientHeight >= this.scrollHeight - 10) {
                loadMore();
            }
        }, 100);
    });

    // 刪除按鈕事件
    $(document).on('click', '.btn-delete', function (e) {
        e.preventDefault();
        const roomId = $(this).data('roomid');
        if (confirm(`確定要刪除房間編號 ${roomId} 嗎？`)) {
            $.ajax({
                url: `/api/Root/DeleteRoom/${roomId}`,
                type: 'DELETE',
                success: function () {
                    alert('刪除成功');
                    $(e.target).closest('tr').remove();
                },
                error: function (xhr) {
                    alert('刪除失敗：' + (xhr.responseJSON?.message || '未知錯誤'));
                }
            });
        }
    });
});
