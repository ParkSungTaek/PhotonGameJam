using Fusion.Sockets;
using Fusion;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Client.SystemEnum;
using Unity.VisualScripting;

namespace Client
{
    public class NetworkHandler : MonoBehaviour, INetworkRunnerCallbacks
    {
        public GameMode mode { get; set; }

        [SerializeField] private Player _playerPrefab;
        public Dictionary<PlayerRef, Player> _spawnedCharacters = new Dictionary<PlayerRef, Player>();

        CharacterInputHandler characterInputHandler;
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
            if (characterInputHandler == null && EntityManager.Instance.MyPlayer != null)
            {
                characterInputHandler = EntityManager.Instance.MyPlayer.GetComponent<CharacterInputHandler>();
            }

            if (characterInputHandler != null)
            {
                input.Set(characterInputHandler.GetNetworkInput());
            }
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

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log("Player Join");
            if (runner.IsServer)
            {
                // Create a unique position for the player
                Vector3 spawnPosition = new Vector3(-0.1806704f, 0.688218f, 0.0f);
                Player networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player, (runner, o) =>
                {
                    //var naem = MyInfoManager.Instance.GetNickName();
                    //ChatManager.Instance.SendPublicChat(ChatManager.Instance.currentChannelName, $"Join Character Name: {MyInfoManager.Instance.GetNickName()}");
                    //Dictionary<DecoType, DecoData> decoData = MyInfoManager.Instance.GetDecoData();
                    //foreach( var data in decoData )
                    //{
                    //    o.GetComponent<Player>().SetDecoData(data.Key, data.Value);
                    //}
                    //o.GetComponent<Player>().SetNickName(MyInfoManager.Instance.GetNickName());
                });

                // Keep track of the player avatars for easy access
                _spawnedCharacters.Add(player, networkPlayerObject);
            }
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

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
        }
    }
}