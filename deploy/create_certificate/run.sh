#!/bin/sh

docker build -f createcertificate.dockerfile -t tizen-certificate .

docker run -it --network=host  \
	-v "$(dirname "$(pwd)")/certificates:/home/tizen/SamsungCertificate/ea_cert" \
	-v /tmp/.X11-unix:/tmp/.X11-unix \
	-v "$HOME/.Xauthority:/root/.Xauthority:rw" \
	-e DISPLAY="$DISPLAY" \
	tizen-certificate
