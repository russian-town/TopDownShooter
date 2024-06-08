using Source.Codebase.Controllers.Presenters;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;
using Source.Codebase.Services.Abstract;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class BulletViewFactory
    {
        private readonly IStaticDataService _staticDataService;

        public BulletViewFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Create(Bullet bullet)
        {
           BulletView viewTemplate =
                _staticDataService.GetViewTemplate<BulletView>();
            BulletView view = Object.Instantiate(viewTemplate);
            BulletPresenter bulletPresenter = new(bullet, view);
            view.Construct(bulletPresenter);
        }
    }
}
