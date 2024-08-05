using Fusion.Sockets;
using Fusion;
using System.Collections.Generic;
using System;
using UnityEngine;
using static Client.SystemEnum;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Fusion.Addons.Physics;
using UnityEditor;

namespace Client
{
    public class NetworkHandler : MonoBehaviour, INetworkRunnerCallbacks
    {
        public GameMode mode { get; set; }

        [SerializeField] private Player _playerPrefab;
        public Dictionary<PlayerRef, Player> _spawnedCharacters = new Dictionary<PlayerRef, Player>();

        CharacterInputHandler characterInputHandler;

        public NetworkRunner _runner;

        public string lobbyName = "default";

        public async void StartGame(GameMode mode)
        {
            // Create the Fusion runner and let it know that we will be providing user input
            _runner = gameObject.GetComponent<NetworkRunner>();
            _runner.ProvideInput = true;


            _runner.JoinSessionLobby(SessionLobby.Shared, lobbyName);
            
        }

        public void RetrunLobby()
        {
            //_runner.Despawn(_runner.GetPlayerObject(_runner.LocalPlayer));
            _runner.Shutdown(true, ShutdownReason.Ok);
        }

        public void CreateRandomSession()
        {
            //int randomInt = UnityEngine.Random.Range(1000, 9999);
            //string randomSessionName = "Room-" + randomInt.ToString();
            string randomSessionName = "TestRoom";
            SceneManager.Instance.LoadScene(SystemEnum.Scenes.InGame);
            _runner.StartGame(new StartGameArgs()
            {
                Scene = SceneRef.FromIndex((int)SystemEnum.Scenes.InGame),
                SessionName = randomSessionName,
                GameMode = GameMode.Shared,
            }) ;
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
                Vector3 spawnPosition = new Vector3(-0.1806704f, 0.688218f, 0.0f);
                Player networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);

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
            Debug.Log("Session List Update");
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