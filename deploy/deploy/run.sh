#!/bin/sh

./env.sh

yes | cp "$pwd/../../src/tizen-client/dist/*.*" ./dist

docker build -f deploy.dockerfile -t tizen-deploy .

docker run --network=host  -v $(dirname $(pwd))/certificates:/home/tizen/SamsungCertificate/ea_cert:ro -it -e TV_IP=$TV_IP tizen-deploy
