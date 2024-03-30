# BioSyncVR
Team members: Julien Samuel Eddy Guimez, Lydia Chung, Christopher Harm, Vasishta Akinapalli, Nick McClure

## Completed Work
The first environment “Beach” is complete. It features palm trees and moving waves. The wave system was added to be easily controllable, so that the level of the water can be controlled and waves can be intensified or weakened depending on the focus state of the user. This is a future feature that will be implemented once data streaming from the EEG headband is fully implemented.

With regards to the feedback algorithm that will be used to drive UX elements such as the weather and scenery objects, we are still in the process of retrieving and conditioning the data for input into the feedback algorithm. The Muse 2 headband offers an array of sensors (eeg, accelerometer, gyroscope, and ppg) that we hope to incorporate into the feedback algorithm in order to create the most dynamic and engaging experience for the end-user. We are further refining the preprocessing and conditioning algorithm in order to isolate brain signal from noise that is inherent with dry electrode EEG sensors. So far the sensor data is pre-processed into three main stages: noise reduction (a lowpass butterworth filter), normalization (z-score), and transformation into the frequency-domain (Fast Fourier Transform FFT). Based on the spectral components in the frequency representation we are able to interpret signal based on the classifications of brain wave signals (Delta: 1 - 4 Hz, Theta: 4 - 8 Hz, Alpha: 8 - 13 Hz, Beta 13 - 30 Hz, GammaL 30 - 44 Hz).

## Known Bugs
The Oculus controllers are cubes because we have not found a suitable model for them yet.

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
