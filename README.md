# Solution:
Asynchronous requests with memory cache to store information locally, avoiding new requests.
The cache lifetime has been set to 1 minute for example purposes.

## Running the application:
docker build -t beststories-img .

docker run -d -p 5001:80 --name beststories-api beststories-img

## Possible improvements:
- Distributed cache utilization (example using Redis e docker-compose)
- Escalate the application using multiple instances of the service
    Ex.:
    
    docker swarm init
    
    docker build -t beststories-img .
    
    docker service create --publish 5001:80 --name beststories-api beststories-img
    
    docker service scale beststories-api=5
