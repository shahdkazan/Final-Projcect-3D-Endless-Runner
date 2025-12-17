# Final-Projcect-3D-Endless-Runner
Tomato Dash

Tomato Dash is an endless running game developed in Unity, where the player collects tomatoes while navigating through a farm village-themed environment filled with dynamic obstacles.


Overview

Tomato Dash is a 3D endless runner that demonstrates Unity scripting concepts, environment generation, character control, obstacles, collectibles, UI implementation, and animation integration. The player runs automatically forward, collects tomatoes, avoids obstacles, and can jump or slide to survive as long as possible.

Project Objectives

The project aims to:

Implement Unity’s basic scripting concepts to create core gameplay systems.

Handle player input for interactive controls.

Use prefabs and materials efficiently.

Apply physics and collision systems for gameplay interaction.

Create a functional user interface (menus, score, buttons).

Import and integrate animations (Mixamo or similar).

Use Scriptable Objects to manage reusable data.

Work with Unity’s event-driven UI elements (buttons, images, sliders).

Apply lighting techniques such as directional lights, point lights, and light baking.

Gameplay

Theme: Farm village

Player Movement: Automatic forward running; lateral movement within bounds; jump and slide mechanics.

Obstacles: Randomly generated static and rolling obstacles with varying sizes, patterns, and movement types.

Collectibles: Tomatoes spawn in rows; collecting increases score.

End Condition: Collision with an obstacle triggers game over.

Features
Environment

Segmented environment generated dynamically using prefabs.

Old segments are deleted for optimization.

Clamp lateral movement to keep the player in bounds.

Player Mechanics

Forward running and strafing with smooth animation transitions.

Jumping and sliding only when grounded.

Collision detection with obstacles and collectibles.

Obstacles

Obstacles spawn randomly with different sizes and movement types.

Scriptable Objects store obstacle data (prefab, size, spawn chance, movement type, speed).

Collectibles

Spawned dynamically in lanes with spacing and row logic.

Score updates in real-time and is displayed on UI.

User Interface

Main Menu: Start button, game description submenu.

Pause Menu: Pause/resume, volume slider, return to main menu.

Game Over Screen: Final score, time survived, restart button, return to main menu.

Game Over & Restart

Game ends on player-obstacle collision.

Clean reset of game state (score, timer, player position) when restarting.

Lighting & Visual Effects

Directional lighting for overall environment.

Point lights for collectibles.

Skybox applied for immersive environment.

Consistent visual style across segments.

Character Animations

Running, jumping, and falling animations.

Animator controller handles state transitions smoothly.

Controls

Left/Right: Move the player laterally.

Up: Jump

Down: Slide

Game Loop

Main Menu: Start game or view description.

Gameplay: Player automatically runs forward; obstacles and collectibles spawn dynamically.

Pause: Player can pause/resume and adjust volume.

Game Over: Collision with obstacle triggers game over screen showing score and survival time.

Restart/Main Menu: Player can restart the game or return to the main menu.

Lighting & Visuals

Directional light intensity and filters applied for ambient mood.

Point lights highlight collectibles.

Skybox sets the farm village aesthetic.

Character Animations

Animator Controller manages transitions between running, jumping, sliding, and falling.

Integrated with player movement script to trigger animations based on input and collision.
