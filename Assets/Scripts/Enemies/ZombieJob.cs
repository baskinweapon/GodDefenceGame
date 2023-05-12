using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Enemies {
    public struct ZombieJob : IJobParallelForTransform {
        public float _speed;
        public float _deltaTime;

        public NativeArray<Vector3> directions;

        public void Execute(int index, TransformAccess transform) {
            transform.position += directions[index] * _speed * _deltaTime;
        }
    }
}