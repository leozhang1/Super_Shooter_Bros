﻿using Game.Enums;
using Game.Hash;
using Game.States;
using UnityEngine;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/force transition", order = 0)]
    public class ForceTransition : StateData
    {
        [Range(0, 1f)]
        public float transitionTime;

        override public void OnEnter(CharacterState character, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.jump], false);
        }

        override public void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            // timer
            if (asi.normalizedTime >= transitionTime)
            {
                a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.force_transition], true);
            }
        }

        override public void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(HashManager.Instance.animationParamsDict[AnimationParameters.force_transition], false);
        }
    }
}
