using System;
using System.Collections.Generic;
using Source.Codebase.Domain.Configs;
using Source.Codebase.Domain.Models;
using Source.Codebase.Presentation;
using Source.Codebase.Services.Abstract;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<Type, object> _viewTemplateByTypes;
        private Dictionary<Type, EntityConfig> _entityConfigByType;

        public StaticDataService(LevelConfig levelConfig)
        {
            _entityConfigByType = new()
            {
                {typeof(Player), levelConfig.PlayerConfig },
                {typeof(Enemy), levelConfig.EnemyConfig }
            };

            _viewTemplateByTypes = new()
            {
                { typeof(WallView), levelConfig.WallViewTemplate },
                { typeof(PlayerView), levelConfig.PlayerViewTemplate },
                { typeof(EnemyView), levelConfig.EnemyViewTemplate },
                { typeof(BulletView), levelConfig.BulletViewTemplate }
            };
        }

        public EntityConfig GetEntityConfigByType(Type type) 
        {
            if (_entityConfigByType.ContainsKey(type) == false)
                throw new Exception($"EntityConfig for {type} does not exist!");

            return _entityConfigByType[type];
        }

        public T GetViewTemplate<T>() where T : MonoBehaviour
        {
            if (_viewTemplateByTypes.TryGetValue(typeof(T), out object viewTemplate))
                return viewTemplate as T;

            throw new Exception($"Can't find viewTemplate with given type: {typeof(T)} ");
        }
    }
}
