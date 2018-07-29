Clear-Host
Invoke-Expression -Command $PSScriptRoot/src/copy_nuget.ps1
$packages = Get-ChildItem -Path $PSScriptRoot/nuget -Recurse | where { $_.extension -eq ".nupkg" }
$keys = (Get-ChildItem Env:Path).Value.Split(";") | where { Test-Path "$($_)\\keys.json" } 
$keys = Get-Content "$($keys)\\keys.json" | ConvertFrom-Json
foreach ($package in $packages) {
    dotnet nuget push $package.FullName -k $keys.nuget -s https://api.nuget.org/v3/index.json
}
Read-Host 'Press enter to continue...' | Out-Null