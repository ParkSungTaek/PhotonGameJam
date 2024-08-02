using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public struct NetworkInputData : INetworkInput
{
    public const byte MOUSEBUTTON0 = 1;
    public const byte MOUSEBUTTON1 = 2;

    public NetworkButtons buttons;

    public Vector3 movementInput; // 이동
    public NetworkBool isJumpPressed; // 점프
    public NetworkBool isFirePressed; // 총 발사
    public Vector3 lookDirection; // 이동

}
