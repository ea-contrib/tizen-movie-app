#!/bin/sh

TV_IP='192.168.88.248'

docker build -f deploy.dockerfile -t tizen-deploy .

baseDir = $(pwd)
baseDir = "$(dirname $baseDir)"

yes | cp "$pwd/../../src/tizen-client/dist/*.*" ./dist

docker run -it --network=host \
	-v $baseDir/certificates:/home/tizen/SamsungCertificate/ea_cert:ro \
	-e TV_IP=$TV_IP \
	tizen-deploy
