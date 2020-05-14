#!/bin/bash

docker build -f createcertificate.dockerfile \
	--build-arg IMAGE=eacontrib/tizen-ide \
	--build-arg VERSION=latest \
	-t tizen-certificate .

if [ "$(uname -s)" == "Darwin" ]; then
    host + "$(ifconfig en0 | awk '/inet /{print $2}')"
    DISPLAY=$(ifconfig en0 | awk '/inet /{print $2 ":0"}')
fi

docker run -it --rm --network=host  \
	-v "$(dirname "$(pwd)")/certificates:/home/tizen/SamsungCertificate/ea_cert" \
	-v /tmp/.X11-unix:/tmp/.X11-unix \
	-v "$HOME/.Xauthority:/root/.Xauthority:rw" \
	-e DISPLAY="$DISPLAY" \
	tizen-certificate
