# Nodulus

**Nodulus** is a clever puzzle game based on the mathematical theory behind plank puzzles. It consists of a grid of cubes and rods that can be rotated to reach the objective.

## Project Structure

This repository contains:
- **Nodulus/nodulus/**: The main Unity project folder.
- **Assets/Luxodd.Game/**: Luxodd Arcade integration plugin.
- **Assets/Scripts/**: Core gameplay logic and view components.

## Modern Features & Integration

This version of Nodulus is being integrated with the **Luxodd Arcade** platform, featuring:
- **Automatic Scene Flow**: Logo (13s) -> Blank (3s) -> Game.
- **Arcade Controls**: Optimized for joystick and physical button inputs.
- **Network Synchronization**: Automated session reporting and credit management via the Luxodd Unity Plugin.

## Arcade Controls & Mapping

The game is configured for a **1-Joystick (Player 1)** setup. The controls are bridged via the `NodulusArcadeAdapter.cs` script.

### Joystick (Player 1)
- **Node Selection**: Move the joystick **Up, Down, Left, or Right** to jump the selection highlight between cubes.
- **Deadzone**: Pre-configured at `0.5f` to prevent accidental drifting.

### Physical Buttons
- **Black Button + Joystick**: Performs a **Pull or Push** action in the direction the joystick is being held.
  - *Example*: Hold Joystick **Right** + Press **Black Button** = Push/Pull to the right.
- **Red Button + Joystick**: Alternative action trigger (functions similarly to the Black Button for redundancy).
- **Black Button alone**: General selection/tap (no directional action).

---

## Getting Started

1. Open the project in Unity (Version 6000.3.2f1 recommended).
2. Ensure the `Network` prefab is present in the initialization scenes.
3. Configure the `TimedSceneSwitcher` on the Logo and Blank scenes for the desired transition timing.

## Development Tasks

- [x] Initial Project Review
- [x] Fix compilation errors (HttpUtility dependency removed)
- [x] Implement Timed Scene Transitions
- [/] Luxodd Arcade Integration (Joystick mapping & Session tracking)

## Credits

Original game design and development by [Dan Kondratyuk](https://hyperparticle.com/about/).
Open sourced under the MIT license.
