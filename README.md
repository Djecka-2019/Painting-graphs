#Курсач YOMAYO)

##This is my coursework on topic Graph coloring.

## Basic information
This program can visualise a colored graph enetered by user or generate random graph with amount of edges entered by user.

You can enter your graph in 2 ways:

1) Adjacency matrix
2) Adjacency list

And choose one of three methods of coloring:
1) Greedy coloring method
2) Search with return using MRV heuristic
3) Search with return using heuristic degree

If you want to generate random graph you enter amount of edges, choose preferred method of coloring and click button "Згенерувати випадкову матрицю". If you want to input graph manually, choose your method of graph input, method of coloring, input all the needed data and press button "Почати".

Here is the description of used coloring methods down below.

##Greedy coloring method
Greedy coloring method is very simple. We go through each edge and color them in the first possible color. This method can be not very effective on large amounts of data but is very easy in realisation.

##Search with return
Search with return works on the principle of "Trial and error". We do some steps to solve the problem. if unsuccessful - return and try the other way. We use two heuristics to make search with return more efficient.

### MRV
Minimal remaining Values or MRV - is a heristic that chooses a target with the least available values. It is quite simple - edges with the least possible values have a higher priority for us, so we color them first.

###Heuristic degree
This heuristic chooses a target that affects the largest number of unassigned variables. It is quite similar to MRV - edges that affect the largest number of other uncolored edges have higher prioriy for us, so we color them first.
