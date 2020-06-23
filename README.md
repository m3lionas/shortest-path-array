# shortest-path-array
Solution to the problem:

Determine if you can reach the last element on a positive/negative numbers array, according to
these rules:
1. You start at the first element.
2. Current element value indicates how many steps you can take at most.
- Example: if the value is 3 then you can take 0, 1, 2 or 3 steps;
- Example: if the value is 0 then you stuck – the end.

## What's inside?
- Application finds the shortest path and displays it together with the number of steps.
- Arrays are read from input.txt, application can process a batch of them.
- Stores solutions in solutions.txt, if the application finds an already calculated solution, it takes it from there.
- User can ask to show all already calculated solutions or just search for a single one.
- Several Unit Tests to check if the main algorithm that calculates the solution is working properly.
- Handled errors (mainly reading from/writing to file, parsing strings into integers).


## Instructions
- It's a MS Visual Studio project, the main code is inside shortest-path-array\MainProgram.cs.
- Write your arrays of intergers into input.txt (all numbers seperated from eachother by a single space, each array in a new line). The application will process them from there.
- You can launch the application as any other regular MS Visual Studio project.
