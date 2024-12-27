# com.milen.ui-things
A Unity package repo containing UI utilities.

## Notes
*ShowMessageOnClick* is a MonoBehaviour that displays an Android toast message when the associated GameObject is tapped.

The message is provided by a MonoBehaviour that implements the IMessageProvider interface.

```CS
public class TestMessageProvider : MonoBehaviour,
	IMessageProvider
{
    public string Message { get => "Hello!"; }
}
```

## Usage

- Setup a message provider:
  - Create a new MonoBehavior Script that implements the IMessageProvider interface.
  - Attach the script to a game object in the scene.

- Setup an object that will trigger the message display on click:
  - Using the Unity Editor, create a new game object or select an existing one.
  - Add a collider component to the selected game object (if there isn't one already).
  - Add the "Show Message On Click" component to the selected game object.
  - Drag the "message provider" game object and drop it onto the "Show Message On Click -> Message Provider" field in the object inspector.
