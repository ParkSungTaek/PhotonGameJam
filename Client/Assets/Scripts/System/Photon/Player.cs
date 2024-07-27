using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using static Fusion.Sockets.NetBitBuffer;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;

public class Player : NetworkBehaviour
{
    private NetworkCharacterController _cc;
    Vector3 direction;
    Animator anim;
    public Camera Camera;
    [SerializeField] private Ball _prefabBall;
    [SerializeField] private PhysxBall _prefabPhysxBall;
    private Vector3 _forward = Vector3.forward;
    [Networked] private TickTimer delay { get; set; }

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData data))
        {
            data.direction.Normalize();
            direction = data.direction;

            _cc.Move(5 * data.direction * Runner.DeltaTime);

            Vector2 a = new Vector2(data.direction.x, data.direction.z);
            float walkSpeed = Mathf.Clamp01(a.magnitude);

            anim.SetFloat("walkSpeed", walkSpeed);

            if (data.direction.sqrMagnitude > 0)
                _forward = data.direction;

            if (HasStateAuthority && delay.ExpiredOrNotRunning(Runner))
            {
                if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON0))
                {
                    delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
                    Runner.Spawn(_prefabBall,
                      transform.position + _forward,
                      Quaternion.LookRotation(_forward),
                      Object.InputAuthority,
                      (runner, o) =>
                      {
                          // Initialize the Ball before synchronizing it
                          o.GetComponent<Ball>().Init();
                      });
                }
                else if (data.buttons.IsSet(NetworkInputData.MOUSEBUTTON1))
                {
                    delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
                    Runner.Spawn(_prefabPhysxBall,
                      transform.position + _forward,
                      Quaternion.LookRotation(_forward),
                      Object.InputAuthority,
                      (runner, o) =>
                      {
                          o.GetComponent<PhysxBall>().Init(10 * _forward);
                      });
                }
            }
        }
    }

    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            Camera = Camera.main;
            Camera.GetComponent<FirstPersonCamera>().transform.rotation = Quaternion.Euler(60.0f, 0.0f, 0.0f);
            Camera.GetComponent<FirstPersonCamera>().Target = transform;
        }
    }
}
