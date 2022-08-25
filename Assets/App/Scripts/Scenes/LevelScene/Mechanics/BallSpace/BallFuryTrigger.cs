using System;
using Blocks;
using UnityEngine;

namespace BallSpace
{
    public class BallFuryTrigger : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _ballRigidbody2D;
        [SerializeField] private ParticleSystem _fireParticle;
        [SerializeField] private float _forceMultiplier;

        private void OnEnable()
        {
            _fireParticle.Play();
        }

        private void Update()
        {
            ParticleSystem.ForceOverLifetimeModule force = _fireParticle.forceOverLifetime;
            force.xMultiplier = _ballRigidbody2D.velocity.x * _forceMultiplier * -1;
            force.yMultiplier = _ballRigidbody2D.velocity.y * _forceMultiplier * -1;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Block block))
            {
                block.DestroyBlock();
            }
        }
    }
}