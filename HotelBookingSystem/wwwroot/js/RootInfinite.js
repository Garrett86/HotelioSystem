$(document).ready(function () {
    const tbody = $('#myTable tbody');
    let page = 1;
    const pageSize = 15;
    let noMoreData = false;
    let loading = false;
    let searchQuery = '';

    $('#searchForm').on('submit', function (e) {
        e.preventDefault();
        page = 1;
        noMoreData = false;
        searchQuery = $(this).serialize();
        tbody.empty();
        loadMore();
    });

    function loadMore() {
        if (loading || noMoreData) return;
        loading = true;

        const url = `/api/rooms?${searchQuery}&page=${page}&pageSize=${pageSize}`;

        $.ajax({
            url: url,
            type: 'GET',
            success: function (res) {
                const items = res.items || res.Items || [];
                if (items.length === 0) {
                    noMoreData = true;
                    tbody.append(`<tr><td colspan="8" class="text-center text-muted">沒有更多資料了</td></tr>`);
                } else {
                    appendRows(items);
                    page++;
                }
                loading = false;
                $('#table-container').show();
            },
            error: function (xhr) {
                alert('載入失敗: ' + (xhr.responseJSON?.message || '未知錯誤'));
                loading = false;
            }
        });
    }

    function appendRows(items) {
        items.forEach(item => {
            const editUrl = `${getByIdRoomBaseUrl}?id=${item.roomId}`;
            tbody.append(`
                <tr>
                    <td><a href="${editUrl}" class="btn btn-sm btn-link text-primary"><i class="fas fa-edit"></i></a></td>
                    <td><a href="#" class="btn-delete btn btn-sm btn-link text-danger" data-roomid="${item.roomId}"><i class="fa fa-trash"></i></a></td>
                    <td>${item.roomId}</td>
                    <td>${item.roomType}</td>
                    <td>${item.floor}</td>
                    <td>${item.roomNumber}</td>
                    <td>${item.bedType}</td>
                    <td>${item.price}</td>
                    <td>${item.cookingCount}</td>
                </tr>`);
        });
    }

    $(document).on('click', '.btn-delete', function (e) {
        e.preventDefault();
        const roomId = $(this).data('roomid');
        if (!roomId) return alert('找不到房間編號');

        if (confirm(`確定要刪除房間編號 ${roomId} 嗎？`)) {
            $.ajax({
                url: `/api/rooms/${roomId}`,
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

    // 初次載入
    loadMore();
});
