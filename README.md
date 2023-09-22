# 05-assignment
1. SQL scripts to create database
2. Reading data with SQL Client
## Team mates
* [Frederik Strøm Friborg](https://github.com/Freddo2300)
* [Mansour Hamidi](https://github.com/MansourHamidi94)
* [Marc Pedersen](https://github.com/BareMarcP)
## Getting started
Clone repo, like so:
```term
git clone https://github.com/Freddo2300/05-assignment.git
```
Build project and install dependencies, like so:
```term
dotnet build
```
## Establishing connection to database
As none of the team are Windows users, we have had a tough time installing and using MS SQL. Therefore, we decided to go with docker.

The following tools are required:
* <code>docker</code>
* <code>docker-compose</code>

Assuming that the above are installed, proceed like so:

1. ```docker pull usildevops/mssql-docker-enhanced:latest```
2. ```cd docker```
3. ```docker-compose up -d```