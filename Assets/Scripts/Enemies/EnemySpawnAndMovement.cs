using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Unity.Collections;
using Unity.Jobs;
using UnityEditor;
using UnityEngine;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

namespace Enemies {
    public class EnemySpawnAndMovement : MonoBehaviour {
        public static Action<Transform> OnDeleteEnemy;
        public static Action OnKillAll;

        public ObjectPool[] objectPools;
        
        public Vector3 pyramidPosition;
        public Vector2 zoneRange;
        public int startValue = 10;
        public float spawnRate = 0.5f;
        
        private List<Transform> _enemies = new ();
        TransformAccessArray _transformAccessArray;
        
        private ObjectPool _objectPool;

        // when we want to change prefab for spawn
        public void ChangePool(int index) {
            _objectPool = objectPools[index];
        }
        
        public void Start() {
            ChangePool(0);
            _transformAccessArray = new TransformAccessArray(_enemies.ToArray());
            
            for (int i = 0; i < startValue; i++) {
                CreateEnemy();
            }

            OnDeleteEnemy += DeleteEnemy;
            OnKillAll += KillAll;
        }
        
        /// <summary>
        /// Create new enemy from random position and add to NativeArray
        /// </summary>
        private void CreateEnemy() {
            if (_enemies.Count >= 300) return;
            
            _enemies.Add(_objectPool.GetPooledObject().transform);
            _enemies[^1].gameObject.SetActive(true);
            var sign1 = Random.value < .5? 1 : -1;
            var sign2 = Random.value < .5? 1 : -1;
            _enemies[^1].position = new Vector3(sign1 * Random.Range(zoneRange.x, zoneRange.y), 0, sign2 * Random.Range(zoneRange.x, zoneRange.y));  
            _enemies[^1].rotation = Quaternion.LookRotation(pyramidPosition - _enemies[^1].position);
            
            _transformAccessArray.Dispose();
            _transformAccessArray = new TransformAccessArray(_enemies.ToArray());
        }

        private void KillAll() {
            foreach (var zombie in _enemies) {
                zombie.gameObject.SetActive(false);
            }
            
            _enemies.Clear();
            
            _transformAccessArray.Dispose();
            _transformAccessArray = new TransformAccessArray(_enemies.ToArray());
        }

        // delete enemy from NativeArray
        private void DeleteEnemy(Transform enemy) {
            _enemies.Remove(enemy);
            
            _transformAccessArray.Dispose();
            _transformAccessArray = new TransformAccessArray(_enemies.ToArray());
        }

        
        public void Update() {
            ChangeSpawnRateATime(Time.deltaTime);
            
            // direction and speed for each enemy
            NativeArray<Vector3> directions = new NativeArray<Vector3>(_enemies.Count, Allocator.TempJob);
            NativeArray<float> speed = new NativeArray<float>(_enemies.Count, Allocator.TempJob);
            
            for (int i = 0; i < directions.Length; i++) {
                directions[i] = (pyramidPosition - _enemies[i].position).normalized; // set direction to pyramid
                speed[i] = Random.Range(1f, 3f); // set random speed
            }
            
            // create job
            ZombieJob zombieJob = new ZombieJob {
                _speed = speed,
                _deltaTime = Time.deltaTime,
                directions = directions
            };
            
            JobHandle jobHandle = zombieJob.Schedule(_transformAccessArray);
            
            jobHandle.Complete();
            directions.Dispose();
            speed.Dispose();
        }

        
        // we need more enemies
        private float cooldown;
        private int countEnemySpawn;
        private float rate = 20;
        private void ChangeSpawnRateATime(float time) {
            cooldown += time;
            if (cooldown >= spawnRate) {
                cooldown = 0;
                CreateEnemy();
                countEnemySpawn++;
            }

            if (!(countEnemySpawn >= rate)) return;
            if (!(spawnRate > 0.5f)) return;
            spawnRate -= 0.1f;
            rate *= 0.61f;
        }
        
        private void OnDestroy() {
            _transformAccessArray.Dispose();
            OnDeleteEnemy -= DeleteEnemy;
            OnKillAll -= KillAll;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemySpawnAndMovement))]
public class EnemySpawnerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (GUILayout.Button("Change pool")) {
            ((EnemySpawnAndMovement) target).ChangePool(1);
        }
    }
}
#endif