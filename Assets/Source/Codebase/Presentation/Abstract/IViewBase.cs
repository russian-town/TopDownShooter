using Source.Codebase.Controllers.Presenters.Abstract;

namespace Source.Codebase.Presentation.Abstract
{
    public interface IViewBase
    {
        public void Construct(IPresenter presenter);
    }
}