using ET.Device;
using ET.Enums.Scenes;
using ET.Interface;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Core.LevelInfo
{
    public class InfoSceneObjects : MonoBehaviour, ISceneInformation
    {
        [SerializeField] private SceneIndex _levelIndex;

        [Header("References to player objects in the scene")]
        [SerializeField] private Transform _playerSpawnTarget;

        [Header("References to enemy objects in the scene")]
        [SerializeField] private Transform[] _enemySpawnTarget;

        [Header("References to static level objects in the scene")]
        [SerializeField] private GameObject _levelStructure;

        public SceneIndex LevelIndex { get => _levelIndex; }

        public Transform PlayerSpawnTarget { get => _playerSpawnTarget; }
    }
}
