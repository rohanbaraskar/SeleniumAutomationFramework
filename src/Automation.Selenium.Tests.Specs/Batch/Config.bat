@echo off

COLOR 0a

:: PROJECT FILES
SET ProjectHome=..\..\..\..\..\
SET LivingDocFolder=%ProjectHome%\src\Automation.Selenium.Tests.Specs\bin\Debug\LivingDoc
SET FeatureFolder=%ProjectHome%\src\Automation.Selenium.Tests.Specs\Features
SET SpecsDLL=src\Automation.Selenium.Tests.Specs\bin\Debug\Automation.Selenium.Tests.Specs.dll
SET BDDTestProject="%ProjectHome%\src\Automation.Selenium.Tests.Specs\Automation.Selenium.Tests.Specs.csproj"

:: UTILITIES
SET PicklesExe=%ProjectHome%\packages\Pickles.CommandLine.1.1.0\tools\pickles.exe
SET MSTestExe="%ProgramFiles(x86)%\Microsoft Visual Studio 12.0\Common7\IDE\mstest.exe"
SET SpecflowExecutable=%ProjectHome%\packages\SpecFlow.1.9.0\tools\specflow.exe

:: FILE NAMES
SET StepDefHTMLOutput=StepDefinitionReport.html
SET MsTestResultFile="TestResults.trx"
SET HTMLTestOutput="TestResults.html"

