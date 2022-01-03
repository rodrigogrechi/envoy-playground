# Envoy studies
## setup
1. docker (tested in v20.10.11)
2. docker-compose (tested in v1.29.2)
3. curl (to make request from terminal)
## to run
```shell
docker-compose pull
docker-compose up --build -d
curl -v 0.0.0.0:8080
```