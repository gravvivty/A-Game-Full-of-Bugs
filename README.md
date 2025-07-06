# A Game Full of Bugs - Y3 Game Design

###### Author: Steven Gayer (gravvivty)

###### Project Type: Game Design Coursework

---

### Project Information

<body>
This repository contains the code and documentation for *A Game Full of Bugs*, a comedic 2D puzzle-adventure developed as part of the Game Design module at university.

<br><br>

Players navigate bug-themed levels, solve environmental puzzles, and manage an inventory system that allows combining items to progress through increasingly challenging stages. The game blends humor with platforming mechanics and interactive storytelling.
</body>

---
The team's decision was to devellop the game in German therefore only supporting said language.
#### [Game Download / Demo](YOUR_RELEASE_URL_HERE)

---

### My Contributions

As part of this group project, my primary responsibilities included:

- **Level Layout Design**  
    - Designed and implemented the level architecture and platform placements.

- **Teleport Systems**  
    - Developed teleport mechanics enabling players to traverse non-linear level layouts.

- **UI Management**  
    - Implemented the UI framework, including menus, in-game HUD, and visual feedback for game states.

- **In-Game Cutscenes**  
    - Created interactive cutscenes to enhance storytelling and improve player immersion.

- **Inventory System & Combination Mechanics**  
    - Developed a functional inventory, allowing players to collect, store, and combine items to solve puzzles and unlock new areas.

---

### Folder Structure

[Animation](Assets/Animations) - Contains sprites, textures, and visual assets used throughout the game.

[Sound](Assets/Sound) - Contains sound effects and background music used in-game.

[Scenes](Assets/Scenes) - All Unity scenes making up game levels, menus, and cutscenes.

[Scripts](#code-structure) - Source code files implementing all game logic and systems.

[Prefabs](Assets/Prefabs) - Unity prefabs for re-usable game objects like UI elements, environment props, and interactive objects.

---

***

#### Table of Contents

1. [Scenes](#scenes)
2. [Scripts](#scripts)

    1. [Audio](#audio)
    2. [Dialogue](#dialogue)
    3. [Helper](#helper)
    4. [Interactable](#interactable)
    5. [Vent System](#vent-system)
    6. [Inventory](#inventory)

---

### Scenes

<body>

> A brief overview of the key scenes in the project:

- **MainMenu** — The game’s entry point, containing UI navigation to start the game, select a level, or exit.
- **Level_1_XX** (and others) — Core levels featuring puzzles and the main story of the game.
- **Cutscenes** — Dedicated scenes for narrative events.
</body>

***

### Scripts

> Breakdown of the main script categories:

---

### Audio

> Scripts related to sound effects and background music management. Handles SFX playback, mute toggling, and persistent audio settings.

| Script                 | Description                                                                 |
|------------------------|-----------------------------------------------------------------------------|
| **CustomAudioManager** | Plays and stops sound effects by name. Sets up `AudioSource` components for all sounds. |
| **Sound**              | Serializable class that defines a sound’s name, clip, volume, and loop setting. |
| **MusicSettings**      | Singleton used to manage global mute state. Interfaces with `CustomMusicManager`. |
| **CustomMusicManager** | Scene-level music manager. Handles playback, mute toggling, and stores mute state in `PlayerPrefs`. |

---

### Dialogue

> Scripts responsible for managing dialogue flow, conditions, rewards, and UI. Supports branching conversations, item-based conditions, and scene-linked rewards.

| Script                 | Description                                                                 |
|------------------------|-----------------------------------------------------------------------------|
| **DialogueManager**    | Central controller for dialogue logic. Handles dialogue flow, choices, and applying rewards. |
| **DialogueUI**         | Displays dialogue lines and choices. Dynamically creates buttons based on dialogue data. |
| **DialogueData**       | A `ScriptableObject` containing a list of dialogue lines. Used to start and reference conversations. |
| **DialogueLine**       | Represents a single line of dialogue with speaker, text, choices, and conditions. |
| **DialogueChoice**     | A selectable option in dialogue that may lead to another dialogue line. Can have conditions. |
| **DialogueCondition**  | Defines conditions for dialogue (e.g. has specific item). |
| **DialogueReward**     | Triggers events or rewards after dialogue (e.g. give item, load scene, play animation). |
| **SimpleDialogueBox**  | A basic dialogue box for testing, showing static lines on mouse click. |
| **ConditionType / RewardType** | Enums that define valid condition and reward types used in dialogue flow. |

---

### Helper

> Utility scripts for enhancing interaction, visual feedback, and user input. These scripts assist with raycasting, cursor visuals, and sprite outlining.

| Script              | Description                                                                     |
|---------------------|---------------------------------------------------------------------------------|
| **SpriteOutline**   | Adds an outline effect to sprites using a child renderer with a custom shader. |
| **MouseRaycast**    | Performs 2D raycasts from the mouse to detect objects under the cursor.         |
| **CursorManager**   | Manages and updates custom mouse cursors based on interaction context.          |

---

### Interactable

> Contains the core systems for interactive world objects, including NPCs, item receivers, vents, and interaction feedback. Supports proximity-based interaction, item usage, and contextual cursor behavior.

| Script                   | Description                                                                 |
|--------------------------|-----------------------------------------------------------------------------|
| **Interactables**        | Abstract base class for all interactable objects. Handles cursor changes, outlines, and distance-based interaction. |
| **NPC**                  | Inherits from `Interactables`. Controls NPC dialogue and proximity logic.  |
| **NPCMovement**          | Moves NPCs toward target positions and adjusts sprite layers for depth.    |
| **AntReceiver**          | Accepts specific items from the player and shows a temporary UI prompt.    |
| **FlowerReceiver**       | Handles combining specific items with a flower object. Plays feedback and activates changes. |

### Vent System

> A sub-system under `Interactable` for vent-based teleportation mechanics. Allows players to move between connected vents if conditions are met.

| Script                      | Description                                                                |
|-----------------------------|----------------------------------------------------------------------------|
| **VentTeleportSystem**      | Manages vent teleportation. Validates range, handles wasp proximity, and performs movement. |
| **Vent**                    | An interactable vent entry point. Triggers teleport and plays SFX.         |
| **VentDestination**         | Defines teleport destination for a connected vent.    

---

### Inventory

> Manages item collection, inventory UI, and item combination logic. Supports drag-and-drop interaction, tooltips, and item usage on world objects.

| Script                 | Description                                                                 |
|------------------------|-----------------------------------------------------------------------------|
| **ItemData**           | `ScriptableObject` that defines item info, icon, and combination logic.     |
| **InventoryManager**   | Singleton that manages adding, removing, and checking items in the inventory. |
| **InventorySlotUI**    | Handles drag-and-drop item behavior, combinations, and tooltip display.     |
| **InventoryUI**        | Controls the inventory panel UI and dynamically updates slots.              |

---

### Installation

To run the project locally:

- Ensure you have the required version of Unity installed.
- Clone the repository:
  ```bash
  git clone https://github.com/gravvivty/A-Game-Full-of-Bugs.git
  ```
- Open the project in Unity.
- Build and run via the Unity Editor.

---

### Credits

<body>

**A Game Full of Bugs** was developed as a team project for the Game Design module.

> This portfolio repository focuses on my individual contributions, including level design, teleport mechanics, UI systems, in-game cutscenes, and inventory combination systems.

All assets were made within the team.
</body>

---
