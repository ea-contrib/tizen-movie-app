#!/bin/sh

./env.sh

docker build -f createcertificate.dockerfile -t tizen-certificate .

docker run --network=host  -v $(dirname $(pwd))/certificates:/home/tizen/SamsungCertificate/ea_cert -v /tmp/.X11-unix:/tmp/.X11-unix --volume="$HOME/.Xauthority:/root/.Xauthority:rw" -it -e DISPLAY=$DISPLAY tizen-certificate
