Event Objects
=============

This is an implementation of couple of [Ryan
Hipple](https://twitter.com/roboryantron)'s [scriptable
objects](https://youtu.be/raQ3iHhE_Kk). Note that he is not affiliated with
this project.

After playing around with a basic implementation, I really liked how Hipple's
Variable Object system worked. However, this early implementation relied on
Bash scripts to automate certain editor tasks... not the best solution!

So I spent a couple months refining the editor tools for this. I think the
project is at a point where I'm happy to share it with the world. Most of this
work isn't on the original scriptable objects, but on the Unity Editor tools.

Installation
------------
Go to *Projects*, and grab the latest unitypackage. Extract this into your
project, and you're done! Alternatively, you can also clone/submodule this into
your current project using git. If you do that, I recommend putting it under a
path such as Imported/ReachBeyond/plugins/EventObjects.

In the future, I'd like to get this Unity's Asset Store. I'll put a link here
once that becomes a reality.

Usage
-----
I recommend you watch the video linked above. It explains the gist of this
project.

Event Objects are a way to send 'signals' to other components. You create
Events (i.e. EventObjects), which are ScriptableObjects. One example would be
an Event Object called "PlayerDies," which would get triggered when a player
dies. Then you'd give your player prefab an EventObjectInvoker component, and
give this your "PlayerDies" event.

Now, make a component that has a public (or serialized)
ReachBeyond.EventObjects.EventObjectListener variable. In the Unity Editor,
you'll now be able to cause something to happen using a Unity Event. This might
be respawning the player, showing a gameover screen, or something else.

This system is a bit limited, as you cannot pass values via events. I hope to
add this some time in the future.



Other Things
------------
More documentation will come eventually.

If you have a bug to report or a feature request, please open an issue!

For the license on the code, refer to the LICENSE file.

However, the icons aren't my own; they come from
[www.flaticon.com](www.flaticon.com). As per their license, there's a
credits file in the icons folder.
