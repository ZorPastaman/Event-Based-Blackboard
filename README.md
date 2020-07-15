# Event Based Blackboard

## What is Event Based Blackboard?

Event Based Blackboard for Unity is a flexible data storage that can contain any count of properties of any type.
The properties can be accessed with property names.
Any part of a code can subscribe to a blackboard value change and receive a callback.

The main advantage of Event Based Blackboard is that it allocates as little as possible. Also it has good enough performance.

## Installation

This repo is a regular Unity package. You can install it as your project dependency.
More here: https://docs.unity3d.com/Manual/upm-dependencies.html.

## Usage

### Setup

#### As a Unity component

1. Add a [BlackboardContainerComponent](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Components/Main/BlackboardContainerComponent.cs)
to a GameObject in your scene. You can find it in **Add Component/Event Based Blackboard/Main/Blackboard Container**.
That component automatically creates a [Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) inside.
You can access it via `BlackboardContainerComponent.blackboard` property.
2. Add a [Flusher](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Components/Main/Flushers/BlackboardContainerFlusher.cs).
You can find them in **Add Component/Event Based Blackboard/Main/Flushers/**.
You have to add a [Flusher](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Components/Main/Flushers/BlackboardContainerFlusher.cs)
because [Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/Blackboard.cs)
doesn't apply changes until a special method is called which is done by a
[Flusher](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Components/Main/Flushers/BlackboardContainerFlusher.cs).
As a [Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) flushed all changes, it sends callbacks.

#### As a regular c# class

Simply create a [Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) with its default constructor: `new Blackboard()`.
You may get all the features of the Event Based Blackboard this way too.
But you have to call `Blackboard.Flush()` yourself to apply changes and send callbacks.

### [Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) API

- `TryGetValue()` tries to get and return a value of a specified type and property name.
- `SetValue()` sets a value of of specified type and property name into a buffer.
- `Subscribe()` subscribes a specified callback to a value change of  a specified type and property name.
- `Unsubscribe()` unsubscribes a specified callback from a value change of  a specified type and property name.
- `Flush()` applies all set into buffers values and sends callbacks of their changes.

Those are main methods of [Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/Blackboard.cs),
you can find more in its source code.

All the methods of [Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/Blackboard.cs)
use [BlackboardPropertyName](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/BlackboardPropertyName.cs)
as a property name, not string. You can create that struct with one of its constructors. It transforms strings into unique integer ids. That makes work of
[Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) faster. But it's not recommended to create a new
[BlackboardPropertyName](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/BlackboardPropertyName.cs) every time, cache it.

## Serialization

If you use a [Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) as a regular c# class,
you have to support serialization yourself.

If you use a [BlackboardContainerComponent](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Components/Main/BlackboardContainerComponent.cs),
you can create a [Serialized Tables Container](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Serialization/SerializedTablesContainer.cs)
in **Assets/Create/Event Based Blackboard/Serialized Tables Container** and link it in your
[BlackboardContainerComponent](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Components/Main/BlackboardContainerComponent.cs).
It will automatically apply all properties of a
[Serialized Tables Container](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Serialization/SerializedTablesContainer.cs) on Awake().

### How to customize serialization

Although there are many types supported by Event Based Blackboard out of the box, you may need to serialize more types or your own types.
In that case, you need to inherit
[GeneratedValueSerializedTable](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Serialization/SerializedTables/GeneratedValueSerializedTable.cs)
or [SerializedValueSerializedTable](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Serialization/SerializedTables/SerializedValueSerializedTable.cs).

If you need a full customizable serialized table, you can inherit
[SerializedTable_Base](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Serialization/SerializedTables/SerializedTable_Base.cs).

If you need a full customizable serialized container, you can inherit
[SerializedContainer](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Serialization/SerializedContainer.cs).

## Blackboard editor support

Because Unity draws only serialized by itself properties, properties of a [Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/Blackboard.cs)
aren't drawn by Unity. But [BlackboardContainerComponent](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Components/Main/BlackboardContainerComponent.cs)
has a custom editor to show and make editable all of its [Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) properties.
Although there are many types that are drawn out of the box, you may need to draw more types in Unity editor. You can achieve it by inheriting
[BlackboardValueView](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Editor/ValueViews/BlackboardValueView.cs) or
[UnityObjectBlackboardValueView](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Editor/ValueViews/Implementations/UnityObjectBlackboardValueView.cs).

## Logs
There is a special class for logging the whole Blackboard system:
[BlackboardDebug](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Debug/BlackboardDebug.cs).
It contains conditional methods for logging. You can control a compilation of those methods with define symbols:
- EVENT_BLACKBOARD_LOG_DETAILS - log every change of the Blackboard system.
- EVENT_BLACKBOARD_LOG
- EVENT_BLACKBOARD_LOG_WARNING
- EVENT_BLACKBOARD_LOG_ERROR

[BlackboardDebug](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Debug/BlackboardDebug.cs)
has all of them as public const strings.

## Little features

### [BlackboardPropertyReference](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Components/BlackboardPropertyReference.cs)

It's a simple serializable struct with two fields: reference to
[BlackboardContainerComponent](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Components/Main/BlackboardContainerComponent.cs)
and property name of string type. It has a custom editor that makes a selector for the property name field. The selector is filled with property names of
all serialized properties of the referenced
[BlackboardContainerComponent](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Components/Main/BlackboardContainerComponent.cs).
It's recommended to use it for custom components where you need to reference a specific property in a
[Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/Blackboard.cs)
of a [BlackboardContainerComponent](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Components/Main/BlackboardContainerComponent.cs).

### [Event](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/CustomTypes/Event.cs)

It's an empty struct which comparisons are always false. It's useful to make a 
[Blackboard](https://github.com/ZorPastaman/Event-Based-Blackboard/blob/develop/Runtime/Core/Blackboard.cs) event because setting it will always cause sending callbacks.

## Extensions
You can find useful extensions for Event Based Blackboard in the different repo: https://github.com/ZorPastaman/Event-Based-Blackboard-Extensions.

## See also

- [Simple Blackboard](https://github.com/ZorPastaman/Simple-Blackboard) - another version of a blackboard system which doesn't support events but requires much less memory.
