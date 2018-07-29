Push-Location $PSScriptRoot
$packDir = $PSScriptRoot + "\\..\\nuget"
Write-Host $packDir
$projects = Get-ChildItem -Path $PSScriptRoot -Recurse | where {$_.extension -eq ".nupkg" }
foreach ($project in $projects) {
    Copy-Item -Path $project.FullName -Destination $packDir
}
Pop-Location
Read-Host 'Press enter to continue...' | Out-Null