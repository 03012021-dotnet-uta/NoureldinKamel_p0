"use strict";
// initializing variables
let sweet = "sweet";
let salty = "salty";
let sweetnsalty = "sweet’nSalty";

let sweetNum = 3;
let saltyNum = 5;
// 5 and 3 in one
let sweetnsaltyNum = 15;

// determine the words allowed per line
let printsPerLine = 10;

// set the start and the end counters
let startNum = 0;
let endNum = 1000;

// run the whole program
function runSweetnSalty(start, end, printsPerLine) {
  let sweetCounter = 0;
  let saltyCounter = 0;
  let sweetnsaltyCounter = 0;
  let printString = "~~~Starting SweetnSalty~~~";
  // loop from the start till the end
  for (let counter = start; counter < end; counter++) {
    //check if the 10th number is here to add a new line
    if (counter % printsPerLine == 0) {
      printString += "\n";
    }
    // first check if divisible by both for sweatnsalty, add string and increment counter
    if (counter % sweetnsaltyNum == 0) {
      printString += sweetnsalty + "\t";
      sweetnsaltyCounter++;
    }
    // then check if sweet, add string and increment counter
    else if (counter % sweetNum == 0) {
      sweetCounter++;
      printString += sweet + "\t";
    }
    // then check if salty, add string and increment counter
    else if (counter % saltyNum == 0) {
      saltyCounter++;
      printString += salty + "\t";
    }
    // else just add the number, add number
    else {
      printString += counter + "\t";
    }
  }

  // writing the whole sweet, salty, sweetnsalty results
  console.log(printString);

  // writing how many sweets, saltys and sweetnsaltys
  printResult(sweet, sweetCounter);
  printResult(salty, saltyCounter);
  printResult(sweetnsalty, sweetnsaltyCounter);
}

// method for printing the result
function printResult(printString, counter) {
  console.log("the number of " + printString + " printed are: " + counter);
}

// invoke running the program
runSweetnSalty(startNum, endNum, printsPerLine);
