using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using internPJ3.Category;
using internPJ3.Category.DTO;
using internPJ3.Controllers;
using internPJ3.Products;
using internPJ3.Products.DTO;
using internPJ3.Tour;
using internPJ3.Tour.DTO;
using internPJ3.Web.Models.Product;
using internPJ3.Web.Models.Tour;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace internPJ3.Web.Controllers
{
	public class TourController : internPJ3ControllerBase
	{
		private readonly ITourAppService _tourService;

		public TourController(ITourAppService tourService)
		{
			_tourService = tourService;
		}

		//GetAll
		public async Task<ActionResult> Index(TourGetAllDto input)
		{
			var output = (await _tourService.GetAll(input)).Items;



			var model = new TourModel
			{
				TourListDtos = output
			};
			return View(model);
		}

		//Update
		public async Task<ActionResult> UpdateModal(int Id) //rì quét từ phía giao diện
		{

			
			var tour = await _tourService.Get(Id); //product này sẽ nhận về dto dựa trên giá trị Id của sản phẩm bằng phương thức get
			
			var model = new TourModel //Tạo biến model để búng giá trị sang cho View, EditProductModel là 1 ViewModel chứa thông tin hiển thị giao diện
			{
				Id = tour.Id,
				TourName = tour.TourName,
				MinGroupSize = tour.MinGroupSize,
				MaxGroupSize = tour.MaxGroupSize,
				TourTypeCid = tour.TourTypeCid,
				StartDate = tour.StartDate,
				EndDate = tour.EndDate,
				Transportation = tour.Transportation,
				TourPrice = tour.TourPrice,
				PhoneNumber = tour.PhoneNumber,
				Description = tour.Description,
				Attachment = tour.Attachment
			};
			return PartialView("_UpdateModal", model);  //Nạp file _UpdateModal - chứa giao diện chỉnh sửa

		}



		//Xử lý files
		[HttpPost]
		public async Task<ActionResult> UploadFile(IFormFile AttachmentFile)
		{

			if (AttachmentFile == null || AttachmentFile.Length == 0) //Kiểm tra file
			{
				return Json(new { success = false, message = "Không có file được upload" });
			}

			//var newFileName = Guid.NewGuid().ToString() + fileExtension;
			var newFileName = AttachmentFile.FileName; // Giữ nguyên tên file cũ, dùng cho việc ghi đè
			var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files");

			var filePath = Path.Combine(uploadPath, newFileName); //khởi tại filePath mới

			using (FileStream stream = new(filePath, FileMode.Create)) //lưu file ảnh
			{
				await AttachmentFile.CopyToAsync(stream);
			}

			var fileUrl = Url.Content("~/Files/" + newFileName);
			return Json(new { success = true, filePath = fileUrl });
		}





		//Delete - có thể không cần tới




	}
}
