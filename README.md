# MessagingSystem

Taking advantage of the Observer pattern to decouple code using c# events


## How to use

### 1. Create a derived class from `Event`
you can even pass parameters
```c#
// Event.cs

public class PlayerSpawnEvent : Event
{
    public GameObject player;
    public Vector3 position;
}
```

### 2. Create a method to handle the event where ever you want
you can create multiple methods in multiple classes to handle the event

```c#
// ParticlesManager.cs

public void OnPlayerSpawn(PlayerSpawnEvent e)
{
    Instantiate(spawnParticles, e.position, Quaternion.identity);
}
```

```c#
// PlayerManager.cs

public void OnPlayerSpawn(PlayerSpawnEvent e)
{
    players.Add(e.player)
}
```

### 3. Subscribe to the event
don't forget to unsubscribe
```c#
// ParticlesManager.cs

private void OnEnable()
{
    EventManager.Instance.AddListener<PlayerSpawnEvent>(OnPlayerSpawn);
}
private void OnDisable()
{
    EventManager.Instance.RemoveListener<PlayerSpawnEvent>(OnPlayerSpawn);
}
```

```c#
// PlayerManager.cs

private void OnEnable()
{
    EventManager.Instance.AddListener<PlayerSpawnEvent>(OnPlayerSpawn);
}
private void OnDisable()
{
    EventManager.Instance.RemoveListener<PlayerSpawnEvent>(OnPlayerSpawn);
}
```

### 4. Fire the event from any where
```c#
// GameManager.cs

private void Start()
{
    EventManager.Instance.Raise(new PlayerSpawnEvent()
    {
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.Identity);
        position = Vector3.zero;
    });
}
```