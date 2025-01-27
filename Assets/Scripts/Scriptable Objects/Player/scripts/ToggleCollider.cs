﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Game.States;

namespace Game.PlayerCharacter
{
    [CreateAssetMenu(fileName = "New State", menuName = "ability/Toggle Collider", order = 0)]
    public class ToggleCollider : StateData
    {
        public bool colliderSwitch;
        public bool onStart, onEnd;

        public override void OnEnter(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            Debug.Log(asi.IsName("HangingIdle") ? "turning collider off" : "turning collider on");

            if (onStart)
            {
                PlayerMovement p = c.GetPlayerMoveMent(a);
                toggleCollider(p);
            }

        }
        public override void OnAbilityUpdate(CharacterState c, Animator a, AnimatorStateInfo asi)
        {

        }

        public override void OnExit(CharacterState c, Animator a, AnimatorStateInfo asi)
        {
            if (onEnd)
            {
                PlayerMovement p = c.GetPlayerMoveMent(a);
                toggleCollider(p);
            }
        }

        private void toggleCollider(PlayerMovement p)
        {
            // rare null ref exception here
            p.RB.velocity = new Vector3(0,0,0);
            p.BoxCollider.enabled = colliderSwitch;
        }
    }
}
