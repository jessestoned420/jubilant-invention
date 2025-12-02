# MILEHIGH.WORLD - Cinematic Scene

## Overview

This repository contains the Unity source script for a cinematic sequence from the game MILEHIGH.WORLD. The script, `Cinematic_ConfrontLucent.cs`, manages a dialogue and action-heavy confrontation between the game's protagonists and the antagonist, Lucent.

The mission briefing describes the stakes: *"Intelligence indicates that the Fallen Star, Lucent, has breached the shattered realm of ƁÅČ̣ĤÎŘØN̈. He seeks a forgotten celestial engine at its core, a primordial device with the power to tune the resonant frequency of reality itself. His goal is to attune the engine to the entropic signature of the Void, causing a cascading corruption that would rewrite the entire Verse into a domain of chaos and erasure. Amidst the gravity-defying crystalline archipelago and under the silent gaze of its faceless keepers, the ƁÅČĤĪŘĪM, the heroes must intervene before Lucent can complete his blasphemous work."*

## Features

*   **Cinematic Control:** The script manages a full cinematic sequence, including dialogue, character animations, and camera cuts.
*   **Character Management:** It references and controls multiple character GameObjects and their associated AudioSources for voice lines.
*   **UI Integration:** The script seamlessly integrates with the UI, displaying speaker names and dialogue text using TextMeshPro.
*   **Extensible:** The cinematic is built as a coroutine, making it easy to add, remove, or re-sequence events.

## Scene Setup

To use this script in your Unity project, follow these steps:

1.  **Scene Controller:** Create an empty GameObject in your scene and name it "SceneController".
2.  **Attach Script:** Attach the `Cinematic_ConfrontLucent.cs` script to the "SceneController" GameObject.
3.  **Character Prefabs:** Place the prefabs for each of the following characters into your scene:
    *   Sky.ix
    *   Micah
    *   Kai
    *   Lucent
    *   Ingris
    *   Aeron
    *   Zaia
    *   Otis/X
    *   Otis
4.  **Required Components:** Ensure each character prefab has its corresponding ability script (e.g., `Ability_Skyix.cs`) and an `AudioSource` component for voice lines.
5.  **UI Canvas:** Create a UI Canvas in your scene. Add a Panel or empty GameObject named "DialogueBox".
6.  **Text Elements:** Inside the "DialogueBox", add two "TextMeshPro - Text (UI)" elements. Name them "SpeakerNameText" and "DialogueText", and style them as desired.
7.  **Assign References:** Select the "SceneController" GameObject. In the Inspector, drag the character GameObjects from the Hierarchy into their corresponding "Character" fields (e.g., drag the "Sky.ix" object to the "Skyix_Character" field).
8.  **Assign AudioSources:** Similarly, drag each character's AudioSource component into the corresponding "VoiceSource" field.
9.  **Assign UI Elements:** Finally, drag the "DialogueBox" GameObject, "SpeakerNameText", and "DialogueText" UI elements into their respective fields under the "UI Components" header in the Inspector.
10. **Play:** The scene is now set up. The cinematic will start automatically when the scene is played.

## Future Development

This cinematic is a foundational piece of the narrative. Future development could include:

*   **Player Choice:** Incorporating player choices that could alter the dialogue or outcome of the scene.
*   **Localization:** Adding support for multiple languages.
*   **Dynamic Events:** Tying cinematic events to in-game triggers or player progression.
