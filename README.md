🌱 Farm Life
Farm Life is a 2D Unity-based farm game that merges productivity with relaxation. Designed especially for students, developers, the player grows their farm while respecting time cycles—making progress through real-life focus and consistent routines.

🎮 Key Features
🌾 Planting and crop growth with time-based logic

⏱️ Real-time day/night cycle via TimeController

💰 Currency and interactive shop system (CurrencyController, ShopController)

🛏️ Sleep system to advance the day (BedController)

🧺 Inventory for seeds and harvests (InventoryController)

📦 Grid-based planting system (GridController, GrowBlock)

🎵 Background music and sound system (AudioManager)

📷 Free-moving camera (CameraController)

🎬 Animated main menu background (MainMenuController)

🚪 Area transitions with auto-respawn (AreaSwitcher)

🌽 Clean UI for managing seeds, crops, and player stats

🕹️ Controls
WASD or Arrow Keys — move the character

I - open inventory 

B - opens the shop

Mouse — UI and menu interaction

Space / Enter — context-based interactions (plant, sleep, talk)

Esc — pause or open menu

🗺️ Scene Structure
level0 to level4 — represent different farm zones

Main Menu — initial scene with animated background objects

🧩 Script Overview
Script	Description
PlayerController.cs	Handles player movement
TimeController.cs	Manages the game’s time and daily cycles
CropController.cs	Handles crop growth logic
ShopController.cs	Shop logic and transactions
CurrencyController.cs	Controls player’s money
InventoryController.cs	Manages seeds and crop inventory
GridController.cs	Controls planting zones
BedController.cs	Allows the player to sleep and skip the day
AreaSwitcher.cs	Manages area transitions using PlayerPrefs
AudioManager.cs	Handles audio playback
UIController.cs	Updates HUD and in-game UI

🚀 How to Run the Project
This repository includes the game build and script files. 

To run locally:

Download and install builder file, extract the folder and execute Farm Life.exe

Press Play starting from the MainMenu or farm entry scene

📌 Future Ideas
🌤️ Day changes

📈 Farm evolution tied to real-life productivity

🪴 New seeds, markets, and farming upgrades

🖼️ Screenshots

<img width="600" height="600" alt="image" src="https://github.com/user-attachments/assets/de30b14f-8b27-42ed-808d-95c98f0091e0" />

<img width="600" height="600" alt="image" src="https://github.com/user-attachments/assets/09afd9f3-82e4-461b-8331-3d361dbe7945" />

<img width="600" height="600" alt="image" src="https://github.com/user-attachments/assets/33f7e6af-a87b-4498-b5e9-bdeca2e66d6c" />

<img width="600" height="600" alt="image" src="https://github.com/user-attachments/assets/c7f7da66-3c74-4fc1-8e0e-a8ccdc2f1aa0" />



📄 License
This project is licensed under the MIT License — see the LICENSE file for details.

📬 Contact
Developed by Paulo Ricardo Potter Marchi

