& ./env.ps1

docker build -f deploy.dockerfile -t tizen-deploy .

Get-ChildItem -Path C:\Temp -Include *.* -exclude .gitkeep -File -Recurse | foreach { $_.Delete()}

$distDirectory = "$PSScriptRoot\..\..\src\tizen-client\dist\*.*"
Copy-item -Force -Recurse -Verbose $distDirectory -Destination ".\dist"

$baseDir = $PSScriptRoot -replace '[\\]', '/'
$baseDir = Split-Path $baseDir

docker run --network=host -e TV_IP=$env:TV_IP  -v $baseDir/certificates:/home/tizen/SamsungCertificate/ea_cert:ro -it tizen-deploy
