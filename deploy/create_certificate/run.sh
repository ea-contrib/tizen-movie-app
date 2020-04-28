#!/bin/sh

./env.sh

docker build -f createcertificate.dockerfile -t tizen-certificate .

baseDir = $(pwd)
baseDir = "$(dirname $baseDir)"

docker run --network=host  -v $baseDir/certificates:/home/tizen/SamsungCertificate/ea_cert --volume="$HOME/.Xauthority:/root/.Xauthority:rw" -it -e DISPLAY=$DISPLAY tizen-certificate
