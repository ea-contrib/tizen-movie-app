& ./env.ps1

docker build --build-arg -f createcertificate.dockerfile -t tizen-certificate .

$env:DISPLAY= $env:host_ip + ":0.0"

$baseDir = $PSScriptRoot -replace '[\\]', '/'
$baseDir = Split-Path $baseDir
write-host "azazaza"
write-host "$env:DISPLAY"
write-host $baseDir

docker run --network=host  -v $baseDir/certificates:/home/tizen/SamsungCertificate/ea_cert -it -e DISPLAY=$env:DISPLAY tizen-certificate
