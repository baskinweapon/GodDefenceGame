
using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

namespace Enemies {
    [RequireComponent(typeof(ObjectPool))]
    public class ZombieSpawner : MonoBehaviour {
        public static Action<Transform> OnDeleteEnemy;
        public static Action OnKillAll;

        public Vector3 pyramidPosition;
        public Vector2 zoneRange;
        public float speed;
        public int startValue = 10;

        private ObjectPool _objectPool;
        
        private List<Transform> _zombies = new List<Transform>();
        TransformAccessArray _transformAccessArray;
        
        
        public void Start() {
            _objectPool = GetComponent<ObjectPool>();
            _transformAccessArray = new TransformAccessArray(_zombies.ToArray());
            
            for (int i = 0; i < startValue; i++) {
                CreateEnemy();
            }

            OnDeleteEnemy += DeleteEnemy;
            OnKillAll += KillAll;
            InvokeRepeating(nameof(CreateEnemy), 0f, 0.5f);
        }

        public void CreateEnemy() {
            if (_zombies.Count >= 300) return;
            
            _zombies.Add(_objectPool.GetPooledObject().transform);
            _zombies[^1].gameObject.SetActive(true);
            var sign_1 = Random.value < .5? 1 : -1;
            var sign_2 = Random.value < .5? 1 : -1;
            _zombies[^1].position = new Vector3(sign_1 * Random.Range(zoneRange.x, zoneRange.y), 0, sign_2 * Random.Range(zoneRange.x, zoneRange.y));  
            _zombies[^1].rotation = Quaternion.LookRotation(pyramidPosition - _zombies[^1].position);
            
            _transformAccessArray.Dispose();
            _transformAccessArray = new TransformAccessArray(_zombies.ToArray());
        }

        public void KillAll() {
            foreach (var zombie in _zombies) {
                zombie.gameObject.SetActive(false);
            }
            
            _zombies.Clear();
            
            _transformAccessArray.Dispose();
            _transformAccessArray = new TransformAccessArray(_zombies.ToArray());
        }
        
        public void DeleteEnemy(Transform enemy) {
            _zombies.Remove(enemy);
            
            _transformAccessArray.Dispose();
            _transformAccessArray = new TransformAccessArray(_zombies.ToArray());
        }
        
        public void Update() {
            NativeArray<Vector3> directions = new NativeArray<Vector3>(_zombies.Count, Allocator.TempJob);
            
            for (int i = 0; i < directions.Length; i++) {
                directions[i] = (pyramidPosition - _zombies[i].position).normalized;
            }
            
            ZombieJob zombieJob = new ZombieJob {
                _speed = speed,
                _deltaTime = Time.deltaTime,
                directions = directions
            };
            
            JobHandle jobHandle = zombieJob.Schedule(_transformAccessArray);
            
            jobHandle.Complete();
            directions.Dispose();
            
        }
        
        private void OnDestroy() {
            _transformAccessArray.Dispose();
            OnDeleteEnemy -= DeleteEnemy;
            OnKillAll -= KillAll;
        }
    }
}