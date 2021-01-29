# PyramidChallenge

PyramidChallenge is a solution that take x number of lines with y number of integers as input. Going through the integer values from top to bottom, the program will find the path that provides the maximum possible sum of the numbers for the path, per given the rules below:

1. You will start from the top and move downwards to the last possible node.
2. You must proceed by changing between even and odd numbers subsequently. Suppose that
   you are on an even number, the next number you choose must be odd, or if you are on an
   odd number the next number must be even. In other words, the final path would be Odd ->
   even -> odd -> even …
3. You must reach to the bottom of the pyramid.
4. Assume that there is at least one valid path to the bottom.
5. If there are multiple paths, which result in the same maximum amount, you can choose any
   of them.

## Pyramid.UI appsettings.json

The project Pyramid.UI is a Console Application and have a settingsfile; appsettings.json with the following options

```json
{
  "inputFilePath": "C:\\PyramidTest\\PyramidUI\\InputFile.txt",
  "ForceOddEvenRule": true,
  "NumberOfChildNodes": 2
}
```

## inputFilePath

inputFilePath is the path to the input file that is included in the project.

By default the solution uses a Valuegenerator to avoid exceptions on first run. Should you want to use the file included, make sure to update the inputFilePath to the correct path for the inputFile.

## ForceOddEvenRule

Indicates if the rule number 2 should be used. True; the rule is enforeced; false the rule is ignored

## NumberOfChildNodes

By default, each node has only two children here (except the bottom row). Should one want to expand the number of nodes to more than 2, then NumberOfChildNodes should be set to x number of child nodes. **_It is assumed that the input from either file or value genreator is supplying the correct number of values. InputFile nor ValueGenerator is supporting a NumberOfChildNodes higher than 2_**
