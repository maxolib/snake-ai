# SnakeAI

SnakeAI is the AI experimentation for Snake game that was implemented in Unity engine.



# AI List

- Traditional
  - [Version 1](#####version-1)
  - [Version 2](#####version-2)
- Machine Learning (Reinforcement Learning)
  - [ML1](#####ML1)
  - [ML2](#####ML2)
  - [ML3](#####ML3)
  - [ML4](#####ML4)
  - [ML5](#####ML5)
  - [ML6](#####ML6)



# Experimentation

### Traditional AI

- ##### ML1Version 1

  ![snake-traditional-1](Resources/snake-traditional-1.gif)

  | Name                  | Description                                          |
  | --------------------- | ---------------------------------------------------- |
  | Condition             | - Finding food directions (Random if more than one ) |
  | Board size limitation | unlimited                                            |

- ##### Version 2

  ![snake-traditional-2](Resources/snake-traditional-2.gif)

  | Name                  | Description                                                  |
  | --------------------- | ------------------------------------------------------------ |
  | Condition             | - Same as `Version1`<br />- Removing non-moveable direction for direction set<br />   (both food and non-food direction set)<br />- Calculating game over for a next turn <br />   then remove it from the direction set |
  | Board size limitation | unlimited                                                    |

### Machine Learning

- ##### ML1

  ![snake-traditional-1](Resources/snake-ml1.gif)

  | Name       | Description                          |
  | ---------- | ------------------------------------ |
  | Action     | Up, Down, Left, Right                |
  | State      | - Food position<br />- Head position |
  | Config     | - Default                            |
  | Limitation | -                                    |

- ##### ML2

  ![snake-traditional-1](Resources/snake-ml2.gif)

  | Name       | Description                                                  |
  | ---------- | ------------------------------------------------------------ |
  | Action     | Up, Down, Left, Right                                        |
  | State      | - **2x** Food position<br />- **2x** Head position<br />- **100x** All of block type (Zero, Positive and Negative) |
  | Config     | - Default                                                    |
  | Limitation | 10x10 board size only                                        |

- ##### ML3

  ![snake-traditional-1](Resources/snake-ml3.gif)

  | Name       | Description                                                  |
  | ---------- | ------------------------------------------------------------ |
  | Action     | Up, Down, Left, Right                                        |
  | State      | - **2x** Food position<br />- **2x** Head position<br />- **100x** All of block type (Zero, Positive and Negative) |
  | Config     | - Default<br />- batch_size: 128 <br />- buffer_size: 2048 <br />- hidden_units: 256 |
  | Limitation | 10x10 board size only                                        |

- ##### ML4

  ![snake-traditional-1](Resources/snake-ml4.gif)

  | Name       | Description                                                  |
  | ---------- | ------------------------------------------------------------ |
  | Action     | Up, Down, Left, Right                                        |
  | State      | - **2x** Food position<br />- **2x** Head position<br />- **100x** All of block type (Zero, Positive and Negative)<br />- **1x** Number of tails |
  | Config     | - Default<br />- batch_size: 128 <br />- buffer_size: 2048 <br />- hidden_units: 256<br />- beta: 1.0e-4 |
  | Limitation | **10x10** board size only                                    |

- ##### ML5

  ![snake-traditional-1](Resources/snake-ml5.gif)

  | Name       | Description                                                  |
  | ---------- | ------------------------------------------------------------ |
  | Action     | Up, Down, Left, Right                                        |
  | State      | - **2x** Food position<br />- **2x** Head position<br />- **100x** All of block type (Zero, Positive and Negative)<br />- **1x** Number of tails |
  | Config     | - Default<br />- batch_size: 128 <br />- buffer_size: 2048 <br />- hidden_units: 256 |
  | Limitation | **10x10** board size only                                    |

- ##### ML6

  ![snake-traditional-1](Resources/snake-ml6.gif)

  | Name       | Description                                                  |
  | ---------- | ------------------------------------------------------------ |
  | Action     | Up, Down, Left, Right                                        |
  | State      | - **2x** Food position<br />- **2x** Head position<br />- **100x** All of block type (Positive and Negative)<br />- **1x** Number of tails<br />- **4x** Food Direction Set<br />- **4x** Non-Food Direction Set |
  | Config     | - Default<br />- batch_size: 128 <br />- buffer_size: 2048 <br />- hidden_units: 256 |
  | Limitation | **10x10** board size only                                    |
