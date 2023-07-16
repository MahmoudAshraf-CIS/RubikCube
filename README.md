# Rubik's Cube Game

This is a Rubik's Cube game developed using Unity 3D. The game utilizes the Command design pattern for the gameplay mechanics, and the Model-View-Presenter (MVP) design pattern for the game hierarchy.

## Table of Contents

- [Installation](#installation)
- [Gameplay](#gameplay)
- [Design Patterns Used](#design-patterns-used)
- [Demo Video](#demo-video)
- [Contributing](#contributing)
- [License](#license)

## Installation

To run the game, you need to have Unity 3D installed on your computer. You can download the latest version of Unity from the official website: [https://unity.com/ ↗](https://unity.com/).

Once you have Unity installed, you can clone the repository using the following command:

```
git clone https://github.com/your-username/your-repo.git
```

After cloning the repository, open it in Unity and navigate to the `Assets/Scenes` folder. Open the `TotalGame.unity` file and click on the play button to start the game.

## Gameplay

The Rubik's Cube game allows players to solve a N*N Rubik's Cube puzzle. The game features a virtual Rubik's Cube that can be rotated using the mouse. The objective of the game is to solve the Rubik's Cube as quickly as possible.

To solve the Rubik's Cube, players need to use a combination of commands to rotate the cube's faces. The game provides a set of predefined commands that players can use to rotate the cube. The commands are executed using touch on the screen or by typing keyboard shortcuts.

Players can also undo their moves using the undo button. Additionally, players can restart the game from the menu.
Disable and enable the timer from the menu as well.

## Design Patterns Used

The game utilizes the following design patterns:

### Command Design Pattern

The Command design pattern is used to implement the gameplay mechanics of the Rubik's Cube. Each command represents a rotation of a face of the cube. The commands are executed by a command executor, which executes the commands in the order they were given.

### Model-View-Presenter (MVP) Design Pattern

The MVP design pattern is used to organize the game hierarchy. The model represents the data and business logic of the game, the view represents the user interface, and the presenter acts as the middleman between the model and the view.

## Demo Video

Watch a demo of the Rubik's Cube game in action:


<iframe width="560" height="315" src="https://www.youtube.com/embed/PkiM8C7YahA" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>

## Contributing

Contributions are welcome! If you find a bug or have a feature request, please open an issue on GitHub. If you want to contribute code, please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.