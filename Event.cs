using UnityEngine;

namespace MessagingSystem
{
    /// <summary>
    /// Base event for all EventManager events.
    /// </summary>
    public abstract class Event
    {
    }

    #region Game Manager Events

    public class StartGameEvent : Event
    {
    }

    public class TogglePauseEvent : Event
    {
        public bool Paused { get; private set; }

        public TogglePauseEvent(bool paused)
        {
            Paused = paused;
        }
    }

    public class MainMenuEvent : Event
    {
    }

    public class StopGameEvent : Event
    {
    }

    public class GameOverEvent : Event
    {
        public bool Win;

        public GameOverEvent(bool win)
        {
            Win = win;
        }
    }

    #endregion

    #region Chunk Events

    public class NewChunkEvent : Event
    {
        public Vector2 Position { get; private set; }
        public float Size { get; private set; }

        public NewChunkEvent(Vector2 position, float size)
        {
            Position = position;
            Size = size;
        }
    }

    public class EnableChunkEvent : Event
    {
        public Vector2 Position { get; private set; }

        public EnableChunkEvent(Vector2 position)
        {
            Position = position;
        }
    }

    public class DisableChunkEvent : Event
    {
        public Vector2 Position { get; private set; }

        public DisableChunkEvent(Vector2 position)
        {
            Position = position;
        }
    }

    public class LoadChunkEvent : Event
    {
        public Vector2[] Positions { get; private set; }

        public LoadChunkEvent(Vector2[] positions)
        {
            Positions = positions;
        }
    }

    #endregion

    #region Player Events

    public class PlayerSpawnEvent : Event
    {
        public Transform Player { get; private set; }

        public PlayerSpawnEvent(Transform player)
        {
            Player = player;
        }
    }

    public class PlayerDeathEvent : Event
    {
    }

    #endregion

    public class ProjectileHitEvent : Event
    {
        public Transform Hit { get; private set; }
        public int Damage { get; private set; }

        public ProjectileHitEvent(Transform hit, int damage)
        {
            Hit = hit;
            Damage = damage;
        }
    }

    #region Entity Events

    public class EntityDamagedEvent : Event
    {
        public Transform Transform { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }

        public EntityDamagedEvent(Transform transform, int health, int maxHealth)
        {
            Transform = transform;
            Health = health;
            MaxHealth = maxHealth;
        }
    }

    public class EntityKilledEvent : Event
    {
        public Transform Transform { get; private set; }

        public EntityKilledEvent(Transform transform)
        {
            Transform = transform;
        }
    }

    #endregion

    #region Spaceship Events

    public class SpaceshipCrashedEvent : Event
    {
        public Transform Transform { get; private set; }
        public float HitForce { get; private set; }

        public SpaceshipCrashedEvent(Transform transform, float hitForce)
        {
            Transform = transform;
            HitForce = hitForce;
        }
    }

    public class SpaceshipLandedEvent : Event
    {
        public Transform Transform { get; private set; }

        public SpaceshipLandedEvent(Transform transform)
        {
            Transform = transform;
        }
    }

    public class SpaceshipTookOffEvent : Event
    {
        public Transform Transform { get; private set; }

        public SpaceshipTookOffEvent(Transform transform)
        {
            Transform = transform;
        }
    }

    #endregion

    #region Waves Events

    public class NewWaveEvent : Event
    {
    }

    public class WaveClearedEvent : Event
    {
    }

    #endregion
}