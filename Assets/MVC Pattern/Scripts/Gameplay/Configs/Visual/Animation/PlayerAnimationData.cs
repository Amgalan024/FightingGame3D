using System;
using System.Collections.Generic;
using System.Linq;
using MVC.Configs.Enums;
using UnityEngine;

namespace MVC.Configs.Animation
{
    [Serializable]
    public class PlayerAnimationData
    {
        [SerializeField] private List<TweenWithDirectionConfig> _jumpTweenData;
        [SerializeField] private List<TweenWithDirectionConfig> _fallTweenData;

        public List<TweenWithDirectionConfig> JumpTweenData => _jumpTweenData;
        public List<TweenWithDirectionConfig> FallTweenData => _fallTweenData;

        public TweenConfig GetTweenDataByMovementType(IEnumerable<TweenWithDirectionConfig> list,
            MovementType movementType)
        {
            return list.FirstOrDefault(d => d.MovementType == movementType)?.TweenConfig;
        }
    }
}