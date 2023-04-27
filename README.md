# BetaUtils code styling and conventions

In order for pull request to be valid for code review it has to follow code styling and conventions defined in this file.

# Creating working branch

New branch where changes will be made to existing code base have to contain tree values:

1. ```Type of change``` that will be done
2. Name of the feature where that change will be made on
3. Short description of the change

## Type of changes:
```feature``` - is for adding, refactoring or removing a feature\
```bugfix``` - is for fixing a bug\
```hotfix``` - is for changing code with a temporary solution and/or without following the usual process (usually because of an emergency)\
```test``` is for experimenting outside of an issue/ticket

Example of branch for creating ```README.md``` file:

```feature/readme/creating_readme```
# Commit
The commit message should have certain qualities:

```Length```: Commit message should ideally be no longer than 50 - 75 characters.\
```Content```: Be direct, try to eliminate filler words and phrases in these sentences (examples: though, maybe, I think, kind of). Think like a journalist.\
```Capitalization and Punctuation```: Capitalize the first word and do not end in punctuation.
