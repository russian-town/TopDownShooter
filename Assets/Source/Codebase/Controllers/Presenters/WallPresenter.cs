using Source.Codebase.Controllers.Presenters.Abstract;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;

namespace Source.Codebase.Controllers.Presenters
{
    public class WallPresenter : IPresenter
    {
        private readonly Wall _wall;
        private readonly WallView _view;

        public WallPresenter(Wall wall, WallView view)
        {
            _wall = wall;
            _view = view;
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}
