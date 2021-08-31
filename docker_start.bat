docker stop hpc_crud_api_image
docker rm hpc_crud_api_image
docker build --tag hpc_crud_api_image .
docker run -p 8080:80 --name hpc_crud_api_image -d hpc_api_image