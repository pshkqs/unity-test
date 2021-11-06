using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public struct Score
    {
        public int Current => _current;

        public int Max
        {
            get => PlayerPrefs.GetInt("max-score");
            private set => PlayerPrefs.SetInt("max-score", value);
        }

       private int _current;

        public void AddScore(int value)
        {
            _current += value;

            if (_current > Max)
                Max = _current;
        }
    }

    public class Player : MonoBehaviour
    {
        public int Score => _score.Current;
        
        private Score _score;
        [SerializeField] private int _health;

        [SerializeField] private Spawner _spawner;
        
        [SerializeField] private UnityEvent<int> _healthChanged;
        [SerializeField] private UnityEvent<Score> _scoreChanged;
        [SerializeField] private UnityEvent<Score> _gameOver;

        private void Start()
        {
            _healthChanged?.Invoke(_health);
            _scoreChanged?.Invoke(_score);
            _spawner.StartSpawning();
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0) return;

            _health -= damage;

            if (_health < 0)
                _health = 0;

            if (_health == 0)
            {
                _gameOver?.Invoke(_score);
                _spawner.Stop();
            }

            _healthChanged?.Invoke(_health);
        }

        public void AddScore(int value)
        {
            if (value <= 0) return;

            _score.AddScore(value);
            _scoreChanged?.Invoke(_score);
        }
    }
}