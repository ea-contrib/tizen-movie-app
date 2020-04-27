docker build --tag tizen .
$env:DISPLAY="192.168.88.252:0.0"

docker run --network=host -it -e DISPLAY=$env:DISPLAY tizen /bin/sh