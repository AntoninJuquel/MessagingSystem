# MessagingSystem

**A lightweight, decoupled messaging/event system for Unity using the Observer pattern and C# generics.**

This system allows components to communicate cleanly without tight coupling. Define strongly-typed event classes, subscribe from anywhere, and fire events globally with no references between systems.

---

## ✨ Features

* 🔄 Observer pattern implementation via generic C# events
* 💬 Event classes can carry custom payloads (e.g., `GameObject`, `Vector3`)
* 🧠 Decouples systems without requiring singletons or direct references
* 💡 Clean API: `Raise`, `AddListener`, `RemoveListener`

---

## 🛠 How to Use

### 1. Define a Custom Event

Create a new class that inherits from `Event`. You can include any parameters you want.

```csharp
public class PlayerSpawnEvent : Event
{
    public GameObject player;
    public Vector3 position;
}
```

---

### 2. Create Event Handlers

Any class can listen to an event by creating a method that matches the signature.

```csharp
public class ParticlesManager : MonoBehaviour
{
    public void OnPlayerSpawn(PlayerSpawnEvent e)
    {
        Instantiate(spawnParticles, e.position, Quaternion.identity);
    }
}
```

```csharp
public class PlayerManager : MonoBehaviour
{
    public List<GameObject> players = new();

    public void OnPlayerSpawn(PlayerSpawnEvent e)
    {
        players.Add(e.player);
    }
}
```

---

### 3. Subscribe and Unsubscribe

Register your handler using the generic `AddListener<T>()` method — and remember to remove it!

```csharp
private void OnEnable()
{
    EventManager.Instance.AddListener<PlayerSpawnEvent>(OnPlayerSpawn);
}

private void OnDisable()
{
    EventManager.Instance.RemoveListener<PlayerSpawnEvent>(OnPlayerSpawn);
}
```

> ⚠️ Avoid memory leaks and unexpected calls by always unsubscribing when the object is disabled or destroyed.

---

### 4. Raise Events

Fire the event from anywhere — no references required.

```csharp
private void Start()
{
    var player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

    EventManager.Instance.Raise(new PlayerSpawnEvent
    {
        player = player,
        position = Vector3.zero
    });
}
```

---

## 📁 Folder Structure

```
MessagingSystem/
├── Event.cs              // Base class for all events
├── EventManager.cs       // Handles subscriptions and dispatching
└── README.md
```

---

## ✅ Benefits

* Modular and testable code structure
* No direct dependencies between systems
* Great for handling global systems like:

  * Player spawning
  * UI updates
  * Audio triggers
  * Quest progress

---

## 💡 Tips

* Use `[DisallowMultipleComponent]` on managers to avoid duplicate subscriptions.
* You can create `StaticEvent<T>` types for global listeners if needed.
* Avoid `Update()` polling — events are more efficient and cleaner.