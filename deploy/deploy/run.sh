#!/bin/bash

./env.sh
export TV_IP='192.168.100.3'

yes | cp $(dirname $(dirname $(pwd)))/src/tizen-client/dist/* ./dist

docker build -f deploy.dockerfile -t tizen-deploy .

docker run --network=host  -v $(dirname $(pwd))/certificates:/home/tizen/SamsungCertificate/ea_cert:ro -it -e TV_IP=$TV_IP tizen-deploy
