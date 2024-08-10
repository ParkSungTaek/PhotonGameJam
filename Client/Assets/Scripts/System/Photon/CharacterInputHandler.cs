using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class CharacterInputHandler : MonoBehaviour
    {
        Vector3 _moveInputVector = Vector3.zero; // 이동
        bool isJumpButtonPressed = true; // 점프

        private bool leftMouseButton; // 왼쪽 마우스 버튼
        private bool rightButton; // 오른쪽 마우스 버튼

        // TODO 김선중 임시로 만듦 테스트용
        bool changeBullet = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // TODO 김선중 죽었을 때 input 못 받게
            var player = EntityManager.Instance.MyPlayer;
            if (player != null ) 
            {
                if (player.PlayerInfo == null) return;
                if (player.PlayerInfo.IsLive == false) return;
            }

            if (Input.GetKey(KeyCode.A))
                _moveInputVector = Vector3.left;

            if (Input.GetKey(KeyCode.D))
                _moveInputVector = Vector3.right;

            if (Input.GetKey(KeyCode.Space))
                isJumpButtonPressed = true;

            // TODO 김선중 임시용 나중에 삭제 예정
            if (Input.GetKey(KeyCode.B))
                changeBullet = true;


            leftMouseButton = leftMouseButton || Input.GetMouseButton(0);
            rightButton = rightButton || Input.GetMouseButton(1);
        }

        public NetworkInputData GetNetworkInput()
        {
            NetworkInputData networkInputData = new NetworkInputData();

            networkInputData.movementInput = _moveInputVector;
            networkInputData.isJumpPressed = isJumpButtonPressed;

            // TODO 김선중 임시용 나중에 삭제 예정
            networkInputData.isChangeBullet = changeBullet;
            changeBullet = false;

            networkInputData.buttons.Set(NetworkInputData.MOUSEBUTTON0, leftMouseButton);
            leftMouseButton = false;
            networkInputData.buttons.Set(NetworkInputData.MOUSEBUTTON1, rightButton);
            rightButton = false;

            networkInputData.lookDirection = GameManager.Instance.LookDirection; 
            _moveInputVector = Vector3.zero;
            isJumpButtonPressed = false;
            return networkInputData;
        }
    }
}