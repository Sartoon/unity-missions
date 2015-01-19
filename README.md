# unity-missions
A simple mission management and spawning system for Unity.

The provided Demo project provides an easy way to see the system in action.

# Important classes

## MissionManager object
### Required as an object in the scene
Overall manager for missions. 
### Fields available in the Inspector
* Start Mission Delay - float in seconds
  * This will delay the start of any mission before it is activated. This can be used to let the user see what missions are coming up.
* Remove Finished Mission Delay - float in seconds
  * This will delay the removal of any finished missions from the current mission list. This can be used to let the user see the result of missions.
* Spawn Time - float in seconds
  * Determines how often the Mission Manager will attempt to spawn new missions. If the allowDuplicateMissions flag is set to false, or the maximumActiveMissions is set to a low number, missions may not always be spawned at this time
* Maximum Active Missions - int
  * Restricts how many missions can be active at one time
* Allow Duplicate Missions
  * Determines if two missions of the same type can be active at once
* Run Missions On Start - bool
  * If set to true, the Mission Manager will begin processing missions when Unity calls the Start() function
### Fields hidden from the Inspector
* Run Missions - bool
  * When set to true the Mission Manager will process missions in the Update() loop. Setting Run Missions On Start will cause this to be automatically set to true, otherwise the variable must be set an appropriate time by an external script.
* Score Listener - IScoreListener
  * A reference to an object that implements the IScoreListener interface. This will be passed to any newly created missions to allow missions to add scores when completed. Must be set by an external script, preferably in Awake() to ensure any startup missions created are correctly configured.

## EventManager
### Required as an object in the scene
Manages the propagation of game events to objects registered as Event Listeners. MissionManager will automatically detect this object and add it to any newly created missions.

To use this, classes that create events should have a reference to this class and then use EventManager.LogEvent(string[]) to register an event.

LogEvent(string[]) takes an array of strings so that one action can trigger many events. For example, double jumping in a platformer could trigger a "jump" event and also a "double jump" event.

## Mission
Contains a set of conditions that must be met.

The base class could have been more abstracted, but I made the decision to create it based around timed Verb + Noun based missions (i.e. Jump 10 times).

Creating verb + noun missions is very easy, as can be seen in the demo files included. 

There is also a demo class included to demonstrate a negative mission, in this case "Don't jump for the next 3 seconds". This overrides some of the functions in order to do this.

All of the important settings and functions can be seen by reading through the 3 example missions provided.

# Less important classes

## MissionInitiator
Initiates MissionManager with a list of Missions that will be created randomly during play. 

Can optionally call MissionManager.CreateStartupMissions(int amount, bool inOrder) to create the initial set of missions. When inOrder is set to true the startup missions will be created in the order they were added to the MissionManager.

An example class is included in the source, but this has does not need to be a standalone script, however it may be useful to keep it separated in this fashion.

## MissionManagerDisplay
Purely a convenience class. Attach to a UnityEngine.UI.Text object and it will display the mission descriptions as obtained from MissionManager.

# Example usage
## or, how the demo project was created
* Create a MissionManager object and add the associated script
* Create an EventManager object and add the associated script
* Create an object that will initialise the MissionManager with your Mission types (see MissionInitiator)
* Add references to the EventManager object to any objects you wish to log events. Edit or add scripts to call EventManager.LogEvent("eventName") when those events occur.
* Create missions based off the events logged
* Add these mission types to your Mission initiator class 
* Create a UI Text object and add MissionManagerDisplay to it.
* In the Inspector, set the MissionManager to Run Missions On Start and ensure your Mission Initiator populates the Mission Manager on Start.
* Create a class that implements the IScoreListener interface. In this class find the MissionManager object and set MissionManager.scoreListener to your class.