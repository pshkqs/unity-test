using System.Collections;
using UnityEngine;

namespace Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Circle _prefab;
        [SerializeField] [Range(0f, 0.5f)] private float _constraint; // from corners
        [SerializeField] [Range(0f, 255f)] private float _minColor;
        [SerializeField] private AnimationCurve _speedRate;
        [SerializeField] private AnimationCurve _spawnDelayRate;
        [SerializeField] private Player _player;
    
        private Camera _camera;
        private bool _spawning;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void StartSpawning() => StartCoroutine(SpawnLoop());

        public void Stop() => _spawning = false;

        private IEnumerator SpawnLoop()
        {
            _spawning = true;
            
            while (_spawning)
            {
                Spawn();
                yield return new WaitForSeconds(_spawnDelayRate.Evaluate(_player.Score));
            }
        }

        private void Spawn()
        {
            var startPosition = GetRandomPosition(1f);
            var destination = GetRandomPosition(0f);

            var settings = new CircleSettings()
            {
                Destination = destination,
                Speed = _speedRate.Evaluate(_player.Score), 
                Color = GetRandomColor(),
                Damage = Random.Range(1, 6),
                Score = Random.Range(1, 4),
                Destroyed = _player.AddScore,
                ReachedDestination = _player.TakeDamage
            };
        
            Instantiate(_prefab, startPosition, Quaternion.identity).Init(settings);
        }

        private Vector3 GetRandomPosition(float screenY) 
            => _camera.ViewportToWorldPoint(new Vector3(Random.Range(0f + _constraint, 1f - _constraint), screenY, 50f));

        private Color GetRandomColor()
        {
            var colors = new float[3];

            for(var i = 0; i < 3; i++)
            {
                if (Random.Range(0, 3) == i)
                    colors[i] = _minColor / 255;
                else
                    colors[i] = Random.value;
            }
        
            return new Color(colors[0], colors[1], colors[2]);
        }
    }
}