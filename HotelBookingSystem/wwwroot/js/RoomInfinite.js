$(document).ready(function () {
    const tbody = $('#myTable tbody');
    let page = 1;
    const pageSize = 10;
    let loading = false;
    let noMoreData = false;

    function appendRows(items) {
        items.forEach(item => {
            const tr = `<tr>
            <td>
            <a href="@Action.Url("Bookpage", "Book")" class="glyphicon glyphicon-edit button-edit-enabled horizontal-center"></a>
            </td>
            <td>${item.roomId}</td>
            <td>${item.roomType}</td>
            <td>${item.floor}</td>
            <td>${item.roomNumber}</td>  <!-- 注意大小寫 -->
            <td>${item.bedType}</td>
            <td>${item.price}</td>
        </tr>`;
            tbody.append(tr);
        });
    }


    function loadMore() {
        if (loading || noMoreData) return;
        loading = true;

        $.ajax({
            url: '/api/Root/SearchByTag',
            type: 'GET',
            data: { page: page, pageSize: pageSize },
            success: function (response) {
                const items = response.items || response.Items || [];
                if (items.length === 0) {
                    noMoreData = true;
                    tbody.append('<tr><td colspan="4" style="text-align:center;">沒有更多資料了</td></tr>');
                } else {
                    appendRows(items);
                    page++;
                }
                loading = false;
            },
            error: function (xhr, status, error) {
                console.error('Ajax Error:', status, error, xhr.responseText);
                alert('Failed to load items: ' + (xhr.responseJSON?.message || error));
            }
        });
    }

    // 初始載入
    loadMore();

    // 監聽滾動事件（假設容器有設定固定高度與 overflow）
    $('#table-container').on('scroll', function () {
        if (this.scrollTop + this.clientHeight >= this.scrollHeight - 10) {
            loadMore();
        }
    });
});
