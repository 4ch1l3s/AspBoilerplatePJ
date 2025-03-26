$(document).ready(function () {
  $(".increment, .decrement").click(function () {
    let button = $(this);
    let inputField = button.closest(".input-group").find(".order-quantity");
    let currentValue = parseInt(inputField.val());
    let productId = inputField.data("id");

    // Xác định hành động: tăng hoặc giảm
    if (button.hasClass("increment")) {
      currentValue++;
    } else if (button.hasClass("decrement") && currentValue > 1) {
      currentValue--;
    } else {
      return; // Không giảm nhỏ hơn 1
    }

    // Cập nhật giao diện ngay lập tức
    inputField.val(currentValue);

    // Gửi Ajax để cập nhật trong DB
    $.ajax({
      url: "/Cart/UpdateQuantity",
      type: "POST",
      data: { id: productId, quantity: currentValue },
      success: function (response) {
        console.log("Cập nhật thành công:", response);
      },
      error: function () {
        console.error("Lỗi khi cập nhật số lượng");
      }
    });
  });
 


});
