using System;
using System.Collections.Generic;
using Source.Codebase.Domain.Configs;
using Source.Codebase.Presentation;
using Source.Codebase.Services.Abstract;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<Type, object> _viewTemplateByTypes;

        public StaticDataService(LevelConfig levelConfig)
        {
            _viewTemplateByTypes = new()
            {
                { typeof(WallView), levelConfig.WallViewTemplate }
            };
        }

        public T GetViewTemplate<T>() where T : MonoBehaviour
        {
            if (_viewTemplateByTypes.TryGetValue(typeof(T), out object viewTemplate))
                return viewTemplate as T;

            throw new Exception($"Can't find viewTemplate with given type: {typeof(T)} ");
        }
    }
}
