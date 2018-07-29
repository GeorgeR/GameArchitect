Push-Location $PSScriptRoot
dotnet publish -o "..\\..\\build\\"
Pop-Location
Read-Host 'Press enter to continue...' | Out-Null