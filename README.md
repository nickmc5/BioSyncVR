# BioSync VR

BioSync VR is a virtual reality meditation application that uses a Muse EEG headset to capture brain signals and affect the weather in two immersive environments. The app is designed to provide a unique, responsive meditation experience that adapts to the user's focus level.

## Features

- Two immersive environments: Beach and Mountain
- Real-time weather changes based on user's focus level
- Integration with Muse EEG headset for brain signal capture
- 5-minute and 10-minute meditation sessions
- Session results display with focus level graph
- Compatible with SteamVR-supported headsets

## Environments

### Beach Scene
- Clear, sunny sky with calm waves when focused
- Increasing wind, rougher waves, and darkening sky with rain as focus decreases

### Mountain Scene
- Snowy mountain environment with bonfire and tent
- Weather changes (e.g., fog, snow intensity) based on focus level
- a Bonfire with variable fire intensity

## How to Use

1. Launch the application
2. From the main menu:
   - Wait for the Muse headset connection message
     - If connection fails, the app will reattempt
     - If no connection is made, a constant focus level will be used
   - Choose to start a session:
     - Select environment (Beach or Mountain)
     - Choose session duration (5 or 10 minutes)
   - During the session:
     - Focus on meditation to influence the environment
     - Check remaining time by flipping your wrist (like checking a watch)
   - After the session, view your results graph
3. From the main menu, you can also view previous session results

## Installation

1. Download the ZIP file from the [GitHub repository](https://github.com/nickmc5/BioSyncVR/tree/main)
2. Extract the contents of the ZIP file
3. Run the .exe file to start the application

## System Requirements

- PCVR setup with a VR headset compatible with SteamVR
- [Minimum system specifications for running VR applications]
- Muse EEG headset (optional, but highly recommended for full experience)

## Troubleshooting

- Users using SteamVR or Virtual Desktop with a Quest standalone headset may have different button bindings. Check the button bindings set by SteamVR in the pause menu. Pause menu default controls should be (X+Y) on left controller.
- This is a primarily stationary experience. Standing, sitting, or laying down is the expected method of experiencing BioSync VR. Unexpected behavior may occur if a user strays very far from the center of the meditative environment.
- In the Mountain environment, the camera rotation may not follow the user's head rotation. This will be fixed in a hotfix soon.
- Certain images do not show up in the main menu. Left image is the Beach, Right image is the mountain.

## Credits

- Julien Samuel Eddy Guimez
- Lydia Chung
- Nick McClure
- Vasishta Akinapalli
- Christopher Ham
