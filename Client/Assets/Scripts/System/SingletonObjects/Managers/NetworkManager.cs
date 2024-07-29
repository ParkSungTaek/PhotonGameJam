using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using System.Text;
using UnityEngine.Networking;
using Fusion;
using Fusion.Sockets;



namespace Client
{
    public class NetworkManager : Singleton<NetworkManager>, INetworkRunnerCallbacks
    {
        public GameMode mode {  get; set; }

        private NetworkManager()
        {
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            //var data = new NetworkInputData();

            //if (Input.GetKey(KeyCode.W))
            //    data.direction += Vector3.forward;

            //if (Input.GetKey(KeyCode.S))
            //    data.direction += Vector3.back;

            //if (Input.GetKey(KeyCode.A))
            //    data.direction += Vector3.left;

            //if (Input.GetKey(KeyCode.D))
            //    data.direction += Vector3.right;

            //data.buttons.Set(NetworkInputData.MOUSEBUTTON0, _mouseButton0);
            //_mouseButton0 = false;
            //data.buttons.Set(NetworkInputData.MOUSEBUTTON1, _mouseButton1);
            //_mouseButton1 = false;

            //input.Set(data);
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        [SerializeField] private NetworkPrefabRef _playerPrefab;
        public Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log("Player Join");
            //if (runner.IsServer)
            //{
            //    // Create a unique position for the player
            //    Vector3 spawnPosition = new Vector3(92.8f, 0.5f, -2.0f);
            //    NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            //    // Keep track of the player avatars for easy access
            //    _spawnedCharacters.Add(player, networkPlayerObject);
            //}
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log("Player Left");
            //if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
            //{
            //    runner.Despawn(networkObject);
            //    _spawnedCharacters.Remove(player);
            //}
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }
    }
}