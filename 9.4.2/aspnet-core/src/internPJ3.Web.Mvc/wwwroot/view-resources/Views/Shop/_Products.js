

var currentPage = $("#view-more-btn").attr("data-current-page")


   

 // Gắn sự kiện click vào nút "View More"
$('#view-more-btn').click(function () {
  
  var totalPage = $(this).attr("data-total-page")
  var pageSize = $(this).attr("data-page-size")  
   
  console.log("Current Page: " + currentPage);
  console.log("Total Page: " + totalPage);
  console.log("Page Size: " + pageSize);

       // Tăng giá trị trang hiện tại mỗi khi bấm vào nút
      currentPage++;
        if (currentPage <= totalPage) {
            $.ajax({
              url: '/Shop/Products?page='+ currentPage , // Gọi action Products trong controller Shop
                type: 'GET',
              data: {
                    pageLocation: "Products",
                    page: currentPage,
                    pageSize: pageSize
                },
                success: function (data) {
                    // Append thêm các sản phẩm mới vào sản phẩm hiện tại
                    $('#product-container .row').append($(data).find('.row').html());

                    // Ẩn nút "View More" nếu đã load hết các trang
                    if (currentPage >= totalPage) {
                        $('#view-more-btn').hide();
                    }
                },
                error: function () {
                    alert('Failed to load more products.');
                }
            });
        }
    });

$(document).ready(function () {
  $(".add-to-cart").click(function () {
    var productId = $(this).data("product-id");
    var quantity = 1; // Hoặc lấy số lượng từ input

    $.ajax({
      url: "/Cart/AddToCart",
      type: "POST",
      contentType: "application/json",
      data: JSON.stringify({
        ProductId: productId,
        AddQuantity: quantity
      }),
      success: function (response) {
        alert("Sản phẩm đã được thêm vào giỏ hàng!");
      },
      error: function (xhr) {
        alert("Lỗi: " + xhr.responseText);
      }
    });
  });

})