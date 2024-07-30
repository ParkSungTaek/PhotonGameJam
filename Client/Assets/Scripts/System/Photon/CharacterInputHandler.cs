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
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.A))
                _moveInputVector = Vector3.left;

            if (Input.GetKey(KeyCode.D))
                _moveInputVector = Vector3.right;

            if (Input.GetKey(KeyCode.Space))
                isJumpButtonPressed = true;

            leftMouseButton = leftMouseButton || Input.GetMouseButton(0);
            rightButton = rightButton || Input.GetMouseButton(1);
        }

        public NetworkInputData GetNetworkInput()
        {
            NetworkInputData networkInputData = new NetworkInputData();

            networkInputData.movementInput = _moveInputVector;
            networkInputData.isJumpPressed = isJumpButtonPressed;

            networkInputData.buttons.Set(NetworkInputData.MOUSEBUTTON0, leftMouseButton);
            leftMouseButton = false;
            networkInputData.buttons.Set(NetworkInputData.MOUSEBUTTON1, rightButton);
            rightButton = false;

            _moveInputVector = Vector3.zero;
            isJumpButtonPressed = false;
            return networkInputData;
        }
    }
}
