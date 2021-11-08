@ECHO ON
cd OneAtmosphere
set Relativepath=%CD%
set dllPath=bin\Debug\OneAtmosphere.dll
set FullPath=%Relativepath%\%dllPath%
cd packages\NUnit.ConsoleRunner.3.11.1\tools
nunit3-console %FullPath% --inprocess --where:"cat==36523"
pause