using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;

using internPJ3.Products.DTO;
using internPJ3.Tour.DTO;
using internPJ3.Users.Dto;
using Microsoft.EntityFrameworkCore;



namespace internPJ3.Tour
{

	public class TourAppService : internPJ3AppServiceBase, ITourAppService
	{
		private readonly IRepository<TourE, long> _tourRepository; // Giúp làm việc với database mà không cần SQL Query
		private readonly IAbpSession _abpSession;


		public TourAppService(IRepository<TourE, long> tourRepository, IAbpSession abpSession) // Thiết lập để TaskAppService có thể làm việc với dữ liệu ProductF
																																													 // thông qua _taskRepository - ChatGPT
		{
			_tourRepository = tourRepository;
			_abpSession = abpSession;
		}


		//Get - All
		public async Task<PagedResultDto<TourListDto>> GetAll(TourGetAllDto input) //Dữ liệu trả về là danh sách sản phẩm được gói trong đối tượng ListResultDto
		{
			var tour = _tourRepository.GetAll();//laays tour từ database


			//Logic của Search
			if (!string.IsNullOrEmpty(input.SearchString))
			{
				tour = tour.Where(n => n.TourName.Contains(input.SearchString));
			}


			if (input.TrashBox == true)
			{
				tour = tour.Where(x => x.IsDeleted); //Xóa mềm
			}
			else
			{
				tour = tour.Where(x => !x.IsDeleted);
			}

			var TotalCount = await tour.CountAsync(); //đếm dữ liệu có trong bảng

			var items = await tour
				.OrderBy(x => x.Id)
				.PageBy(input)
				.Select(x => new TourListDto
				{
					Id = x.Id,
					TourName = x.TourName,
					GroupSize = (x.MinGroupSize == 0 && x.MaxGroupSize == 0) ? "-" :
						(x.MinGroupSize == 0) ? "Max " + x.MaxGroupSize.ToString() + " pax" :
						(x.MaxGroupSize == 0) ? "Min - " + x.MinGroupSize.ToString() + " pax" :
						x.MinGroupSize.ToString() + "-" + x.MaxGroupSize.ToString() + " pax",
					TourTypeCid = x.TourTypeCid,
					DateT = x.StartDate.Value.ToString("dd/MM/yyyy") + " - " + x.EndDate.Value.ToString("dd/MM/yyyy"),
					Transportation = x.Transportation,
					TourPrice = x.TourPrice,
					PhoneNumber = x.PhoneNumber,
					Description = x.Description,
					Attachment = x.Attachment
				})
			.ToListAsync();


			return new PagedResultDto<TourListDto>(TotalCount,
					items
			);
		}


		//Create
		public async Task Create(TourCreateDto input)
		{
			TourE Te = new TourE();
			Te.TourName = input.TourName;
			Te.MinGroupSize = input.MinGroupSize;
			Te.MaxGroupSize = input.MaxGroupSize;
			Te.TourTypeCid = input.TourTypeCid;
			Te.StartDate = input.StartDate;
			Te.EndDate = input.EndDate;
			Te.Transportation = input.Transportation;
			Te.TourPrice = input.TourPrice;
			Te.PhoneNumber = input.PhoneNumber;
			Te.Description = input.Description;
			Te.Attachment = input.Attachment;
			Te.CreationTime = DateTime.Now;
			Te.CreatorUserId = _abpSession.UserId;
			await _tourRepository.InsertAsync(Te);
		}


		//Soft Delete
		public async Task Delete(int Id)
		{
			var tour = await _tourRepository.GetAsync(Id);

			tour.IsDeleted = true;
			tour.DeleterUserId = _abpSession.UserId;
			tour.DeletionTime = DateTime.Now;
			await _tourRepository.UpdateAsync(tour);
		}

		//Update
		public async Task Update(TourUpdateDto input)
		{
			var tour = await _tourRepository.GetAsync(input.Id);
			tour.Id = input.Id;
			tour.TourName = input.TourName;
			tour.MinGroupSize = input.MinGroupSize;
			tour.MaxGroupSize = input.MaxGroupSize;
			tour.TourTypeCid = input.TourTypeCid;
			tour.StartDate = input.StartDate;
			tour.EndDate = input.EndDate;
			tour.Transportation = input.Transportation;
			tour.TourPrice = input.TourPrice;
			tour.PhoneNumber = input.PhoneNumber;
			tour.Description = input.Description;
			tour.Attachment = input.Attachment;
			tour.LastModificationTime = DateTime.Now;
			tour.LastModifierUserId = _abpSession.UserId;
			await _tourRepository.UpdateAsync(tour);
		}


		//Hard Delete
		public async Task HardDelete(long id)
		{
			_tourRepository.DeleteAsync(id);
		}


		//Get 1 - then update lmao
		public async Task<TourUpdateDto> Get(int id)
		{
			var input = await _tourRepository.GetAsync(id);
			TourUpdateDto tour = new TourUpdateDto();

			tour.Id = id;
			tour.TourName = input.TourName;
			tour.MinGroupSize = input.MinGroupSize;
			tour.MaxGroupSize = input.MaxGroupSize;
			tour.TourTypeCid = input.TourTypeCid;
			tour.StartDate = input.StartDate;
			tour.EndDate = input.EndDate;
			tour.Transportation = input.Transportation;
			tour.TourPrice = input.TourPrice;
			tour.PhoneNumber = input.PhoneNumber;
			tour.Description = input.Description;
			tour.Attachment = input.Attachment;
			return tour;
		}

		//Lang
		public async Task ChangeLanguage(ChangeUserLanguageDto input)
		{
			await SettingManager.ChangeSettingForUserAsync(
					AbpSession.ToUserIdentifier(),
					LocalizationSettingNames.DefaultLanguage,
					input.LanguageName
			);
		}



	}
}
