using Source.Codebase.Controllers.Presenters;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;
using Source.Codebase.Services.Abstract;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class WallViewFactory
    {
        private readonly IStaticDataService _staticDataService;

        public WallViewFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Create(Wall wall, Vector2 position)
        {
            WallView template = _staticDataService.GetViewTemplate<WallView>();
            WallView view = Object.Instantiate(template, position, Quaternion.identity);
            WallPresenter presenter = new(wall, view);
            view.Construct(presenter);
        }
    }
}
