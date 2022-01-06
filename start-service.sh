#!/bin/sh
dotnet /app/${SOLUTION} &
/usr/local/bin/envoy -c /etc/envoy.yaml --service-cluster "discovery-${SERVICE_NAME}"