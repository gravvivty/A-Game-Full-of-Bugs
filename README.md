# A Game Full of Bugs - Y3 GamesDev

###### Author: Steven Gayer (gravvivty)

###### Project Type: Game Design Coursework

<p align="center">
<img style="align-content: center" src="YOUR_BANNER_IMAGE_URL_HERE">
</p>

---

### Project Information

<body>
This repository contains the code and documentation for **A Game Full of Bugs**, a comedic 2D puzzle-platformer developed as part of the Software Engineering module at university.

<br><br>

Players navigate bug-themed levels, solve environmental puzzles, and manage an inventory system that allows combining items to progress through increasingly challenging stages. The game blends humor with platforming mechanics and interactive storytelling.
</body>

---

#### [Game Download / Demo](YOUR_RELEASE_URL_HERE)

---

### My Contributions

As part of this group project, my primary responsibilities included:

- **Level Layout Design**  
    - Designed and implemented the level architecture and platform placements for engaging and balanced gameplay.

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

[Art](Art) - Contains sprites, textures, and visual assets used throughout the game.

[Audio](Audio) - Contains sound effects and background music used in-game.

[Scenes](Scenes) - All Unity scenes making up game levels, menus, and cutscenes.

[Scripts](#code-structure) - Source code files implementing all game logic and systems.

[Prefabs](Prefabs) - Unity prefabs for re-usable game objects like UI elements, environment props, and interactive objects.

[Docs](Docs) - Documentation related to the project, including design documents and implementation notes.

---

### Code Structure

***

#### Table of Contents

1. [Editor](#editor)
2. [Packages](#packages)
3. [Scenes](#scenes)
4. [Scripts](#scripts)

    1. [Gameplay](#gameplay)
    2. [UI](#ui)
    3. [Inventory](#inventory)
    4. [Systems](#systems)
    5. [Utils](#utils)

---

### Editor

<body>

> Contains custom Unity Editor tools and scripts created to improve development workflow and debugging. Examples include editor windows for managing level elements or automating repetitive tasks.
</body>

***

### Packages

<body>

> The project makes use of Unity packages and third-party assets to enhance visuals and workflow. Notable examples:

- [TextMeshPro (TMP)](https://docs.unity3d.com/Manual/com.unity.textmeshpro.html) — Advanced text rendering used throughout the game UI.
- [Cinemachine](https://docs.unity3d.com/Packages/com.unity.cinemachine@2.6/manual/index.html) — Used to manage dynamic camera movements, particularly during cutscenes.
</body>

***

### Scenes

<body>

> A brief overview of the key scenes in the project:

- **MainMenu** — The game’s entry point, containing UI navigation to start the game, view credits, or exit.
- **Level_1** (and others) — Core levels featuring platforming, puzzles, and teleport mechanics.
- **Cutscenes** — Dedicated scenes for narrative events and interactive story sequences.
- **GameOver** — Displays UI when the player fails a level.
</body>

***

### Scripts

> Breakdown of the main script categories:

---

#### Gameplay

- Handles core platforming mechanics.
- Implements teleport systems allowing seamless level traversal.
- Manages interactions between player and environmental elements.

---

#### UI

- Controls UI canvases and elements like menus, HUD, popups, and dialogues.
- Manages cutscene overlays and transitions.

---

#### Inventory

- Provides a system to store, manage, and combine items.
- Drives puzzle mechanics that rely on item usage and combination logic.

---

#### Systems

- Manages global systems such as game state management, audio management, and player preferences.

---

#### Utils

- Helper functions, extensions, and utilities shared across different systems.

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

**A Game Full of Bugs** was developed as a team project for the Software Engineering module.

> This portfolio repository focuses on my individual contributions, including level design, teleport mechanics, UI systems, in-game cutscenes, and inventory combination systems.

All third-party assets used are credited to their respective authors within the project files and documentation.
</body>

---
