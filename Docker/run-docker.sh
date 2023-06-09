#!/bin/bash

# 启动MariaDB服务
/usr/bin/mysqld_safe &

# 等待MariaDB服务完全启动
while ! mysqladmin ping --silent; do
    sleep 1
done

# 配置MariaDB用户和数据库
mysql -u root -e "ALTER USER 'root'@'localhost' IDENTIFIED BY 'iiLvWUGkiTgq8zL';FLUSH PRIVILEGES;" && \
mysql -u root -p'iiLvWUGkiTgq8zL' -e "CREATE DATABASE ShortLinkDB;CREATE USER 'shortlink'@'%' IDENTIFIED BY 'shortlink';GRANT ALL PRIVILEGES ON ShortLinkDB.* TO 'shortlink'@'%';FLUSH PRIVILEGES;"

# 启动Nginx
service nginx start

# 启动.NET应用程序
dotnet /app/dotnet-build/ShortLinkGeneration.dll
