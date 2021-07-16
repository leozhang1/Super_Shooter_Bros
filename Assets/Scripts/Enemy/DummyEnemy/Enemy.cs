using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Game.Interfaces;
using Game.HealthManager;
using Game.Pooling;
using System.Linq;

namespace Game.EnemyAI
{
    public class Enemy : PoolableObject, IDamageable
    {
        [Header("Custom Script Components")]
        public EnemyAIController aiController;
        public AttackRadius attackRadius;

        [Space(10)]
        public NavMeshAgent agent;
        public float enemyHealth = 100;
        private const string attack = "attack", stopAttack = "stopAttack";
        public Animator animator;
        Coroutine lookRoutine;
        Coroutine handleDeathRoutine;
        Rigidbody rb = null;
        [SerializeField] GameObject enemyRobot = null;
        [SerializeField] GameObject enemyRobotRagdoll = null;
        [SerializeField] BoxCollider[] enemyColliders = null;

        void onAttack(IDamageable target)
        {
            animator.ResetTrigger(stopAttack);
            animator.SetTrigger(attack);
            if (lookRoutine != null)
            {
                StopCoroutine(lookRoutine);
            }

            lookRoutine = StartCoroutine(lookAt(target.getTransform()));
        }
        
        IEnumerator lookAt(Transform target)
        {
            Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);

            float time = 0;
            while (time < 1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
                time += Time.deltaTime * 2;
                yield return null;
            }

            transform.rotation = lookRotation;
        }

        void OnEnable()
        {
            rb = GetComponent<Rigidbody>();
            attackRadius.OnAttack += onAttack;

            // re-parent the ragdoll to this game object since this enemy is being respawned
            if (enemyRobotRagdoll.transform.parent != transform)
            {
                enemyRobotRagdoll.transform.SetParent(transform);
                enemyRobotRagdoll.SetActive(false);
            }
        }

        public override void OnDisable()
        {
            base.OnDisable();

            attackRadius.OnAttack -= onAttack;
            agent.enabled = false;
        }

        void OnDestroy()
        {
            UnityEngine.Debug.Log($"{gameObject.name} destroyed");
        }

        public void takeDamage(float damage)
        {
            enemyHealth = Mathf.Max(enemyHealth - damage, 0f);

            if (enemyHealth <= 0f)
            {
                if (handleDeathRoutine != null) StopCoroutine(handleDeathRoutine);

                handleDeathRoutine = StartCoroutine(handleDeath());
            }
        }

        IEnumerator handleDeath()
        {
            // turn on ragdoll and turn off player robot mesh
            enemyRobotRagdoll.SetActive(true);
            HealthDamageManager.instance.copyTransformData(enemyRobot.transform, enemyRobotRagdoll.transform, rb.velocity);
            // enemyRobotRagdoll.transform.parent = null;
            rb.velocity = Vector3.zero;
            yield return null;

            // turn off enemy colliders and navmesh agent
            agent.enabled = false;
            foreach (var collider in enemyColliders)
            {
                collider.enabled = false;
            }


            
            enemyRobot.SetActive(false);

            yield return new WaitForSeconds(3f);
            
            this.gameObject.SetActive(false);
        }

        public Transform getTransform()
        {
            return transform;
        }
    }
}
