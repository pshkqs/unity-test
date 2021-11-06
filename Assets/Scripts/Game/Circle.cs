using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Circle : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        
        private CircleSettings _settings;

        public void Init(in CircleSettings settings)
        {
            _settings = settings;

            GetComponent<SpriteRenderer>().color = settings.Color;
        }

        private void PlayParticle()
        {
            var particle = Instantiate(_particle, transform.position, Quaternion.identity);
            var main = particle.main;
            main.startColor = _settings.Color;
            particle.Play();
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position, _settings.Destination, _settings.Speed * Time.fixedDeltaTime);

            if (transform.position == _settings.Destination)
            {
                _settings.ReachedDestination?.Invoke(_settings.Damage);
                PlayParticle();
                Destroy(gameObject);
            }
        }

        private void OnMouseUpAsButton()
        {
            _settings.Destroyed?.Invoke(_settings.Score);
            PlayParticle();
            Destroy(gameObject);
        }
    }
    
    public struct CircleSettings
    {
        public Vector3 Destination;
        public Color Color;
        public float Speed;
        public int Damage;
        public int Score;
        public Action<int> ReachedDestination;
        public Action<int> Destroyed;
    }
}