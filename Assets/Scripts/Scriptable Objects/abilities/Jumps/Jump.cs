using UnityEngine;

namespace Game.PlayerCharacter
{

    [CreateAssetMenu(fileName = "New State", menuName = "ability/jump", order = 0)]
    public class Jump : StateData
    {
        [Range(1, 10)]
        public float jumpForce;

        override public void OnEnter(PlayerState character, Animator a, AnimatorStateInfo asi)
        {
            // ensure we don't play the landing animation too early
            a.SetBool(AnimationParameters.isGrounded.ToString(), false);

            // get player rigidbody and apply force to the jump
            Rigidbody rb = character.GetPlayerMoveMent(a).RB;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        override public void UpdateAbility(PlayerState c, Animator a, AnimatorStateInfo asi)
        {

        }

        override public void OnExit(PlayerState c, Animator a, AnimatorStateInfo asi)
        {
            a.SetBool(AnimationParameters.jump.ToString(), false);
        }

    }




        #region old code
        // [Range(1, 10)]
        // public float jumpForce = 5f;
        // Rigidbody rb;
        // bool isGrounded = true;
        // bool shouldJump;
        // readonly string jumpInput = "Jump";

        // void Awake()
        // {
        //     rb = GetComponent<Rigidbody>();
        // }

        // void Update()
        // {
        //     if (isGrounded)
        //     {
        //         shouldJump = Input.GetButtonDown(jumpInput);
        //     }
        //     else
        //     {
        //         shouldJump = false;
        //     }
        // }

        // void FixedUpdate()
        // {
        //     if (shouldJump)
        //     {
        //         print("jump");
        //         rb.velocity = new Vector3(0, 10, 0);
        //     }
        // }

        // void OnCollisionEnter(Collision c)
        // {
        //     if (c.gameObject.CompareTag("ground"))
        //     {
        //         isGrounded = true;
        //     }
        // }

        // void OnCollisionExit(Collision c)
        // {
        //     if (c.gameObject.CompareTag("ground"))
        //     {
        //         isGrounded = false;
        //     }
        // }
        #endregion
}
