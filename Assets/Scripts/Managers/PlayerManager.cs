using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{

    public class PlayerManager : MonoBehaviour
    {
    
        public bool isGameClear = false;
        public bool isEndClearMove = false;

        public GameObject playSceneManager;
        public GameObject balloon;

        private void Update()
        {
            if (playSceneManager.GetComponent<Managers.PlaySceneManager>().isGameClear)
            {
                SetIsGameClear(true);
            }

            if (balloon && balloon.GetComponent<Controllers.BalloonController>().isEndClearMove)
            {
                SetIsEndClearMove(true);
            }
        }

        private void SetIsGameClear(bool flag)
        {
            isGameClear = flag;
        }

        private void SetIsEndClearMove(bool flag)
        {
            isEndClearMove = flag;
        }
    }
}
