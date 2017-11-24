# CacheAndTransposition
Homework for class DS I

You should edit and run only file **Runner/Program.cs** !!

Change paths and arguments so that it matches your version.

Note that I made few minor edits in cachesim.c (only I/O - not inner logic) for easier manipulation using my pipe.

Parameters:
  * OnlyOnce - runs the program for just one instance.
  * ShowOutput - is currently unused and is included only for historical purposes
  * ShowTransposition - when you are debugging the transposition, this checks whether to show the matrices
  * ToCache - indicates whether to use cachesim or transposition-debug-feature
  * StartSize - holds an initial matrix size
  
Program goes throught all combinations of parameters for each increasing size of a matrix and executes desired function. If one of the programs run by this exits with wrong exit code, the program will finish and exit as well.

The implementation of the rest of the programs is as follows:

Transposition.exe -[r,n] x
 -- executes the transposition program with recursive / naive implementation for matrix of size x
 
Outputs will be written in output.txt file located in Runner/bin/Debug/.
