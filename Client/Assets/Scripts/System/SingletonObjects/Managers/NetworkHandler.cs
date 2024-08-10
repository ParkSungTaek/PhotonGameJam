using Fusion.Sockets;
using Fusion;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Client.SystemEnum;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Fusion.Addons.Physics;
using static Unity.Burst.Intrinsics.X86.Avx;

namespace Client
{
    public class NetworkHandler : MonoBehaviour, INetworkRunnerCallbacks
    {
        public GameMode mode { get; set; }

        [SerializeField] private Player _playerPrefab;
        public Dictionary<PlayerRef, Player> _spawnedCharacters = new Dictionary<PlayerRef, Player>();

        CharacterInputHandler characterInputHandler;

        public NetworkRunner _runner;

        List<SessionInfo> _sessionList;

        public string lobbyName = "default";

        public void StartGame()
        {
            // Create the Fusion runner and let it know that we will be providing user input
            _runner = gameObject.GetComponent<NetworkRunner>();
            _runner.ProvideInput = true;


            _runner.JoinSessionLobby(SessionLobby.Shared, lobbyName);
        }

        public void RetrunLobby()
        {
            //_runner.Despawn(_runner.GetPlayerObject(_runner.LocalPlayer));
            ChatManager.Instance.SetState(OnlineState.Lobby);
            _runner.Shutdown(true, ShutdownReason.Ok);
        }

        public void CreateRandomSession()
        {
            MyInfoManager.Instance.SetMatchData(true, 2);

            if (MyInfoManager.Instance.GetMatchData().isMatch == false)
            {
                return;
            }

            foreach (SessionInfo sessionInfo in _sessionList)
            {
                if (sessionInfo.Name[0] != 'T')
                {
                    continue;
                }

                if (sessionInfo.IsOpen == false)
                {
                    continue;
                }

                if(sessionInfo.PlayerCount >= 2)
                {
                    continue;
                }

                var tmp = (int)sessionInfo.Properties["map"];
                JoinSession(sessionInfo.Name, GameMode.Shared, (Scenes)tmp);
            }

            string randomSessionName = CreadRandomSessionName(_sessionList);

            // �� ���� �̱�
            var map = GetRandomEnumValueInRange(Scenes.GrassStage, Scenes.MaxCount);

            CreateSession(randomSessionName, GameMode.Shared, map);
        }

        public void CreateTestSession()
        {
            //int randomInt = UnityEngine.Random.Range(1000, 9999);
            //string randomSessionName = "Room-" + randomInt.ToString();
            string randomSessionName = "TestRoom";
            JoinSession(randomSessionName, GameMode.Shared, Scenes.GrassStage);
        }

        public void CreateSession(string sessionName, GameMode mode, Scenes map)
        {
            var customProps = new Dictionary<string, SessionProperty>();
            customProps["map"] = (int)map;

            SceneManager.Instance.LoadScene(map);
            _runner.StartGame(new StartGameArgs()
            {
                Scene = SceneRef.FromIndex((int)map),
                SessionName = sessionName,
                GameMode = mode,
                SessionProperties = customProps,
            });
        }

        public void JoinSession(string sessionName, GameMode mode, Scenes map)
        {
            SceneManager.Instance.LoadScene(map);
            _runner.StartGame(new StartGameArgs()
            {
                Scene = SceneRef.FromIndex((int)map),
                SessionName = sessionName,
                GameMode = mode,
            });
        }

        public int GetSceneIndex(string sceneName)
        {
            for(int i=0; i< UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string name = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                if ( name == sceneName )
                {
                    return i;
                }
            }

            return -1;
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

        public async void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {

            // Step 3.
            // Shutdown the current Runner, this will not be used anymore. Perform any prior setup and tear down of the old Runner

            // The new "ShutdownReason.HostMigration" can be used here to inform why it's being shut down in the "OnShutdown" callback
            await runner.Shutdown(shutdownReason: ShutdownReason.HostMigration);

            // Step 4.
            // Create a new Runner.
            var newRunner = Instantiate(_runner);

            // setup the new runner...

            // Start the new Runner using the "HostMigrationToken" and pass a callback ref in "HostMigrationResume".
            StartGameResult result = await newRunner.StartGame(new StartGameArgs()
            {
                // SessionName = SessionName,              // ignored, peer never disconnects from the Photon Cloud
                // GameMode = gameMode,                    // ignored, Game Mode comes with the HostMigrationToken
                HostMigrationToken = hostMigrationToken,   // contains all necessary info to restart the Runner
                HostMigrationResume = HostMigrationResume, // this will be invoked to resume the simulation
                                                           // other args
            });

            // Check StartGameResult as usual
            if (result.Ok == false)
            {
                Debug.LogWarning(result.ShutdownReason);
            }
            else
            {
                Debug.Log("Done");
            }
        }

        void HostMigrationResume(NetworkRunner runner) 
        {
            // Get a temporary reference for each NO from the old Host
            foreach (var resumeNO in runner.GetResumeSnapshotNetworkObjects())
            {
                if (
                    // Extract any NetworkBehavior used to represent the position/rotation of the NetworkObject
                    // this can be either a NetworkTransform or a NetworkRigidBody, for example
                    resumeNO.TryGetBehaviour<NetworkRigidbody2D>(out var posRot)) 
                {

                    runner.Spawn(resumeNO, position: posRot.RBPosition, rotation: posRot.RBRotation, onBeforeSpawned: (runner, newNO) =>
                    {
                        // One key aspects of the Host Migration is to have a simple way of restoring the old NetworkObjects state
                        // If all state of the old NetworkObject is all what is necessary, just call the NetworkObject.CopyStateFrom
                        newNO.CopyStateFrom(resumeNO);

                        // and/or

                        // If only partial State is necessary, it is possible to copy it only from specific NetworkBehaviours
                        if (resumeNO.TryGetBehaviour<NetworkBehaviour>(out var myCustomNetworkBehaviour))
                        {
                            newNO.GetComponent<NetworkBehaviour>().CopyStateFrom(myCustomNetworkBehaviour);
                        }
                    });
                }
            }
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
            if (player == _runner.LocalPlayer)
            {
                Vector3 spawnPosition = new Vector3(0.0f, 20.0f, 0.0f);

                var map = (int)Scenes.InGame;
                SessionProperty tmpMap;
                if( _runner.SessionInfo.Properties.TryGetValue("map", out tmpMap) )
                {
                    map = (int)tmpMap;
                }

                switch ((Scenes)map)
                {
                    case Scenes.GrassStage:
                        if(player.PlayerId == 1)
                        {
                            spawnPosition = new Vector3(-16.5f, 2.0f, 0.0f);
                        }
                        else
                        {
                            spawnPosition = new Vector3(16.5f, 2.0f, 0.0f);
                        }
                        break;

                    case Scenes.GrassStage2:
                        if (player.PlayerId == 1)
                        {
                            spawnPosition = new Vector3(-14.7f, -7.0f, 0.0f);
                        }
                        else
                        {
                            spawnPosition = new Vector3(14.7f, -7.0f, 0.0f);
                        }
                        break;

                    default:
                        break;
                }
                
                Player networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);

                _spawnedCharacters.Add(player, networkPlayerObject);
                EntityManager.Instance.SpawnedCharacters.Add(player, networkPlayerObject);
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

        public string CreadRandomSessionName(List<SessionInfo> sessionList)
        {
            while(true) 
            {
                int randomInt = UnityEngine.Random.Range(1000, 9999);
                string randomSessionName = "Toom-" + randomInt.ToString();

                if (sessionList.Count <= 0)
                {
                    return randomSessionName;
                }

                foreach (SessionInfo sessionInfo in sessionList)
                {
                    if (string.Equals(sessionInfo.Name, randomSessionName) == false)
                    {
                        return randomSessionName;
                    }
                }
            }
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            Debug.Log($"Session List Updated with {sessionList.Count} session(s)");

            _sessionList = sessionList;
            return;
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            // Can check if the Runner is being shutdown because of the Host Migration
            if (shutdownReason == ShutdownReason.HostMigration)
            {
                // ...
            }
            else
            {
                SceneManager.Instance.LoadScene(SystemEnum.Scenes.Lobby);

            }
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
        }
    }
}