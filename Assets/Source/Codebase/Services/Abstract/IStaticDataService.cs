using UnityEngine;

namespace Source.Codebase.Services.Abstract
{
    public interface IStaticDataService
    {
        public T GetViewTemplate<T>() where T : MonoBehaviour;
    }
}
