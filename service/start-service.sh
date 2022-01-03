#!/bin/sh
dotnet /app/service.dll &
/usr/local/bin/envoy -c /etc/envoy.yaml --service-cluster "weather-forecast"