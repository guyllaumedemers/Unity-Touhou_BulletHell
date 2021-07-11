# Unity Touhou - Bullet Hell Game <img src="/readmeImg.png" width="5%" heright="5%">

Welcome to Unity-Touhou, a personnal project currently in the making.
Useful scripts are located in Assets/Scripts/..

## Getting Started

This repository has for objective of practicing Architectural Design Pattern, Memory Management / Optimization
and prioritize Composition over Inheritance.

👾 

## Content

* [Assets/Scripts/Interfaces](https://github.com/guyllaumedemers/Unity-Touhou_BulletHell/tree/master/Assets/Scripts/Interfaces) : Interfaces Scripts
* [Assets/Scripts/Behaviours](https://github.com/guyllaumedemers/Unity-Touhou_BulletHell/tree/master/Assets/Scripts/Behaviours) : Behaviours Scripts (Strategy Pattern)
* [Assets/Scipts/Manager](https://github.com/guyllaumedemers/Unity-Touhou_BulletHell/tree/master/Assets/Scripts/Manager) : Manager Scripts (Factory Pattern)
* [Assets/Scripts/..](https://github.com/guyllaumedemers/Unity-Touhou_BulletHell/blob/master/Assets/Scripts/ObjectPool.cs) : Object Pooling (Memory Management)
* [Assets/Scripts/..](https://github.com/guyllaumedemers/Unity-Touhou_BulletHell/blob/master/Assets/Scripts/Waves/WaveSystem.cs) : Wave System (Wave Management)

#### Game Mechanics and Features

  * Input System
  * Pattern Generation 👻 (*early stage*)
  * Collision System (*thinking about using RTree to make access bullets depending on the area they are in and make things more efficient*)
  * Player Mechanics
  * Movement Mechanics (*early stage*)
  * Orb Rotation
  * Bullet Management
  * Unit Management
  * Wave System (*early stage - need to figure out some stuff like : setting proper position from which units are suppose to spawn*)

#### Design Pattern and Memory Optimization

  * Factory Pattern
  * Object Pooling
  * Batching
  * Strategy Pattern
  * Observer Pattern (*will have to implement an event system to trigger animation when limit scores are reach*)

#### Tools
  * XML Serialization (*WIP*)

## Resources

💬 References for patterns are given from : [Design Patterns: Elements of Reusable Object‑Oriented Software](https://www.amazon.ca/-/fr/Gamma-Erich-ebook/dp/B000SEIBB8)
