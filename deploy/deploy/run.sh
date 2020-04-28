#!/bin/sh

./env.sh

docker build -f deploy.dockerfile -t tizen-deploy .

baseDir = $(pwd)
baseDir = "$(dirname $baseDir)"

yes | cp "$pwd/../../src/tizen-client/dist/*.*" ./dist

docker run --network=host  -v $baseDir/certificates:/home/tizen/SamsungCertificate/ea_cert:ro -it -e TV_IP=$TV_IP tizen-deploy
