@echo off

call Config.bat

echo ::: Generating Step Definition Report
%SpecflowExecutable% stepdefinitionreport %BDDTestProject% /out:%StepDefHTMLOutput%

echo ::: Opening HTML output
%StepDefHTMLOutput%