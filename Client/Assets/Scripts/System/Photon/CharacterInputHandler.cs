using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class CharacterInputHandler : MonoBehaviour
    {
        Vector3 _moveInputVector = Vector3.zero;
        bool isJumpButtonPressed = true;
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


        }

        public NetworkInputData GetNetworkInput()
        {
            NetworkInputData networkInputData = new NetworkInputData();

            networkInputData.movementInput = _moveInputVector;
            networkInputData.isJumpPressed = isJumpButtonPressed;

            isJumpButtonPressed = false;
            return networkInputData;
        }
    }
}
