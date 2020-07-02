#!/bin/bash

pushd  ../src/server/dotnet/

docker-compose up --build

popd
