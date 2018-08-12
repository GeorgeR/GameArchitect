Clear-Host

Push-Location $PSScriptRoot
$packDir = $PSScriptRoot + "\\nuget\\"
if(!(Test-Path $packDir)) {
    New-Item -ItemType Directory -Force -Path $packDir
}

Write-Host "Finding latest packages to upload"
$projects = Get-ChildItem -Path $PSScriptRoot\src -Recurse | where {$_.extension -eq ".nupkg" }
foreach ($project in $projects) {
    Copy-Item -Path $project.FullName -Destination $packDir
}
Pop-Location

$packages = Get-ChildItem -Path $PSScriptRoot/nuget -Recurse | where { $_.extension -eq ".nupkg" }

$regex = [regex]::new('(.*?)(\d+\.\d+\.\d+)(\.nupkg)')
$latestPackages = @()
foreach ($package in $packages) {
    $fragment = $regex.Match($package)[0].Groups[1].Value

    $latest = Get-ChildItem -Path $PSScriptRoot/nuget -Recurse | where { $_.Name -match $fragment }
    $latestVersion = $latest | ForEach-Object { $regex.Match($_)[0].Groups[2].Value } | %{[System.Version]$_} | Sort-Object
    [array]::Reverse($latestVersion)

    $latest = $latest | where { $_.Name -match $latestVersion[0].ToString() }

    if($latest.Count -eq 0) {
        Write-Error "Null Array"
    }
    else {
        [array]::Reverse($latest)
        if($latestPackages -notcontains $latest[0]) {
            $latestPackages += $latest[0]
        }
    }
}

$latestPackages = $latestPackages | Sort-Object -Unique
Write-Host "Found latest packages"

$keys = (Get-ChildItem Env:Path).Value.Split(";") | where { Test-Path "$($_)\\keys\\keys.json" } 
$keys = Get-Content "$($keys)\\keys\\keys.json" | ConvertFrom-Json

foreach ($package in $latestPackages) {
    dotnet nuget push $package.FullName -k $keys.nuget -s https://api.nuget.org/v3/index.json
}

Read-Host 'Press enter to continue...' | Out-Null