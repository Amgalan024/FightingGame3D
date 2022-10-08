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
        [SerializeField] private List<DirectionTweenVector> _jumpTweenData;
        [SerializeField] private List<DirectionTweenVector> _fallTweenData;

        public List<DirectionTweenVector> JumpTweenData => _jumpTweenData;
        public List<DirectionTweenVector> FallTweenData => _fallTweenData;

        public TweenVectorData GetTweenDataByDirection(IEnumerable<DirectionTweenVector> list,
            DirectionType directionType)
        {
            return list.FirstOrDefault(d => d.DirectionType == directionType)?.TweenVectorData;
        }
    }
}