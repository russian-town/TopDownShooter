using Source.Codebase.Controllers.Presenters;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;
using Source.Codebase.Services.Abstract;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class BulletFactory
    {
        private readonly IStaticDataService _staticDataService;

        public BulletFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Create(Vector3 position, Vector3 direction)
        {
            Bullet bullet = new(100f);
            BulletView viewTemplate =
                 _staticDataService.GetViewTemplate<BulletView>();
            BulletView view = Object.Instantiate(viewTemplate, position, Quaternion.identity);
            BulletPresenter bulletPresenter = new(bullet, view, direction);
            view.Construct(bulletPresenter);
        }
    }
}
