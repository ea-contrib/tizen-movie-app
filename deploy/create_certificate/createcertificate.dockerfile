ARG IMAGE
ARG VERSION
FROM ${IMAGE}:${VERSION}

ENTRYPOINT /home/tizen/tizen-studio/tools/certificate-manager/certificate-manager