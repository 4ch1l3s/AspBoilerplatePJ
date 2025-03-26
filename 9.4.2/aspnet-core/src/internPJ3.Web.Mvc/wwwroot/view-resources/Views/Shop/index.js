$(document).ready(function () {
  let totalPages = parseInt($(".pagination").data("total-pages")) || 1;

  function loadProducts(page) {
    $.ajax({
      url: "/Shop/Index",
      type: "GET",
      data: { page: page },
      beforeSend: function () {
        //loading
        $("#product-list").html("<p>...</p>");
      },
      success: function (response) {
        $("#product-list").html($(response).find("#product-list").html());
        $(".pagination").html($(response).find(".pagination").html());

        updatePagination(page);
      },
      error: function () {
        alert("err");
      }
    });
  }

  function updatePagination(currentPage) {
    $(".pagination .page-item").removeClass("active disabled");

    if (currentPage <= 1) {
      $(".pagination .prev").addClass("disabled");
    }
    if (currentPage >= totalPages) {
      $(".pagination .next").addClass("disabled");
    }

    $(".pagination .page-number[data-page='" + currentPage + "']").addClass("active");
  }

 
  $(document).on("click", ".pagination .page-item:not(.disabled) a", function (e) {
    e.preventDefault();
    let page = parseInt($(this).data("page"));

    if (!isNaN(page) && page > 0 && page <= totalPages) {
      loadProducts(page);
    }
  });

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


});
