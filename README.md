# SnakeAI

#### Controller

- Traditional AI

  - Version 1

    ![snake-traditional-1](Resources/snake-traditional-1.gif)
  
    | Name                  | Description                                          |
    | --------------------- | ---------------------------------------------------- |
    | Condition             | - Finding food directions (Random if more than one ) |
    | Board size limitation | unlimited                                            |

  - Version 2
  
    ![snake-traditional-2](Resources/snake-traditional-2.gif)
    
    | Name                  | Description                                                  |
    | --------------------- | ------------------------------------------------------------ |
    | Condition             | - Same as `Version1`<br />- Removing non-moveable direction for direction set<br />   (both food and non-food direction set)<br />- Calculating game over for a next turn <br />   then remove it from the direction set |
    | Board size limitation | unlimited                                                    |
