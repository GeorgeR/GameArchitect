Clear-Host

Push-Location $PSScriptRoot\\src
Remove-Item -Path "$($PSScriptRoot)\\build\\" -Recurse
dotnet publish -f netstandard2.0 -o "..\\..\\build\\"
dotnet publish -f netcoreapp2.1 -o "..\\..\\build\\"
Pop-Location
Read-Host 'Press enter to continue...' | Out-Null