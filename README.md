# com.milen.ui-things
A Unity package repo containing UI utilities


## Usage

1. Setup a message provider:

1.1 Create a new MonoBehavior Script that implements the IMessageProvider interface.
1.2 Attach the script to a game object in the scene.

2. Setup an object that will trigger the message display on click:

2.1 Using the Unity Editor, create a new game object or select an existing one.
2.2 Add a collider component to the selected game object (if there isn't one already).
2.3 Add the "Show Message On Click" component to the selected game object.
2.4 Drag the "message provider" game object and drop it onto the "Show Message On Click -> Message Provider" field in the object inspector.

