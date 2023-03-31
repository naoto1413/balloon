using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class SpacianController : MonoBehaviour
    {
        public float jumpingPower;
        private Rigidbody2D rb2d;

        private GameObject clearSceneManager;

        private int jumpCount = 0;
        private bool isAvailableJump = true;

        private const int JUMP_MAX_COUNT = 2;

        public bool isEndJumpFlag = false;

        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            clearSceneManager = GameObject.Find("ClearSceneManager");
        }

        void Update()
        {
            if (CheckJumpSpacianFlag())
            {
                Jump();
            }

            if (jumpCount >= JUMP_MAX_COUNT && rb2d.IsSleeping())
            {
                SetIsEndJumpFlag(true);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (jumpCount < JUMP_MAX_COUNT && CheckJumpSpacianFlag())
            {
                SetIsAvailableJump(true);
                Jump();
            }
        }

        private void Jump()
        {
            if (GetIsAvailableJump())
            {
                rb2d.velocity = new Vector3(0, 100, 0);
                rb2d.AddForce(transform.up * jumpingPower);
                jumpCount++;
                SetIsAvailableJump(false);
            }
        }

        private bool CheckJumpSpacianFlag()
        {
            bool returnFlag = false;

            bool clearSceneManagerFlag = clearSceneManager.GetComponent<Managers.ClearSceneManager>().jumpSpacianFlg;
            
            if(clearSceneManagerFlag)
            {
                returnFlag = true;
            }
            
            return returnFlag;
        }

        private void SetIsAvailableJump(bool flag)
        {
            isAvailableJump = flag;
        }

        private bool GetIsAvailableJump()
        {
            return isAvailableJump;
            
        }

        private void SetIsEndJumpFlag(bool flag)
        {
            isEndJumpFlag = flag;
        }
    }
}

