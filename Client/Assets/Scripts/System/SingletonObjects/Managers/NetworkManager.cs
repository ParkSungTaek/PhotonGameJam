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
    public class NetworkManager : Singleton<NetworkManager>
    {
        public GameMode mode {  get; set; }

        [SerializeField] private NetworkPrefabRef _playerPrefab;
        public Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

        private NetworkManager()
        {
        }
    }
}