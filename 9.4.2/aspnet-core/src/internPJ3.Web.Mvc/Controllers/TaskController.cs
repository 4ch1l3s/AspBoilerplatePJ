using System.Threading.Tasks;
using internPJ3.Controllers;
using internPJ3.Tasks.DTO;
using internPJ3.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using internPJ3.Web.Models.Task;
using Acme.SimpleTaskApp.Common;
using internPJ3.Web.Models.People;
using System.Linq;
using Abp.Application.Services.Dto;
using internPJ3.Common;
using internPJ3.Products;
using internPJ3TaskApp.Tasks;
namespace internPJ3.Web.Controllers
{
    public class TaskController : internPJ3ControllerBase
	{
        private readonly ITaskService _taskAppService;
        private readonly ILookupAppService _lookupAppService;

        public TaskController(ITaskService taskAppService, ILookupAppService lookupAppService)
        {
            _taskAppService = taskAppService;
            _lookupAppService = lookupAppService;
        }   

        public async Task<ActionResult> Index(GetAllTasksInput input)
        {
            var output = (await _taskAppService.GetAll(input)).Items;
            var model = new TaskModel
            {
                TaskListDtos = output
            };
            return View(model); 
        }

        public async Task<ActionResult> Create()
        {
            //var peopleSelectListItems = (await _lookupAppService.GetPeopleComboboxItems()).Items
            //    .Select(p => p.ToSelectListItem())
            //    .ToList();

            //peopleSelectListItems.Insert(0, new SelectListItem { Value = string.Empty, Text = L("Unassigned"), Selected = true });

            //return View(new CreateTaskViewModel(peopleSelectListItems));
            return View();
        }
    }
}
