# Inventory

Data-driven inventory for games in C#

Inventory provides an easily extendable system for managing items and inventories in games.
Inspired by Minecraft, it incorporates data-driven definitions for items, containers,
recipes and even loot tables. Each of the aforementioned elements can be serialised to JSON,
allowing for defining inventory assets both in code and via JSON files.

Because different games have different requirements, this system is meant to be extended by the
user. It's impossible to cover every case, and even if I managed to do this, it'd become a bloated
mess. Therefore, this system should be thought of as a skeleton of a fully functional inventory 
system in your game.
