Set-Location "C:\Tools\xUnit20\"
& "xunit.console.x86.exe" $(Get-ChildItem $PSScriptRoot *.Test.dll -Recurse | % { $($_.FullName) }) -appveyor