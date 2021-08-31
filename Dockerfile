#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HPCAPI/HPCAPI.csproj", "HPCAPI/"]
RUN dotnet restore "HPCAPI/HPCAPI.csproj"
COPY . .
WORKDIR "/src/HPCAPI"
RUN dotnet build "HPCAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HPCAPI.csproj" -c Release -o /app/publish

#Angular build
FROM node as nodebuilder

# set working directory
RUN mkdir /usr/src/app
WORKDIR /usr/src/app

# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH


# install and cache app dependencies
COPY HPCAPI/ClientApp/package.json /usr/src/app/package.json
RUN npm cache clean --force  
RUN npm install
#RUN npm install -g npm@6
#RUN npm install -g @angular/cli@8 --unsafe
#RUN npm install --save-dev @angular-devkit/build-angular
#RUN npm install @angular/compiler-cli --save
#RUN npm install @angular/compiler --save
#RUN npm i ngx-toastr@latest --save
# add app

COPY HPCAPI/ClientApp/. /usr/src/app

RUN npm run build

#End Angular build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/ClientApp/dist
COPY --from=nodebuilder /usr/src/app/dist/. /app/ClientApp/dist/
ENTRYPOINT ["dotnet", "HPCAPI.dll"]