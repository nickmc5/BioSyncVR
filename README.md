# BioSyncVR
Team members: Julien Samuel Eddy Guimez, Lydia Chung, Christopher Harm, Vasishta Akinapalli, Nick McClure

## Completed Work
During this final stretch of progress before the end of the semester, our group worked on things such as implementing models, animations, and rays for the VR hands; adding a canvas that will become a menu and can currently display text from a file that is read using a script; improvements to our Unity scene, and EEG data processing progress. 

The VR hands in our project before were just cubes to show where they were, now they have a model that is animated based on what buttons the user is pressing. They also have an interactor ray that will be used to interact with objects in the application such as menus and possibly items. When implementing the rays, there are still issues with getting it to interact with the other objects in the scene, but this will be resolved with more research and trial and error. 

There is currently a rudimentary canvas that will become a menu system. This canvas can display text that is read from a file using a C# script. In future iterations of the project, canvases will be used in a start scene, and in an options menu. This also means that we will be implementing scene switching. The user will choose their options in the start scene, and when they hit start, the ten minute guided meditation will begin and it will switch the scene to the correct environment that they selected. 

There is some progress on a weather system. Currently, we have two separate skyboxes: one for sunny weather, and one for stormy weather. In the future, when the EEG data is read by Unity, we will have a weather system that includes particles and automatic skybox and water level changes.

Audio systems were recently implemented. Their functionality extends to a soothing ambient sound whose volume will shift according to focus level, so as not to disturb users who are deep in meditation. Wind particles will also be added to give a visual feedback to the wind audio for those who may be hearing-impaired. 

## Known Bugs
Bad PPG signal, minimal oscillation to perform peak detection and peak to peak analysis

There is no easing function currently so a drastic change in focus level will look really unnatural

Hand rays don’t interact with canvas elements

PPG stream not present when using bluemuse on LSL, windows blocks LSL from communicating with bluemuse to python program if bluemuse isn't given admin

Although we imported sound files into the scene, there is no audio playing yet, because we have not implemented an audio controller.

## Architectural Elements

BioSyncVR is a virtual reality meditative application for Meta Quest headsets that integrate biofeedback from a Muse 2 headband to create a dynamic weather system for the user.

### External Interface

The external interface starts with opening the app on the headset. Currently, we use QuestLink to connect the Quest to Unity on our test computer to display the game. Then, the headband is donned and connected to the device. Currently, the headband is paired to the testing computer through an open source app called BlueMuse. Then the play button is pressed in Unity and the game enters the persistent state. 
As this is a product meant for commercial users, our external interface attempts to streamline the experience for users to get into the app. The setup of the hardware (testing computer, headset, and headband) will reduce in complexity as we move from a testing model to a user model. The setup of the software external interface will be hidden, as it will register as an app on the headset for end users.  

### Persistent State

The persistent state is the game loop and storage for EEG data. The game loop repeatedly runs until the program is shut off, and it manages the current state shown to users, the delivery of game data to the internal systems, and any other backend work. The game loop is accessible from the external interface through the play button on the app. 
The EEG data needs to be streamed and stored onto the test computer right when the game loop starts, and it should continue to be streamed until the app is exited. Currently, the EEG data is being streamed from the headband using an open source app, and it is being stored in CSV files. The files are then used as a buffer that the game loop reads and uses to adjust the state of the app in VR. The EEG data isn’t meant to be directly accessible from the external interface, since it should be abstracted away from the user in the end product. Instead, it automatically launches once the app enters the persistent state.
The internal systems have access to the persistent state, since the game loop is technically part of the internal systems and persistent state, and the EEG data is being stored in a buffer for the internal systems to read. 


### Internal Systems

The internal systems are the methods/processes that run in the game loop on Unity and the data processors that run on the EEG data. They are connected to the persistent state as described in the persistent state section. The game loop processes generate visuals based on user location, change weather based on processed EEG data, and give feedback on the user’s meditation feedback. They also do less specific tasks like exiting and entering the app. The data processor for EEG data takes in the sensor information on heart rate and brain waves and assigns a score to the current packet of information. Then that score is sent to the game loop, which generates weather based on it. Currently, we are working on the Unity scene development, so we have not gotten into scripting and state yet. For the hardware, we are still deciding on the dataflow, which means we haven’t moved onto EEG information scoring yet.
