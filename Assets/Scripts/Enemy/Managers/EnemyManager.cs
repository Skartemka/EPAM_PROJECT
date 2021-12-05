using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ET.Enemy.AI;

namespace ET.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        private Transform _playerTransform = null;
        private readonly List<GameObject> _listEnemies = new List<GameObject>();
        private Transform[] _spawnTarget = null;

        private int _childCountParent = 0;
        private float _timer = 0f;
        private float _timeRespawn = 20f;

        [Header("Prefab Enemy")]
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private EnemyStateController _enemyStateController;

        public EnemyStateController EnemyStateController { get => _enemyStateController; }
        public Transform PlayerTransform { get => _playerTransform;} 

        protected void Start()
        {
            if (_playerTransform)
            {
                InitializeTargetsSpawn();
                StartCoroutine(CreateSpawnPoints());
            }
        }

        protected void Update()
        {
            if (_playerTransform)
            {
                RespawnEnemies();
            }
        }

        private void InitializeTargetsSpawn()
        {
            _childCountParent = transform.childCount;

            _spawnTarget = new Transform[_childCountParent];

            for (int i = 0; i < _childCountParent; i++)
            {
                _spawnTarget[i] = gameObject.GetComponentInChildren<Transform>().GetChild(i);
            }
        }

        public void GetPlayerPosition(Transform target)
        {
            _playerTransform = target;
        }

        private GameObject CreateEnemy(Transform target)
        {
            var enemy = Instantiate(_enemyPrefab, target.position, Quaternion.identity);

            return enemy;
        }

        private IEnumerator CreateSpawnPoints()
        {
            for (int i = 0; i < _spawnTarget.Length; i++)
            {
                _listEnemies.Add(CreateEnemy(_spawnTarget[i]));
                _listEnemies[i].GetComponent<EnemyStateController>().GetPlayerPosition(_playerTransform);

                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }

        private void RespawnEnemies()
        {
            _timer += Time.deltaTime;

            if (_timer > _timeRespawn)
            {
                _listEnemies.Clear();

                if (_listEnemies.Count == 0)
                {
                    transform.position = _playerTransform.position;

                    StartCoroutine(CreateSpawnPoints());
                }
                _timer = 0f;
            }
        }
    }
}
