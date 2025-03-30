$(document).ready(function () {
	updateTotalPrice();
	$(".cart-increment, .cart-decrement").click(function () {
		let button = $(this);
		let subTotal = button.closest("tr").find(".sub-total");
		let inputField = button.closest(".input-group").find("input");
		let currentValue = parseInt(inputField.val());
		let productPrice = parseInt(button.closest("tr").find(".product-price").text());

		let prodcutId = inputField.attr('data-product-id');
		let cartId = inputField.attr('data-cart-id');
		console.log(cartId);
		//let orderQuantity = ;



		// Xác định hành động: tăng hoặc giảm
		if (button.hasClass("cart-increment")) {
			currentValue++;
		} else if (button.hasClass("cart-decrement") && currentValue > 1) {
			currentValue--;
		} else {
			return; // Không giảm nhỏ hơn 1
		}
		// Cập nhật giao diện
		inputField.val(currentValue);
		subTotal.text(currentValue * productPrice);
		updateTotalPrice();
		updateQuantityToServer(prodcutId, currentValue, cartId);

		// Gửi Ajax để cập nhật trong DB
		//$.ajax({
		//  url: "/Cart/UpdateQuantity",
		//  type: "POST",
		//  data: { id: productId, quantity: currentValue },
		//  success: function (response) {
		//    console.log("Cập nhật thành công:", response);
		//  },
		//  error: function () {
		//    console.error("Lỗi khi cập nhật số lượng");
		//  }
		//});
	});
	







});


function updateTotalPrice() {
	let total = 0;
	$(".sub-total").each(function () {
		total += parseInt($(this).text());
	});
	$(".total-price-container .total-price").text(total);
};


function updateQuantityToServer(productId, currentValue, cartId) {
	$.ajax({
		url: "/Cart/UpdateCartItemQuantity",  // Đường dẫn API phía backend
		type: "PUT",
		contentType: "application/json",
		data: {
			ProductId: productId,
			NewQuantity: currentValue,
			CartId: cartId
		},
		success: function (response) {
			console.log("Cập nhật thành công:", response);
		},
		error: function (xhr, status, error) {
			console.error("Lỗi khi cập nhật số lượng:", error);
		}
	});
}
