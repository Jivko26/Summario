docker login -u jivko26 -p @Alenka16
docker build -f Dockerfile -t docker.io/jivko26/summario-ui .
docker push docker.io/jivko26/summario-ui