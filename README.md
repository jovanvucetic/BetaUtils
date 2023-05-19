Build status (main branch):\
[![Code Coverage & Quality](https://github.com/jovanvucetic/BetaUtils/actions/workflows/Code%20Coverage%20&%20Quality.yml/badge.svg)](https://github.com/jovanvucetic/BetaUtils/actions/workflows/Code%20Coverage%20&%20Quality.yml)\
Code coverage (main branch):\
[![codecov](https://codecov.io/gh/jovanvucetic/BetaUtils/branch/main/graph/badge.svg?token=HLRMMX05NP)](https://codecov.io/gh/jovanvucetic/BetaUtils)
<!-- TOC -->
* [BetaUtils code styling and conventions](#betautils-code-styling-and-conventions)
* [Creating working branch](#creating-working-branch)
  * [Type of changes:](#type-of-changes)
* [Commit](#commit)
* [Unit tests and coverage](#unit-tests-and-coverage)
    * [For changes to show up it might take up from 3-5 minutes!](#for-changes-to-show-up-it-might-take-up-from-3-5-minutes)
  * [Locally creating code coverage](#locally-creating-code-coverage)
    * [Generating code coverage and report](#generating-code-coverage-and-report)
    * [For repeating test coverage generation steps 1, 2, 3, 4, 5 have to be repeated.](#for-repeating-test-coverage-generation-steps-1-2-3-4-5-have-to-be-repeated)
<!-- TOC -->

# BetaUtils code styling and conventions

In order for pull request to be valid for code review it has to follow code styling and conventions defined in this
file.

# Creating working branch

New branch where changes will be made to existing code base have to contain tree values:

1. ```Type of change``` that will be done
2. Name of the feature where that change will be made on
3. Short description of the change

## Type of changes:

```feature``` - is for adding, refactoring or removing a feature\
```bugfix``` - is for fixing a bug\
```hotfix``` - is for changing code with a temporary solution and/or without following the usual process (usually
because of an emergency)\
```test``` is for experimenting outside of an issue/ticket

Example of branch for creating ```README.md``` file:

```feature/readme/creating_readme```

# Commit

The commit message should have certain qualities:

```Length```: Commit message should ideally be no longer than 50 - 75 characters.\
```Content```: Be direct, try to eliminate filler words and phrases in these sentences (examples: though, maybe, I
think, kind of). Think like a journalist.\
```Capitalization and Punctuation```: Capitalize the first word and do not end in punctuation.

# Unit tests and coverage

Each pull request / push will trigger github action which will check for code coverage and generate report.\
Report can be seen on: https://app.codecov.io/gh/jovanvucetic/BetaUtils
by selecting branch that you want to see code coverage of (as shown on screenshot).

If code coverage is under 80% build will fail!!

![img.png](./DocumentationResource/Screenshot%201.png)

### For changes to show up it might take up from 3-5 minutes!

## Locally creating code coverage

In order to manually trigger code coverage you will have to install `ReportGenerator` by running command:\

```dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.1.20```

### Generating code coverage and report

1. Make sure that folder by name `TestResults` does not exist in solution root, if folder exist, delete it (it may cause
   false report).\
   ![img.png](./DocumentationResource/Screenshot%202.png)

2. In terminal run
   command: `dotnet test --warnaserror --collect:"XPlat Code Coverage" --logger trx --results-directory TestResults /p:CoverletOutputFormat=lcov /p:MergeWith=coverage.json`,
   running this command will create previous step deleted `TestResult` folder with test data.

3. In terminal run
   command: `reportgenerator "-reports:TestResults\**\coverage.cobertura.xml" "-targetdir:CoverageReports" "-reporttypes:Html"`,
   running this command will create new folder `CoverageReports`.

4. Newly created `CoverogaReports` folder will contain `index.html`.

5. Opening `index.html` will show solution structure where you can navigate to code which coverage you want to check
   out.

### For repeating test coverage generation steps 1, 2, 3, 4, 5 have to be repeated.
