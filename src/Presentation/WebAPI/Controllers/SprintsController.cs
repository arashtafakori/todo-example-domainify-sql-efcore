using Contract;
using Domain.ProjectSettingAggregation;
using Domainify.AspMvc;
using Domainify.Domain;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.WebAPI
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SprintsController : ApiController
    {
        private readonly IProjectSettingService _projectSettingService;

        public SprintsController(IProjectSettingService service)
        {
            _projectSettingService = service;
        }
        [HttpGet($"/v1/{nameof(ProjectsController)}/{{{nameof(GetSprintsList.ProjectId)}}}/[controller]")]
        public async Task<ActionResult<PaginatedList<SprintViewModel>>> GetList(
            string projectId)
        {
            var request = GetRequest<GetSprintsList>()
                           .SetProjectId(projectId);
            return await _projectSettingService.Process(request);
        }
        [HttpGet($"/v1.1/{nameof(ProjectsController)}/{{{nameof(GetSprintsList.ProjectId)}}}/[controller]")]
        public async Task<ActionResult<PaginatedList<SprintViewModel>>> GetList(
            string projectId, 
            int? pageNumber = null,
            int? pageSize = null, 
            bool? withTasks = null)
        {
            var request = GetRequest<GetSprintsList>()
                           .SetProjectId(projectId);
            request.Setup(
                paginationSetting: new PaginationSetting(
                    defaultPageNumber: 1, defaultPageSize: 10));

            return await _projectSettingService.Process(request);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SprintViewModel?>> Get(string id, bool? withTasks = null)
        {
            var request = GetRequest<GetSprint>();
            request.SetId(id);

            return await _projectSettingService.Process(request);
        }

        [HttpPost]
        public async Task<ActionResult<SprintViewModel?>> Define(DefineSprint request)
        {
            var id = await _projectSettingService.Process(request);
            return StatusCode(201, id);
        }

        [HttpPatch("[action]")]
        public async Task<IActionResult> ChangeSprintName(ChangeSprintName request)
        {
            await _projectSettingService.Process(request);
            return NoContent();
        }
        [HttpPatch("[action]")]
        public async Task<IActionResult> ChangeSprintTimeSpan(ChangeSprintTimeSpan request)
        {
            await _projectSettingService.Process(request);
            return NoContent();
        }
        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> Delete(
            string id, bool toDeleteAllTaskStatus)
        {
            var request = GetRequest<DeleteSprint>();
            request.SetId(id);

            await _projectSettingService.Process(request);
            return NoContent();
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> CheckSprintForDeletingPermanently(string id)
        {
            await _projectSettingService.Process(new CheckSprintForDeletingPermanently(id));
            return NoContent();
        }
        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> Restore(string id)
        {
            await _projectSettingService.Process(new RestoreSprint(id));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermanently(
            string id, bool toDeleteAllTaskStatus)
        {
            var request = GetRequest<DeleteSprintPermanently>();
            request.SetId(id);

            await _projectSettingService.Process(request);
            return NoContent();
        }
    }
}