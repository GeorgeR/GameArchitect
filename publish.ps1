Clear-Host

Push-Location $PSScriptRoot\\src
Remove-Item -Path "$($PSScriptRoot)\\build\\" -Recurse
dotnet build -f netstandard2.0
dotnet build -f netcoreapp2.1
dotnet publish -f netstandard2.0 -o "..\\..\\build\\"
dotnet publish -f netcoreapp2.1 -o "..\\..\\build\\" --self-contained
Pop-Location
Read-Host 'Press enter to continue...' | Out-Null