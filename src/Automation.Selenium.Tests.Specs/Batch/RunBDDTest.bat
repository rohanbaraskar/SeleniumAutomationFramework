@echo off

call Config.bat

echo ::: Reading parameters..
SET TAGS=%1

echo ::: House Keeping...
if Exist %MsTestResultFile% del %MsTestResultFile% 

echo ::: Running BDD tests using MSTest runnner...
if "%TAGS%" == "" (
    echo ::: No tags detected. Running all tests
    %MSTestExe% /testcontainer:%ProjectHome%%SpecsDLL% /resultsfile:%MsTestResultFile% 
) ELSE (
    echo ::: Running tests with tags %TAGS%
    %MSTestExe% /testcontainer:%ProjectHome%%SpecsDLL% /category:"%TAGS%" /resultsfile:%MsTestResultFile% 
)

echo ::: Generating Test Result Output %HTMLTestOutput% ...
%SpecflowExecutable% mstestexecutionreport %BDDTestProject% /testResult:%MsTestResultFile% /out:%HTMLTestOutput%

echo ::: Opening HTML output
%HTMLTestOutput%