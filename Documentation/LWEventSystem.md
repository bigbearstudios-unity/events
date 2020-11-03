# LWEventSystem

All examples below use the GlobalLWEventSystem for reference but each one can be ran against your own instance of a LWEventSystem.

## Using Event Strings

In order to use events a Listener must be setup. This will be the method which is called when the event is trigged by the system:

``` csharp

GlobalLWEventSystem.ListenFor("PlayerDestroyed", OnPlayerDestroyed);

//Where OnPlayerDestroyed
void OnPlayerDestroyed(string eventString, object caller, object data) {

}

```

The OnPlayerDestroyed method will then be called when the following event is recieved by the event system:

``` csharp

GlobalLWEventSystem.Broadcast("PlayerDestroyed");

```

Along with the name of the event to be called you can also send through a caller and a data object like so:

``` csharp

GlobalLWEventSystem.Broadcast("PlayerDestroyed", caller, object);

```

## Removing Events

You can remove an event for a given listener:

``` csharp

GlobalLWEventSystem.Remove(this, "PlayerDestroyed");

```

Or you can remove all events for a given lister: 

``` csharp

GlobalLWEventSystem.Remove(this);

```

You can also remove all events for a given string:

``` csharp

GlobalLWEventSystem.Remove("PlayerDestroyed");

```

## Clearing Events 

There is also methods to remove *all* events from the EventSystem via: 

``` csharp 

GlobalLWEventSystem.Clear();

```