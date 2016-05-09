# Lanepeons

A trading card game based on the Leage of Legends universe.

Lanepeons tries to emulate the usual League of Legends game, but from a trading card game's perspective. Instead of having 10 players (as a standard 5v5 match), Lanepeons is played by only two players, each one taking control of one of the two sides on the map.

At the start of a game, each player draws 5 cards into his hand. Cards consist of League of Legends champions. On his turn, a player can only play one card, then the turn passes to his opponent. Champion cards can be played on one of the three lanes on the map. Each time a card is played on one of the lanes, the lane's battle turn decreases by one. At 0, a battle starts on the lane. The side with the greatest power on that lane wins, and also destroys the enemy champions and turret if the power difference is large enough. The ultimate goal is to destroy the opponents Nexus. The Nexus is destroyed when no turrets are left alive on a lane.

<p>You can play the WIP version here http://korbul.github.io/Lanepeons/
<br/>
Remember that this is a multiplayer game. You have to play with a friend, or open two tabs and play by yourself.</p>

## Motivation

This project was born as a response to the <a href="https://developer.riotgames.com/discussion/announcements/show/eoq3tZd1">RIOT API Challenge 2016</a>

The RIOT api exposes a plethora of data ranging from champion info to player and match info. A lot of other entries focus on formatting and displaying this data. This project aims to create a game out of it.

Luckily enough, the champion data form the api contains information that is great to build rules upon. One example is the champion tags. This way the "Assasin" tag could become "+2 damage when facing only one enemy on lane". Considering this, champion cards quickly gain depth and usability in game. Along with data like champion image, name, etc. a champion can become a fully fledged card, and all of this in a dynamic way. There is no need to define any kind of data before the build. Everything comes from the api and is created at runtime.

To make this available to as many people as possible, Unity game engine was used alongside the WebGL deployment option. This enables the game to run in a browser environment. Furthermore, the SocketIO library was used to enable realtime multiplayer straight in your browser. This repo is for the client side of the application. The server, written for node.js is quite simple and only acts as a relay between the players.

## Installation

This is a Unity 5.3.3f1 project.

Clone this repo and use the "Open project" button in Unity on the root folder.

## WIP
6th of May 2016

<a href="http://i.imgur.com/UKkVWSU.gif">WIP 1</a>

7th of May 2016 - added turret representation. Early lane battle and UI

<img src="http://i.imgur.com/BZc8FeE.gif"/>

8th of May 2016 - added websockets multiplayer, turret explosion

<img src="http://i.imgur.com/b7Vn8cY.gif"/>

## License

Attribution-NonCommercial 4.0 International (CC BY-NC 4.0)

<img src="http://mirrors.creativecommons.org/presskit/buttons/88x31/png/by-nc.png" height="64"/>

Lanepeons isn't endorsed by Riot Games and doesn't reflect the views or opinions of Riot Games or anyone officially involved in producing or managing League of Legends. League of Legends and Riot Games are trademarks or registered trademarks of Riot Games, Inc. League of Legends © Riot Games, Inc.
