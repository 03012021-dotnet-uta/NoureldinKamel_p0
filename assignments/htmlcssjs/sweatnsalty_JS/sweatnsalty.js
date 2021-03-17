"use strict";
// initializing variables
let sweet = "sweet";
let salty = "salty";
let sweetnsalty = "sweetnsalty";

let sweetNum = 3;
let saltyNum = 5;
// 5 and 3 in one
let sweetnsaltyNum = 15;

// determine the words allowed per line
let printsPerLine = 10;

// set the start and the end counters
let startNum = 0;
let endNum = 1000;

// /** create a check */
// function checkNumber(num, print) {
//   this.checkNum = num;
//   this.checkPrint = print;

//   function check(n) {
//     if (n % this.checkNum == 0) {
//       return print;
//     }
//     return n;
//   }
// }

// // creating the check for each case
// let sweetCheck = checkNumber(sweetNum, sweet);
// let saltyCheck = checkNumber(saltyNum, salty);
// let sweetnsaltyCheck = checkNumber(sweetnsaltyNum, sweetnsalty);

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

  console.log(printString);

  printResult(sweet, sweetCounter);
  printResult(salty, saltyCounter);
  printResult(sweetnsalty, sweetnsaltyCounter);
}

function printResult(printString, counter) {
  console.log("the number of " + printString + " printed are: " + counter);
}

runSweetnSalty(startNum, endNum, printsPerLine);
