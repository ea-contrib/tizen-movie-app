& ./env.ps1

docker build -f createcertificate.dockerfile `
--build-arg IMAGE=eacontrib/tizen-ide `
--build-arg VERSION=latest `
-t tizen-certificate .

$env:DISPLAY= $env:host_ip + ":0.0"

$baseDir = $PSScriptRoot -replace '[\\]', '/'
$baseDir = Split-Path $baseDir

write-host "$env:DISPLAY"

docker run --network=host -it --rm `
-v $baseDir/certificates:/home/tizen/SamsungCertificate/ea_cert `
-e DISPLAY=$env:DISPLAY `
-tizen-certificate
