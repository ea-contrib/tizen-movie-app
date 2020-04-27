docker build --tag tizen .
docker run tizen tizen cli-config -l
docker run -it tizen /bin/sh