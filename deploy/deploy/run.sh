#!/bin/sh

./env.sh

yes | cp "$pwd/../../src/tizen-client/dist/*.*" ./dist

docker build -f deploy.dockerfile -t tizen-deploy .

baseDir = $(pwd)
baseDir = "$(dirname $baseDir)"

docker run --network=host  -v $baseDir/certificates:/home/tizen/SamsungCertificate/ea_cert:ro -it -e TV_IP=$TV_IP tizen-deploy
