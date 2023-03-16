
namespace Beis.WebApplication.Controllers
{
    public abstract class BaseController<T> : Controller where T : class, new()
    {
        protected ILogger _logger;
        protected IHttpContextAccessor _httpContextAccessor;
        protected ISessionService _sessionService;

        protected WebApplicationDto dto;

        public BaseController(ILogger logger, IHttpContextAccessor httpContextAccessor, ISessionService sessionService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _sessionService = sessionService;
        }

        public abstract void MapModelToDto(T model);
        public abstract T MapDtoToModel(T model);

        protected bool LoadDtoFromSession() => _sessionService.TryGet<WebApplicationDto>(nameof(WebApplicationDto), _httpContextAccessor.HttpContext, out dto);          
        protected void StoreDtoToSession() => _sessionService.Set(nameof(WebApplicationDto), dto, _httpContextAccessor.HttpContext);

        protected T LoadDtoAndModelFromSession()
        {
            var model = new T();
            if (LoadDtoFromSession())
            {
                MapDtoToModel(model);
            }
            return model;
        }

        protected T LoadDtoAndModelFromSession(T model)
        {
            if (LoadDtoFromSession())
            {
                MapDtoToModel(model);
            }
            return model;
        }

        protected Result StoreDtoAndModelToSession(T model)
        {
            if (LoadDtoFromSession())
            {
                MapModelToDto(model);
                StoreDtoToSession();
                return Result.Ok();
            }
            else
            {
                ModelState.Clear();
                
                ModelState.AddModelError("Unexpected Error", "There was an unexpected error.");
                var reason = "There was a problem getting the dto from the session.";
                _logger.LogError(reason);
                return Result.Fail(reason);
            }
        }
    }
}

